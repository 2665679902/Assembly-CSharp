using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class KInputController : IInputHandler
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600020C RID: 524 RVA: 0x0000BE31 File Offset: 0x0000A031
	public string handlerName
	{
		get
		{
			return "KInputController";
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600020D RID: 525 RVA: 0x0000BE38 File Offset: 0x0000A038
	// (set) Token: 0x0600020E RID: 526 RVA: 0x0000BE40 File Offset: 0x0000A040
	public KInputHandler inputHandler { get; set; }

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600020F RID: 527 RVA: 0x0000BE49 File Offset: 0x0000A049
	// (set) Token: 0x06000210 RID: 528 RVA: 0x0000BE51 File Offset: 0x0000A051
	public bool IsGamepad { get; private set; }

	// Token: 0x06000211 RID: 529 RVA: 0x0000BE5C File Offset: 0x0000A05C
	public KInputController(bool is_gamepad)
	{
		this.mBindings = new List<KInputBinding>();
		this.mEvents = new List<KInputEvent>();
		this.mDirtyBindings = false;
		this.IsGamepad = is_gamepad;
		this.mAxis = new float[4];
		this.mActiveModifiers = Modifier.None;
		this.mActionState = new bool[277];
		this.mScrollState = new bool[2];
		this.inputHandler = new KInputHandler(this, this);
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000BEE6 File Offset: 0x0000A0E6
	public void ClearBindings()
	{
		this.mBindings.Clear();
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000BEF3 File Offset: 0x0000A0F3
	public void Bind(KKeyCode key_code, Modifier modifier, global::Action action)
	{
		this.mBindings.Add(new KInputBinding(key_code, modifier, action));
		this.mDirtyBindings = true;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000BF0F File Offset: 0x0000A10F
	public void QueueButtonEvent(KInputController.KeyDef key_def, bool is_down)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		this.QueueButtonEvent_Internal(key_def, is_down);
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000BF24 File Offset: 0x0000A124
	private void QueueButtonEvent_Internal(KInputController.KeyDef key_def, bool is_down)
	{
		bool[] mActionFlags = key_def.mActionFlags;
		key_def.mIsDown = is_down;
		InputEventType inputEventType = (is_down ? InputEventType.KeyDown : InputEventType.KeyUp);
		for (int i = 0; i < mActionFlags.Length; i++)
		{
			if (mActionFlags[i])
			{
				this.mActionState[i] = is_down;
			}
		}
		KButtonEvent kbuttonEvent = new KButtonEvent(this, inputEventType, mActionFlags);
		this.mEvents.Add(kbuttonEvent);
		KInputManager.SetUserActive();
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000BF80 File Offset: 0x0000A180
	public void QueueControllerEvent(global::Action action, bool is_down)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		InputEventType inputEventType = (is_down ? InputEventType.KeyDown : InputEventType.KeyUp);
		this.mActionState[(int)action] = is_down;
		KButtonEvent kbuttonEvent = new KButtonEvent(this, inputEventType, action);
		this.mEvents.Add(kbuttonEvent);
		KInputManager.SetUserActive();
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
	private void GenerateActionFlagTable()
	{
		this.mKeyDefLookup.Clear();
		foreach (KInputBinding kinputBinding in this.mBindings)
		{
			KInputController.KeyDefEntry keyDefEntry = new KInputController.KeyDefEntry(kinputBinding.mKeyCode, kinputBinding.mModifier);
			KInputController.KeyDef keyDef = null;
			if (!this.mKeyDefLookup.TryGetValue(keyDefEntry, out keyDef))
			{
				keyDef = new KInputController.KeyDef(kinputBinding.mKeyCode, kinputBinding.mModifier);
				this.mKeyDefLookup[keyDefEntry] = keyDef;
			}
			keyDef.mActionFlags[(int)kinputBinding.mAction] = true;
		}
		this.mKeyDefs = new KInputController.KeyDef[this.mKeyDefLookup.Count];
		this.mKeyDefLookup.Values.CopyTo(this.mKeyDefs, 0);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000C098 File Offset: 0x0000A298
	public bool GetKeyDown(KKeyCode key_code)
	{
		bool flag = false;
		if (key_code < KKeyCode.KleiKeys)
		{
			flag = Input.GetKeyDown((KeyCode)key_code);
		}
		else if (key_code != KKeyCode.MouseScrollDown)
		{
			if (key_code == KKeyCode.MouseScrollUp)
			{
				flag = this.mScrollState[0];
			}
		}
		else
		{
			flag = this.mScrollState[1];
		}
		return flag;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
	public bool GetKeyUp(KKeyCode key_code)
	{
		return key_code < KKeyCode.KleiKeys && Input.GetKeyUp((KeyCode)key_code);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000C104 File Offset: 0x0000A304
	public void CheckModifier(KKeyCode[] key_codes, Modifier modifier)
	{
		this.mActiveModifiers &= ~modifier;
		foreach (KKeyCode kkeyCode in key_codes)
		{
			if (this.GetKeyDown(kkeyCode) || Input.GetKey((KeyCode)kkeyCode))
			{
				this.mActiveModifiers |= modifier;
				return;
			}
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000C154 File Offset: 0x0000A354
	private void UpdateAxis()
	{
		this.mAxis[2] = Input.GetAxis("Mouse X");
		this.mAxis[3] = Input.GetAxis("Mouse Y");
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000C17A File Offset: 0x0000A37A
	private void UpdateModifiers()
	{
		this.CheckModifier(KInputController.altCodes, Modifier.Alt);
		this.CheckModifier(KInputController.ctrlCodes, Modifier.Ctrl);
		this.CheckModifier(KInputController.shiftCodes, Modifier.Shift);
		this.CheckModifier(KInputController.capsCodes, Modifier.CapsLock);
		this.CheckModifier(KInputController.backtickCodes, Modifier.Backtick);
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000C1BC File Offset: 0x0000A3BC
	private void UpdateScrollStates()
	{
		float axis = Input.GetAxis("Mouse ScrollWheel");
		this.mScrollState[1] = axis < 0f;
		this.mScrollState[0] = axis > 0f;
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
	public void ToggleKeyboard(bool active)
	{
		this.mIgnoreKeyboard = active;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000C1FD File Offset: 0x0000A3FD
	public void ToggleMouse(bool active)
	{
		this.mIgnoreMouse = active;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000C208 File Offset: 0x0000A408
	public void Update()
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		if (this.mDirtyBindings)
		{
			this.GenerateActionFlagTable();
			this.mDirtyBindings = false;
		}
		if (!this.IsGamepad)
		{
			this.UpdateScrollStates();
			this.UpdateAxis();
			this.UpdateModifiers();
			foreach (KInputController.KeyDef keyDef in this.mKeyDefs)
			{
				int mKeyCode = (int)keyDef.mKeyCode;
				if ((!this.mIgnoreKeyboard || mKeyCode >= 323) && (!this.mIgnoreMouse || ((mKeyCode < 323 || mKeyCode >= 330) && mKeyCode != 1001 && mKeyCode != 1002)))
				{
					if ((!keyDef.mIsDown || keyDef.mKeyCode == KKeyCode.MouseScrollDown || keyDef.mKeyCode == KKeyCode.MouseScrollUp) && this.GetKeyDown(keyDef.mKeyCode) && this.mActiveModifiers == keyDef.mModifier)
					{
						this.QueueButtonEvent(keyDef, true);
					}
					if (keyDef.mIsDown && this.GetKeyUp(keyDef.mKeyCode))
					{
						this.QueueButtonEvent(keyDef, false);
					}
				}
			}
			return;
		}
		for (int j = 0; j < 277; j++)
		{
			global::Action action = (global::Action)j;
			bool steamInputActionIsDown = KInputManager.steamInputInterpreter.GetSteamInputActionIsDown(action);
			if (this.mActionState[j] != steamInputActionIsDown)
			{
				this.QueueControllerEvent(action, steamInputActionIsDown);
			}
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000C35C File Offset: 0x0000A55C
	public void Dispatch()
	{
		foreach (KInputEvent kinputEvent in this.mEvents)
		{
			this.inputHandler.HandleEvent(kinputEvent);
			KInputManager.currentController = kinputEvent.Controller;
			KInputManager.currentControllerIsGamepad = kinputEvent.Controller.IsGamepad;
		}
		if (KInputManager.currentController != KInputManager.prevController)
		{
			KInputManager.InputChange.Invoke();
		}
		KInputManager.prevController = KInputManager.currentController;
		this.mEvents.Clear();
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000C3FC File Offset: 0x0000A5FC
	public bool IsActive(global::Action action)
	{
		return this.mActionState[(int)action];
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000C406 File Offset: 0x0000A606
	public float GetAxis(Axis axis)
	{
		return this.mAxis[(int)axis];
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000C410 File Offset: 0x0000A610
	public void HandleCancelInput()
	{
		if (this.IsGamepad)
		{
			return;
		}
		foreach (KInputController.KeyDef keyDef in this.mKeyDefs)
		{
			if (keyDef.mIsDown && keyDef.mKeyCode < KKeyCode.KleiKeys && !Input.GetKey((KeyCode)keyDef.mKeyCode))
			{
				this.QueueButtonEvent_Internal(keyDef, false);
			}
		}
		this.UpdateModifiers();
		this.inputHandler.HandleCancelInput();
		this.Dispatch();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000C480 File Offset: 0x0000A680
	public KKeyCode GetInputForAction(global::Action action)
	{
		foreach (KInputBinding kinputBinding in this.mBindings)
		{
			if (kinputBinding.mAction == action)
			{
				return kinputBinding.mKeyCode;
			}
		}
		return KKeyCode.None;
	}

	// Token: 0x040001F0 RID: 496
	private List<KInputBinding> mBindings;

	// Token: 0x040001F1 RID: 497
	private List<KInputEvent> mEvents;

	// Token: 0x040001F2 RID: 498
	private KInputController.KeyDef[] mKeyDefs = new KInputController.KeyDef[0];

	// Token: 0x040001F3 RID: 499
	private bool mDirtyBindings;

	// Token: 0x040001F4 RID: 500
	private float[] mAxis;

	// Token: 0x040001F5 RID: 501
	private Modifier mActiveModifiers;

	// Token: 0x040001F6 RID: 502
	private bool[] mActionState;

	// Token: 0x040001F7 RID: 503
	private bool[] mScrollState;

	// Token: 0x040001F8 RID: 504
	private bool mIgnoreKeyboard;

	// Token: 0x040001F9 RID: 505
	private bool mIgnoreMouse;

	// Token: 0x040001FA RID: 506
	private Dictionary<KInputController.KeyDefEntry, KInputController.KeyDef> mKeyDefLookup = new Dictionary<KInputController.KeyDefEntry, KInputController.KeyDef>();

	// Token: 0x040001FC RID: 508
	private static readonly KKeyCode[] altCodes = new KKeyCode[]
	{
		KKeyCode.LeftAlt,
		KKeyCode.RightAlt
	};

	// Token: 0x040001FD RID: 509
	private static readonly KKeyCode[] ctrlCodes = new KKeyCode[]
	{
		KKeyCode.LeftControl,
		KKeyCode.RightControl
	};

	// Token: 0x040001FE RID: 510
	private static readonly KKeyCode[] shiftCodes = new KKeyCode[]
	{
		KKeyCode.LeftShift,
		KKeyCode.RightShift
	};

	// Token: 0x040001FF RID: 511
	private static readonly KKeyCode[] capsCodes = new KKeyCode[] { KKeyCode.CapsLock };

	// Token: 0x04000200 RID: 512
	private static readonly KKeyCode[] backtickCodes = new KKeyCode[] { KKeyCode.BackQuote };

	// Token: 0x0200097C RID: 2428
	private enum Scroll
	{
		// Token: 0x040020ED RID: 8429
		Up,
		// Token: 0x040020EE RID: 8430
		Down,
		// Token: 0x040020EF RID: 8431
		NumStates
	}

	// Token: 0x0200097D RID: 2429
	public struct KeyDefEntry
	{
		// Token: 0x060052EA RID: 21226 RVA: 0x0009B309 File Offset: 0x00099509
		public KeyDefEntry(KKeyCode key_code, Modifier modifier)
		{
			this.mKeyCode = key_code;
			this.mModifier = modifier;
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0009B319 File Offset: 0x00099519
		private void Print()
		{
			global::Debug.Log(this.mKeyCode.ToString() + this.mModifier.ToString());
		}

		// Token: 0x040020F0 RID: 8432
		private KKeyCode mKeyCode;

		// Token: 0x040020F1 RID: 8433
		private Modifier mModifier;
	}

	// Token: 0x0200097E RID: 2430
	[DebuggerDisplay("Key: {mKeyCode} Mod: {mModifier}")]
	public class KeyDef
	{
		// Token: 0x060052EC RID: 21228 RVA: 0x0009B347 File Offset: 0x00099547
		public KeyDef(KKeyCode key_code, Modifier modifier)
		{
			this.mKeyCode = key_code;
			this.mModifier = modifier;
			this.mActionFlags = new bool[277];
		}

		// Token: 0x040020F2 RID: 8434
		public KKeyCode mKeyCode;

		// Token: 0x040020F3 RID: 8435
		public Modifier mModifier;

		// Token: 0x040020F4 RID: 8436
		public bool[] mActionFlags;

		// Token: 0x040020F5 RID: 8437
		public bool mIsDown;
	}
}
