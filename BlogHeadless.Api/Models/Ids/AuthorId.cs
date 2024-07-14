using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Api.Models.Ids
{
    public  class AuthorId: AbstractId
    {
        public AuthorId() : base() { }
        public AuthorId(Guid guid) : base(guid) { }
    }
}
