using System;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	public class SampleDescriber
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x0007180F File Offset: 0x0006FA0F
		// (set) Token: 0x060033FF RID: 13311 RVA: 0x00071817 File Offset: 0x0006FA17
		public string name { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x00071820 File Offset: 0x0006FA20
		// (set) Token: 0x06003401 RID: 13313 RVA: 0x00071828 File Offset: 0x0006FA28
		[StringEnumConverter]
		public SampleDescriber.PointSelectionMethod selectMethod { get; protected set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x00071831 File Offset: 0x0006FA31
		// (set) Token: 0x06003403 RID: 13315 RVA: 0x00071839 File Offset: 0x0006FA39
		public MinMax density { get; protected set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x00071842 File Offset: 0x0006FA42
		// (set) Token: 0x06003405 RID: 13317 RVA: 0x0007184A File Offset: 0x0006FA4A
		public float avoidRadius { get; protected set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x00071853 File Offset: 0x0006FA53
		// (set) Token: 0x06003407 RID: 13319 RVA: 0x0007185B File Offset: 0x0006FA5B
		[StringEnumConverter]
		public PointGenerator.SampleBehaviour sampleBehaviour { get; protected set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x00071864 File Offset: 0x0006FA64
		// (set) Token: 0x06003409 RID: 13321 RVA: 0x0007186C File Offset: 0x0006FA6C
		public bool doAvoidPoints { get; protected set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x00071875 File Offset: 0x0006FA75
		// (set) Token: 0x0600340B RID: 13323 RVA: 0x0007187D File Offset: 0x0006FA7D
		public bool dontRelaxChildren { get; protected set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x00071886 File Offset: 0x0006FA86
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x0007188E File Offset: 0x0006FA8E
		public MinMax blobSize { get; protected set; }

		// Token: 0x0600340E RID: 13326 RVA: 0x00071897 File Offset: 0x0006FA97
		public SampleDescriber()
		{
			this.doAvoidPoints = true;
			this.dontRelaxChildren = false;
		}

		// Token: 0x02000AF2 RID: 2802
		public enum PointSelectionMethod
		{
			// Token: 0x04002551 RID: 9553
			RandomPoints,
			// Token: 0x04002552 RID: 9554
			Centroid
		}

		// Token: 0x02000AF3 RID: 2803
		[Serializable]
		public class Override
		{
			// Token: 0x060057E4 RID: 22500 RVA: 0x000A3F4E File Offset: 0x000A214E
			public Override()
			{
			}

			// Token: 0x060057E5 RID: 22501 RVA: 0x000A3F56 File Offset: 0x000A2156
			public Override(float? massOverride, float? massMultiplier, float? temperatureOverride, float? temperatureMultiplier, string diseaseOverride, int? diseaseAmountOverride)
			{
				this.massOverride = massOverride;
				this.massMultiplier = massMultiplier;
				this.temperatureOverride = temperatureOverride;
				this.temperatureMultiplier = temperatureMultiplier;
				this.diseaseOverride = diseaseOverride;
				this.diseaseAmountOverride = diseaseAmountOverride;
			}

			// Token: 0x17000ED3 RID: 3795
			// (get) Token: 0x060057E6 RID: 22502 RVA: 0x000A3F8B File Offset: 0x000A218B
			// (set) Token: 0x060057E7 RID: 22503 RVA: 0x000A3F93 File Offset: 0x000A2193
			public float? massOverride { get; protected set; }

			// Token: 0x17000ED4 RID: 3796
			// (get) Token: 0x060057E8 RID: 22504 RVA: 0x000A3F9C File Offset: 0x000A219C
			// (set) Token: 0x060057E9 RID: 22505 RVA: 0x000A3FA4 File Offset: 0x000A21A4
			public float? massMultiplier { get; protected set; }

			// Token: 0x17000ED5 RID: 3797
			// (get) Token: 0x060057EA RID: 22506 RVA: 0x000A3FAD File Offset: 0x000A21AD
			// (set) Token: 0x060057EB RID: 22507 RVA: 0x000A3FB5 File Offset: 0x000A21B5
			public float? temperatureOverride { get; protected set; }

			// Token: 0x17000ED6 RID: 3798
			// (get) Token: 0x060057EC RID: 22508 RVA: 0x000A3FBE File Offset: 0x000A21BE
			// (set) Token: 0x060057ED RID: 22509 RVA: 0x000A3FC6 File Offset: 0x000A21C6
			public float? temperatureMultiplier { get; protected set; }

			// Token: 0x17000ED7 RID: 3799
			// (get) Token: 0x060057EE RID: 22510 RVA: 0x000A3FCF File Offset: 0x000A21CF
			// (set) Token: 0x060057EF RID: 22511 RVA: 0x000A3FD7 File Offset: 0x000A21D7
			public string diseaseOverride { get; protected set; }

			// Token: 0x17000ED8 RID: 3800
			// (get) Token: 0x060057F0 RID: 22512 RVA: 0x000A3FE0 File Offset: 0x000A21E0
			// (set) Token: 0x060057F1 RID: 22513 RVA: 0x000A3FE8 File Offset: 0x000A21E8
			public int? diseaseAmountOverride { get; protected set; }

			// Token: 0x060057F2 RID: 22514 RVA: 0x000A3FF4 File Offset: 0x000A21F4
			public void ModMultiplyMass(float mult)
			{
				if (this.massMultiplier == null)
				{
					this.massMultiplier = new float?(mult);
					return;
				}
				this.massMultiplier *= mult;
			}
		}
	}
}
