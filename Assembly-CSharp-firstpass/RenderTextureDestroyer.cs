using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000042 RID: 66
[AddComponentMenu("KMonoBehaviour/Plugins/RenderTextureDestroyer")]
public class RenderTextureDestroyer : KMonoBehaviour
{
	// Token: 0x060002F6 RID: 758 RVA: 0x000104C8 File Offset: 0x0000E6C8
	public static void DestroyInstance()
	{
		RenderTextureDestroyer.Instance = null;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000104D0 File Offset: 0x0000E6D0
	protected override void OnPrefabInit()
	{
		RenderTextureDestroyer.Instance = this;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x000104D8 File Offset: 0x0000E6D8
	public void Add(RenderTexture render_texture)
	{
		this.queued.Add(render_texture);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x000104E8 File Offset: 0x0000E6E8
	private void LateUpdate()
	{
		foreach (RenderTexture renderTexture in this.finished)
		{
			UnityEngine.Object.Destroy(renderTexture);
		}
		this.finished.Clear();
		this.finished.AddRange(this.queued);
		this.queued.Clear();
	}

	// Token: 0x040003AE RID: 942
	public static RenderTextureDestroyer Instance;

	// Token: 0x040003AF RID: 943
	public List<RenderTexture> queued = new List<RenderTexture>();

	// Token: 0x040003B0 RID: 944
	public List<RenderTexture> finished = new List<RenderTexture>();
}
