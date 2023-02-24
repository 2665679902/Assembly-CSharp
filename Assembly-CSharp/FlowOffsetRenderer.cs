using System;
using UnityEngine;

// Token: 0x0200077C RID: 1916
[AddComponentMenu("KMonoBehaviour/scripts/FlowOffsetRenderer")]
public class FlowOffsetRenderer : KMonoBehaviour
{
	// Token: 0x060034BE RID: 13502 RVA: 0x0011C73C File Offset: 0x0011A93C
	protected override void OnSpawn()
	{
		this.FlowMaterial = new Material(Shader.Find("Klei/Flow"));
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
		this.OnResize();
		this.DoUpdate(0.1f);
	}

	// Token: 0x060034BF RID: 13503 RVA: 0x0011C798 File Offset: 0x0011A998
	private void OnResize()
	{
		for (int i = 0; i < this.OffsetTextures.Length; i++)
		{
			if (this.OffsetTextures[i] != null)
			{
				this.OffsetTextures[i].DestroyRenderTexture();
			}
			this.OffsetTextures[i] = new RenderTexture(Screen.width / 2, Screen.height / 2, 0, RenderTextureFormat.ARGBHalf);
			this.OffsetTextures[i].filterMode = FilterMode.Bilinear;
			this.OffsetTextures[i].name = "FlowOffsetTexture";
		}
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x0011C814 File Offset: 0x0011AA14
	private void LateUpdate()
	{
		if ((Time.deltaTime > 0f && Time.timeScale > 0f) || this.forceUpdate)
		{
			float num = Time.deltaTime / Time.timeScale;
			this.DoUpdate(num * Time.timeScale / 4f + num * 0.5f);
		}
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x0011C868 File Offset: 0x0011AA68
	private void DoUpdate(float dt)
	{
		this.CurrentTime += dt;
		float num = this.CurrentTime * this.PhaseMultiplier;
		num -= (float)((int)num);
		float num2 = num - (float)((int)num);
		float num3 = 1f;
		if (num2 <= this.GasPhase0)
		{
			num3 = 0f;
		}
		this.GasPhase0 = num2;
		float num4 = 1f;
		float num5 = num + 0.5f - (float)((int)(num + 0.5f));
		if (num5 <= this.GasPhase1)
		{
			num4 = 0f;
		}
		this.GasPhase1 = num5;
		Shader.SetGlobalVector(this.ParametersName, new Vector4(this.GasPhase0, 0f, 0f, 0f));
		Shader.SetGlobalVector("_NoiseParameters", new Vector4(this.NoiseInfluence, this.NoiseScale, 0f, 0f));
		RenderTexture renderTexture = this.OffsetTextures[this.OffsetIdx];
		this.OffsetIdx = (this.OffsetIdx + 1) % 2;
		RenderTexture renderTexture2 = this.OffsetTextures[this.OffsetIdx];
		Material flowMaterial = this.FlowMaterial;
		flowMaterial.SetTexture("_PreviousOffsetTex", renderTexture);
		flowMaterial.SetVector("_FlowParameters", new Vector4(Time.deltaTime * this.OffsetSpeed, num3, num4, 0f));
		flowMaterial.SetVector("_MinFlow", new Vector4(this.MinFlow0.x, this.MinFlow0.y, this.MinFlow1.x, this.MinFlow1.y));
		flowMaterial.SetVector("_VisibleArea", new Vector4(0f, 0f, (float)Grid.WidthInCells, (float)Grid.HeightInCells));
		flowMaterial.SetVector("_LiquidGasMask", new Vector4(this.LiquidGasMask.x, this.LiquidGasMask.y, 0f, 0f));
		Graphics.Blit(renderTexture, renderTexture2, flowMaterial);
		Shader.SetGlobalTexture(this.OffsetTextureName, renderTexture2);
	}

	// Token: 0x040020A7 RID: 8359
	private float GasPhase0;

	// Token: 0x040020A8 RID: 8360
	private float GasPhase1;

	// Token: 0x040020A9 RID: 8361
	public float PhaseMultiplier;

	// Token: 0x040020AA RID: 8362
	public float NoiseInfluence;

	// Token: 0x040020AB RID: 8363
	public float NoiseScale;

	// Token: 0x040020AC RID: 8364
	public float OffsetSpeed;

	// Token: 0x040020AD RID: 8365
	public string OffsetTextureName;

	// Token: 0x040020AE RID: 8366
	public string ParametersName;

	// Token: 0x040020AF RID: 8367
	public Vector2 MinFlow0;

	// Token: 0x040020B0 RID: 8368
	public Vector2 MinFlow1;

	// Token: 0x040020B1 RID: 8369
	public Vector2 LiquidGasMask;

	// Token: 0x040020B2 RID: 8370
	[SerializeField]
	private Material FlowMaterial;

	// Token: 0x040020B3 RID: 8371
	[SerializeField]
	private bool forceUpdate;

	// Token: 0x040020B4 RID: 8372
	private TextureLerper FlowLerper;

	// Token: 0x040020B5 RID: 8373
	public RenderTexture[] OffsetTextures = new RenderTexture[2];

	// Token: 0x040020B6 RID: 8374
	private int OffsetIdx;

	// Token: 0x040020B7 RID: 8375
	private float CurrentTime;
}
