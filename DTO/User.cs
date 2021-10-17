using System;

namespace Library {
    class User {  
        protected string _cf;  
        protected string _name;  
        protected string _surname;
        protected string _mail;
        protected string _pass;
        protected string _num;
  
        public User() { }  
        
        public string CF {  
            get => _cf;   
            set => _cf = value; 
        }

        public string Name {  
            get => _name;   
            set => _name = value; 
        }  

        public string Surname {
            get => _surname;
            set => _surname = value;
        }

        public string Mail {
            get => _mail;
            set => _mail = value;
        }

        public string Pass {
            get => _pass;
            set => _pass = value;
        }

        public string Num {
            get => _num;
            set => _num = value;
        }
    }   
}
