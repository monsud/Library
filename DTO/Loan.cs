using System;

namespace Library
{
    class Loan
    {
        protected string _cf;
        protected string _id;
        protected DateTime _startDate;
        protected DateTime _endDate;
        protected string _randid;

        public Loan() { }

        public string CF
        {
            get => _cf;
            set => _cf = value;
        }

        public string ID
        {
            get => _id;
            set => _id = value;
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value;
        }

        public string RandID
        {
            get => _randid;
            set => _randid = value;
        }

        public override string ToString()
        {
            return " Fiscal code of the applicant: " + CF + " Document's ID: " + ID + " Start date of the loan: " + StartDate + " End date of the loan: " + EndDate + "Random ID: " + RandID;
        }
    }
}
