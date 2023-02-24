using System;
using System.IO;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public interface ISerializableComponentManager : IComponentManager
{
	// Token: 0x060006C5 RID: 1733
	void Serialize(GameObject go, BinaryWriter writer);

	// Token: 0x060006C6 RID: 1734
	void Deserialize(GameObject go, IReader reader);
}
