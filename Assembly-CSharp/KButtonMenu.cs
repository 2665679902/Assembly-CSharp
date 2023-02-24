using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000AC4 RID: 2756
public class KButtonMenu : KScreen
{
	// Token: 0x0600545F RID: 21599 RVA: 0x001E9EE9 File Offset: 0x001E80E9
	protected override void OnActivate()
	{
		base.ConsumeMouseScroll = this.ShouldConsumeMouseScroll;
		this.RefreshButtons();
	}

	// Token: 0x06005460 RID: 21600 RVA: 0x001E9EFD File Offset: 0x001E80FD
	public void SetButtons(IList<KButtonMenu.ButtonInfo> buttons)
	{
		this.buttons = buttons;
		if (this.activateOnSpawn)
		{
			this.RefreshButtons();
		}
	}

	// Token: 0x06005461 RID: 21601 RVA: 0x001E9F14 File Offset: 0x001E8114
	public virtual void RefreshButtons()
	{
		if (this.buttonObjects != null)
		{
			for (int i = 0; i < this.buttonObjects.Length; i++)
			{
				UnityEngine.Object.Destroy(this.buttonObjects[i]);
			}
			this.buttonObjects = null;
		}
		if (this.buttons == null)
		{
			return;
		}
		this.buttonObjects = new GameObject[this.buttons.Count];
		for (int j = 0; j < this.buttons.Count; j++)
		{
			KButtonMenu.ButtonInfo binfo = this.buttons[j];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, Vector3.zero, Quaternion.identity);
			this.buttonObjects[j] = gameObject;
			Transform transform = ((this.buttonParent != null) ? this.buttonParent : base.transform);
			gameObject.transform.SetParent(transform, false);
			gameObject.SetActive(true);
			gameObject.name = binfo.text + "Button";
			LocText[] componentsInChildren = gameObject.GetComponentsInChildren<LocText>(true);
			if (componentsInChildren != null)
			{
				foreach (LocText locText in componentsInChildren)
				{
					locText.text = ((locText.name == "Hotkey") ? GameUtil.GetActionString(binfo.shortcutKey) : binfo.text);
					locText.color = (binfo.isEnabled ? new Color(1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f));
				}
			}
			ToolTip componentInChildren = gameObject.GetComponentInChildren<ToolTip>();
			if (binfo.toolTip != null && binfo.toolTip != "" && componentInChildren != null)
			{
				componentInChildren.toolTip = binfo.toolTip;
			}
			KButtonMenu screen = this;
			KButton button = gameObject.GetComponent<KButton>();
			button.isInteractable = binfo.isEnabled;
			if (binfo.popupOptions == null && binfo.onPopulatePopup == null)
			{
				UnityAction onClick = binfo.onClick;
				System.Action action = delegate
				{
					onClick();
					if (!this.keepMenuOpen && screen != null)
					{
						screen.Deactivate();
					}
				};
				button.onClick += action;
			}
			else
			{
				button.onClick += delegate
				{
					this.SetupPopupMenu(binfo, button);
				};
			}
			binfo.uibutton = button;
			KButtonMenu.ButtonInfo.HoverCallback onHover = binfo.onHover;
		}
		this.Update();
	}

	// Token: 0x06005462 RID: 21602 RVA: 0x001EA1C8 File Offset: 0x001E83C8
	protected Button.ButtonClickedEvent SetupPopupMenu(KButtonMenu.ButtonInfo binfo, KButton button)
	{
		Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
		UnityAction unityAction = delegate
		{
			List<KButtonMenu.ButtonInfo> list = new List<KButtonMenu.ButtonInfo>();
			if (binfo.onPopulatePopup != null)
			{
				binfo.popupOptions = binfo.onPopulatePopup();
			}
			string[] popupOptions = binfo.popupOptions;
			for (int i = 0; i < popupOptions.Length; i++)
			{
				string text = popupOptions[i];
				string delegate_str = text;
				list.Add(new KButtonMenu.ButtonInfo(delegate_str, delegate
				{
					binfo.onPopupClick(delegate_str);
					if (!this.keepMenuOpen)
					{
						this.Deactivate();
					}
				}, global::Action.NumActions, null, null, null, true, null, null, null));
			}
			KButtonMenu component = Util.KInstantiate(ScreenPrefabs.Instance.ButtonGrid.gameObject, null, null).GetComponent<KButtonMenu>();
			component.SetButtons(list.ToArray());
			RootMenu.Instance.AddSubMenu(component);
			Game.Instance.LocalPlayer.ScreenManager.ActivateScreen(component.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
			Vector3 vector = default(Vector3);
			if (Util.IsOnLeftSideOfScreen(button.transform.GetPosition()))
			{
				vector.x = button.GetComponent<RectTransform>().rect.width * 0.25f;
			}
			else
			{
				vector.x = -button.GetComponent<RectTransform>().rect.width * 0.25f;
			}
			component.transform.SetPosition(button.transform.GetPosition() + vector);
		};
		binfo.onClick = unityAction;
		buttonClickedEvent.AddListener(unityAction);
		return buttonClickedEvent;
	}

	// Token: 0x06005463 RID: 21603 RVA: 0x001EA218 File Offset: 0x001E8418
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.buttons == null)
		{
			return;
		}
		for (int i = 0; i < this.buttons.Count; i++)
		{
			KButtonMenu.ButtonInfo buttonInfo = this.buttons[i];
			if (e.TryConsume(buttonInfo.shortcutKey))
			{
				this.buttonObjects[i].GetComponent<KButton>().PlayPointerDownSound();
				this.buttonObjects[i].GetComponent<KButton>().SignalClick(KKeyCode.Mouse0);
				break;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005464 RID: 21604 RVA: 0x001EA291 File Offset: 0x001E8491
	protected override void OnPrefabInit()
	{
		base.Subscribe<KButtonMenu>(315865555, KButtonMenu.OnSetActivatorDelegate);
	}

	// Token: 0x06005465 RID: 21605 RVA: 0x001EA2A4 File Offset: 0x001E84A4
	private void OnSetActivator(object data)
	{
		this.go = (GameObject)data;
		this.Update();
	}

	// Token: 0x06005466 RID: 21606 RVA: 0x001EA2B8 File Offset: 0x001E84B8
	protected override void OnDeactivate()
	{
	}

	// Token: 0x06005467 RID: 21607 RVA: 0x001EA2BC File Offset: 0x001E84BC
	private void Update()
	{
		if (!this.followGameObject || this.go == null || base.canvas == null)
		{
			return;
		}
		Vector3 vector = Camera.main.WorldToViewportPoint(this.go.transform.GetPosition());
		RectTransform component = base.GetComponent<RectTransform>();
		RectTransform component2 = base.canvas.GetComponent<RectTransform>();
		if (component != null)
		{
			component.anchoredPosition = new Vector2(vector.x * component2.sizeDelta.x - component2.sizeDelta.x * 0.5f, vector.y * component2.sizeDelta.y - component2.sizeDelta.y * 0.5f);
		}
	}

	// Token: 0x0400395B RID: 14683
	[SerializeField]
	protected bool followGameObject;

	// Token: 0x0400395C RID: 14684
	[SerializeField]
	protected bool keepMenuOpen;

	// Token: 0x0400395D RID: 14685
	[SerializeField]
	protected Transform buttonParent;

	// Token: 0x0400395E RID: 14686
	public GameObject buttonPrefab;

	// Token: 0x0400395F RID: 14687
	public bool ShouldConsumeMouseScroll;

	// Token: 0x04003960 RID: 14688
	[NonSerialized]
	public GameObject[] buttonObjects;

	// Token: 0x04003961 RID: 14689
	protected GameObject go;

	// Token: 0x04003962 RID: 14690
	protected IList<KButtonMenu.ButtonInfo> buttons;

	// Token: 0x04003963 RID: 14691
	private static readonly EventSystem.IntraObjectHandler<KButtonMenu> OnSetActivatorDelegate = new EventSystem.IntraObjectHandler<KButtonMenu>(delegate(KButtonMenu component, object data)
	{
		component.OnSetActivator(data);
	});

	// Token: 0x0200193E RID: 6462
	public class ButtonInfo
	{
		// Token: 0x06008FA7 RID: 36775 RVA: 0x00310A28 File Offset: 0x0030EC28
		public ButtonInfo(string text = null, UnityAction on_click = null, global::Action shortcut_key = global::Action.NumActions, KButtonMenu.ButtonInfo.HoverCallback on_hover = null, string tool_tip = null, GameObject visualizer = null, bool is_enabled = true, string[] popup_options = null, Action<string> on_popup_click = null, Func<string[]> on_populate_popup = null)
		{
			this.text = text;
			this.shortcutKey = shortcut_key;
			this.onClick = on_click;
			this.onHover = on_hover;
			this.visualizer = visualizer;
			this.toolTip = tool_tip;
			this.isEnabled = is_enabled;
			this.uibutton = null;
			this.popupOptions = popup_options;
			this.onPopupClick = on_popup_click;
			this.onPopulatePopup = on_populate_popup;
		}

		// Token: 0x06008FA8 RID: 36776 RVA: 0x00310A98 File Offset: 0x0030EC98
		public ButtonInfo(string text, global::Action shortcutKey, UnityAction onClick, KButtonMenu.ButtonInfo.HoverCallback onHover = null, object userData = null)
		{
			this.text = text;
			this.shortcutKey = shortcutKey;
			this.onClick = onClick;
			this.onHover = onHover;
			this.userData = userData;
			this.visualizer = null;
			this.uibutton = null;
		}

		// Token: 0x06008FA9 RID: 36777 RVA: 0x00310AE8 File Offset: 0x0030ECE8
		public ButtonInfo(string text, GameObject visualizer, global::Action shortcutKey, UnityAction onClick, KButtonMenu.ButtonInfo.HoverCallback onHover = null, object userData = null)
		{
			this.text = text;
			this.shortcutKey = shortcutKey;
			this.onClick = onClick;
			this.onHover = onHover;
			this.visualizer = visualizer;
			this.userData = userData;
			this.uibutton = null;
		}

		// Token: 0x040073BC RID: 29628
		public string text;

		// Token: 0x040073BD RID: 29629
		public global::Action shortcutKey;

		// Token: 0x040073BE RID: 29630
		public GameObject visualizer;

		// Token: 0x040073BF RID: 29631
		public UnityAction onClick;

		// Token: 0x040073C0 RID: 29632
		public KButtonMenu.ButtonInfo.HoverCallback onHover;

		// Token: 0x040073C1 RID: 29633
		public FMODAsset clickSound;

		// Token: 0x040073C2 RID: 29634
		public KButton uibutton;

		// Token: 0x040073C3 RID: 29635
		public string toolTip;

		// Token: 0x040073C4 RID: 29636
		public bool isEnabled = true;

		// Token: 0x040073C5 RID: 29637
		public string[] popupOptions;

		// Token: 0x040073C6 RID: 29638
		public Action<string> onPopupClick;

		// Token: 0x040073C7 RID: 29639
		public Func<string[]> onPopulatePopup;

		// Token: 0x040073C8 RID: 29640
		public object userData;

		// Token: 0x02002101 RID: 8449
		// (Invoke) Token: 0x0600A5BF RID: 42431
		public delegate void HoverCallback(GameObject hoverTarget);

		// Token: 0x02002102 RID: 8450
		// (Invoke) Token: 0x0600A5C3 RID: 42435
		public delegate void Callback();
	}
}
