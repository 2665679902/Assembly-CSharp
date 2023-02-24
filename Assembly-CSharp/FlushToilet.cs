using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020005BD RID: 1469
public class FlushToilet : StateMachineComponent<FlushToilet.SMInstance>, IUsable, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x0600246E RID: 9326 RVA: 0x000C4E84 File Offset: 0x000C3084
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		ConduitFlow liquidConduitFlow = Game.Instance.liquidConduitFlow;
		liquidConduitFlow.onConduitsRebuilt += this.OnConduitsRebuilt;
		liquidConduitFlow.AddConduitUpdater(new Action<float>(this.OnConduitUpdate), ConduitFlowPriority.Default);
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		this.fillMeter = new MeterController(component2, "meter_target", "meter", this.meterOffset, Grid.SceneLayer.NoLayer, new Vector3(0.4f, 3.2f, 0.1f), Array.Empty<string>());
		this.contaminationMeter = new MeterController(component2, "meter_target", "meter_dirty", this.meterOffset, Grid.SceneLayer.NoLayer, new Vector3(0.4f, 3.2f, 0.1f), Array.Empty<string>());
		Components.Toilets.Add(this);
		Components.BasicBuildings.Add(this);
		base.smi.StartSM();
		base.smi.ShowFillMeter();
	}

	// Token: 0x0600246F RID: 9327 RVA: 0x000C4F85 File Offset: 0x000C3185
	protected override void OnCleanUp()
	{
		Game.Instance.liquidConduitFlow.onConduitsRebuilt -= this.OnConduitsRebuilt;
		Components.BasicBuildings.Remove(this);
		Components.Toilets.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x000C4FBE File Offset: 0x000C31BE
	private void OnConduitsRebuilt()
	{
		base.Trigger(-2094018600, null);
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x000C4FCC File Offset: 0x000C31CC
	public bool IsUsable()
	{
		return base.smi.HasTag(GameTags.Usable);
	}

	// Token: 0x06002472 RID: 9330 RVA: 0x000C4FE0 File Offset: 0x000C31E0
	private void Flush(Worker worker)
	{
		ListPool<GameObject, Storage>.PooledList pooledList = ListPool<GameObject, Storage>.Allocate();
		this.storage.Find(FlushToilet.WaterTag, pooledList);
		float num = 0f;
		float num2 = this.massConsumedPerUse;
		foreach (GameObject gameObject in pooledList)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			float num3 = Mathf.Min(component.Mass, num2);
			component.Mass -= num3;
			num2 -= num3;
			num += num3 * component.Temperature;
		}
		pooledList.Recycle();
		float num4 = this.massEmittedPerUse - this.massConsumedPerUse;
		num += num4 * this.newPeeTemperature;
		float num5 = num / this.massEmittedPerUse;
		byte index = Db.Get().Diseases.GetIndex(this.diseaseId);
		this.storage.AddLiquid(SimHashes.DirtyWater, this.massEmittedPerUse, num5, index, this.diseasePerFlush, false, true);
		if (worker != null)
		{
			worker.GetComponent<PrimaryElement>().AddDisease(index, this.diseaseOnDupePerFlush, "FlushToilet.Flush");
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(DUPLICANTS.DISEASES.ADDED_POPFX, Db.Get().Diseases[(int)index].Name, this.diseasePerFlush + this.diseaseOnDupePerFlush), base.transform, Vector3.up, 1.5f, false, false);
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, true);
			return;
		}
		DebugUtil.LogWarningArgs(new object[] { "Tried to add disease on toilet use but worker was null" });
	}

	// Token: 0x06002473 RID: 9331 RVA: 0x000C5190 File Offset: 0x000C3390
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(SimHashes.Water).tag.ProperName();
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement, false));
		return list;
	}

	// Token: 0x06002474 RID: 9332 RVA: 0x000C520C File Offset: 0x000C340C
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag.ProperName();
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(this.massEmittedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.newPeeTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(this.massEmittedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.newPeeTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false));
		Disease disease = Db.Get().Diseases.Get(this.diseaseId);
		int num = this.diseasePerFlush + this.diseaseOnDupePerFlush;
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.DiseaseSource, false));
		return list;
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x000C530D File Offset: 0x000C350D
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(this.RequirementDescriptors());
		list.AddRange(this.EffectDescriptors());
		return list;
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x000C532C File Offset: 0x000C352C
	private void OnConduitUpdate(float dt)
	{
		if (this.GetSMI() == null)
		{
			return;
		}
		ConduitFlow liquidConduitFlow = Game.Instance.liquidConduitFlow;
		bool flag = base.smi.master.requireOutput && liquidConduitFlow.GetContents(this.outputCell).mass > 0f && base.smi.HasContaminatedMass();
		base.smi.sm.outputBlocked.Set(flag, base.smi, false);
	}

	// Token: 0x040014F7 RID: 5367
	private MeterController fillMeter;

	// Token: 0x040014F8 RID: 5368
	private MeterController contaminationMeter;

	// Token: 0x040014F9 RID: 5369
	public Meter.Offset meterOffset = Meter.Offset.Behind;

	// Token: 0x040014FA RID: 5370
	[SerializeField]
	public float massConsumedPerUse = 5f;

	// Token: 0x040014FB RID: 5371
	[SerializeField]
	public float massEmittedPerUse = 5f;

	// Token: 0x040014FC RID: 5372
	[SerializeField]
	public float newPeeTemperature;

	// Token: 0x040014FD RID: 5373
	[SerializeField]
	public string diseaseId;

	// Token: 0x040014FE RID: 5374
	[SerializeField]
	public int diseasePerFlush;

	// Token: 0x040014FF RID: 5375
	[SerializeField]
	public int diseaseOnDupePerFlush;

	// Token: 0x04001500 RID: 5376
	[SerializeField]
	public bool requireOutput = true;

	// Token: 0x04001501 RID: 5377
	[MyCmpGet]
	private ConduitConsumer conduitConsumer;

	// Token: 0x04001502 RID: 5378
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001503 RID: 5379
	public static readonly Tag WaterTag = GameTagExtensions.Create(SimHashes.Water);

	// Token: 0x04001504 RID: 5380
	private int inputCell;

	// Token: 0x04001505 RID: 5381
	private int outputCell;

	// Token: 0x020011FB RID: 4603
	public class SMInstance : GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.GameInstance
	{
		// Token: 0x06007891 RID: 30865 RVA: 0x002BF655 File Offset: 0x002BD855
		public SMInstance(FlushToilet master)
			: base(master)
		{
			this.activeUseChores = new List<Chore>();
			this.UpdateFullnessState();
			this.UpdateDirtyState();
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x002BF678 File Offset: 0x002BD878
		public bool HasValidConnections()
		{
			return Game.Instance.liquidConduitFlow.HasConduit(base.master.inputCell) && (!base.master.requireOutput || Game.Instance.liquidConduitFlow.HasConduit(base.master.outputCell));
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x002BF6CC File Offset: 0x002BD8CC
		public bool UpdateFullnessState()
		{
			float num = 0f;
			ListPool<GameObject, FlushToilet>.PooledList pooledList = ListPool<GameObject, FlushToilet>.Allocate();
			base.master.storage.Find(FlushToilet.WaterTag, pooledList);
			foreach (GameObject gameObject in pooledList)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				num += component.Mass;
			}
			pooledList.Recycle();
			bool flag = num >= base.master.massConsumedPerUse;
			base.master.conduitConsumer.enabled = !flag;
			float num2 = Mathf.Clamp01(num / base.master.massConsumedPerUse);
			base.master.fillMeter.SetPositionPercent(num2);
			return flag;
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x002BF798 File Offset: 0x002BD998
		public void UpdateDirtyState()
		{
			float percentComplete = base.GetComponent<ToiletWorkableUse>().GetPercentComplete();
			base.master.contaminationMeter.SetPositionPercent(percentComplete);
		}

		// Token: 0x06007895 RID: 30869 RVA: 0x002BF7C4 File Offset: 0x002BD9C4
		public void Flush()
		{
			base.master.fillMeter.SetPositionPercent(0f);
			base.master.contaminationMeter.SetPositionPercent(1f);
			base.smi.ShowFillMeter();
			Worker worker = base.master.GetComponent<ToiletWorkableUse>().worker;
			base.master.Flush(worker);
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x002BF823 File Offset: 0x002BDA23
		public void ShowFillMeter()
		{
			base.master.fillMeter.gameObject.SetActive(true);
			base.master.contaminationMeter.gameObject.SetActive(false);
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x002BF854 File Offset: 0x002BDA54
		public bool HasContaminatedMass()
		{
			foreach (GameObject gameObject in base.GetComponent<Storage>().items)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && component.ElementID == SimHashes.DirtyWater && component.Mass > 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x002BF8D4 File Offset: 0x002BDAD4
		public void ShowContaminatedMeter()
		{
			base.master.fillMeter.gameObject.SetActive(false);
			base.master.contaminationMeter.gameObject.SetActive(true);
		}

		// Token: 0x04005C96 RID: 23702
		public List<Chore> activeUseChores;
	}

	// Token: 0x020011FC RID: 4604
	public class States : GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet>
	{
		// Token: 0x06007899 RID: 30873 RVA: 0x002BF904 File Offset: 0x002BDB04
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disconnected;
			this.disconnected.PlayAnim("off").EventTransition(GameHashes.ConduitConnectionChanged, this.backedup, (FlushToilet.SMInstance smi) => smi.HasValidConnections()).Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
			this.backedup.PlayAnim("off").ToggleStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, null).EventTransition(GameHashes.ConduitConnectionChanged, this.disconnected, (FlushToilet.SMInstance smi) => !smi.HasValidConnections())
				.ParamTransition<bool>(this.outputBlocked, this.fillingInactive, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsFalse)
				.Enter(delegate(FlushToilet.SMInstance smi)
				{
					smi.GetComponent<Operational>().SetActive(false, false);
				});
			this.filling.PlayAnim("off").Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			}).EventTransition(GameHashes.ConduitConnectionChanged, this.disconnected, (FlushToilet.SMInstance smi) => !smi.HasValidConnections())
				.ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue)
				.EventTransition(GameHashes.OnStorageChange, this.ready, (FlushToilet.SMInstance smi) => smi.UpdateFullnessState())
				.EventTransition(GameHashes.OperationalChanged, this.fillingInactive, (FlushToilet.SMInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.fillingInactive.PlayAnim("off").Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			}).EventTransition(GameHashes.OperationalChanged, this.filling, (FlushToilet.SMInstance smi) => smi.GetComponent<Operational>().IsOperational)
				.ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue);
			this.ready.DefaultState(this.ready.idle).ToggleTag(GameTags.Usable).Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.master.fillMeter.SetPositionPercent(1f);
				smi.master.contaminationMeter.SetPositionPercent(0f);
			})
				.PlayAnim("off")
				.EventTransition(GameHashes.ConduitConnectionChanged, this.disconnected, (FlushToilet.SMInstance smi) => !smi.HasValidConnections())
				.ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue)
				.ToggleChore(new Func<FlushToilet.SMInstance, Chore>(this.CreateUrgentUseChore), this.flushing)
				.ToggleChore(new Func<FlushToilet.SMInstance, Chore>(this.CreateBreakUseChore), this.flushing);
			this.ready.idle.Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.FlushToilet, null).WorkableStartTransition((FlushToilet.SMInstance smi) => smi.master.GetComponent<ToiletWorkableUse>(), this.ready.inuse);
			this.ready.inuse.Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.ShowContaminatedMeter();
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.FlushToiletInUse, null).Update(delegate(FlushToilet.SMInstance smi, float dt)
			{
				smi.UpdateDirtyState();
			}, UpdateRate.SIM_200ms, false)
				.WorkableCompleteTransition((FlushToilet.SMInstance smi) => smi.master.GetComponent<ToiletWorkableUse>(), this.flushing)
				.WorkableStopTransition((FlushToilet.SMInstance smi) => smi.master.GetComponent<ToiletWorkableUse>(), this.flushed);
			this.flushing.Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.Flush();
			}).PlayAnim("flush").OnAnimQueueComplete(this.flushed);
			this.flushed.EventTransition(GameHashes.OnStorageChange, this.fillingInactive, (FlushToilet.SMInstance smi) => !smi.HasContaminatedMass()).ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue);
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x002BFDDF File Offset: 0x002BDFDF
		private Chore CreateUrgentUseChore(FlushToilet.SMInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.Pee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderFull, null);
			chore.AddPrecondition(ChorePreconditions.instance.NotCurrentlyPeeing, null);
			return chore;
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x002BFE19 File Offset: 0x002BE019
		private Chore CreateBreakUseChore(FlushToilet.SMInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.BreakPee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderNotFull, null);
			return chore;
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x002BFE44 File Offset: 0x002BE044
		private Chore CreateUseChore(FlushToilet.SMInstance smi, ChoreType choreType)
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

		// Token: 0x04005C97 RID: 23703
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State disconnected;

		// Token: 0x04005C98 RID: 23704
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State backedup;

		// Token: 0x04005C99 RID: 23705
		public FlushToilet.States.ReadyStates ready;

		// Token: 0x04005C9A RID: 23706
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State fillingInactive;

		// Token: 0x04005C9B RID: 23707
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State filling;

		// Token: 0x04005C9C RID: 23708
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State flushing;

		// Token: 0x04005C9D RID: 23709
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State flushed;

		// Token: 0x04005C9E RID: 23710
		public StateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.BoolParameter outputBlocked;

		// Token: 0x02001FA4 RID: 8100
		public class ReadyStates : GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State
		{
			// Token: 0x04008CEE RID: 36078
			public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State idle;

			// Token: 0x04008CEF RID: 36079
			public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State inuse;
		}
	}
}
