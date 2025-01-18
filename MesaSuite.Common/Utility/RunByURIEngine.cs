using MesaSuite.Common.Attributes;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MesaSuite.Common.Utility
{
    public class RunByURIEngine
    {
        public delegate void OpenFormDelegate(Form form, NameValueCollection additionalData);
        public delegate object MainThreadInvoker(Delegate method);

        private Dictionary<string, Type> _typesByUriPath = new Dictionary<string, Type>();
        private OpenFormDelegate _openFormDelegate;
        private MainThreadInvoker _mainThreadInvoker;
        
        public RunByURIEngine(OpenFormDelegate openFormDelegate, MainThreadInvoker methodInvoker)
        {
            _openFormDelegate = openFormDelegate;
            _mainThreadInvoker = methodInvoker;
            foreach(Type type in Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(Form).IsAssignableFrom(t)))
            {
                UriReachableAttribute uriReachableAttribute = type.GetCustomAttribute<UriReachableAttribute>();
                if (uriReachableAttribute == null)
                {
                    continue;
                }

                string uriPath = uriReachableAttribute.UriPath;
                if (uriPath.Contains("{"))
                {
                    uriPath = uriPath.Substring(0, uriPath.IndexOf("{"));
                }

                if (uriPath.EndsWith("/"))
                {
                    uriPath = uriPath.Substring(0, uriPath.Length - 1);
                }

                _typesByUriPath.Add(uriPath, type);
            }
        }

        public void CheckArgsForRun(string[] args)
        {
            if (args != null)
            {
                foreach (string arg in args)
                {
                    Match match = Regex.Match(arg, @"\bmesasuite://[^\s]+");
                    if (match.Success && Uri.TryCreate(match.Value, UriKind.Absolute, out Uri uri) && uri.Scheme.Equals("mesasuite", StringComparison.OrdinalIgnoreCase))
                    {
                        RunByUri(uri);
                        return;
                    }
                }
            }
        }

        public async void RunByUri(Uri uri)
        {
            if (!uri.Scheme.Equals("mesasuite", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            string lookupPath = uri.AbsolutePath.Substring(1);
            string[] lookupPathParts = lookupPath.Split('/');
            Type type = null;
            for(int i = lookupPathParts.Length; i > 0; i--)
            {
                string lookupPathAttempt = string.Join("/", lookupPathParts.Take(i));
                type = _typesByUriPath.GetOrDefault(lookupPathAttempt);
                if (type != null)
                {
                    break;
                }
            }

            if (type == null)
            {
                return;
            }

            Func<Form> createForm = () => (Form)Activator.CreateInstance(type);
            Form form = null;
            int attempts = 0;
            while(form == null && attempts < 10)
            {
                form = _mainThreadInvoker(createForm) as Form;
                if (form != null)
                {
                    break;
                }
                await Task.Delay(500);
                attempts++;
            }

            if (form == null)
            {
                return;
            }

            // Set properties
            string parameterPath = type.GetCustomAttribute<UriReachableAttribute>().UriPath;
            if (parameterPath.Contains("{"))
            {
                string[] parameterPathParts = parameterPath.Split('/');
                string[] inboundUriParts = uri.AbsolutePath.Substring(1).Split('/');

                int parametersStartAtIndex = -1;
                for(int i = 0; i < parameterPathParts.Length; i++)
                {
                    if (parameterPathParts[i].StartsWith("{"))
                    {
                        parametersStartAtIndex = i;
                        break;
                    }
                }

                if (parametersStartAtIndex != -1 && inboundUriParts.Length > parametersStartAtIndex)
                {
                    for(int i = parametersStartAtIndex; i < inboundUriParts.Length; i++)
                    {
                        string parameterName = parameterPathParts[i].Substring(1, parameterPathParts[i].Length - 2);
                        string parameterValue = inboundUriParts[i];

                        PropertyInfo propertyInfo = form.GetType().GetProperty(parameterName, BindingFlags.Public | BindingFlags.Instance);
                        if (propertyInfo == null)
                        {
                            continue;
                        }

                        try
                        {
                            Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                            object convertedValue = Convert.ChangeType(parameterValue, propertyType);
                            propertyInfo.SetValue(form, convertedValue);
                        }
                        catch { }
                    }
                }
            }
            _openFormDelegate(form, HttpUtility.ParseQueryString(uri.Query));
        }
    }
}
