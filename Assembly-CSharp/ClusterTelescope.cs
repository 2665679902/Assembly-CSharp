using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000587 RID: 1415
public class ClusterTelescope : GameStateMachine<ClusterTelescope, ClusterTelescope.Instance, IStateMachineTarget, ClusterTelescope.Def>
{
	// Token: 0x06002283 RID: 8835 RVA: 0x000BB210 File Offset: 0x000B9410
	private static string GetStatusItemString(string src_str, object data)
	{
		ClusterTelescope.Instance instance = (ClusterTelescope.Instance)data;
		return src_str.Replace("{VISIBILITY}", GameUtil.GetFormattedPercent(instance.PercentClear * 100f, GameUtil.TimeSlice.None)).Replace("{RADIUS}", instance.def.clearScanCellRadius.ToString());
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x000BB25C File Offset: 0x000B945C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.ready.no_visibility;
		this.ready.EventTransition(GameHashes.ClusterFogOfWarRevealed, (ClusterTelescope.Instance smi) => Game.Instance, this.all_work_complete, (ClusterTelescope.Instance smi) => !smi.CheckHasAnalyzeTarget());
		this.ready.no_visibility.UpdateTransition(this.ready.ready_to_work, (ClusterTelescope.Instance smi, float dt) => smi.HasSkyVisibility(), UpdateRate.SIM_200ms, false).ToggleStatusItem(ClusterTelescope.noVisibilityStatusItem, null);
		this.ready.ready_to_work.UpdateTransition(this.ready.no_visibility, (ClusterTelescope.Instance smi, float dt) => !smi.HasSkyVisibility(), UpdateRate.SIM_200ms, false).ToggleChore((ClusterTelescope.Instance smi) => smi.CreateChore(), this.ready.no_visibility);
		this.all_work_complete.ToggleMainStatusItem(Db.Get().BuildingStatusItems.ClusterTelescopeAllWorkComplete, null).EventTransition(GameHashes.ClusterLocationChanged, (ClusterTelescope.Instance smi) => Game.Instance, this.ready.no_visibility, (ClusterTelescope.Instance smi) => smi.CheckHasAnalyzeTarget());
	}

	// Token: 0x040013E7 RID: 5095
	private static StatusItem noVisibilityStatusItem = new StatusItem("SPACE_VISIBILITY_NONE", "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, new Func<string, object, string>(ClusterTelescope.GetStatusItemString));

	// Token: 0x040013E8 RID: 5096
	public GameStateMachine<ClusterTelescope, ClusterTelescope.Instance, IStateMachineTarget, ClusterTelescope.Def>.State all_work_complete;

	// Token: 0x040013E9 RID: 5097
	public ClusterTelescope.ReadyStates ready;

	// Token: 0x020011B7 RID: 4535
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005BB9 RID: 23481
		public int clearScanCellRadius = 15;

		// Token: 0x04005BBA RID: 23482
		public int analyzeClusterRadius = 3;

		// Token: 0x04005BBB RID: 23483
		public KAnimFile[] workableOverrideAnims;

		// Token: 0x04005BBC RID: 23484
		public bool providesOxygen;
	}

	// Token: 0x020011B8 RID: 4536
	public class ReadyStates : GameStateMachine<ClusterTelescope, ClusterTelescope.Instance, IStateMachineTarget, ClusterTelescope.Def>.State
	{
		// Token: 0x04005BBD RID: 23485
		public GameStateMachine<ClusterTelescope, ClusterTelescope.Instance, IStateMachineTarget, ClusterTelescope.Def>.State no_visibility;

		// Token: 0x04005BBE RID: 23486
		public GameStateMachine<ClusterTelescope, ClusterTelescope.Instance, IStateMachineTarget, ClusterTelescope.Def>.State ready_to_work;
	}

	// Token: 0x020011B9 RID: 4537
	public new class Instance : GameStateMachine<ClusterTelescope, ClusterTelescope.Instance, IStateMachineTarget, ClusterTelescope.Def>.GameInstance
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060077AC RID: 30636 RVA: 0x002BBEF3 File Offset: 0x002BA0F3
		public float PercentClear
		{
			get
			{
				return this.m_percentClear;
			}
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x002BBEFB File Offset: 0x002BA0FB
		public Instance(IStateMachineTarget smi, ClusterTelescope.Def def)
			: base(smi, def)
		{
			this.workableOverrideAnims = def.workableOverrideAnims;
			this.providesOxygen = def.providesOxygen;
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x002BBF20 File Offset: 0x002BA120
		public bool CheckHasAnalyzeTarget()
		{
			ClusterFogOfWarManager.Instance smi = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
			if (this.m_hasAnalyzeTarget && !smi.IsLocationRevealed(this.m_analyzeTarget))
			{
				return true;
			}
			AxialI myWorldLocation = this.GetMyWorldLocation();
			this.m_hasAnalyzeTarget = smi.GetUnrevealedLocationWithinRadius(myWorldLocation, base.def.analyzeClusterRadius, out this.m_analyzeTarget);
			return this.m_hasAnalyzeTarget;
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x002BBF7C File Offset: 0x002BA17C
		public Chore CreateChore()
		{
			WorkChore<ClusterTelescope.ClusterTelescopeWorkable> workChore = new WorkChore<ClusterTelescope.ClusterTelescopeWorkable>(Db.Get().ChoreTypes.Research, this.m_workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			if (this.providesOxygen)
			{
				workChore.AddPrecondition(Telescope.ContainsOxygen, null);
			}
			return workChore;
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x002BBFCA File Offset: 0x002BA1CA
		public AxialI GetAnalyzeTarget()
		{
			global::Debug.Assert(this.m_hasAnalyzeTarget, "GetAnalyzeTarget called but this telescope has no target assigned.");
			return this.m_analyzeTarget;
		}

		// Token: 0x060077B1 RID: 30641 RVA: 0x002BBFE4 File Offset: 0x002BA1E4
		public bool HasSkyVisibility()
		{
			Extents extents = base.GetComponent<Building>().GetExtents();
			int num;
			bool flag = Grid.IsRangeExposedToSunlight(Grid.XYToCell(extents.x, extents.y), base.def.clearScanCellRadius, new CellOffset(1, 0), out num, 1);
			this.m_percentClear = (float)num / (float)(base.def.clearScanCellRadius * 2 + 1);
			return flag;
		}

		// Token: 0x04005BBF RID: 23487
		private float m_percentClear;

		// Token: 0x04005BC0 RID: 23488
		[Serialize]
		private bool m_hasAnalyzeTarget;

		// Token: 0x04005BC1 RID: 23489
		[Serialize]
		private AxialI m_analyzeTarget;

		// Token: 0x04005BC2 RID: 23490
		[MyCmpAdd]
		private ClusterTelescope.ClusterTelescopeWorkable m_workable;

		// Token: 0x04005BC3 RID: 23491
		public KAnimFile[] workableOverrideAnims;

		// Token: 0x04005BC4 RID: 23492
		public bool providesOxygen;
	}

	// Token: 0x020011BA RID: 4538
	public class ClusterTelescopeWorkable : Workable, OxygenBreather.IGasProvider
	{
		// Token: 0x060077B2 RID: 30642 RVA: 0x002BC044 File Offset: 0x002BA244
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
			this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE;
			this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
			this.skillExperienceMultiplier = SKILLS.ALL_DAY_EXPERIENCE;
			this.requiredSkillPerk = Db.Get().SkillPerks.CanUseClusterTelescope.Id;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.radiationShielding = new AttributeModifier(Db.Get().Attributes.RadiationResistance.Id, FIXEDTRAITS.COSMICRADIATION.TELESCOPE_RADIATION_SHIELDING, STRINGS.BUILDINGS.PREFABS.CLUSTERTELESCOPEENCLOSED.NAME, false, false, true);
		}

		// Token: 0x060077B3 RID: 30643 RVA: 0x002BC0EF File Offset: 0x002BA2EF
		protected override void OnCleanUp()
		{
			if (this.telescopeTargetMarker != null)
			{
				Util.KDestroyGameObject(this.telescopeTargetMarker);
			}
			base.OnCleanUp();
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x002BC110 File Offset: 0x002BA310
		protected override void OnSpawn()
		{
			base.OnSpawn();
			this.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(this.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
			this.m_fowManager = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
			base.SetWorkTime(float.PositiveInfinity);
			this.overrideAnims = this.m_telescope.workableOverrideAnims;
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x002BC174 File Offset: 0x002BA374
		private void OnWorkableEvent(Workable workable, Workable.WorkableEvent ev)
		{
			Worker worker = base.worker;
			if (worker == null)
			{
				return;
			}
			KPrefabID component = worker.GetComponent<KPrefabID>();
			OxygenBreather component2 = worker.GetComponent<OxygenBreather>();
			Attributes attributes = worker.GetAttributes();
			if (ev == Workable.WorkableEvent.WorkStarted)
			{
				base.ShowProgressBar(true);
				this.progressBar.SetUpdateFunc(() => this.m_fowManager.GetRevealCompleteFraction(this.currentTarget));
				this.currentTarget = this.m_telescope.GetAnalyzeTarget();
				if (!ClusterGrid.Instance.GetEntityOfLayerAtCell(this.currentTarget, EntityLayer.Telescope))
				{
					this.telescopeTargetMarker = GameUtil.KInstantiate(Assets.GetPrefab("TelescopeTarget"), Grid.SceneLayer.Background, null, 0);
					this.telescopeTargetMarker.SetActive(true);
					this.telescopeTargetMarker.GetComponent<TelescopeTarget>().Init(this.currentTarget);
				}
				if (this.m_telescope.providesOxygen)
				{
					attributes.Add(this.radiationShielding);
					this.workerGasProvider = component2.GetGasProvider();
					component2.SetGasProvider(this);
					component2.GetComponent<CreatureSimTemperatureTransfer>().enabled = false;
					component.AddTag(GameTags.Shaded, false);
				}
				base.GetComponent<Operational>().SetActive(true, false);
				this.checkMarkerFrequency = UnityEngine.Random.Range(2f, 5f);
				return;
			}
			if (ev != Workable.WorkableEvent.WorkStopped)
			{
				return;
			}
			if (this.m_telescope.providesOxygen)
			{
				attributes.Remove(this.radiationShielding);
				component2.SetGasProvider(this.workerGasProvider);
				component2.GetComponent<CreatureSimTemperatureTransfer>().enabled = true;
				component.RemoveTag(GameTags.Shaded);
			}
			base.GetComponent<Operational>().SetActive(false, false);
			if (this.telescopeTargetMarker != null)
			{
				Util.KDestroyGameObject(this.telescopeTargetMarker);
			}
			base.ShowProgressBar(false);
		}

		// Token: 0x060077B6 RID: 30646 RVA: 0x002BC308 File Offset: 0x002BA508
		public override List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> descriptors = base.GetDescriptors(go);
			Element element = ElementLoader.FindElementByHash(SimHashes.Oxygen);
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(element.tag.ProperName(), string.Format(STRINGS.BUILDINGS.PREFABS.TELESCOPE.REQUIREMENT_TOOLTIP, element.tag.ProperName()), Descriptor.DescriptorType.Requirement);
			descriptors.Add(descriptor);
			return descriptors;
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x002BC364 File Offset: 0x002BA564
		protected override bool OnWorkTick(Worker worker, float dt)
		{
			AxialI analyzeTarget = this.m_telescope.GetAnalyzeTarget();
			bool flag = false;
			if (analyzeTarget != this.currentTarget)
			{
				if (this.telescopeTargetMarker)
				{
					this.telescopeTargetMarker.GetComponent<TelescopeTarget>().Init(analyzeTarget);
				}
				this.currentTarget = analyzeTarget;
				flag = true;
			}
			if (!flag && this.checkMarkerTimer > this.checkMarkerFrequency)
			{
				this.checkMarkerTimer = 0f;
				if (!this.telescopeTargetMarker && !ClusterGrid.Instance.GetEntityOfLayerAtCell(this.currentTarget, EntityLayer.Telescope))
				{
					this.telescopeTargetMarker = GameUtil.KInstantiate(Assets.GetPrefab("TelescopeTarget"), Grid.SceneLayer.Background, null, 0);
					this.telescopeTargetMarker.SetActive(true);
					this.telescopeTargetMarker.GetComponent<TelescopeTarget>().Init(this.currentTarget);
				}
			}
			this.checkMarkerTimer += dt;
			float num = ROCKETRY.CLUSTER_FOW.POINTS_TO_REVEAL / ROCKETRY.CLUSTER_FOW.DEFAULT_CYCLES_PER_REVEAL / 600f;
			float num2 = dt * num;
			this.m_fowManager.EarnRevealPointsForLocation(this.currentTarget, num2);
			return base.OnWorkTick(worker, dt);
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x002BC472 File Offset: 0x002BA672
		public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
		{
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x002BC474 File Offset: 0x002BA674
		public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
		{
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x002BC476 File Offset: 0x002BA676
		public bool ShouldEmitCO2()
		{
			return false;
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x002BC479 File Offset: 0x002BA679
		public bool ShouldStoreCO2()
		{
			return false;
		}

		// Token: 0x060077BC RID: 30652 RVA: 0x002BC47C File Offset: 0x002BA67C
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

		// Token: 0x04005BC5 RID: 23493
		[MySmiReq]
		private ClusterTelescope.Instance m_telescope;

		// Token: 0x04005BC6 RID: 23494
		private ClusterFogOfWarManager.Instance m_fowManager;

		// Token: 0x04005BC7 RID: 23495
		private GameObject telescopeTargetMarker;

		// Token: 0x04005BC8 RID: 23496
		private AxialI currentTarget;

		// Token: 0x04005BC9 RID: 23497
		private OxygenBreather.IGasProvider workerGasProvider;

		// Token: 0x04005BCA RID: 23498
		[MyCmpGet]
		private Storage storage;

		// Token: 0x04005BCB RID: 23499
		private AttributeModifier radiationShielding;

		// Token: 0x04005BCC RID: 23500
		private float checkMarkerTimer;

		// Token: 0x04005BCD RID: 23501
		private float checkMarkerFrequency = 1f;
	}
}
