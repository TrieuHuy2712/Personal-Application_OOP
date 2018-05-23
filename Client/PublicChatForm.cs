using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Client
{
    public partial class PublicChatForm : Form
    {
        public readonly LoginForm formLogin = new LoginForm();
        public PublicChatForm()
        {
            InitializeComponent();
        }
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            formLogin.Client.Received += _client_Received;
            formLogin.Client.Disconnected += Client_Disconnected;
            
            Text = "TCP Chat - " + formLogin.txtIP.Text + " - (Connected as: " + formLogin.txtNickname.Text + ")";
            formLogin.ShowDialog();
        }

        private static void Client_Disconnected(ClientSettings cs)
        {
            
        }

        public void _client_Received(ClientSettings cs, string received)
        {
            var cmd = received.Split('|');
            switch (cmd[0])
            {
                case "Users":
                    this.Invoke(() =>
                    {
                        userList.Items.Clear();
                        for (int i = 1; i < cmd.Length; i++)
                        {
                            if (cmd[i] != "Connected" | cmd[i] != "RefreshChat")
                            {
                                userList.Items.Add(cmd[i]);
                            }
                        }
                    });
                    break;
                case "Message":
                    this.Invoke(() =>
                    {
                        txtReceive.Text += cmd[1] +"\r\n";

                    });
                    break;
                case "RefreshChat":
                    this.Invoke(() =>
                    {
                        txtReceive.Text = cmd[1];
                        
                    });
                    break;
                case "Disconnect":
                    Application.Exit();
                    break;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != string.Empty)
            {
               
                txtReceive.ForeColor =  txtInput.ForeColor;
                txtReceive.Font = txtInput.Font;
                txtReceive.AppendText(formLogin.txtNickname.Text + " says: " + txtInput.Text + "\r\n");
                formLogin.Client.Send("Message|" + formLogin.txtNickname.Text + "|" + txtInput.Text+ "|"+txtReceive.Font+"|"+txtReceive.ForeColor);
               
                txtInput.Text = string.Empty;
                
            }
        }

        

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.TextLength;
        }

        private void PublicChatForm_Load(object sender, EventArgs e)
        {

        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtInput.Focus();
                txtInput.Select(0, 0);
                btnSend.PerformClick();

            }
        }
    }
}