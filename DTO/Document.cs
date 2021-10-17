using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Library {
    class Document {  
        protected string _id;  
        protected string _title;  
        protected string _year;
        protected Sector _sec;
        protected State _sta;
        protected string _shelfid;
        protected List<Author> _authors;
        public enum Sector {
            History,
            Math,
            Science,
            IT,
            Music
        }

        public enum State {
            Borrowed,
            Available
        }
  
        public Document() { } 

        public string ID {  
            get => _id;   
            set => _id = value; 
        }

        public string Title {  
            get => _title;   
            set => _title = value; 
        }  

        public string Year {
            get => _year;
            set => _year = value;
        }

        public Sector Sec {
            get => _sec;
            set => _sec = value;
        }

        public State Sta {
            get => _sta;
            set => _sta = value;
        }

        public string ShelfId {
            get => _shelfid;
            set => _shelfid = value;
        }
        public List<Author> authors
        {
            get => _authors;
            set => _authors = value;
        }

        public virtual XElement toXML()
        {
            XElement element =
                 new XElement("Document",
                 new XElement("DocumentID", ID),
                 new XElement("Title", Title),
                 new XElement("Year", Year),
                 new XElement("ShelfID", ShelfId),
                 new XElement("Status", Sta),
                 new XElement("Sector", Sec));
            return element;
        }
    }

    class Author
    {
        protected string _nameaut;
        protected string _suraut;

        public string NameAut
        {
            get => _nameaut;
            set => _nameaut = value;
        }

        public string SurAut
        {
            get => _suraut;
            set => _suraut = value;
        }
        public override string ToString()
        {
            return NameAut + " " + SurAut;
        }
        public XElement GetAutXML()
        {
            XElement element =
            new XElement("Name", NameAut,
            new XElement("Surname", SurAut));
            return element;
        }
    }

    //Child class: Book
    class Book : Document {  
        protected string _npage;  
        public Book() { }  
        public string NPage {
            get => _npage;
            set => _npage = value;
        }

        public string GetAuthorsToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Author author in authors)
            {
                sb.Append(author.ToString());
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return "BOOK == " + " ISBN: " + ID + " Title: " + Title + " Year: " + Year + " Sector: " + Sec + " State: " + Sta + " Shelf ID " + ShelfId + " Author's name and surname " + GetAuthorsToString() + " " + " Number of pages: " + NPage;
        }

        public override XElement toXML()
        {
            XElement elementBase = base.toXML();
            elementBase.Add(new XElement("Type", "Book"));
            elementBase.Add(new XElement("NumberOfPages", NPage));
            return elementBase;
        }
    }

    //Child class: DVD
    class DVD : Document {  
        private string _dur;
        public DVD() { }  
        public string Dur {
            get => _dur;
            set => _dur = value;
        }
        public string GetAuthorsToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Author author in authors)
            {
                sb.Append(author.ToString());
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return "DVD == " + " Serial number: " + ID + " Title: " + Title + " Year: " + Year + " Sector: " + Sec + " State: " + Sta + " Shelf ID " + ShelfId + " Author's name and surname " + GetAuthorsToString() + " Duration of the film: " + Dur;
        }

        public override XElement toXML()
        {
            XElement elementBase = base.toXML();
            elementBase.Add(new XElement("Type", "DVD"));
            elementBase.Add(new XElement("Duration", Dur));
            return elementBase;
        }
    }
}   
