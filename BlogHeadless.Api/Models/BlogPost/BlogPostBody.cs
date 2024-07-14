using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Data.Models.BlogPost
{
    public class BlogPostBody
    {
        public string Value { get; private set; }

        public BlogPostBody(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("BlogBody can't be null or empty");
            }

            Value = value.Trim();

            if (value.Length < 50)
            {
                throw new ArgumentException("BlogBody has at least 50 character");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return Value.Equals(obj);
        }


    }
}
