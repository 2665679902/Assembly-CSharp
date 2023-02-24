using System;
using System.Collections.Generic;

// Token: 0x0200084B RID: 2123
public class SuitWearer : GameStateMachine<SuitWearer, SuitWearer.Instance>
{
	// Token: 0x06003D24 RID: 15652 RVA: 0x00155C84 File Offset: 0x00153E84
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.PathAdvanced, delegate(SuitWearer.Instance smi, object data)
		{
			smi.OnPathAdvanced(data);
		}).DoNothing();
		this.suit.DoNothing();
		this.nosuit.DoNothing();
	}

	// Token: 0x04002806 RID: 10246
	public GameStateMachine<SuitWearer, SuitWearer.Instance, IStateMachineTarget, object>.State suit;

	// Token: 0x04002807 RID: 10247
	public GameStateMachine<SuitWearer, SuitWearer.Instance, IStateMachineTarget, object>.State nosuit;

	// Token: 0x020015F2 RID: 5618
	public new class Instance : GameStateMachine<SuitWearer, SuitWearer.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008601 RID: 34305 RVA: 0x002ED9B4 File Offset: 0x002EBBB4
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.navigator = master.GetComponent<Navigator>();
			this.navigator.SetFlags(PathFinder.PotentialPath.Flags.PerformSuitChecks);
			this.prefabInstanceID = this.navigator.GetComponent<KPrefabID>().InstanceID;
			master.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("snapto_neck", false);
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x002EDA22 File Offset: 0x002EBC22
		public void OnPathAdvanced(object data)
		{
			if (this.navigator.CurrentNavType == NavType.Hover && (this.navigator.flags & PathFinder.PotentialPath.Flags.HasJetPack) <= PathFinder.PotentialPath.Flags.None)
			{
				this.navigator.SetCurrentNavType(NavType.Floor);
			}
			this.UnreserveSuits();
			this.ReserveSuits();
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x002EDA5C File Offset: 0x002EBC5C
		public void ReserveSuits()
		{
			PathFinder.Path path = this.navigator.path;
			if (path.nodes == null)
			{
				return;
			}
			bool flag = (this.navigator.flags & PathFinder.PotentialPath.Flags.HasAtmoSuit) > PathFinder.PotentialPath.Flags.None;
			bool flag2 = (this.navigator.flags & PathFinder.PotentialPath.Flags.HasJetPack) > PathFinder.PotentialPath.Flags.None;
			bool flag3 = (this.navigator.flags & PathFinder.PotentialPath.Flags.HasOxygenMask) > PathFinder.PotentialPath.Flags.None;
			for (int i = 0; i < path.nodes.Count - 1; i++)
			{
				int cell = path.nodes[i].cell;
				Grid.SuitMarker.Flags flags = (Grid.SuitMarker.Flags)0;
				PathFinder.PotentialPath.Flags flags2 = PathFinder.PotentialPath.Flags.None;
				if (Grid.TryGetSuitMarkerFlags(cell, out flags, out flags2))
				{
					bool flag4 = (flags2 & PathFinder.PotentialPath.Flags.HasAtmoSuit) > PathFinder.PotentialPath.Flags.None;
					bool flag5 = (flags2 & PathFinder.PotentialPath.Flags.HasJetPack) > PathFinder.PotentialPath.Flags.None;
					bool flag6 = (flags2 & PathFinder.PotentialPath.Flags.HasOxygenMask) > PathFinder.PotentialPath.Flags.None;
					bool flag7 = flag2 || flag || flag3;
					bool flag8 = flag4 == flag && flag5 == flag2 && flag6 == flag3;
					bool flag9 = SuitMarker.DoesTraversalDirectionRequireSuit(cell, path.nodes[i + 1].cell, flags);
					if (flag9 && !flag7)
					{
						Grid.ReserveSuit(cell, this.prefabInstanceID, true);
						this.suitReservations.Add(cell);
						if (flag4)
						{
							flag = true;
						}
						if (flag5)
						{
							flag2 = true;
						}
						if (flag6)
						{
							flag3 = true;
						}
					}
					else if (!flag9 && flag8 && Grid.HasEmptyLocker(cell, this.prefabInstanceID))
					{
						Grid.ReserveEmptyLocker(cell, this.prefabInstanceID, true);
						this.emptyLockerReservations.Add(cell);
						if (flag4)
						{
							flag = false;
						}
						if (flag5)
						{
							flag2 = false;
						}
						if (flag6)
						{
							flag3 = false;
						}
					}
				}
			}
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x002EDBD4 File Offset: 0x002EBDD4
		public void UnreserveSuits()
		{
			foreach (int num in this.suitReservations)
			{
				if (Grid.HasSuitMarker[num])
				{
					Grid.ReserveSuit(num, this.prefabInstanceID, false);
				}
			}
			this.suitReservations.Clear();
			foreach (int num2 in this.emptyLockerReservations)
			{
				if (Grid.HasSuitMarker[num2])
				{
					Grid.ReserveEmptyLocker(num2, this.prefabInstanceID, false);
				}
			}
			this.emptyLockerReservations.Clear();
		}

		// Token: 0x04006866 RID: 26726
		private List<int> suitReservations = new List<int>();

		// Token: 0x04006867 RID: 26727
		private List<int> emptyLockerReservations = new List<int>();

		// Token: 0x04006868 RID: 26728
		private Navigator navigator;

		// Token: 0x04006869 RID: 26729
		private int prefabInstanceID;
	}
}
