using CefSharp.DevTools.CSS;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesaSuite.Common.Data
{
    public abstract class DataAccess
    {
        public string Resource { internal get; set; }
        public APIs API { internal get; set; }
        public bool UseHTTPS { internal get; set; } = true;
        public bool RequestSuccessful { get; protected set; }
        public bool RequireAuthentication { internal get; set; } = true;
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public List<string> RequestFields { get; set; } = new List<string>();
        public List<string> AdditionalFields { get; set; } = new List<string>();
        public bool SuppressErrors { get; set; }
        public string LastError { get; private set; }

        public DataAccess(APIs api, string resource)
        {
            API = api;
            Resource = resource;
        }

        protected string GetResourceURI()
        {
            if (GlobalSettings.InternalEditionMode)
            {
                return new InternalEditionResourceWriter().Write(this);
            }

            string resourceWriterClass = ConfigurationManager.AppSettings.Get("MesaSuite.Common.ResourceWriter." + API.ToString());
            if (string.IsNullOrEmpty(resourceWriterClass))
            {
                resourceWriterClass = ConfigurationManager.AppSettings.Get("MesaSuite.Common.ResourceWriter");
            }

            Type resourceWriterType = Assembly.GetExecutingAssembly().GetType(resourceWriterClass);
            if (resourceWriterType == null)
            {
                throw new ConfigurationErrorsException("Resource Writer must be defined in the configuration file");
            }

            IResourceWriter writer = (IResourceWriter)Activator.CreateInstance(resourceWriterType);
            return writer.Write(this);
        }

        private bool _invalidResponseRetry;
        protected delegate Task<string> ResponseWebExceptionRetryCallback();
        protected Task<string> HandleResponseWebException(WebException ex, ResponseWebExceptionRetryCallback retryCallback)
        {
            LastError = null;

            if (ex.Response != null)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (!_invalidResponseRetry)
                    {
                        Authentication.GetAuthToken(true);
                        _invalidResponseRetry = true;
                        return retryCallback();
                    }
                    else
                    {
                        LastError = "You must sign in to view this content";
                        if (!SuppressErrors)
                        {
                            MessageBox.Show(LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        RequestSuccessful = false;
                        return Task.FromResult<string>(null);
                    }
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    LastError = "You do not have sufficient permissions to view this content";
                    if (!SuppressErrors)
                    {
                        MessageBox.Show(LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RequestSuccessful = false;
                    return Task.FromResult<string>(null);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    StringBuilder errorText = new StringBuilder("An error occurred with your request");

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string additional = reader.ReadToEnd();

                        var messageObject = new { Message = string.Empty };

                        try
                        {
                            messageObject = JsonConvert.DeserializeAnonymousType(additional, messageObject);
                        }
                        catch { }

                        if (!string.IsNullOrWhiteSpace(messageObject?.Message))
                        {
                            errorText = new StringBuilder(messageObject.Message);
                        }
                        else if (!string.IsNullOrEmpty(additional))
                        {
                            errorText.AppendLine();
                            errorText.Append(additional);
                        }
                    }

                    LastError = errorText.ToString();
                    if (!SuppressErrors)
                    {
                        MessageBox.Show(LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RequestSuccessful = false;
                    return Task.FromResult<string>(null);
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    LastError = "The resource you requested was not found";
                    if (!SuppressErrors)
                    {
                        MessageBox.Show(LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RequestSuccessful = false;
                    return Task.FromResult<string>(null);
                }
            }

            throw ex;
        }

        protected void TimeZoneCorrection(object anObject, bool toLocalTime)
        {
            if (anObject == null)
            {
                return;
            }

            if (typeof(IEnumerable).IsAssignableFrom(anObject.GetType()) && anObject.GetType().IsGenericType)
            {
                IEnumerable objects = (IEnumerable)anObject;
                foreach (object subObject in objects)
                {
                    TimeZoneCorrection(subObject, toLocalTime);
                }

                return;
            }

            foreach (PropertyInfo property in anObject.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(DateTime?) && property.GetValue(anObject) == null)
                {
                    continue;
                }

                if (property.PropertyType == typeof(DateTime) && ((DateTime)property.GetValue(anObject) == default(DateTime) || (DateTime)property.GetValue(anObject) == DateTime.MaxValue))
                {
                    continue;
                }

                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType.IsGenericType)
                {
                    IEnumerable objects = (IEnumerable)property.GetValue(anObject);
                    if (objects != null)
                    {
                        foreach (object subObject in objects)
                        {
                            TimeZoneCorrection(subObject, toLocalTime);
                        }
                    }

                    continue;
                }

                if (property.PropertyType.Assembly.GetReferencedAssemblies().Select(an => an.FullName).Contains(Assembly.GetExecutingAssembly().FullName))
                {
                    TimeZoneCorrection(property.GetValue(anObject), toLocalTime);
                    continue;
                }

                if (property.SetMethod == null)
                {
                    continue;
                }

                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    DateTime currentValue;
                    if (property.PropertyType == typeof(DateTime?))
                    {
                        currentValue = ((DateTime?)property.GetValue(anObject)).Value;
                    }
                    else
                    {
                        currentValue = (DateTime)property.GetValue(anObject);
                    }

                    if (toLocalTime)
                    {
                        currentValue = currentValue.ConvertToLocalTime();
                    }
                    else
                    {
                        currentValue = currentValue.ConvertToServerTime();
                    }
                    property.SetValue(anObject, currentValue);
                }
            }
        }

        public enum APIs
        {
            [EnumValue("mcsync")]
            MCSync,
            [EnumValue("system")]
            SystemManagement,
            [EnumValue("company")]
            CompanyStudio,
            [EnumValue("gov")]
            GovernmentPortal,
            [EnumValue("tow")]
            TowTickets,
            [EnumValue("fleet")]
            FleetTracking
        }
    }
}
