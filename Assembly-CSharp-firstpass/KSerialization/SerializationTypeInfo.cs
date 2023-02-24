using System;

namespace KSerialization
{
	// Token: 0x020004FE RID: 1278
	public enum SerializationTypeInfo : byte
	{
		// Token: 0x040013B8 RID: 5048
		UserDefined,
		// Token: 0x040013B9 RID: 5049
		SByte,
		// Token: 0x040013BA RID: 5050
		Byte,
		// Token: 0x040013BB RID: 5051
		Boolean,
		// Token: 0x040013BC RID: 5052
		Int16,
		// Token: 0x040013BD RID: 5053
		UInt16,
		// Token: 0x040013BE RID: 5054
		Int32,
		// Token: 0x040013BF RID: 5055
		UInt32,
		// Token: 0x040013C0 RID: 5056
		Int64,
		// Token: 0x040013C1 RID: 5057
		UInt64,
		// Token: 0x040013C2 RID: 5058
		Single,
		// Token: 0x040013C3 RID: 5059
		Double,
		// Token: 0x040013C4 RID: 5060
		String,
		// Token: 0x040013C5 RID: 5061
		Enumeration,
		// Token: 0x040013C6 RID: 5062
		Vector2I,
		// Token: 0x040013C7 RID: 5063
		Vector2,
		// Token: 0x040013C8 RID: 5064
		Vector3,
		// Token: 0x040013C9 RID: 5065
		Array,
		// Token: 0x040013CA RID: 5066
		Pair,
		// Token: 0x040013CB RID: 5067
		Dictionary,
		// Token: 0x040013CC RID: 5068
		List,
		// Token: 0x040013CD RID: 5069
		HashSet,
		// Token: 0x040013CE RID: 5070
		Queue,
		// Token: 0x040013CF RID: 5071
		Colour,
		// Token: 0x040013D0 RID: 5072
		IS_GENERIC_TYPE = 128,
		// Token: 0x040013D1 RID: 5073
		IS_VALUE_TYPE = 64,
		// Token: 0x040013D2 RID: 5074
		VALUE_MASK = 63
	}
}
