//Raajpaal Gohlwar CIS 340 8:35 AM MiniProject2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MiniProject2
{
    class Device
    {
        private string skuNumber;
        private string itemName;
        private string availability;

        public string SkuNumber
        {
            get { return skuNumber; }
            set { skuNumber = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public Device()
        { }

        //Overloaded constructor for AddDevice method
        public Device(string name)
        {
            ItemName = name;

            WriteLine($"Added {ItemName} to Catalog.");
        }

        //Overloaded constructor for EditDevice method
        public Device(string name, bool edit)
        {
            ItemName = name;

            WriteLine("Device information updated.");
        }

        //Prints deviceArray information
        public void PrintDeviceInformation()
        {
            WriteLine($"{SkuNumber,-10}{ItemName,-50}{Availability,-15}");
        }

        //Prints ModifiedDisplayList
        public void PrintDeviceInformation(bool modifiedList)
        {
            WriteLine($"{SkuNumber,-10}{ItemName,-50}");
        }

        //Creates device in deviceArray
        public Device CreateDevice(string sku, string name)
        {
            Device tmpDevice = new Device();
            tmpDevice.SkuNumber = sku;
            tmpDevice.ItemName = name;
            tmpDevice.Availability = "Available";

            return tmpDevice;
        }

        //Validates that the SKU number entered is a maximum of 6 characters
        public bool ValidateSkuNumber(string skuInput)
        {
            bool validate = false;

            if (skuInput.Length <= 6)
            {
                SkuNumber = skuInput;
                validate = true;
            }
            else
            {
                WriteLine("The SKU Number must be a maximum of 6 characters in length.");
                validate = false;
            }

            return validate;
        }
    }
}
