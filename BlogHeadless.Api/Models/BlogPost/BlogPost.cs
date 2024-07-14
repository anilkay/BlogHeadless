using BlogHeadless.Api.Models.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Data.Models.BlogPost
{
    public class BlogPost
    {
        public required BlogPostId Id { get; set; }
        public required BlogPostHeader BlogHeader { get; set; }
        public required BlogPostBody BlogBody { get; set; }
        public required Author.Author Author { get; set; }

    }
}
