using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;

namespace SqlAutoStartR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SqlStart();
        }

        public void SqlStart()
        {

            try
            {
                ServiceController ser = new ServiceController();
                ser.ServiceName = "mssqlserver";
                if (ser.Status != ServiceControllerStatus.Running)
                {
                    calisti++;
                    string path = "SqlStopStart.bat";
                    if (!File.Exists(path))
                    {
                        File.WriteAllText("SqlStopStart.bat", "net start mssqlserver");
                    }
                    Process p = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(path);
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.Verb = "runas";
                    p.StartInfo = psi;
                    p.Start();

                    label2.Text = calisti.ToString();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("HATA.txt", ""+ex.Message);
            }
           
        }

        public static int calisti = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            calisti = 0;
            label2.Text = calisti.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = kapat;
            this.Hide();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.Visible == true)
                    {
                        this.Hide();
                    }
                    else
                    {
                        this.Show();
                        this.Activate();
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1, "SQL R Otomatik Servis Açıldı", "SQL Servis", ToolTipIcon.Info);
            this.Hide();
        }

        bool kapat = true;
        private void btnKapat_Click(object sender, EventArgs e)
        {
            kapat = false;
            this.Close();
        }
    }
}
