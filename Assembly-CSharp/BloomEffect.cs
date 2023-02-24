using System;
using UnityEngine;

// Token: 0x020009E2 RID: 2530
public class BloomEffect : MonoBehaviour
{
	// Token: 0x170005A6 RID: 1446
	// (get) Token: 0x06004B9C RID: 19356 RVA: 0x001A8F17 File Offset: 0x001A7117
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.blurShader);
				this.m_Material.hideFlags = HideFlags.DontSave;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06004B9D RID: 19357 RVA: 0x001A8F4B File Offset: 0x001A714B
	protected void OnDisable()
	{
		if (this.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x06004B9E RID: 19358 RVA: 0x001A8F68 File Offset: 0x001A7168
	protected void Start()
	{
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
		this.BloomMaskMaterial = new Material(Shader.Find("Klei/PostFX/BloomMask"));
		this.BloomCompositeMaterial = new Material(Shader.Find("Klei/PostFX/BloomComposite"));
	}

	// Token: 0x06004B9F RID: 19359 RVA: 0x001A8FC8 File Offset: 0x001A71C8
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06004BA0 RID: 19360 RVA: 0x001A9034 File Offset: 0x001A7234
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06004BA1 RID: 19361 RVA: 0x001A9098 File Offset: 0x001A7298
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
		temporary.name = "bloom_source";
		Graphics.Blit(source, temporary, this.BloomMaskMaterial);
		int num = Math.Max(source.width / 4, 4);
		int num2 = Math.Max(source.height / 4, 4);
		RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0);
		renderTexture.name = "bloom_downsampled";
		this.DownSample4x(temporary, renderTexture);
		RenderTexture.ReleaseTemporary(temporary);
		for (int i = 0; i < this.iterations; i++)
		{
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0);
			temporary2.name = "bloom_blurred";
			this.FourTapCone(renderTexture, temporary2, i);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary2;
		}
		this.BloomCompositeMaterial.SetTexture("_BloomTex", renderTexture);
		Graphics.Blit(source, destination, this.BloomCompositeMaterial);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x04003189 RID: 12681
	private Material BloomMaskMaterial;

	// Token: 0x0400318A RID: 12682
	private Material BloomCompositeMaterial;

	// Token: 0x0400318B RID: 12683
	public int iterations = 3;

	// Token: 0x0400318C RID: 12684
	public float blurSpread = 0.6f;

	// Token: 0x0400318D RID: 12685
	public Shader blurShader;

	// Token: 0x0400318E RID: 12686
	private Material m_Material;
}
