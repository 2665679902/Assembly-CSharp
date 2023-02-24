using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200065D RID: 1629
public class Toilet : StateMachineComponent<Toilet.StatesInstance>, ISaveLoadable, IUsable, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000E5AF3 File Offset: 0x000E3CF3
	// (set) Token: 0x06002BBC RID: 11196 RVA: 0x000E5AFB File Offset: 0x000E3CFB
	public int FlushesUsed
	{
		get
		{
			return this._flushesUsed;
		}
		set
		{
			this._flushesUsed = value;
			base.smi.sm.flushes.Set(value, base.smi, false);
		}
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x000E5B24 File Offset: 0x000E3D24
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Toilets.Add(this);
		Components.BasicBuildings.Add(this);
		base.smi.StartSM();
		base.GetComponent<ToiletWorkableUse>().trackUses = true;
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
		this.meter.SetPositionPercent((float)this.FlushesUsed / (float)this.maxFlushes);
		this.FlushesUsed = this._flushesUsed;
		base.Subscribe<Toilet>(493375141, Toilet.OnRefreshUserMenuDelegate);
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x000E5BD7 File Offset: 0x000E3DD7
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.BasicBuildings.Remove(this);
		Components.Toilets.Remove(this);
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x000E5BF5 File Offset: 0x000E3DF5
	public bool IsUsable()
	{
		return base.smi.HasTag(GameTags.Usable);
	}

	// Token: 0x06002BC0 RID: 11200 RVA: 0x000E5C08 File Offset: 0x000E3E08
	public void Flush(Worker worker)
	{
		this.FlushesUsed++;
		this.meter.SetPositionPercent((float)this.FlushesUsed / (float)this.maxFlushes);
		float num = 0f;
		Tag tag = ElementLoader.FindElementByHash(SimHashes.Dirt).tag;
		float num2;
		SimUtil.DiseaseInfo diseaseInfo;
		this.storage.ConsumeAndGetDisease(tag, base.smi.DirtUsedPerFlush(), out num2, out diseaseInfo, out num);
		byte index = Db.Get().Diseases.GetIndex(this.diseaseId);
		float num3 = base.smi.MassPerFlush() + num2;
		GameObject gameObject = ElementLoader.FindElementByHash(this.solidWastePerUse.elementID).substance.SpawnResource(base.transform.GetPosition(), num3, this.solidWasteTemperature, index, this.diseasePerFlush, true, false, false);
		gameObject.GetComponent<PrimaryElement>().AddDisease(diseaseInfo.idx, diseaseInfo.count, "Toilet.Flush");
		this.storage.Store(gameObject, false, false, true, false);
		worker.GetComponent<PrimaryElement>().AddDisease(index, this.diseaseOnDupePerFlush, "Toilet.Flush");
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(DUPLICANTS.DISEASES.ADDED_POPFX, Db.Get().Diseases[(int)index].Name, this.diseasePerFlush + this.diseaseOnDupePerFlush), base.transform, Vector3.up, 1.5f, false, false);
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, true);
	}

	// Token: 0x06002BC1 RID: 11201 RVA: 0x000E5D88 File Offset: 0x000E3F88
	private void OnRefreshUserMenu(object data)
	{
		if (base.smi.GetCurrentState() == base.smi.sm.full || !base.smi.IsSoiled || base.smi.cleanChore != null)
		{
			return;
		}
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("status_item_toilet_needs_emptying", UI.USERMENUACTIONS.CLEANTOILET.NAME, delegate
		{
			base.smi.GoTo(base.smi.sm.earlyclean);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CLEANTOILET.TOOLTIP, true), 1f);
	}

	// Token: 0x06002BC2 RID: 11202 RVA: 0x000E5E1A File Offset: 0x000E401A
	private void SpawnMonster()
	{
		GameUtil.KInstantiate(Assets.GetPrefab(new Tag("Glom")), base.smi.transform.GetPosition(), Grid.SceneLayer.Creatures, null, 0).SetActive(true);
	}

	// Token: 0x06002BC3 RID: 11203 RVA: 0x000E5E4C File Offset: 0x000E404C
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = base.GetComponent<ManualDeliveryKG>().RequestedItemTag.ProperName();
		float num = base.smi.DirtUsedPerFlush();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x06002BC4 RID: 11204 RVA: 0x000E5ED0 File Offset: 0x000E40D0
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(this.solidWastePerUse.elementID).tag.ProperName();
		float num = base.smi.MassPerFlush() + base.smi.DirtUsedPerFlush();
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.solidWasteTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.solidWasteTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false));
		Disease disease = Db.Get().Diseases.Get(this.diseaseId);
		int num2 = this.diseasePerFlush + this.diseaseOnDupePerFlush;
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num2, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num2, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.DiseaseSource, false));
		return list;
	}

	// Token: 0x06002BC5 RID: 11205 RVA: 0x000E5FE5 File Offset: 0x000E41E5
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(this.RequirementDescriptors());
		list.AddRange(this.EffectDescriptors());
		return list;
	}

	// Token: 0x040019DE RID: 6622
	[SerializeField]
	public Toilet.SpawnInfo solidWastePerUse;

	// Token: 0x040019DF RID: 6623
	[SerializeField]
	public float solidWasteTemperature;

	// Token: 0x040019E0 RID: 6624
	[SerializeField]
	public Toilet.SpawnInfo gasWasteWhenFull;

	// Token: 0x040019E1 RID: 6625
	[SerializeField]
	public int maxFlushes = 15;

	// Token: 0x040019E2 RID: 6626
	[SerializeField]
	public string diseaseId;

	// Token: 0x040019E3 RID: 6627
	[SerializeField]
	public int diseasePerFlush;

	// Token: 0x040019E4 RID: 6628
	[SerializeField]
	public int diseaseOnDupePerFlush;

	// Token: 0x040019E5 RID: 6629
	[SerializeField]
	public float dirtUsedPerFlush = 13f;

	// Token: 0x040019E6 RID: 6630
	[Serialize]
	public int _flushesUsed;

	// Token: 0x040019E7 RID: 6631
	private MeterController meter;

	// Token: 0x040019E8 RID: 6632
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040019E9 RID: 6633
	[MyCmpReq]
	private ManualDeliveryKG manualdeliverykg;

	// Token: 0x040019EA RID: 6634
	private static readonly EventSystem.IntraObjectHandler<Toilet> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Toilet>(delegate(Toilet component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x02001313 RID: 4883
	[Serializable]
	public struct SpawnInfo
	{
		// Token: 0x06007C83 RID: 31875 RVA: 0x002D04F1 File Offset: 0x002CE6F1
		public SpawnInfo(SimHashes element_id, float mass, float interval)
		{
			this.elementID = element_id;
			this.mass = mass;
			this.interval = interval;
		}

		// Token: 0x04005F77 RID: 24439
		[HashedEnum]
		public SimHashes elementID;

		// Token: 0x04005F78 RID: 24440
		public float mass;

		// Token: 0x04005F79 RID: 24441
		public float interval;
	}

	// Token: 0x02001314 RID: 4884
	public class StatesInstance : GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.GameInstance
	{
		// Token: 0x06007C84 RID: 31876 RVA: 0x002D0508 File Offset: 0x002CE708
		public StatesInstance(Toilet master)
			: base(master)
		{
			this.activeUseChores = new List<Chore>();
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06007C85 RID: 31877 RVA: 0x002D0527 File Offset: 0x002CE727
		public bool IsSoiled
		{
			get
			{
				return base.master.FlushesUsed > 0;
			}
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x002D0537 File Offset: 0x002CE737
		public int GetFlushesRemaining()
		{
			return base.master.maxFlushes - base.master.FlushesUsed;
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x002D0550 File Offset: 0x002CE750
		public bool RequiresDirtDelivery()
		{
			if (base.master.storage.IsEmpty())
			{
				return true;
			}
			Tag tag = ElementLoader.FindElementByHash(SimHashes.Dirt).tag;
			return !base.master.storage.Has(tag) || (base.master.storage.GetAmountAvailable(tag) < base.master.manualdeliverykg.capacity && !this.IsSoiled);
		}

		// Token: 0x06007C88 RID: 31880 RVA: 0x002D05C9 File Offset: 0x002CE7C9
		public float MassPerFlush()
		{
			return base.master.solidWastePerUse.mass;
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x002D05DB File Offset: 0x002CE7DB
		public float DirtUsedPerFlush()
		{
			return base.master.dirtUsedPerFlush;
		}

		// Token: 0x06007C8A RID: 31882 RVA: 0x002D05E8 File Offset: 0x002CE7E8
		public bool IsToxicSandRemoved()
		{
			Tag tag = GameTagExtensions.Create(base.master.solidWastePerUse.elementID);
			return base.master.storage.FindFirst(tag) == null;
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x002D0624 File Offset: 0x002CE824
		public void CreateCleanChore()
		{
			if (this.cleanChore != null)
			{
				this.cleanChore.Cancel("dupe");
			}
			ToiletWorkableClean component = base.master.GetComponent<ToiletWorkableClean>();
			this.cleanChore = new WorkChore<ToiletWorkableClean>(Db.Get().ChoreTypes.CleanToilet, component, null, true, new Action<Chore>(this.OnCleanComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x002D068C File Offset: 0x002CE88C
		public void CancelCleanChore()
		{
			if (this.cleanChore != null)
			{
				this.cleanChore.Cancel("Cancelled");
				this.cleanChore = null;
			}
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x002D06B0 File Offset: 0x002CE8B0
		private void DropFromStorage(Tag tag)
		{
			ListPool<GameObject, Toilet>.PooledList pooledList = ListPool<GameObject, Toilet>.Allocate();
			base.master.storage.Find(tag, pooledList);
			foreach (GameObject gameObject in pooledList)
			{
				base.master.storage.Drop(gameObject, true);
			}
			pooledList.Recycle();
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x002D072C File Offset: 0x002CE92C
		private void OnCleanComplete(Chore chore)
		{
			this.cleanChore = null;
			Tag tag = GameTagExtensions.Create(base.master.solidWastePerUse.elementID);
			Tag tag2 = ElementLoader.FindElementByHash(SimHashes.Dirt).tag;
			this.DropFromStorage(tag);
			this.DropFromStorage(tag2);
			base.master.meter.SetPositionPercent((float)base.master.FlushesUsed / (float)base.master.maxFlushes);
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x002D07A0 File Offset: 0x002CE9A0
		public void Flush()
		{
			Worker worker = base.master.GetComponent<ToiletWorkableUse>().worker;
			base.master.Flush(worker);
		}

		// Token: 0x04005F7A RID: 24442
		public Chore cleanChore;

		// Token: 0x04005F7B RID: 24443
		public List<Chore> activeUseChores;

		// Token: 0x04005F7C RID: 24444
		public float monsterSpawnTime = 1200f;
	}

	// Token: 0x02001315 RID: 4885
	public class States : GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet>
	{
		// Token: 0x06007C90 RID: 31888 RVA: 0x002D07CC File Offset: 0x002CE9CC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.needsdirt;
			this.root.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.needsdirt, (Toilet.StatesInstance smi) => smi.RequiresDirtDelivery()).EventTransition(GameHashes.OperationalChanged, this.notoperational, (Toilet.StatesInstance smi) => !smi.Get<Operational>().IsOperational);
			this.needsdirt.Enter(delegate(Toilet.StatesInstance smi)
			{
				if (smi.RequiresDirtDelivery())
				{
					smi.master.manualdeliverykg.RequestDelivery();
				}
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Unusable, null).EventTransition(GameHashes.OnStorageChange, this.ready, (Toilet.StatesInstance smi) => !smi.RequiresDirtDelivery());
			this.ready.ParamTransition<int>(this.flushes, this.full, (Toilet.StatesInstance smi, int p) => smi.GetFlushesRemaining() <= 0).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Toilet, null).ToggleRecurringChore(new Func<Toilet.StatesInstance, Chore>(this.CreateUrgentUseChore), null)
				.ToggleRecurringChore(new Func<Toilet.StatesInstance, Chore>(this.CreateBreakUseChore), null)
				.ToggleTag(GameTags.Usable)
				.EventHandler(GameHashes.Flush, delegate(Toilet.StatesInstance smi, object data)
				{
					smi.Flush();
				});
			this.earlyclean.PlayAnims((Toilet.StatesInstance smi) => Toilet.States.FULL_ANIMS, KAnim.PlayMode.Once).OnAnimQueueComplete(this.earlyWaitingForClean);
			this.earlyWaitingForClean.Enter(delegate(Toilet.StatesInstance smi)
			{
				smi.CreateCleanChore();
			}).Exit(delegate(Toilet.StatesInstance smi)
			{
				smi.CancelCleanChore();
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.ToiletNeedsEmptying, null)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Unusable, null)
				.EventTransition(GameHashes.OnStorageChange, this.empty, (Toilet.StatesInstance smi) => smi.IsToxicSandRemoved());
			this.full.PlayAnims((Toilet.StatesInstance smi) => Toilet.States.FULL_ANIMS, KAnim.PlayMode.Once).OnAnimQueueComplete(this.fullWaitingForClean);
			this.fullWaitingForClean.Enter(delegate(Toilet.StatesInstance smi)
			{
				smi.CreateCleanChore();
			}).Exit(delegate(Toilet.StatesInstance smi)
			{
				smi.CancelCleanChore();
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.ToiletNeedsEmptying, null)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Unusable, null)
				.EventTransition(GameHashes.OnStorageChange, this.empty, (Toilet.StatesInstance smi) => smi.IsToxicSandRemoved())
				.Enter(delegate(Toilet.StatesInstance smi)
				{
					smi.Schedule(smi.monsterSpawnTime, delegate
					{
						smi.master.SpawnMonster();
					}, null);
				});
			this.empty.PlayAnim("off").Enter("ClearFlushes", delegate(Toilet.StatesInstance smi)
			{
				smi.master.FlushesUsed = 0;
			}).GoTo(this.needsdirt);
			this.notoperational.EventTransition(GameHashes.OperationalChanged, this.needsdirt, (Toilet.StatesInstance smi) => smi.Get<Operational>().IsOperational).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Unusable, null);
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x002D0BD4 File Offset: 0x002CEDD4
		private Chore CreateUrgentUseChore(Toilet.StatesInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.Pee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderFull, null);
			chore.AddPrecondition(ChorePreconditions.instance.NotCurrentlyPeeing, null);
			return chore;
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x002D0C10 File Offset: 0x002CEE10
		private Chore CreateBreakUseChore(Toilet.StatesInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.BreakPee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderNotFull, null);
			chore.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Hygiene);
			return chore;
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x002D0C64 File Offset: 0x002CEE64
		private Chore CreateUseChore(Toilet.StatesInstance smi, ChoreType choreType)
		{
			WorkChore<ToiletWorkableUse> workChore = new WorkChore<ToiletWorkableUse>(choreType, smi.master, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
			smi.activeUseChores.Add(workChore);
			WorkChore<ToiletWorkableUse> workChore2 = workChore;
			workChore2.onExit = (Action<Chore>)Delegate.Combine(workChore2.onExit, new Action<Chore>(delegate(Chore exiting_chore)
			{
				smi.activeUseChores.Remove(exiting_chore);
			}));
			workChore.AddPrecondition(ChorePreconditions.instance.IsPreferredAssignableOrUrgentBladder, smi.master.GetComponent<Assignable>());
			workChore.AddPrecondition(ChorePreconditions.instance.IsExclusivelyAvailableWithOtherChores, smi.activeUseChores);
			return workChore;
		}

		// Token: 0x04005F7D RID: 24445
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State needsdirt;

		// Token: 0x04005F7E RID: 24446
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State empty;

		// Token: 0x04005F7F RID: 24447
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State notoperational;

		// Token: 0x04005F80 RID: 24448
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State ready;

		// Token: 0x04005F81 RID: 24449
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State earlyclean;

		// Token: 0x04005F82 RID: 24450
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State earlyWaitingForClean;

		// Token: 0x04005F83 RID: 24451
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State full;

		// Token: 0x04005F84 RID: 24452
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State fullWaitingForClean;

		// Token: 0x04005F85 RID: 24453
		private static readonly HashedString[] FULL_ANIMS = new HashedString[] { "full_pre", "full" };

		// Token: 0x04005F86 RID: 24454
		public StateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.IntParameter flushes = new StateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.IntParameter(0);

		// Token: 0x0200200F RID: 8207
		public class ReadyStates : GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State
		{
			// Token: 0x04008EFF RID: 36607
			public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State idle;

			// Token: 0x04008F00 RID: 36608
			public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State inuse;

			// Token: 0x04008F01 RID: 36609
			public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State flush;
		}
	}
}
