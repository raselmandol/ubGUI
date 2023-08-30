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
    public partial class mainForm : Form
    {
        private Computer computer;
        public mainForm()
        {
            InitializeComponent();
            InitializeRAMCounter();
            InitializeCounters();
            InitializeComputer();
            //for process start
            Process[] allProcesses = Process.GetProcesses();
            int totalProcesses = allProcesses.Length;

            int runningProcesses = 0;
            foreach (Process process in allProcesses)
            {
                if (process.Responding)
                    runningProcesses++;
            }
            processCnt.Text=totalProcesses.ToString();
            processRunningCnt.Text=runningProcesses.ToString();
            //for process end


            //for threads start
            int totalThreads = Process.GetCurrentProcess().Threads.Count;
            int runningThreads = 0;
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            {
                if (thread.ThreadState == ThreadState.Running)
                    runningThreads++;
            }
            threadsCnt.Text=totalThreads.ToString();
            threadsRunningCnt.Text=runningThreads.ToString();
            //for threads end

            //os info start
            labelOS.Text = Environment.OSVersion.VersionString;
            //os end

            //kernel start
            labelKernel.Text=GetKernelVersion();
            //kernel end
            //bit start
            labelMachine.Text= (Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit").ToString();
            labelUser.Text = Environment.UserName;
            labelPackageManager.Text = GetPackageManager();
            int updateCount = GetInstalledUpdatesCount();
            labelUpdates.Text = updateCount.ToString();
            int packageCount = GetInstalledPackageCount();
            labelTotalPackages.Text = packageCount.ToString();
            bool firewallStatus = IsFirewallEnabled();
            bool vpnStatus = IsVpnActive();
            labelFirewallStatus.Text = firewallStatus.ToString();
            labelVpnStatus.Text = vpnStatus.ToString();
            ram.Text = GetTotalRamSize().ToString()+" Gb";
           //cached.Text = GetTotalCachedRam().ToString();
            //RamFree.Text=GetTotalFreeRam().ToString();
            //RamUsed.Text = GetTotalUsedRam().ToString();
            //others Top start


            //others top end





        }

        //other top functions start




        static ulong GetTotalRamSize()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
                ulong totalRamSizeBytes = Convert.ToUInt64(obj["TotalPhysicalMemory"]);
                ulong totalRamSizeKb = totalRamSizeBytes / 1024;
                ulong totalRamSizeMb = totalRamSizeKb / 1024;
                ulong totalRamSizeGb = totalRamSizeMb / 1024;

                return totalRamSizeGb;
            }

            return 0;
        }

  
        static bool IsFirewallEnabled()
        {
            using (ServiceController serviceController = new ServiceController("MpsSvc"))
            {
                try
                {
                    ServiceControllerStatus status = serviceController.Status;
                    return status == ServiceControllerStatus.Running;
                }
                catch
                {
                    return false;
                }
            }
        }

        static bool IsVpnActive()
        {
            using (ServiceController serviceController = new ServiceController("RasMan"))
            {
                try
                {
                    ServiceControllerStatus status = serviceController.Status;
                    return status == ServiceControllerStatus.Running;
                }
                catch
                {
                    return false;
                }
            }
        }


        static int GetInstalledPackageCount()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product"))
                {
                    int packageCount = searcher.Get().Count;
                    return packageCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: 0");
            }

            return 0;
        }

        static int GetInstalledUpdatesCount()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_QuickFixEngineering"))
                {
                    int updateCount = searcher.Get().Count;
                    return updateCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("0 Updates");
            }

            return 0;
        }




        //other top function end




        static TimeSpan GetSystemUptime()
        {
            int tickCount = Environment.TickCount;
            TimeSpan uptime = TimeSpan.FromMilliseconds(tickCount);
            return uptime;
        }

        static string GetPackageManager()
        {
            string[] packageManagers = { "Chocolatey", "Scoop", "Winget", "NuGet", "Npackd", "OneGet","AppGet" }; 

            foreach (string packageManager in packageManagers)
            {
                if (IsPackageManagerInstalled(packageManager))
                    return packageManager;
            }

            return "Unknown";
        }
        static bool IsPackageManagerInstalled(string packageManager)
        {
            string pathVariable = Environment.GetEnvironmentVariable("PATH");
            string[] paths = pathVariable.Split(';');

            foreach (string path in paths)
            {
                string packageManagerPath = System.IO.Path.Combine(path, packageManager + ".exe");
                if (System.IO.File.Exists(packageManagerPath))
                    return true;
            }

            return false;
        }


        static string GetKernelVersion()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key != null)
                    {
                        string kernelVersion = key.GetValue("CurrentVersion").ToString();
                        string buildNumber = key.GetValue("CurrentBuildNumber").ToString();
                        return $"{kernelVersion} (Build {buildNumber})";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't Get Kernel");
            }

            return "Unknown";
        }

        private void labelActivities_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttonTimeDate.Text = DateTime.Now.ToString("HH:mm:ss") + " " + DateTime.Now.ToString("MM/dd/yyyy");

        }

        private void buttonTimeDate_Click(object sender, EventArgs e)
        {
            

        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void labelOS_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TimeSpan uptime = GetSystemUptime();
            labelUptime.Text = uptime.ToString();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Check if VS Code is installed
            if (IsVsCodeInstalled())
            {
                OpenApplication("C:\\Program Files\\Microsoft VS Code\\Code.exe");
            }
            // Check if Code::Blocks is installed
            else if (IsCodeBlocksInstalled())
            {
                OpenApplication("C:\\Program Files\\CodeBlocks\\codeblocks.exe");
            }
            // Fallback to opening Notepad
            else
            {
                OpenApplication("notepad");
            }
        }
        private bool IsVsCodeInstalled()
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "code";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


        private bool IsCodeBlocksInstalled()
        {
            try {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "codeblocks";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private void OpenApplication(string applicationPath)
        {
            try
            {
                Process.Start("\"" + applicationPath + "\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the application: " + ex.Message);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            OpenTerminal();
        }
        private void OpenTerminal()
        {
            try
            {
                Process.Start("cmd"); // For Windows
                                      // Process.Start("gnome-terminal"); // For Linux (GNOME)
                                      // Process.Start("xterm"); // For Linux (X11)
                                      // Process.Start("terminal"); // For macOS
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the terminal: " + ex.Message);
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            OpenNotepad();
        }
        private void OpenNotepad()
        {
            try
            {
                Process.Start("notepad");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Notepad: " + ex.Message);
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenApplication2("explorer.exe");
        }

        private void OpenApplication2(string applicationPath)
        {
            try
            {
                Process.Start(applicationPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the application: " + ex.Message);
            }
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

        private void iconButton5_Click(object sender, EventArgs e)
        {
            OpenApplication2("control.exe");
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            OpenURL("https://github.com");
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            OpenURL("https://mail.google.com");
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            SimulateWindowsKeyPress();
        }
        private void SimulateWindowsKeyPress()
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x0001;
            const int KEYEVENTF_KEYUP = 0x0002;
            const int VK_LWIN = 0x5B;

            try
            {
                // Press the Windows key
                keybd_event(VK_LWIN, 0, KEYEVENTF_EXTENDEDKEY, 0);

                // Release the Windows key
                keybd_event(VK_LWIN, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error simulating the Windows key press: " + ex.Message);
            }
        }

        // Import the keybd_event function from the Win32 API
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private void iconButton9_Click(object sender, EventArgs e)
        {
            //OpenURL("https://raselmandol.com");
            aboutMe ab= new aboutMe();
            ab.Show();
        }

        private void processShowPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void DisplayTopProcessByRAM()
        {
            // Get the processes and sort them by RAM usage
            Process[] processes = Process.GetProcesses();
            Array.Sort(processes, (x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));

            if (processes.Length > 0)
            {
                Process process = processes[0];

                // Calculate CPU and RAM usage
                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
                float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
                float ramUsage = ramCounter.NextValue() / (1024 * 1024); // Convert to MB

                // Set the text of the labels
                Invoke((MethodInvoker)delegate
                {
                    processLabel.Text = $"{process.ProcessName}";
                    cpuLabel.Text = $"{cpuUsage.ToString("0.00")}%";
                    ramLabel.Text = $"{ramUsage.ToString("0.00")} MB";
                    pidLabel.Text = $"{process.Id}";
                });
            }
            else
            {
                // No processes found
                Invoke((MethodInvoker)delegate
                {
                    processLabel.Text = "False";
                    cpuLabel.Text = "False";
                    ramLabel.Text = "False";
                    pidLabel.Text = "False";
                });
            }
        }
        //second largest
        private void DisplayTopProcessByRAM2()
        {
            // Get the processes and sort them by RAM usage
            Process[] processes = Process.GetProcesses();
            Array.Sort(processes, (x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));

            if (processes.Length > 1)
            {
                Process process = processes[1];

                // Calculate CPU and RAM usage
                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
                float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
                float ramUsage = ramCounter.NextValue() / (1024 * 1024); // Convert to MB

                // Set the text of the labels
                Invoke((MethodInvoker)delegate
                {
                    processLabel1.Text = $"{process.ProcessName}";
                    cpuLabel1.Text = $"{cpuUsage.ToString("0.00")}%";
                    ramLabel1.Text = $"{ramUsage.ToString("0.00")} MB";
                    pidLabel1.Text = $"{process.Id}";
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    // No processes found
                    processLabel1.Text = "False";
                    cpuLabel1.Text = "False";
                    ramLabel1.Text = "False";
                    pidLabel1.Text = "False";
                });
            }
        }


        //3rd largest
        private void DisplayTopProcessByRAM3()
        {
            // Get the processes and sort them by RAM usage
            Process[] processes = Process.GetProcesses();
            Array.Sort(processes, (x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));

            if (processes.Length > 2)
            {
                Process process = processes[2];

                // Calculate CPU and RAM usage
                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
                float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
                float ramUsage = ramCounter.NextValue() / (1024 * 1024); // Convert to MB

                // Set the text of the labels
                Invoke((MethodInvoker)delegate
                {
                    processLabel2.Text = $"{process.ProcessName}";
                    cpuLabel2.Text = $"{cpuUsage.ToString("0.00")}%";
                    ramLabel2.Text = $"{ramUsage.ToString("0.00")} MB";
                    pidLabel2.Text = $"{process.Id}";
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    // No processes found
                    processLabel2.Text = "False";
                    cpuLabel2.Text = "False";
                    ramLabel2.Text = "False";
                    pidLabel2.Text = "False";
                });
            }
        }

        //4th largest
        private void DisplayTopProcessByRAM4()
        {
            // Get the processes and sort them by RAM usage
            Process[] processes = Process.GetProcesses();
            Array.Sort(processes, (x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));

            if (processes.Length > 3)
            {
                Process process = processes[3];

                // Calculate CPU and RAM usage
                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
                float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
                float ramUsage = ramCounter.NextValue() / (1024 * 1024); // Convert to MB

                // Set the text of the labels
                Invoke((MethodInvoker)delegate
                {
                    processLabel3.Text = $"{process.ProcessName}";
                    cpuLabel3.Text = $"{cpuUsage.ToString("0.00")}%";
                    ramLabel3.Text = $"{ramUsage.ToString("0.00")} MB";
                    pidLabel3.Text = $"{process.Id}";
                });
            }
            else
            {
                // No processes found
                Invoke((MethodInvoker)delegate
                {
                    processLabel3.Text = "False";
                    cpuLabel3.Text = "False";
                    ramLabel3.Text = "False";
                    pidLabel3.Text = "False";
                });
            }
        }

        private void p1_Tick(object sender, EventArgs e)
        {
            DisplayTopProcessByRAM();
          
            
           
        }

        private void p2_Tick(object sender, EventArgs e)
        {
            DisplayTopProcessByRAM2();
        }

        private void p3_Tick(object sender, EventArgs e)
        {
            DisplayTopProcessByRAM3();
        }

        private void p4_Tick(object sender, EventArgs e)
        {
            DisplayTopProcessByRAM4();
        }

        private PerformanceCounter ramCounter;
        private PerformanceCounter cacheCounter;
        private void InitializeCounters()
        {
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            cacheCounter = new PerformanceCounter("Memory", "Cache Bytes");

            timerRAMUsage.Start();
        }
        private void InitializeRAMCounter()
        {
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            timerRAMUsage.Start();
        }

        private void timerRAMUsage_Tick(object sender, EventArgs e)
        {
            float availableRAM = ramCounter.NextValue();
            float totalRAM = GetTotalPhysicalMemory();

            float ramUsage = 100 - (availableRAM / totalRAM) * 100;
            //progressBarRAMUsage.Value = (int)ramUsage;
            float freeRam=totalRAM - ramUsage;
            //progressBarRAMUsage.ForeColor = Color.Aqua;
            //float availableRAMa = ramCounter.NextValue();
            float cachedRAM = cacheCounter.NextValue();

            cached.Text = $"{cachedRAM / (1024 * 1024):F2} MB";
            RamUsed.Text=$"{ramUsage}%";
            RamFree.Text = $"{100-ramUsage}%";
        }

        private float GetTotalPhysicalMemory()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                long totalPhysicalMemory = Convert.ToInt64(obj["TotalPhysicalMemory"]);
                float totalRAM = totalPhysicalMemory / (1024 * 1024);
                return totalRAM;
            }

            return 0;
        }
        private void InitializeComputer()
        {
            computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;

            timerTemperature.Start();
        }


        private void timer3timerRAMUsage_Tick(object sender, EventArgs e)
        {

        }

        private void timerTemperature_Tick(object sender, EventArgs e)
        {
            float cpuTemperature = GetCPUTemperature();
            if (cpuTemperature > 0)
            {
                labelTemperature.Text = $"{cpuTemperature}°C";
            }
            else
            {
                labelTemperature.Text = $"NORMAL";
            }
            
        }
        private float GetCPUTemperature()
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            Console.WriteLine($"Sensor Name: {sensor.Name}");
                        }
                    }
                }
            }

            return 0;
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            Process.Start("control", "/name Microsoft.Language");
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            Process.Start("mmsys.cpl");
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            Process.Start("powercfg.cpl");
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void progressBarRAMUsage_Click(object sender, EventArgs e)
        {

        }
    }
}
