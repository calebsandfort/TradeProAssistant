/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public partial class BullPutSpread
	{
		#region Custom Properties

		#endregion

		#region Custom Methods

		#endregion

		#region Comparisons
		public static bool operator ==(BullPutSpread entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is BullPutSpread && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((BullPutSpread)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(BullPutSpread entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is BullPutSpread && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((BullPutSpread)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is BullPutSpread && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((BullPutSpread)obj).Identifier);
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
