using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public record FromSqlRawParams
    {
        public string Sql { get; set; }
        public object[] Parameters { get; set; }

        public FromSqlRawParams(string Sql, object[] Parameters = default)
        {
            this.Sql = Sql;
            this.Parameters = Parameters;
        }

    }
}
