using System;
using UnityEngine;

// Token: 0x020008BF RID: 2239
public class GridCompositor : MonoBehaviour
{
	// Token: 0x0600405C RID: 16476 RVA: 0x00168070 File Offset: 0x00166270
	public static void DestroyInstance()
	{
		GridCompositor.Instance = null;
	}

	// Token: 0x0600405D RID: 16477 RVA: 0x00168078 File Offset: 0x00166278
	private void Awake()
	{
		GridCompositor.Instance = this;
		base.enabled = false;
	}

	// Token: 0x0600405E RID: 16478 RVA: 0x00168087 File Offset: 0x00166287
	private void Start()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/GridCompositor"));
	}

	// Token: 0x0600405F RID: 16479 RVA: 0x0016809E File Offset: 0x0016629E
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, this.material);
	}

	// Token: 0x06004060 RID: 16480 RVA: 0x001680AD File Offset: 0x001662AD
	public void ToggleMajor(bool on)
	{
		this.onMajor = on;
		this.Refresh();
	}

	// Token: 0x06004061 RID: 16481 RVA: 0x001680BC File Offset: 0x001662BC
	public void ToggleMinor(bool on)
	{
		this.onMinor = on;
		this.Refresh();
	}

	// Token: 0x06004062 RID: 16482 RVA: 0x001680CB File Offset: 0x001662CB
	private void Refresh()
	{
		base.enabled = this.onMinor || this.onMajor;
	}

	// Token: 0x04002A2E RID: 10798
	public Material material;

	// Token: 0x04002A2F RID: 10799
	public static GridCompositor Instance;

	// Token: 0x04002A30 RID: 10800
	private bool onMajor;

	// Token: 0x04002A31 RID: 10801
	private bool onMinor;
}
