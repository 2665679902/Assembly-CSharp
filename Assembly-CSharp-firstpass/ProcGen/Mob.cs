using System;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class Mob : SampleDescriber
	{
		// Token: 0x06003452 RID: 13394 RVA: 0x00071DFD File Offset: 0x0006FFFD
		public Mob()
		{
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x00071E05 File Offset: 0x00070005
		public Mob(Mob.Location location)
		{
			this.location = location;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x00071E14 File Offset: 0x00070014
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x00071E1C File Offset: 0x0007001C
		public string prefabName { get; private set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x00071E25 File Offset: 0x00070025
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x00071E2D File Offset: 0x0007002D
		public int width { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x00071E36 File Offset: 0x00070036
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x00071E3E File Offset: 0x0007003E
		public int height { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x00071E47 File Offset: 0x00070047
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x00071E4F File Offset: 0x0007004F
		[StringEnumConverter]
		public Mob.Location location { get; private set; }

		// Token: 0x02000AF7 RID: 2807
		public enum Location
		{
			// Token: 0x04002581 RID: 9601
			Floor,
			// Token: 0x04002582 RID: 9602
			Ceiling,
			// Token: 0x04002583 RID: 9603
			Air,
			// Token: 0x04002584 RID: 9604
			BackWall,
			// Token: 0x04002585 RID: 9605
			NearWater,
			// Token: 0x04002586 RID: 9606
			NearLiquid,
			// Token: 0x04002587 RID: 9607
			Solid,
			// Token: 0x04002588 RID: 9608
			Water,
			// Token: 0x04002589 RID: 9609
			ShallowLiquid,
			// Token: 0x0400258A RID: 9610
			Surface,
			// Token: 0x0400258B RID: 9611
			LiquidFloor,
			// Token: 0x0400258C RID: 9612
			AnyFloor
		}
	}
}
