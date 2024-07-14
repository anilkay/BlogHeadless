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

            try
            {
                Guid controlId = (Guid)obj;

                return Value.Equals(controlId);
            }
            catch
            {
                var abstractId=(AbstractId)obj;
                return Value.Equals(abstractId.Value);
            }
  
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
