using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000631 RID: 1585
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Refinery")]
public class Refinery : KMonoBehaviour
{
	// Token: 0x060029D5 RID: 10709 RVA: 0x000DCBF8 File Offset: 0x000DADF8
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x020012BE RID: 4798
	[Serializable]
	public struct OrderSaveData
	{
		// Token: 0x06007B64 RID: 31588 RVA: 0x002CBC84 File Offset: 0x002C9E84
		public OrderSaveData(string id, bool infinite)
		{
			this.id = id;
			this.infinite = infinite;
		}

		// Token: 0x04005E96 RID: 24214
		public string id;

		// Token: 0x04005E97 RID: 24215
		public bool infinite;
	}
}
