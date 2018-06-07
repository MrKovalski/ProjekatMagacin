using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ProjekatMagacin
{
    [Serializable]
    public class PersonClass : INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        
        //fdfgd

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ime;

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        private string prezime;

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }

        public PersonClass()
        {

        }



    }
}
