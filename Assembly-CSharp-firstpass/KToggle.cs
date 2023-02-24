using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000069 RID: 105
public class KToggle : Toggle
{
	// Token: 0x1400001D RID: 29
	// (add) Token: 0x0600044A RID: 1098 RVA: 0x00015C08 File Offset: 0x00013E08
	// (remove) Token: 0x0600044B RID: 1099 RVA: 0x00015C40 File Offset: 0x00013E40
	public event System.Action onClick;

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x0600044C RID: 1100 RVA: 0x00015C78 File Offset: 0x00013E78
	// (remove) Token: 0x0600044D RID: 1101 RVA: 0x00015CB0 File Offset: 0x00013EB0
	public event System.Action onDoubleClick;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x0600044E RID: 1102 RVA: 0x00015CE8 File Offset: 0x00013EE8
	// (remove) Token: 0x0600044F RID: 1103 RVA: 0x00015D20 File Offset: 0x00013F20
	public new event Action<bool> onValueChanged;

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06000450 RID: 1104 RVA: 0x00015D58 File Offset: 0x00013F58
	// (remove) Token: 0x06000451 RID: 1105 RVA: 0x00015D90 File Offset: 0x00013F90
	public event KToggle.PointerEvent onPointerEnter;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06000452 RID: 1106 RVA: 0x00015DC8 File Offset: 0x00013FC8
	// (remove) Token: 0x06000453 RID: 1107 RVA: 0x00015E00 File Offset: 0x00014000
	public event KToggle.PointerEvent onPointerExit;

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000454 RID: 1108 RVA: 0x00015E35 File Offset: 0x00014035
	public bool GetMouseOver
	{
		get
		{
			return this.mouseOver;
		}
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00015E3D File Offset: 0x0001403D
	public void ClearOnClick()
	{
		this.onClick = null;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00015E46 File Offset: 0x00014046
	public void ClearPointerCallbacks()
	{
		this.onPointerEnter = null;
		this.onPointerExit = null;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00015E56 File Offset: 0x00014056
	public void ClearAllCallbacks()
	{
		this.ClearOnClick();
		this.ClearPointerCallbacks();
		this.onDoubleClick = null;
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00015E6C File Offset: 0x0001406C
	public void Click()
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		if (!this.IsInteractable())
		{
			return;
		}
		if (UnityEngine.EventSystems.EventSystem.current == null || !UnityEngine.EventSystems.EventSystem.current.enabled)
		{
			return;
		}
		if (this.isOn)
		{
			this.Deselect();
			this.isOn = false;
		}
		else
		{
			this.Select();
			this.isOn = true;
		}
		if (this.soundPlayer.AcceptClickCondition != null && !this.soundPlayer.AcceptClickCondition())
		{
			this.soundPlayer.Play(3);
		}
		else
		{
			this.soundPlayer.Play(this.isOn ? 0 : 1);
		}
		base.gameObject.Trigger(2098165161, null);
		this.onClick.Signal();
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00015F28 File Offset: 0x00014128
	private void OnValueChanged(bool value)
	{
		if (!this.IsInteractable())
		{
			return;
		}
		ImageToggleState[] components = base.GetComponents<ImageToggleState>();
		if (components != null && components.Length != 0)
		{
			ImageToggleState[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActiveState(value);
			}
		}
		this.ActivateFlourish(value);
		this.onValueChanged.Signal(value);
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00015F78 File Offset: 0x00014178
	public void ForceUpdateVisualState()
	{
		ImageToggleState[] components = base.GetComponents<ImageToggleState>();
		if (components != null && components.Length != 0)
		{
			ImageToggleState[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResetColor();
			}
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00015FAC File Offset: 0x000141AC
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			return;
		}
		if (!this.IsInteractable())
		{
			return;
		}
		if (eventData.clickCount == 1 || this.onDoubleClick == null)
		{
			this.Click();
			return;
		}
		if (eventData.clickCount == 2 && this.onDoubleClick != null)
		{
			this.onDoubleClick();
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00016008 File Offset: 0x00014208
	public override void OnDeselect(BaseEventData eventData)
	{
		if (this.GetParentToggleGroup(eventData) == base.group)
		{
			base.OnDeselect(eventData);
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00016025 File Offset: 0x00014225
	public void Deselect()
	{
		base.OnDeselect(null);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00016030 File Offset: 0x00014230
	public void ClearAnimState()
	{
		if (this.artExtension.animator != null && this.artExtension.animator.isInitialized)
		{
			Animator animator = this.artExtension.animator;
			animator.SetBool("Toggled", false);
			animator.Play("idle", 0);
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00016084 File Offset: 0x00014284
	public override void OnSelect(BaseEventData eventData)
	{
		if (base.group != null)
		{
			foreach (Toggle toggle in base.group.ActiveToggles())
			{
				((KToggle)toggle).Deselect();
			}
			base.group.SetAllTogglesOff(true);
		}
		base.OnSelect(eventData);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x000160FC File Offset: 0x000142FC
	public void ActivateFlourish(bool state)
	{
		if (this.artExtension.animator != null && this.artExtension.animator.isInitialized)
		{
			this.artExtension.animator.SetBool("Toggled", state);
		}
		if (this.artExtension.SelectedFlourish != null)
		{
			this.artExtension.SelectedFlourish.enabled = state;
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00016168 File Offset: 0x00014368
	public void ActivateFlourish(bool state, ImageToggleState.State ImageState)
	{
		ImageToggleState[] components = base.GetComponents<ImageToggleState>();
		if (components != null && components.Length != 0)
		{
			ImageToggleState[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetState(ImageState);
			}
		}
		this.ActivateFlourish(state);
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x000161A4 File Offset: 0x000143A4
	private ToggleGroup GetParentToggleGroup(BaseEventData eventData)
	{
		PointerEventData pointerEventData = eventData as PointerEventData;
		if (pointerEventData == null)
		{
			return null;
		}
		GameObject gameObject = pointerEventData.pointerPressRaycast.gameObject;
		if (gameObject == null)
		{
			return null;
		}
		Toggle componentInParent = gameObject.GetComponentInParent<Toggle>();
		if (componentInParent == null || componentInParent.group == null)
		{
			return null;
		}
		return componentInParent.group;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00016200 File Offset: 0x00014400
	public void OnPointerEnter()
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		ImageToggleState[] components = base.GetComponents<ImageToggleState>();
		if (components != null && components.Length != 0)
		{
			ImageToggleState[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnHoverIn();
			}
		}
		this.soundPlayer.Play(2);
		this.mouseOver = true;
		if (this.onPointerEnter != null)
		{
			this.onPointerEnter();
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00016268 File Offset: 0x00014468
	public void OnPointerExit()
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		ImageToggleState[] components = base.GetComponents<ImageToggleState>();
		if (components != null && components.Length != 0)
		{
			ImageToggleState[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnHoverOut();
			}
		}
		this.mouseOver = false;
		if (this.onPointerExit != null)
		{
			this.onPointerExit();
		}
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x000162C2 File Offset: 0x000144C2
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		this.OnPointerEnter();
		base.OnPointerEnter(eventData);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x000162D9 File Offset: 0x000144D9
	public override void OnPointerExit(PointerEventData eventData)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		this.OnPointerExit();
		base.OnPointerExit(eventData);
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000467 RID: 1127 RVA: 0x000162F0 File Offset: 0x000144F0
	// (set) Token: 0x06000468 RID: 1128 RVA: 0x000162F8 File Offset: 0x000144F8
	public new bool isOn
	{
		get
		{
			return base.isOn;
		}
		set
		{
			base.isOn = value;
			this.OnValueChanged(base.isOn);
		}
	}

	// Token: 0x040004A9 RID: 1193
	[SerializeField]
	public ToggleSoundPlayer soundPlayer;

	// Token: 0x040004AF RID: 1199
	public Image bgImage;

	// Token: 0x040004B0 RID: 1200
	public Image fgImage;

	// Token: 0x040004B1 RID: 1201
	public KToggleArtExtensions artExtension;

	// Token: 0x040004B2 RID: 1202
	protected bool mouseOver;

	// Token: 0x020009BD RID: 2493
	// (Invoke) Token: 0x06005368 RID: 21352
	public delegate void PointerEvent();
}
