using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace SmartLibraryLib.IDao
{
    public interface IDao<T>
    {
        int Save(T entity);
        int Update(T entity);
        int Delete(T entity);

        List<T> GetAll();
        DataSet GetReportAll();
    }
}
