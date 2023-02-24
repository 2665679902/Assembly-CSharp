using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000659 RID: 1625
[AddComponentMenu("KMonoBehaviour/Workable/Telescope")]
public class Telescope : Workable, OxygenBreather.IGasProvider, IGameObjectEffectDescriptor, ISim200ms
{
	// Token: 0x06002B89 RID: 11145 RVA: 0x000E4DC4 File Offset: 0x000E2FC4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.ALL_DAY_EXPERIENCE;
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x000E4E1C File Offset: 0x000E301C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SpacecraftManager.instance.Subscribe(532901469, new Action<object>(this.UpdateWorkingState));
		Components.Telescopes.Add(this);
		if (Telescope.reducedVisibilityStatusItem == null)
		{
			Telescope.reducedVisibilityStatusItem = new StatusItem("SPACE_VISIBILITY_REDUCED", "BUILDING", "status_item_no_sky", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			Telescope.reducedVisibilityStatusItem.resolveStringCallback = new Func<string, object, string>(Telescope.GetStatusItemString);
			Telescope.noVisibilityStatusItem = new StatusItem("SPACE_VISIBILITY_NONE", "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			Telescope.noVisibilityStatusItem.resolveStringCallback = new Func<string, object, string>(Telescope.GetStatusItemString);
		}
		this.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(this.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
		this.operational = base.GetComponent<Operational>();
		this.storage = base.GetComponent<Storage>();
		this.UpdateWorkingState(null);
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x000E4F1A File Offset: 0x000E311A
	protected override void OnCleanUp()
	{
		Components.Telescopes.Remove(this);
		SpacecraftManager.instance.Unsubscribe(532901469, new Action<object>(this.UpdateWorkingState));
		base.OnCleanUp();
	}

	// Token: 0x06002B8C RID: 11148 RVA: 0x000E4F48 File Offset: 0x000E3148
	public void Sim200ms(float dt)
	{
		Extents extents = base.GetComponent<Building>().GetExtents();
		int num;
		bool flag = Grid.IsRangeExposedToSunlight(Grid.XYToCell(extents.x, extents.y), this.clearScanCellRadius, new CellOffset(1, 0), out num, 1);
		this.percentClear = (float)num / (float)(this.clearScanCellRadius * 2 + 1);
		KSelectable component = base.GetComponent<KSelectable>();
		Operational component2 = base.GetComponent<Operational>();
		component.ToggleStatusItem(Telescope.noVisibilityStatusItem, !flag, this);
		component.ToggleStatusItem(Telescope.reducedVisibilityStatusItem, flag && this.percentClear < 1f, this);
		component2.SetFlag(Telescope.visibleSkyFlag, flag);
		if (!component2.IsActive && component2.IsOperational && this.chore == null)
		{
			this.chore = this.CreateChore();
			base.SetWorkTime(float.PositiveInfinity);
		}
	}

	// Token: 0x06002B8D RID: 11149 RVA: 0x000E5014 File Offset: 0x000E3214
	private static string GetStatusItemString(string src_str, object data)
	{
		Telescope telescope = (Telescope)data;
		return src_str.Replace("{VISIBILITY}", GameUtil.GetFormattedPercent(telescope.percentClear * 100f, GameUtil.TimeSlice.None)).Replace("{RADIUS}", telescope.clearScanCellRadius.ToString());
	}

	// Token: 0x06002B8E RID: 11150 RVA: 0x000E505C File Offset: 0x000E325C
	private void OnWorkableEvent(Workable workable, Workable.WorkableEvent ev)
	{
		Worker worker = base.worker;
		if (worker == null)
		{
			return;
		}
		OxygenBreather component = worker.GetComponent<OxygenBreather>();
		KPrefabID component2 = worker.GetComponent<KPrefabID>();
		if (ev == Workable.WorkableEvent.WorkStarted)
		{
			base.ShowProgressBar(true);
			this.progressBar.SetUpdateFunc(delegate
			{
				if (SpacecraftManager.instance.HasAnalysisTarget())
				{
					return SpacecraftManager.instance.GetDestinationAnalysisScore(SpacecraftManager.instance.GetStarmapAnalysisDestinationID()) / (float)ROCKETRY.DESTINATION_ANALYSIS.COMPLETE;
				}
				return 0f;
			});
			this.workerGasProvider = component.GetGasProvider();
			component.SetGasProvider(this);
			component.GetComponent<CreatureSimTemperatureTransfer>().enabled = false;
			component2.AddTag(GameTags.Shaded, false);
			return;
		}
		if (ev != Workable.WorkableEvent.WorkStopped)
		{
			return;
		}
		component.SetGasProvider(this.workerGasProvider);
		component.GetComponent<CreatureSimTemperatureTransfer>().enabled = true;
		base.ShowProgressBar(false);
		component2.RemoveTag(GameTags.Shaded);
	}

	// Token: 0x06002B8F RID: 11151 RVA: 0x000E5118 File Offset: 0x000E3318
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (SpacecraftManager.instance.HasAnalysisTarget())
		{
			int starmapAnalysisDestinationID = SpacecraftManager.instance.GetStarmapAnalysisDestinationID();
			SpaceDestination destination = SpacecraftManager.instance.GetDestination(starmapAnalysisDestinationID);
			float num = 1f / (float)destination.OneBasedDistance;
			float num2 = (float)ROCKETRY.DESTINATION_ANALYSIS.DISCOVERED;
			float default_CYCLES_PER_DISCOVERY = ROCKETRY.DESTINATION_ANALYSIS.DEFAULT_CYCLES_PER_DISCOVERY;
			float num3 = num2 / default_CYCLES_PER_DISCOVERY / 600f;
			float num4 = dt * num * num3;
			SpacecraftManager.instance.EarnDestinationAnalysisPoints(starmapAnalysisDestinationID, num4);
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06002B90 RID: 11152 RVA: 0x000E518C File Offset: 0x000E338C
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		Element element = ElementLoader.FindElementByHash(SimHashes.Oxygen);
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(element.tag.ProperName(), string.Format(STRINGS.BUILDINGS.PREFABS.TELESCOPE.REQUIREMENT_TOOLTIP, element.tag.ProperName()), Descriptor.DescriptorType.Requirement);
		descriptors.Add(descriptor);
		return descriptors;
	}

	// Token: 0x06002B91 RID: 11153 RVA: 0x000E51E8 File Offset: 0x000E33E8
	protected Chore CreateChore()
	{
		WorkChore<Telescope> workChore = new WorkChore<Telescope>(Db.Get().ChoreTypes.Research, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		workChore.AddPrecondition(Telescope.ContainsOxygen, null);
		return workChore;
	}

	// Token: 0x06002B92 RID: 11154 RVA: 0x000E5228 File Offset: 0x000E3428
	protected void UpdateWorkingState(object data)
	{
		bool flag = false;
		if (SpacecraftManager.instance.HasAnalysisTarget() && SpacecraftManager.instance.GetDestinationAnalysisState(SpacecraftManager.instance.GetDestination(SpacecraftManager.instance.GetStarmapAnalysisDestinationID())) != SpacecraftManager.DestinationAnalysisState.Complete)
		{
			flag = true;
		}
		KSelectable component = base.GetComponent<KSelectable>();
		bool flag2 = !flag && !SpacecraftManager.instance.AreAllDestinationsAnalyzed();
		component.ToggleStatusItem(Db.Get().BuildingStatusItems.NoApplicableAnalysisSelected, flag2, null);
		this.operational.SetFlag(this.flag, flag);
		if (!flag && base.worker)
		{
			base.StopWork(base.worker, true);
		}
	}

	// Token: 0x06002B93 RID: 11155 RVA: 0x000E52C6 File Offset: 0x000E34C6
	public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
	{
	}

	// Token: 0x06002B94 RID: 11156 RVA: 0x000E52C8 File Offset: 0x000E34C8
	public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
	{
	}

	// Token: 0x06002B95 RID: 11157 RVA: 0x000E52CA File Offset: 0x000E34CA
	public bool ShouldEmitCO2()
	{
		return false;
	}

	// Token: 0x06002B96 RID: 11158 RVA: 0x000E52CD File Offset: 0x000E34CD
	public bool ShouldStoreCO2()
	{
		return false;
	}

	// Token: 0x06002B97 RID: 11159 RVA: 0x000E52D0 File Offset: 0x000E34D0
	public bool ConsumeGas(OxygenBreather oxygen_breather, float amount)
	{
		if (this.storage.items.Count <= 0)
		{
			return false;
		}
		GameObject gameObject = this.storage.items[0];
		if (gameObject == null)
		{
			return false;
		}
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		bool flag = component.Mass >= amount;
		component.Mass = Mathf.Max(0f, component.Mass - amount);
		return flag;
	}

	// Token: 0x040019BF RID: 6591
	public int clearScanCellRadius = 15;

	// Token: 0x040019C0 RID: 6592
	private OxygenBreather.IGasProvider workerGasProvider;

	// Token: 0x040019C1 RID: 6593
	private Operational operational;

	// Token: 0x040019C2 RID: 6594
	private float percentClear;

	// Token: 0x040019C3 RID: 6595
	private static readonly Operational.Flag visibleSkyFlag = new Operational.Flag("VisibleSky", Operational.Flag.Type.Requirement);

	// Token: 0x040019C4 RID: 6596
	private static StatusItem reducedVisibilityStatusItem;

	// Token: 0x040019C5 RID: 6597
	private static StatusItem noVisibilityStatusItem;

	// Token: 0x040019C6 RID: 6598
	private Storage storage;

	// Token: 0x040019C7 RID: 6599
	public static readonly Chore.Precondition ContainsOxygen = new Chore.Precondition
	{
		id = "ContainsOxygen",
		sortOrder = 1,
		description = DUPLICANTS.CHORES.PRECONDITIONS.CONTAINS_OXYGEN,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.chore.target.GetComponent<Storage>().FindFirstWithMass(GameTags.Oxygen, 0f) != null;
		}
	};

	// Token: 0x040019C8 RID: 6600
	private Chore chore;

	// Token: 0x040019C9 RID: 6601
	private Operational.Flag flag = new Operational.Flag("ValidTarget", Operational.Flag.Type.Requirement);
}
