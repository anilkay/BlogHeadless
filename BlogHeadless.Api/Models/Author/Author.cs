using BlogHeadless.Api.Models;
using BlogHeadless.Api.Models.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Data.Models.Author
{
    public class Author
    {
        public AuthorId Id { get; set; }
        public AuthorName Name { get; set; }
        public Email Email { get; set; }


    }
}
