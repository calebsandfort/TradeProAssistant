﻿using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class PutDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Decimal Bid { get; set; }
		public Decimal Ask { get; set; }

		#region Comparisons
		public static bool operator ==(PutDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is PutDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((PutDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(PutDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is PutDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((PutDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is PutDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((PutDto)obj).Identifier);
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
