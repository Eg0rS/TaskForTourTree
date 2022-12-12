using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Models.Model;

namespace Tour_3.Service
{
    public class AnalyticsService
    {
        private readonly DroneService _droneService;
        private readonly FlightService _flightService;

        public AnalyticsService(DroneService droneService, FlightService flightService)
        {
            _droneService = droneService;
            _flightService = flightService;
        }

        private double GetProductivityForOneDrone(DroneModel drone)
        {
            var flights = _flightService.GetAllFlights();

            if (!flights.Any() || drone == null)
            {
                return 0;
            }

            var flightForOneDrone = flights.Where(f => f.DroneId == drone.Id).ToList();
            if (!flightForOneDrone.Any())
            {
                return 0;
            }

            var flightTime = flightForOneDrone.Average(f => f.FlightTime);
            var flightDistance = flightForOneDrone.Average(f => f.Distance);
            var flightHeight = flightForOneDrone.Average(f => f.Height);
            var flightMission = flightForOneDrone.Sum(f => f.MissionCompleted);
            var fightConsumptionFuel = flightForOneDrone.Average(f => f.ConsumptionFuel);
            var isShotSown = flightForOneDrone.Any(f => f.IsShotSown);


            double flightTimeProductivity = Math.Abs(flightTime - drone.MaxFlightTime) / drone.MaxFlightTime;
            double flightDistanceProductivity = Math.Abs(flightDistance - drone.MaxFlightDistance) / drone.MaxFlightDistance;
            double flightHeightProductivity = Math.Abs(flightHeight - drone.Weight) / drone.Weight;
            double fightConsumptionFuelProductivity = Math.Abs(fightConsumptionFuel - drone.MaxSpeed) / drone.MaxSpeed;
            double flightMissionProductivity = Math.Abs((double)flightMission - flightForOneDrone.Count) / flightForOneDrone.Count * 4;
            double isShotSownProductivity = isShotSown ? 0.5 : 1;

            if (flightTimeProductivity > 1)
            {
                flightTimeProductivity = 1;
            }

            if (flightDistanceProductivity > 1)
            {
                flightDistanceProductivity = 1;
            }

            if (flightHeightProductivity > 1)
            {
                flightHeightProductivity = 1;
            }

            if (fightConsumptionFuelProductivity > 1)
            {
                fightConsumptionFuelProductivity = 1;
            }

            var total = Math.Round(flightMissionProductivity / (flightTimeProductivity + flightDistanceProductivity + flightHeightProductivity + fightConsumptionFuelProductivity) *
                                   isShotSownProductivity, 2);


            return total;
        }

        public IEnumerable<object> GetDronesWithProductivity(SelectedAnalytics selectedAnalytics)
        {
            var drones = _droneService.GetAllDrones();
            var dronesWithProductivity = selectedAnalytics.model == "Не выбрана" ? drones : drones.Where(d => d.Model == selectedAnalytics.model);
            var result = dronesWithProductivity
                .Select(d => new
                {
                    Drone = d,
                    Productivity = GetProductivityForOneDrone(d)
                })
                .OrderByDescending(d => d.Productivity)
                .Select(d => new { d.Drone.Id, d.Drone.Name, d.Drone.Model, d.Productivity })
                .Cast<object>().ToList();

            if (selectedAnalytics.acsending)
            {
                result.Reverse();
            }

            return result;
        }

        public IEnumerable<object> GetDronesWithConsumption(SelectedAnalytics selectedAnalytics)
        {
            var drones = _droneService.GetAllDrones();
            var dronesWithConsumption = selectedAnalytics.model == "Не выбрана" ? drones : drones.Where(d => d.Model == selectedAnalytics.model);
            var result = dronesWithConsumption
                .Select(d => new
                {
                    Drone = d,
                    Consumption = _flightService.GetAllFlights().Where(f => f.DroneId == d.Id).Sum(f => f.ConsumptionFuel)
                })
                .OrderByDescending(d => d.Consumption)
                .Select(d => new { d.Drone.Id, d.Drone.Name, d.Drone.Model, d.Consumption })
                .Cast<object>().ToList();

            if (selectedAnalytics.acsending)
            {
                result.Reverse();
            }

            return result;
        }

        public IEnumerable<object> GetDronesWithCondition(SelectedAnalytics selectedAnalytics)
        {
            var drones = _droneService.GetAllDrones();
            var dronesWithCondition = selectedAnalytics.model == "Не выбрана" ? drones : drones.Where(d => d.Model == selectedAnalytics.model);
            var result = dronesWithCondition
                .Select(d => new
                {
                    Drone = d,
                    Condition = _flightService.GetAllFlights().Where(f => f.DroneId == d.Id).Average(f =>
                    {
                        double flightTimeProductivity = Math.Abs(f.FlightTime - d.MaxFlightTime) / d.MaxFlightTime;
                        double flightDistanceProductivity = Math.Abs(f.Distance - d.MaxFlightDistance) / d.MaxFlightDistance;
                        double flightHeightProductivity = Math.Abs(f.Height - d.Weight) / d.Weight;

                        if (flightTimeProductivity > 1)
                        {
                            flightTimeProductivity = 1;
                        }

                        if (flightDistanceProductivity > 1)
                        {
                            flightDistanceProductivity = 1;
                        }

                        if (flightHeightProductivity > 1)
                        {
                            flightHeightProductivity = 1;
                        }


                        return (flightTimeProductivity + flightDistanceProductivity + flightHeightProductivity) / 3 * 100;
                    })
                })
                .OrderByDescending(d => d.Condition)
                .Select(d => new { d.Drone.Id, d.Drone.Name, d.Drone.Model, d.Condition })
                .Cast<object>().ToList();

            if (selectedAnalytics.acsending)
            {
                result.Reverse();
            }

            return result;
        }
    }

    public class SelectedAnalytics
    {
        public bool acsending { get; set; }
        public string model { get; set; }
    }
}