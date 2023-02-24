using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000825 RID: 2085
public class DecompositionMonitor : GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance>
{
	// Token: 0x06003C6B RID: 15467 RVA: 0x00150A70 File Offset: 0x0014EC70
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.Update("UpdateDecomposition", delegate(DecompositionMonitor.Instance smi, float dt)
		{
			smi.UpdateDecomposition(dt);
		}, UpdateRate.SIM_200ms, false).ParamTransition<float>(this.decomposition, this.rotten, GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.IsGTEOne).ToggleAttributeModifier("Dead", (DecompositionMonitor.Instance smi) => smi.satisfiedDecorModifier, null)
			.ToggleAttributeModifier("Dead", (DecompositionMonitor.Instance smi) => smi.satisfiedDecorRadiusModifier, null);
		this.rotten.DefaultState(this.rotten.exposed).ToggleStatusItem(Db.Get().DuplicantStatusItems.Rotten, null).ToggleAttributeModifier("Rotten", (DecompositionMonitor.Instance smi) => smi.rottenDecorModifier, null)
			.ToggleAttributeModifier("Rotten", (DecompositionMonitor.Instance smi) => smi.rottenDecorRadiusModifier, null);
		this.rotten.exposed.DefaultState(this.rotten.exposed.openair).EventTransition(GameHashes.OnStore, this.rotten.stored, (DecompositionMonitor.Instance smi) => !smi.IsExposed());
		this.rotten.exposed.openair.Enter(delegate(DecompositionMonitor.Instance smi)
		{
			if (smi.spawnsRotMonsters)
			{
				smi.ScheduleGoTo(UnityEngine.Random.Range(150f, 300f), this.rotten.spawningmonster);
			}
		}).Transition(this.rotten.exposed.submerged, (DecompositionMonitor.Instance smi) => smi.IsSubmerged(), UpdateRate.SIM_200ms).ToggleFX((DecompositionMonitor.Instance smi) => this.CreateFX(smi));
		this.rotten.exposed.submerged.DefaultState(this.rotten.exposed.submerged.idle).Transition(this.rotten.exposed.openair, (DecompositionMonitor.Instance smi) => !smi.IsSubmerged(), UpdateRate.SIM_200ms);
		this.rotten.exposed.submerged.idle.ScheduleGoTo(0.25f, this.rotten.exposed.submerged.dirtywater);
		this.rotten.exposed.submerged.dirtywater.Enter("DirtyWater", delegate(DecompositionMonitor.Instance smi)
		{
			smi.DirtyWater(smi.dirtyWaterMaxRange);
		}).GoTo(this.rotten.exposed.submerged.idle);
		this.rotten.spawningmonster.Enter(delegate(DecompositionMonitor.Instance smi)
		{
			if (this.remainingRotMonsters > 0)
			{
				this.remainingRotMonsters--;
				GameUtil.KInstantiate(Assets.GetPrefab(new Tag("Glom")), smi.transform.GetPosition(), Grid.SceneLayer.Creatures, null, 0).SetActive(true);
			}
			smi.GoTo(this.rotten.exposed);
		});
		this.rotten.stored.EventTransition(GameHashes.OnStore, this.rotten.exposed, (DecompositionMonitor.Instance smi) => smi.IsExposed());
	}

	// Token: 0x06003C6C RID: 15468 RVA: 0x00150DB0 File Offset: 0x0014EFB0
	private FliesFX.Instance CreateFX(DecompositionMonitor.Instance smi)
	{
		if (!smi.isMasterNull)
		{
			return new FliesFX.Instance(smi.master, new Vector3(0f, 0f, -0.1f));
		}
		return null;
	}

	// Token: 0x0400275A RID: 10074
	public StateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.FloatParameter decomposition;

	// Token: 0x0400275B RID: 10075
	[SerializeField]
	public int remainingRotMonsters = 3;

	// Token: 0x0400275C RID: 10076
	public GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x0400275D RID: 10077
	public DecompositionMonitor.RottenState rotten;

	// Token: 0x02001597 RID: 5527
	public class SubmergedState : GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006722 RID: 26402
		public GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04006723 RID: 26403
		public GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State dirtywater;
	}

	// Token: 0x02001598 RID: 5528
	public class ExposedState : GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006724 RID: 26404
		public DecompositionMonitor.SubmergedState submerged;

		// Token: 0x04006725 RID: 26405
		public GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State openair;
	}

	// Token: 0x02001599 RID: 5529
	public class RottenState : GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006726 RID: 26406
		public DecompositionMonitor.ExposedState exposed;

		// Token: 0x04006727 RID: 26407
		public GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State stored;

		// Token: 0x04006728 RID: 26408
		public GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.State spawningmonster;
	}

	// Token: 0x0200159A RID: 5530
	public new class Instance : GameStateMachine<DecompositionMonitor, DecompositionMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008471 RID: 33905 RVA: 0x002E9DD0 File Offset: 0x002E7FD0
		public Instance(IStateMachineTarget master, Disease disease, float decompositionRate = 0.00083333335f, bool spawnRotMonsters = true)
			: base(master)
		{
			base.gameObject.AddComponent<DecorProvider>();
			this.decompositionRate = decompositionRate;
			this.disease = disease;
			this.spawnsRotMonsters = spawnRotMonsters;
		}

		// Token: 0x06008472 RID: 33906 RVA: 0x002E9ED8 File Offset: 0x002E80D8
		public void UpdateDecomposition(float dt)
		{
			float num = dt * this.decompositionRate;
			base.sm.decomposition.Delta(num, base.smi);
		}

		// Token: 0x06008473 RID: 33907 RVA: 0x002E9F08 File Offset: 0x002E8108
		public bool IsExposed()
		{
			KPrefabID component = base.smi.GetComponent<KPrefabID>();
			return component == null || !component.HasTag(GameTags.Preserved);
		}

		// Token: 0x06008474 RID: 33908 RVA: 0x002E9F3A File Offset: 0x002E813A
		public bool IsRotten()
		{
			return base.IsInsideState(base.sm.rotten);
		}

		// Token: 0x06008475 RID: 33909 RVA: 0x002E9F4D File Offset: 0x002E814D
		public bool IsSubmerged()
		{
			return PathFinder.IsSubmerged(Grid.PosToCell(base.master.transform.GetPosition()));
		}

		// Token: 0x06008476 RID: 33910 RVA: 0x002E9F6C File Offset: 0x002E816C
		public void DirtyWater(int maxCellRange = 3)
		{
			int num = Grid.PosToCell(base.master.transform.GetPosition());
			if (Grid.Element[num].id == SimHashes.Water)
			{
				SimMessages.ReplaceElement(num, SimHashes.DirtyWater, CellEventLogger.Instance.DecompositionDirtyWater, Grid.Mass[num], Grid.Temperature[num], Grid.DiseaseIdx[num], Grid.DiseaseCount[num], -1);
				return;
			}
			if (Grid.Element[num].id == SimHashes.DirtyWater)
			{
				int[] array = new int[4];
				for (int i = 0; i < maxCellRange; i++)
				{
					for (int j = 0; j < maxCellRange; j++)
					{
						array[0] = Grid.OffsetCell(num, new CellOffset(-i, j));
						array[1] = Grid.OffsetCell(num, new CellOffset(i, j));
						array[2] = Grid.OffsetCell(num, new CellOffset(-i, -j));
						array[3] = Grid.OffsetCell(num, new CellOffset(i, -j));
						array.Shuffle<int>();
						foreach (int num2 in array)
						{
							if (Grid.GetCellDistance(num, num2) < maxCellRange - 1 && Grid.IsValidCell(num2) && Grid.Element[num2].id == SimHashes.Water)
							{
								SimMessages.ReplaceElement(num2, SimHashes.DirtyWater, CellEventLogger.Instance.DecompositionDirtyWater, Grid.Mass[num2], Grid.Temperature[num2], Grid.DiseaseIdx[num2], Grid.DiseaseCount[num2], -1);
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x04006729 RID: 26409
		public float decompositionRate;

		// Token: 0x0400672A RID: 26410
		public Disease disease;

		// Token: 0x0400672B RID: 26411
		public int dirtyWaterMaxRange = 3;

		// Token: 0x0400672C RID: 26412
		public bool spawnsRotMonsters = true;

		// Token: 0x0400672D RID: 26413
		public AttributeModifier satisfiedDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, -65f, DUPLICANTS.MODIFIERS.DEAD.NAME, false, false, true);

		// Token: 0x0400672E RID: 26414
		public AttributeModifier satisfiedDecorRadiusModifier = new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, 4f, DUPLICANTS.MODIFIERS.DEAD.NAME, false, false, true);

		// Token: 0x0400672F RID: 26415
		public AttributeModifier rottenDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, -100f, DUPLICANTS.MODIFIERS.ROTTING.NAME, false, false, true);

		// Token: 0x04006730 RID: 26416
		public AttributeModifier rottenDecorRadiusModifier = new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, 4f, DUPLICANTS.MODIFIERS.ROTTING.NAME, false, false, true);
	}
}
