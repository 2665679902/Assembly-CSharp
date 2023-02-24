using System;
using LibNoiseDotNet.Graphics.Tools.Noise;
using LibNoiseDotNet.Graphics.Tools.Noise.Filter;

namespace ProcGen.Noise
{
	// Token: 0x020004EA RID: 1258
	public class Filter : NoiseBase
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x000770CF File Offset: 0x000752CF
		public override Type GetObjectType()
		{
			return typeof(Filter);
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000770DB File Offset: 0x000752DB
		// (set) Token: 0x06003640 RID: 13888 RVA: 0x000770E3 File Offset: 0x000752E3
		public Filter.NoiseFilter filter { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000770EC File Offset: 0x000752EC
		// (set) Token: 0x06003642 RID: 13890 RVA: 0x000770F4 File Offset: 0x000752F4
		public float frequency { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000770FD File Offset: 0x000752FD
		// (set) Token: 0x06003644 RID: 13892 RVA: 0x00077105 File Offset: 0x00075305
		public float lacunarity { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x0007710E File Offset: 0x0007530E
		// (set) Token: 0x06003646 RID: 13894 RVA: 0x00077116 File Offset: 0x00075316
		public int octaves { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x0007711F File Offset: 0x0007531F
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x00077127 File Offset: 0x00075327
		public float offset { get; set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x00077130 File Offset: 0x00075330
		// (set) Token: 0x0600364A RID: 13898 RVA: 0x00077138 File Offset: 0x00075338
		public float gain { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x00077141 File Offset: 0x00075341
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x00077149 File Offset: 0x00075349
		public float exponent { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x00077152 File Offset: 0x00075352
		// (set) Token: 0x0600364E RID: 13902 RVA: 0x0007715A File Offset: 0x0007535A
		public float scale { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x00077163 File Offset: 0x00075363
		// (set) Token: 0x06003650 RID: 13904 RVA: 0x0007716B File Offset: 0x0007536B
		public float bias { get; set; }

		// Token: 0x06003651 RID: 13905 RVA: 0x00077174 File Offset: 0x00075374
		public Filter()
		{
			this.filter = Filter.NoiseFilter.RidgedMultiFractal;
			this.frequency = 0.1f;
			this.lacunarity = 3f;
			this.octaves = 0;
			this.offset = 1f;
			this.gain = 1f;
			this.exponent = 0.9f;
			this.scale = 1f;
			this.bias = 0f;
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000771E4 File Offset: 0x000753E4
		public Filter(Filter src)
		{
			this.filter = src.filter;
			this.frequency = src.frequency;
			this.lacunarity = src.lacunarity;
			this.octaves = src.octaves;
			this.offset = src.offset;
			this.gain = src.gain;
			this.exponent = src.exponent;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x0007724C File Offset: 0x0007544C
		public IModule3D CreateModule()
		{
			FilterModule filterModule = null;
			switch (this.filter)
			{
			case Filter.NoiseFilter.Pipe:
				filterModule = new Pipe();
				break;
			case Filter.NoiseFilter.SumFractal:
				filterModule = new SumFractal();
				break;
			case Filter.NoiseFilter.SinFractal:
				filterModule = new SinFractal();
				break;
			case Filter.NoiseFilter.Billow:
				filterModule = new Billow();
				break;
			case Filter.NoiseFilter.MultiFractal:
				filterModule = new MultiFractal();
				break;
			case Filter.NoiseFilter.HeterogeneousMultiFractal:
				filterModule = new HeterogeneousMultiFractal();
				break;
			case Filter.NoiseFilter.HybridMultiFractal:
				filterModule = new HybridMultiFractal();
				break;
			case Filter.NoiseFilter.RidgedMultiFractal:
				filterModule = new RidgedMultiFractal();
				break;
			case Filter.NoiseFilter.Voronoi:
				filterModule = new Voronoi();
				break;
			}
			if (filterModule != null)
			{
				filterModule.Frequency = this.frequency;
				filterModule.Lacunarity = this.lacunarity;
				filterModule.OctaveCount = (float)this.octaves;
				filterModule.Offset = this.offset;
				filterModule.Gain = this.gain;
				filterModule.SpectralExponent = this.exponent;
				if (this.filter == Filter.NoiseFilter.Billow)
				{
					Billow billow = (Billow)filterModule;
					billow.Scale = this.scale;
					billow.Bias = this.bias;
				}
			}
			return (IModule3D)filterModule;
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x0007734E File Offset: 0x0007554E
		public void SetSouces(IModule3D target, IModule3D sourceModule)
		{
			(target as FilterModule).Primitive3D = sourceModule;
		}

		// Token: 0x02000B13 RID: 2835
		public enum NoiseFilter
		{
			// Token: 0x040025FC RID: 9724
			_UNSET_,
			// Token: 0x040025FD RID: 9725
			Pipe,
			// Token: 0x040025FE RID: 9726
			SumFractal,
			// Token: 0x040025FF RID: 9727
			SinFractal,
			// Token: 0x04002600 RID: 9728
			Billow,
			// Token: 0x04002601 RID: 9729
			MultiFractal,
			// Token: 0x04002602 RID: 9730
			HeterogeneousMultiFractal,
			// Token: 0x04002603 RID: 9731
			HybridMultiFractal,
			// Token: 0x04002604 RID: 9732
			RidgedMultiFractal,
			// Token: 0x04002605 RID: 9733
			Voronoi
		}
	}
}
