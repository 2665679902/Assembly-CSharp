using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B2F RID: 2863
[AddComponentMenu("KMonoBehaviour/scripts/MinionTodoChoreEntry")]
public class MinionTodoChoreEntry : KMonoBehaviour
{
	// Token: 0x06005867 RID: 22631 RVA: 0x002001BA File Offset: 0x001FE3BA
	public void SetMoreAmount(int amount)
	{
		if (amount == 0)
		{
			this.moreLabel.gameObject.SetActive(false);
			return;
		}
		this.moreLabel.text = string.Format(UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TRUNCATED_CHORES, amount);
	}

	// Token: 0x06005868 RID: 22632 RVA: 0x002001F4 File Offset: 0x001FE3F4
	public void Apply(Chore.Precondition.Context context)
	{
		ChoreConsumer consumer = context.consumerState.consumer;
		if (this.targetChore == context.chore && context.chore.target == this.lastChoreTarget && context.chore.masterPriority == this.lastPrioritySetting)
		{
			return;
		}
		this.targetChore = context.chore;
		this.lastChoreTarget = context.chore.target;
		this.lastPrioritySetting = context.chore.masterPriority;
		string choreName = GameUtil.GetChoreName(context.chore, context.data);
		string text = GameUtil.ChoreGroupsForChoreType(context.chore.choreType);
		string text2 = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.CHORE_TARGET;
		text2 = text2.Replace("{Target}", (context.chore.target.gameObject == consumer.gameObject) ? UI.UISIDESCREENS.MINIONTODOSIDESCREEN.SELF_LABEL.text : context.chore.target.gameObject.GetProperName());
		if (text != null)
		{
			text2 = text2.Replace("{Groups}", text);
		}
		string text3 = ((context.chore.masterPriority.priority_class == PriorityScreen.PriorityClass.basic) ? context.chore.masterPriority.priority_value.ToString() : "");
		Sprite sprite = ((context.chore.masterPriority.priority_class == PriorityScreen.PriorityClass.basic) ? this.prioritySprites[context.chore.masterPriority.priority_value - 1] : null);
		ChoreGroup choreGroup = MinionTodoChoreEntry.BestPriorityGroup(context, consumer);
		this.icon.sprite = ((choreGroup != null) ? Assets.GetSprite(choreGroup.sprite) : null);
		this.label.SetText(choreName);
		this.subLabel.SetText(text2);
		this.priorityLabel.SetText(text3);
		this.priorityIcon.sprite = sprite;
		this.moreLabel.text = "";
		base.GetComponent<ToolTip>().SetSimpleTooltip(MinionTodoChoreEntry.TooltipForChore(context, consumer));
		KButton componentInChildren = base.GetComponentInChildren<KButton>();
		componentInChildren.ClearOnClick();
		if (componentInChildren.bgImage != null)
		{
			componentInChildren.bgImage.colorStyleSetting = ((context.chore.driver == consumer.choreDriver) ? this.buttonColorSettingCurrent : this.buttonColorSettingStandard);
			componentInChildren.bgImage.ApplyColorStyleSetting();
		}
		GameObject gameObject = context.chore.target.gameObject;
		componentInChildren.ClearOnPointerEvents();
		componentInChildren.GetComponentInChildren<KButton>().onClick += delegate
		{
			if (context.chore != null && !context.chore.target.isNull)
			{
				Vector3 vector = new Vector3(context.chore.target.gameObject.transform.position.x, context.chore.target.gameObject.transform.position.y + 1f, CameraController.Instance.transform.position.z);
				CameraController.Instance.SetTargetPos(vector, 10f, true);
			}
		};
	}

	// Token: 0x06005869 RID: 22633 RVA: 0x002004E4 File Offset: 0x001FE6E4
	private static ChoreGroup BestPriorityGroup(Chore.Precondition.Context context, ChoreConsumer choreConsumer)
	{
		ChoreGroup choreGroup = null;
		if (context.chore.choreType.groups.Length != 0)
		{
			choreGroup = context.chore.choreType.groups[0];
			for (int i = 1; i < context.chore.choreType.groups.Length; i++)
			{
				if (choreConsumer.GetPersonalPriority(choreGroup) < choreConsumer.GetPersonalPriority(context.chore.choreType.groups[i]))
				{
					choreGroup = context.chore.choreType.groups[i];
				}
			}
		}
		return choreGroup;
	}

	// Token: 0x0600586A RID: 22634 RVA: 0x0020056C File Offset: 0x001FE76C
	private static string TooltipForChore(Chore.Precondition.Context context, ChoreConsumer choreConsumer)
	{
		bool flag = context.chore.masterPriority.priority_class == PriorityScreen.PriorityClass.basic || context.chore.masterPriority.priority_class == PriorityScreen.PriorityClass.high;
		string text;
		switch (context.chore.masterPriority.priority_class)
		{
		case PriorityScreen.PriorityClass.idle:
			text = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_IDLE;
			goto IL_9D;
		case PriorityScreen.PriorityClass.personalNeeds:
			text = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_PERSONAL;
			goto IL_9D;
		case PriorityScreen.PriorityClass.topPriority:
			text = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_EMERGENCY;
			goto IL_9D;
		case PriorityScreen.PriorityClass.compulsory:
			text = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_COMPULSORY;
			goto IL_9D;
		}
		text = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_NORMAL;
		IL_9D:
		float num = 0f;
		int num2 = (int)(context.chore.masterPriority.priority_class * (PriorityScreen.PriorityClass)100);
		num += (float)num2;
		int num3 = (flag ? choreConsumer.GetPersonalPriority(context.chore.choreType) : 0);
		num += (float)(num3 * 10);
		int num4 = (flag ? context.chore.masterPriority.priority_value : 0);
		num += (float)num4;
		float num5 = (float)context.priority / 10000f;
		num += num5;
		text = text.Replace("{Description}", (context.chore.driver == choreConsumer.choreDriver) ? UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_DESC_ACTIVE : UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_DESC_INACTIVE);
		text = text.Replace("{IdleDescription}", (context.chore.driver == choreConsumer.choreDriver) ? UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_IDLEDESC_ACTIVE : UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_IDLEDESC_INACTIVE);
		string text2 = GameUtil.ChoreGroupsForChoreType(context.chore.choreType);
		ChoreGroup choreGroup = MinionTodoChoreEntry.BestPriorityGroup(context, choreConsumer);
		text = text.Replace("{Name}", choreConsumer.name);
		text = text.Replace("{Errand}", GameUtil.GetChoreName(context.chore, context.data));
		text = text.Replace("{Groups}", text2);
		text = text.Replace("{BestGroup}", (choreGroup != null) ? choreGroup.Name : context.chore.choreType.Name);
		text = text.Replace("{ClassPriority}", num2.ToString());
		text = text.Replace("{PersonalPriority}", JobsTableScreen.priorityInfo[num3].name.text);
		text = text.Replace("{PersonalPriorityValue}", (num3 * 10).ToString());
		text = text.Replace("{Building}", context.chore.gameObject.GetProperName());
		text = text.Replace("{BuildingPriority}", num4.ToString());
		text = text.Replace("{TypePriority}", num5.ToString());
		return text.Replace("{TotalPriority}", num.ToString());
	}

	// Token: 0x04003BCE RID: 15310
	public Image icon;

	// Token: 0x04003BCF RID: 15311
	public Image priorityIcon;

	// Token: 0x04003BD0 RID: 15312
	public LocText priorityLabel;

	// Token: 0x04003BD1 RID: 15313
	public LocText label;

	// Token: 0x04003BD2 RID: 15314
	public LocText subLabel;

	// Token: 0x04003BD3 RID: 15315
	public LocText moreLabel;

	// Token: 0x04003BD4 RID: 15316
	public List<Sprite> prioritySprites;

	// Token: 0x04003BD5 RID: 15317
	[SerializeField]
	private ColorStyleSetting buttonColorSettingCurrent;

	// Token: 0x04003BD6 RID: 15318
	[SerializeField]
	private ColorStyleSetting buttonColorSettingStandard;

	// Token: 0x04003BD7 RID: 15319
	private Chore targetChore;

	// Token: 0x04003BD8 RID: 15320
	private IStateMachineTarget lastChoreTarget;

	// Token: 0x04003BD9 RID: 15321
	private PrioritySetting lastPrioritySetting;
}
