using BlogHeadless.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHeadless.Data.Models.Subscriber
{
    public  class Subscriber
    {
        public Ulid Id { get; set; }
        public  Email Email { get; set; }
        public  SubsriptionSource SubsriptionSource { get; set; }

        public Subscriber()
        {
            Id = Ulid.NewUlid();
        }

        public Subscriber(string email,string subsriptionSource):this()
        {
            Email=new Email(email);

            SubsriptionSource possibleSource;

            bool isParsed = Enum.TryParse(subsriptionSource, out possibleSource);

            if (!isParsed)
            {
                throw new ArgumentException();
            }

            SubsriptionSource = possibleSource;

        }

    }
    public enum SubsriptionSource {
        Blog,
        Email,
        Other
    }
}
