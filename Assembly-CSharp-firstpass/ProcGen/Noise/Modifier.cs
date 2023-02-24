using System;
using System.Collections.Generic;
using LibNoiseDotNet.Graphics.Tools.Noise;
using LibNoiseDotNet.Graphics.Tools.Noise.Modifier;

namespace ProcGen.Noise
{
	// Token: 0x020004EC RID: 1260
	public class Modifier : NoiseBase
	{
		// Token: 0x06003659 RID: 13913 RVA: 0x000773AC File Offset: 0x000755AC
		public override Type GetObjectType()
		{
			return typeof(Modifier);
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000773B8 File Offset: 0x000755B8
		// (set) Token: 0x0600365B RID: 13915 RVA: 0x000773C0 File Offset: 0x000755C0
		public Modifier.ModifyType modifyType { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000773C9 File Offset: 0x000755C9
		// (set) Token: 0x0600365D RID: 13917 RVA: 0x000773D1 File Offset: 0x000755D1
		public float lower { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000773DA File Offset: 0x000755DA
		// (set) Token: 0x0600365F RID: 13919 RVA: 0x000773E2 File Offset: 0x000755E2
		public float upper { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000773EB File Offset: 0x000755EB
		// (set) Token: 0x06003661 RID: 13921 RVA: 0x000773F3 File Offset: 0x000755F3
		public float exponent { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000773FC File Offset: 0x000755FC
		// (set) Token: 0x06003663 RID: 13923 RVA: 0x00077404 File Offset: 0x00075604
		public bool invert { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x0007740D File Offset: 0x0007560D
		// (set) Token: 0x06003665 RID: 13925 RVA: 0x00077415 File Offset: 0x00075615
		public float scale { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x0007741E File Offset: 0x0007561E
		// (set) Token: 0x06003667 RID: 13927 RVA: 0x00077426 File Offset: 0x00075626
		public float bias { get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x0007742F File Offset: 0x0007562F
		// (set) Token: 0x06003669 RID: 13929 RVA: 0x00077437 File Offset: 0x00075637
		public Vector2f scale2d { get; set; }

		// Token: 0x0600366A RID: 13930 RVA: 0x00077440 File Offset: 0x00075640
		public Modifier()
		{
			this.modifyType = Modifier.ModifyType.Abs;
			this.lower = -1f;
			this.upper = 1f;
			this.exponent = 0.02f;
			this.invert = false;
			this.scale = 1f;
			this.bias = 0f;
			this.scale2d = new Vector2f(1, 1);
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000774A8 File Offset: 0x000756A8
		public IModule3D CreateModule()
		{
			switch (this.modifyType)
			{
			case Modifier.ModifyType.Abs:
				return new Abs();
			case Modifier.ModifyType.Clamp:
				return new Clamp
				{
					LowerBound = this.lower,
					UpperBound = this.upper
				};
			case Modifier.ModifyType.Exponent:
				return new Exponent
				{
					ExponentValue = this.exponent
				};
			case Modifier.ModifyType.Invert:
				return new Invert();
			case Modifier.ModifyType.ScaleBias:
				return new ScaleBias
				{
					Scale = this.scale,
					Bias = this.bias
				};
			case Modifier.ModifyType.Scale2d:
				return new Scale2d
				{
					Scale = this.scale2d
				};
			case Modifier.ModifyType.Curve:
				return new Curve();
			case Modifier.ModifyType.Terrace:
				return new Terrace();
			default:
				return null;
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x00077564 File Offset: 0x00075764
		public IModule3D CreateModule(IModule3D sourceModule)
		{
			switch (this.modifyType)
			{
			case Modifier.ModifyType.Abs:
				return new Abs(sourceModule);
			case Modifier.ModifyType.Clamp:
				return new Clamp(sourceModule, this.lower, this.upper);
			case Modifier.ModifyType.Exponent:
				return new Exponent(sourceModule, this.exponent);
			case Modifier.ModifyType.Invert:
				return new Invert(sourceModule);
			case Modifier.ModifyType.ScaleBias:
				return new ScaleBias(sourceModule, this.scale, this.bias);
			case Modifier.ModifyType.Scale2d:
				return new Scale2d(sourceModule, this.scale2d);
			case Modifier.ModifyType.Curve:
				return new Curve(sourceModule);
			case Modifier.ModifyType.Terrace:
				return new Terrace(sourceModule);
			default:
				return null;
			}
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x00077604 File Offset: 0x00075804
		public void SetSouces(IModule3D target, IModule3D sourceModule, FloatList controlFloats, ControlPointList controlPoints)
		{
			(target as ModifierModule).SourceModule = sourceModule;
			if (this.modifyType == Modifier.ModifyType.Curve)
			{
				Curve curve = target as Curve;
				curve.ClearControlPoints();
				using (List<ControlPoint>.Enumerator enumerator = controlPoints.GetControls().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ControlPoint controlPoint = enumerator.Current;
						curve.AddControlPoint(controlPoint);
					}
					return;
				}
			}
			if (this.modifyType == Modifier.ModifyType.Terrace)
			{
				Terrace terrace = target as Terrace;
				terrace.ClearControlPoints();
				foreach (float num in controlFloats.points)
				{
					terrace.AddControlPoint(num);
				}
			}
		}

		// Token: 0x02000B14 RID: 2836
		public enum ModifyType
		{
			// Token: 0x04002607 RID: 9735
			_UNSET_,
			// Token: 0x04002608 RID: 9736
			Abs,
			// Token: 0x04002609 RID: 9737
			Clamp,
			// Token: 0x0400260A RID: 9738
			Exponent,
			// Token: 0x0400260B RID: 9739
			Invert,
			// Token: 0x0400260C RID: 9740
			ScaleBias,
			// Token: 0x0400260D RID: 9741
			Scale2d,
			// Token: 0x0400260E RID: 9742
			Curve,
			// Token: 0x0400260F RID: 9743
			Terrace
		}
	}
}
