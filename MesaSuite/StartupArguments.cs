using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MesaSuite
{
    public class StartupArguments
    {
        public static string FolderToDelete { get; set; }
        public static int UpdaterProcessID { get; set; } = -1;
        public static string Run { get; set; }
        public static Uri RunUri { get; set; }

        public static Dictionary<string, List<string>> ArgumentsByPrefix = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public static void SetupArguments(string[] args)
        {
            StringBuilder argCombiner = new StringBuilder();
            foreach(string arg in args)
            {
                if (argCombiner.Length > 0)
                {
                    argCombiner.Append(" ");
                }

                argCombiner.Append(arg);
            }

            string[] betterArgs = argCombiner.ToString().Split('-');
            for (int i = 1; i < betterArgs.Length; i++)
            {
                string arg = betterArgs[i];
                string[] subargs = arg.Split(' ');
                if (arg.StartsWith("folder"))
                {
                    if (subargs.Length < 2)
                    {
                        continue;
                    }

                    FolderToDelete = subargs[1];
                }
                else if (arg.StartsWith("processID"))
                {
                    if (subargs.Length < 2 || !int.TryParse(subargs[1], out int processID))
                    {
                        continue;
                    }

                    UpdaterProcessID = processID;
                }
                else if (arg.StartsWith("run"))
                {
                    if (subargs.Length < 2)
                    {
                        continue;
                    }

                    Run = subargs[1];
                }
                else if (arg.Contains("."))
                {
                    string prefix = arg.Substring(0, arg.IndexOf('.'));

                    if (!ArgumentsByPrefix.ContainsKey(prefix))
                    {
                        ArgumentsByPrefix[prefix] = new List<string>();
                    }

                    ArgumentsByPrefix[prefix].Add(arg);
                }
            }

            foreach(string arg in args)
            {
                Match match = Regex.Match(arg, @"\bmesasuite://[^\s]+");
                if (match.Success && Uri.TryCreate(match.Value, UriKind.Absolute, out Uri uri) && uri.Scheme.Equals("mesasuite", StringComparison.OrdinalIgnoreCase))
                {
                    RunUri = uri;
                }
            }
        }

        public static string[] GetArgsForApp(string appName)
        {
            string[] args = null;
            if (ArgumentsByPrefix.ContainsKey(appName))
            {
                args = ArgumentsByPrefix[appName].ToArray();
            }

            if (RunUri != null)
            {
                args = args ?? new string[0];
                if (RunUri.Authority.Equals(appName, StringComparison.OrdinalIgnoreCase))
                {
                    Array.Resize(ref args, args.Length + 1);
                    args[args.Length - 1] = RunUri.ToString();
                }
            }

            return args;
        }
    }
}
