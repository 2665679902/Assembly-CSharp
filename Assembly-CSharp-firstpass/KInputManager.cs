using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000034 RID: 52
public class KInputManager
{
	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000257 RID: 599 RVA: 0x0000CF12 File Offset: 0x0000B112
	public static bool isFocused
	{
		get
		{
			return KInputManager.hasFocus && !KInputManager.devToolFocus;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000258 RID: 600 RVA: 0x0000CF25 File Offset: 0x0000B125
	// (set) Token: 0x06000259 RID: 601 RVA: 0x0000CF2C File Offset: 0x0000B12C
	public static long lastUserActionTicks { get; private set; }

	// Token: 0x0600025A RID: 602 RVA: 0x0000CF34 File Offset: 0x0000B134
	public static void SetUserActive()
	{
		if (KInputManager.isFocused)
		{
			KInputManager.lastUserActionTicks = DateTime.Now.Ticks;
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000CF5C File Offset: 0x0000B15C
	public KInputManager()
	{
		KInputManager.lastUserActionTicks = DateTime.Now.Ticks;
		KInputManager.hasFocus = true;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000CF92 File Offset: 0x0000B192
	public void AddController(KInputController controller)
	{
		this.mControllers.Add(controller);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
	public KInputController GetController(int controller_index)
	{
		DebugUtil.Assert(controller_index < this.mControllers.Count);
		return this.mControllers[controller_index];
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000CFC1 File Offset: 0x0000B1C1
	public int GetControllerCount()
	{
		return this.mControllers.Count;
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000CFCE File Offset: 0x0000B1CE
	public KInputController GetDefaultController()
	{
		return this.GetController(0);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
	public virtual void Update()
	{
		if (KInputManager.isFocused)
		{
			for (int i = 0; i < this.mControllers.Count; i++)
			{
				this.mControllers[i].Update();
			}
			this.Dispatch();
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000D01C File Offset: 0x0000B21C
	public virtual void Dispatch()
	{
		if (KInputManager.isFocused)
		{
			for (int i = 0; i < this.mControllers.Count; i++)
			{
				this.mControllers[i].Dispatch();
			}
		}
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000D058 File Offset: 0x0000B258
	public virtual void OnApplicationFocus(bool focus)
	{
		KInputManager.hasFocus = focus;
		KInputManager.SetUserActive();
		if (!KInputManager.isFocused)
		{
			Input.ResetInputAxes();
			foreach (KInputController kinputController in this.mControllers)
			{
				kinputController.HandleCancelInput();
			}
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
	public static Vector3 GetMousePos()
	{
		if (KInputManager.isMousePosLocked)
		{
			return KInputManager.lockedMousePos;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			return KInputManager.virtualCursorPos;
		}
		return Input.mousePosition;
	}

	// Token: 0x0400023C RID: 572
	protected List<KInputController> mControllers = new List<KInputController>();

	// Token: 0x0400023D RID: 573
	private static bool hasFocus = false;

	// Token: 0x0400023E RID: 574
	public static bool devToolFocus = false;

	// Token: 0x04000240 RID: 576
	public static SteamInputInterpreter steamInputInterpreter = new SteamInputInterpreter();

	// Token: 0x04000241 RID: 577
	public static Vector3F virtualCursorPos;

	// Token: 0x04000242 RID: 578
	public static bool currentControllerIsGamepad;

	// Token: 0x04000243 RID: 579
	public static KInputController prevController;

	// Token: 0x04000244 RID: 580
	public static KInputController currentController;

	// Token: 0x04000245 RID: 581
	public static UnityEvent InputChange = new UnityEvent();

	// Token: 0x04000246 RID: 582
	public static bool isMousePosLocked;

	// Token: 0x04000247 RID: 583
	public static Vector3 lockedMousePos;
}
