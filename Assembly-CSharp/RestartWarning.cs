using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008E6 RID: 2278
public class RestartWarning : MonoBehaviour
{
	// Token: 0x06004199 RID: 16793 RVA: 0x0016F7E6 File Offset: 0x0016D9E6
	private void Update()
	{
		if (RestartWarning.ShouldWarn)
		{
			this.text.enabled = true;
			this.image.enabled = true;
		}
	}

	// Token: 0x04002BBB RID: 11195
	public static bool ShouldWarn;

	// Token: 0x04002BBC RID: 11196
	public LocText text;

	// Token: 0x04002BBD RID: 11197
	public Image image;
}
