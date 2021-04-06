using System;
using System.Windows.Forms;
using Client.gui;
using Networking;
using Services;


namespace Client
{
    public class StartChatClient
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IContestServices server = new ContestServerObjectProxy("127.0.0.1", 55555);
            var ctrl = new MainController(server);
            var loginForm = new LoginForm(ctrl);
            Application.Run(loginForm);
        }
    }
}