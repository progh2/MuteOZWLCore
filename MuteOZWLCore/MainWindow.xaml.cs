using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuteOZWLCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string REG_ADDRESS = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";
        const string KEY = @"C:\Program Files (x86)\FORCS\OZWebLauncher\OZWLBridge.exe";
        const string KEY_VALUE = @"~ RUNASADMIN";
        RegistryKey reg;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getStatus();
        }

        private void getStatus()
        {
            reg = Registry.LocalMachine.CreateSubKey(REG_ADDRESS);
            string key_value = Convert.ToString(reg.GetValue(KEY, ""));
            //lblStatus.Content = key_value;

            if (reg.ValueCount > 0 && key_value == KEY_VALUE)
            {
                lblStatus.Content = "알림 끄기가 적용되어 있습니다";
                btnMuteOn.IsEnabled = false;
                btnMuteOff.IsEnabled = true;
            }
            else
            {
                lblStatus.Content = "알림 끄기가 적용되어있지 않습니다";
                btnMuteOn.IsEnabled = true;
                btnMuteOff.IsEnabled = false;
            }

            reg.Close();
        }


        private void Mnu_Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void Mnu_GoHomepage(object sender, RoutedEventArgs e)
        {            
            Process.Start("cmd", "/C start https://progh2.github.io/MuteOZWL/");
        }

        private void btnMuteOn_Click(object sender, RoutedEventArgs e)
        {
            reg = Registry.LocalMachine.CreateSubKey(REG_ADDRESS);
            reg.SetValue(KEY, KEY_VALUE);
            reg.Close();
            MessageBox.Show("알림 끄기가 적용되었습니다.");
            getStatus();
        }

        private void btnMuteOff_Click(object sender, RoutedEventArgs e)
        {
            reg = Registry.LocalMachine.CreateSubKey(REG_ADDRESS);
            reg.DeleteValue(KEY);
            reg.Close();
            MessageBox.Show("알림 끄기가 취소되었습니다.");
            getStatus();
        }
    }
}
