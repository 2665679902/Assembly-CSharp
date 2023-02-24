using System;
using KSerialization;
using UnityEngine;

// Token: 0x020009B3 RID: 2483
[SerializationConfig(MemberSerialization.OptOut)]
[AddComponentMenu("KMonoBehaviour/scripts/UnstableGround")]
public class UnstableGround : KMonoBehaviour
{
	// Token: 0x0400306E RID: 12398
	public SimHashes element;

	// Token: 0x0400306F RID: 12399
	public float mass;

	// Token: 0x04003070 RID: 12400
	public float temperature;

	// Token: 0x04003071 RID: 12401
	public byte diseaseIdx;

	// Token: 0x04003072 RID: 12402
	public int diseaseCount;
}
