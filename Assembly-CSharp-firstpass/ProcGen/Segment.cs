using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004D1 RID: 1233
	[SerializationConfig(MemberSerialization.OptOut)]
	public struct Segment
	{
		// Token: 0x060034CD RID: 13517 RVA: 0x00072E5C File Offset: 0x0007105C
		public Segment(Vector2 e0, Vector2 e1)
		{
			this.e0 = e0;
			this.e1 = e1;
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x00072E6C File Offset: 0x0007106C
		public List<Segment> Stagger(SeededRandom rnd, float maxDistance = 10f, float staggerRange = 3f)
		{
			List<Segment> list = new List<Segment>();
			Vector2 vector = this.e1 - this.e0;
			Vector2 vector2 = this.e0;
			Vector2 vector3 = this.e1;
			float num = vector.magnitude / maxDistance;
			Vector2 normalized = new Vector2(-vector.y, vector.x).normalized;
			int num2 = 0;
			while ((float)num2 < num)
			{
				vector3 = this.e0 + vector * (1f / num) * (float)num2 + normalized * rnd.RandomRange(-staggerRange, staggerRange);
				list.Add(new Segment(vector2, vector3));
				vector2 = vector3;
				num2++;
			}
			list.Add(new Segment(vector3, this.e1));
			return list;
		}

		// Token: 0x04001289 RID: 4745
		public Vector2 e0;

		// Token: 0x0400128A RID: 4746
		public Vector2 e1;
	}
}
