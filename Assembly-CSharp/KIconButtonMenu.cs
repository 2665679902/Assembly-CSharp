using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000AC6 RID: 2758
public class KIconButtonMenu : KScreen
{
	// Token: 0x06005472 RID: 21618 RVA: 0x001EA644 File Offset: 0x001E8844
	protected override void OnActivate()
	{
		base.OnActivate();
		this.RefreshButtons();
	}

	// Token: 0x06005473 RID: 21619 RVA: 0x001EA652 File Offset: 0x001E8852
	public void SetButtons(IList<KIconButtonMenu.ButtonInfo> buttons)
	{
		this.buttons = buttons;
		if (this.activateOnSpawn)
		{
			this.RefreshButtons();
		}
	}

	// Token: 0x06005474 RID: 21620 RVA: 0x001EA66C File Offset: 0x001E886C
	public void RefreshButtonTooltip()
	{
		for (int i = 0; i < this.buttons.Count; i++)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = this.buttons[i];
			if (buttonInfo.buttonGo == null || buttonInfo == null)
			{
				return;
			}
			ToolTip componentInChildren = buttonInfo.buttonGo.GetComponentInChildren<ToolTip>();
			if (buttonInfo.text != null && buttonInfo.text != "" && componentInChildren != null)
			{
				componentInChildren.toolTip = buttonInfo.GetTooltipText();
				LocText componentInChildren2 = buttonInfo.buttonGo.GetComponentInChildren<LocText>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.text = buttonInfo.text;
				}
			}
		}
	}

	// Token: 0x06005475 RID: 21621 RVA: 0x001EA710 File Offset: 0x001E8910
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
		if (this.buttons == null || this.buttons.Count == 0)
		{
			return;
		}
		this.buttonObjects = new GameObject[this.buttons.Count];
		for (int j = 0; j < this.buttons.Count; j++)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = this.buttons[j];
			if (buttonInfo != null)
			{
				GameObject binstance = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, Vector3.zero, Quaternion.identity);
				buttonInfo.buttonGo = binstance;
				this.buttonObjects[j] = binstance;
				Transform transform = ((this.buttonParent != null) ? this.buttonParent : base.transform);
				binstance.transform.SetParent(transform, false);
				binstance.SetActive(true);
				binstance.name = buttonInfo.text + "Button";
				KButton component = binstance.GetComponent<KButton>();
				if (component != null && buttonInfo.onClick != null)
				{
					component.onClick += buttonInfo.onClick;
				}
				Image image = null;
				if (component)
				{
					image = component.fgImage;
				}
				if (image != null)
				{
					image.gameObject.SetActive(false);
					foreach (Sprite sprite in this.icons)
					{
						if (sprite != null && sprite.name == buttonInfo.iconName)
						{
							image.sprite = sprite;
							image.gameObject.SetActive(true);
							break;
						}
					}
				}
				if (buttonInfo.texture != null)
				{
					RawImage componentInChildren = binstance.GetComponentInChildren<RawImage>();
					if (componentInChildren != null)
					{
						componentInChildren.gameObject.SetActive(true);
						componentInChildren.texture = buttonInfo.texture;
					}
				}
				ToolTip componentInChildren2 = binstance.GetComponentInChildren<ToolTip>();
				if (buttonInfo.text != null && buttonInfo.text != "" && componentInChildren2 != null)
				{
					componentInChildren2.toolTip = buttonInfo.GetTooltipText();
					LocText componentInChildren3 = binstance.GetComponentInChildren<LocText>();
					if (componentInChildren3 != null)
					{
						componentInChildren3.text = buttonInfo.text;
					}
				}
				if (buttonInfo.onToolTip != null)
				{
					componentInChildren2.OnToolTip = buttonInfo.onToolTip;
				}
				KIconButtonMenu screen = this;
				System.Action onClick = buttonInfo.onClick;
				System.Action action = delegate
				{
					onClick.Signal();
					if (!this.keepMenuOpen && screen != null)
					{
						screen.Deactivate();
					}
					if (binstance != null)
					{
						KToggle component3 = binstance.GetComponent<KToggle>();
						if (component3 != null)
						{
							this.SelectToggle(component3);
						}
					}
				};
				KToggle componentInChildren4 = binstance.GetComponentInChildren<KToggle>();
				if (componentInChildren4 != null)
				{
					ToggleGroup component2 = base.GetComponent<ToggleGroup>();
					if (component2 == null)
					{
						component2 = this.externalToggleGroup;
					}
					componentInChildren4.group = component2;
					componentInChildren4.onClick += action;
					Navigation navigation = componentInChildren4.navigation;
					navigation.mode = (this.automaticNavigation ? Navigation.Mode.Automatic : Navigation.Mode.None);
					componentInChildren4.navigation = navigation;
				}
				else
				{
					KBasicToggle componentInChildren5 = binstance.GetComponentInChildren<KBasicToggle>();
					if (componentInChildren5 != null)
					{
						componentInChildren5.onClick += action;
					}
				}
				if (component != null)
				{
					component.isInteractable = buttonInfo.isInteractable;
				}
				buttonInfo.onCreate.Signal(buttonInfo);
			}
		}
		this.Update();
	}

	// Token: 0x06005476 RID: 21622 RVA: 0x001EAA80 File Offset: 0x001E8C80
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.buttons == null)
		{
			return;
		}
		if (!base.gameObject.activeSelf || !base.enabled)
		{
			return;
		}
		for (int i = 0; i < this.buttons.Count; i++)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = this.buttons[i];
			if (e.TryConsume(buttonInfo.shortcutKey))
			{
				this.buttonObjects[i].GetComponent<KButton>().PlayPointerDownSound();
				this.buttonObjects[i].GetComponent<KButton>().SignalClick(KKeyCode.Mouse0);
				break;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005477 RID: 21623 RVA: 0x001EAB0F File Offset: 0x001E8D0F
	protected override void OnPrefabInit()
	{
		base.Subscribe<KIconButtonMenu>(315865555, KIconButtonMenu.OnSetActivatorDelegate);
	}

	// Token: 0x06005478 RID: 21624 RVA: 0x001EAB22 File Offset: 0x001E8D22
	private void OnSetActivator(object data)
	{
		this.go = (GameObject)data;
		this.Update();
	}

	// Token: 0x06005479 RID: 21625 RVA: 0x001EAB38 File Offset: 0x001E8D38
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

	// Token: 0x0600547A RID: 21626 RVA: 0x001EABF4 File Offset: 0x001E8DF4
	protected void SelectToggle(KToggle selectedToggle)
	{
		if (UnityEngine.EventSystems.EventSystem.current == null || !UnityEngine.EventSystems.EventSystem.current.enabled)
		{
			return;
		}
		if (this.currentlySelectedToggle == selectedToggle)
		{
			this.currentlySelectedToggle = null;
		}
		else
		{
			this.currentlySelectedToggle = selectedToggle;
		}
		GameObject[] array = this.buttonObjects;
		for (int i = 0; i < array.Length; i++)
		{
			KToggle component = array[i].GetComponent<KToggle>();
			if (component != null)
			{
				if (component == this.currentlySelectedToggle)
				{
					component.Select();
					component.isOn = true;
				}
				else
				{
					component.Deselect();
					component.isOn = false;
				}
			}
		}
	}

	// Token: 0x0600547B RID: 21627 RVA: 0x001EAC8C File Offset: 0x001E8E8C
	public void ClearSelection()
	{
		foreach (GameObject gameObject in this.buttonObjects)
		{
			KToggle component = gameObject.GetComponent<KToggle>();
			if (component != null)
			{
				component.Deselect();
				component.isOn = false;
			}
			else
			{
				KBasicToggle component2 = gameObject.GetComponent<KBasicToggle>();
				if (component2 != null)
				{
					component2.isOn = false;
				}
			}
			ImageToggleState component3 = gameObject.GetComponent<ImageToggleState>();
			if (component3.GetIsActive())
			{
				component3.SetInactive();
			}
		}
		ToggleGroup component4 = base.GetComponent<ToggleGroup>();
		if (component4 != null)
		{
			component4.SetAllTogglesOff(true);
		}
		this.SelectToggle(null);
	}

	// Token: 0x04003968 RID: 14696
	[SerializeField]
	protected bool followGameObject;

	// Token: 0x04003969 RID: 14697
	[SerializeField]
	protected bool keepMenuOpen;

	// Token: 0x0400396A RID: 14698
	[SerializeField]
	protected bool automaticNavigation = true;

	// Token: 0x0400396B RID: 14699
	[SerializeField]
	protected Transform buttonParent;

	// Token: 0x0400396C RID: 14700
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x0400396D RID: 14701
	[SerializeField]
	protected Sprite[] icons;

	// Token: 0x0400396E RID: 14702
	[SerializeField]
	private ToggleGroup externalToggleGroup;

	// Token: 0x0400396F RID: 14703
	protected KToggle currentlySelectedToggle;

	// Token: 0x04003970 RID: 14704
	[NonSerialized]
	public GameObject[] buttonObjects;

	// Token: 0x04003971 RID: 14705
	[SerializeField]
	public TextStyleSetting ToggleToolTipTextStyleSetting;

	// Token: 0x04003972 RID: 14706
	private UnityAction inputChangeReceiver;

	// Token: 0x04003973 RID: 14707
	protected GameObject go;

	// Token: 0x04003974 RID: 14708
	protected IList<KIconButtonMenu.ButtonInfo> buttons;

	// Token: 0x04003975 RID: 14709
	private static readonly global::EventSystem.IntraObjectHandler<KIconButtonMenu> OnSetActivatorDelegate = new global::EventSystem.IntraObjectHandler<KIconButtonMenu>(delegate(KIconButtonMenu component, object data)
	{
		component.OnSetActivator(data);
	});

	// Token: 0x02001945 RID: 6469
	public class ButtonInfo
	{
		// Token: 0x06008FB6 RID: 36790 RVA: 0x00310DB0 File Offset: 0x0030EFB0
		public ButtonInfo(string iconName = "", string text = "", System.Action on_click = null, global::Action shortcutKey = global::Action.NumActions, Action<GameObject> on_refresh = null, Action<KIconButtonMenu.ButtonInfo> on_create = null, Texture texture = null, string tooltipText = "", bool is_interactable = true)
		{
			this.iconName = iconName;
			this.text = text;
			this.shortcutKey = shortcutKey;
			this.onClick = on_click;
			this.onCreate = on_create;
			this.texture = texture;
			this.tooltipText = tooltipText;
			this.isInteractable = is_interactable;
		}

		// Token: 0x06008FB7 RID: 36791 RVA: 0x00310E00 File Offset: 0x0030F000
		public string GetTooltipText()
		{
			string text = ((this.tooltipText == "") ? this.text : this.tooltipText);
			if (this.shortcutKey != global::Action.NumActions)
			{
				text = GameUtil.ReplaceHotkeyString(text, this.shortcutKey);
			}
			return text;
		}

		// Token: 0x040073D7 RID: 29655
		public string iconName;

		// Token: 0x040073D8 RID: 29656
		public string text;

		// Token: 0x040073D9 RID: 29657
		public string tooltipText;

		// Token: 0x040073DA RID: 29658
		public string[] multiText;

		// Token: 0x040073DB RID: 29659
		public global::Action shortcutKey;

		// Token: 0x040073DC RID: 29660
		public bool isInteractable;

		// Token: 0x040073DD RID: 29661
		public Action<KIconButtonMenu.ButtonInfo> onCreate;

		// Token: 0x040073DE RID: 29662
		public System.Action onClick;

		// Token: 0x040073DF RID: 29663
		public Func<string> onToolTip;

		// Token: 0x040073E0 RID: 29664
		public GameObject buttonGo;

		// Token: 0x040073E1 RID: 29665
		public object userData;

		// Token: 0x040073E2 RID: 29666
		public Texture texture;

		// Token: 0x02002103 RID: 8451
		// (Invoke) Token: 0x0600A5C7 RID: 42439
		public delegate void Callback();
	}
}
