using System;
using UnityEngine;

namespace LibNoiseDotNet.Graphics.Tools.Noise.Modifier
{
	// Token: 0x020004B8 RID: 1208
	public class Scale2d : ModifierModule, IModule3D, IModule
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x0007085C File Offset: 0x0006EA5C
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x00070864 File Offset: 0x0006EA64
		public Vector2 Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x0007086D File Offset: 0x0006EA6D
		public Scale2d()
		{
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x0007088A File Offset: 0x0006EA8A
		public Scale2d(IModule source)
			: base(source)
		{
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000708A8 File Offset: 0x0006EAA8
		public Scale2d(IModule source, Vector2 scale)
			: base(source)
		{
			this._scale = scale;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000708CD File Offset: 0x0006EACD
		public float GetValue(float x, float y, float z)
		{
			return ((IModule3D)this._sourceModule).GetValue(x * this._scale.x, y, z * this._scale.y);
		}

		// Token: 0x0400121F RID: 4639
		public const float DEFAULT_SCALE = 1f;

		// Token: 0x04001220 RID: 4640
		protected Vector2 _scale = Vector2.one * 1f;
	}
}
