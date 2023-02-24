using System;
using System.Collections.Generic;

// Token: 0x020007B1 RID: 1969
[Obsolete("No longer used. Use IGameObjectEffectDescriptor instead", false)]
public interface IEffectDescriptor
{
	// Token: 0x060037C0 RID: 14272
	List<Descriptor> GetDescriptors(BuildingDef def);
}
