using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000948 RID: 2376
[AddComponentMenu("KMonoBehaviour/scripts/HarvestablePOIStates")]
public class HarvestablePOIStates : GameStateMachine<HarvestablePOIStates, HarvestablePOIStates.Instance, IStateMachineTarget, HarvestablePOIStates.Def>
{
	// Token: 0x06004632 RID: 17970 RVA: 0x0018B5F4 File Offset: 0x001897F4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.root.Enter(delegate(HarvestablePOIStates.Instance smi)
		{
			if (smi.configuration == null || smi.configuration.typeId == HashedString.Invalid)
			{
				smi.configuration = smi.GetComponent<HarvestablePOIConfigurator>().MakeConfiguration();
				smi.poiCapacity = UnityEngine.Random.Range(0f, smi.configuration.GetMaxCapacity());
			}
		});
		this.idle.ParamTransition<float>(this.poiCapacity, this.recharging, (HarvestablePOIStates.Instance smi, float f) => f < smi.configuration.GetMaxCapacity());
		this.recharging.EventHandler(GameHashes.NewDay, (HarvestablePOIStates.Instance smi) => GameClock.Instance, delegate(HarvestablePOIStates.Instance smi)
		{
			smi.RechargePOI(600f);
		}).ParamTransition<float>(this.poiCapacity, this.idle, (HarvestablePOIStates.Instance smi, float f) => f >= smi.configuration.GetMaxCapacity());
	}

	// Token: 0x04002E7B RID: 11899
	public GameStateMachine<HarvestablePOIStates, HarvestablePOIStates.Instance, IStateMachineTarget, HarvestablePOIStates.Def>.State idle;

	// Token: 0x04002E7C RID: 11900
	public GameStateMachine<HarvestablePOIStates, HarvestablePOIStates.Instance, IStateMachineTarget, HarvestablePOIStates.Def>.State recharging;

	// Token: 0x04002E7D RID: 11901
	public StateMachine<HarvestablePOIStates, HarvestablePOIStates.Instance, IStateMachineTarget, HarvestablePOIStates.Def>.FloatParameter poiCapacity = new StateMachine<HarvestablePOIStates, HarvestablePOIStates.Instance, IStateMachineTarget, HarvestablePOIStates.Def>.FloatParameter(1f);

	// Token: 0x0200173A RID: 5946
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200173B RID: 5947
	public new class Instance : GameStateMachine<HarvestablePOIStates, HarvestablePOIStates.Instance, IStateMachineTarget, HarvestablePOIStates.Def>.GameInstance, IGameObjectEffectDescriptor
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06008A10 RID: 35344 RVA: 0x002FA59A File Offset: 0x002F879A
		// (set) Token: 0x06008A11 RID: 35345 RVA: 0x002FA5A2 File Offset: 0x002F87A2
		public float poiCapacity
		{
			get
			{
				return this._poiCapacity;
			}
			set
			{
				this._poiCapacity = value;
				base.smi.sm.poiCapacity.Set(value, base.smi, false);
			}
		}

		// Token: 0x06008A12 RID: 35346 RVA: 0x002FA5C9 File Offset: 0x002F87C9
		public Instance(IStateMachineTarget target, HarvestablePOIStates.Def def)
			: base(target, def)
		{
		}

		// Token: 0x06008A13 RID: 35347 RVA: 0x002FA5D4 File Offset: 0x002F87D4
		public void RechargePOI(float dt)
		{
			float num = dt / this.configuration.GetRechargeTime();
			float num2 = this.configuration.GetMaxCapacity() * num;
			this.DeltaPOICapacity(num2);
		}

		// Token: 0x06008A14 RID: 35348 RVA: 0x002FA604 File Offset: 0x002F8804
		public void DeltaPOICapacity(float delta)
		{
			this.poiCapacity += delta;
			this.poiCapacity = Mathf.Min(this.configuration.GetMaxCapacity(), this.poiCapacity);
		}

		// Token: 0x06008A15 RID: 35349 RVA: 0x002FA630 File Offset: 0x002F8830
		public bool POICanBeHarvested()
		{
			return this.poiCapacity > 0f;
		}

		// Token: 0x06008A16 RID: 35350 RVA: 0x002FA640 File Offset: 0x002F8840
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			foreach (KeyValuePair<SimHashes, float> keyValuePair in this.configuration.GetElementsWithWeights())
			{
				SimHashes key = keyValuePair.Key;
				string text = ElementLoader.FindElementByHash(key).tag.ProperName();
				list.Add(new Descriptor(string.Format(UI.SPACEDESTINATIONS.HARVESTABLE_POI.POI_PRODUCTION, text), string.Format(UI.SPACEDESTINATIONS.HARVESTABLE_POI.POI_PRODUCTION_TOOLTIP, key.ToString()), Descriptor.DescriptorType.Effect, false));
			}
			list.Add(new Descriptor(string.Format("{0}/{1}", GameUtil.GetFormattedMass(this.poiCapacity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedMass(this.configuration.GetMaxCapacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), "Capacity", Descriptor.DescriptorType.Effect, false));
			return list;
		}

		// Token: 0x04006C6E RID: 27758
		[Serialize]
		public HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration configuration;

		// Token: 0x04006C6F RID: 27759
		[Serialize]
		private float _poiCapacity;
	}
}
