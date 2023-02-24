using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000057 RID: 87
[AddComponentMenu("KMonoBehaviour/Plugins/KButton")]
public class KButton : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000367 RID: 871 RVA: 0x00011F70 File Offset: 0x00010170
	// (remove) Token: 0x06000368 RID: 872 RVA: 0x00011FA8 File Offset: 0x000101A8
	public event System.Action onClick;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000369 RID: 873 RVA: 0x00011FE0 File Offset: 0x000101E0
	// (remove) Token: 0x0600036A RID: 874 RVA: 0x00012018 File Offset: 0x00010218
	public event System.Action onDoubleClick;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x0600036B RID: 875 RVA: 0x00012050 File Offset: 0x00010250
	// (remove) Token: 0x0600036C RID: 876 RVA: 0x00012088 File Offset: 0x00010288
	public event Action<KKeyCode> onBtnClick;

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x0600036D RID: 877 RVA: 0x000120C0 File Offset: 0x000102C0
	// (remove) Token: 0x0600036E RID: 878 RVA: 0x000120F8 File Offset: 0x000102F8
	public event System.Action onPointerEnter;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x0600036F RID: 879 RVA: 0x00012130 File Offset: 0x00010330
	// (remove) Token: 0x06000370 RID: 880 RVA: 0x00012168 File Offset: 0x00010368
	public event System.Action onPointerExit;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000371 RID: 881 RVA: 0x000121A0 File Offset: 0x000103A0
	// (remove) Token: 0x06000372 RID: 882 RVA: 0x000121D8 File Offset: 0x000103D8
	public event System.Action onPointerDown;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000373 RID: 883 RVA: 0x00012210 File Offset: 0x00010410
	// (remove) Token: 0x06000374 RID: 884 RVA: 0x00012248 File Offset: 0x00010448
	public event System.Action onPointerUp;

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000376 RID: 886 RVA: 0x00012299 File Offset: 0x00010499
	// (set) Token: 0x06000375 RID: 885 RVA: 0x0001227D File Offset: 0x0001047D
	public bool isInteractable
	{
		get
		{
			return this.interactable;
		}
		set
		{
			this.interactable = value;
			this.UpdateColor(this.interactable, this.mouseOver, false);
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000377 RID: 887 RVA: 0x000122A1 File Offset: 0x000104A1
	public bool GetMouseOver
	{
		get
		{
			return this.mouseOver;
		}
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000122A9 File Offset: 0x000104A9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateColor(this.interactable, false, false);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000122BF File Offset: 0x000104BF
	public void ClearOnClick()
	{
		this.onClick = null;
		this.onBtnClick = null;
		this.onDoubleClick = null;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000122D6 File Offset: 0x000104D6
	public void ClearOnPointerEvents()
	{
		this.onPointerEnter = null;
		this.onPointerExit = null;
		this.onPointerDown = null;
		this.onPointerUp = null;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x000122F4 File Offset: 0x000104F4
	public void OnPointerUp(PointerEventData eventData)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		this.UpdateColor(this.interactable, false, false);
		this.onPointerUp.Signal();
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001231C File Offset: 0x0001051C
	public void OnPointerDown(PointerEventData eventData)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		this.UpdateColor(this.interactable, true, true);
		this.PlayPointerDownSound();
		this.onPointerDown.Signal();
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0001234A File Offset: 0x0001054A
	public void SignalClick(KKeyCode btn)
	{
		if (this.interactable)
		{
			if (this.onClick != null && btn != KKeyCode.Mouse1)
			{
				this.onClick();
			}
			if (this.onBtnClick != null)
			{
				this.onBtnClick(btn);
			}
		}
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00012383 File Offset: 0x00010583
	public void SignalDoubleClick(KKeyCode btn)
	{
		if (this.interactable && this.onDoubleClick != null)
		{
			this.onDoubleClick();
		}
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000123A0 File Offset: 0x000105A0
	public void OnPointerClick(PointerEventData eventData)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		if (this.interactable)
		{
			KKeyCode kkeyCode = KKeyCode.None;
			switch (eventData.button)
			{
			case PointerEventData.InputButton.Left:
				kkeyCode = KKeyCode.Mouse0;
				break;
			case PointerEventData.InputButton.Right:
				kkeyCode = KKeyCode.Mouse1;
				break;
			case PointerEventData.InputButton.Middle:
				kkeyCode = KKeyCode.Mouse2;
				break;
			}
			if ((eventData.clickCount == 1 || this.onDoubleClick == null) && (this.onClick != null || this.onBtnClick != null))
			{
				this.SignalClick(kkeyCode);
				return;
			}
			if (eventData.clickCount == 2 && this.onDoubleClick != null)
			{
				this.SignalDoubleClick(kkeyCode);
			}
		}
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00012438 File Offset: 0x00010638
	public void OnPointerEnter(PointerEventData eventData)
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
		this.UpdateColor(this.interactable, true, false);
		this.soundPlayer.Play(1);
		this.mouseOver = true;
		this.onPointerEnter.Signal();
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000124A4 File Offset: 0x000106A4
	public void OnPointerExit(PointerEventData eventData)
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
		this.UpdateColor(this.interactable, false, false);
		this.mouseOver = false;
		this.onPointerExit.Signal();
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00012504 File Offset: 0x00010704
	private void UpdateColor(bool interactable, bool hover, bool press)
	{
		if (this.bgImage == null)
		{
			this.bgImage = base.GetComponent<KImage>();
			string text = "";
			Transform transform = base.transform;
			int num = 0;
			while (num < 5 && transform.parent != null)
			{
				transform = transform.parent;
				string name = transform.name;
				text = string.Format("{0}/{1}", name, text);
				num++;
			}
			if (this.bgImage == null)
			{
				return;
			}
		}
		this.UpdateKImageColor(this.bgImage, interactable, hover, press);
		for (int i = 0; i < this.additionalKImages.Length; i++)
		{
			this.UpdateKImageColor(this.additionalKImages[i], interactable, hover, press);
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000125B4 File Offset: 0x000107B4
	private void UpdateKImageColor(KImage image, bool interactable, bool hover, bool press)
	{
		if (image != null)
		{
			if (interactable)
			{
				if (press)
				{
					image.ColorState = KImage.ColorSelector.Active;
					return;
				}
				image.ColorState = (hover ? KImage.ColorSelector.Hover : KImage.ColorSelector.Inactive);
				return;
			}
			else
			{
				image.ColorState = (hover ? KImage.ColorSelector.Disabled : KImage.ColorSelector.Disabled);
			}
		}
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000125EC File Offset: 0x000107EC
	public void PlayPointerDownSound()
	{
		if (!this.interactable || (this.soundPlayer.AcceptClickCondition != null && !this.soundPlayer.AcceptClickCondition()))
		{
			this.soundPlayer.Play(2);
			return;
		}
		this.soundPlayer.Play(0);
	}

	// Token: 0x04000413 RID: 1043
	[SerializeField]
	public ButtonSoundPlayer soundPlayer;

	// Token: 0x04000414 RID: 1044
	public KImage bgImage;

	// Token: 0x04000415 RID: 1045
	public Image fgImage;

	// Token: 0x04000416 RID: 1046
	public KImage[] additionalKImages;

	// Token: 0x0400041E RID: 1054
	private bool interactable = true;

	// Token: 0x0400041F RID: 1055
	private bool mouseOver;
}
