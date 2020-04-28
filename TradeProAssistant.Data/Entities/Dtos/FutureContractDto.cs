using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class FutureContractDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public String Name { get; set; }
		public String Symbol { get; set; }
		public Decimal TickValue { get; set; }
		public Decimal TickSize { get; set; }


		#region Comparisons
		public static bool operator ==(FutureContractDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is FutureContractDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((FutureContractDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(FutureContractDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is FutureContractDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((FutureContractDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is FutureContractDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((FutureContractDto)obj).Identifier);
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
            return Name.ToString();
        }
		#endregion
	}
}
