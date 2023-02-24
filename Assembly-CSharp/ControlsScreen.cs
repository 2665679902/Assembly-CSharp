using System;
using UnityEngine.UI;

// Token: 0x02000A7A RID: 2682
public class ControlsScreen : KScreen
{
	// Token: 0x06005219 RID: 21017 RVA: 0x001DA714 File Offset: 0x001D8914
	protected override void OnPrefabInit()
	{
		BindingEntry[] bindingEntries = GameInputMapping.GetBindingEntries();
		string text = "";
		foreach (BindingEntry bindingEntry in bindingEntries)
		{
			text += bindingEntry.mAction.ToString();
			text += ": ";
			text += bindingEntry.mKeyCode.ToString();
			text += "\n";
		}
		this.controlLabel.text = text;
	}

	// Token: 0x0600521A RID: 21018 RVA: 0x001DA799 File Offset: 0x001D8999
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Help) || e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
		}
	}

	// Token: 0x04003759 RID: 14169
	public Text controlLabel;
}
