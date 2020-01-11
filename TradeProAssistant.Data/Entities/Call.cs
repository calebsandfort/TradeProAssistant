using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class Call
	{
		[NotMapped]
		public static CallPropertyNames PropertyNames = new CallPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal Bid { get; set; }

		public Decimal Ask { get; set; }

		#region Constructor
		public  Call()
		{
				}
		#endregion

		#region Comparisons
		public static bool operator ==(Call entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is Call && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((Call)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(Call entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is Call && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((Call)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is Call && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((Call)obj).Identifier);
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
