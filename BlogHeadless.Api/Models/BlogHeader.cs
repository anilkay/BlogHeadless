using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models
{
    public  class BlogHeader: AbstractStringField
    {

        public BlogHeader(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("BlogHeader can't be null or empty");
            }

             Value = value.Trim();

            if (value.Length < 5)
            {
                throw new ArgumentException("BlogHeader has at least 50 character");
            }

            Value = value;
        }

      

    }
}
