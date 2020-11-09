using System;
using System.Data;
using System.Linq;

namespace AddressBookUsingLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Created And Managed Using Linq");
            //UC1 -- Create Data Table
            DataTable addressBook = new DataTable();
            Console.WriteLine("Address Book Has Been Created");

            //UC2 -- Insert Columns Into The Data Table
            addressBook.Columns.Add("FirstName");
            addressBook.Columns.Add("LastName");
            addressBook.Columns.Add("Address");
            addressBook.Columns.Add("City");
            addressBook.Columns.Add("State");
            addressBook.Columns.Add("Zip", typeof(int));
            addressBook.Columns.Add("PhoneNumber");
            addressBook.Columns.Add("Email");
            addressBook.Columns.Add("Name");
            addressBook.Columns.Add("Type");
            UniqueConstraint uniqueFirstName = new UniqueConstraint(new DataColumn[] {
            addressBook.Columns["FirstName"], addressBook.Columns["LastName"]});
            addressBook.Constraints.Add(uniqueFirstName);

            //UC3 -- Insert Contacts into the data table
            InsertDefaultContacts(addressBook);
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter 1 to display all the contacts, 2 to insert new contacts, 3 to edit contacts" +
                    " 4 to delete a contact, 5 to find a person based on city or state, 6 to find the count of contacts in a city or state" +
                    " 7 to order contacts according to their names of a city and any other number to Exit");
                int input = Convert.ToInt32(Console.ReadLine());
                switch(input)
                {
                    case 1:
                        DisplayAllContacts(addressBook);
                        break;
                    case 2:
                        InsertNewContacts(addressBook);
                        break;
                    case 3:
                        EditContacts(addressBook);
                        break;
                    case 4:
                        DeleteContacts(addressBook);
                        break;
                    case 5:
                        FindBasedOnCityOrState(addressBook);
                        break;
                    case 6:
                        CountByCityOrState(addressBook);
                        break;
                    case 7:
                        OrderContactsOfACity(addressBook);
                        break;
                    default:
                        flag = false;
                        break;
                }
            }

        }

        public static void InsertDefaultContacts(DataTable addressBook)
        {
            addressBook.Rows.Add("First", "Last", "Address", "City", "State", 4556, "45657898", "Email", "Office","Friends");
            addressBook.Rows.Add("First2", "Last2", "Address2", "City5", "State", 456, "456578987", "Email5", "Office", "Family");
            addressBook.Rows.Add("First3", "Last3", "Address4", "City2", "State2", 556, "4565789", "Email4", "Home", "Friends");
            addressBook.Rows.Add("First4", "Last4", "Address6", "City4", "State2", 4956, "4565798", "Email2", "Office", "Profession");
            addressBook.Rows.Add("First5", "Last5", "Address3", "City5", "State4", 4556, "4565898", "Email3", "Home", "Friends");
            addressBook.Rows.Add("First6", "Last6", "Address2", "City5", "State4", 4656, "4567898", "Email5", "Office", "Family");
            addressBook.Rows.Add("First7", "Last7", "Address5", "City3", "State3", 4856, "4557898", "Email2", "Home", "Family");
            addressBook.Rows.Add("First8", "Last8", "Address3", "City4", "State5", 4256, "4657898", "Email3", "Home", "Family");
            addressBook.Rows.Add("First9", "Last9", "Address4", "City4", "State3", 4506, "5657898", "Email", "Office", "Friends");
        }

        public static void InsertNewContacts(DataTable addressBook)
        {
            Console.WriteLine("Enter-> First Name, Last Name, Address, City, State, Zip, Phone Number, Email ID one after the other.");
            String firstName = Console.ReadLine();
            String lastName = Console.ReadLine();
            String address = Console.ReadLine();
            String city = Console.ReadLine();
            String state = Console.ReadLine();
            int zip = Convert.ToInt32(Console.ReadLine());
            String phoneNumber = Console.ReadLine();
            String email = Console.ReadLine();
            addressBook.Rows.Add(firstName, lastName, address, city, state, zip, phoneNumber, email);
            Console.WriteLine("The Contact Has Been Added");
        }

        public static void DisplayAllContacts(DataTable addressBook)
        {
            Console.WriteLine("First Name -- Last Name -- Address -- City -- State -- Zip -- Phone Number -- Email ID -- Name -- Type");
            var data = from contacts in addressBook.AsEnumerable() select contacts;
            foreach(var item in data)
            {
                Console.WriteLine(item[0]+" -- "+ item[1] + " -- " + item[2] + " -- " + item[3] + " -- " + item[4] + " -- " + item[5] + " -- " + item[6] + " -- " + item[7]+" -- "+item[8]+" -- "+item[9]);
            }
        }

        public static void EditContacts(DataTable addressBook)
        {
            Console.WriteLine("Enter The Name Of The Contact To Be Edited");
            string firstName = Console.ReadLine();

            var data = from contacts in addressBook.AsEnumerable() where contacts.Field<string>("FirstName") == firstName select contacts;

            foreach (var item in data)
            {
                Console.WriteLine("Current Address is: " + item[2] + "\n Enter new address");
                string address = Console.ReadLine();
                item[2] = address;

                Console.WriteLine("Current City is: " + item[3] + "\n Enter new City");
                string city = Console.ReadLine();
                item[3] = city;

                Console.WriteLine("Current State is: " + item[4] + "\n Enter new State");
                string state = Console.ReadLine();
                item[4] = state;

                Console.WriteLine("Current Zip is: " + item[5] + "\n Enter new Zip");
                int zip = Convert.ToInt32(Console.ReadLine());
                item[5] = zip;

                Console.WriteLine("Current Phone Number is: " + item[6] + "\n Enter new Phone Number");
                string phoneNumber = Console.ReadLine();
                item[6] = phoneNumber;

                Console.WriteLine("Current EmailID is: " + item[7] + "\n Enter new EmailID");
                string email = Console.ReadLine();
                item[7] = email;
            }

        }

        public static void DeleteContacts(DataTable addressBook)
        {
            Console.WriteLine("Enter The Name Of The Contact To Be Deleted");
            string firstName = Console.ReadLine();
            var data = (from contacts in addressBook.AsEnumerable() where contacts.Field<string>("FirstName") == firstName select contacts).First();
            if (data != null)
                data.Delete();
        }

        public static void FindBasedOnCityOrState(DataTable addressBook)
        {
            Console.WriteLine("Enter The City or State to Search for a Contact");
            string cityOrState = Console.ReadLine();
            var data = from contacts in addressBook.AsEnumerable() where contacts.Field<string>("City") == cityOrState || contacts.Field<string>("State") == cityOrState select contacts;
            if (data.Count()==0)
                Console.WriteLine("No data to show");
            else
            {
                Console.WriteLine("First Name -- Last Name -- Address -- City -- State -- Zip -- Phone Number -- Email");
                foreach(var item in data)
                {
                    Console.WriteLine(item[0]+" -- "+ item[1] + " -- " + item[2] + " -- " + item[3] + " -- " + item[4] + " -- " + item[5] + " -- " + item[6] + " -- " + item[7] + " -- " + item[8] + " -- " + item[9]);
                }
            }
        }

        public static void CountByCityOrState(DataTable addressBook)
        {
            Console.WriteLine("Enter The City or State to Search for a Contact");
            string cityOrState = Console.ReadLine();
            var data = from contacts in addressBook.AsEnumerable() where contacts.Field<string>("City") == cityOrState || contacts.Field<string>("State") == cityOrState select contacts;
            Console.WriteLine("The number of contact persons in "+cityOrState+" are "+data.Count());
        }

        public static void OrderContactsOfACity(DataTable addressBook)
        {
            Console.WriteLine("Enter The City for ordering a Contact");
            string city = Console.ReadLine();
            var data = from contacts in addressBook.AsEnumerable() where contacts.Field<string>("City") == city orderby contacts.Field<string>("FirstName") select contacts;
            if (data.Count() == 0)
                Console.WriteLine("No data to show");
            else
            {
                Console.WriteLine("First Name -- Last Name -- Address -- City -- State -- Zip -- Phone Number -- Email");
                foreach (var item in data)
                {
                    Console.WriteLine(item[0] + " -- " + item[1] + " -- " + item[2] + " -- " + item[3] + " -- " + item[4] + " -- " + item[5] + " -- " + item[6] + " -- " + item[7] + " -- " + item[8] + " -- " + item[9]);
                }
            }
        }

        
    }
}
