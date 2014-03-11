using System;
using System.Collections.Generic;
using System.Text;

using System.Data.OleDb;

namespace SmartLibraryServer
{
    public class DBConnection
    {
        public static OleDbConnection GetConnection()
        {
            string appDir = System.IO.Directory.GetCurrentDirectory();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + appDir + "\\DbPerpus.mdb";

            // deklarasi objek koneksi
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open(); // buka koneksi

            return conn;
        }
    }
}

