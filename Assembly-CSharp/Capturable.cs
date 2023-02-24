using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020006C3 RID: 1731
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Capturable")]
public class Capturable : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x1700034E RID: 846
	// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000F8EFB File Offset: 0x000F70FB
	public bool IsMarkedForCapture
	{
		get
		{
			return this.markedForCapture;
		}
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x000F8F04 File Offset: 0x000F7104
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Capturables.Add(this);
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.attributeConverter = Db.Get().AttributeConverters.CapturableSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Ranching.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanWrangleCreatures.Id;
		this.resetProgressOnStop = true;
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.multitoolContext = "capture";
		this.multitoolHitEffectTag = "fx_capture_splash";
	}

	// Token: 0x06002F18 RID: 12056 RVA: 0x000F8FC4 File Offset: 0x000F71C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Capturable>(1623392196, Capturable.OnDeathDelegate);
		base.Subscribe<Capturable>(493375141, Capturable.OnRefreshUserMenuDelegate);
		base.Subscribe<Capturable>(-1582839653, Capturable.OnTagsChangedDelegate);
		if (this.markedForCapture)
		{
			Prioritizable.AddRef(base.gameObject);
		}
		this.UpdateStatusItem();
		this.UpdateChore();
		base.SetWorkTime(10f);
	}

	// Token: 0x06002F19 RID: 12057 RVA: 0x000F9034 File Offset: 0x000F7234
	protected override void OnCleanUp()
	{
		Components.Capturables.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002F1A RID: 12058 RVA: 0x000F9048 File Offset: 0x000F7248
	public override Vector3 GetTargetPoint()
	{
		Vector3 vector = base.transform.GetPosition();
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		if (component != null)
		{
			vector = component.bounds.center;
		}
		vector.z = 0f;
		return vector;
	}

	// Token: 0x06002F1B RID: 12059 RVA: 0x000F908D File Offset: 0x000F728D
	private void OnDeath(object data)
	{
		this.allowCapture = false;
		this.markedForCapture = false;
		this.UpdateChore();
	}

	// Token: 0x06002F1C RID: 12060 RVA: 0x000F90A3 File Offset: 0x000F72A3
	private void OnTagsChanged(object data)
	{
		this.MarkForCapture(this.markedForCapture);
	}

	// Token: 0x06002F1D RID: 12061 RVA: 0x000F90B4 File Offset: 0x000F72B4
	public void MarkForCapture(bool mark)
	{
		PrioritySetting prioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);
		this.MarkForCapture(mark, prioritySetting, false);
	}

	// Token: 0x06002F1E RID: 12062 RVA: 0x000F90D4 File Offset: 0x000F72D4
	public void MarkForCapture(bool mark, PrioritySetting priority, bool updateMarkedPriority = false)
	{
		mark = mark && this.IsCapturable();
		if (this.markedForCapture && !mark)
		{
			Prioritizable.RemoveRef(base.gameObject);
		}
		else if (!this.markedForCapture && mark)
		{
			Prioritizable.AddRef(base.gameObject);
			Prioritizable component = base.GetComponent<Prioritizable>();
			if (component)
			{
				component.SetMasterPriority(priority);
			}
		}
		else if (updateMarkedPriority && this.markedForCapture && mark)
		{
			Prioritizable component2 = base.GetComponent<Prioritizable>();
			if (component2)
			{
				component2.SetMasterPriority(priority);
			}
		}
		this.markedForCapture = mark;
		this.UpdateStatusItem();
		this.UpdateChore();
	}

	// Token: 0x06002F1F RID: 12063 RVA: 0x000F9170 File Offset: 0x000F7370
	public bool IsCapturable()
	{
		return this.allowCapture && !base.gameObject.HasTag(GameTags.Trapped) && !base.gameObject.HasTag(GameTags.Stored) && !base.gameObject.HasTag(GameTags.Creatures.Bagged);
	}

	// Token: 0x06002F20 RID: 12064 RVA: 0x000F91C4 File Offset: 0x000F73C4
	private void OnRefreshUserMenu(object data)
	{
		if (!this.IsCapturable())
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((!this.markedForCapture) ? new KIconButtonMenu.ButtonInfo("action_capture", UI.USERMENUACTIONS.CAPTURE.NAME, delegate
		{
			this.MarkForCapture(true);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CAPTURE.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_capture", UI.USERMENUACTIONS.CANCELCAPTURE.NAME, delegate
		{
			this.MarkForCapture(false);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELCAPTURE.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06002F21 RID: 12065 RVA: 0x000F9268 File Offset: 0x000F7468
	private void UpdateStatusItem()
	{
		this.shouldShowSkillPerkStatusItem = this.markedForCapture;
		base.UpdateStatusItem(null);
		if (this.markedForCapture)
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.OrderCapture, this);
			return;
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.OrderCapture, false);
	}

	// Token: 0x06002F22 RID: 12066 RVA: 0x000F92CC File Offset: 0x000F74CC
	private void UpdateChore()
	{
		if (this.markedForCapture && this.chore == null)
		{
			this.chore = new WorkChore<Capturable>(Db.Get().ChoreTypes.Capture, this, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			return;
		}
		if (!this.markedForCapture && this.chore != null)
		{
			this.chore.Cancel("not marked for capture");
			this.chore = null;
		}
	}

	// Token: 0x06002F23 RID: 12067 RVA: 0x000F933D File Offset: 0x000F753D
	protected override void OnStartWork(Worker worker)
	{
		base.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.Stunned, false);
	}

	// Token: 0x06002F24 RID: 12068 RVA: 0x000F9350 File Offset: 0x000F7550
	protected override void OnStopWork(Worker worker)
	{
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.Creatures.Stunned);
	}

	// Token: 0x06002F25 RID: 12069 RVA: 0x000F9364 File Offset: 0x000F7564
	protected override void OnCompleteWork(Worker worker)
	{
		int num = this.NaturalBuildingCell();
		if (Grid.Solid[num])
		{
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
			{
				num = num2;
			}
		}
		this.MarkForCapture(false);
		this.baggable.SetWrangled();
		this.baggable.transform.SetPosition(Grid.CellToPosCCC(num, Grid.SceneLayer.Ore));
	}

	// Token: 0x06002F26 RID: 12070 RVA: 0x000F93D0 File Offset: 0x000F75D0
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (this.allowCapture)
		{
			descriptors.Add(new Descriptor(UI.BUILDINGEFFECTS.CAPTURE_METHOD_WRANGLE, UI.BUILDINGEFFECTS.TOOLTIPS.CAPTURE_METHOD_WRANGLE, Descriptor.DescriptorType.Effect, false));
		}
		return descriptors;
	}

	// Token: 0x04001C53 RID: 7251
	[MyCmpAdd]
	private Baggable baggable;

	// Token: 0x04001C54 RID: 7252
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x04001C55 RID: 7253
	public bool allowCapture = true;

	// Token: 0x04001C56 RID: 7254
	[Serialize]
	private bool markedForCapture;

	// Token: 0x04001C57 RID: 7255
	private Chore chore;

	// Token: 0x04001C58 RID: 7256
	private static readonly EventSystem.IntraObjectHandler<Capturable> OnDeathDelegate = new EventSystem.IntraObjectHandler<Capturable>(delegate(Capturable component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x04001C59 RID: 7257
	private static readonly EventSystem.IntraObjectHandler<Capturable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Capturable>(delegate(Capturable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04001C5A RID: 7258
	private static readonly EventSystem.IntraObjectHandler<Capturable> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Capturable>(delegate(Capturable component, object data)
	{
		component.OnTagsChanged(data);
	});
}
