using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A69 RID: 2665
public class CodexLargeSpacer : CodexWidget<CodexLargeSpacer>
{
	// Token: 0x0600518F RID: 20879 RVA: 0x001D7277 File Offset: 0x001D5477
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
