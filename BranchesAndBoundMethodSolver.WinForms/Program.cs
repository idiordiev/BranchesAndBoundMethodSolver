using Serilog;

namespace BranchesAndBoundMethodSolver.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            ApplicationConfiguration.Initialize();
            Application.Run(new BranchesAndBoundMethodSolver());
        }
    }
}