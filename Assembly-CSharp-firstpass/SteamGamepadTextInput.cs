using System;
using Steamworks;

// Token: 0x02000038 RID: 56
public class SteamGamepadTextInput
{
	// Token: 0x06000268 RID: 616 RVA: 0x0000D174 File Offset: 0x0000B374
	public static bool IsActive()
	{
		return KInputManager.steamInputInterpreter.Initialized && (KInputManager.currentControllerIsGamepad || SteamUtils.IsSteamRunningOnSteamDeck());
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000D194 File Offset: 0x0000B394
	public static void ShowTextInputScreen(string desc, string init, Action<SteamGamepadTextInputData> action)
	{
		DebugUtil.DevAssert(!SteamGamepadTextInput.active, "Gamepad input already active.", null);
		if (SteamUtils.ShowGamepadTextInput(EGamepadTextInputMode.k_EGamepadTextInputModeNormal, EGamepadTextInputLineMode.k_EGamepadTextInputLineModeSingleLine, desc, 512U, init))
		{
			SteamGamepadTextInput.GamepadInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate(SteamGamepadTextInput.OnGamepadInputDismissed));
			SteamGamepadTextInput.action = action;
			SteamGamepadTextInput.active = true;
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
	private static void OnGamepadInputDismissed(GamepadTextInputDismissed_t callback)
	{
		SteamGamepadTextInputData steamGamepadTextInputData = default(SteamGamepadTextInputData);
		steamGamepadTextInputData.submitted = false;
		steamGamepadTextInputData.input = "";
		if (callback.m_bSubmitted)
		{
			steamGamepadTextInputData.submitted = true;
			string text;
			if (SteamUtils.GetEnteredGamepadTextInput(out text, callback.m_unSubmittedText) && text != null)
			{
				steamGamepadTextInputData.input = text;
			}
		}
		SteamGamepadTextInput.GamepadInputDismissed.Dispose();
		SteamGamepadTextInput.active = false;
		SteamGamepadTextInput.action(steamGamepadTextInputData);
	}

	// Token: 0x0400033F RID: 831
	private static bool active;

	// Token: 0x04000340 RID: 832
	private static Action<SteamGamepadTextInputData> action;

	// Token: 0x04000341 RID: 833
	private static Callback<GamepadTextInputDismissed_t> GamepadInputDismissed;
}
