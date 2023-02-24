using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000677 RID: 1655
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/CO2")]
public class CO2 : KMonoBehaviour
{
	// Token: 0x06002CA6 RID: 11430 RVA: 0x000EA136 File Offset: 0x000E8336
	public void StartLoop()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.Play("exhale_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Play("exhale_loop", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06002CA7 RID: 11431 RVA: 0x000EA173 File Offset: 0x000E8373
	public void TriggerDestroy()
	{
		base.GetComponent<KBatchedAnimController>().Play("exhale_pst", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001A8F RID: 6799
	[Serialize]
	[NonSerialized]
	public Vector3 velocity = Vector3.zero;

	// Token: 0x04001A90 RID: 6800
	[Serialize]
	[NonSerialized]
	public float mass;

	// Token: 0x04001A91 RID: 6801
	[Serialize]
	[NonSerialized]
	public float temperature;

	// Token: 0x04001A92 RID: 6802
	[Serialize]
	[NonSerialized]
	public float lifetimeRemaining;
}
