using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

// namespace SmartLibraryLib.IDao dan SmartLibraryLib.Model perlu ditambahkan
// agar bisa mengakses semua interface IDao dan class Model
using SmartLibraryLib.IDao;
using SmartLibraryLib.Model;

// namespace untuk mengakses Db Ms Access
using System.Data.OleDb;

namespace SmartLibraryServer.Dao
{
    // agar method class BukuDao bisa diakses secara remote
    // class BukuDao harus turunan dari class MarshalByRefObject

    // selain itu class BukuDao juga mengimplementasikan interface IBukuDao
    public class BukuDao : MarshalByRefObject, IBukuDao
    {
        // deklarasi objek conn untuk koneksi ke database
        private OleDbConnection conn = null;
        private string strSql = string.Empty;

        // constructor
        public BukuDao()
        {
            string appDir = System.IO.Directory.GetCurrentDirectory();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + appDir + "\\DbPerpus.mdb;User Id=admin;Password=;";
            conn = new OleDbConnection(strConn);
            conn.Open();
        }

        public List<Buku> GetByName(string judul)
        {
            List<Buku> daftarBuku = new List<Buku>();

            strSql = "SELECT isbn, judul, edisi, bahasa " +
                     "FROM buku " +
                     "WHERE judul LIKE @1 " +
                     "ORDER BY judul";                
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", "%" + judul + "%");
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        Buku buku = MappingDtrToObject(dtr);
                        daftarBuku.Add(buku);
                    }
                }
            }
            return daftarBuku;
        }        

        public Buku GetByID(string bukuID)
        {
            Buku buku = null;

            strSql = "SELECT isbn, judul, edisi, bahasa " +
                     "FROM buku " +
                     "WHERE isbn = @1";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", bukuID);
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                {
                    if (dtr.Read())
                    {
                        buku = MappingDtrToObject(dtr);
                    }
                }
            }
            return buku;
        }

        public DataSet GetReportByName(string judul)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Save(Buku buku)
        {
            strSql = "INSERT INTO buku (isbn, judul, edisi, bahasa, penerbit_id) " +
                     "VALUES (@1, @2, @3, @4, @5)";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", buku.ISBN);
                cmd.Parameters.AddWithValue("@2", buku.Judul);
                cmd.Parameters.AddWithValue("@3", buku.Edisi);
                cmd.Parameters.AddWithValue("@4", buku.Bahasa);
                cmd.Parameters.AddWithValue("@5", buku.Penerbit.PenerbitID);

                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(Buku buku)
        {
            strSql = "UPDATE buku SET judul = @1, edisi = @2, bahasa = @3, penerbit_id = @4 " +
                     "WHERE isbn = @5";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", buku.Judul);
                cmd.Parameters.AddWithValue("@2", buku.Edisi);
                cmd.Parameters.AddWithValue("@3", buku.Bahasa);
                cmd.Parameters.AddWithValue("@4", buku.Penerbit.PenerbitID);
                cmd.Parameters.AddWithValue("@5", buku.ISBN);

                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(Buku buku)
        {
            strSql = "DELETE FROM buku WHERE isbn = @1";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", buku.ISBN);

                return cmd.ExecuteNonQuery();
            }
        }

        public List<Buku> GetAll()
        {
            List<Buku> daftarBuku = new List<Buku>();

            strSql = "SELECT isbn, judul, edisi, bahasa " +
                     "FROM buku " +
                     "ORDER BY judul";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        Buku buku = MappingDtrToObject(dtr);
                        daftarBuku.Add(buku);
                    }
                }
            }
            return daftarBuku;
        }        

        public DataSet GetReportAll()
        {
            strSql = "SELECT isbn, judul, edisi, bahasa " +
                     "FROM buku " +
                     "ORDER BY judul";
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = new OleDbCommand(strSql, conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "buku");

            return ds;
        }

        private static Buku MappingDtrToObject(OleDbDataReader dtr)
        {
            Buku buku = new Buku();
            buku.ISBN = dtr[0] is DBNull ? string.Empty : dtr.GetString(0);
            buku.Judul = dtr[1] is DBNull ? string.Empty : dtr.GetString(1);
            buku.Edisi = dtr[2] is DBNull ? string.Empty : dtr.GetString(2);
            buku.Bahasa = dtr[3] is DBNull ? string.Empty : dtr.GetString(3);

            return buku;
        }

    }
}
