using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

// tambahkan agar bisa mengakses class model
using SmartLibraryLib.Model;

namespace SmartLibraryLib.IDao
{
    // interface IBukuDao turunan dari interface IDao
    public interface IBukuDao : IDao<Buku>
    {
        List<Buku> GetByName(string judul);
        Buku GetByID(string bukuID);

        // untuk keperluan membuat report
        // menggunakan Active Report .NET
        DataSet GetReportByName(string judul);
    }
}
