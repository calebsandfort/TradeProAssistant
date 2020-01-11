using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class OptionStrikeDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Decimal StrikePrice { get; set; }


		#region Comparisons
		public static bool operator ==(OptionStrikeDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is OptionStrikeDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((OptionStrikeDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(OptionStrikeDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is OptionStrikeDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((OptionStrikeDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionStrikeDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((OptionStrikeDto)obj).Identifier);
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
	}
}
