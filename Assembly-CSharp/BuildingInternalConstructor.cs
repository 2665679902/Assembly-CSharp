using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200056A RID: 1386
public class BuildingInternalConstructor : GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>
{
	// Token: 0x06002189 RID: 8585 RVA: 0x000B68DC File Offset: 0x000B4ADC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (BuildingInternalConstructor.Instance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(BuildingInternalConstructor.Instance smi)
		{
			smi.ShowConstructionSymbol(false);
		});
		this.operational.DefaultState(this.operational.constructionRequired).EventTransition(GameHashes.OperationalChanged, this.inoperational, (BuildingInternalConstructor.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.operational.constructionRequired.EventTransition(GameHashes.OnStorageChange, this.operational.constructionHappening, (BuildingInternalConstructor.Instance smi) => smi.GetMassForConstruction() != null).EventTransition(GameHashes.OnStorageChange, this.operational.constructionSatisfied, (BuildingInternalConstructor.Instance smi) => smi.HasOutputInStorage()).ToggleFetch((BuildingInternalConstructor.Instance smi) => smi.CreateFetchList(), this.operational.constructionHappening)
			.ParamTransition<bool>(this.constructionRequested, this.operational.constructionSatisfied, GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.IsFalse)
			.Enter(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(true);
			})
			.Exit(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(false);
			});
		this.operational.constructionHappening.EventTransition(GameHashes.OnStorageChange, this.operational.constructionSatisfied, (BuildingInternalConstructor.Instance smi) => smi.HasOutputInStorage()).EventTransition(GameHashes.OnStorageChange, this.operational.constructionRequired, (BuildingInternalConstructor.Instance smi) => smi.GetMassForConstruction() == null).ToggleChore((BuildingInternalConstructor.Instance smi) => smi.CreateWorkChore(), this.operational.constructionHappening, this.operational.constructionHappening)
			.ParamTransition<bool>(this.constructionRequested, this.operational.constructionSatisfied, GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.IsFalse)
			.Enter(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(true);
			})
			.Exit(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(false);
			});
		this.operational.constructionSatisfied.EventTransition(GameHashes.OnStorageChange, this.operational.constructionRequired, (BuildingInternalConstructor.Instance smi) => !smi.HasOutputInStorage() && this.constructionRequested.Get(smi)).ParamTransition<bool>(this.constructionRequested, this.operational.constructionRequired, (BuildingInternalConstructor.Instance smi, bool p) => p && !smi.HasOutputInStorage());
	}

	// Token: 0x04001346 RID: 4934
	public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State inoperational;

	// Token: 0x04001347 RID: 4935
	public BuildingInternalConstructor.OperationalStates operational;

	// Token: 0x04001348 RID: 4936
	public StateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.BoolParameter constructionRequested = new StateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.BoolParameter(true);

	// Token: 0x02001192 RID: 4498
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005B36 RID: 23350
		public DefComponent<Storage> storage;

		// Token: 0x04005B37 RID: 23351
		public float constructionMass;

		// Token: 0x04005B38 RID: 23352
		public List<string> outputIDs;

		// Token: 0x04005B39 RID: 23353
		public bool spawnIntoStorage;

		// Token: 0x04005B3A RID: 23354
		public string constructionSymbol;
	}

	// Token: 0x02001193 RID: 4499
	public class OperationalStates : GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State
	{
		// Token: 0x04005B3B RID: 23355
		public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State constructionRequired;

		// Token: 0x04005B3C RID: 23356
		public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State constructionHappening;

		// Token: 0x04005B3D RID: 23357
		public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State constructionSatisfied;
	}

	// Token: 0x02001194 RID: 4500
	public new class Instance : GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x0600770B RID: 30475 RVA: 0x002BA1FC File Offset: 0x002B83FC
		public Instance(IStateMachineTarget master, BuildingInternalConstructor.Def def)
			: base(master, def)
		{
			this.storage = def.storage.Get(this);
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new InternalConstructionCompleteCondition(this));
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x002BA22C File Offset: 0x002B842C
		protected override void OnCleanUp()
		{
			Element element = null;
			float num = 0f;
			float num2 = 0f;
			byte maxValue = byte.MaxValue;
			int num3 = 0;
			foreach (string text in base.def.outputIDs)
			{
				GameObject gameObject = this.storage.FindFirst(text);
				if (gameObject != null)
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					global::Debug.Assert(element == null || element == component.Element);
					element = component.Element;
					num2 = GameUtil.GetFinalTemperature(num, num2, component.Mass, component.Temperature);
					num += component.Mass;
					gameObject.DeleteObject();
				}
			}
			if (element != null)
			{
				element.substance.SpawnResource(base.transform.GetPosition(), num, num2, maxValue, num3, false, false, false);
			}
			base.OnCleanUp();
		}

		// Token: 0x0600770D RID: 30477 RVA: 0x002BA32C File Offset: 0x002B852C
		public FetchList2 CreateFetchList()
		{
			FetchList2 fetchList = new FetchList2(this.storage, Db.Get().ChoreTypes.Fetch);
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			fetchList.Add(component.Element.tag, null, base.def.constructionMass, Operational.State.None);
			return fetchList;
		}

		// Token: 0x0600770E RID: 30478 RVA: 0x002BA378 File Offset: 0x002B8578
		public PrimaryElement GetMassForConstruction()
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			return this.storage.FindFirstWithMass(component.Element.tag, base.def.constructionMass);
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x002BA3AD File Offset: 0x002B85AD
		public bool HasOutputInStorage()
		{
			return this.storage.FindFirst(base.def.outputIDs[0].ToTag());
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x002BA3D5 File Offset: 0x002B85D5
		public bool IsRequestingConstruction()
		{
			base.sm.constructionRequested.Get(this);
			return base.smi.sm.constructionRequested.Get(base.smi);
		}

		// Token: 0x06007711 RID: 30481 RVA: 0x002BA404 File Offset: 0x002B8604
		public void ConstructionComplete(bool force = false)
		{
			SimHashes simHashes;
			if (!force)
			{
				PrimaryElement massForConstruction = this.GetMassForConstruction();
				simHashes = massForConstruction.ElementID;
				float mass = massForConstruction.Mass;
				float num = massForConstruction.Temperature * massForConstruction.Mass;
				massForConstruction.Mass -= base.def.constructionMass;
				Mathf.Clamp(num / mass, 288.15f, 318.15f);
			}
			else
			{
				simHashes = SimHashes.Cuprite;
				float temperature = base.GetComponent<PrimaryElement>().Temperature;
			}
			foreach (string text in base.def.outputIDs)
			{
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(text), base.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0);
				gameObject.GetComponent<PrimaryElement>().SetElement(simHashes, false);
				gameObject.SetActive(true);
				if (base.def.spawnIntoStorage)
				{
					this.storage.Store(gameObject, false, false, true, false);
				}
			}
		}

		// Token: 0x06007712 RID: 30482 RVA: 0x002BA50C File Offset: 0x002B870C
		public WorkChore<BuildingInternalConstructorWorkable> CreateWorkChore()
		{
			return new WorkChore<BuildingInternalConstructorWorkable>(Db.Get().ChoreTypes.Build, base.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x06007713 RID: 30483 RVA: 0x002BA544 File Offset: 0x002B8744
		public void ShowConstructionSymbol(bool show)
		{
			KBatchedAnimController component = base.master.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				component.SetSymbolVisiblity(base.def.constructionSymbol, show);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06007714 RID: 30484 RVA: 0x002BA580 File Offset: 0x002B8780
		public string SidescreenButtonText
		{
			get
			{
				if (!base.smi.sm.constructionRequested.Get(base.smi))
				{
					return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.ALLOW_INTERNAL_CONSTRUCTOR.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
				}
				return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.DISALLOW_INTERNAL_CONSTRUCTOR.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06007715 RID: 30485 RVA: 0x002BA60C File Offset: 0x002B880C
		public string SidescreenButtonTooltip
		{
			get
			{
				if (!base.smi.sm.constructionRequested.Get(base.smi))
				{
					return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.ALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
				}
				return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.DISALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
			}
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x002BA698 File Offset: 0x002B8898
		public void OnSidescreenButtonPressed()
		{
			base.smi.sm.constructionRequested.Set(!base.smi.sm.constructionRequested.Get(base.smi), base.smi, false);
			if (DebugHandler.InstantBuildMode && base.smi.sm.constructionRequested.Get(base.smi) && !this.HasOutputInStorage())
			{
				this.ConstructionComplete(true);
			}
		}

		// Token: 0x06007717 RID: 30487 RVA: 0x002BA713 File Offset: 0x002B8913
		public void SetButtonTextOverride(ButtonMenuTextOverride text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x002BA71A File Offset: 0x002B891A
		public bool SidescreenEnabled()
		{
			return true;
		}

		// Token: 0x06007719 RID: 30489 RVA: 0x002BA71D File Offset: 0x002B891D
		public bool SidescreenButtonInteractable()
		{
			return true;
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x002BA720 File Offset: 0x002B8920
		public int ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x04005B3E RID: 23358
		private Storage storage;

		// Token: 0x04005B3F RID: 23359
		[Serialize]
		private float constructionElapsed;

		// Token: 0x04005B40 RID: 23360
		private ProgressBar progressBar;
	}
}
