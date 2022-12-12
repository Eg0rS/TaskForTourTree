using System;
using System.Windows.Forms;
using DataBase;
using Tour_3.Service;

namespace Tour_3
{
    static class Program
    {
        public static DbConnection dbConnection;
        public static DroneService droneService;
        public static FlightService flightService;
        public static AnalyticsService analyticsService;

        [STAThread]
        static void Main()
        {
            dbConnection = new DbConnection();
            droneService = new DroneService(dbConnection);
            flightService = new FlightService(dbConnection);
            analyticsService = new AnalyticsService(droneService, flightService);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}