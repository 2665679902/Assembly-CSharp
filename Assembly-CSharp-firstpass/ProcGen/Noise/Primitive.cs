using System;
using LibNoiseDotNet.Graphics.Tools.Noise;
using LibNoiseDotNet.Graphics.Tools.Noise.Primitive;

namespace ProcGen.Noise
{
	// Token: 0x020004ED RID: 1261
	public class Primitive : NoiseBase
	{
		// Token: 0x0600366E RID: 13934 RVA: 0x000776D8 File Offset: 0x000758D8
		public override Type GetObjectType()
		{
			return typeof(Primitive);
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000776E4 File Offset: 0x000758E4
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x000776EC File Offset: 0x000758EC
		public NoisePrimitive primative { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000776F5 File Offset: 0x000758F5
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x000776FD File Offset: 0x000758FD
		public NoiseQuality quality { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x00077706 File Offset: 0x00075906
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x0007770E File Offset: 0x0007590E
		public int seed { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x00077717 File Offset: 0x00075917
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x0007771F File Offset: 0x0007591F
		public float offset { get; set; }

		// Token: 0x06003677 RID: 13943 RVA: 0x00077728 File Offset: 0x00075928
		public Primitive()
		{
			this.primative = NoisePrimitive.ImprovedPerlin;
			this.quality = NoiseQuality.Best;
			this.seed = 0;
			this.offset = 1f;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x00077750 File Offset: 0x00075950
		public Primitive(Primitive src)
		{
			this.primative = src.primative;
			this.quality = src.quality;
			this.seed = src.seed;
			this.offset = src.offset;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x00077788 File Offset: 0x00075988
		public IModule3D CreateModule(int globalSeed)
		{
			PrimitiveModule primitiveModule = null;
			switch (this.primative)
			{
			case NoisePrimitive.Constant:
				primitiveModule = new Constant(this.offset);
				break;
			case NoisePrimitive.Spheres:
				primitiveModule = new Spheres(this.offset);
				break;
			case NoisePrimitive.Cylinders:
				primitiveModule = new Cylinders(this.offset);
				break;
			case NoisePrimitive.BevinsValue:
				primitiveModule = new BevinsValue();
				break;
			case NoisePrimitive.BevinsGradient:
				primitiveModule = new BevinsGradient();
				break;
			case NoisePrimitive.ImprovedPerlin:
				primitiveModule = new ImprovedPerlin();
				break;
			case NoisePrimitive.SimplexPerlin:
				primitiveModule = new SimplexPerlin();
				break;
			}
			primitiveModule.Quality = this.quality;
			primitiveModule.Seed = globalSeed + this.seed;
			return (IModule3D)primitiveModule;
		}
	}
}
