using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200067F RID: 1663
public class ChainedBuilding : GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>
{
	// Token: 0x06002CD2 RID: 11474 RVA: 0x000EB2C8 File Offset: 0x000E94C8
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.unlinked;
		StatusItem statusItem = new StatusItem("NotLinkedToHeadStatusItem", BUILDING.STATUSITEMS.NOTLINKEDTOHEAD.NAME, BUILDING.STATUSITEMS.NOTLINKEDTOHEAD.TOOLTIP, "status_item_not_linked", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
		statusItem.resolveTooltipCallback = delegate(string tooltip, object obj)
		{
			ChainedBuilding.StatesInstance statesInstance = (ChainedBuilding.StatesInstance)obj;
			return tooltip.Replace("{headBuilding}", Strings.Get("STRINGS.BUILDINGS.PREFABS." + statesInstance.def.headBuildingTag.Name.ToUpper() + ".NAME")).Replace("{linkBuilding}", Strings.Get("STRINGS.BUILDINGS.PREFABS." + statesInstance.def.linkBuildingTag.Name.ToUpper() + ".NAME"));
		};
		this.root.OnSignal(this.doRelink, this.DEBUG_relink);
		this.unlinked.ParamTransition<bool>(this.isConnectedToHead, this.linked, GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.IsTrue).ToggleStatusItem(statusItem, (ChainedBuilding.StatesInstance smi) => smi);
		this.linked.ParamTransition<bool>(this.isConnectedToHead, this.unlinked, GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.IsFalse);
		this.DEBUG_relink.Enter(delegate(ChainedBuilding.StatesInstance smi)
		{
			smi.DEBUG_Relink();
		});
	}

	// Token: 0x04001AC3 RID: 6851
	private GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.State unlinked;

	// Token: 0x04001AC4 RID: 6852
	private GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.State linked;

	// Token: 0x04001AC5 RID: 6853
	private GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.State DEBUG_relink;

	// Token: 0x04001AC6 RID: 6854
	private StateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.BoolParameter isConnectedToHead = new StateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.BoolParameter();

	// Token: 0x04001AC7 RID: 6855
	private StateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.Signal doRelink;

	// Token: 0x02001344 RID: 4932
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006001 RID: 24577
		public Tag headBuildingTag;

		// Token: 0x04006002 RID: 24578
		public Tag linkBuildingTag;

		// Token: 0x04006003 RID: 24579
		public ObjectLayer objectLayer;
	}

	// Token: 0x02001345 RID: 4933
	public class StatesInstance : GameStateMachine<ChainedBuilding, ChainedBuilding.StatesInstance, IStateMachineTarget, ChainedBuilding.Def>.GameInstance
	{
		// Token: 0x06007D2A RID: 32042 RVA: 0x002D33FC File Offset: 0x002D15FC
		public StatesInstance(IStateMachineTarget master, ChainedBuilding.Def def)
			: base(master, def)
		{
			BuildingDef def2 = master.GetComponent<Building>().Def;
			this.widthInCells = def2.WidthInCells;
			int num = Grid.PosToCell(this);
			this.neighbourCheckCells = new List<int>
			{
				Grid.OffsetCell(num, -(this.widthInCells - 1) / 2 - 1, 0),
				Grid.OffsetCell(num, this.widthInCells / 2 + 1, 0)
			};
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x002D346C File Offset: 0x002D166C
		public override void StartSM()
		{
			base.StartSM();
			bool flag = false;
			HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.PooledHashSet pooledHashSet = HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.Allocate();
			this.CollectToChain(ref pooledHashSet, ref flag, null);
			this.PropogateFoundHead(flag, pooledHashSet);
			this.PropagateChangedEvent(this, pooledHashSet);
			pooledHashSet.Recycle();
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x002D34A8 File Offset: 0x002D16A8
		public void DEBUG_Relink()
		{
			bool flag = false;
			HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.PooledHashSet pooledHashSet = HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.Allocate();
			this.CollectToChain(ref pooledHashSet, ref flag, null);
			this.PropogateFoundHead(flag, pooledHashSet);
			pooledHashSet.Recycle();
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x002D34D8 File Offset: 0x002D16D8
		protected override void OnCleanUp()
		{
			HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.PooledHashSet pooledHashSet = HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.Allocate();
			foreach (int num in this.neighbourCheckCells)
			{
				bool flag = false;
				this.CollectNeighbourToChain(num, ref pooledHashSet, ref flag, this);
				this.PropogateFoundHead(flag, pooledHashSet);
				this.PropagateChangedEvent(this, pooledHashSet);
				pooledHashSet.Clear();
			}
			pooledHashSet.Recycle();
			base.OnCleanUp();
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x002D355C File Offset: 0x002D175C
		public HashSet<ChainedBuilding.StatesInstance> GetLinkedBuildings(ref HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.PooledHashSet chain)
		{
			bool flag = false;
			this.CollectToChain(ref chain, ref flag, null);
			return chain;
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x002D3578 File Offset: 0x002D1778
		private void PropogateFoundHead(bool foundHead, HashSet<ChainedBuilding.StatesInstance> chain)
		{
			foreach (ChainedBuilding.StatesInstance statesInstance in chain)
			{
				statesInstance.sm.isConnectedToHead.Set(foundHead, statesInstance, false);
			}
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x002D35D4 File Offset: 0x002D17D4
		private void PropagateChangedEvent(ChainedBuilding.StatesInstance changedLink, HashSet<ChainedBuilding.StatesInstance> chain)
		{
			foreach (ChainedBuilding.StatesInstance statesInstance in chain)
			{
				statesInstance.Trigger(-1009905786, changedLink);
			}
		}

		// Token: 0x06007D31 RID: 32049 RVA: 0x002D3628 File Offset: 0x002D1828
		private void CollectToChain(ref HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.PooledHashSet chain, ref bool foundHead, ChainedBuilding.StatesInstance ignoredLink = null)
		{
			if (ignoredLink != null && ignoredLink == this)
			{
				return;
			}
			if (chain.Contains(this))
			{
				return;
			}
			chain.Add(this);
			if (base.HasTag(base.def.headBuildingTag))
			{
				foundHead = true;
			}
			foreach (int num in this.neighbourCheckCells)
			{
				this.CollectNeighbourToChain(num, ref chain, ref foundHead, null);
			}
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x002D36B0 File Offset: 0x002D18B0
		private void CollectNeighbourToChain(int cell, ref HashSetPool<ChainedBuilding.StatesInstance, ChainedBuilding.StatesInstance>.PooledHashSet chain, ref bool foundHead, ChainedBuilding.StatesInstance ignoredLink = null)
		{
			GameObject gameObject = Grid.Objects[cell, (int)base.def.objectLayer];
			if (gameObject == null)
			{
				return;
			}
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (!component.HasTag(base.def.linkBuildingTag) && !component.IsPrefabID(base.def.headBuildingTag))
			{
				return;
			}
			ChainedBuilding.StatesInstance smi = gameObject.GetSMI<ChainedBuilding.StatesInstance>();
			if (smi != null)
			{
				smi.CollectToChain(ref chain, ref foundHead, ignoredLink);
			}
		}

		// Token: 0x04006004 RID: 24580
		private int widthInCells;

		// Token: 0x04006005 RID: 24581
		private List<int> neighbourCheckCells;
	}
}
