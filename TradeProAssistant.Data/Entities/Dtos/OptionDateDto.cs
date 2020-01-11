using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class OptionDateDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime ExpiryDate { get; set; }

		public List<OptionStrikeDto> Strikes { get; set; }

		#region Comparisons
		public static bool operator ==(OptionDateDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is OptionDateDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((OptionDateDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(OptionDateDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is OptionDateDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((OptionDateDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionDateDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((OptionDateDto)obj).Identifier);
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
