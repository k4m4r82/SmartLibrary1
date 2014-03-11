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
    // agar method class PenerbitDao bisa diakses secara remote
    // class PenerbitDao harus turunan dari class MarshalByRefObject

    // selain itu class PenerbitDao juga mengimplementasikan interface IPenerbitDao
    public class PenerbitDao : MarshalByRefObject, IPenerbitDao
    {
        // deklarasi objek conn untuk koneksi ke database
        private OleDbConnection conn = null;
        private string strSql = string.Empty;

        // constructor
        public PenerbitDao()
        {
            // TODO : buka koneksi
        }

        public List<Penerbit> GetByName(string namaPenerbit)
        {
            List<Penerbit> daftarPenerbit = new List<Penerbit>();

            strSql = "SELECT id, nama " +
                     "FROM penerbit " +
                     "WHERE nama LIKE @1 " +
                     "ORDER BY nama";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", "%" + namaPenerbit + "%");
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        Penerbit penerbit = MappingDtrToObject(dtr);
                        daftarPenerbit.Add(penerbit);
                    }
                }
            }

            return daftarPenerbit;
        }        

        public DataSet GetReportByName(string penerbit)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Save(Penerbit penerbit)
        {
            strSql = "INSERT INTO penerbit (nama) " +
                     "VALUES (@1)";
            using (OleDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = strSql;
                cmd.Parameters.AddWithValue("@1", penerbit.Nama);                
                cmd.ExecuteNonQuery();

                strSql = "SELECT MAX(id) FROM penerbit";
                cmd.CommandText = strSql;

                // mengembalikan id terakhir penerbit
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Update(Penerbit penerbit)
        {
            strSql = "UPDATE penerbit SET nama = @1 " +
                     "WHERE id = @2";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", penerbit.Nama);
                cmd.Parameters.AddWithValue("@2", penerbit.PenerbitID);

                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(Penerbit penerbit)
        {
            strSql = "DELETE FROM penerbit WHERE id = @1";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@1", penerbit.PenerbitID);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Penerbit> GetAll()
        {
            List<Penerbit> daftarPenerbit = new List<Penerbit>();

            strSql = "SELECT id, nama " +
                     "FROM penerbit " +
                     "ORDER BY nama";
            using (OleDbCommand cmd = new OleDbCommand(strSql, conn))
            {
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        Penerbit penerbit = MappingDtrToObject(dtr);
                        daftarPenerbit.Add(penerbit);
                    }
                }
            }

            return daftarPenerbit;
        }

        public DataSet GetReportAll()
        {
            strSql = "SELECT id, nama " +
                     "FROM penerbit " +
                     "ORDER BY nama";
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = new OleDbCommand(strSql, conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "penerbit");

            return ds;
        }

        private static Penerbit MappingDtrToObject(OleDbDataReader dtr)
        {
            Penerbit penerbit = new Penerbit();
            penerbit.PenerbitID = dtr[0] is DBNull ? 0 : dtr.GetInt32(0);
            penerbit.Nama = dtr[1] is DBNull ? string.Empty : dtr.GetString(1);
            return penerbit;
        }

    }
}
