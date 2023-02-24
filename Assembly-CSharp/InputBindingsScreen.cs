using System;
using System.Collections.Generic;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AC0 RID: 2752
public class InputBindingsScreen : KModalScreen
{
	// Token: 0x0600541E RID: 21534 RVA: 0x001E88EB File Offset: 0x001E6AEB
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600541F RID: 21535 RVA: 0x001E88EE File Offset: 0x001E6AEE
	private bool IsKeyDown(KeyCode key_code)
	{
		return Input.GetKey(key_code) || Input.GetKeyDown(key_code);
	}

	// Token: 0x06005420 RID: 21536 RVA: 0x001E8900 File Offset: 0x001E6B00
	private string GetModifierString(Modifier modifiers)
	{
		string text = "";
		foreach (object obj in Enum.GetValues(typeof(Modifier)))
		{
			Modifier modifier = (Modifier)obj;
			if ((modifiers & modifier) != Modifier.None)
			{
				text = text + " + " + modifier.ToString();
			}
		}
		return text;
	}

	// Token: 0x06005421 RID: 21537 RVA: 0x001E8980 File Offset: 0x001E6B80
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.entryPrefab.SetActive(false);
		this.prevScreenButton.onClick += this.OnPrevScreen;
		this.nextScreenButton.onClick += this.OnNextScreen;
	}

	// Token: 0x06005422 RID: 21538 RVA: 0x001E89D0 File Offset: 0x001E6BD0
	protected override void OnActivate()
	{
		this.CollectScreens();
		string text = this.screens[this.activeScreen];
		string text2 = "STRINGS.INPUT_BINDINGS." + text.ToUpper() + ".NAME";
		this.screenTitle.text = Strings.Get(text2);
		this.closeButton.onClick += this.OnBack;
		this.backButton.onClick += this.OnBack;
		this.resetButton.onClick += this.OnReset;
		this.BuildDisplay();
	}

	// Token: 0x06005423 RID: 21539 RVA: 0x001E8A6C File Offset: 0x001E6C6C
	private void CollectScreens()
	{
		this.screens.Clear();
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry = GameInputMapping.KeyBindings[i];
			if (bindingEntry.mGroup != null && bindingEntry.mRebindable && !this.screens.Contains(bindingEntry.mGroup) && DlcManager.IsDlcListValidForCurrentContent(bindingEntry.dlcIds))
			{
				if (bindingEntry.mGroup == "Root")
				{
					this.activeScreen = this.screens.Count;
				}
				this.screens.Add(bindingEntry.mGroup);
			}
		}
	}

	// Token: 0x06005424 RID: 21540 RVA: 0x001E8B06 File Offset: 0x001E6D06
	protected override void OnDeactivate()
	{
		GameInputMapping.SaveBindings();
		this.DestroyDisplay();
	}

	// Token: 0x06005425 RID: 21541 RVA: 0x001E8B13 File Offset: 0x001E6D13
	private LocString GetActionString(global::Action action)
	{
		return null;
	}

	// Token: 0x06005426 RID: 21542 RVA: 0x001E8B18 File Offset: 0x001E6D18
	private string GetBindingText(BindingEntry binding)
	{
		string text = GameUtil.GetKeycodeLocalized(binding.mKeyCode);
		if (binding.mKeyCode != KKeyCode.LeftAlt && binding.mKeyCode != KKeyCode.RightAlt && binding.mKeyCode != KKeyCode.LeftControl && binding.mKeyCode != KKeyCode.RightControl && binding.mKeyCode != KKeyCode.LeftShift && binding.mKeyCode != KKeyCode.RightShift)
		{
			text += this.GetModifierString(binding.mModifier);
		}
		return text;
	}

	// Token: 0x06005427 RID: 21543 RVA: 0x001E8B94 File Offset: 0x001E6D94
	private void BuildDisplay()
	{
		string text = this.screens[this.activeScreen];
		string text2 = "STRINGS.INPUT_BINDINGS." + text.ToUpper() + ".NAME";
		this.screenTitle.text = Strings.Get(text2);
		if (this.entryPool == null)
		{
			this.entryPool = new UIPool<HorizontalLayoutGroup>(this.entryPrefab.GetComponent<HorizontalLayoutGroup>());
		}
		this.DestroyDisplay();
		int num = 0;
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry binding = GameInputMapping.KeyBindings[i];
			if (binding.mGroup == this.screens[this.activeScreen] && binding.mRebindable && DlcManager.IsDlcListValidForCurrentContent(binding.dlcIds))
			{
				GameObject gameObject = this.entryPool.GetFreeElement(this.parent, true).gameObject;
				TMP_Text componentInChildren = gameObject.transform.GetChild(0).GetComponentInChildren<LocText>();
				string text3 = "STRINGS.INPUT_BINDINGS." + binding.mGroup.ToUpper() + "." + binding.mAction.ToString().ToUpper();
				componentInChildren.text = Strings.Get(text3);
				LocText key_label = gameObject.transform.GetChild(1).GetComponentInChildren<LocText>();
				key_label.text = this.GetBindingText(binding);
				KButton button = gameObject.GetComponentInChildren<KButton>();
				button.onClick += delegate
				{
					this.waitingForKeyPress = true;
					this.actionToRebind = binding.mAction;
					this.ignoreRootConflicts = binding.mIgnoreRootConflics;
					this.activeButton = button;
					key_label.text = UI.FRONTEND.INPUT_BINDINGS_SCREEN.WAITING_FOR_INPUT;
				};
				gameObject.transform.SetSiblingIndex(num);
				num++;
			}
		}
	}

	// Token: 0x06005428 RID: 21544 RVA: 0x001E8D70 File Offset: 0x001E6F70
	private void DestroyDisplay()
	{
		this.entryPool.ClearAll();
	}

	// Token: 0x06005429 RID: 21545 RVA: 0x001E8D80 File Offset: 0x001E6F80
	private void Update()
	{
		if (this.waitingForKeyPress)
		{
			Modifier modifier = Modifier.None;
			modifier |= ((this.IsKeyDown(KeyCode.LeftAlt) || this.IsKeyDown(KeyCode.RightAlt)) ? Modifier.Alt : Modifier.None);
			modifier |= ((this.IsKeyDown(KeyCode.LeftControl) || this.IsKeyDown(KeyCode.RightControl)) ? Modifier.Ctrl : Modifier.None);
			modifier |= ((this.IsKeyDown(KeyCode.LeftShift) || this.IsKeyDown(KeyCode.RightShift)) ? Modifier.Shift : Modifier.None);
			modifier |= (this.IsKeyDown(KeyCode.CapsLock) ? Modifier.CapsLock : Modifier.None);
			modifier |= (this.IsKeyDown(KeyCode.BackQuote) ? Modifier.Backtick : Modifier.None);
			bool flag = false;
			for (int i = 0; i < InputBindingsScreen.validKeys.Length; i++)
			{
				KeyCode keyCode = InputBindingsScreen.validKeys[i];
				if (Input.GetKeyDown(keyCode))
				{
					KKeyCode kkeyCode = (KKeyCode)keyCode;
					this.Bind(kkeyCode, modifier);
					flag = true;
				}
			}
			if (!flag)
			{
				float axis = Input.GetAxis("Mouse ScrollWheel");
				KKeyCode kkeyCode2 = KKeyCode.None;
				if (axis < 0f)
				{
					kkeyCode2 = KKeyCode.MouseScrollDown;
				}
				else if (axis > 0f)
				{
					kkeyCode2 = KKeyCode.MouseScrollUp;
				}
				if (kkeyCode2 != KKeyCode.None)
				{
					this.Bind(kkeyCode2, modifier);
				}
			}
		}
	}

	// Token: 0x0600542A RID: 21546 RVA: 0x001E8E98 File Offset: 0x001E7098
	private BindingEntry GetDuplicatedBinding(string activeScreen, BindingEntry new_binding)
	{
		BindingEntry bindingEntry = default(BindingEntry);
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry2 = GameInputMapping.KeyBindings[i];
			if (new_binding.IsBindingEqual(bindingEntry2) && (bindingEntry2.mGroup == null || bindingEntry2.mGroup == activeScreen || bindingEntry2.mGroup == "Root" || activeScreen == "Root") && (!(activeScreen == "Root") || !bindingEntry2.mIgnoreRootConflics) && (!(bindingEntry2.mGroup == "Root") || !new_binding.mIgnoreRootConflics))
			{
				bindingEntry = bindingEntry2;
				break;
			}
		}
		return bindingEntry;
	}

	// Token: 0x0600542B RID: 21547 RVA: 0x001E8F44 File Offset: 0x001E7144
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.waitingForKeyPress)
		{
			e.Consumed = true;
			return;
		}
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600542C RID: 21548 RVA: 0x001E8F76 File Offset: 0x001E7176
	public override void OnKeyUp(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x0600542D RID: 21549 RVA: 0x001E8F80 File Offset: 0x001E7180
	private void OnBack()
	{
		int num = this.NumUnboundActions();
		if (num == 0)
		{
			this.Deactivate();
			return;
		}
		string text;
		if (num == 1)
		{
			BindingEntry firstUnbound = this.GetFirstUnbound();
			text = string.Format(UI.FRONTEND.INPUT_BINDINGS_SCREEN.UNBOUND_ACTION, firstUnbound.mAction.ToString());
		}
		else
		{
			text = UI.FRONTEND.INPUT_BINDINGS_SCREEN.MULTIPLE_UNBOUND_ACTIONS;
		}
		this.confirmDialog = Util.KInstantiateUI(this.confirmPrefab.gameObject, base.transform.gameObject, false).GetComponent<ConfirmDialogScreen>();
		this.confirmDialog.PopupConfirmDialog(text, delegate
		{
			this.Deactivate();
		}, delegate
		{
			this.confirmDialog.Deactivate();
		}, null, null, null, null, null, null);
		this.confirmDialog.gameObject.SetActive(true);
	}

	// Token: 0x0600542E RID: 21550 RVA: 0x001E903C File Offset: 0x001E723C
	private int NumUnboundActions()
	{
		int num = 0;
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry = GameInputMapping.KeyBindings[i];
			if (bindingEntry.mKeyCode == KKeyCode.None && bindingEntry.mRebindable && (BuildMenu.UseHotkeyBuildMenu() || !bindingEntry.mIgnoreRootConflics))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600542F RID: 21551 RVA: 0x001E9090 File Offset: 0x001E7290
	private BindingEntry GetFirstUnbound()
	{
		BindingEntry bindingEntry = default(BindingEntry);
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry2 = GameInputMapping.KeyBindings[i];
			if (bindingEntry2.mKeyCode == KKeyCode.None)
			{
				bindingEntry = bindingEntry2;
				break;
			}
		}
		return bindingEntry;
	}

	// Token: 0x06005430 RID: 21552 RVA: 0x001E90D0 File Offset: 0x001E72D0
	private void OnReset()
	{
		GameInputMapping.KeyBindings = (BindingEntry[])GameInputMapping.DefaultBindings.Clone();
		Global.GetInputManager().RebindControls();
		this.BuildDisplay();
	}

	// Token: 0x06005431 RID: 21553 RVA: 0x001E90F6 File Offset: 0x001E72F6
	public void OnPrevScreen()
	{
		if (this.activeScreen > 0)
		{
			this.activeScreen--;
		}
		else
		{
			this.activeScreen = this.screens.Count - 1;
		}
		this.BuildDisplay();
	}

	// Token: 0x06005432 RID: 21554 RVA: 0x001E912A File Offset: 0x001E732A
	public void OnNextScreen()
	{
		if (this.activeScreen < this.screens.Count - 1)
		{
			this.activeScreen++;
		}
		else
		{
			this.activeScreen = 0;
		}
		this.BuildDisplay();
	}

	// Token: 0x06005433 RID: 21555 RVA: 0x001E9160 File Offset: 0x001E7360
	private void Bind(KKeyCode kkey_code, Modifier modifier)
	{
		BindingEntry bindingEntry = new BindingEntry(this.screens[this.activeScreen], GamepadButton.NumButtons, kkey_code, modifier, this.actionToRebind, true, this.ignoreRootConflicts);
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry2 = GameInputMapping.KeyBindings[i];
			if (bindingEntry2.mRebindable && bindingEntry2.mAction == this.actionToRebind)
			{
				BindingEntry duplicatedBinding = this.GetDuplicatedBinding(this.screens[this.activeScreen], bindingEntry);
				bindingEntry.mButton = GameInputMapping.KeyBindings[i].mButton;
				GameInputMapping.KeyBindings[i] = bindingEntry;
				this.activeButton.GetComponentInChildren<LocText>().text = this.GetBindingText(bindingEntry);
				if (duplicatedBinding.mAction != global::Action.Invalid && duplicatedBinding.mAction != this.actionToRebind)
				{
					this.confirmDialog = Util.KInstantiateUI(this.confirmPrefab.gameObject, base.transform.gameObject, false).GetComponent<ConfirmDialogScreen>();
					string text = Strings.Get("STRINGS.INPUT_BINDINGS." + duplicatedBinding.mGroup.ToUpper() + "." + duplicatedBinding.mAction.ToString().ToUpper());
					string bindingText = this.GetBindingText(duplicatedBinding);
					string text2 = string.Format(UI.FRONTEND.INPUT_BINDINGS_SCREEN.DUPLICATE, text, bindingText);
					this.Unbind(duplicatedBinding.mAction);
					this.confirmDialog.PopupConfirmDialog(text2, null, null, null, null, null, null, null, null);
					this.confirmDialog.gameObject.SetActive(true);
				}
				Global.GetInputManager().RebindControls();
				this.waitingForKeyPress = false;
				this.actionToRebind = global::Action.NumActions;
				this.activeButton = null;
				this.BuildDisplay();
				return;
			}
		}
	}

	// Token: 0x06005434 RID: 21556 RVA: 0x001E9324 File Offset: 0x001E7524
	private void Unbind(global::Action action)
	{
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry = GameInputMapping.KeyBindings[i];
			if (bindingEntry.mAction == action)
			{
				bindingEntry.mKeyCode = KKeyCode.None;
				bindingEntry.mModifier = Modifier.None;
				GameInputMapping.KeyBindings[i] = bindingEntry;
			}
		}
	}

	// Token: 0x0400392B RID: 14635
	private const string ROOT_KEY = "STRINGS.INPUT_BINDINGS.";

	// Token: 0x0400392C RID: 14636
	[SerializeField]
	private OptionsMenuScreen optionsScreen;

	// Token: 0x0400392D RID: 14637
	[SerializeField]
	private ConfirmDialogScreen confirmPrefab;

	// Token: 0x0400392E RID: 14638
	public KButton backButton;

	// Token: 0x0400392F RID: 14639
	public KButton resetButton;

	// Token: 0x04003930 RID: 14640
	public KButton closeButton;

	// Token: 0x04003931 RID: 14641
	public KButton prevScreenButton;

	// Token: 0x04003932 RID: 14642
	public KButton nextScreenButton;

	// Token: 0x04003933 RID: 14643
	private bool waitingForKeyPress;

	// Token: 0x04003934 RID: 14644
	private global::Action actionToRebind = global::Action.NumActions;

	// Token: 0x04003935 RID: 14645
	private bool ignoreRootConflicts;

	// Token: 0x04003936 RID: 14646
	private KButton activeButton;

	// Token: 0x04003937 RID: 14647
	[SerializeField]
	private LocText screenTitle;

	// Token: 0x04003938 RID: 14648
	[SerializeField]
	private GameObject parent;

	// Token: 0x04003939 RID: 14649
	[SerializeField]
	private GameObject entryPrefab;

	// Token: 0x0400393A RID: 14650
	private ConfirmDialogScreen confirmDialog;

	// Token: 0x0400393B RID: 14651
	private int activeScreen = -1;

	// Token: 0x0400393C RID: 14652
	private List<string> screens = new List<string>();

	// Token: 0x0400393D RID: 14653
	private UIPool<HorizontalLayoutGroup> entryPool;

	// Token: 0x0400393E RID: 14654
	private static readonly KeyCode[] validKeys = new KeyCode[]
	{
		KeyCode.Backspace,
		KeyCode.Tab,
		KeyCode.Clear,
		KeyCode.Return,
		KeyCode.Pause,
		KeyCode.Space,
		KeyCode.Exclaim,
		KeyCode.DoubleQuote,
		KeyCode.Hash,
		KeyCode.Dollar,
		KeyCode.Ampersand,
		KeyCode.Quote,
		KeyCode.LeftParen,
		KeyCode.RightParen,
		KeyCode.Asterisk,
		KeyCode.Plus,
		KeyCode.Comma,
		KeyCode.Minus,
		KeyCode.Period,
		KeyCode.Slash,
		KeyCode.Alpha0,
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
		KeyCode.Colon,
		KeyCode.Semicolon,
		KeyCode.Less,
		KeyCode.Equals,
		KeyCode.Greater,
		KeyCode.Question,
		KeyCode.At,
		KeyCode.LeftBracket,
		KeyCode.Backslash,
		KeyCode.RightBracket,
		KeyCode.Caret,
		KeyCode.Underscore,
		KeyCode.BackQuote,
		KeyCode.A,
		KeyCode.B,
		KeyCode.C,
		KeyCode.D,
		KeyCode.E,
		KeyCode.F,
		KeyCode.G,
		KeyCode.H,
		KeyCode.I,
		KeyCode.J,
		KeyCode.K,
		KeyCode.L,
		KeyCode.M,
		KeyCode.N,
		KeyCode.O,
		KeyCode.P,
		KeyCode.Q,
		KeyCode.R,
		KeyCode.S,
		KeyCode.T,
		KeyCode.U,
		KeyCode.V,
		KeyCode.W,
		KeyCode.X,
		KeyCode.Y,
		KeyCode.Z,
		KeyCode.Delete,
		KeyCode.Keypad0,
		KeyCode.Keypad1,
		KeyCode.Keypad2,
		KeyCode.Keypad3,
		KeyCode.Keypad4,
		KeyCode.Keypad5,
		KeyCode.Keypad6,
		KeyCode.Keypad7,
		KeyCode.Keypad8,
		KeyCode.Keypad9,
		KeyCode.KeypadPeriod,
		KeyCode.KeypadDivide,
		KeyCode.KeypadMultiply,
		KeyCode.KeypadMinus,
		KeyCode.KeypadPlus,
		KeyCode.KeypadEnter,
		KeyCode.KeypadEquals,
		KeyCode.UpArrow,
		KeyCode.DownArrow,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.Insert,
		KeyCode.Home,
		KeyCode.End,
		KeyCode.PageUp,
		KeyCode.PageDown,
		KeyCode.F1,
		KeyCode.F2,
		KeyCode.F3,
		KeyCode.F4,
		KeyCode.F5,
		KeyCode.F6,
		KeyCode.F7,
		KeyCode.F8,
		KeyCode.F9,
		KeyCode.F10,
		KeyCode.F11,
		KeyCode.F12,
		KeyCode.F13,
		KeyCode.F14,
		KeyCode.F15
	};
}
