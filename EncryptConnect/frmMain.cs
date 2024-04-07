using System;
using System.Windows.Forms;

namespace EncryptConnect
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            lblPower.Text = string.Format(lblPower.Text, DateTime.Now.Year);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                txtKeys.Text = "";

                #region Rang buoc
                if (txtServer.Text == "")
                {
                    MessageBox.Show("Please type <Server name>", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtServer.Focus();
                    return;
                }
                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Please type <Username>.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please type <Password>.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }

                if (txtDatabase.Text == "")
                {
                    MessageBox.Show("Please type <Database name>.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }
                #endregion

                string Conn = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                    txtServer.Text, txtDatabase.Text, txtUserName.Text, txtPassword.Text);
                //Check connect
                System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(Conn);
                sqlConn.Open();

                txtKeys.Text = EncDec.Encrypt(Conn, "afhkjafjkafsadjhfaiusdhfadsf");

                sqlConn.Close();
            }
            catch {
                MessageBox.Show("No connection. Please check your network connection.", "Notice", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtKeys.Text = EncDec.Decrypt(txtKeys.Text, "afhkjafjkafsadjhfaiusdhfadsf");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtKeys_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtKeys.Focus())
                txtKeys.SelectAll();
        }

        private void lblPower_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://beesky.vn/lien-he");
        }
    }
}
