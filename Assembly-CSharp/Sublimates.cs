using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200099B RID: 2459
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Sublimates")]
public class Sublimates : KMonoBehaviour, ISim200ms
{
	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x060048F1 RID: 18673 RVA: 0x00198BC0 File Offset: 0x00196DC0
	public float Temperature
	{
		get
		{
			return this.primaryElement.Temperature;
		}
	}

	// Token: 0x060048F2 RID: 18674 RVA: 0x00198BCD File Offset: 0x00196DCD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Sublimates>(-2064133523, Sublimates.OnAbsorbDelegate);
		base.Subscribe<Sublimates>(1335436905, Sublimates.OnSplitFromChunkDelegate);
		this.simRenderLoadBalance = true;
	}

	// Token: 0x060048F3 RID: 18675 RVA: 0x00198BFE File Offset: 0x00196DFE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.flowAccumulator = Game.Instance.accumulators.Add("EmittedMass", this);
		this.RefreshStatusItem(Sublimates.EmitState.Emitting);
	}

	// Token: 0x060048F4 RID: 18676 RVA: 0x00198C28 File Offset: 0x00196E28
	protected override void OnCleanUp()
	{
		this.flowAccumulator = Game.Instance.accumulators.Remove(this.flowAccumulator);
		base.OnCleanUp();
	}

	// Token: 0x060048F5 RID: 18677 RVA: 0x00198C4C File Offset: 0x00196E4C
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable != null)
		{
			Sublimates component = pickupable.GetComponent<Sublimates>();
			if (component != null)
			{
				this.sublimatedMass += component.sublimatedMass;
			}
		}
	}

	// Token: 0x060048F6 RID: 18678 RVA: 0x00198C8C File Offset: 0x00196E8C
	private void OnSplitFromChunk(object data)
	{
		Pickupable pickupable = data as Pickupable;
		PrimaryElement component = pickupable.GetComponent<PrimaryElement>();
		Sublimates component2 = pickupable.GetComponent<Sublimates>();
		if (component2 == null)
		{
			return;
		}
		float mass = this.primaryElement.Mass;
		float mass2 = component.Mass;
		float num = mass / (mass2 + mass);
		this.sublimatedMass = component2.sublimatedMass * num;
		float num2 = 1f - num;
		component2.sublimatedMass *= num2;
	}

	// Token: 0x060048F7 RID: 18679 RVA: 0x00198CF8 File Offset: 0x00196EF8
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		bool flag = this.HasTag(GameTags.Sealed);
		Pickupable component = base.GetComponent<Pickupable>();
		Storage storage = ((component != null) ? component.storage : null);
		if (flag && !this.decayStorage)
		{
			return;
		}
		if (flag && storage != null && storage.HasTag(GameTags.CorrosionProof))
		{
			return;
		}
		Element element = ElementLoader.FindElementByHash(this.info.sublimatedElement);
		if (this.primaryElement.Temperature <= element.lowTemp)
		{
			this.RefreshStatusItem(Sublimates.EmitState.BlockedOnTemperature);
			return;
		}
		float num2 = Grid.Mass[num];
		if (num2 < this.info.maxDestinationMass)
		{
			float num3 = this.primaryElement.Mass;
			if (num3 > 0f)
			{
				float num4 = Mathf.Pow(num3, this.info.massPower);
				float num5 = Mathf.Max(this.info.sublimationRate, this.info.sublimationRate * num4);
				num5 *= dt;
				num5 = Mathf.Min(num5, num3);
				this.sublimatedMass += num5;
				num3 -= num5;
				if (this.sublimatedMass > this.info.minSublimationAmount)
				{
					float num6 = this.sublimatedMass / this.primaryElement.Mass;
					byte b;
					int num7;
					if (this.info.diseaseIdx == 255)
					{
						b = this.primaryElement.DiseaseIdx;
						num7 = (int)((float)this.primaryElement.DiseaseCount * num6);
						this.primaryElement.ModifyDiseaseCount(-num7, "Sublimates.SimUpdate");
					}
					else
					{
						float num8 = this.sublimatedMass / this.info.sublimationRate;
						b = this.info.diseaseIdx;
						num7 = (int)((float)this.info.diseaseCount * num8);
					}
					float num9 = Mathf.Min(this.sublimatedMass, this.info.maxDestinationMass - num2);
					if (num9 <= 0f)
					{
						this.RefreshStatusItem(Sublimates.EmitState.BlockedOnPressure);
						return;
					}
					this.Emit(num, num9, this.primaryElement.Temperature, b, num7);
					this.sublimatedMass = Mathf.Max(0f, this.sublimatedMass - num9);
					this.primaryElement.Mass = Mathf.Max(0f, this.primaryElement.Mass - num9);
					this.UpdateStorage();
					this.RefreshStatusItem(Sublimates.EmitState.Emitting);
					if (flag && this.decayStorage && storage != null)
					{
						storage.Trigger(-794517298, new BuildingHP.DamageSourceInfo
						{
							damage = 1,
							source = BUILDINGS.DAMAGESOURCES.CORROSIVE_ELEMENT,
							popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.CORROSIVE_ELEMENT,
							fullDamageEffectName = "smoke_damage_kanim"
						});
						return;
					}
				}
			}
			else if (this.sublimatedMass > 0f)
			{
				float num10 = Mathf.Min(this.sublimatedMass, this.info.maxDestinationMass - num2);
				if (num10 > 0f)
				{
					this.Emit(num, num10, this.primaryElement.Temperature, this.primaryElement.DiseaseIdx, this.primaryElement.DiseaseCount);
					this.sublimatedMass = Mathf.Max(0f, this.sublimatedMass - num10);
					this.primaryElement.Mass = Mathf.Max(0f, this.primaryElement.Mass - num10);
					this.UpdateStorage();
					this.RefreshStatusItem(Sublimates.EmitState.Emitting);
					return;
				}
				this.RefreshStatusItem(Sublimates.EmitState.BlockedOnPressure);
				return;
			}
			else if (!this.primaryElement.KeepZeroMassObject)
			{
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
		}
		else
		{
			this.RefreshStatusItem(Sublimates.EmitState.BlockedOnPressure);
		}
	}

	// Token: 0x060048F8 RID: 18680 RVA: 0x001990A8 File Offset: 0x001972A8
	private void UpdateStorage()
	{
		Pickupable component = base.GetComponent<Pickupable>();
		if (component != null && component.storage != null)
		{
			component.storage.Trigger(-1697596308, base.gameObject);
		}
	}

	// Token: 0x060048F9 RID: 18681 RVA: 0x001990EC File Offset: 0x001972EC
	private void Emit(int cell, float mass, float temperature, byte disease_idx, int disease_count)
	{
		SimMessages.AddRemoveSubstance(cell, this.info.sublimatedElement, CellEventLogger.Instance.SublimatesEmit, mass, temperature, disease_idx, disease_count, true, -1);
		Game.Instance.accumulators.Accumulate(this.flowAccumulator, mass);
		if (this.spawnFXHash != SpawnFXHashes.None)
		{
			base.transform.GetPosition().z = Grid.GetLayerZ(Grid.SceneLayer.Front);
			Game.Instance.SpawnFX(this.spawnFXHash, base.transform.GetPosition(), 0f);
		}
	}

	// Token: 0x060048FA RID: 18682 RVA: 0x00199174 File Offset: 0x00197374
	public float AvgFlowRate()
	{
		return Game.Instance.accumulators.GetAverageRate(this.flowAccumulator);
	}

	// Token: 0x060048FB RID: 18683 RVA: 0x0019918C File Offset: 0x0019738C
	private void RefreshStatusItem(Sublimates.EmitState newEmitState)
	{
		if (newEmitState == this.lastEmitState)
		{
			return;
		}
		switch (newEmitState)
		{
		case Sublimates.EmitState.Emitting:
			if (this.info.sublimatedElement == SimHashes.Oxygen)
			{
				this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingOxygenAvg, this);
			}
			else
			{
				this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingGasAvg, this);
			}
			break;
		case Sublimates.EmitState.BlockedOnPressure:
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingBlockedHighPressure, this);
			break;
		case Sublimates.EmitState.BlockedOnTemperature:
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingBlockedLowTemperature, this);
			break;
		}
		this.lastEmitState = newEmitState;
	}

	// Token: 0x04002FEC RID: 12268
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04002FED RID: 12269
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002FEE RID: 12270
	[SerializeField]
	public SpawnFXHashes spawnFXHash;

	// Token: 0x04002FEF RID: 12271
	public bool decayStorage;

	// Token: 0x04002FF0 RID: 12272
	[SerializeField]
	public Sublimates.Info info;

	// Token: 0x04002FF1 RID: 12273
	[Serialize]
	private float sublimatedMass;

	// Token: 0x04002FF2 RID: 12274
	private HandleVector<int>.Handle flowAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04002FF3 RID: 12275
	private Sublimates.EmitState lastEmitState = (Sublimates.EmitState)(-1);

	// Token: 0x04002FF4 RID: 12276
	private static readonly EventSystem.IntraObjectHandler<Sublimates> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<Sublimates>(delegate(Sublimates component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x04002FF5 RID: 12277
	private static readonly EventSystem.IntraObjectHandler<Sublimates> OnSplitFromChunkDelegate = new EventSystem.IntraObjectHandler<Sublimates>(delegate(Sublimates component, object data)
	{
		component.OnSplitFromChunk(data);
	});

	// Token: 0x0200179C RID: 6044
	[Serializable]
	public struct Info
	{
		// Token: 0x06008B54 RID: 35668 RVA: 0x002FF421 File Offset: 0x002FD621
		public Info(float rate, float min_amount, float max_destination_mass, float mass_power, SimHashes element, byte disease_idx = 255, int disease_count = 0)
		{
			this.sublimationRate = rate;
			this.minSublimationAmount = min_amount;
			this.maxDestinationMass = max_destination_mass;
			this.massPower = mass_power;
			this.sublimatedElement = element;
			this.diseaseIdx = disease_idx;
			this.diseaseCount = disease_count;
		}

		// Token: 0x04006D8D RID: 28045
		public float sublimationRate;

		// Token: 0x04006D8E RID: 28046
		public float minSublimationAmount;

		// Token: 0x04006D8F RID: 28047
		public float maxDestinationMass;

		// Token: 0x04006D90 RID: 28048
		public float massPower;

		// Token: 0x04006D91 RID: 28049
		public byte diseaseIdx;

		// Token: 0x04006D92 RID: 28050
		public int diseaseCount;

		// Token: 0x04006D93 RID: 28051
		[HashedEnum]
		public SimHashes sublimatedElement;
	}

	// Token: 0x0200179D RID: 6045
	private enum EmitState
	{
		// Token: 0x04006D95 RID: 28053
		Emitting,
		// Token: 0x04006D96 RID: 28054
		BlockedOnPressure,
		// Token: 0x04006D97 RID: 28055
		BlockedOnTemperature
	}
}
