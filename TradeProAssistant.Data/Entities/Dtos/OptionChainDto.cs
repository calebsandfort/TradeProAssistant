using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class OptionChainDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime Date { get; set; }

		public SecurityDto Security { get; set; }

		public List<OptionDateDto> Dates { get; set; }

		#region Comparisons
		public static bool operator ==(OptionChainDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is OptionChainDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((OptionChainDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(OptionChainDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is OptionChainDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((OptionChainDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionChainDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((OptionChainDto)obj).Identifier);
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
            return Date.ToString();
        }
		#endregion
	}
}
