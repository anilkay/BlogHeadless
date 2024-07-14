using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models
{
    public  class Email:AbstractStringField
    {
        public Email(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Invalid Email");
            }

            var matchResult=Regex.IsMatch(email,
                  @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                  RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            if (!matchResult)
            {
                throw new ArgumentException("Invalid Email");
            }

            Value = email;




        }
    }
}
