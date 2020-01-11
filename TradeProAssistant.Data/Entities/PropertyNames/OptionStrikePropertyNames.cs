using System;

namespace Entities
{
	public class OptionStrikePropertyNames : PropertyNamesBase
	{
		public OptionStrikePropertyNames() : base(String.Empty) {}
		public OptionStrikePropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String StrikePrice { get { return ResolvePropertyName("StrikePrice"); } }
		public String OptionDateIdentifier { get { return ResolvePropertyName("OptionDateIdentifier"); } }
		public String OptionDateInclude { get { return ResolvePropertyName("OptionDate"); } }
		public OptionDatePropertyNames OptionDate { get { return new OptionDatePropertyNames(ResolvePropertyName("OptionDate")); } }
		public String CallIdentifier { get { return ResolvePropertyName("CallIdentifier"); } }
		public String CallInclude { get { return ResolvePropertyName("Call"); } }
		public CallPropertyNames Call { get { return new CallPropertyNames(ResolvePropertyName("Call")); } }
		public String PutIdentifier { get { return ResolvePropertyName("PutIdentifier"); } }
		public String PutInclude { get { return ResolvePropertyName("Put"); } }
		public PutPropertyNames Put { get { return new PutPropertyNames(ResolvePropertyName("Put")); } }
	}
}
