using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using static Library.Document;

namespace Library
{
    class CLibrary {

        protected List<User> _userList = new List<User>();
        protected List<Document> _documList = new List<Document>();
        protected List<Loan> _loanList = new List<Loan>();
        public CLibrary()

        {
            _userList = new List<User>();
            _documList = new List<Document>();
            _loanList = new List<Loan>();
        }
        public void Init() {
            ReadAccounts();
            ReadBooks();
            ReadDVDs();
            ReadLoans();
        }

        //Account methods
        public void CreateAccount(string cf, string name, string surname, string mail, string pass, string num)
        {
            try
            {
                User u = new User();
                if (!IsUserUniqueByCF(cf))
                {
                    u.CF = cf;
                    u.Name = name;
                    u.Surname = surname;
                    u.Mail = mail;
                    u.Pass = pass;
                    u.Num = num;
                    _userList.Add(u);
                } 
                else
                {
                    Console.Write("This account just exists. ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void ReadAccounts()
        {
            var file = @"z:/Library/Files/users.txt";
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        string[] strArray;
                        string str = sr.ReadLine();

                        strArray = str.Split('|');
                        if (strArray.Length != 6)
                        {
                            Console.WriteLine("Array isn't complete. ");
                        }
                        else
                        {
                            CreateAccount(strArray[0], strArray[1], strArray[2], strArray[3], strArray[4], strArray[5]);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        //Document methods (books and DVDs)
        public void CreateBook(string id, string title, string year, string sec, string sta, string sid, string aut, string npag)
        {
            try
            {
                Book book = new Book();
                if(!IsDocumentUniqueByID(id))
                {
                    book.ID = id;
                    book.Title = title;
                    book.Year = year;
                    Sector sector = (Sector)Enum.Parse(typeof(Sector), sec);
                    if (Enum.IsDefined(typeof(Sector), sector))
                    {
                        book.Sec = sector;
                    }
                    State state = (State)Enum.Parse(typeof(State), sta);
                    if (Enum.IsDefined(typeof(State), state))
                    {
                        book.Sta = state;
                    }
                    book.ShelfId = sid;
                    book.NPage = npag;

                    book.authors = new List<Author>();

                    // Nome3,Cognome3;Nome4,Cognome4
                    string[] arrayAuthors = aut.Split(';');
                    if (arrayAuthors.Count() > 0)
                    {
                        //Nome3,Cognome3;
                        foreach (string authorString in arrayAuthors)
                        {
                            string[] arrayAuthor = authorString.Split(',');
                            if (arrayAuthor.Count() == 2)
                            {
                                Author author = new Author();
                                author.NameAut = arrayAuthor[0];
                                author.SurAut = arrayAuthor[1];
                                book.authors.Add(author);
                            }
                        }
                    }
                    _documList.Add(book);
                } 
                else
                {
                    Console.Write("This book just exists. ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void CreateDVD(string id, string title, string year, string sec, string sta, string sid, string aut, string dur)
        {
            try
            {
                DVD dvd = new DVD();
                if (!IsDocumentUniqueByID(id))
                {
                    dvd.ID = id;
                    dvd.Title = title;
                    dvd.Year = year;
                    Sector sector = (Sector)Enum.Parse(typeof(Sector), sec);
                    if (Enum.IsDefined(typeof(Sector), sector))
                    {
                        dvd.Sec = sector;
                    }
                    State state = (State)Enum.Parse(typeof(State), sta);
                    if (Enum.IsDefined(typeof(State), state))
                    {
                        dvd.Sta = state;
                    }
                    dvd.ShelfId = sid;
                    dvd.Dur = dur;
                    dvd.authors = new List<Author>();

                    string[] arrayAuthors = aut.Split(';');
                    if (arrayAuthors.Count() > 0)
                    {
                        foreach (string authorString in arrayAuthors)
                        {
                            string[] arrayAuthor = authorString.Split(',');
                            if (arrayAuthor.Count() == 2)
                            {
                                Author author = new Author();
                                author.NameAut = arrayAuthor[0];
                                author.SurAut = arrayAuthor[1];
                                dvd.authors.Add(author);
                            }
                        }
                    }
                    _documList.Add(dvd);
                }
                 else
                {
                    Console.Write("This DVD just exists. ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void ReadBooks()
    {
        var file = @"z:/Library/Files/books.txt";
        int numLine = 0;
        try
        {
            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.Peek() >= 0)
                {
                    numLine++;
                    string[] strArray;
                    string str = sr.ReadLine();

                    strArray = str.Split('|');
                    if (strArray.Length != 8)
                    {
                        Console.WriteLine(String.Format("Line {0} isn't complete. ", numLine));
                    }
                    else {
                         CreateBook(strArray[0], strArray[1], strArray[2], strArray[3], strArray[4], strArray[5], strArray[6], strArray[7]);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
        public void ReadDVDs()
        {
            var file = @"z:/Library/Files/dvds.txt";
            int numLine = 0;
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        numLine++;
                        string[] strArray;
                        string str = sr.ReadLine();

                        strArray = str.Split('|');
                        if (strArray.Length != 8)
                        {
                            Console.WriteLine(String.Format("Line {0} isn't complete. ", numLine));
                        }
                        else
                        {
                            CreateDVD(strArray[0], strArray[1], strArray[2], strArray[3], strArray[4], strArray[5], strArray[6], strArray[7]);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public void LoanToXMLInFile()
        {
            if (_loanList.Count > 0)
            {
                XElement element =
                    new XElement("LoanList",
                    from loan in _loanList
                    select new XElement("Information",
                        new XElement("FiscalCode", loan.CF),
                        new XElement("DocumentID", loan.ID),
                        new XElement("StartDate", loan.StartDate),
                        new XElement("EndDate", loan.EndDate),
                        new XElement("RandomCode", loan.RandID))
                 );
                element.Save("loanout.xml");
                string str = File.ReadAllText("loanout.xml");
                Console.WriteLine(str);
            }
            else
                throw new Exception("List is null. Cannot write a null list");
        }
        public void DocumentsToXML()
        {
            if (_documList.Count > 0)
            {
                XElement element =
                    new XElement("Library",
                    from doc in _documList
                    select new XElement("Document",
                        new XElement("DocumentID", doc.ID),
                        new XElement("Title", doc.Title),
                        new XElement("Year", doc.Year),
                        new XElement("ShelfID", doc.ShelfId),
                        new XElement("Status",
                            new XAttribute("Type","Actual"),
                        doc.Sta),
                        new XElement("Sector",
                            new XAttribute("Type", "Name"),
                        doc.Sec),
                     new XElement("Authors",
                     from Author aut in doc.authors
                     select new XElement("Author",
                        new XElement("Name", aut.NameAut),
                        new XElement("Surname", aut.SurAut))
                        )
                       )
                 );
                element.Save("doc.xml");
                string str = File.ReadAllText("doc.xml");
                Console.WriteLine(str);
            }
            else
                throw new Exception("List is null. Cannot write a null list");
        }

        public void Reader()
        {
            string sourcePath = @"z:\Library\Files\docs.xml";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(sourcePath);

            XmlNodeList nodeList = xdoc.GetElementsByTagName("Document");

            foreach (XmlNode node in nodeList)
            {
                dynamic doc = null;
                string typeImm = node.SelectSingleNode("Type").InnerText;
                switch (typeImm)
                {
                    case "Book":
                        doc = new Book();
                        doc.NPage = node.SelectSingleNode("NumberOfPages").InnerText;
                        break;
                    case "DVD":
                        doc = new DVD();
                        doc.Dur = node.SelectSingleNode("Duration").InnerText;
                        break;
                }
                doc.ID = node.SelectSingleNode("HouseCode").InnerText;
                imm.Sup = node.SelectSingleNode("AreaInMq").InnerText;

                XmlNode addressNode = node.SelectSingleNode("Address");
                foreach (XmlNode addressChild in addressNode.ChildNodes)
                {
                    switch (addressChild.Name)
                    {
                        case "Street":
                            imm.Address = addressChild.InnerText;
                            break;
                        case "City":
                            imm.City = addressChild.InnerText;
                            break;
                        case "PostalCode":
                            imm.CAP = addressChild.InnerText;
                            break;
                    }
                }
            }
        }


        //Loan methods 
        public void CreateLoan (string cf, string id, string start, string end)
        {
            StringBuilder sb = new StringBuilder();
            int lenght = 10;
            var stringChars = new char[lenght];
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var rand = new Random();
            try
            {
                Loan l = new Loan();
                //if ((!IsBorrowedAlready(id,sb)) && (CanBorrowDocument(cf,sb)))
                //{
                    l.CF = cf;
                    l.ID = id;
                    DateTime str;
                    if(DateTime.TryParse(start, out str)) {
                        l.StartDate = str;
                    }
                    DateTime ed;
                    if (DateTime.TryParse(end, out ed))
                    {
                        l.EndDate = ed;
                    }
                   for (int i = 0; i < stringChars.Length; i++) {
                        stringChars[i] = chars[rand.Next(chars.Length)];
                    }
                    l.RandID = new string(stringChars);
                _loanList.Add(l);
                //}
                //else
                //{
                //    Console.WriteLine(sb);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void ReadLoans()
        {
            var file = @"z:/Library/Files/loans.txt";
            int numLine = 0;
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        numLine++;
                        string[] strArray;
                        string str = sr.ReadLine();

                        strArray = str.Split('|');
                        if (strArray.Length != 4)
                        {
                            Console.WriteLine(String.Format("Line {0} isn't complete. ", numLine));
                        }
                        else
                        {
                             CreateLoan(strArray[0], strArray[1], strArray[2], strArray[3]);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public int TotalOfLoansByCF(string cf)
        {
            int count = 0;
            try
            {
                List<Loan> numberList = new List<Loan>();
                if (_loanList != null)
                {
                    foreach (Loan l in _loanList)
                    {
                        if ((l.CF == cf) && (DateTime.Today <= l.EndDate))
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return count;
        }
        public void WriteLoan()
        {
            var file = @"z:/Library/Files/newloans.txt";
            try
            {
                /*
                foreach (Loan loan in _loanList)
                {
                    File.AppendAllText(file,loan + Environment.NewLine);
                }
                */
                foreach (Loan loan in _loanList)
                {
                    File.WriteAllText(file, loan.CF + ";");
                    File.WriteAllText(file, loan.ID + ";");
                    File.WriteAllText(file, loan.StartDate.ToString() + ";");
                    File.WriteAllText(file, loan.EndDate.ToString() + ";");
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        //Checking methods
        public bool IsUserUniqueByCF(string cf)
        {
            try
            {
                if (_userList != null)
                {
                    var CFQuery =
                        (from user in _userList
                         where user.CF == cf
                         select user).Count();
                    if (CFQuery == 1)
                    {
                        return true;
                    }
                }
                return false;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }
        public bool IsDocumentUniqueByID(string id)
        {
            try
            {
                if (_documList != null)
                {
                    var IDQuery =
                        (from doc in _documList
                         where doc.ID == id
                         select doc).Count();
                    if (IDQuery == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }
        public bool IsUserRandomCodeUnique(string rand)
        {
            try
            {
                  var CodQuery =
                        (from loan in _loanList
                         where loan.RandID == rand
                         select loan).Count();
                    if (CodQuery == 1)
                    {
                        return true;
                    }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }
        public bool CanBorrowDocument(string cf, StringBuilder sb)
        {
            int total = 3;
            int c = 0;
            {
                try
                {
                    if (_loanList != null)
                    {
                        foreach (Loan l in _loanList)
                        {
                            if (l.CF == cf)
                            {
                                c++;
                            }
                        }
                    }
                    sb.AppendLine(" You have just requested maximum loan. ");
                    return !(c >= total);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return false;
                }
            }
        }
        public bool IsBorrowedAlready(string id, StringBuilder sb)
        {
            {
                try
                {
                    if (_documList != null)
                    {
                        foreach (Document d in _documList)
                        {
                            if ((d.ID == id) && (d.Sta == State.Borrowed))
                            {
                                sb.AppendLine("This document is just borrowed. ");
                                return true;
                            }
                            d.Sta = State.Borrowed;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return false;
                }
            }
        }

        //Searching methods
        public IEnumerable<Document> SearchDocByCod (string cod)
        {
            try
            {
                if (_documList != null)
                {
                    var CodQuery =
                        from doc in _documList
                        where doc.ID == cod
                        select doc;
                    return CodQuery;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public IEnumerable<Document> SearchDocByTitle(string tit)
        {
            try
            {
                if (_documList != null)
                {
                    var TitQuery =
                        from doc in _documList
                        where doc.Title == tit
                        select doc;
                    return TitQuery;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public IEnumerable<Loan> SearchLoanByDate(DateTime date)
        {
            try
            {
                if (_loanList != null)
                {

                    var DateQuery =
                        from loan in _loanList
                        where loan.StartDate == date
                        select loan;
                    return DateQuery;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public int SearchTotLoansByID(string id)
        {
            int count = 0;
            try
            {
                if (_loanList != null)
                {

                    var NumQuery =
                        from loan in _loanList
                        where loan.ID == id
                        select loan;
                    count = NumQuery.Count();
                }
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return count;
            }
        }
        public IEnumerable<Document> SearchDocByAuthor (string key)
        {
            try
            {
                if (_documList != null)
                {
                    var AutQuery = from doc in _documList
                                 from Author aut in doc.authors
                                 where aut.NameAut == key || aut.SurAut == key
                                 select doc;
                    return AutQuery;
                }
                return null;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }

        }  
        public IEnumerable<Loan> SearchLoanByUser (string name, string surname)
        {
            try
            {
                if (_loanList != null && _userList != null)
                {
                    var UserQuery =
                        from user in _userList
                        join loan in _loanList on user.CF equals loan.CF 
                        where user.Name == name && user.Surname == surname
                        select  loan;
                    return UserQuery;
                }
                return null;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
    }
}