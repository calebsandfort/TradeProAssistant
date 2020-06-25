using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class TradingSettingsDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime MonthStart { get; set; }
		public int Weeks { get; set; }

		public RiskParametersDto RiskParameters { get; set; }

		#region Comparisons
		public static bool operator ==(TradingSettingsDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is TradingSettingsDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((TradingSettingsDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(TradingSettingsDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is TradingSettingsDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((TradingSettingsDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is TradingSettingsDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((TradingSettingsDto)obj).Identifier);
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
