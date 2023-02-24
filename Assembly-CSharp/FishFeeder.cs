using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005BA RID: 1466
public class FishFeeder : GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>
{
	// Token: 0x0600245F RID: 9311 RVA: 0x000C4ADC File Offset: 0x000C2CDC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.notoperational;
		this.root.Enter(new StateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State.Callback(FishFeeder.SetupFishFeederTopAndBot)).Exit(new StateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State.Callback(FishFeeder.CleanupFishFeederTopAndBot)).EventHandler(GameHashes.OnStorageChange, new GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.GameEvent.Callback(FishFeeder.OnStorageChange))
			.EventHandler(GameHashes.RefreshUserMenu, new GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.GameEvent.Callback(FishFeeder.OnRefreshUserMenu));
		this.notoperational.TagTransition(GameTags.Operational, this.operational, false);
		this.operational.DefaultState(this.operational.on).TagTransition(GameTags.Operational, this.notoperational, true);
		this.operational.on.DoNothing();
		int num = 19;
		FishFeeder.ballSymbols = new HashedString[num];
		for (int i = 0; i < num; i++)
		{
			FishFeeder.ballSymbols[i] = "ball" + i.ToString();
		}
	}

	// Token: 0x06002460 RID: 9312 RVA: 0x000C4BD4 File Offset: 0x000C2DD4
	private static void SetupFishFeederTopAndBot(FishFeeder.Instance smi)
	{
		Storage storage = smi.Get<Storage>();
		smi.fishFeederTop = new FishFeeder.FishFeederTop(smi, FishFeeder.ballSymbols, storage.Capacity());
		smi.fishFeederTop.RefreshStorage();
		smi.fishFeederBot = new FishFeeder.FishFeederBot(smi, 10f, FishFeeder.ballSymbols);
		smi.fishFeederBot.RefreshStorage();
		smi.fishFeederTop.ToggleMutantSeedFetches(smi.ForbidMutantSeeds);
		smi.UpdateMutantSeedStatusItem();
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x000C4C42 File Offset: 0x000C2E42
	private static void CleanupFishFeederTopAndBot(FishFeeder.Instance smi)
	{
		smi.fishFeederTop.Cleanup();
		smi.fishFeederBot.Cleanup();
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x000C4C5C File Offset: 0x000C2E5C
	private static void MoveStoredContentsToConsumeOffset(FishFeeder.Instance smi)
	{
		foreach (GameObject gameObject in smi.GetComponent<Storage>().items)
		{
			if (!(gameObject == null))
			{
				FishFeeder.OnStorageChange(smi, gameObject);
			}
		}
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x000C4CC0 File Offset: 0x000C2EC0
	private static void OnStorageChange(FishFeeder.Instance smi, object data)
	{
		if ((GameObject)data == null)
		{
			return;
		}
		smi.fishFeederTop.RefreshStorage();
		smi.fishFeederBot.RefreshStorage();
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x000C4CE8 File Offset: 0x000C2EE8
	private static void OnRefreshUserMenu(FishFeeder.Instance smi, object data)
	{
		if (DlcManager.FeatureRadiationEnabled())
		{
			Game.Instance.userMenu.AddButton(smi.gameObject, new KIconButtonMenu.ButtonInfo("action_switch_toggle", smi.ForbidMutantSeeds ? UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.ACCEPT : UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.REJECT, delegate
			{
				smi.ForbidMutantSeeds = !smi.ForbidMutantSeeds;
				FishFeeder.OnRefreshUserMenu(smi, null);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.FISH_FEEDER_TOOLTIP, true), 1f);
		}
	}

	// Token: 0x040014EF RID: 5359
	public GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State notoperational;

	// Token: 0x040014F0 RID: 5360
	public FishFeeder.OperationalState operational;

	// Token: 0x040014F1 RID: 5361
	public static HashedString[] ballSymbols;

	// Token: 0x020011F3 RID: 4595
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020011F4 RID: 4596
	public class OperationalState : GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State
	{
		// Token: 0x04005C81 RID: 23681
		public GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State on;
	}

	// Token: 0x020011F5 RID: 4597
	public new class Instance : GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.GameInstance
	{
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x002BEF56 File Offset: 0x002BD156
		// (set) Token: 0x0600787F RID: 30847 RVA: 0x002BEF5E File Offset: 0x002BD15E
		public bool ForbidMutantSeeds
		{
			get
			{
				return this.forbidMutantSeeds;
			}
			set
			{
				this.forbidMutantSeeds = value;
				this.fishFeederTop.ToggleMutantSeedFetches(this.forbidMutantSeeds);
				this.UpdateMutantSeedStatusItem();
			}
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x002BEF80 File Offset: 0x002BD180
		public Instance(IStateMachineTarget master, FishFeeder.Def def)
			: base(master, def)
		{
			this.mutantSeedStatusItem = new StatusItem("FISHFEEDERACCEPTSMUTANTSEEDS", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x002BEFBE File Offset: 0x002BD1BE
		public void UpdateMutantSeedStatusItem()
		{
			base.gameObject.GetComponent<KSelectable>().ToggleStatusItem(this.mutantSeedStatusItem, DlcManager.IsContentActive("EXPANSION1_ID") && !this.forbidMutantSeeds, null);
		}

		// Token: 0x04005C82 RID: 23682
		private StatusItem mutantSeedStatusItem;

		// Token: 0x04005C83 RID: 23683
		public FishFeeder.FishFeederTop fishFeederTop;

		// Token: 0x04005C84 RID: 23684
		public FishFeeder.FishFeederBot fishFeederBot;

		// Token: 0x04005C85 RID: 23685
		[Serialize]
		private bool forbidMutantSeeds;
	}

	// Token: 0x020011F6 RID: 4598
	public class FishFeederTop : IRenderEveryTick
	{
		// Token: 0x06007882 RID: 30850 RVA: 0x002BEFF0 File Offset: 0x002BD1F0
		public FishFeederTop(FishFeeder.Instance smi, HashedString[] ball_symbols, float capacity)
		{
			this.smi = smi;
			this.ballSymbols = ball_symbols;
			this.massPerBall = capacity / (float)ball_symbols.Length;
			this.FillFeeder(this.mass);
			SimAndRenderScheduler.instance.Add(this, false);
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x002BF02C File Offset: 0x002BD22C
		private void FillFeeder(float mass)
		{
			KBatchedAnimController component = this.smi.GetComponent<KBatchedAnimController>();
			SymbolOverrideController component2 = this.smi.GetComponent<SymbolOverrideController>();
			KAnim.Build.Symbol symbol = null;
			Storage component3 = this.smi.GetComponent<Storage>();
			if (component3.items.Count > 0 && component3.items[0] != null)
			{
				symbol = this.smi.GetComponent<Storage>().items[0].GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbol("algae");
			}
			for (int i = 0; i < this.ballSymbols.Length; i++)
			{
				bool flag = mass > (float)(i + 1) * this.massPerBall;
				component.SetSymbolVisiblity(this.ballSymbols[i], flag);
				if (symbol != null)
				{
					component2.AddSymbolOverride(this.ballSymbols[i], symbol, 0);
				}
			}
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x002BF118 File Offset: 0x002BD318
		public void RefreshStorage()
		{
			float num = 0f;
			foreach (GameObject gameObject in this.smi.GetComponent<Storage>().items)
			{
				if (!(gameObject == null))
				{
					num += gameObject.GetComponent<PrimaryElement>().Mass;
				}
			}
			this.targetMass = num;
		}

		// Token: 0x06007885 RID: 30853 RVA: 0x002BF194 File Offset: 0x002BD394
		public void RenderEveryTick(float dt)
		{
			this.timeSinceLastBallAppeared += dt;
			if (this.targetMass > this.mass && this.timeSinceLastBallAppeared > 0.025f)
			{
				float num = Mathf.Min(this.massPerBall, this.targetMass - this.mass);
				this.mass += num;
				this.FillFeeder(this.mass);
				this.timeSinceLastBallAppeared = 0f;
			}
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x002BF208 File Offset: 0x002BD408
		public void Cleanup()
		{
			SimAndRenderScheduler.instance.Remove(this);
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x002BF218 File Offset: 0x002BD418
		public void ToggleMutantSeedFetches(bool allow)
		{
			StorageLocker component = this.smi.GetComponent<StorageLocker>();
			if (component != null)
			{
				component.UpdateForbiddenTag(GameTags.MutatedSeed, !allow);
			}
		}

		// Token: 0x04005C86 RID: 23686
		private FishFeeder.Instance smi;

		// Token: 0x04005C87 RID: 23687
		private float mass;

		// Token: 0x04005C88 RID: 23688
		private float targetMass;

		// Token: 0x04005C89 RID: 23689
		private HashedString[] ballSymbols;

		// Token: 0x04005C8A RID: 23690
		private float massPerBall;

		// Token: 0x04005C8B RID: 23691
		private float timeSinceLastBallAppeared;
	}

	// Token: 0x020011F7 RID: 4599
	public class FishFeederBot
	{
		// Token: 0x06007888 RID: 30856 RVA: 0x002BF24C File Offset: 0x002BD44C
		public FishFeederBot(FishFeeder.Instance smi, float mass_per_ball, HashedString[] ball_symbols)
		{
			this.smi = smi;
			this.massPerBall = mass_per_ball;
			this.anim = GameUtil.KInstantiate(Assets.GetPrefab("FishFeederBot"), smi.transform.GetPosition(), Grid.SceneLayer.Front, null, 0).GetComponent<KBatchedAnimController>();
			this.anim.transform.SetParent(smi.transform);
			this.anim.gameObject.SetActive(true);
			this.anim.SetSceneLayer(Grid.SceneLayer.Building);
			this.anim.Play("ball", KAnim.PlayMode.Once, 1f, 0f);
			this.anim.Stop();
			foreach (HashedString hashedString in ball_symbols)
			{
				this.anim.SetSymbolVisiblity(hashedString, false);
			}
			Storage[] components = smi.gameObject.GetComponents<Storage>();
			this.topStorage = components[0];
			this.botStorage = components[1];
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x002BF344 File Offset: 0x002BD544
		public void RefreshStorage()
		{
			if (this.refreshingStorage)
			{
				return;
			}
			this.refreshingStorage = true;
			foreach (GameObject gameObject in this.botStorage.items)
			{
				if (!(gameObject == null))
				{
					int num = Grid.CellBelow(Grid.CellBelow(Grid.PosToCell(this.smi.transform.GetPosition())));
					gameObject.transform.SetPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.BuildingBack));
				}
			}
			if (this.botStorage.IsEmpty())
			{
				float num2 = 0f;
				foreach (GameObject gameObject2 in this.topStorage.items)
				{
					if (!(gameObject2 == null))
					{
						num2 += gameObject2.GetComponent<PrimaryElement>().Mass;
					}
				}
				if (num2 > 0f)
				{
					this.anim.SetSymbolVisiblity(FishFeeder.FishFeederBot.HASH_FEEDBALL, true);
					this.anim.Play("ball", KAnim.PlayMode.Once, 1f, 0f);
					Pickupable pickupable = this.topStorage.items[0].GetComponent<Pickupable>().Take(this.massPerBall);
					KAnim.Build.Symbol symbol = pickupable.GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbol("algae");
					if (symbol != null)
					{
						this.anim.GetComponent<SymbolOverrideController>().AddSymbolOverride(FishFeeder.FishFeederBot.HASH_FEEDBALL, symbol, 0);
					}
					this.botStorage.Store(pickupable.gameObject, false, false, true, false);
					int num3 = Grid.CellBelow(Grid.CellBelow(Grid.PosToCell(this.smi.transform.GetPosition())));
					pickupable.transform.SetPosition(Grid.CellToPosCBC(num3, Grid.SceneLayer.BuildingUse));
				}
				else
				{
					this.anim.SetSymbolVisiblity(FishFeeder.FishFeederBot.HASH_FEEDBALL, false);
				}
			}
			this.refreshingStorage = false;
		}

		// Token: 0x0600788A RID: 30858 RVA: 0x002BF56C File Offset: 0x002BD76C
		public void Cleanup()
		{
		}

		// Token: 0x04005C8C RID: 23692
		private KBatchedAnimController anim;

		// Token: 0x04005C8D RID: 23693
		private Storage topStorage;

		// Token: 0x04005C8E RID: 23694
		private Storage botStorage;

		// Token: 0x04005C8F RID: 23695
		private bool refreshingStorage;

		// Token: 0x04005C90 RID: 23696
		private FishFeeder.Instance smi;

		// Token: 0x04005C91 RID: 23697
		private float massPerBall;

		// Token: 0x04005C92 RID: 23698
		private static readonly HashedString HASH_FEEDBALL = "feedball";
	}
}
