using System;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class GasAndLiquidConsumerMonitor : GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>
{
	// Token: 0x06001907 RID: 6407 RVA: 0x00085B68 File Offset: 0x00083D68
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.cooldown;
		this.cooldown.Enter("ClearTargetCell", delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.ClearTargetCell();
		}).ScheduleGoTo((GasAndLiquidConsumerMonitor.Instance smi) => UnityEngine.Random.Range(smi.def.minCooldown, smi.def.maxCooldown), this.satisfied);
		this.satisfied.Enter("ClearTargetCell", delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.ClearTargetCell();
		}).TagTransition((GasAndLiquidConsumerMonitor.Instance smi) => smi.def.transitionTag, this.looking, false);
		this.looking.ToggleBehaviour((GasAndLiquidConsumerMonitor.Instance smi) => smi.def.behaviourTag, (GasAndLiquidConsumerMonitor.Instance smi) => smi.targetCell != -1, delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.GoTo(this.cooldown);
		}).TagTransition((GasAndLiquidConsumerMonitor.Instance smi) => smi.def.transitionTag, this.satisfied, true).Update("FindElement", delegate(GasAndLiquidConsumerMonitor.Instance smi, float dt)
		{
			smi.FindElement();
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x04000DFF RID: 3583
	private GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.State cooldown;

	// Token: 0x04000E00 RID: 3584
	private GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.State satisfied;

	// Token: 0x04000E01 RID: 3585
	private GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.State looking;

	// Token: 0x020010A2 RID: 4258
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005839 RID: 22585
		public Tag[] transitionTag = new Tag[] { GameTags.Creatures.Hungry };

		// Token: 0x0400583A RID: 22586
		public Tag behaviourTag = GameTags.Creatures.WantsToEat;

		// Token: 0x0400583B RID: 22587
		public float minCooldown = 5f;

		// Token: 0x0400583C RID: 22588
		public float maxCooldown = 5f;

		// Token: 0x0400583D RID: 22589
		public Diet diet;

		// Token: 0x0400583E RID: 22590
		public float consumptionRate = 0.5f;

		// Token: 0x0400583F RID: 22591
		public Tag consumableElementTag = Tag.Invalid;
	}

	// Token: 0x020010A3 RID: 4259
	public new class Instance : GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.GameInstance
	{
		// Token: 0x060073CC RID: 29644 RVA: 0x002B1858 File Offset: 0x002AFA58
		public Instance(IStateMachineTarget master, GasAndLiquidConsumerMonitor.Def def)
			: base(master, def)
		{
			this.navigator = base.smi.GetComponent<Navigator>();
			DebugUtil.Assert(base.smi.def.diet != null || this.storage != null, "GasAndLiquidConsumerMonitor needs either a diet or a storage");
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x002B18B0 File Offset: 0x002AFAB0
		public void ClearTargetCell()
		{
			this.targetCell = -1;
			this.massUnavailableFrameCount = 0;
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x002B18C0 File Offset: 0x002AFAC0
		public void FindElement()
		{
			this.targetCell = -1;
			this.FindTargetCell();
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x002B18D0 File Offset: 0x002AFAD0
		public bool IsConsumableCell(int cell, out Element element)
		{
			element = Grid.Element[cell];
			bool flag = true;
			bool flag2 = true;
			if (base.smi.def.consumableElementTag != Tag.Invalid)
			{
				flag = element.HasTag(base.smi.def.consumableElementTag);
			}
			if (base.smi.def.diet != null)
			{
				flag2 = false;
				Diet.Info[] infos = base.smi.def.diet.infos;
				for (int i = 0; i < infos.Length; i++)
				{
					if (infos[i].IsMatch(element.tag))
					{
						flag2 = true;
						break;
					}
				}
			}
			return flag && flag2;
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x002B1970 File Offset: 0x002AFB70
		public void FindTargetCell()
		{
			GasAndLiquidConsumerMonitor.ConsumableCellQuery consumableCellQuery = new GasAndLiquidConsumerMonitor.ConsumableCellQuery(base.smi, 25);
			this.navigator.RunQuery(consumableCellQuery);
			if (consumableCellQuery.success)
			{
				this.targetCell = consumableCellQuery.GetResultCell();
				this.targetElement = consumableCellQuery.targetElement;
			}
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x002B19B8 File Offset: 0x002AFBB8
		public void Consume(float dt)
		{
			int index = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(GasAndLiquidConsumerMonitor.Instance.OnMassConsumedCallback), this, "GasAndLiquidConsumerMonitor").index;
			SimMessages.ConsumeMass(Grid.PosToCell(this), this.targetElement.id, base.def.consumptionRate * dt, 3, index);
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x002B1A14 File Offset: 0x002AFC14
		private static void OnMassConsumedCallback(Sim.MassConsumedCallback mcd, object data)
		{
			((GasAndLiquidConsumerMonitor.Instance)data).OnMassConsumed(mcd);
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x002B1A24 File Offset: 0x002AFC24
		private void OnMassConsumed(Sim.MassConsumedCallback mcd)
		{
			if (!base.IsRunning())
			{
				return;
			}
			if (mcd.mass > 0f)
			{
				if (base.def.diet != null)
				{
					this.massUnavailableFrameCount = 0;
					Diet.Info dietInfo = base.def.diet.GetDietInfo(this.targetElement.tag);
					if (dietInfo == null)
					{
						return;
					}
					float num = dietInfo.ConvertConsumptionMassToCalories(mcd.mass);
					CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = new CreatureCalorieMonitor.CaloriesConsumedEvent
					{
						tag = this.targetElement.tag,
						calories = num
					};
					base.Trigger(-2038961714, caloriesConsumedEvent);
					return;
				}
				else if (this.storage != null)
				{
					this.storage.AddElement(this.targetElement.id, mcd.mass, mcd.temperature, mcd.diseaseIdx, mcd.diseaseCount, false, true);
					return;
				}
			}
			else
			{
				this.massUnavailableFrameCount++;
				if (this.massUnavailableFrameCount >= 2)
				{
					base.Trigger(801383139, null);
				}
			}
		}

		// Token: 0x04005840 RID: 22592
		public int targetCell = -1;

		// Token: 0x04005841 RID: 22593
		private Element targetElement;

		// Token: 0x04005842 RID: 22594
		private Navigator navigator;

		// Token: 0x04005843 RID: 22595
		private int massUnavailableFrameCount;

		// Token: 0x04005844 RID: 22596
		[MyCmpGet]
		private Storage storage;
	}

	// Token: 0x020010A4 RID: 4260
	public class ConsumableCellQuery : PathFinderQuery
	{
		// Token: 0x060073D4 RID: 29652 RVA: 0x002B1B24 File Offset: 0x002AFD24
		public ConsumableCellQuery(GasAndLiquidConsumerMonitor.Instance smi, int maxIterations)
		{
			this.smi = smi;
			this.maxIterations = maxIterations;
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x002B1B3C File Offset: 0x002AFD3C
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			int num = Grid.CellAbove(cell);
			this.success = this.smi.IsConsumableCell(cell, out this.targetElement) || (Grid.IsValidCell(num) && this.smi.IsConsumableCell(num, out this.targetElement));
			if (!this.success)
			{
				int num2 = this.maxIterations - 1;
				this.maxIterations = num2;
				return num2 <= 0;
			}
			return true;
		}

		// Token: 0x04005845 RID: 22597
		public bool success;

		// Token: 0x04005846 RID: 22598
		public Element targetElement;

		// Token: 0x04005847 RID: 22599
		private GasAndLiquidConsumerMonitor.Instance smi;

		// Token: 0x04005848 RID: 22600
		private int maxIterations;
	}
}
