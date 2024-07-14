using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models
{
    public  class AuthorName:AbstractStringField
    {
        public AuthorName(string authorName) 
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentNullException("AuthorName can't be null or mepty");
            }

            authorName=authorName.Trim();

            if (authorName.Length < 3)
            {
                throw new ArgumentNullException("AuthorName must be atlest 3 character long");

            }

            Value = authorName;

        }
    }
}
