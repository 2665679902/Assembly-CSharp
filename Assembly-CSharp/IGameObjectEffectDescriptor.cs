using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007B0 RID: 1968
public interface IGameObjectEffectDescriptor
{
	// Token: 0x060037BF RID: 14271
	List<Descriptor> GetDescriptors(GameObject go);
}
