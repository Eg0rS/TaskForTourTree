using DataBase;
using tour3.Service;

namespace tour3
{
    internal static class Program
    {
        public static DbConnection _dbConnection;
        public static DroneService _droneService;
        public static FlightService _flightService;
        public static AnalyticsService _analyticsService;

        [STAThread]
        static void Main()
        {
            _dbConnection = new DbConnection();
            _droneService = new DroneService(_dbConnection);
            _flightService = new FlightService(_dbConnection);
            _analyticsService = new AnalyticsService(_droneService, _flightService);


            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}