// See https://aka.ms/new-console-template for more information
Console.WriteLine("----- Welcome to AddressBook System -----\n");

AddressBook_Ops obj = new AddressBook_Ops();
AddNewBook_Model model1 = new AddNewBook_Model();
AddContact_To_Book_Model model2 = new AddContact_To_Book_Model();
AddAddressofContact_Model model3 = new AddAddressofContact_Model();
bool check = true;
while (check)
{
    Console.WriteLine("\nChoose the Operation :");
    Console.WriteLine("1.Add New Book TO DB\n2.Add New Contacts TO Book\n3.Display AdressBook Contents\n4.Display Data of AddressBook DB\n5.Exit");
    Console.Write("\n> ENter your choice : ");
    int option = Convert.ToInt32(Console.ReadLine());

    switch (option)
    {
        case 1:
            Console.WriteLine("\n> Add New Book to AddressBook DB");
            Console.Write("\nHow many Books you want to add :- ");
            int count = Convert.ToInt32(Console.ReadLine());
            int num_of_records = 0;
            while (count > 0)
            {
                AddressBook_Ops obj1 = new AddressBook_Ops();
                num_of_records++;
                Console.WriteLine("\nEnter Details for Book {0} :", num_of_records);
                AddNewBook_Model addNewBook_Model = obj1.GetBookDetails();
                obj1.AddNewBook(addNewBook_Model);
                count--;
            }
            if (count == 0)
                Console.WriteLine("AddressBook added TO DB successfully...");
            break;
        case 2:
            Console.WriteLine("\n> Add New Contact TO AddressBook ");
            Console.Write("\nHow many contacts you want to add :- ");
            int count2 = Convert.ToInt32(Console.ReadLine());
            int num_of_records2 = 0;
            while (count2 > 0)
            {
                AddressBook_Ops obj3 = new AddressBook_Ops();
                num_of_records2++;
                Console.WriteLine("\nEnter Details for Contact {0} :", num_of_records2);
                AddContact_To_Book_Model addNewBook_Model = obj3.GetContactDetails();
                if (addNewBook_Model != null)
                {
                    obj3.AddNewContact(addNewBook_Model);
                }
                else
                    break;
                count2--;
            }
            if (count2 == 0)
                Console.WriteLine("Contacts added TO DB successfully...");
            break;

        case 3:
            Console.WriteLine("\n> Add New Address TO AddressBook ");
            Console.Write("\nHow many Addresses you want to add :- ");
            int count3 = Convert.ToInt32(Console.ReadLine());
            int num_of_records3 = 0;
            while (count3 > 0)
            {
                AddressBook_Ops obj4 = new AddressBook_Ops();
                num_of_records3++;
                Console.WriteLine("\nEnter Details for Address {0} :", num_of_records3);
                AddAddressofContact_Model addAddressofContact_Model = obj4.GetAddressDetails();
                if (addAddressofContact_Model != null)
                {
                    obj4.AddNewAddress(addAddressofContact_Model);
                }
                else
                    break;
                count3--;
            }
            if (count3 == 0)
                Console.WriteLine("Addresses added TO DB successfully...");
            break;

        case 4:
            Console.WriteLine("\n> AddressBook System Data ");
            Console.Write("Chooce Which Data You want to Display : \n1.AddressBooks Data of AddressBook DB\n2.Contacts Data of AddressBook DB \n3.Addresses Data of AddressBook DB \nEnter your choice :-");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1: obj.DisplayBookList(); break;
                case 2: obj.DisplayContactList(); break;
                case 3: obj.DisplayAddressList(); break;

                default: Console.WriteLine("Enter valid choice!!"); break;
            }
            break;

        case 5:
            Console.WriteLine("----- Thank You -----");
            check = false;
            break;

        default: Console.WriteLine("Please Enter a valid choice!!"); break;
    }
}




