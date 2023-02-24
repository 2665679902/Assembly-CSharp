using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000826 RID: 2086
public class DecorMonitor : GameStateMachine<DecorMonitor, DecorMonitor.Instance>
{
	// Token: 0x06003C71 RID: 15473 RVA: 0x00150E80 File Offset: 0x0014F080
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleAttributeModifier("DecorSmoother", (DecorMonitor.Instance smi) => smi.GetDecorModifier(), (DecorMonitor.Instance smi) => true).Update("DecorSensing", delegate(DecorMonitor.Instance smi, float dt)
		{
			smi.Update(dt);
		}, UpdateRate.SIM_200ms, false).EventHandler(GameHashes.NewDay, (DecorMonitor.Instance smi) => GameClock.Instance, delegate(DecorMonitor.Instance smi)
		{
			smi.OnNewDay();
		});
	}

	// Token: 0x0400275E RID: 10078
	public static float MAXIMUM_DECOR_VALUE = 120f;

	// Token: 0x0200159C RID: 5532
	public new class Instance : GameStateMachine<DecorMonitor, DecorMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008483 RID: 33923 RVA: 0x002EA174 File Offset: 0x002E8374
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.cycleTotalDecor = 2250f;
			this.amount = Db.Get().Amounts.Decor.Lookup(base.gameObject);
			this.modifier = new AttributeModifier(Db.Get().Amounts.Decor.deltaAttribute.Id, 1f, DUPLICANTS.NEEDS.DECOR.OBSERVED_DECOR, false, false, false);
		}

		// Token: 0x06008484 RID: 33924 RVA: 0x002EA2A5 File Offset: 0x002E84A5
		public AttributeModifier GetDecorModifier()
		{
			return this.modifier;
		}

		// Token: 0x06008485 RID: 33925 RVA: 0x002EA2B0 File Offset: 0x002E84B0
		public void Update(float dt)
		{
			int num = Grid.PosToCell(base.gameObject);
			if (!Grid.IsValidCell(num))
			{
				return;
			}
			float decorAtCell = GameUtil.GetDecorAtCell(num);
			this.cycleTotalDecor += decorAtCell * dt;
			float num2 = 0f;
			float num3 = 4.1666665f;
			if (Mathf.Abs(decorAtCell - this.amount.value) > 0.5f)
			{
				if (decorAtCell > this.amount.value)
				{
					num2 = 3f * num3;
				}
				else if (decorAtCell < this.amount.value)
				{
					num2 = -num3;
				}
			}
			else
			{
				this.amount.value = decorAtCell;
			}
			this.modifier.SetValue(num2);
		}

		// Token: 0x06008486 RID: 33926 RVA: 0x002EA354 File Offset: 0x002E8554
		public void OnNewDay()
		{
			this.yesterdaysTotalDecor = this.cycleTotalDecor;
			this.cycleTotalDecor = 0f;
			float totalValue = base.gameObject.GetAttributes().Add(Db.Get().Attributes.DecorExpectation).GetTotalValue();
			float num = this.yesterdaysTotalDecor / 600f;
			num += totalValue;
			Effects component = base.gameObject.GetComponent<Effects>();
			foreach (KeyValuePair<float, string> keyValuePair in this.effectLookup)
			{
				if (num < keyValuePair.Key)
				{
					component.Add(keyValuePair.Value, true);
					break;
				}
			}
		}

		// Token: 0x06008487 RID: 33927 RVA: 0x002EA418 File Offset: 0x002E8618
		public float GetTodaysAverageDecor()
		{
			return this.cycleTotalDecor / (GameClock.Instance.GetCurrentCycleAsPercentage() * 600f);
		}

		// Token: 0x06008488 RID: 33928 RVA: 0x002EA431 File Offset: 0x002E8631
		public float GetYesterdaysAverageDecor()
		{
			return this.yesterdaysTotalDecor / 600f;
		}

		// Token: 0x0400673C RID: 26428
		[Serialize]
		private float cycleTotalDecor;

		// Token: 0x0400673D RID: 26429
		[Serialize]
		private float yesterdaysTotalDecor;

		// Token: 0x0400673E RID: 26430
		private AmountInstance amount;

		// Token: 0x0400673F RID: 26431
		private AttributeModifier modifier;

		// Token: 0x04006740 RID: 26432
		private List<KeyValuePair<float, string>> effectLookup = new List<KeyValuePair<float, string>>
		{
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * -0.25f, "DecorMinus1"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0f, "Decor0"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0.25f, "Decor1"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0.5f, "Decor2"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0.75f, "Decor3"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE, "Decor4"),
			new KeyValuePair<float, string>(float.MaxValue, "Decor5")
		};
	}
}
