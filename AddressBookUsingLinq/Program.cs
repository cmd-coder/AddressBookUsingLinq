using System;
using System.Data;

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
            UniqueConstraint uniqueFirstName = new UniqueConstraint(new DataColumn[] { 
            addressBook.Columns["FirstName"], addressBook.Columns["LastName"]});
            addressBook.Constraints.Add(uniqueFirstName);

        }
    }
}
