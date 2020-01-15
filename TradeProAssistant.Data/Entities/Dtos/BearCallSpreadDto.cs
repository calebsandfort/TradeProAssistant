using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class BearCallSpreadDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Decimal Credit { get; set; }
		public Decimal Risk { get; set; }
		public Int32 Quantity { get; set; }

		public CallDto SellCall { get; set; }
		public CallDto BuyCall { get; set; }
		public SecurityDto Security { get; set; }

		#region Comparisons
		public static bool operator ==(BearCallSpreadDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is BearCallSpreadDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((BearCallSpreadDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(BearCallSpreadDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is BearCallSpreadDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((BearCallSpreadDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is BearCallSpreadDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((BearCallSpreadDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion

		#region ToString
		public override string ToString()
        {
            return Identifier.ToString();
        }
		#endregion
	}
}
