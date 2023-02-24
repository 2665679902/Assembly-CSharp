using System;
using UnityEngine;

// Token: 0x020008CA RID: 2250
public class MultipleRenderTargetProxy : MonoBehaviour
{
	// Token: 0x060040AE RID: 16558 RVA: 0x0016A1F8 File Offset: 0x001683F8
	private void Start()
	{
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
		}
		this.CreateRenderTarget();
		ShaderReloader.Register(new System.Action(this.OnShadersReloaded));
	}

	// Token: 0x060040AF RID: 16559 RVA: 0x0016A24F File Offset: 0x0016844F
	public void ToggleColouredOverlayView(bool enabled)
	{
		this.colouredOverlayBufferEnabled = enabled;
		this.CreateRenderTarget();
	}

	// Token: 0x060040B0 RID: 16560 RVA: 0x0016A260 File Offset: 0x00168460
	private void CreateRenderTarget()
	{
		RenderBuffer[] array = new RenderBuffer[this.colouredOverlayBufferEnabled ? 3 : 2];
		this.Textures[0] = this.RecreateRT(this.Textures[0], 24, RenderTextureFormat.ARGB32);
		this.Textures[0].filterMode = FilterMode.Point;
		this.Textures[0].name = "MRT0";
		this.Textures[1] = this.RecreateRT(this.Textures[1], 0, RenderTextureFormat.R8);
		this.Textures[1].filterMode = FilterMode.Point;
		this.Textures[1].name = "MRT1";
		array[0] = this.Textures[0].colorBuffer;
		array[1] = this.Textures[1].colorBuffer;
		if (this.colouredOverlayBufferEnabled)
		{
			this.Textures[2] = this.RecreateRT(this.Textures[2], 0, RenderTextureFormat.ARGB32);
			this.Textures[2].filterMode = FilterMode.Bilinear;
			this.Textures[2].name = "MRT2";
			array[2] = this.Textures[2].colorBuffer;
		}
		base.GetComponent<Camera>().SetTargetBuffers(array, this.Textures[0].depthBuffer);
		this.OnShadersReloaded();
	}

	// Token: 0x060040B1 RID: 16561 RVA: 0x0016A38C File Offset: 0x0016858C
	private RenderTexture RecreateRT(RenderTexture rt, int depth, RenderTextureFormat format)
	{
		RenderTexture renderTexture = rt;
		if (rt == null || rt.width != Screen.width || rt.height != Screen.height || rt.format != format)
		{
			if (rt != null)
			{
				rt.DestroyRenderTexture();
			}
			renderTexture = new RenderTexture(Screen.width, Screen.height, depth, format);
		}
		return renderTexture;
	}

	// Token: 0x060040B2 RID: 16562 RVA: 0x0016A3E9 File Offset: 0x001685E9
	private void OnResize()
	{
		this.CreateRenderTarget();
	}

	// Token: 0x060040B3 RID: 16563 RVA: 0x0016A3F1 File Offset: 0x001685F1
	private void Update()
	{
		if (!this.Textures[0].IsCreated())
		{
			this.CreateRenderTarget();
		}
	}

	// Token: 0x060040B4 RID: 16564 RVA: 0x0016A408 File Offset: 0x00168608
	private void OnShadersReloaded()
	{
		Shader.SetGlobalTexture("_MRT0", this.Textures[0]);
		Shader.SetGlobalTexture("_MRT1", this.Textures[1]);
		if (this.colouredOverlayBufferEnabled)
		{
			Shader.SetGlobalTexture("_MRT2", this.Textures[2]);
		}
	}

	// Token: 0x04002B1B RID: 11035
	public RenderTexture[] Textures = new RenderTexture[3];

	// Token: 0x04002B1C RID: 11036
	private bool colouredOverlayBufferEnabled;
}
