using System;
using UnityEngine;

// Token: 0x02000C1D RID: 3101
public class URLOpenFunction : MonoBehaviour
{
	// Token: 0x06006235 RID: 25141 RVA: 0x002443BD File Offset: 0x002425BD
	private void Start()
	{
		if (this.triggerButton != null)
		{
			this.triggerButton.ClearOnClick();
			this.triggerButton.onClick += delegate
			{
				this.OpenUrl(this.fixedURL);
			};
		}
	}

	// Token: 0x06006236 RID: 25142 RVA: 0x002443EF File Offset: 0x002425EF
	public void OpenUrl(string url)
	{
		App.OpenWebURL(url);
	}

	// Token: 0x040043E7 RID: 17383
	[SerializeField]
	private KButton triggerButton;

	// Token: 0x040043E8 RID: 17384
	[SerializeField]
	private string fixedURL;
}
