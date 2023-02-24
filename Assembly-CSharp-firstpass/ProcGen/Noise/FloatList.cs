using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProcGen.Noise
{
	// Token: 0x020004EB RID: 1259
	[Serializable]
	public class FloatList : NoiseBase
	{
		// Token: 0x06003655 RID: 13909 RVA: 0x0007735C File Offset: 0x0007555C
		public override Type GetObjectType()
		{
			return typeof(FloatList);
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06003656 RID: 13910 RVA: 0x00077368 File Offset: 0x00075568
		// (set) Token: 0x06003657 RID: 13911 RVA: 0x00077370 File Offset: 0x00075570
		[SerializeField]
		public List<float> points { get; set; }

		// Token: 0x06003658 RID: 13912 RVA: 0x00077379 File Offset: 0x00075579
		public FloatList()
		{
			this.points = new List<float>();
			this.points.Add(0f);
			this.points.Add(1f);
		}
	}
}
