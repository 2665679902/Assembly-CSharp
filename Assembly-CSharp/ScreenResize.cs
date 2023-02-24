using System;
using UnityEngine;

// Token: 0x020008CE RID: 2254
public class ScreenResize : MonoBehaviour
{
	// Token: 0x060040C5 RID: 16581 RVA: 0x0016A864 File Offset: 0x00168A64
	private void Awake()
	{
		ScreenResize.Instance = this;
		this.isFullscreen = Screen.fullScreen;
		this.OnResize = (System.Action)Delegate.Combine(this.OnResize, new System.Action(this.SaveResolutionToPrefs));
	}

	// Token: 0x060040C6 RID: 16582 RVA: 0x0016A89C File Offset: 0x00168A9C
	private void LateUpdate()
	{
		if (Screen.width != this.Width || Screen.height != this.Height || this.isFullscreen != Screen.fullScreen)
		{
			this.Width = Screen.width;
			this.Height = Screen.height;
			this.isFullscreen = Screen.fullScreen;
			this.TriggerResize();
		}
	}

	// Token: 0x060040C7 RID: 16583 RVA: 0x0016A8F7 File Offset: 0x00168AF7
	public void TriggerResize()
	{
		if (this.OnResize != null)
		{
			this.OnResize();
		}
	}

	// Token: 0x060040C8 RID: 16584 RVA: 0x0016A90C File Offset: 0x00168B0C
	private void SaveResolutionToPrefs()
	{
		GraphicsOptionsScreen.OnResize();
	}

	// Token: 0x04002B2F RID: 11055
	public System.Action OnResize;

	// Token: 0x04002B30 RID: 11056
	public static ScreenResize Instance;

	// Token: 0x04002B31 RID: 11057
	private int Width;

	// Token: 0x04002B32 RID: 11058
	private int Height;

	// Token: 0x04002B33 RID: 11059
	private bool isFullscreen;
}
