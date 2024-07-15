using BlogHeadless.Api.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Data.Models.BlogPost
{
    public  class BlogPostTags:AbstractStringField
    {
        public List<string> GetTags() 
        { 
           if (Value is null) 
              return new List<string>();
          
           return Value.Split(',').ToList();
            
        }

        public BlogPostTags(List<string> blogPostTags) 
        { 
            if(blogPostTags is null)
            {
               throw  new ArgumentNullException(nameof(blogPostTags));
            }

            if(blogPostTags.Count() <= 0) 
            { 
               throw new ArgumentException(nameof(blogPostTags));
            }

            Value = string.Join(",", blogPostTags);
        }

        public BlogPostTags(string blogPostTags)
        {
            Value = blogPostTags;
        }

    }
}
