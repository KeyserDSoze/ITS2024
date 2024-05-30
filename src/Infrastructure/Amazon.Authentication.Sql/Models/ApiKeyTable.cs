using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Authentication.Sql.Models
{
    public sealed class ApiKeyTable
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
