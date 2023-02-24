using System;
using UnityEngine;

// Token: 0x020008BB RID: 2235
public class CameraRenderTexture : MonoBehaviour
{
	// Token: 0x0600404C RID: 16460 RVA: 0x00166F7C File Offset: 0x0016517C
	private void Awake()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/CameraRenderTexture"));
	}

	// Token: 0x0600404D RID: 16461 RVA: 0x00166F93 File Offset: 0x00165193
	private void Start()
	{
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
		}
		this.OnResize();
	}

	// Token: 0x0600404E RID: 16462 RVA: 0x00166FD0 File Offset: 0x001651D0
	private void OnResize()
	{
		if (this.resultTexture != null)
		{
			this.resultTexture.DestroyRenderTexture();
		}
		this.resultTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		this.resultTexture.name = base.name;
		this.resultTexture.filterMode = FilterMode.Point;
		this.resultTexture.autoGenerateMips = false;
		if (this.TextureName != "")
		{
			Shader.SetGlobalTexture(this.TextureName, this.resultTexture);
		}
	}

	// Token: 0x0600404F RID: 16463 RVA: 0x00167059 File Offset: 0x00165259
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, this.resultTexture, this.material);
	}

	// Token: 0x06004050 RID: 16464 RVA: 0x0016706D File Offset: 0x0016526D
	public RenderTexture GetTexture()
	{
		return this.resultTexture;
	}

	// Token: 0x06004051 RID: 16465 RVA: 0x00167075 File Offset: 0x00165275
	public bool ShouldFlip()
	{
		return false;
	}

	// Token: 0x04002A25 RID: 10789
	public string TextureName;

	// Token: 0x04002A26 RID: 10790
	private RenderTexture resultTexture;

	// Token: 0x04002A27 RID: 10791
	private Material material;
}
