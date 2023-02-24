using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A63 RID: 2659
public class CodexText : CodexWidget<CodexText>
{
	// Token: 0x17000602 RID: 1538
	// (get) Token: 0x0600515B RID: 20827 RVA: 0x001D6DC5 File Offset: 0x001D4FC5
	// (set) Token: 0x0600515C RID: 20828 RVA: 0x001D6DCD File Offset: 0x001D4FCD
	public string text { get; set; }

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x0600515D RID: 20829 RVA: 0x001D6DD6 File Offset: 0x001D4FD6
	// (set) Token: 0x0600515E RID: 20830 RVA: 0x001D6DDE File Offset: 0x001D4FDE
	public string messageID { get; set; }

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x0600515F RID: 20831 RVA: 0x001D6DE7 File Offset: 0x001D4FE7
	// (set) Token: 0x06005160 RID: 20832 RVA: 0x001D6DEF File Offset: 0x001D4FEF
	public CodexTextStyle style { get; set; }

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x06005162 RID: 20834 RVA: 0x001D6E0B File Offset: 0x001D500B
	// (set) Token: 0x06005161 RID: 20833 RVA: 0x001D6DF8 File Offset: 0x001D4FF8
	public string stringKey
	{
		get
		{
			return "--> " + (this.text ?? "NULL");
		}
		set
		{
			this.text = Strings.Get(value);
		}
	}

	// Token: 0x06005163 RID: 20835 RVA: 0x001D6E26 File Offset: 0x001D5026
	public CodexText()
	{
		this.style = CodexTextStyle.Body;
	}

	// Token: 0x06005164 RID: 20836 RVA: 0x001D6E35 File Offset: 0x001D5035
	public CodexText(string text, CodexTextStyle style = CodexTextStyle.Body, string id = null)
	{
		this.text = text;
		this.style = style;
		if (id != null)
		{
			this.messageID = id;
		}
	}

	// Token: 0x06005165 RID: 20837 RVA: 0x001D6E58 File Offset: 0x001D5058
	public void ConfigureLabel(LocText label, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		label.gameObject.SetActive(true);
		label.AllowLinks = this.style == CodexTextStyle.Body;
		label.textStyleSetting = textStyles[this.style];
		label.text = this.text;
		label.ApplySettings();
	}

	// Token: 0x06005166 RID: 20838 RVA: 0x001D6EA4 File Offset: 0x001D50A4
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureLabel(contentGameObject.GetComponent<LocText>(), textStyles);
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
