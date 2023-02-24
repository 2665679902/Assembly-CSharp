using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004D2 RID: 1234
	[SerializationConfig(MemberSerialization.OptOut)]
	public class Path
	{
		// Token: 0x060034CF RID: 13519 RVA: 0x00072F32 File Offset: 0x00071132
		public Path()
		{
			this.pathElements = new List<Segment>();
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x00072F45 File Offset: 0x00071145
		public void AddSegment(Segment segment)
		{
			this.pathElements.Add(segment);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x00072F53 File Offset: 0x00071153
		public void AddSegment(Vector2 e0, Vector2 e1)
		{
			this.pathElements.Add(new Segment(e0, e1));
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x00072F68 File Offset: 0x00071168
		public void Stagger(SeededRandom rnd, float maxDistance = 10f, float staggerRange = 3f)
		{
			List<Segment> list = new List<Segment>();
			for (int i = 0; i < this.pathElements.Count; i++)
			{
				list.AddRange(this.pathElements[i].Stagger(rnd, maxDistance, staggerRange));
			}
			this.pathElements = list;
		}

		// Token: 0x0400128B RID: 4747
		public List<Segment> pathElements;
	}
}
