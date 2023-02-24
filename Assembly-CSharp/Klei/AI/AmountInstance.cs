using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D62 RID: 3426
	[SerializationConfig(MemberSerialization.OptIn)]
	[DebuggerDisplay("{amount.Name} {value} ({deltaAttribute.value}/{minAttribute.value}/{maxAttribute.value})")]
	public class AmountInstance : ModifierInstance<Amount>, ISaveLoadable, ISim200ms
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060068AE RID: 26798 RVA: 0x0028BB84 File Offset: 0x00289D84
		public Amount amount
		{
			get
			{
				return this.modifier;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060068AF RID: 26799 RVA: 0x0028BB8C File Offset: 0x00289D8C
		// (set) Token: 0x060068B0 RID: 26800 RVA: 0x0028BB94 File Offset: 0x00289D94
		public bool paused
		{
			get
			{
				return this._paused;
			}
			set
			{
				this._paused = this.paused;
				if (this._paused)
				{
					this.Deactivate();
					return;
				}
				this.Activate();
			}
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x0028BBB7 File Offset: 0x00289DB7
		public float GetMin()
		{
			return this.minAttribute.GetTotalValue();
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x0028BBC4 File Offset: 0x00289DC4
		public float GetMax()
		{
			return this.maxAttribute.GetTotalValue();
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x0028BBD1 File Offset: 0x00289DD1
		public float GetDelta()
		{
			return this.deltaAttribute.GetTotalValue();
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x0028BBE0 File Offset: 0x00289DE0
		public AmountInstance(Amount amount, GameObject game_object)
			: base(game_object, amount)
		{
			Attributes attributes = game_object.GetAttributes();
			this.minAttribute = attributes.Add(amount.minAttribute);
			this.maxAttribute = attributes.Add(amount.maxAttribute);
			this.deltaAttribute = attributes.Add(amount.deltaAttribute);
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x0028BC32 File Offset: 0x00289E32
		public float SetValue(float value)
		{
			this.value = Mathf.Min(Mathf.Max(value, this.GetMin()), this.GetMax());
			return this.value;
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x0028BC57 File Offset: 0x00289E57
		public void Publish(float delta, float previous_value)
		{
			if (this.OnDelta != null)
			{
				this.OnDelta(delta);
			}
			if (this.OnMaxValueReached != null && previous_value < this.GetMax() && this.value >= this.GetMax())
			{
				this.OnMaxValueReached();
			}
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x0028BC98 File Offset: 0x00289E98
		public float ApplyDelta(float delta)
		{
			float num = this.value;
			this.SetValue(this.value + delta);
			this.Publish(delta, num);
			return this.value;
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x0028BCC9 File Offset: 0x00289EC9
		public string GetValueString()
		{
			return this.amount.GetValueString(this);
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x0028BCD7 File Offset: 0x00289ED7
		public string GetDescription()
		{
			return this.amount.GetDescription(this);
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x0028BCE5 File Offset: 0x00289EE5
		public string GetTooltip()
		{
			return this.amount.GetTooltip(this);
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x0028BCF3 File Offset: 0x00289EF3
		public void Activate()
		{
			SimAndRenderScheduler.instance.Add(this, false);
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x0028BD01 File Offset: 0x00289F01
		public void Sim200ms(float dt)
		{
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x0028BD04 File Offset: 0x00289F04
		public static void BatchUpdate(List<UpdateBucketWithUpdater<ISim200ms>.Entry> amount_instances, float time_delta)
		{
			if (time_delta == 0f)
			{
				return;
			}
			AmountInstance.BatchUpdateContext batchUpdateContext = new AmountInstance.BatchUpdateContext(amount_instances, time_delta);
			AmountInstance.batch_update_job.Reset(batchUpdateContext);
			int num = 512;
			for (int i = 0; i < amount_instances.Count; i += num)
			{
				int num2 = i + num;
				if (amount_instances.Count < num2)
				{
					num2 = amount_instances.Count;
				}
				AmountInstance.batch_update_job.Add(new AmountInstance.BatchUpdateTask(i, num2));
			}
			GlobalJobManager.Run(AmountInstance.batch_update_job);
			batchUpdateContext.Finish();
			AmountInstance.batch_update_job.Reset(null);
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x0028BD84 File Offset: 0x00289F84
		public void Deactivate()
		{
			SimAndRenderScheduler.instance.Remove(this);
		}

		// Token: 0x04004ED5 RID: 20181
		[Serialize]
		public float value;

		// Token: 0x04004ED6 RID: 20182
		public AttributeInstance minAttribute;

		// Token: 0x04004ED7 RID: 20183
		public AttributeInstance maxAttribute;

		// Token: 0x04004ED8 RID: 20184
		public AttributeInstance deltaAttribute;

		// Token: 0x04004ED9 RID: 20185
		public Action<float> OnDelta;

		// Token: 0x04004EDA RID: 20186
		public System.Action OnMaxValueReached;

		// Token: 0x04004EDB RID: 20187
		public bool hide;

		// Token: 0x04004EDC RID: 20188
		private bool _paused;

		// Token: 0x04004EDD RID: 20189
		private static WorkItemCollection<AmountInstance.BatchUpdateTask, AmountInstance.BatchUpdateContext> batch_update_job = new WorkItemCollection<AmountInstance.BatchUpdateTask, AmountInstance.BatchUpdateContext>();

		// Token: 0x02001E41 RID: 7745
		private class BatchUpdateContext
		{
			// Token: 0x06009B29 RID: 39721 RVA: 0x003360A8 File Offset: 0x003342A8
			public BatchUpdateContext(List<UpdateBucketWithUpdater<ISim200ms>.Entry> amount_instances, float time_delta)
			{
				for (int num = 0; num != amount_instances.Count; num++)
				{
					UpdateBucketWithUpdater<ISim200ms>.Entry entry = amount_instances[num];
					entry.lastUpdateTime = 0f;
					amount_instances[num] = entry;
				}
				this.amount_instances = amount_instances;
				this.time_delta = time_delta;
				this.results = ListPool<AmountInstance.BatchUpdateContext.Result, AmountInstance>.Allocate();
				this.results.Capacity = this.amount_instances.Count;
			}

			// Token: 0x06009B2A RID: 39722 RVA: 0x00336118 File Offset: 0x00334318
			public void Finish()
			{
				foreach (AmountInstance.BatchUpdateContext.Result result in this.results)
				{
					result.amount_instance.Publish(result.delta, result.previous);
				}
				this.results.Recycle();
			}

			// Token: 0x04008828 RID: 34856
			public List<UpdateBucketWithUpdater<ISim200ms>.Entry> amount_instances;

			// Token: 0x04008829 RID: 34857
			public float time_delta;

			// Token: 0x0400882A RID: 34858
			public ListPool<AmountInstance.BatchUpdateContext.Result, AmountInstance>.PooledList results;

			// Token: 0x02002D88 RID: 11656
			public struct Result
			{
				// Token: 0x0400B9F0 RID: 47600
				public AmountInstance amount_instance;

				// Token: 0x0400B9F1 RID: 47601
				public float previous;

				// Token: 0x0400B9F2 RID: 47602
				public float delta;
			}
		}

		// Token: 0x02001E42 RID: 7746
		private struct BatchUpdateTask : IWorkItem<AmountInstance.BatchUpdateContext>
		{
			// Token: 0x06009B2B RID: 39723 RVA: 0x00336188 File Offset: 0x00334388
			public BatchUpdateTask(int start, int end)
			{
				this.start = start;
				this.end = end;
			}

			// Token: 0x06009B2C RID: 39724 RVA: 0x00336198 File Offset: 0x00334398
			public void Run(AmountInstance.BatchUpdateContext context)
			{
				for (int num = this.start; num != this.end; num++)
				{
					AmountInstance amountInstance = (AmountInstance)context.amount_instances[num].data;
					float num2 = amountInstance.GetDelta() * context.time_delta;
					if (num2 != 0f)
					{
						context.results.Add(new AmountInstance.BatchUpdateContext.Result
						{
							amount_instance = amountInstance,
							previous = amountInstance.value,
							delta = num2
						});
						amountInstance.SetValue(amountInstance.value + num2);
					}
				}
			}

			// Token: 0x0400882B RID: 34859
			private int start;

			// Token: 0x0400882C RID: 34860
			private int end;
		}
	}
}
