using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A60 RID: 2656
public interface ICodexWidget
{
	// Token: 0x06005148 RID: 20808
	void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles);
}
