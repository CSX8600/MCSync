using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevTools.Properties;

namespace DevTools
{
    public static class DevBackendAuth
    {
        public static event EventHandler IsRunningChanged;

        private static TcpListener listener;
        private static Thread authThread;
        private static bool _isRunning;

        private static List<string> Groups = new List<string>();
        private static Dictionary<string, User> UsersByUsername = new Dictionary<string, User>();

        public static void Start()
        {
            if (IsRunning) return;

            UsersByUsername = new Dictionary<string, User>();
            Groups = new List<string>();

            using (StringReader reader = new StringReader(Resources.Users))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }

                    string[] values = line.Split(',');
                    User newUser = new User();
                    newUser.Username = values[0];
                    newUser.Password = values[1];
                    newUser.FirstName = values[2];
                    newUser.LastName = values[3];
                    newUser.Email = values[4];

                    if (bool.TryParse(values[5], out bool active))
                    {
                        newUser.Active = active;
                    }

                    string membersOf = values[6];
                    string[] memberships = membersOf.Split(';');
                    foreach(string membership in memberships)
                    {
                        newUser.MemberOf.Add(membership);
                    }

                    UsersByUsername.Add(newUser.Username, newUser);
                }
            }

            using(StringReader reader = new StringReader(Resources.Groups))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }

                    Groups.Add(line);
                }
            }

            listener = new TcpListener(IPAddress.Any, 48175);
            listener.Start();

            authThread = new Thread(new ThreadStart(Listen));
            authThread.IsBackground = true;
            authThread.Start();
        }

        private static void Listen()
        {
            IsRunning = true;
            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    try
                    {
                        using (BinaryReader reader = new BinaryReader(client.GetStream()))
                        using (BinaryWriter writer = new BinaryWriter(client.GetStream()))
                        {
                            string command = reader.ReadString();
                            HandleCommand(command, reader, writer);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while interacting with the client:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        client.Close();
                    }
                }
            }
            catch (SocketException se) { }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred while listening for a client:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                IsRunning = false;
            }
        }

        public static void Stop()
        {
            if (!IsRunning) return;

            listener.Stop();
        }

        private static void HandleCommand(string command, BinaryReader reader, BinaryWriter writer)
        {
            switch(command)
            {
                case "GetAllUsers":
                    GetAllUsers(writer);
                    break;
                case "GetAllGroups":
                    GetAllGroups(writer);
                    break;
                case "GetUserInformation":
                    GetUserInformation(reader, writer);
                    break;
                case "SetUserInformation":
                    SetUserInformation(reader);
                    break;
                case "CreateUserInformation":
                    CreateUserInformation(reader);
                    break;
                case "DisableUser":
                    DisableUser(reader);
                    break;
                case "VerifyUser":
                    VerifyUser(reader, writer);
                    break;
            }
        }

        private static void GetAllUsers(BinaryWriter writer)
        {
            writer.Write(UsersByUsername.Count);
            foreach (string username in UsersByUsername.Keys)
            {
                writer.Write(username);
            }
        }

        private static void GetAllGroups(BinaryWriter writer)
        {
            writer.Write(Groups.Count);
            foreach(string group in Groups)
            {
                writer.Write(group);
            }
        }

        private static void GetUserInformation(BinaryReader reader, BinaryWriter writer)
        {
            string username = reader.ReadString();

            if (UsersByUsername.TryGetValue(username, out User user))
            {
                writer.Write(user.FirstName);
                writer.Write(user.LastName);
                writer.Write(user.Email);
                writer.Write(user.MemberOf.Count);
                foreach(string membership in user.MemberOf)
                {
                    writer.Write(membership);
                }
            }
            else
            {
                writer.Write("");
                writer.Write("");
                writer.Write("");
                writer.Write(0);
            }
        }

        private static void SetUserInformation(BinaryReader reader)
        {
            string username = reader.ReadString();

            if (UsersByUsername.TryGetValue(username, out User user))
            {
                user.FirstName = reader.ReadString();
                user.LastName = reader.ReadString();
                user.Email = reader.ReadString();
                user.Active = true;

                int iterations = reader.ReadInt32();
                for(int i = 0; i < iterations; i++)
                {
                    string group = reader.ReadString();
                    if (!user.MemberOf.Contains(group, StringComparer.OrdinalIgnoreCase))
                    {
                        user.MemberOf.Add(reader.ReadString());
                    }
                }
            }
            else // Still need to consume data
            {
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                int iterations = reader.ReadInt32();
                for (int i = 0; i < iterations; i++)
                {
                    reader.ReadString();
                }
            }
        }

        private static void CreateUserInformation(BinaryReader reader)
        {
            User user = new User();
            user.Username = reader.ReadString();
            user.Password = reader.ReadString();
            user.FirstName = reader.ReadString();
            user.LastName = reader.ReadString();
            user.Email = reader.ReadString();

            int iterations = reader.ReadInt32();
            for(int i = 0; i < iterations; i++)
            {
                user.MemberOf.Add(reader.ReadString());
            }

            UsersByUsername[user.Username] = user;
        }

        private static void DisableUser(BinaryReader reader)
        {
            string username = reader.ReadString();
            if (UsersByUsername.TryGetValue(username, out User user))
            {
                user.Active = false;
            }
        }

        private static void VerifyUser(BinaryReader reader, BinaryWriter writer)
        {
            string username = reader.ReadString();
            string password = reader.ReadString();

            if (!UsersByUsername.TryGetValue(username, out User user))
            {
                writer.Write(false);
                return;
            }

            writer.Write(user.Password == password);
        }

        public static bool IsRunning
        {
            get { return _isRunning; }
            set 
            {
                _isRunning = value;
                IsRunningChanged?.Invoke(null, new EventArgs());
            }
        }

        private class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public bool Active { get; set; } = true;
            public List<string> MemberOf { get; set; } = new List<string>();
        }
    }
}
