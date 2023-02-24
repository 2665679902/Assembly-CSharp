using System;
using UnityEngine;

// Token: 0x020006D1 RID: 1745
public class ElementDropperMonitor : GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>
{
	// Token: 0x06002F70 RID: 12144 RVA: 0x000FAA40 File Offset: 0x000F8C40
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.EventHandler(GameHashes.DeathAnimComplete, delegate(ElementDropperMonitor.Instance smi)
		{
			smi.DropDeathElement();
		});
		this.satisfied.OnSignal(this.cellChangedSignal, this.readytodrop, (ElementDropperMonitor.Instance smi) => smi.ShouldDropElement());
		this.readytodrop.ToggleBehaviour(GameTags.Creatures.WantsToDropElements, (ElementDropperMonitor.Instance smi) => true, delegate(ElementDropperMonitor.Instance smi)
		{
			smi.GoTo(this.satisfied);
		}).EventHandler(GameHashes.ObjectMovementStateChanged, delegate(ElementDropperMonitor.Instance smi, object d)
		{
			if ((GameHashes)d == GameHashes.ObjectMovementWakeUp)
			{
				smi.GoTo(this.satisfied);
			}
		});
	}

	// Token: 0x04001C8D RID: 7309
	public GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.State satisfied;

	// Token: 0x04001C8E RID: 7310
	public GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.State readytodrop;

	// Token: 0x04001C8F RID: 7311
	public StateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.Signal cellChangedSignal;

	// Token: 0x020013C2 RID: 5058
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006184 RID: 24964
		public SimHashes dirtyEmitElement;

		// Token: 0x04006185 RID: 24965
		public float dirtyProbabilityPercent;

		// Token: 0x04006186 RID: 24966
		public float dirtyCellToTargetMass;

		// Token: 0x04006187 RID: 24967
		public float dirtyMassPerDirty;

		// Token: 0x04006188 RID: 24968
		public float dirtyMassReleaseOnDeath;

		// Token: 0x04006189 RID: 24969
		public byte emitDiseaseIdx = byte.MaxValue;

		// Token: 0x0400618A RID: 24970
		public float emitDiseasePerKg;
	}

	// Token: 0x020013C3 RID: 5059
	public new class Instance : GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.GameInstance
	{
		// Token: 0x06007EED RID: 32493 RVA: 0x002DA7D6 File Offset: 0x002D89D6
		public Instance(IStateMachineTarget master, ElementDropperMonitor.Def def)
			: base(master, def)
		{
			Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "ElementDropperMonitor.Instance");
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x002DA802 File Offset: 0x002D8A02
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x002DA827 File Offset: 0x002D8A27
		private void OnCellChange()
		{
			base.sm.cellChangedSignal.Trigger(this);
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x002DA83A File Offset: 0x002D8A3A
		public bool ShouldDropElement()
		{
			return this.IsValidDropCell() && UnityEngine.Random.Range(0f, 100f) < base.def.dirtyProbabilityPercent;
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x002DA864 File Offset: 0x002D8A64
		public void DropDeathElement()
		{
			this.DropElement(base.def.dirtyMassReleaseOnDeath, base.def.dirtyEmitElement, base.def.emitDiseaseIdx, Mathf.RoundToInt(base.def.dirtyMassReleaseOnDeath * base.def.dirtyMassPerDirty));
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x002DA8B4 File Offset: 0x002D8AB4
		public void DropPeriodicElement()
		{
			this.DropElement(base.def.dirtyMassPerDirty, base.def.dirtyEmitElement, base.def.emitDiseaseIdx, Mathf.RoundToInt(base.def.emitDiseasePerKg * base.def.dirtyMassPerDirty));
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x002DA904 File Offset: 0x002D8B04
		public void DropElement(float mass, SimHashes element_id, byte disease_idx, int disease_count)
		{
			if (mass <= 0f)
			{
				return;
			}
			Element element = ElementLoader.FindElementByHash(element_id);
			float temperature = base.GetComponent<PrimaryElement>().Temperature;
			if (element.IsGas || element.IsLiquid)
			{
				SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), element_id, CellEventLogger.Instance.ElementConsumerSimUpdate, mass, temperature, disease_idx, disease_count, true, -1);
			}
			else if (element.IsSolid)
			{
				element.substance.SpawnResource(base.transform.GetPosition() + new Vector3(0f, 0.5f, 0f), mass, temperature, disease_idx, disease_count, false, true, false);
			}
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, element.name, base.gameObject.transform, 1.5f, false);
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x002DA9D4 File Offset: 0x002D8BD4
		public bool IsValidDropCell()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Grid.IsValidCell(num) && Grid.IsGas(num) && Grid.Mass[num] <= 1f;
		}
	}
}
