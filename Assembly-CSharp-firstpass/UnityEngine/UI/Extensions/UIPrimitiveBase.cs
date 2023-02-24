using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000245 RID: 581
	public class UIPrimitiveBase : MaskableGraphic, ILayoutElement, ICanvasRaycastFilter
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000459C0 File Offset: 0x00043BC0
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x000459C8 File Offset: 0x00043BC8
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x000459DE File Offset: 0x00043BDE
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x000459FB File Offset: 0x00043BFB
		public Sprite overrideSprite
		{
			get
			{
				if (!(this.m_OverrideSprite == null))
				{
					return this.m_OverrideSprite;
				}
				return this.sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00045A11 File Offset: 0x00043C11
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x00045A19 File Offset: 0x00043C19
		public float eventAlphaThreshold
		{
			get
			{
				return this.m_EventAlphaThreshold;
			}
			set
			{
				this.m_EventAlphaThreshold = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00045A24 File Offset: 0x00043C24
		public override Texture mainTexture
		{
			get
			{
				if (!(this.overrideSprite == null))
				{
					return this.overrideSprite.texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00045A80 File Offset: 0x00043C80
		public float pixelsPerUnit
		{
			get
			{
				float num = 100f;
				if (this.sprite)
				{
					num = this.sprite.pixelsPerUnit;
				}
				float num2 = 100f;
				if (base.canvas)
				{
					num2 = base.canvas.referencePixelsPerUnit;
				}
				return num / num2;
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00045AD0 File Offset: 0x00043CD0
		protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
		{
			UIVertex[] array = new UIVertex[4];
			for (int i = 0; i < vertices.Length; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.color = this.color;
				simpleVert.position = vertices[i];
				simpleVert.uv0 = uvs[i];
				array[i] = simpleVert;
			}
			return array;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00045B39 File Offset: 0x00043D39
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00045B3B File Offset: 0x00043D3B
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00045B3D File Offset: 0x00043D3D
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00045B44 File Offset: 0x00043D44
		public virtual float preferredWidth
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				return this.overrideSprite.rect.size.x / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00045B84 File Offset: 0x00043D84
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00045B8B File Offset: 0x00043D8B
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00045B94 File Offset: 0x00043D94
		public virtual float preferredHeight
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				return this.overrideSprite.rect.size.y / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00045BD4 File Offset: 0x00043DD4
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00045BDB File Offset: 0x00043DDB
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00045BE0 File Offset: 0x00043DE0
		public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (this.m_EventAlphaThreshold >= 1f)
			{
				return true;
			}
			Sprite overrideSprite = this.overrideSprite;
			if (overrideSprite == null)
			{
				return true;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out vector);
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			vector.x += base.rectTransform.pivot.x * pixelAdjustedRect.width;
			vector.y += base.rectTransform.pivot.y * pixelAdjustedRect.height;
			vector = this.MapCoordinate(vector, pixelAdjustedRect);
			Rect textureRect = overrideSprite.textureRect;
			Vector2 vector2 = new Vector2(vector.x / textureRect.width, vector.y / textureRect.height);
			float num = Mathf.Lerp(textureRect.x, textureRect.xMax, vector2.x) / (float)overrideSprite.texture.width;
			float num2 = Mathf.Lerp(textureRect.y, textureRect.yMax, vector2.y) / (float)overrideSprite.texture.height;
			bool flag;
			try
			{
				flag = overrideSprite.texture.GetPixelBilinear(num, num2).a >= this.m_EventAlphaThreshold;
			}
			catch (UnityException ex)
			{
				Debug.LogError("Using clickAlphaThreshold lower than 1 on Image whose sprite texture cannot be read. " + ex.Message + " Also make sure to disable sprite packing for this sprite.", this);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00045D48 File Offset: 0x00043F48
		private Vector2 MapCoordinate(Vector2 local, Rect rect)
		{
			Rect rect2 = this.sprite.rect;
			return new Vector2(local.x * rect2.width / rect.width, local.y * rect2.height / rect.height);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00045D94 File Offset: 0x00043F94
		private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					ref Vector4 ptr = ref border;
					int num3 = i;
					ptr[num3] *= num2;
					ptr = ref border;
					num3 = i + 2;
					ptr[num3] *= num2;
				}
			}
			return border;
		}

		// Token: 0x0400095E RID: 2398
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x0400095F RID: 2399
		[NonSerialized]
		private Sprite m_OverrideSprite;

		// Token: 0x04000960 RID: 2400
		internal float m_EventAlphaThreshold = 1f;
	}
}
