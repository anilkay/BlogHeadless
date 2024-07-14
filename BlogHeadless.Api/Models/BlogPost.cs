using BlogHeadless.Api.Models.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models
{
    public class BlogPost
    {
        public required BlogPostId Id { get; set; }
        public required BlogHeader BlogHeader { get; set; }
        public required BlogBody BlogBody { get; set; }
        public required Author Author { get; set; }

    }
}
