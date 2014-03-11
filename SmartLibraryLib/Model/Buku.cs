using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLibraryLib.Model
{
    [Serializable]
    public class Buku
    {
        private string _iSBN;
        public string ISBN
        {
            get { return _iSBN; }
            set { _iSBN = value; }
        }

        private string _judul;
        public string Judul
        {
            get { return _judul; }
            set { _judul = value; }
        }

        private string _edisi;
        public string Edisi
        {
            get { return _edisi; }
            set { _edisi = value; }
        }

        private string _bahasa;
        public string Bahasa
        {
            get { return _bahasa; }
            set { _bahasa = value; }
        }

        private Penerbit _penerbit = new Penerbit();
        public Penerbit Penerbit
        {
            get { return _penerbit; }
            set { _penerbit = value; }
        }
    }
}
