﻿using System;
using System.Windows;
using WereDev.Utils.Wu10Man.Helpers;
using WereDev.Utils.Wu10Man.UserWindows;

namespace WereDev.Utils.Wu10Man
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            Wu10Logger.LogInfo("Application starting");
            try
            {
                WriteStartupLogs();
                this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
                this.MainWindow = new MainWindow();
                this.MainWindow.Show();

                Wu10Logger.LogInfo("Application started");
            }
            catch (Exception ex)
            {
                Wu10Logger.LogError(ex);
                MessageBox.Show("An error occured attempting to initialize the application.  Check the log file for more details.", "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Wu10Logger.LogInfo("Application ended");
            base.OnExit(e);
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Wu10Logger.LogError(e.Exception);
            string errorMessage = string.Format("{0}\r\n\r\nCheck the logs for more details.", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        void WriteStartupLogs()
        {
            var appVersion = this.GetType().Assembly.GetName().Version;
            Wu10Logger.LogInfo($"Application version: v{appVersion.ToString()}");
            Wu10Logger.LogInfo(EnvironmentVersionHelper.GetWindowsVersion());
            Wu10Logger.LogInfo($".Net Framework: {EnvironmentVersionHelper.GetDotNetFrameworkBuild()}");
        }
    }
}
