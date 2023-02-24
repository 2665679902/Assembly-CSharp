using System;
using LibNoiseDotNet.Graphics.Tools.Noise;
using LibNoiseDotNet.Graphics.Tools.Noise.Primitive;
using LibNoiseDotNet.Graphics.Tools.Noise.Tranformer;

namespace ProcGen.Noise
{
	// Token: 0x020004EF RID: 1263
	public class Transformer : NoiseBase
	{
		// Token: 0x06003687 RID: 13959 RVA: 0x0007796A File Offset: 0x00075B6A
		public override Type GetObjectType()
		{
			return typeof(Transformer);
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x00077976 File Offset: 0x00075B76
		// (set) Token: 0x06003689 RID: 13961 RVA: 0x0007797E File Offset: 0x00075B7E
		public Transformer.TransformerType transformerType { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x00077987 File Offset: 0x00075B87
		// (set) Token: 0x0600368B RID: 13963 RVA: 0x0007798F File Offset: 0x00075B8F
		public float power { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600368C RID: 13964 RVA: 0x00077998 File Offset: 0x00075B98
		// (set) Token: 0x0600368D RID: 13965 RVA: 0x000779A0 File Offset: 0x00075BA0
		public Vector2f vector { get; set; }

		// Token: 0x0600368E RID: 13966 RVA: 0x000779A9 File Offset: 0x00075BA9
		public Transformer()
		{
			this.transformerType = Transformer.TransformerType.Displace;
			this.power = 1f;
			this.vector = new Vector2f(0, 0);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000779D0 File Offset: 0x00075BD0
		public IModule3D CreateModule()
		{
			if (this.transformerType == Transformer.TransformerType.Turbulence)
			{
				return new Turbulence
				{
					Power = this.power
				};
			}
			if (this.transformerType == Transformer.TransformerType.RotatePoint)
			{
				return new RotatePoint
				{
					XAngle = this.vector.x,
					YAngle = this.vector.y,
					ZAngle = 0f
				};
			}
			if (this.transformerType == Transformer.TransformerType.TranslatePoint)
			{
				return new TranslatePoint
				{
					XTranslate = this.vector.x,
					ZTranslate = this.vector.y
				};
			}
			return new Displace();
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x00077A6C File Offset: 0x00075C6C
		public IModule3D CreateModule(IModule3D sourceModule, IModule3D xModule, IModule3D yModule, IModule3D zModule)
		{
			if (this.transformerType == Transformer.TransformerType.Turbulence)
			{
				return new Turbulence(sourceModule, xModule, yModule, zModule, this.power);
			}
			if (this.transformerType == Transformer.TransformerType.RotatePoint)
			{
				return new RotatePoint(sourceModule, this.vector.x, this.vector.y, 0f);
			}
			if (this.transformerType == Transformer.TransformerType.TranslatePoint)
			{
				return new TranslatePoint(sourceModule, this.vector.x, 0f, this.vector.y);
			}
			return new Displace(sourceModule, xModule, yModule, zModule);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x00077AF4 File Offset: 0x00075CF4
		public void SetSouces(IModule3D target, IModule3D sourceModule, IModule3D xModule, IModule3D yModule, IModule3D zModule)
		{
			if (this.transformerType == Transformer.TransformerType.Turbulence)
			{
				Turbulence turbulence = target as Turbulence;
				turbulence.SourceModule = sourceModule;
				IModule module;
				if (xModule == null)
				{
					IModule3D module3D = new Constant(0f);
					module = module3D;
				}
				else
				{
					module = xModule;
				}
				turbulence.XDistortModule = module;
				turbulence.YDistortModule = new Constant(0f);
				IModule module2;
				if (yModule == null)
				{
					IModule3D module3D = new Constant(0f);
					module2 = module3D;
				}
				else
				{
					module2 = yModule;
				}
				turbulence.ZDistortModule = module2;
				return;
			}
			if (this.transformerType == Transformer.TransformerType.RotatePoint)
			{
				(target as RotatePoint).SourceModule = sourceModule;
				return;
			}
			if (this.transformerType == Transformer.TransformerType.TranslatePoint)
			{
				(target as TranslatePoint).SourceModule = sourceModule;
				return;
			}
			Displace displace = target as Displace;
			displace.SourceModule = sourceModule;
			IModule module3;
			if (xModule == null)
			{
				IModule3D module3D = new Constant(0f);
				module3 = module3D;
			}
			else
			{
				module3 = xModule;
			}
			displace.XDisplaceModule = module3;
			displace.YDisplaceModule = new Constant(0f);
			IModule module4;
			if (yModule == null)
			{
				IModule3D module3D = new Constant(0f);
				module4 = module3D;
			}
			else
			{
				module4 = yModule;
			}
			displace.ZDisplaceModule = module4;
		}

		// Token: 0x02000B16 RID: 2838
		public enum TransformerType
		{
			// Token: 0x04002615 RID: 9749
			_UNSET_,
			// Token: 0x04002616 RID: 9750
			Displace,
			// Token: 0x04002617 RID: 9751
			Turbulence,
			// Token: 0x04002618 RID: 9752
			RotatePoint,
			// Token: 0x04002619 RID: 9753
			TranslatePoint
		}
	}
}
