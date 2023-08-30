using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using OpenHardwareMonitor.Hardware;

namespace ub
{
    public partial class aboutMe : Form
    {
        public aboutMe()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        private void OpenURL(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the URL: " + ex.Message);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenURL("https://raselmandol.com");
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            OpenURL("https://github.com/raselmandol");
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            OpenURL("https://linkedin.com/in/raselmandol");
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenURL("https://instagram.com/rasel_mandol");
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            OpenURL("https://facebook.com/1raselmandol");
        }
    }
}
