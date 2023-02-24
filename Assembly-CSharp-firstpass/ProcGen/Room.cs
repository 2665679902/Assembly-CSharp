using System;
using System.Collections.Generic;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004C0 RID: 1216
	public class Room : SampleDescriber
	{
		// Token: 0x06003449 RID: 13385 RVA: 0x00071C9F File Offset: 0x0006FE9F
		public Room()
		{
			this.mobs = new List<WeightedMob>();
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x00071CB2 File Offset: 0x0006FEB2
		// (set) Token: 0x0600344B RID: 13387 RVA: 0x00071CBA File Offset: 0x0006FEBA
		[StringEnumConverter]
		public Room.Shape shape { get; private set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x00071CC3 File Offset: 0x0006FEC3
		// (set) Token: 0x0600344D RID: 13389 RVA: 0x00071CCB File Offset: 0x0006FECB
		[StringEnumConverter]
		public Room.Selection mobselection { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x00071CD4 File Offset: 0x0006FED4
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x00071CDC File Offset: 0x0006FEDC
		public List<WeightedMob> mobs { get; private set; }

		// Token: 0x06003450 RID: 13392 RVA: 0x00071CE8 File Offset: 0x0006FEE8
		public void ResetMobs(SeededRandom rnd)
		{
			if (this.mobselection == Room.Selection.WeightedBucket)
			{
				if (this.bucket == null)
				{
					this.bucket = new List<WeightedMob>();
					for (int i = 0; i < this.mobs.Count; i++)
					{
						int num = 0;
						while ((float)num < this.mobs[i].weight)
						{
							this.bucket.Add(new WeightedMob(this.mobs[i].tag, 1f));
							num++;
						}
					}
				}
				this.bucket.ShuffleSeeded(rnd.RandomSource());
				this.mobIter = this.bucket.GetEnumerator();
				return;
			}
			this.mobIter = this.mobs.GetEnumerator();
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00071DA4 File Offset: 0x0006FFA4
		public WeightedMob GetNextMob(SeededRandom rnd)
		{
			WeightedMob weightedMob = null;
			switch (this.mobselection)
			{
			case Room.Selection.OneOfEach:
			case Room.Selection.WeightedBucket:
				if (this.mobIter.MoveNext())
				{
					weightedMob = this.mobIter.Current;
				}
				break;
			case Room.Selection.Weighted:
				weightedMob = WeightedRandom.Choose<WeightedMob>(this.mobs, rnd);
				break;
			}
			return weightedMob;
		}

		// Token: 0x0400125F RID: 4703
		private List<WeightedMob>.Enumerator mobIter;

		// Token: 0x04001260 RID: 4704
		private List<WeightedMob> bucket;

		// Token: 0x02000AF5 RID: 2805
		public enum Shape
		{
			// Token: 0x0400256D RID: 9581
			Circle,
			// Token: 0x0400256E RID: 9582
			Oval,
			// Token: 0x0400256F RID: 9583
			Blob,
			// Token: 0x04002570 RID: 9584
			Line,
			// Token: 0x04002571 RID: 9585
			Square,
			// Token: 0x04002572 RID: 9586
			TallThin,
			// Token: 0x04002573 RID: 9587
			ShortWide,
			// Token: 0x04002574 RID: 9588
			Template,
			// Token: 0x04002575 RID: 9589
			PhysicalLayout,
			// Token: 0x04002576 RID: 9590
			Splat
		}

		// Token: 0x02000AF6 RID: 2806
		public enum Selection
		{
			// Token: 0x04002578 RID: 9592
			None,
			// Token: 0x04002579 RID: 9593
			OneOfEach,
			// Token: 0x0400257A RID: 9594
			NOfEach,
			// Token: 0x0400257B RID: 9595
			Weighted,
			// Token: 0x0400257C RID: 9596
			WeightedBucket,
			// Token: 0x0400257D RID: 9597
			WeightedResample,
			// Token: 0x0400257E RID: 9598
			PickOneWeighted,
			// Token: 0x0400257F RID: 9599
			HorizontalSlice
		}
	}
}
