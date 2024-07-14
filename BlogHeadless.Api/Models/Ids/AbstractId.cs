using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UUIDNext;

namespace BlogHeadless.Api.Models.Ids
{
    public abstract class AbstractId
    {
        public Guid Value { get; private set; }

        public AbstractId()
        {
            Value = Uuid.NewDatabaseFriendly(Database.SqlServer);
        }

        public AbstractId(Guid value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            Guid controlId = (Guid)obj;

            return Value.Equals(controlId);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
