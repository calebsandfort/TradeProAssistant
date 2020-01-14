using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class CallDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Decimal Bid { get; set; }
		public Decimal Ask { get; set; }

		#region Comparisons
		public static bool operator ==(CallDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is CallDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((CallDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(CallDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is CallDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((CallDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is CallDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((CallDto)obj).Identifier);
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
