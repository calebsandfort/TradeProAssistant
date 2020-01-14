/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public partial class DayCandlestick
	{
		#region Custom Properties

		#endregion

		#region Custom Methods

		#endregion

		#region Comparisons
		public static bool operator ==(DayCandlestick entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is DayCandlestick && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((DayCandlestick)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(DayCandlestick entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is DayCandlestick && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((DayCandlestick)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is DayCandlestick && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((DayCandlestick)obj).Identifier);
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
*/
