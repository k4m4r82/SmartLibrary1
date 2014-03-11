using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLibraryLib.Model
{
    [Serializable]
    public class Penerbit
    {
        private int _penerbitID;
        public int PenerbitID
        {
            get { return _penerbitID; }
            set { _penerbitID = value; }
        }

        private string _nama;
        public string Nama
        {
            get { return _nama; }
            set { _nama = value; }
        }
    }
}
