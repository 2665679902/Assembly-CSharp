using System;
using UnityEngine;

// Token: 0x020008CF RID: 2255
public class SimDebugViewCompositor : MonoBehaviour
{
	// Token: 0x060040CA RID: 16586 RVA: 0x0016A91B File Offset: 0x00168B1B
	private void Awake()
	{
		SimDebugViewCompositor.Instance = this;
	}

	// Token: 0x060040CB RID: 16587 RVA: 0x0016A923 File Offset: 0x00168B23
	private void OnDestroy()
	{
		SimDebugViewCompositor.Instance = null;
	}

	// Token: 0x060040CC RID: 16588 RVA: 0x0016A92B File Offset: 0x00168B2B
	private void Start()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/SimDebugViewCompositor"));
		this.Toggle(false);
	}

	// Token: 0x060040CD RID: 16589 RVA: 0x0016A949 File Offset: 0x00168B49
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, this.material);
	}

	// Token: 0x060040CE RID: 16590 RVA: 0x0016A958 File Offset: 0x00168B58
	public void Toggle(bool is_on)
	{
		base.enabled = is_on;
	}

	// Token: 0x04002B34 RID: 11060
	public Material material;

	// Token: 0x04002B35 RID: 11061
	public static SimDebugViewCompositor Instance;
}
