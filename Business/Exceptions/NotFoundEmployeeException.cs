using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class NotFoundEmployeeException : Exception
    {
        public string PropertyName { get; set; }
        public NotFoundEmployeeException(string porpName,string? message) : base(message)
        {
            PropertyName = porpName;
        }
    }
}
