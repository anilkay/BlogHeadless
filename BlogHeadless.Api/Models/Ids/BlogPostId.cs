using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UUIDNext;

namespace BlogHeadless.Api.Models.Ids
{
    public  class BlogPostId: AbstractId
    {
        public BlogPostId():base() { }
        public BlogPostId(Guid guid):base(guid) { }
    }
}
