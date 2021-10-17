using System;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            CLibrary libAccount = new CLibrary();
            libAccount.Init();
            Console.WriteLine("Welcome to the library application. ");
            int userChoice = 0, searchChoice = 0;
            try
            {
                do
                {
                    Console.WriteLine("=====================================");
                    Console.WriteLine("1. Ask for a book or DVD on loan .");
                    Console.WriteLine("2. Search options");
                    Console.WriteLine("3. Write loan list in a file.");
                    Console.WriteLine("4. Export all library in XML.");
                    Console.WriteLine("5. Read all docs in XML.");
                    Console.WriteLine("6. Exit");
                    Console.WriteLine("=====================================");
                    userChoice = int.Parse(Console.ReadLine());
                    switch (userChoice)
                    {
                        case 1:
                            libAccount.WriteLoan();
                            break;

                        case 2:
                            do
                            {
                                Console.WriteLine("=====================================");
                                Console.WriteLine("Select from following option.");
                                Console.WriteLine("1. Search total of loans about one user by CF.");
                                Console.WriteLine("2. Search a document by ID.");
                                Console.WriteLine("3. Search a document by title.");
                                Console.WriteLine("4. Search total of loans in a single date by a date insert manually.");
                                Console.WriteLine("5. Search total of loans of a document in history with ID.");
                                Console.WriteLine("6. Search a document by author.");
                                Console.WriteLine("7. Search a loan by user's name and surname. ");
                                Console.WriteLine("8. Exit.");
                                searchChoice = int.Parse(Console.ReadLine());
                                switch (searchChoice)
                                {
                                    case 1:
                                        int nLoan = 0;
                                        Console.Write("Enter your fiscal code to search: - ");
                                        string scf = Console.ReadLine();
                                        nLoan = libAccount.TotalOfLoansByCF(scf);
                                        Console.WriteLine("The amount of loans for this user is: ");
                                        Console.WriteLine(nLoan);
                                        break;

                                    case 2:
                                        Console.Write("Enter a code for research: - ");
                                        string qcod = Console.ReadLine();
                                        var queryCod = libAccount.SearchDocByCod(qcod);
                                        foreach (var docum in queryCod)
                                        {
                                            Console.Write(docum.ToString());
                                        }
                                        break;
                                    case 3:
                                        Console.Write("Enter a title for research: - ");
                                        string tit = Console.ReadLine();
                                        var queryTit = libAccount.SearchDocByTitle(tit);
                                        foreach (var title in queryTit)
                                        {
                                            Console.Write(title.ToString());
                                        }
                                        break;
                                    case 4:
                                        Console.Write("Enter a date for research: - ");
                                        DateTime userDateTime;
                                        string dateString = Console.ReadLine();
                                        if (DateTime.TryParse(dateString, out userDateTime))
                                        {
                                            var queryDate = libAccount.SearchLoanByDate(userDateTime);
                                            foreach (var date in queryDate)
                                            {
                                                Console.Write(date.ToString());
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("You have entered an incorrect value.");
                                        }
                                        break;
                                    case 5:
                                        int tot = 0;
                                        Console.Write("Enter a code to search: - ");
                                        string ncod = Console.ReadLine();
                                        tot = libAccount.SearchTotLoansByID(ncod);
                                        Console.WriteLine("The amount of loans for this document is: ");
                                        Console.WriteLine(tot);
                                        break;
                                    case 6:
                                        Console.Write("Enter a name or a surname for research: - ");
                                        string autcod = Console.ReadLine();
                                        var documents = libAccount.SearchDocByAuthor(autcod);
                                        foreach (var doc in documents)
                                        {
                                            Console.WriteLine(doc.ToString());
                                        }
                                        break;
                                    case 7:
                                        Console.Write("Enter a name for research: - ");
                                        string useN = Console.ReadLine();
                                        Console.Write("Enter a surname for research: - ");
                                        string useS = Console.ReadLine();
                                        var userQue= libAccount.SearchLoanByUser(useN,useS);
                                        foreach (var user in userQue)
                                        {
                                            Console.Write(user.ToString());
                                        }
                                        break;
                                    case 8:
                                        break;
                                    default:
                                        Console.WriteLine("Sorry. You have entered wrong choice. Please try again");
                                        break;
                                }
                            } while (searchChoice != 8); 
                            break;

                        case 3:
                            libAccount.LoanToXMLInFile();
                            break;

                        case 4:
                            libAccount.DocumentsToXML();
                            break;
                        case 5:
                            libAccount.Reader();
                            break;
                        case 6:
                            break;

                        default:
                            Console.WriteLine("Sorry. You have entered wrong choice. Please try again");
                            break;
                    }
                } while (userChoice != 6);
            }
            catch (Exception error)
            {
                Console.WriteLine("Something went wrong. " + error.Message);
            }
        }
    }
}
