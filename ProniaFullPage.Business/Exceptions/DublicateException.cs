using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Exceptions
{
    public class DublicateException : Exception
    {
        public DublicateException(string? message) : base(message)
        {
        }
    }
}
