using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020004BE RID: 1214
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Repairable")]
public class Repairable : Workable
{
	// Token: 0x06001C01 RID: 7169 RVA: 0x00094B04 File Offset: 0x00092D04
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
		base.Subscribe<Repairable>(493375141, Repairable.OnRefreshUserMenuDelegate);
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.showProgressBar = false;
		this.faceTargetWhenWorking = true;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x00094BB4 File Offset: 0x00092DB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new Repairable.SMInstance(this);
		this.smi.StartSM();
		this.workTime = float.PositiveInfinity;
		this.workTimeRemaining = float.PositiveInfinity;
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x00094BE9 File Offset: 0x00092DE9
	private void OnProxyStorageChanged(object data)
	{
		base.Trigger(-1697596308, data);
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x00094BF7 File Offset: 0x00092DF7
	protected override void OnLoadLevel()
	{
		this.smi = null;
		base.OnLoadLevel();
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x00094C06 File Offset: 0x00092E06
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("Destroy Repairable");
		}
		base.OnCleanUp();
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x00094C28 File Offset: 0x00092E28
	private void OnRefreshUserMenu(object data)
	{
		if (base.gameObject != null && this.smi != null)
		{
			if (this.smi.GetCurrentState() == this.smi.sm.forbidden)
			{
				Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_repair", STRINGS.BUILDINGS.REPAIRABLE.ENABLE_AUTOREPAIR.NAME, new System.Action(this.AllowRepair), global::Action.NumActions, null, null, null, STRINGS.BUILDINGS.REPAIRABLE.ENABLE_AUTOREPAIR.TOOLTIP, true), 0.5f);
				return;
			}
			Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_repair", STRINGS.BUILDINGS.REPAIRABLE.DISABLE_AUTOREPAIR.NAME, new System.Action(this.CancelRepair), global::Action.NumActions, null, null, null, STRINGS.BUILDINGS.REPAIRABLE.DISABLE_AUTOREPAIR.TOOLTIP, true), 0.5f);
		}
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x00094D0C File Offset: 0x00092F0C
	private void AllowRepair()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.hp.Repair(this.hp.MaxHitPoints);
			this.OnCompleteWork(null);
		}
		this.smi.sm.allow.Trigger(this.smi);
		this.OnRefreshUserMenu(null);
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x00094D5F File Offset: 0x00092F5F
	public void CancelRepair()
	{
		if (this.smi != null)
		{
			this.smi.sm.forbid.Trigger(this.smi);
		}
		this.OnRefreshUserMenu(null);
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x00094D8C File Offset: 0x00092F8C
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			component.SetFlag(Repairable.repairedFlag, false);
		}
		this.timeSpentRepairing = 0f;
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x00094DC8 File Offset: 0x00092FC8
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		float num = Mathf.Sqrt(base.GetComponent<PrimaryElement>().Mass);
		float num2 = ((this.expectedRepairTime < 0f) ? num : this.expectedRepairTime) * 0.1f;
		if (this.timeSpentRepairing >= num2)
		{
			this.timeSpentRepairing -= num2;
			int num3 = 0;
			if (worker != null)
			{
				num3 = (int)Db.Get().Attributes.Machinery.Lookup(worker).GetTotalValue();
			}
			int num4 = Mathf.CeilToInt((float)(10 + Math.Max(0, num3 * 10)) * 0.1f);
			this.hp.Repair(num4);
			if (this.hp.HitPoints >= this.hp.MaxHitPoints)
			{
				return true;
			}
		}
		this.timeSpentRepairing += dt;
		return false;
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x00094E90 File Offset: 0x00093090
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			component.SetFlag(Repairable.repairedFlag, true);
		}
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x00094EC0 File Offset: 0x000930C0
	protected override void OnCompleteWork(Worker worker)
	{
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			component.SetFlag(Repairable.repairedFlag, true);
		}
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x00094EEC File Offset: 0x000930EC
	public void CreateStorageProxy()
	{
		if (this.storageProxy == null)
		{
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(RepairableStorageProxy.ID), base.transform.gameObject, null);
			gameObject.transform.SetLocalPosition(Vector3.zero);
			this.storageProxy = gameObject.GetComponent<Storage>();
			this.storageProxy.prioritizable = base.transform.GetComponent<Prioritizable>();
			this.storageProxy.prioritizable.AddRef();
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x00094F74 File Offset: 0x00093174
	[OnSerializing]
	private void OnSerializing()
	{
		this.storedData = null;
		if (this.storageProxy != null && !this.storageProxy.IsEmpty())
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.storageProxy.Serialize(binaryWriter);
				}
				this.storedData = memoryStream.ToArray();
			}
		}
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x00094FFC File Offset: 0x000931FC
	[OnSerialized]
	private void OnSerialized()
	{
		this.storedData = null;
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x00095008 File Offset: 0x00093208
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.storedData != null)
		{
			FastReader fastReader = new FastReader(this.storedData);
			this.CreateStorageProxy();
			this.storageProxy.Deserialize(fastReader);
			this.storedData = null;
		}
	}

	// Token: 0x04000FA9 RID: 4009
	public float expectedRepairTime = -1f;

	// Token: 0x04000FAA RID: 4010
	[MyCmpGet]
	private BuildingHP hp;

	// Token: 0x04000FAB RID: 4011
	private Repairable.SMInstance smi;

	// Token: 0x04000FAC RID: 4012
	private Storage storageProxy;

	// Token: 0x04000FAD RID: 4013
	[Serialize]
	private byte[] storedData;

	// Token: 0x04000FAE RID: 4014
	private float timeSpentRepairing;

	// Token: 0x04000FAF RID: 4015
	private static readonly Operational.Flag repairedFlag = new Operational.Flag("repaired", Operational.Flag.Type.Functional);

	// Token: 0x04000FB0 RID: 4016
	private static readonly EventSystem.IntraObjectHandler<Repairable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Repairable>(delegate(Repairable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x020010FA RID: 4346
	public class SMInstance : GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.GameInstance
	{
		// Token: 0x06007509 RID: 29961 RVA: 0x002B4A17 File Offset: 0x002B2C17
		public SMInstance(Repairable smi)
			: base(smi)
		{
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x002B4A20 File Offset: 0x002B2C20
		public bool HasRequiredMass()
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			float num = component.Mass * 0.1f;
			PrimaryElement primaryElement = base.smi.master.storageProxy.FindPrimaryElement(component.ElementID);
			return primaryElement != null && primaryElement.Mass >= num;
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x002B4A74 File Offset: 0x002B2C74
		public KeyValuePair<Tag, float> GetRequiredMass()
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			float num = component.Mass * 0.1f;
			PrimaryElement primaryElement = base.smi.master.storageProxy.FindPrimaryElement(component.ElementID);
			float num2 = ((primaryElement != null) ? Math.Max(0f, num - primaryElement.Mass) : num);
			return new KeyValuePair<Tag, float>(component.Element.tag, num2);
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x002B4AE1 File Offset: 0x002B2CE1
		public void ConsumeRepairMaterials()
		{
			base.smi.master.storageProxy.ConsumeAllIgnoringDisease();
		}

		// Token: 0x0600750D RID: 29965 RVA: 0x002B4AF8 File Offset: 0x002B2CF8
		public void DestroyStorageProxy()
		{
			if (base.smi.master.storageProxy != null)
			{
				base.smi.master.transform.GetComponent<Prioritizable>().RemoveRef();
				base.smi.master.storageProxy.DropAll(false, false, default(Vector3), true, null);
				Util.KDestroyGameObject(base.smi.master.storageProxy.gameObject);
			}
		}

		// Token: 0x0600750E RID: 29966 RVA: 0x002B4B73 File Offset: 0x002B2D73
		public bool NeedsRepairs()
		{
			return base.smi.master.GetComponent<BuildingHP>().NeedsRepairs;
		}

		// Token: 0x04005938 RID: 22840
		private const float REQUIRED_MASS_SCALE = 0.1f;
	}

	// Token: 0x020010FB RID: 4347
	public class States : GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable>
	{
		// Token: 0x0600750F RID: 29967 RVA: 0x002B4B8C File Offset: 0x002B2D8C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.repaired;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.forbidden.OnSignal(this.allow, this.repaired);
			this.allowed.Enter(delegate(Repairable.SMInstance smi)
			{
				smi.master.CreateStorageProxy();
			}).DefaultState(this.allowed.needMass).EventHandler(GameHashes.BuildingFullyRepaired, delegate(Repairable.SMInstance smi)
			{
				smi.ConsumeRepairMaterials();
			})
				.EventTransition(GameHashes.BuildingFullyRepaired, this.repaired, null)
				.OnSignal(this.forbid, this.forbidden)
				.Exit(delegate(Repairable.SMInstance smi)
				{
					smi.DestroyStorageProxy();
				});
			this.allowed.needMass.Enter(delegate(Repairable.SMInstance smi)
			{
				Prioritizable.AddRef(smi.master.storageProxy.transform.parent.gameObject);
			}).Exit(delegate(Repairable.SMInstance smi)
			{
				if (!smi.isMasterNull && smi.master.storageProxy != null)
				{
					Prioritizable.RemoveRef(smi.master.storageProxy.transform.parent.gameObject);
				}
			}).EventTransition(GameHashes.OnStorageChange, this.allowed.repairable, (Repairable.SMInstance smi) => smi.HasRequiredMass())
				.ToggleChore(new Func<Repairable.SMInstance, Chore>(this.CreateFetchChore), this.allowed.repairable, this.allowed.needMass)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.WaitingForRepairMaterials, (Repairable.SMInstance smi) => smi.GetRequiredMass());
			this.allowed.repairable.ToggleRecurringChore(new Func<Repairable.SMInstance, Chore>(this.CreateRepairChore), null).ToggleStatusItem(Db.Get().BuildingStatusItems.PendingRepair, null);
			this.repaired.EventTransition(GameHashes.BuildingReceivedDamage, this.allowed, (Repairable.SMInstance smi) => smi.NeedsRepairs()).OnSignal(this.allow, this.allowed).OnSignal(this.forbid, this.forbidden);
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x002B4DD8 File Offset: 0x002B2FD8
		private Chore CreateFetchChore(Repairable.SMInstance smi)
		{
			PrimaryElement component = smi.master.GetComponent<PrimaryElement>();
			PrimaryElement primaryElement = smi.master.storageProxy.FindPrimaryElement(component.ElementID);
			float num = component.Mass * 0.1f - ((primaryElement != null) ? primaryElement.Mass : 0f);
			HashSet<Tag> hashSet = new HashSet<Tag> { GameTagExtensions.Create(component.ElementID) };
			return new FetchChore(Db.Get().ChoreTypes.RepairFetch, smi.master.storageProxy, num, hashSet, FetchChore.MatchCriteria.MatchID, Tag.Invalid, null, null, true, null, null, null, Operational.State.None, 0);
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x002B4E74 File Offset: 0x002B3074
		private Chore CreateRepairChore(Repairable.SMInstance smi)
		{
			WorkChore<Repairable> workChore = new WorkChore<Repairable>(Db.Get().ChoreTypes.Repair, smi.master, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
			Deconstructable component = smi.master.GetComponent<Deconstructable>();
			if (component != null)
			{
				workChore.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDeconstruction, component);
			}
			Breakable component2 = smi.master.GetComponent<Breakable>();
			if (component2 != null)
			{
				workChore.AddPrecondition(Repairable.States.IsNotBeingAttacked, component2);
			}
			workChore.AddPrecondition(Repairable.States.IsNotAngry, null);
			return workChore;
		}

		// Token: 0x04005939 RID: 22841
		public StateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.Signal allow;

		// Token: 0x0400593A RID: 22842
		public StateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.Signal forbid;

		// Token: 0x0400593B RID: 22843
		public GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.State forbidden;

		// Token: 0x0400593C RID: 22844
		public Repairable.States.AllowedState allowed;

		// Token: 0x0400593D RID: 22845
		public GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.State repaired;

		// Token: 0x0400593E RID: 22846
		public static readonly Chore.Precondition IsNotBeingAttacked = new Chore.Precondition
		{
			id = "IsNotBeingAttacked",
			description = DUPLICANTS.CHORES.PRECONDITIONS.IS_NOT_BEING_ATTACKED,
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				bool flag = true;
				if (data != null)
				{
					flag = ((Breakable)data).worker == null;
				}
				return flag;
			}
		};

		// Token: 0x0400593F RID: 22847
		public static readonly Chore.Precondition IsNotAngry = new Chore.Precondition
		{
			id = "IsNotAngry",
			description = DUPLICANTS.CHORES.PRECONDITIONS.IS_NOT_ANGRY,
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				Traits traits = context.consumerState.traits;
				AmountInstance amountInstance = Db.Get().Amounts.Stress.Lookup(context.consumerState.gameObject);
				return !(traits != null) || amountInstance == null || amountInstance.value < STRESS.ACTING_OUT_RESET || !traits.HasTrait("Aggressive");
			}
		};

		// Token: 0x02001F76 RID: 8054
		public class AllowedState : GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.State
		{
			// Token: 0x04008BDA RID: 35802
			public GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.State needMass;

			// Token: 0x04008BDB RID: 35803
			public GameStateMachine<Repairable.States, Repairable.SMInstance, Repairable, object>.State repairable;
		}
	}
}
