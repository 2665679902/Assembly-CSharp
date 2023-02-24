using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005AA RID: 1450
[AddComponentMenu("KMonoBehaviour/scripts/DirectionControl")]
public class DirectionControl : KMonoBehaviour
{
	// Token: 0x060023C2 RID: 9154 RVA: 0x000C1324 File Offset: 0x000BF524
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.allowedDirection = WorkableReactable.AllowedDirection.Any;
		this.directionInfos = new DirectionControl.DirectionInfo[]
		{
			new DirectionControl.DirectionInfo
			{
				allowLeft = true,
				allowRight = true,
				iconName = "action_direction_both",
				name = UI.USERMENUACTIONS.WORKABLE_DIRECTION_BOTH.NAME,
				tooltip = UI.USERMENUACTIONS.WORKABLE_DIRECTION_BOTH.TOOLTIP
			},
			new DirectionControl.DirectionInfo
			{
				allowLeft = true,
				allowRight = false,
				iconName = "action_direction_left",
				name = UI.USERMENUACTIONS.WORKABLE_DIRECTION_LEFT.NAME,
				tooltip = UI.USERMENUACTIONS.WORKABLE_DIRECTION_LEFT.TOOLTIP
			},
			new DirectionControl.DirectionInfo
			{
				allowLeft = false,
				allowRight = true,
				iconName = "action_direction_right",
				name = UI.USERMENUACTIONS.WORKABLE_DIRECTION_RIGHT.NAME,
				tooltip = UI.USERMENUACTIONS.WORKABLE_DIRECTION_RIGHT.TOOLTIP
			}
		};
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.DirectionControl, this);
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x000C1450 File Offset: 0x000BF650
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.SetAllowedDirection(this.allowedDirection);
		base.Subscribe<DirectionControl>(493375141, DirectionControl.OnRefreshUserMenuDelegate);
		base.Subscribe<DirectionControl>(-905833192, DirectionControl.OnCopySettingsDelegate);
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x000C1488 File Offset: 0x000BF688
	private void SetAllowedDirection(WorkableReactable.AllowedDirection new_direction)
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		DirectionControl.DirectionInfo directionInfo = this.directionInfos[(int)new_direction];
		bool flag = directionInfo.allowLeft && directionInfo.allowRight;
		bool flag2 = !flag && directionInfo.allowLeft;
		bool flag3 = !flag && directionInfo.allowRight;
		component.SetSymbolVisiblity("arrow2", flag);
		component.SetSymbolVisiblity("arrow_left", flag2);
		component.SetSymbolVisiblity("arrow_right", flag3);
		if (new_direction != this.allowedDirection)
		{
			this.allowedDirection = new_direction;
			if (this.onDirectionChanged != null)
			{
				this.onDirectionChanged(this.allowedDirection);
			}
		}
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x000C152F File Offset: 0x000BF72F
	private void OnChangeWorkableDirection()
	{
		this.SetAllowedDirection((WorkableReactable.AllowedDirection.Left + (int)this.allowedDirection) % (WorkableReactable.AllowedDirection)this.directionInfos.Length);
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x000C1548 File Offset: 0x000BF748
	private void OnCopySettings(object data)
	{
		DirectionControl component = ((GameObject)data).GetComponent<DirectionControl>();
		this.SetAllowedDirection(component.allowedDirection);
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x000C1570 File Offset: 0x000BF770
	private void OnRefreshUserMenu(object data)
	{
		int num = (int)((WorkableReactable.AllowedDirection.Left + (int)this.allowedDirection) % (WorkableReactable.AllowedDirection)this.directionInfos.Length);
		DirectionControl.DirectionInfo directionInfo = this.directionInfos[num];
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo(directionInfo.iconName, directionInfo.name, new System.Action(this.OnChangeWorkableDirection), global::Action.NumActions, null, null, null, directionInfo.tooltip, true), 0.4f);
	}

	// Token: 0x0400147C RID: 5244
	[Serialize]
	public WorkableReactable.AllowedDirection allowedDirection;

	// Token: 0x0400147D RID: 5245
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400147E RID: 5246
	private DirectionControl.DirectionInfo[] directionInfos;

	// Token: 0x0400147F RID: 5247
	public Action<WorkableReactable.AllowedDirection> onDirectionChanged;

	// Token: 0x04001480 RID: 5248
	private static readonly EventSystem.IntraObjectHandler<DirectionControl> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<DirectionControl>(delegate(DirectionControl component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04001481 RID: 5249
	private static readonly EventSystem.IntraObjectHandler<DirectionControl> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<DirectionControl>(delegate(DirectionControl component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x020011DF RID: 4575
	private struct DirectionInfo
	{
		// Token: 0x04005C47 RID: 23623
		public bool allowLeft;

		// Token: 0x04005C48 RID: 23624
		public bool allowRight;

		// Token: 0x04005C49 RID: 23625
		public string iconName;

		// Token: 0x04005C4A RID: 23626
		public string name;

		// Token: 0x04005C4B RID: 23627
		public string tooltip;
	}
}
