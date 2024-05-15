using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class ContentTypeException : Exception
    {
        public string PropertyName { get; set; }
        public ContentTypeException(string propName,string? message) : base(message)
        {
            PropertyName = propName;
        }
    }
}
