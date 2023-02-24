using System;
using UnityEngine;

// Token: 0x020009E4 RID: 2532
public class FixGraphicsCorruption : MonoBehaviour
{
	// Token: 0x06004BA6 RID: 19366 RVA: 0x001A91AC File Offset: 0x001A73AC
	private void Start()
	{
		Camera component = base.GetComponent<Camera>();
		component.transparencySortMode = TransparencySortMode.Orthographic;
		component.tag = "Untagged";
	}

	// Token: 0x06004BA7 RID: 19367 RVA: 0x001A91C5 File Offset: 0x001A73C5
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest);
	}
}
