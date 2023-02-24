using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BDD RID: 3037
public class SelfDestructButtonSideScreen : SideScreenContent
{
	// Token: 0x06005FB8 RID: 24504 RVA: 0x00231109 File Offset: 0x0022F309
	protected override void OnSpawn()
	{
		this.Refresh();
		this.button.onClick += this.TriggerDestruct;
	}

	// Token: 0x06005FB9 RID: 24505 RVA: 0x00231128 File Offset: 0x0022F328
	public override int GetSideScreenSortOrder()
	{
		return -150;
	}

	// Token: 0x06005FBA RID: 24506 RVA: 0x0023112F File Offset: 0x0022F32F
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<CraftModuleInterface>() != null && target.HasTag(GameTags.RocketInSpace);
	}

	// Token: 0x06005FBB RID: 24507 RVA: 0x0023114C File Offset: 0x0022F34C
	public override void SetTarget(GameObject target)
	{
		this.craftInterface = target.GetComponent<CraftModuleInterface>();
		this.acknowledgeWarnings = false;
		this.craftInterface.Subscribe<SelfDestructButtonSideScreen>(-1582839653, SelfDestructButtonSideScreen.TagsChangedDelegate);
		this.Refresh();
	}

	// Token: 0x06005FBC RID: 24508 RVA: 0x0023117D File Offset: 0x0022F37D
	public override void ClearTarget()
	{
		if (this.craftInterface != null)
		{
			this.craftInterface.Unsubscribe<SelfDestructButtonSideScreen>(-1582839653, SelfDestructButtonSideScreen.TagsChangedDelegate, false);
			this.craftInterface = null;
		}
	}

	// Token: 0x06005FBD RID: 24509 RVA: 0x002311AA File Offset: 0x0022F3AA
	private void OnTagsChanged(object data)
	{
		if (((TagChangedEventData)data).tag == GameTags.RocketStranded)
		{
			this.Refresh();
		}
	}

	// Token: 0x06005FBE RID: 24510 RVA: 0x002311C9 File Offset: 0x0022F3C9
	private void TriggerDestruct()
	{
		if (this.acknowledgeWarnings)
		{
			this.craftInterface.gameObject.Trigger(-1061799784, null);
			this.acknowledgeWarnings = false;
		}
		else
		{
			this.acknowledgeWarnings = true;
		}
		this.Refresh();
	}

	// Token: 0x06005FBF RID: 24511 RVA: 0x00231200 File Offset: 0x0022F400
	private void Refresh()
	{
		if (this.craftInterface == null)
		{
			return;
		}
		this.statusText.text = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.MESSAGE_TEXT;
		if (this.acknowledgeWarnings)
		{
			this.button.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TEXT_CONFIRM;
			this.button.GetComponentInChildren<ToolTip>().toolTip = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TOOLTIP_CONFIRM;
			return;
		}
		this.button.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TEXT;
		this.button.GetComponentInChildren<ToolTip>().toolTip = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TOOLTIP;
	}

	// Token: 0x04004199 RID: 16793
	public KButton button;

	// Token: 0x0400419A RID: 16794
	public LocText statusText;

	// Token: 0x0400419B RID: 16795
	private CraftModuleInterface craftInterface;

	// Token: 0x0400419C RID: 16796
	private bool acknowledgeWarnings;

	// Token: 0x0400419D RID: 16797
	private static readonly EventSystem.IntraObjectHandler<SelfDestructButtonSideScreen> TagsChangedDelegate = new EventSystem.IntraObjectHandler<SelfDestructButtonSideScreen>(delegate(SelfDestructButtonSideScreen cmp, object data)
	{
		cmp.OnTagsChanged(data);
	});
}
