using System;
using UnityEngine;

// Token: 0x020008C2 RID: 2242
public class LightBufferCompositor : MonoBehaviour
{
	// Token: 0x0600408C RID: 16524 RVA: 0x00168BE0 File Offset: 0x00166DE0
	private void Start()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/LightBufferCompositor"));
		this.material.SetTexture("_InvalidTex", Assets.instance.invalidAreaTex);
		this.blurMaterial = new Material(Shader.Find("Klei/PostFX/Blur"));
		this.OnShadersReloaded();
		ShaderReloader.Register(new System.Action(this.OnShadersReloaded));
	}

	// Token: 0x0600408D RID: 16525 RVA: 0x00168C48 File Offset: 0x00166E48
	private void OnEnable()
	{
		this.OnShadersReloaded();
	}

	// Token: 0x0600408E RID: 16526 RVA: 0x00168C50 File Offset: 0x00166E50
	private void DownSample4x(Texture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x0600408F RID: 16527 RVA: 0x00168CB2 File Offset: 0x00166EB2
	[ContextMenu("ToggleParticles")]
	private void ToggleParticles()
	{
		this.particlesEnabled = !this.particlesEnabled;
		this.UpdateMaterialState();
	}

	// Token: 0x06004090 RID: 16528 RVA: 0x00168CC9 File Offset: 0x00166EC9
	public void SetParticlesEnabled(bool enabled)
	{
		this.particlesEnabled = enabled;
		this.UpdateMaterialState();
	}

	// Token: 0x06004091 RID: 16529 RVA: 0x00168CD8 File Offset: 0x00166ED8
	private void UpdateMaterialState()
	{
		if (this.particlesEnabled)
		{
			this.material.DisableKeyword("DISABLE_TEMPERATURE_PARTICLES");
			return;
		}
		this.material.EnableKeyword("DISABLE_TEMPERATURE_PARTICLES");
	}

	// Token: 0x06004092 RID: 16530 RVA: 0x00168D04 File Offset: 0x00166F04
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		RenderTexture renderTexture = null;
		if (PropertyTextures.instance != null)
		{
			Texture texture = PropertyTextures.instance.GetTexture(PropertyTextures.Property.Temperature);
			texture.name = "temperature_tex";
			renderTexture = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(texture, renderTexture, this.blurMaterial);
			Shader.SetGlobalTexture("_BlurredTemperature", renderTexture);
		}
		this.material.SetTexture("_LightBufferTex", LightBuffer.Instance.Texture);
		Graphics.Blit(src, dest, this.material);
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	// Token: 0x06004093 RID: 16531 RVA: 0x00168DA0 File Offset: 0x00166FA0
	private void OnShadersReloaded()
	{
		if (this.material != null && Lighting.Instance != null)
		{
			this.material.SetTexture("_EmberTex", Lighting.Instance.Settings.EmberTex);
			this.material.SetTexture("_FrostTex", Lighting.Instance.Settings.FrostTex);
			this.material.SetTexture("_Thermal1Tex", Lighting.Instance.Settings.Thermal1Tex);
			this.material.SetTexture("_Thermal2Tex", Lighting.Instance.Settings.Thermal2Tex);
			this.material.SetTexture("_RadHaze1Tex", Lighting.Instance.Settings.Radiation1Tex);
			this.material.SetTexture("_RadHaze2Tex", Lighting.Instance.Settings.Radiation2Tex);
			this.material.SetTexture("_RadHaze3Tex", Lighting.Instance.Settings.Radiation3Tex);
			this.material.SetTexture("_NoiseTex", Lighting.Instance.Settings.NoiseTex);
		}
	}

	// Token: 0x04002A4E RID: 10830
	[SerializeField]
	private Material material;

	// Token: 0x04002A4F RID: 10831
	[SerializeField]
	private Material blurMaterial;

	// Token: 0x04002A50 RID: 10832
	private bool particlesEnabled = true;
}
