using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem_TSQL
{
    internal class AddressBook_Ops
    {
        //Connection String to pass to sql constructor get Sqlconnetion
        public const string ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = AddressBookSystem_Day34; Integrated Security = True; Connect Timeout = 40; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Establish SQL Connection TO Database
        SqlConnection sqlconnection = new SqlConnection(ConnectionString);

        public static Dictionary<string, AddNewBook_Model> BookDict = new Dictionary<string, AddNewBook_Model>();
        public static Dictionary<int, AddContact_To_Book_Model> ContactDict = new Dictionary<int, AddContact_To_Book_Model>();
        public static Dictionary<int, AddAddressofContact_Model> AddressDict = new Dictionary<int, AddAddressofContact_Model>();

        //Method to get BookDetails
        public AddNewBook_Model GetBookDetails()
        {
            if (BookDict.Count > 0)
            {
                DisplayBookList();
            }
            AddNewBook_Model addNewBook_Model = new AddNewBook_Model();
            Console.Write("\nEnter Address Book ID :- ");
            addNewBook_Model.BookId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Address Book Name :- ");
            addNewBook_Model.BookName = Console.ReadLine();
            foreach (var Bookdata in BookDict.Values)
            {
                if (Bookdata.BookId == addNewBook_Model.BookId)
                {
                    Console.WriteLine("This Address Book ID already Exist!!");
                    return default;
                }
                if (Bookdata.BookName == addNewBook_Model.BookName)
                {
                    Console.WriteLine("This Address Book Name already Exist!!");
                    return default;
                }
            }
            Console.Write("Enter Address Book Type :- 1.Ordinary  2.Commercial  3.Private\nType in words here :- ");
            addNewBook_Model.BookType = Console.ReadLine();
            Console.Write("Enter Date of Address Book Creation :- ");
            addNewBook_Model.DateofBookCreation = Convert.ToDateTime(Console.ReadLine());

            BookDict.Add(addNewBook_Model.BookName, addNewBook_Model);
            return addNewBook_Model;
        }

        //Method to get Contact detials of BOOk
        public AddContact_To_Book_Model GetContactDetails()
        {
            DisplayBookList();
            Console.Write("\nChoose the BookName in which You want to add Contact:");
            string bookchoice = Console.ReadLine();
            AddContact_To_Book_Model addContact_To_Book_Model = new AddContact_To_Book_Model();
            foreach (var bookdata in BookDict)
            {
                if (bookdata.Value.BookName == bookchoice)
                {
                    addContact_To_Book_Model.BookId = bookdata.Value.BookId;
                    Console.Write("\nEnter Person ID :- ");
                    addContact_To_Book_Model.PersonId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter Person Name :- ");
                    addContact_To_Book_Model.PersonName = Console.ReadLine();
                    Console.Write("Enter Gender :- ");
                    addContact_To_Book_Model.Gender = Convert.ToChar(Console.ReadLine());
                    Console.Write("Enter Mobile Number :- ");
                    addContact_To_Book_Model.MobileNo = Convert.ToInt64(Console.ReadLine());

                    ContactDict.Add(addContact_To_Book_Model.PersonId, addContact_To_Book_Model);
                    return addContact_To_Book_Model;
                }
            }
            Console.WriteLine("BookName Not Found!!");
            return default;
        }

        //Method to get Address Details
        public AddAddressofContact_Model GetAddressDetails()
        {
            DisplayBookList();
            Console.Write("\nChoose a BookName in which You want to add address Details :");
            string bookchoice = Console.ReadLine();
            AddAddressofContact_Model addAddressofContact_Model = new AddAddressofContact_Model();
            foreach (var bookdata in BookDict)
            {
                if (bookdata.Key == bookchoice)
                {
                    DisplayContactList();
                    Console.Write("\nChoose a PersonId for which you want to add address Details :");
                    int Personchoice = Convert.ToInt32(Console.ReadLine());
                    foreach (var contactdata in ContactDict)
                    {
                        if (contactdata.Key == Personchoice)
                        {
                            addAddressofContact_Model.PersonId = Personchoice;
                            Console.Write("Enter Address ID :- ");
                            addAddressofContact_Model.AddressId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\nEnter City :- ");
                            addAddressofContact_Model.City = Console.ReadLine();
                            Console.Write("Enter State :- ");
                            addAddressofContact_Model.State = Console.ReadLine();
                            Console.Write("Enter ZipCode :- ");
                            addAddressofContact_Model.ZipCode = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Country :- ");
                            addAddressofContact_Model.Country = Console.ReadLine();

                            AddressDict.Add(addAddressofContact_Model.AddressId, addAddressofContact_Model);
                            return addAddressofContact_Model;
                        }
                    }
                    Console.WriteLine("PersonId Not Found!!");
                    return default;
                }
                else
                {
                    Console.WriteLine("BookName Not Found!!");
                    return default;
                }

            }
            return default;
        }

        //Method TO add Book()
        public void AddNewBook(AddNewBook_Model bookdetails)
        {
            try
            {
                using (sqlconnection)
                {
                    SqlCommand command = new SqlCommand("SP_AddNewBook", sqlconnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookName", bookdetails.BookName);
                    command.Parameters.AddWithValue("@BookType", bookdetails.BookType);
                    command.Parameters.AddWithValue("@DateofBookCreation", bookdetails.DateofBookCreation);

                    sqlconnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }

        }

        //Method TO add contact to book
        public void AddNewContact(AddContact_To_Book_Model contactdetails)
        {
            try
            {
                using (sqlconnection)
                {
                    SqlCommand command = new SqlCommand("SP_AddContacts", sqlconnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("BookId", contactdetails.BookId);
                    command.Parameters.AddWithValue("@PersonName", contactdetails.PersonName);
                    command.Parameters.AddWithValue("@Gender", contactdetails.Gender);
                    command.Parameters.AddWithValue("@MobileNo", contactdetails.MobileNo);

                    sqlconnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }

        }

        //Method TO add contact to book
        public void AddNewAddress(AddAddressofContact_Model Addressdetails)
        {
            try
            {
                using (sqlconnection)
                {
                    SqlCommand command = new SqlCommand("SP_AddAddresses", sqlconnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonId", Addressdetails.PersonId);
                    command.Parameters.AddWithValue("@City", Addressdetails.City);
                    command.Parameters.AddWithValue("@State", Addressdetails.State);
                    command.Parameters.AddWithValue("@ZipCode", Addressdetails.ZipCode);
                    command.Parameters.AddWithValue("@Country", Addressdetails.Country);

                    sqlconnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }

        }

        //MEthod TO Display Books Exists in AddressbookSystem DB
        public void DisplayBookList()
        {
            try
            {
                AddNewBook_Model book = new AddNewBook_Model();

                string query = @"select * from Books ";
                Console.WriteLine("\nBooks Present in AddressBook System DB :");
                int count = 0;

                this.sqlconnection.Open();

                //Passign query to sql command object
                SqlCommand command = new SqlCommand(query, this.sqlconnection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    //Read each row
                    while (sqlDataReader.Read())
                    {
                        book.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        book.BookName = Convert.ToString(sqlDataReader["BookName"]);
                        book.BookType = Convert.ToString(sqlDataReader["BookType"]);
                        book.DateofBookCreation = Convert.ToDateTime(sqlDataReader["DateofBookCreation"]);

                        //Display Record of current object
                        Console.WriteLine("\nBook ID :- {0}\t| Book Name :- {1}\t| Book Type:- {2}\t| Date of Book Creation :- {3}", book.BookId, book.BookName, book.BookType, book.DateofBookCreation);
                    }

                }
                else
                {
                    Console.WriteLine("\n> Your Books DataBase is Empty!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlconnection.Close();
            }

        }

        //MEthod TO Display Contacts Exists in AddressbookSystem DB
        public void DisplayContactList()
        {
            try
            {
                AddContact_To_Book_Model contact = new AddContact_To_Book_Model();

                string query = @"select * from Contacts ";
                Console.WriteLine("\nContacts Present in AddressBook System DB :");
                int count = 0;

                this.sqlconnection.Open();

                //Passign query to sql command object
                SqlCommand command = new SqlCommand(query, this.sqlconnection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    //Read each row
                    while (sqlDataReader.Read())
                    {
                        contact.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        contact.PersonId = Convert.ToInt32(sqlDataReader["PersonId"]);
                        contact.PersonName = Convert.ToString(sqlDataReader["PersonName"]);
                        contact.Gender = Convert.ToChar(sqlDataReader["Gender"]);
                        contact.MobileNo = Convert.ToInt64(sqlDataReader["MobileNo"]);

                        //Display Record of current object
                        Console.WriteLine("\nBook ID :- {0}\t| Person ID :- {1}\t| Person Name :- {2}\t| Gender :- {3}\t| Mobile No. :- {4}", contact.BookId, contact.PersonId, contact.PersonName, contact.Gender, contact.MobileNo);
                    }

                }
                else
                {
                    Console.WriteLine("\n> Your Contacts DataBase is Empty!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlconnection.Close();
            }

        }

        //MEthod TO Display Address Exists in AddressbookSystem DB
        public void DisplayAddressList()
        {
            try
            {
                AddAddressofContact_Model Address = new AddAddressofContact_Model();

                string query = @"select * from Addresses ";
                Console.WriteLine("\nAddress Present in AddressBook System DB :");
                int count = 0;

                this.sqlconnection.Open();

                //Passign query to sql command object
                SqlCommand command = new SqlCommand(query, this.sqlconnection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    //Read each row
                    while (sqlDataReader.Read())
                    {
                        Address.PersonId = Convert.ToInt32(sqlDataReader["PersonId"]);
                        Address.AddressId = Convert.ToInt32(sqlDataReader["AddressId"]);
                        Address.City = Convert.ToString(sqlDataReader["City"]);
                        Address.State = Convert.ToString(sqlDataReader["State"]);
                        Address.AddressId = Convert.ToInt32(sqlDataReader["ZipCode"]);
                        Address.Country = Convert.ToString(sqlDataReader["Country"]);

                        //Display Record of current object
                        Console.WriteLine("\nPerson ID :- {0}\t| Address ID :- {1}\t| City :- {2}\t| State :- {3}\t| ZipCode :- {4}\t| Country :- {5}", Address.PersonId, Address.AddressId, Address.City, Address.State, Address.ZipCode, Address.Country);
                    }

                }
                else
                {
                    Console.WriteLine("\n> You dont have any Address in your DataBase !!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlconnection.Close();
            }

        }

    }
}
