﻿using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private List<Vehicle> m_VehiclesList = new List<Vehicle>();

        public List<Vehicle> VehiclesList
        {
            get
            {
                return m_VehiclesList;
            }
        }

        private void buildVehicleList()
        {
            MethodInfo[] allMethods = this.GetType().GetMethods();

            foreach (MethodInfo methodInfo in allMethods)
            {
                //if (methodInfo.ReturnType is Vehicle)
                if (methodInfo.ReturnType.Name.Equals("Vehicle"))
                {
                    ParameterInfo[] allParams = methodInfo.GetParameters();
                    if (allParams.Length == 0) //get rid of it after dealing with BuildVehicleByIndex
                    {
                        m_VehiclesList.Add(methodInfo.Invoke(this, allParams) as Vehicle);
                    }
                }
            }
        }

        public Vehicle BuildVehicleByIndex(int i_Index) // O(n) -> can be done in O(1) by change Vehicles list to array
        {
            int i = 0;
            Vehicle vehicle = null;
            MethodInfo[] allMethods = this.GetType().GetMethods();

            foreach (MethodInfo methodInfo in allMethods) // Code duplication -> will deal with it later
            {
                if (methodInfo.ReturnType is Vehicle)
                {
                    if (i == i_Index)
                    {
                        vehicle = methodInfo.Invoke(this, null) as Vehicle;
                    }

                    i++;
                }
            }

            return vehicle;
        }

        public string VehicleListToString()
        {
            StringBuilder resString = new StringBuilder();
            int i = 1;

            buildVehicleList();
            foreach (Vehicle vehicle in m_VehiclesList)
            {
                resString.AppendFormat("({0}) {1}", i, vehicle.ToString()).AppendLine().AppendLine();
                i++;
            }

            return resString.ToString();
        }

        public Vehicle BuildGasMotorbike()
        {
            int numOfWheels = 2;
            float maxPressure = 31;
            float fuelTankVolume = 6.2F;
            eFuelType fuelType = eFuelType.Octan98;
            GasEngine engine = new GasEngine(fuelType, fuelTankVolume);

            return new Motorbike(numOfWheels, maxPressure, engine);
        }

        public Vehicle BuildElectricMotorbike()
        {
            int numOfWheels = 2;
            float maxPressure = 31;
            float batteryLife = 2.5F;
            ElectricEngine engine = new ElectricEngine(batteryLife);

            return new Motorbike(numOfWheels, maxPressure, engine);
        }
    }
}
