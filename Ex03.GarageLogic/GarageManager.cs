﻿using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, Client> m_CurrentClients = new Dictionary<string, Client>();

        public void AddNewClient(string i_Name, string i_PhoneNumber, Vehicle i_Vehicle) // First method in file
        {
            Client newClient;

            if (!m_CurrentClients.TryGetValue(i_Vehicle.ID, out newClient))
            {
                newClient = new Client(i_Name, i_PhoneNumber, i_Vehicle);
                m_CurrentClients.Add(i_Vehicle.ID, newClient);
            }
            else
            {
                newClient.Status = eServiceStatus.InRepair;
                throw new ArgumentException("Vehicle is already in the garage");
            }
        }

        public List<string> ShowAllVehiclesInGarage(eServiceStatus i_FilterStatus) // second method in file , return all vehicle's id //finish
        {
            List<string> vehiclesList = new List<string>(m_CurrentClients.Count);

            foreach (string licensePlate in m_CurrentClients.Keys)
            {
                if (i_FilterStatus == eServiceStatus.NoStatus || m_CurrentClients[licensePlate].Status == i_FilterStatus)
                {
                    vehiclesList.Add(licensePlate);
                }
            }

            return vehiclesList;
        }

        public void SetNewStatusForVehicle(string i_VehicleId, eServiceStatus i_NewStatus) // third method in file
        {
            Client vehicleOwner;

            if (checkIfVehicleInGarage(i_VehicleId, out vehicleOwner))
            {
                vehicleOwner.Status = i_NewStatus;
            }
        }

        public void InflateTyresToMax(string i_VehicleId) // forth method in file
        {
            Client vehicleOwner;

            if (checkIfVehicleInGarage(i_VehicleId, out vehicleOwner))
            {
                vehicleOwner.ClientVehicle.InflateTyresToMax();
            }
        }

        public void RefuelVehicle(string i_VehicleId, eFuelType i_FuelType, float i_AmountToAdd) // fifth method in file
        {
            Client vehicleOwner;

            checkIfVehicleInGarage(i_VehicleId, out vehicleOwner);
            GasEngine engineToRefuel = vehicleOwner.ClientVehicle.Engine as GasEngine;

            if (engineToRefuel == null)
            {
                throw new FormatException("Vehicle is not gas vehicle");
            }

            engineToRefuel.Refuel(i_AmountToAdd, i_FuelType);
        }

        public void ChargeVehicle(string i_VehicleId, float i_AmountToAdd) // sixth method in file
        {
            Client vehicleOwner;

            checkIfVehicleInGarage(i_VehicleId, out vehicleOwner);
            ElectricEngine engineToCharge = vehicleOwner.ClientVehicle.Engine as ElectricEngine;

            if (engineToCharge == null)
            {
                throw new FormatException("Vehicle is not electric vehicle");
            }

            engineToCharge.Charge(i_AmountToAdd);
        }

        public string GetVehicleDetails(string i_VehicleId) // seventh method in file
        {
            Client client;

            checkIfVehicleInGarage(i_VehicleId, out client);

            return client.ToString();
        }

        private bool checkIfVehicleInGarage(string i_VehicleId, out Client o_VehicleOwner)
        {
            bool vehicleFound = m_CurrentClients.TryGetValue(i_VehicleId, out o_VehicleOwner);

            if (!vehicleFound)
            {
                throw new ArgumentException("Vehicle is not in the garage");
            }

            return vehicleFound;
        }
    }
}