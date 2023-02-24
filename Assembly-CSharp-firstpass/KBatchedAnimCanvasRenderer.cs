using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class KBatchedAnimCanvasRenderer : MonoBehaviour, IMaskable
{
	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000182 RID: 386 RVA: 0x00008552 File Offset: 0x00006752
	// (set) Token: 0x06000183 RID: 387 RVA: 0x0000855A File Offset: 0x0000675A
	public CanvasRenderer canvass { get; private set; }

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000184 RID: 388 RVA: 0x00008563 File Offset: 0x00006763
	// (set) Token: 0x06000185 RID: 389 RVA: 0x0000856B File Offset: 0x0000676B
	public CompareFunction compare
	{
		get
		{
			return this._cmp;
		}
		set
		{
			if (this._cmp != value)
			{
				if (this.uiMat != null)
				{
					this.uiMat.SetInt("_StencilComp", (int)value);
				}
				this._cmp = value;
			}
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000186 RID: 390 RVA: 0x0000859C File Offset: 0x0000679C
	// (set) Token: 0x06000187 RID: 391 RVA: 0x000085A4 File Offset: 0x000067A4
	public StencilOp stencilOp
	{
		get
		{
			return this._op;
		}
		set
		{
			if (this._op != value)
			{
				if (this.uiMat != null)
				{
					this.uiMat.SetInt("_StencilOp", (int)value);
				}
				this._op = value;
			}
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000188 RID: 392 RVA: 0x000085D5 File Offset: 0x000067D5
	// (set) Token: 0x06000189 RID: 393 RVA: 0x000085DD File Offset: 0x000067DD
	public int writeMask
	{
		get
		{
			return this._writeMask;
		}
		set
		{
			if (this._writeMask != value)
			{
				if (this.uiMat != null)
				{
					this.uiMat.SetInt("_StencilWriteMask", value);
				}
				this._writeMask = value;
			}
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x0600018A RID: 394 RVA: 0x0000860E File Offset: 0x0000680E
	// (set) Token: 0x0600018B RID: 395 RVA: 0x00008616 File Offset: 0x00006816
	public int colorMask
	{
		get
		{
			return this._colorMask;
		}
		set
		{
			if (this._colorMask != value)
			{
				if (this.uiMat != null)
				{
					this.uiMat.SetInt("_ColorMask", value);
				}
				this._colorMask = value;
			}
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00008648 File Offset: 0x00006848
	void IMaskable.RecalculateMasking()
	{
		Mask[] componentsInParent = base.GetComponentsInParent<Mask>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (componentsInParent != null && componentsInParent.Length != 0)
		{
			for (int i = 0; i < componentsInParent.Length; i++)
			{
				if (componentsInParent[i].enabled)
				{
					if (componentsInParent[i].gameObject == base.gameObject)
					{
						flag = true;
						flag2 = !componentsInParent[i].showMaskGraphic;
					}
					else
					{
						flag3 = true;
					}
				}
			}
		}
		if (flag)
		{
			if (flag3)
			{
				this.compare = CompareFunction.Equal;
			}
			else
			{
				this.compare = CompareFunction.Always;
			}
			if (flag2)
			{
				this.colorMask = 0;
			}
			else
			{
				this.colorMask = this._originalColorMask;
			}
			this.writeMask = 1;
			this.stencilOp = StencilOp.Replace;
		}
		else if (flag3)
		{
			this.compare = CompareFunction.Equal;
			this.stencilOp = StencilOp.Keep;
		}
		else
		{
			this.compare = CompareFunction.Disabled;
			this.stencilOp = StencilOp.Keep;
		}
		if (this.uiMat != null)
		{
			this.uiMat.SetInt("_StencilComp", (int)this.compare);
			this.uiMat.SetInt("_StencilOp", (int)this.stencilOp);
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00008748 File Offset: 0x00006948
	public void SetBatch(KAnimConverter.IAnimConverter conv)
	{
		this.converter = conv;
		if (conv != null)
		{
			this.batch = conv.GetBatch();
		}
		else
		{
			this.batch = null;
			if (this.uiMat != null)
			{
				UnityEngine.Object.Destroy(this.uiMat);
				this.uiMat = null;
			}
		}
		if (this.batch != null)
		{
			this.canvass = base.GetComponent<CanvasRenderer>();
			if (this.canvass == null)
			{
				this.canvass = base.gameObject.AddComponent<CanvasRenderer>();
			}
			this.rootRectTransform = base.GetComponent<RectTransform>();
			if (this.rootRectTransform == null)
			{
				this.rootRectTransform = base.gameObject.AddComponent<RectTransform>();
			}
			if (!this.batch.group.InitOK)
			{
				return;
			}
			if (this.uiMat != null)
			{
				UnityEngine.Object.Destroy(this.uiMat);
				this.uiMat = null;
			}
			Material material = this.batch.group.GetMaterial(this.batch.materialType);
			this.uiMat = new Material(material);
			this._originalColorMask = this.uiMat.GetInt("_ColorMask");
			this._colorMask = this._originalColorMask;
			this._writeMask = this.uiMat.GetInt("_StencilWriteMask");
			((IMaskable)this).RecalculateMasking();
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00008890 File Offset: 0x00006A90
	private void UpdateCanvas()
	{
		this.canvass.Clear();
		this.canvass.SetMesh(this.batch.group.mesh);
		this.canvass.materialCount = 1;
		this.canvass.SetMaterial(this.uiMat, 0);
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000088E4 File Offset: 0x00006AE4
	private void CopyPropertyBlockToMaterial()
	{
		if (KBatchedAnimCanvasRenderer.texturesToCopy == null)
		{
			KBatchedAnimCanvasRenderer.texturesToCopy = new KBatchedAnimCanvasRenderer.TextureToCopyEntry[]
			{
				new KBatchedAnimCanvasRenderer.TextureToCopyEntry
				{
					textureId = Shader.PropertyToID("instanceTex"),
					sizeId = Shader.PropertyToID("INSTANCE_TEXTURE_SIZE")
				},
				new KBatchedAnimCanvasRenderer.TextureToCopyEntry
				{
					textureId = Shader.PropertyToID("buildAndAnimTex"),
					sizeId = Shader.PropertyToID("BUILD_AND_ANIM_TEXTURE_SIZE")
				},
				new KBatchedAnimCanvasRenderer.TextureToCopyEntry
				{
					textureId = Shader.PropertyToID("symbolInstanceTex"),
					sizeId = Shader.PropertyToID("SYMBOL_INSTANCE_TEXTURE_SIZE")
				},
				new KBatchedAnimCanvasRenderer.TextureToCopyEntry
				{
					textureId = Shader.PropertyToID("symbolOverrideInfoTex"),
					sizeId = Shader.PropertyToID("SYMBOL_OVERRIDE_INFO_TEXTURE_SIZE")
				}
			};
		}
		foreach (KBatchedAnimCanvasRenderer.TextureToCopyEntry textureToCopyEntry in KBatchedAnimCanvasRenderer.texturesToCopy)
		{
			this.uiMat.SetTexture(textureToCopyEntry.textureId, this.batch.matProperties.GetTexture(textureToCopyEntry.textureId));
			this.uiMat.SetVector(textureToCopyEntry.sizeId, this.batch.matProperties.GetVector(textureToCopyEntry.sizeId));
		}
		for (int j = 0; j < KAnimBatchManager.AtlasNames.Length; j++)
		{
			Texture texture = this.batch.matProperties.GetTexture(KAnimBatchManager.AtlasNames[j]);
			if (texture != null)
			{
				this.uiMat.SetTexture(KAnimBatchManager.AtlasNames[j], texture);
			}
		}
		foreach (KBatchedAnimCanvasRenderer.TextureToCopyEntry textureToCopyEntry2 in KBatchedAnimCanvasRenderer.texturesToCopy)
		{
			this.uiMat.SetTexture(textureToCopyEntry2.textureId, this.batch.matProperties.GetTexture(textureToCopyEntry2.textureId));
			this.uiMat.SetVector(textureToCopyEntry2.sizeId, this.batch.matProperties.GetVector(textureToCopyEntry2.sizeId));
		}
		for (int k = 0; k < KAnimBatchManager.AtlasNames.Length; k++)
		{
			Texture texture2 = this.batch.matProperties.GetTexture(KAnimBatchManager.AtlasNames[k]);
			if (texture2 != null)
			{
				this.uiMat.SetTexture(KAnimBatchManager.AtlasNames[k], texture2);
			}
		}
		this.uiMat.SetFloat(KAnimBatch.ShaderProperty_SUPPORTS_SYMBOL_OVERRIDING, this.batch.matProperties.GetFloat(KAnimBatch.ShaderProperty_SUPPORTS_SYMBOL_OVERRIDING));
		this.uiMat.SetFloat(KAnimBatch.ShaderProperty_ANIM_TEXTURE_START_OFFSET, this.batch.matProperties.GetFloat(KAnimBatch.ShaderProperty_ANIM_TEXTURE_START_OFFSET));
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00008B90 File Offset: 0x00006D90
	private void LateUpdate()
	{
		if (this.batch != null)
		{
			if (base.transform.hasChanged)
			{
				this.batch.SetDirty(this.converter);
				base.transform.hasChanged = false;
			}
			this._ClipRect.x = this.rootRectTransform.rect.xMin;
			this._ClipRect.y = this.rootRectTransform.rect.yMin;
			this._ClipRect.z = this.rootRectTransform.rect.xMax;
			this._ClipRect.w = this.rootRectTransform.rect.yMax;
			this.UpdateCanvas();
			this.CopyPropertyBlockToMaterial();
		}
	}

	// Token: 0x04000091 RID: 145
	private RectTransform rootRectTransform;

	// Token: 0x04000092 RID: 146
	public KAnimBatch batch;

	// Token: 0x04000093 RID: 147
	public Material uiMat;

	// Token: 0x04000094 RID: 148
	private KAnimConverter.IAnimConverter converter;

	// Token: 0x04000095 RID: 149
	private CompareFunction _cmp = CompareFunction.Never;

	// Token: 0x04000096 RID: 150
	private StencilOp _op = StencilOp.Zero;

	// Token: 0x04000097 RID: 151
	private int _writeMask;

	// Token: 0x04000098 RID: 152
	private int _colorMask;

	// Token: 0x04000099 RID: 153
	private int _originalColorMask;

	// Token: 0x0400009A RID: 154
	private static KBatchedAnimCanvasRenderer.TextureToCopyEntry[] texturesToCopy;

	// Token: 0x0400009B RID: 155
	private Vector4 _ClipRect = new Vector4(0f, 0f, 0f, 1f);

	// Token: 0x0200096D RID: 2413
	private struct TextureToCopyEntry
	{
		// Token: 0x04002094 RID: 8340
		public int textureId;

		// Token: 0x04002095 RID: 8341
		public int sizeId;
	}
}
