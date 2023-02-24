using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BAE RID: 2990
public class DoorToggleSideScreen : SideScreenContent
{
	// Token: 0x06005E10 RID: 24080 RVA: 0x00225995 File Offset: 0x00223B95
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.InitButtons();
	}

	// Token: 0x06005E11 RID: 24081 RVA: 0x002259A4 File Offset: 0x00223BA4
	private void InitButtons()
	{
		this.buttonList.Add(new DoorToggleSideScreen.DoorButtonInfo
		{
			button = this.openButton,
			state = Door.ControlState.Opened,
			currentString = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.OPEN,
			pendingString = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.OPEN_PENDING
		});
		this.buttonList.Add(new DoorToggleSideScreen.DoorButtonInfo
		{
			button = this.autoButton,
			state = Door.ControlState.Auto,
			currentString = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.AUTO,
			pendingString = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.AUTO_PENDING
		});
		this.buttonList.Add(new DoorToggleSideScreen.DoorButtonInfo
		{
			button = this.closeButton,
			state = Door.ControlState.Locked,
			currentString = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.CLOSE,
			pendingString = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.CLOSE_PENDING
		});
		using (List<DoorToggleSideScreen.DoorButtonInfo>.Enumerator enumerator = this.buttonList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DoorToggleSideScreen.DoorButtonInfo info = enumerator.Current;
				info.button.onClick += delegate
				{
					this.target.QueueStateChange(info.state);
					this.Refresh();
				};
			}
		}
	}

	// Token: 0x06005E12 RID: 24082 RVA: 0x00225B00 File Offset: 0x00223D00
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Door>() != null;
	}

	// Token: 0x06005E13 RID: 24083 RVA: 0x00225B10 File Offset: 0x00223D10
	public override void SetTarget(GameObject target)
	{
		if (this.target != null)
		{
			this.ClearTarget();
		}
		base.SetTarget(target);
		this.target = target.GetComponent<Door>();
		this.accessTarget = target.GetComponent<AccessControl>();
		if (this.target == null)
		{
			return;
		}
		target.Subscribe(1734268753, new Action<object>(this.OnDoorStateChanged));
		target.Subscribe(-1525636549, new Action<object>(this.OnAccessControlChanged));
		this.Refresh();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005E14 RID: 24084 RVA: 0x00225BA4 File Offset: 0x00223DA4
	public override void ClearTarget()
	{
		if (this.target != null)
		{
			this.target.Unsubscribe(1734268753, new Action<object>(this.OnDoorStateChanged));
			this.target.Unsubscribe(-1525636549, new Action<object>(this.OnAccessControlChanged));
		}
		this.target = null;
	}

	// Token: 0x06005E15 RID: 24085 RVA: 0x00225C00 File Offset: 0x00223E00
	private void Refresh()
	{
		string text = null;
		string text2 = null;
		if (this.buttonList == null || this.buttonList.Count == 0)
		{
			this.InitButtons();
		}
		foreach (DoorToggleSideScreen.DoorButtonInfo doorButtonInfo in this.buttonList)
		{
			if (this.target.CurrentState == doorButtonInfo.state && this.target.RequestedState == doorButtonInfo.state)
			{
				doorButtonInfo.button.isOn = true;
				text = doorButtonInfo.currentString;
				foreach (ImageToggleState imageToggleState in doorButtonInfo.button.GetComponentsInChildren<ImageToggleState>())
				{
					imageToggleState.SetActive();
					imageToggleState.SetActive();
				}
				doorButtonInfo.button.GetComponent<ImageToggleStateThrobber>().enabled = false;
			}
			else if (this.target.RequestedState == doorButtonInfo.state)
			{
				doorButtonInfo.button.isOn = true;
				text2 = doorButtonInfo.pendingString;
				foreach (ImageToggleState imageToggleState2 in doorButtonInfo.button.GetComponentsInChildren<ImageToggleState>())
				{
					imageToggleState2.SetActive();
					imageToggleState2.SetActive();
				}
				doorButtonInfo.button.GetComponent<ImageToggleStateThrobber>().enabled = true;
			}
			else
			{
				doorButtonInfo.button.isOn = false;
				foreach (ImageToggleState imageToggleState3 in doorButtonInfo.button.GetComponentsInChildren<ImageToggleState>())
				{
					imageToggleState3.SetInactive();
					imageToggleState3.SetInactive();
				}
				doorButtonInfo.button.GetComponent<ImageToggleStateThrobber>().enabled = false;
			}
		}
		string text3 = text;
		if (text2 != null)
		{
			text3 = string.Format(UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.PENDING_FORMAT, text3, text2);
		}
		if (this.accessTarget != null && !this.accessTarget.Online)
		{
			text3 = string.Format(UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.ACCESS_FORMAT, text3, UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.ACCESS_OFFLINE);
		}
		if (this.target.building.Def.PrefabID == POIDoorInternalConfig.ID)
		{
			text3 = UI.UISIDESCREENS.DOOR_TOGGLE_SIDE_SCREEN.POI_INTERNAL;
			using (List<DoorToggleSideScreen.DoorButtonInfo>.Enumerator enumerator = this.buttonList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DoorToggleSideScreen.DoorButtonInfo doorButtonInfo2 = enumerator.Current;
					doorButtonInfo2.button.gameObject.SetActive(false);
				}
				goto IL_2A1;
			}
		}
		foreach (DoorToggleSideScreen.DoorButtonInfo doorButtonInfo3 in this.buttonList)
		{
			bool flag = doorButtonInfo3.state != Door.ControlState.Auto || this.target.allowAutoControl;
			doorButtonInfo3.button.gameObject.SetActive(flag);
		}
		IL_2A1:
		this.description.text = text3;
		this.description.gameObject.SetActive(!string.IsNullOrEmpty(text3));
		this.ContentContainer.SetActive(!this.target.isSealed);
	}

	// Token: 0x06005E16 RID: 24086 RVA: 0x00225F38 File Offset: 0x00224138
	private void OnDoorStateChanged(object data)
	{
		this.Refresh();
	}

	// Token: 0x06005E17 RID: 24087 RVA: 0x00225F40 File Offset: 0x00224140
	private void OnAccessControlChanged(object data)
	{
		this.Refresh();
	}

	// Token: 0x0400404E RID: 16462
	[SerializeField]
	private KToggle openButton;

	// Token: 0x0400404F RID: 16463
	[SerializeField]
	private KToggle autoButton;

	// Token: 0x04004050 RID: 16464
	[SerializeField]
	private KToggle closeButton;

	// Token: 0x04004051 RID: 16465
	[SerializeField]
	private LocText description;

	// Token: 0x04004052 RID: 16466
	private Door target;

	// Token: 0x04004053 RID: 16467
	private AccessControl accessTarget;

	// Token: 0x04004054 RID: 16468
	private List<DoorToggleSideScreen.DoorButtonInfo> buttonList = new List<DoorToggleSideScreen.DoorButtonInfo>();

	// Token: 0x02001A61 RID: 6753
	private struct DoorButtonInfo
	{
		// Token: 0x04007764 RID: 30564
		public KToggle button;

		// Token: 0x04007765 RID: 30565
		public Door.ControlState state;

		// Token: 0x04007766 RID: 30566
		public string currentString;

		// Token: 0x04007767 RID: 30567
		public string pendingString;
	}
}
