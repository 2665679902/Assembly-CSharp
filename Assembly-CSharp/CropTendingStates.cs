using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class CropTendingStates : GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>
{
	// Token: 0x06000324 RID: 804 RVA: 0x00018FE8 File Offset: 0x000171E8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.findCrop;
		this.root.Exit(delegate(CropTendingStates.Instance smi)
		{
			this.UnreserveCrop(smi);
			if (!smi.tendedSucceeded)
			{
				this.RestoreSymbolsVisibility(smi);
			}
		});
		this.findCrop.Enter(delegate(CropTendingStates.Instance smi)
		{
			this.FindCrop(smi);
			if (smi.sm.targetCrop.Get(smi) == null)
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			this.ReserverCrop(smi);
			smi.GoTo(this.moveToCrop);
		});
		this.moveToCrop.ToggleStatusItem(CREATURES.STATUSITEMS.DIVERGENT_WILL_TEND.NAME, CREATURES.STATUSITEMS.DIVERGENT_WILL_TEND.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).MoveTo((CropTendingStates.Instance smi) => smi.moveCell, this.tendCrop, this.behaviourcomplete, false).ParamTransition<GameObject>(this.targetCrop, this.behaviourcomplete, (CropTendingStates.Instance smi, GameObject p) => this.targetCrop.Get(smi) == null);
		this.tendCrop.DefaultState(this.tendCrop.pre).ToggleStatusItem(CREATURES.STATUSITEMS.DIVERGENT_TENDING.NAME, CREATURES.STATUSITEMS.DIVERGENT_TENDING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ParamTransition<GameObject>(this.targetCrop, this.behaviourcomplete, (CropTendingStates.Instance smi, GameObject p) => this.targetCrop.Get(smi) == null)
			.Enter(delegate(CropTendingStates.Instance smi)
			{
				smi.animSet = this.GetCropTendingAnimSet(smi);
				this.StoreSymbolsVisibility(smi);
			});
		this.tendCrop.pre.Face(this.targetCrop, 0f).PlayAnim((CropTendingStates.Instance smi) => smi.animSet.crop_tending_pre, KAnim.PlayMode.Once).OnAnimQueueComplete(this.tendCrop.tend);
		this.tendCrop.tend.Enter(delegate(CropTendingStates.Instance smi)
		{
			this.SetSymbolsVisibility(smi, false);
		}).QueueAnim((CropTendingStates.Instance smi) => smi.animSet.crop_tending, false, null).OnAnimQueueComplete(this.tendCrop.pst);
		this.tendCrop.pst.QueueAnim((CropTendingStates.Instance smi) => smi.animSet.crop_tending_pst, false, null).OnAnimQueueComplete(this.behaviourcomplete).Exit(delegate(CropTendingStates.Instance smi)
		{
			GameObject gameObject = smi.sm.targetCrop.Get(smi);
			if (gameObject != null)
			{
				if (smi.effect != null)
				{
					gameObject.GetComponent<Effects>().Add(smi.effect, true);
				}
				smi.tendedSucceeded = true;
				CropTendingStates.CropTendingEventData cropTendingEventData = new CropTendingStates.CropTendingEventData
				{
					source = smi.gameObject,
					cropId = smi.sm.targetCrop.Get(smi).PrefabID()
				};
				smi.sm.targetCrop.Get(smi).Trigger(90606262, cropTendingEventData);
				smi.Trigger(90606262, cropTendingEventData);
			}
		});
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToTendCrops, false);
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00019260 File Offset: 0x00017460
	private CropTendingStates.AnimSet GetCropTendingAnimSet(CropTendingStates.Instance smi)
	{
		CropTendingStates.AnimSet animSet;
		if (smi.def.animSetOverrides.TryGetValue(this.targetCrop.Get(smi).PrefabID(), out animSet))
		{
			return animSet;
		}
		return CropTendingStates.defaultAnimSet;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001929C File Offset: 0x0001749C
	private void FindCrop(CropTendingStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		Crop crop = null;
		int num = Grid.InvalidCell;
		int num2 = 100;
		int num3 = -1;
		foreach (Crop crop2 in Components.Crops.GetWorldItems(smi.gameObject.GetMyWorldId(), false))
		{
			if (smi.effect != null)
			{
				Effects component2 = crop2.GetComponent<Effects>();
				if (component2 != null)
				{
					bool flag = false;
					foreach (string text in smi.def.ignoreEffectGroup)
					{
						if (component2.HasEffect(text))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						continue;
					}
				}
			}
			Growing component3 = crop2.GetComponent<Growing>();
			if ((!(component3 != null) || !component3.IsGrown()) && !crop2.HasTag(GameTags.Creatures.ReservedByCreature) && Vector2.SqrMagnitude(crop2.transform.position - smi.transform.position) <= 625f)
			{
				int num4;
				smi.def.interests.TryGetValue(crop2.PrefabID(), out num4);
				if (num4 >= num3)
				{
					bool flag2 = num4 > num3;
					int num5 = Grid.PosToCell(crop2);
					int[] array = new int[]
					{
						Grid.CellLeft(num5),
						Grid.CellRight(num5)
					};
					int num6 = 100;
					int num7 = Grid.InvalidCell;
					for (int j = 0; j < array.Length; j++)
					{
						if (Grid.IsValidCell(array[j]))
						{
							int navigationCost = component.GetNavigationCost(array[j]);
							if (navigationCost != -1 && navigationCost < num6)
							{
								num6 = navigationCost;
								num7 = array[j];
							}
						}
					}
					if (num6 != -1 && num7 != Grid.InvalidCell && (flag2 || num6 < num2))
					{
						num = num7;
						num2 = num6;
						num3 = num4;
						crop = crop2;
					}
				}
			}
		}
		GameObject gameObject = ((crop != null) ? crop.gameObject : null);
		smi.sm.targetCrop.Set(gameObject, smi, false);
		smi.moveCell = num;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x000194D0 File Offset: 0x000176D0
	private void ReserverCrop(CropTendingStates.Instance smi)
	{
		GameObject gameObject = smi.sm.targetCrop.Get(smi);
		if (gameObject != null)
		{
			DebugUtil.Assert(!gameObject.HasTag(GameTags.Creatures.ReservedByCreature));
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00019518 File Offset: 0x00017718
	private void UnreserveCrop(CropTendingStates.Instance smi)
	{
		GameObject gameObject = smi.sm.targetCrop.Get(smi);
		if (gameObject != null)
		{
			gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001954C File Offset: 0x0001774C
	private void SetSymbolsVisibility(CropTendingStates.Instance smi, bool isVisible)
	{
		if (this.targetCrop.Get(smi) != null)
		{
			string[] hide_symbols_after_pre = smi.animSet.hide_symbols_after_pre;
			if (hide_symbols_after_pre != null)
			{
				KAnimControllerBase component = this.targetCrop.Get(smi).GetComponent<KAnimControllerBase>();
				if (component != null)
				{
					foreach (string text in hide_symbols_after_pre)
					{
						component.SetSymbolVisiblity(text, isVisible);
					}
				}
			}
		}
	}

	// Token: 0x0600032A RID: 810 RVA: 0x000195BC File Offset: 0x000177BC
	private void StoreSymbolsVisibility(CropTendingStates.Instance smi)
	{
		if (this.targetCrop.Get(smi) != null)
		{
			string[] hide_symbols_after_pre = smi.animSet.hide_symbols_after_pre;
			if (hide_symbols_after_pre != null)
			{
				KAnimControllerBase component = this.targetCrop.Get(smi).GetComponent<KAnimControllerBase>();
				if (component != null)
				{
					smi.symbolStates = new bool[hide_symbols_after_pre.Length];
					for (int i = 0; i < hide_symbols_after_pre.Length; i++)
					{
						smi.symbolStates[i] = component.GetSymbolVisiblity(hide_symbols_after_pre[i]);
					}
				}
			}
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001963C File Offset: 0x0001783C
	private void RestoreSymbolsVisibility(CropTendingStates.Instance smi)
	{
		if (this.targetCrop.Get(smi) != null && smi.symbolStates != null)
		{
			string[] hide_symbols_after_pre = smi.animSet.hide_symbols_after_pre;
			if (hide_symbols_after_pre != null)
			{
				KAnimControllerBase component = this.targetCrop.Get(smi).GetComponent<KAnimControllerBase>();
				if (component != null)
				{
					for (int i = 0; i < hide_symbols_after_pre.Length; i++)
					{
						component.SetSymbolVisiblity(hide_symbols_after_pre[i], smi.symbolStates[i]);
					}
				}
			}
		}
	}

	// Token: 0x0400020B RID: 523
	private const int MAX_NAVIGATE_DISTANCE = 100;

	// Token: 0x0400020C RID: 524
	private const int MAX_SQR_EUCLIDEAN_DISTANCE = 625;

	// Token: 0x0400020D RID: 525
	private static CropTendingStates.AnimSet defaultAnimSet = new CropTendingStates.AnimSet
	{
		crop_tending_pre = "crop_tending_pre",
		crop_tending = "crop_tending_loop",
		crop_tending_pst = "crop_tending_pst"
	};

	// Token: 0x0400020E RID: 526
	public StateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.TargetParameter targetCrop;

	// Token: 0x0400020F RID: 527
	private GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State findCrop;

	// Token: 0x04000210 RID: 528
	private GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State moveToCrop;

	// Token: 0x04000211 RID: 529
	private CropTendingStates.TendingStates tendCrop;

	// Token: 0x04000212 RID: 530
	private GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State behaviourcomplete;

	// Token: 0x02000E33 RID: 3635
	public class AnimSet
	{
		// Token: 0x0400511C RID: 20764
		public string crop_tending_pre;

		// Token: 0x0400511D RID: 20765
		public string crop_tending;

		// Token: 0x0400511E RID: 20766
		public string crop_tending_pst;

		// Token: 0x0400511F RID: 20767
		public string[] hide_symbols_after_pre;
	}

	// Token: 0x02000E34 RID: 3636
	public class CropTendingEventData
	{
		// Token: 0x04005120 RID: 20768
		public GameObject source;

		// Token: 0x04005121 RID: 20769
		public Tag cropId;
	}

	// Token: 0x02000E35 RID: 3637
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005122 RID: 20770
		public string effectId;

		// Token: 0x04005123 RID: 20771
		public string[] ignoreEffectGroup;

		// Token: 0x04005124 RID: 20772
		public Dictionary<Tag, int> interests = new Dictionary<Tag, int>();

		// Token: 0x04005125 RID: 20773
		public Dictionary<Tag, CropTendingStates.AnimSet> animSetOverrides = new Dictionary<Tag, CropTendingStates.AnimSet>();
	}

	// Token: 0x02000E36 RID: 3638
	public new class Instance : GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.GameInstance
	{
		// Token: 0x06006BCD RID: 27597 RVA: 0x00296F80 File Offset: 0x00295180
		public Instance(Chore<CropTendingStates.Instance> chore, CropTendingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToTendCrops);
			this.effect = Db.Get().effects.TryGet(base.smi.def.effectId);
		}

		// Token: 0x04005126 RID: 20774
		public Effect effect;

		// Token: 0x04005127 RID: 20775
		public int moveCell;

		// Token: 0x04005128 RID: 20776
		public CropTendingStates.AnimSet animSet;

		// Token: 0x04005129 RID: 20777
		public bool tendedSucceeded;

		// Token: 0x0400512A RID: 20778
		public bool[] symbolStates;
	}

	// Token: 0x02000E37 RID: 3639
	public class TendingStates : GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State
	{
		// Token: 0x0400512B RID: 20779
		public GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State pre;

		// Token: 0x0400512C RID: 20780
		public GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State tend;

		// Token: 0x0400512D RID: 20781
		public GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State pst;
	}
}
