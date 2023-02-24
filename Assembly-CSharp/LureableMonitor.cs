using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class LureableMonitor : GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>
{
	// Token: 0x06001916 RID: 6422 RVA: 0x000860F0 File Offset: 0x000842F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.cooldown;
		this.cooldown.ScheduleGoTo((LureableMonitor.Instance smi) => smi.def.cooldown, this.nolure);
		this.nolure.Update("FindLure", delegate(LureableMonitor.Instance smi, float dt)
		{
			smi.FindLure();
		}, UpdateRate.SIM_1000ms, false).ParamTransition<GameObject>(this.targetLure, this.haslure, (LureableMonitor.Instance smi, GameObject p) => p != null);
		this.haslure.ParamTransition<GameObject>(this.targetLure, this.nolure, (LureableMonitor.Instance smi, GameObject p) => p == null).Update("FindLure", delegate(LureableMonitor.Instance smi, float dt)
		{
			smi.FindLure();
		}, UpdateRate.SIM_1000ms, false).ToggleBehaviour(GameTags.Creatures.MoveToLure, (LureableMonitor.Instance smi) => smi.HasLure(), delegate(LureableMonitor.Instance smi)
		{
			smi.GoTo(this.cooldown);
		});
	}

	// Token: 0x04000E0A RID: 3594
	public StateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.TargetParameter targetLure;

	// Token: 0x04000E0B RID: 3595
	public GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.State nolure;

	// Token: 0x04000E0C RID: 3596
	public GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.State haslure;

	// Token: 0x04000E0D RID: 3597
	public GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.State cooldown;

	// Token: 0x020010AE RID: 4270
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x060073F7 RID: 29687 RVA: 0x002B2116 File Offset: 0x002B0316
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>
			{
				new Descriptor(UI.BUILDINGEFFECTS.CAPTURE_METHOD_LURE, UI.BUILDINGEFFECTS.TOOLTIPS.CAPTURE_METHOD_LURE, Descriptor.DescriptorType.Effect, false)
			};
		}

		// Token: 0x0400586D RID: 22637
		public float cooldown = 20f;

		// Token: 0x0400586E RID: 22638
		public Tag[] lures;
	}

	// Token: 0x020010AF RID: 4271
	public new class Instance : GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.GameInstance
	{
		// Token: 0x060073F9 RID: 29689 RVA: 0x002B2151 File Offset: 0x002B0351
		public Instance(IStateMachineTarget master, LureableMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x002B215C File Offset: 0x002B035C
		public void FindLure()
		{
			LureableMonitor.Instance.LureIterator lureIterator = new LureableMonitor.Instance.LureIterator(base.GetComponent<Navigator>(), base.def.lures);
			GameScenePartitioner.Instance.Iterate<LureableMonitor.Instance.LureIterator>(Grid.PosToCell(base.smi.transform.GetPosition()), 1, GameScenePartitioner.Instance.lure, ref lureIterator);
			lureIterator.Cleanup();
			base.sm.targetLure.Set(lureIterator.result, this, false);
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x002B21CE File Offset: 0x002B03CE
		public bool HasLure()
		{
			return base.sm.targetLure.Get(this) != null;
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x002B21E7 File Offset: 0x002B03E7
		public GameObject GetTargetLure()
		{
			return base.sm.targetLure.Get(this);
		}

		// Token: 0x02001F6F RID: 8047
		private struct LureIterator : GameScenePartitioner.Iterator
		{
			// Token: 0x170009F9 RID: 2553
			// (get) Token: 0x06009EDF RID: 40671 RVA: 0x0033F6C6 File Offset: 0x0033D8C6
			// (set) Token: 0x06009EE0 RID: 40672 RVA: 0x0033F6CE File Offset: 0x0033D8CE
			public int cost { readonly get; private set; }

			// Token: 0x170009FA RID: 2554
			// (get) Token: 0x06009EE1 RID: 40673 RVA: 0x0033F6D7 File Offset: 0x0033D8D7
			// (set) Token: 0x06009EE2 RID: 40674 RVA: 0x0033F6DF File Offset: 0x0033D8DF
			public GameObject result { readonly get; private set; }

			// Token: 0x06009EE3 RID: 40675 RVA: 0x0033F6E8 File Offset: 0x0033D8E8
			public LureIterator(Navigator navigator, Tag[] lures)
			{
				this.navigator = navigator;
				this.lures = lures;
				this.cost = -1;
				this.result = null;
			}

			// Token: 0x06009EE4 RID: 40676 RVA: 0x0033F708 File Offset: 0x0033D908
			public void Iterate(object target_obj)
			{
				Lure.Instance instance = target_obj as Lure.Instance;
				if (instance == null || !instance.IsActive() || !instance.HasAnyLure(this.lures))
				{
					return;
				}
				int navigationCost = this.navigator.GetNavigationCost(Grid.PosToCell(instance.transform.GetPosition()), instance.def.lurePoints);
				if (navigationCost != -1 && (this.cost == -1 || navigationCost < this.cost))
				{
					this.cost = navigationCost;
					this.result = instance.gameObject;
				}
			}

			// Token: 0x06009EE5 RID: 40677 RVA: 0x0033F786 File Offset: 0x0033D986
			public void Cleanup()
			{
			}

			// Token: 0x04008BBB RID: 35771
			private Navigator navigator;

			// Token: 0x04008BBC RID: 35772
			private Tag[] lures;
		}
	}
}
