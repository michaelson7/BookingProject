using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IDataAccess
    {
        Task<List<T>> LoadDataAsync<T, U>(string sqlStatement, U parameters, string connectionStringName, bool isStoredProcedure = false);
        Task<int> SaveDataAsync<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoredProcedure = false);
    }
}
