using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public interface OpstiDomenskiObjekat
    {
        string Table1();
        string Table2();
        string Insert();
        string Update();
        string Join();
        string Where();

        List<OpstiDomenskiObjekat> GetReaderResult(SqlDataReader reader);
    }
}
