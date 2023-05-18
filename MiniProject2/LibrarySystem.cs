//Raajpaal Gohlwar CIS 340 8:35 AM MiniProject2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MiniProject2
{
    class LibrarySystem
    {
        private Device[] deviceArray = new Device[15];
        private Device myDevice = new Device();
        private Utilities myUtilities = new Utilities();
        private int deviceCounter = 0;
        private int menuSelection;

        static void Main(string[] args)
        {
            LibrarySystem myLibrarySystem = new LibrarySystem();
            myLibrarySystem.InstantiateArray();
            myLibrarySystem.PreloadedItems();
            myLibrarySystem.DisplayMenu();
            myLibrarySystem.MenuSelection();
        }

        //Displays the menu to the user
        public void DisplayMenu()
        {
            do
            {
                Console.Clear();

                WriteLine("\t\tLibrary Device Checkout System");
                WriteLine();
                WriteLine();

                WriteLine("1. List Devices by Title\n2. Add New Devices\n3. Edit Device Information" +
                    "\n4. Search by Device Name\n5. Check Out Devices\n6. Check In Devices\n7. Exit");
                WriteLine();

                MenuSelection();

            } while (menuSelection != 7);
        }

        //Reads user input and performs prompted menu selection
        public void MenuSelection()
        {
            menuSelection = ReadInteger("Select menu option (1-7): ", 1);
            switch (menuSelection)
            {
                case 1:
                    DisplayList();
                    myUtilities.Break();
                    break;
                case 2:
                    Console.Clear();
                    WriteLine("\t\tLibrary Device Checkout System - Add New Device");
                    myUtilities.Pause();
                    AddDevice(deviceCounter);
                    myUtilities.Break();
                    break;
                case 3:
                    EditDevice();
                    myUtilities.Break();
                    break;
                case 4:
                    SearchForDevice();
                    break;
                case 5:
                    CheckOutDevice();
                    break;
                case 6:
                    CheckInDevice();
                    myUtilities.Break();
                    break;
                case 7:
                    System.Environment.Exit(0);
                    break;
                default:
                    DisplayMenu();
                    break;
            }
        }

        //ReadInteger method checks input to make sure it is correct
        public int ReadInteger(string displayString, int readNumber = 1)
        {
            bool repeatInput = true;
            int number = 0;

            do
            {
                try
                {
                    Write($"{displayString}");
                    number = Convert.ToInt32(ReadLine());
                    repeatInput = false;
                }
                catch (FormatException)
                {
                    switch (readNumber)
                    {
                        case 1:
                            DisplayMenu();
                            repeatInput = true;
                            break;
                        case 2:
                            DisplayList();
                            repeatInput = true;
                            break;
                        case 3:
                            CheckOutDevice();
                            repeatInput = false;
                            break;
                        case 4:
                            CheckInDevice();
                            repeatInput = false;
                            break;
                    }
                }
                catch (OverflowException)
                {
                    switch (readNumber)
                    {
                        case 1:
                            DisplayMenu();
                            repeatInput = true;
                            break;
                        case 2:
                            DisplayList();
                            repeatInput = true;
                            break;
                        case 3:
                            CheckOutDevice();
                            repeatInput = false;
                            break;
                        case 4:
                            CheckInDevice();
                            repeatInput = false;
                            break;
                    }
                }
                catch (Exception)
                {
                    if (readNumber == 1)
                    {
                        WriteLine("An error occured while running.");
                        myUtilities.Break();
                        myUtilities.Pause();
                        DisplayMenu();
                    }
                }
            } while (repeatInput == true);

            return number;
        }

        //Displaying the list of elements in the deviceArray
        public void DisplayList()
        {
            string number = "#";
            string sku = "SKU";
            string name = "Name";
            string availability = "Status";

            Console.Clear();

            WriteLine("\t\tLibrary Device Checkout System - List");
            myUtilities.Pause();
            WriteLine($"{number,-3}{sku,-10}{name,-50}{availability,-15}");

            for (int i = 0; i < deviceCounter; i++)
            {
                Write($"{(i + 1),-3}");
                deviceArray[i].PrintDeviceInformation();
            }
            WriteLine();
        }

        //Displaying the list of elements that are either checked in or checked out
        public int ModifiedDisplayList(string option)
        {
            string number = "#";
            string sku = "SKU";
            string name = "Name";
            int checkItemNumber = 0;

            WriteLine($"{number,-3}{sku,-10}{name,-50}");

            for (int i = 0; i < deviceCounter; i++)
            {
                if (deviceArray[i].Availability == option)
                {
                    Write($"{(i + 1),-3}");
                    deviceArray[i].PrintDeviceInformation(true);
                    checkItemNumber = (i + 1);
                }
            }
            WriteLine();

            return checkItemNumber;
        }

        //Adds devices to the deviceArray
        public void AddDevice(int counter, bool edit = false)
        {
            string sku;
            string name;
            bool skuValidate = false;

            do
            {
                Write("Enter SKU: ");
                sku = ReadLine().ToUpper();
                skuValidate = myDevice.ValidateSkuNumber(sku);

            } while (skuValidate == false);

            Write("Enter Device Name: ");
            name = ReadLine();
            WriteLine();

            if (edit == false)
            {
                Device newDevice = new Device(name);
            }
            else if (edit == true)
            {
                Device newDevice = new Device(name, true);
            }
            WriteLine();

            deviceArray[counter] = myDevice.CreateDevice(sku, name);
            if (edit == false)
            {
                deviceCounter++;
            }
        }

        //Re-uses AddDevice method to edit device
        public void EditDevice()
        {
            int editDevice;

            DisplayList();
            editDevice = ReadInteger($"Enter device number to edit (1-{deviceCounter}): ", 2);
            if (editDevice > deviceCounter)
            {
                myUtilities.Invalid();
            }
            else
            {
                AddDevice((editDevice - 1), true);
            }
        }

        //Searches deviceArray for matching itemName given user input (case insensitive)
        public void SearchForDevice()
        {
            string number = "#";
            string sku = "SKU";
            string name = "Name";
            string userInput;

            Console.Clear();
            WriteLine("\t\tLibrary Device Checkout System - Search");
            myUtilities.Pause();

            Write("Enter device to search for: ");
            userInput = ReadLine();
            WriteLine();

            WriteLine($"Listings for '{userInput}'");
            WriteLine();
            WriteLine($"{number,-3}{sku,-10}{name,-50}");

            for (int i = 0; i < deviceCounter; i++)
            {
                if (deviceArray[i].ItemName.ToUpper().Contains(userInput.ToUpper()))
                {
                    WriteLine($"{(i + 1),-3}{deviceArray[i].SkuNumber,-10}{deviceArray[i].ItemName,-50}");
                }
            }
            WriteLine();

            myUtilities.Break();
        }

        //Checks out device using ModifiedDisplayList method
        public void CheckOutDevice()
        {
            int deviceNumberCheckOut = 0;
            int checkOutNumber = 0;

            Console.Clear();
            WriteLine("\t\tLibrary Device Checkout System - Check Out Devices");
            myUtilities.Pause();
            WriteLine("Available Devices");
            checkOutNumber = ModifiedDisplayList("Available");

            deviceNumberCheckOut = ReadInteger("Enter device number: ", 3);
            if ((deviceArray[(deviceNumberCheckOut) - 1].Availability == "Checked Out"))
            {
                WriteLine("This device has already been checked out.");
                WriteLine();
            }
            else if (deviceNumberCheckOut > checkOutNumber)
            {
                myUtilities.Invalid();
            }
            else
            {
                deviceArray[(deviceNumberCheckOut - 1)].Availability = "Checked Out";
                WriteLine("Device checked out.");
                WriteLine();
            }

            myUtilities.Break();
        }

        //Checks in device using ModifiedDisplayList method
        public void CheckInDevice()
        {
            int deviceNumberCheckIn;
            int checkInNumber;

            Console.Clear();
            WriteLine("\t\tLibrary Device Checkout System - Check In Devices");
            myUtilities.Pause();
            WriteLine("Checked Out Devices");
            checkInNumber = ModifiedDisplayList("Checked Out");

            deviceNumberCheckIn = ReadInteger("Enter device number: ", 4);
            if ((deviceArray[(deviceNumberCheckIn) - 1].Availability == "Available"))
            {
                WriteLine("This device has not been checked out.");
                WriteLine();
            }
            else if (deviceNumberCheckIn > checkInNumber)
            {
                myUtilities.Invalid();
            }
            else
            {
                deviceArray[(deviceNumberCheckIn - 1)].Availability = "Available";
                WriteLine("Device checked in.");
                WriteLine();
            }
        }

        //Instantiates all elements in the deviceArray
        private void InstantiateArray()
        {
            for (int i = 0; i < deviceArray.Length; i++)
            {
                deviceArray[i] = new Device();
            }
        }

        //List of preloaded items being entered into the deviceArray
        private void PreloadedItems()
        {
            //Item 1
            deviceArray[0].SkuNumber = "4D38T3";
            deviceArray[0].ItemName = "Razer Blade 14-Inch Laptop";
            deviceArray[0].Availability = "Available";
            deviceCounter++;
            //Item 2
            deviceArray[1].SkuNumber = "6WN25";
            deviceArray[1].ItemName = "Apple 9.7-Inch iPad";
            deviceArray[1].Availability = "Available";
            deviceCounter++;
            //Item 3
            deviceArray[2].SkuNumber = "37D494";
            deviceArray[2].ItemName = "TI-84 Plus Graphics Calculator";
            deviceArray[2].Availability = "Checked Out";
            deviceCounter++;
            //Item 4
            deviceArray[3].SkuNumber = "7DG50";
            deviceArray[3].ItemName = "Amazon Kindle Oasis 7-Inch E-Reader";
            deviceArray[3].Availability = "Available";
            deviceCounter++;
            //Item 5
            deviceArray[4].SkuNumber = "BS68S6";
            deviceArray[4].ItemName = "Lenovo Miix 320 10.1-Inch Tablet";
            deviceArray[4].Availability = "Checked Out";
            deviceCounter++;
        }
    }
}
