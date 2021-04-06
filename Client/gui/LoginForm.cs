using System;
using System.Windows.Forms;

namespace Client.gui
{
    public partial class LoginForm : Form
    {

        private MainController ctrl;
        
        public LoginForm(MainController ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }
        
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var username = textBoxPassword.Text;
            var password = textBoxPassword.Text;

            try
            {
                ctrl.Login(username, password);
                var mainForm = new MainForm(ctrl) {Text = @"Welcome " + username};
                mainForm.LoadData();
                mainForm.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, @"Login error");
            }
        }
    }
}