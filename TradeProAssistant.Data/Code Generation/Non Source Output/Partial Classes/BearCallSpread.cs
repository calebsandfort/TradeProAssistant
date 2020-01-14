/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public partial class BearCallSpread
	{
		#region Custom Properties

		#endregion

		#region Custom Methods

		#endregion

		#region Comparisons
		public static bool operator ==(BearCallSpread entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is BearCallSpread && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((BearCallSpread)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(BearCallSpread entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is BearCallSpread && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((BearCallSpread)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is BearCallSpread && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((BearCallSpread)obj).Identifier);
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
