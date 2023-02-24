using System;

namespace ProcGen
{
	// Token: 0x020004C9 RID: 1225
	[Serializable]
	public struct MinMax
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x00072B66 File Offset: 0x00070D66
		// (set) Token: 0x06003493 RID: 13459 RVA: 0x00072B6E File Offset: 0x00070D6E
		public float min { readonly get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x00072B77 File Offset: 0x00070D77
		// (set) Token: 0x06003495 RID: 13461 RVA: 0x00072B7F File Offset: 0x00070D7F
		public float max { readonly get; private set; }

		// Token: 0x06003496 RID: 13462 RVA: 0x00072B88 File Offset: 0x00070D88
		public MinMax(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x00072B98 File Offset: 0x00070D98
		public float GetRandomValueWithinRange(SeededRandom rnd)
		{
			return rnd.RandomRange(this.min, this.max);
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x00072BAC File Offset: 0x00070DAC
		public float GetAverage()
		{
			return (this.min + this.max) / 2f;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00072BC1 File Offset: 0x00070DC1
		public void Mod(MinMax mod)
		{
			this.min += mod.min;
			this.max += mod.max;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x00072BEB File Offset: 0x00070DEB
		public override string ToString()
		{
			return string.Format("min:{0} max:{1}", this.min, this.max);
		}
	}
}
