using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

// tambahkan agar bisa mengakses class model
using SmartLibraryLib.Model;

namespace SmartLibraryLib.IDao
{
    // interface IPenerbitDao turunan dari interface IDao
    public interface IPenerbitDao : IDao<Penerbit>
    {
        List<Penerbit> GetByName(string penerbit);

        // untuk keperluan membuat report
        // menggunakan Active Report .NET
        DataSet GetReportByName(string penerbit);
    }
}
