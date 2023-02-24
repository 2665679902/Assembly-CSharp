using System;
using UnityEngine;

// Token: 0x02000572 RID: 1394
public class AlgaeHabitat : StateMachineComponent<AlgaeHabitat.SMInstance>
{
	// Token: 0x060021B3 RID: 8627 RVA: 0x000B7858 File Offset: 0x000B5A58
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		GameScheduler.Instance.Schedule("WaterFetchingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_FetchingWater, true);
		}, null, null);
		this.ConfigurePollutedWaterOutput();
		Tutorial.Instance.oxygenGenerators.Add(base.gameObject);
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x000B78C7 File Offset: 0x000B5AC7
	protected override void OnCleanUp()
	{
		Tutorial.Instance.oxygenGenerators.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x060021B5 RID: 8629 RVA: 0x000B78E8 File Offset: 0x000B5AE8
	private void ConfigurePollutedWaterOutput()
	{
		Storage storage = null;
		Tag tag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
		foreach (Storage storage2 in base.GetComponents<Storage>())
		{
			if (storage2.storageFilters.Contains(tag))
			{
				storage = storage2;
				break;
			}
		}
		foreach (ElementConverter elementConverter in base.GetComponents<ElementConverter>())
		{
			ElementConverter.OutputElement[] outputElements = elementConverter.outputElements;
			for (int j = 0; j < outputElements.Length; j++)
			{
				if (outputElements[j].elementHash == SimHashes.DirtyWater)
				{
					elementConverter.SetStorage(storage);
					break;
				}
			}
		}
		this.pollutedWaterStorage = storage;
	}

	// Token: 0x0400136D RID: 4973
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400136E RID: 4974
	private Storage pollutedWaterStorage;

	// Token: 0x0400136F RID: 4975
	[SerializeField]
	public float lightBonusMultiplier = 1.1f;

	// Token: 0x04001370 RID: 4976
	public CellOffset pressureSampleOffset = CellOffset.none;

	// Token: 0x02001199 RID: 4505
	public class SMInstance : GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.GameInstance
	{
		// Token: 0x06007735 RID: 30517 RVA: 0x002BAA08 File Offset: 0x002B8C08
		public SMInstance(AlgaeHabitat master)
			: base(master)
		{
			this.converter = master.GetComponent<ElementConverter>();
		}

		// Token: 0x06007736 RID: 30518 RVA: 0x002BAA1D File Offset: 0x002B8C1D
		public bool HasEnoughMass(Tag tag)
		{
			return this.converter.HasEnoughMass(tag, false);
		}

		// Token: 0x06007737 RID: 30519 RVA: 0x002BAA2C File Offset: 0x002B8C2C
		public bool NeedsEmptying()
		{
			return base.smi.master.pollutedWaterStorage.RemainingCapacity() <= 0f;
		}

		// Token: 0x06007738 RID: 30520 RVA: 0x002BAA50 File Offset: 0x002B8C50
		public void CreateEmptyChore()
		{
			if (this.emptyChore != null)
			{
				this.emptyChore.Cancel("dupe");
			}
			AlgaeHabitatEmpty component = base.master.GetComponent<AlgaeHabitatEmpty>();
			this.emptyChore = new WorkChore<AlgaeHabitatEmpty>(Db.Get().ChoreTypes.EmptyStorage, component, null, true, new Action<Chore>(this.OnEmptyComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
		}

		// Token: 0x06007739 RID: 30521 RVA: 0x002BAAB8 File Offset: 0x002B8CB8
		public void CancelEmptyChore()
		{
			if (this.emptyChore != null)
			{
				this.emptyChore.Cancel("Cancelled");
				this.emptyChore = null;
			}
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x002BAADC File Offset: 0x002B8CDC
		private void OnEmptyComplete(Chore chore)
		{
			this.emptyChore = null;
			base.master.pollutedWaterStorage.DropAll(true, false, default(Vector3), true, null);
		}

		// Token: 0x04005B56 RID: 23382
		public ElementConverter converter;

		// Token: 0x04005B57 RID: 23383
		public Chore emptyChore;
	}

	// Token: 0x0200119A RID: 4506
	public class States : GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat>
	{
		// Token: 0x0600773B RID: 30523 RVA: 0x002BAB10 File Offset: 0x002B8D10
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.noAlgae;
			this.root.EventTransition(GameHashes.OperationalChanged, this.notoperational, (AlgaeHabitat.SMInstance smi) => !smi.master.operational.IsOperational).EventTransition(GameHashes.OperationalChanged, this.noAlgae, (AlgaeHabitat.SMInstance smi) => smi.master.operational.IsOperational);
			this.notoperational.QueueAnim("off", false, null);
			this.gotAlgae.PlayAnim("on_pre").OnAnimQueueComplete(this.noWater);
			this.gotEmptied.PlayAnim("on_pre").OnAnimQueueComplete(this.generatingOxygen);
			this.lostAlgae.PlayAnim("on_pst").OnAnimQueueComplete(this.noAlgae);
			this.noAlgae.QueueAnim("off", false, null).EventTransition(GameHashes.OnStorageChange, this.gotAlgae, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Algae)).Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			});
			this.noWater.QueueAnim("on", false, null).Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.GetComponent<PassiveElementConsumer>().EnableConsumption(true);
			}).EventTransition(GameHashes.OnStorageChange, this.lostAlgae, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Algae))
				.EventTransition(GameHashes.OnStorageChange, this.gotWater, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Algae) && smi.HasEnoughMass(GameTags.Water));
			this.needsEmptying.QueueAnim("off", false, null).Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.CreateEmptyChore();
			}).Exit(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.CancelEmptyChore();
			})
				.ToggleStatusItem(Db.Get().BuildingStatusItems.HabitatNeedsEmptying, null)
				.EventTransition(GameHashes.OnStorageChange, this.noAlgae, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Algae) || !smi.HasEnoughMass(GameTags.Water))
				.EventTransition(GameHashes.OnStorageChange, this.gotEmptied, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Algae) && smi.HasEnoughMass(GameTags.Water) && !smi.NeedsEmptying());
			this.gotWater.PlayAnim("working_pre").OnAnimQueueComplete(this.needsEmptying);
			this.generatingOxygen.Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).Update("GeneratingOxygen", delegate(AlgaeHabitat.SMInstance smi, float dt)
			{
				int num = Grid.PosToCell(smi.master.transform.GetPosition());
				smi.converter.OutputMultiplier = ((Grid.LightCount[num] > 0) ? smi.master.lightBonusMultiplier : 1f);
			}, UpdateRate.SIM_200ms, false)
				.QueueAnim("working_loop", true, null)
				.EventTransition(GameHashes.OnStorageChange, this.stoppedGeneratingOxygen, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Water) || !smi.HasEnoughMass(GameTags.Algae) || smi.NeedsEmptying());
			this.stoppedGeneratingOxygen.PlayAnim("working_pst").OnAnimQueueComplete(this.stoppedGeneratingOxygenTransition);
			this.stoppedGeneratingOxygenTransition.EventTransition(GameHashes.OnStorageChange, this.needsEmptying, (AlgaeHabitat.SMInstance smi) => smi.NeedsEmptying()).EventTransition(GameHashes.OnStorageChange, this.noWater, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Water)).EventTransition(GameHashes.OnStorageChange, this.lostAlgae, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Algae))
				.EventTransition(GameHashes.OnStorageChange, this.gotWater, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Water) && smi.HasEnoughMass(GameTags.Algae));
		}

		// Token: 0x04005B58 RID: 23384
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State generatingOxygen;

		// Token: 0x04005B59 RID: 23385
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State stoppedGeneratingOxygen;

		// Token: 0x04005B5A RID: 23386
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State stoppedGeneratingOxygenTransition;

		// Token: 0x04005B5B RID: 23387
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State noWater;

		// Token: 0x04005B5C RID: 23388
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State noAlgae;

		// Token: 0x04005B5D RID: 23389
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State needsEmptying;

		// Token: 0x04005B5E RID: 23390
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State gotAlgae;

		// Token: 0x04005B5F RID: 23391
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State gotWater;

		// Token: 0x04005B60 RID: 23392
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State gotEmptied;

		// Token: 0x04005B61 RID: 23393
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State lostAlgae;

		// Token: 0x04005B62 RID: 23394
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State notoperational;
	}
}
