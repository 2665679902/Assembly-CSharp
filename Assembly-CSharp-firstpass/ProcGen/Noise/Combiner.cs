using System;
using LibNoiseDotNet.Graphics.Tools.Noise;
using LibNoiseDotNet.Graphics.Tools.Noise.Combiner;

namespace ProcGen.Noise
{
	// Token: 0x020004E8 RID: 1256
	public class Combiner : NoiseBase
	{
		// Token: 0x06003632 RID: 13874 RVA: 0x00076F4E File Offset: 0x0007514E
		public override Type GetObjectType()
		{
			return typeof(Combiner);
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x00076F5A File Offset: 0x0007515A
		// (set) Token: 0x06003634 RID: 13876 RVA: 0x00076F62 File Offset: 0x00075162
		public Combiner.CombinerType combineType { get; set; }

		// Token: 0x06003635 RID: 13877 RVA: 0x00076F6B File Offset: 0x0007516B
		public Combiner()
		{
			this.combineType = Combiner.CombinerType.Add;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x00076F7C File Offset: 0x0007517C
		public IModule3D CreateModule()
		{
			switch (this.combineType)
			{
			case Combiner.CombinerType.Add:
				return new Add();
			case Combiner.CombinerType.Max:
				return new Max();
			case Combiner.CombinerType.Min:
				return new Min();
			case Combiner.CombinerType.Multiply:
				return new Multiply();
			case Combiner.CombinerType.Power:
				return new Power();
			default:
				return null;
			}
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x00076FD0 File Offset: 0x000751D0
		public IModule3D CreateModule(IModule3D leftModule, IModule3D rightModule)
		{
			switch (this.combineType)
			{
			case Combiner.CombinerType.Add:
				return new Add(leftModule, rightModule);
			case Combiner.CombinerType.Max:
				return new Max(leftModule, rightModule);
			case Combiner.CombinerType.Min:
				return new Min(leftModule, rightModule);
			case Combiner.CombinerType.Multiply:
				return new Multiply(leftModule, rightModule);
			case Combiner.CombinerType.Power:
				return new Power(leftModule, rightModule);
			default:
				return null;
			}
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x0007702B File Offset: 0x0007522B
		public void SetSouces(IModule3D target, IModule3D leftModule, IModule3D rightModule)
		{
			(target as CombinerModule).LeftModule = leftModule;
			(target as CombinerModule).RightModule = rightModule;
		}

		// Token: 0x02000B11 RID: 2833
		public enum CombinerType
		{
			// Token: 0x040025F3 RID: 9715
			_UNSET_,
			// Token: 0x040025F4 RID: 9716
			Add,
			// Token: 0x040025F5 RID: 9717
			Max,
			// Token: 0x040025F6 RID: 9718
			Min,
			// Token: 0x040025F7 RID: 9719
			Multiply,
			// Token: 0x040025F8 RID: 9720
			Power
		}
	}
}
