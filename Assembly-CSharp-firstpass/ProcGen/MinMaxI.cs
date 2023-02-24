using System;

namespace ProcGen
{
	// Token: 0x020004CA RID: 1226
	[Serializable]
	public struct MinMaxI
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x00072C0D File Offset: 0x00070E0D
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x00072C15 File Offset: 0x00070E15
		public int min { readonly get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x00072C1E File Offset: 0x00070E1E
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x00072C26 File Offset: 0x00070E26
		public int max { readonly get; private set; }

		// Token: 0x0600349F RID: 13471 RVA: 0x00072C2F File Offset: 0x00070E2F
		public MinMaxI(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x00072C3F File Offset: 0x00070E3F
		public int GetRandomValueWithinRange(SeededRandom rnd)
		{
			return rnd.RandomRange(this.min, this.max);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x00072C53 File Offset: 0x00070E53
		public int GetAverage()
		{
			return (this.min + this.max) / 2;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x00072C64 File Offset: 0x00070E64
		public void Mod(MinMaxI mod)
		{
			this.min += mod.min;
			this.max += mod.max;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x00072C8E File Offset: 0x00070E8E
		public override string ToString()
		{
			return string.Format("min:{0} max:{1}", this.min, this.max);
		}
	}
}
