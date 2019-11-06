using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace app.DAL
{
    public class DalParameter
    {
        public string ParameterName { get; set; }
        public ParameterDirection Direction { get; set; }
        public DbType DbType { get; set; }
        public object Value { get; set; }
    }
}