﻿using System.Collections.Generic;

namespace Ex03GarageLogic
{
    public class GarageManager
    {
        Dictionary<string, Client> m_CurrentClients = new Dictionary<string, Client>();

        public void AddNewClient(string i_Name, string i_PhoneNumber, Vehicle i_Vehicle)// or AddNewVehicle . first method in file
        {
            Client newClient;

            if (!isVehicleInGarage(i_Vehicle.ID, out newClient)) // can do just add and it will throw exception
            {
                newClient = new Client(i_Name, i_PhoneNumber, i_Vehicle);
                m_CurrentClients.Add(i_Vehicle.ID, newClient);
            }
            else
            {
                newClient.Status = eServiceStatus.InRepair;
                // throw some message that the vehicle is already in the garage
            }
        }

        public string[] ShowAllVehiclesInGarage() // second method in file , return all vehicle's id //finish
        {
            string[] res = new string[m_CurrentClients.Count];

            m_CurrentClients.Keys.CopyTo(res, 0);

            return res;
        }

        public void SetNewStatusForVehicle(string i_VehicleId, eServiceStatus i_NewStatus) // third method in file
        {
            Client vehicleOwner;

            if (isVehicleInGarage(i_VehicleId, out vehicleOwner))
            {
                vehicleOwner.Status = i_NewStatus;
            }
            else
            {
                // throw exception not in garage
            }
        }

        public void InflateTyresToMax(string i_VehicleId, eServiceStatus i_NewStatus) // forth method in file
        {
            Client vehicleOwner;

            if (isVehicleInGarage(i_VehicleId, out vehicleOwner))
            {
                vehicleOwner.ClientVehicle.InflateTyresToMax();
            }
            else
            {
                // throw exception not in garage
            }
        }

        public void RefuelVehicle(string i_VehicleId, eFuelType i_FuelType, float i_AmountToAdd)   // fifth method in file 
        {
            Client vehicleOwner;

            if (!isVehicleInGarage(i_VehicleId, out vehicleOwner))
            {
                // throw exception
            }

            GasEngine vehicle = vehicleOwner.ClientVehicle as GasEngine;
            if (vehicle == null)
            {
                // throw exception not on gas
            }

            vehicle.Refuel(i_AmountToAdd, i_FuelType);
        }

        public void ChargeVehicle(string i_VehicleId, float i_AmountToAdd)  // sixth method in file
        {
            Client vehicleOwner;

            if (!isVehicleInGarage(i_VehicleId, out vehicleOwner))
            {
                // throw exception
            }

            ElectricEngine vehicle = vehicleOwner.ClientVehicle as ElectricEngine;
            if (vehicle == null)
            {
                // throw exception not electric
            }

            vehicle.Charge(i_AmountToAdd);
        }

        public string GetClientDetails(string i_VehicleId)      // seventh method in file
        {
            return null;// string format that includes all the details
        }

        private bool isVehicleInGarage(string i_VehicleId, out Client o_VehicleOwner)
        {
            return m_CurrentClients.TryGetValue(i_VehicleId, out o_VehicleOwner);
        }
    }
}
