using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C21 RID: 3105
[AddComponentMenu("KMonoBehaviour/scripts/VideoOverlay")]
public class VideoOverlay : KMonoBehaviour
{
	// Token: 0x06006249 RID: 25161 RVA: 0x00244994 File Offset: 0x00242B94
	public void SetText(List<string> strings)
	{
		if (strings.Count != this.textFields.Count)
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				base.name,
				"expects",
				this.textFields.Count,
				"strings passed to it, got",
				strings.Count
			});
		}
		for (int i = 0; i < this.textFields.Count; i++)
		{
			this.textFields[i].text = strings[i];
		}
	}

	// Token: 0x040043FA RID: 17402
	public List<LocText> textFields;
}
