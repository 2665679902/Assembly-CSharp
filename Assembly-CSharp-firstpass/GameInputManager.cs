using System;
using System.Collections.Generic;

// Token: 0x02000027 RID: 39
public class GameInputManager : KInputManager
{
	// Token: 0x06000205 RID: 517 RVA: 0x0000BB90 File Offset: 0x00009D90
	public KInputController AddKeyboardMouseController()
	{
		KInputController kinputController = new KInputController(false);
		foreach (BindingEntry bindingEntry in GameInputMapping.GetBindingEntries())
		{
			kinputController.Bind(bindingEntry.mKeyCode, bindingEntry.mModifier, bindingEntry.mAction);
		}
		base.AddController(kinputController);
		return kinputController;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000BBE0 File Offset: 0x00009DE0
	public KInputController AddGamepadController(int gamepad_index)
	{
		KInputController kinputController = new KInputController(true);
		foreach (BindingEntry bindingEntry in GameInputMapping.GetBindingEntries())
		{
			kinputController.Bind(bindingEntry.mKeyCode, Modifier.None, bindingEntry.mAction);
		}
		base.AddController(kinputController);
		return kinputController;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000BC2C File Offset: 0x00009E2C
	public GameInputManager(BindingEntry[] default_keybindings)
	{
		GameInputMapping.SetDefaultKeyBindings(default_keybindings);
		GameInputMapping.LoadBindings();
		this.AddKeyboardMouseController();
		KInputManager.steamInputInterpreter.OnEnable();
		if (KInputManager.steamInputInterpreter.NumOfISteamInputs > 0)
		{
			this.AddGamepadController(base.GetControllerCount());
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000BC80 File Offset: 0x00009E80
	public void RebindControls()
	{
		foreach (KInputController kinputController in this.mControllers)
		{
			kinputController.ClearBindings();
			foreach (BindingEntry bindingEntry in GameInputMapping.GetBindingEntries())
			{
				kinputController.Bind(bindingEntry.mKeyCode, bindingEntry.mModifier, bindingEntry.mAction);
			}
			kinputController.HandleCancelInput();
		}
		KInputManager.InputChange.Invoke();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000BD1C File Offset: 0x00009F1C
	public override void Update()
	{
		if (KInputManager.isFocused)
		{
			KInputManager.steamInputInterpreter.Update();
			if (KInputManager.steamInputInterpreter.NumOfISteamInputs > 0 && base.GetControllerCount() <= 1)
			{
				this.AddGamepadController(base.GetControllerCount());
			}
			else if (KInputManager.steamInputInterpreter.NumOfISteamInputs < 1 && KInputManager.currentControllerIsGamepad)
			{
				KInputManager.currentControllerIsGamepad = false;
				KInputManager.InputChange.Invoke();
			}
			int num = 0;
			while (num < this.mControllers.Count && num + 1 < this.mControllers.Count)
			{
				if (this.mControllers[num].inputHandler.HandleChildCount() != this.mControllers[num + 1].inputHandler.HandleChildCount())
				{
					this.mControllers[num].inputHandler.TransferHandles(this.mControllers[num + 1].inputHandler);
				}
				num++;
			}
			base.Update();
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000BE0B File Offset: 0x0000A00B
	public override void OnApplicationFocus(bool focusStatus)
	{
		base.OnApplicationFocus(focusStatus);
	}

	// Token: 0x040001EB RID: 491
	public List<IInputHandler> usedMenus = new List<IInputHandler>();
}
