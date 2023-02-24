using System;
using TMPro;

// Token: 0x02000035 RID: 53
public class KInputTextField : TMP_InputField
{
	// Token: 0x06000265 RID: 613 RVA: 0x0000D108 File Offset: 0x0000B308
	private KInputTextField()
	{
		this.onFocus = (System.Action)Delegate.Combine(this.onFocus, new System.Action(delegate
		{
			if (SteamGamepadTextInput.IsActive())
			{
				SteamGamepadTextInput.ShowTextInputScreen("", base.text, new Action<SteamGamepadTextInputData>(this.OnGamepadInputDismissed));
			}
		}));
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000D132 File Offset: 0x0000B332
	private void OnGamepadInputDismissed(SteamGamepadTextInputData data)
	{
		if (data.submitted)
		{
			base.text = data.input;
		}
		base.OnDeselect(null);
	}
}
