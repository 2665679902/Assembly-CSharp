using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020009DB RID: 2523
public class InputModuleSwitch : MonoBehaviour
{
	// Token: 0x06004B62 RID: 19298 RVA: 0x001A6FE8 File Offset: 0x001A51E8
	private void Update()
	{
		if (this.lastMousePosition != Input.mousePosition && KInputManager.currentControllerIsGamepad)
		{
			KInputManager.currentControllerIsGamepad = false;
			KInputManager.InputChange.Invoke();
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			this.virtualInput.enabled = KInputManager.currentControllerIsGamepad;
			if (this.standaloneInput.enabled)
			{
				this.standaloneInput.enabled = false;
				this.virtualInput.forceModuleActive = true;
				this.ChangeInputHandler();
				return;
			}
		}
		else
		{
			this.lastMousePosition = Input.mousePosition;
			this.standaloneInput.enabled = true;
			if (this.virtualInput.enabled)
			{
				this.virtualInput.enabled = false;
				this.standaloneInput.forceModuleActive = true;
				this.ChangeInputHandler();
			}
		}
	}

	// Token: 0x06004B63 RID: 19299 RVA: 0x001A70A4 File Offset: 0x001A52A4
	private void ChangeInputHandler()
	{
		GameInputManager inputManager = Global.GetInputManager();
		for (int i = 0; i < inputManager.usedMenus.Count; i++)
		{
			if (inputManager.usedMenus[i].Equals(null))
			{
				inputManager.usedMenus.RemoveAt(i);
			}
		}
		if (inputManager.GetControllerCount() > 1)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				Cursor.visible = false;
				inputManager.GetController(1).inputHandler.TransferHandles(inputManager.GetController(0).inputHandler);
				return;
			}
			Cursor.visible = true;
			inputManager.GetController(0).inputHandler.TransferHandles(inputManager.GetController(1).inputHandler);
		}
	}

	// Token: 0x04003161 RID: 12641
	public VirtualInputModule virtualInput;

	// Token: 0x04003162 RID: 12642
	public StandaloneInputModule standaloneInput;

	// Token: 0x04003163 RID: 12643
	private Vector3 lastMousePosition;
}
