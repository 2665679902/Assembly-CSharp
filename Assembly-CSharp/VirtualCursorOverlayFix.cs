using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C25 RID: 3109
public class VirtualCursorOverlayFix : MonoBehaviour
{
	// Token: 0x06006269 RID: 25193 RVA: 0x002453E0 File Offset: 0x002435E0
	private void Awake()
	{
		int width = Screen.currentResolution.width;
		int height = Screen.currentResolution.height;
		this.cursorRendTex = new RenderTexture(width, height, 0);
		this.screenSpaceCamera.enabled = true;
		this.screenSpaceCamera.targetTexture = this.cursorRendTex;
		this.screenSpaceOverlayImage.material.SetTexture("_MainTex", this.cursorRendTex);
		base.StartCoroutine(this.RenderVirtualCursor());
	}

	// Token: 0x0600626A RID: 25194 RVA: 0x0024545C File Offset: 0x0024365C
	private IEnumerator RenderVirtualCursor()
	{
		bool ShowCursor = KInputManager.currentControllerIsGamepad;
		while (Application.isPlaying)
		{
			ShowCursor = KInputManager.currentControllerIsGamepad;
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.C))
			{
				ShowCursor = true;
			}
			this.screenSpaceCamera.enabled = true;
			if (!this.screenSpaceOverlayImage.enabled && ShowCursor)
			{
				yield return SequenceUtil.WaitForSecondsRealtime(0.1f);
			}
			this.actualCursor.enabled = ShowCursor;
			this.screenSpaceOverlayImage.enabled = ShowCursor;
			this.screenSpaceOverlayImage.material.SetTexture("_MainTex", this.cursorRendTex);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400441B RID: 17435
	private RenderTexture cursorRendTex;

	// Token: 0x0400441C RID: 17436
	public Camera screenSpaceCamera;

	// Token: 0x0400441D RID: 17437
	public Image screenSpaceOverlayImage;

	// Token: 0x0400441E RID: 17438
	public RawImage actualCursor;
}
