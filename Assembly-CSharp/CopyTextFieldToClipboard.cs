using System;
using UnityEngine;

// Token: 0x02000C29 RID: 3113
[AddComponentMenu("KMonoBehaviour/scripts/CopyTextFieldToClipboard")]
public class CopyTextFieldToClipboard : KMonoBehaviour
{
	// Token: 0x06006286 RID: 25222 RVA: 0x00245C28 File Offset: 0x00243E28
	protected override void OnPrefabInit()
	{
		this.button.onClick += this.OnClick;
	}

	// Token: 0x06006287 RID: 25223 RVA: 0x00245C41 File Offset: 0x00243E41
	private void OnClick()
	{
		TextEditor textEditor = new TextEditor();
		textEditor.text = this.GetText();
		textEditor.SelectAll();
		textEditor.Copy();
	}

	// Token: 0x0400443C RID: 17468
	public KButton button;

	// Token: 0x0400443D RID: 17469
	public Func<string> GetText;
}
