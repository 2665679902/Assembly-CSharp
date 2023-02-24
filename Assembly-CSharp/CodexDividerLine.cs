using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A66 RID: 2662
public class CodexDividerLine : CodexWidget<CodexDividerLine>
{
	// Token: 0x06005183 RID: 20867 RVA: 0x001D7144 File Offset: 0x001D5344
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		contentGameObject.GetComponent<LayoutElement>().minWidth = 530f;
	}
}
