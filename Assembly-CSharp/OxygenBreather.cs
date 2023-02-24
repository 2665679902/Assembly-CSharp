using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000884 RID: 2180
[RequireComponent(typeof(Health))]
[AddComponentMenu("KMonoBehaviour/scripts/OxygenBreather")]
public class OxygenBreather : KMonoBehaviour, ISim200ms
{
	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x06003E7E RID: 15998 RVA: 0x0015D890 File Offset: 0x0015BA90
	public float CO2EmitRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.co2Accumulator);
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x06003E7F RID: 15999 RVA: 0x0015D8A7 File Offset: 0x0015BAA7
	public HandleVector<int>.Handle O2Accumulator
	{
		get
		{
			return this.o2Accumulator;
		}
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x0015D8AF File Offset: 0x0015BAAF
	protected override void OnPrefabInit()
	{
		GameUtil.SubscribeToTags<OxygenBreather>(this, OxygenBreather.OnDeadTagAddedDelegate, true);
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x0015D8BD File Offset: 0x0015BABD
	public bool IsLowOxygen()
	{
		return this.GetOxygenPressure(this.mouthCell) < this.lowOxygenThreshold;
	}

	// Token: 0x06003E82 RID: 16002 RVA: 0x0015D8D4 File Offset: 0x0015BAD4
	protected override void OnSpawn()
	{
		this.airConsumptionRate = Db.Get().Attributes.AirConsumptionRate.Lookup(this);
		this.o2Accumulator = Game.Instance.accumulators.Add("O2", this);
		this.co2Accumulator = Game.Instance.accumulators.Add("CO2", this);
		KSelectable component = base.GetComponent<KSelectable>();
		component.AddStatusItem(Db.Get().DuplicantStatusItems.BreathingO2, this);
		component.AddStatusItem(Db.Get().DuplicantStatusItems.EmittingCO2, this);
		this.temperature = Db.Get().Amounts.Temperature.Lookup(this);
		NameDisplayScreen.Instance.RegisterComponent(base.gameObject, this, false);
	}

	// Token: 0x06003E83 RID: 16003 RVA: 0x0015D992 File Offset: 0x0015BB92
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.o2Accumulator);
		Game.Instance.accumulators.Remove(this.co2Accumulator);
		this.SetGasProvider(null);
		base.OnCleanUp();
	}

	// Token: 0x06003E84 RID: 16004 RVA: 0x0015D9CD File Offset: 0x0015BBCD
	public void Consume(Sim.MassConsumedCallback mass_consumed)
	{
		if (this.onSimConsume != null)
		{
			this.onSimConsume(mass_consumed);
		}
	}

	// Token: 0x06003E85 RID: 16005 RVA: 0x0015D9E4 File Offset: 0x0015BBE4
	public void Sim200ms(float dt)
	{
		if (!base.gameObject.HasTag(GameTags.Dead))
		{
			float num = this.airConsumptionRate.GetTotalValue() * dt;
			bool flag = this.gasProvider.ConsumeGas(this, num);
			if (flag)
			{
				if (this.gasProvider.ShouldEmitCO2())
				{
					float num2 = num * this.O2toCO2conversion;
					Game.Instance.accumulators.Accumulate(this.co2Accumulator, num2);
					this.accumulatedCO2 += num2;
					if (this.accumulatedCO2 >= this.minCO2ToEmit)
					{
						this.accumulatedCO2 -= this.minCO2ToEmit;
						Vector3 position = base.transform.GetPosition();
						Vector3 vector = position;
						vector.x += (this.facing.GetFacing() ? (-this.mouthOffset.x) : this.mouthOffset.x);
						vector.y += this.mouthOffset.y;
						vector.z -= 0.5f;
						if (Mathf.FloorToInt(vector.x) != Mathf.FloorToInt(position.x))
						{
							vector.x = Mathf.Floor(position.x) + (this.facing.GetFacing() ? 0.01f : 0.99f);
						}
						CO2Manager.instance.SpawnBreath(vector, this.minCO2ToEmit, this.temperature.value, this.facing.GetFacing());
					}
				}
				else if (this.gasProvider.ShouldStoreCO2())
				{
					Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
					if (equippable != null)
					{
						float num3 = num * this.O2toCO2conversion;
						Game.Instance.accumulators.Accumulate(this.co2Accumulator, num3);
						this.accumulatedCO2 += num3;
						if (this.accumulatedCO2 >= this.minCO2ToEmit)
						{
							this.accumulatedCO2 -= this.minCO2ToEmit;
							equippable.GetComponent<Storage>().AddGasChunk(SimHashes.CarbonDioxide, this.minCO2ToEmit, this.temperature.value, byte.MaxValue, 0, false, true);
						}
					}
				}
			}
			if (flag != this.hasAir)
			{
				this.hasAirTimer.Start();
				if (this.hasAirTimer.TryStop(2f))
				{
					this.hasAir = flag;
					return;
				}
			}
			else
			{
				this.hasAirTimer.Stop();
			}
		}
	}

	// Token: 0x06003E86 RID: 16006 RVA: 0x0015DC3D File Offset: 0x0015BE3D
	private void OnDeath(object data)
	{
		base.enabled = false;
		KSelectable component = base.GetComponent<KSelectable>();
		component.RemoveStatusItem(Db.Get().DuplicantStatusItems.BreathingO2, false);
		component.RemoveStatusItem(Db.Get().DuplicantStatusItems.EmittingCO2, false);
	}

	// Token: 0x06003E87 RID: 16007 RVA: 0x0015DC7C File Offset: 0x0015BE7C
	private int GetMouthCellAtCell(int cell, CellOffset[] offsets)
	{
		float num = 0f;
		int num2 = cell;
		foreach (CellOffset cellOffset in offsets)
		{
			int num3 = Grid.OffsetCell(cell, cellOffset);
			float oxygenPressure = this.GetOxygenPressure(num3);
			if (oxygenPressure > num && oxygenPressure > this.noOxygenThreshold)
			{
				num = oxygenPressure;
				num2 = num3;
			}
		}
		return num2;
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x06003E88 RID: 16008 RVA: 0x0015DCD4 File Offset: 0x0015BED4
	public int mouthCell
	{
		get
		{
			int num = Grid.PosToCell(this);
			return this.GetMouthCellAtCell(num, this.breathableCells);
		}
	}

	// Token: 0x06003E89 RID: 16009 RVA: 0x0015DCF5 File Offset: 0x0015BEF5
	public bool IsBreathableElementAtCell(int cell, CellOffset[] offsets = null)
	{
		return this.GetBreathableElementAtCell(cell, offsets) != SimHashes.Vacuum;
	}

	// Token: 0x06003E8A RID: 16010 RVA: 0x0015DD0C File Offset: 0x0015BF0C
	public SimHashes GetBreathableElementAtCell(int cell, CellOffset[] offsets = null)
	{
		if (offsets == null)
		{
			offsets = this.breathableCells;
		}
		int mouthCellAtCell = this.GetMouthCellAtCell(cell, offsets);
		if (!Grid.IsValidCell(mouthCellAtCell))
		{
			return SimHashes.Vacuum;
		}
		Element element = Grid.Element[mouthCellAtCell];
		if (!element.IsGas || !element.HasTag(GameTags.Breathable) || Grid.Mass[mouthCellAtCell] <= this.noOxygenThreshold)
		{
			return SimHashes.Vacuum;
		}
		return element.id;
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x06003E8B RID: 16011 RVA: 0x0015DD7C File Offset: 0x0015BF7C
	public bool IsUnderLiquid
	{
		get
		{
			return Grid.Element[this.mouthCell].IsLiquid;
		}
	}

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06003E8C RID: 16012 RVA: 0x0015DD8F File Offset: 0x0015BF8F
	public bool IsSuffocating
	{
		get
		{
			return !this.hasAir;
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x06003E8D RID: 16013 RVA: 0x0015DD9A File Offset: 0x0015BF9A
	public SimHashes GetBreathableElement
	{
		get
		{
			return this.GetBreathableElementAtCell(Grid.PosToCell(this), null);
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x06003E8E RID: 16014 RVA: 0x0015DDA9 File Offset: 0x0015BFA9
	public bool IsBreathableElement
	{
		get
		{
			return this.IsBreathableElementAtCell(Grid.PosToCell(this), null);
		}
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x0015DDB8 File Offset: 0x0015BFB8
	private float GetOxygenPressure(int cell)
	{
		if (Grid.IsValidCell(cell) && Grid.Element[cell].HasTag(GameTags.Breathable))
		{
			return Grid.Mass[cell];
		}
		return 0f;
	}

	// Token: 0x06003E90 RID: 16016 RVA: 0x0015DDE6 File Offset: 0x0015BFE6
	public OxygenBreather.IGasProvider GetGasProvider()
	{
		return this.gasProvider;
	}

	// Token: 0x06003E91 RID: 16017 RVA: 0x0015DDEE File Offset: 0x0015BFEE
	public void SetGasProvider(OxygenBreather.IGasProvider gas_provider)
	{
		if (this.gasProvider != null)
		{
			this.gasProvider.OnClearOxygenBreather(this);
		}
		this.gasProvider = gas_provider;
		if (this.gasProvider != null)
		{
			this.gasProvider.OnSetOxygenBreather(this);
		}
	}

	// Token: 0x040028E0 RID: 10464
	public static CellOffset[] DEFAULT_BREATHABLE_OFFSETS = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(0, 1),
		new CellOffset(1, 1),
		new CellOffset(-1, 1),
		new CellOffset(1, 0),
		new CellOffset(-1, 0)
	};

	// Token: 0x040028E1 RID: 10465
	public float O2toCO2conversion = 0.5f;

	// Token: 0x040028E2 RID: 10466
	public float lowOxygenThreshold;

	// Token: 0x040028E3 RID: 10467
	public float noOxygenThreshold;

	// Token: 0x040028E4 RID: 10468
	public Vector2 mouthOffset;

	// Token: 0x040028E5 RID: 10469
	[Serialize]
	public float accumulatedCO2;

	// Token: 0x040028E6 RID: 10470
	[SerializeField]
	public float minCO2ToEmit = 0.3f;

	// Token: 0x040028E7 RID: 10471
	private bool hasAir = true;

	// Token: 0x040028E8 RID: 10472
	private Timer hasAirTimer = new Timer();

	// Token: 0x040028E9 RID: 10473
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x040028EA RID: 10474
	[MyCmpGet]
	private Facing facing;

	// Token: 0x040028EB RID: 10475
	private HandleVector<int>.Handle o2Accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x040028EC RID: 10476
	private HandleVector<int>.Handle co2Accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x040028ED RID: 10477
	private AmountInstance temperature;

	// Token: 0x040028EE RID: 10478
	private AttributeInstance airConsumptionRate;

	// Token: 0x040028EF RID: 10479
	public CellOffset[] breathableCells;

	// Token: 0x040028F0 RID: 10480
	public Action<Sim.MassConsumedCallback> onSimConsume;

	// Token: 0x040028F1 RID: 10481
	private OxygenBreather.IGasProvider gasProvider;

	// Token: 0x040028F2 RID: 10482
	private static readonly EventSystem.IntraObjectHandler<OxygenBreather> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<OxygenBreather>(GameTags.Dead, delegate(OxygenBreather component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x0200163E RID: 5694
	public interface IGasProvider
	{
		// Token: 0x0600871E RID: 34590
		void OnSetOxygenBreather(OxygenBreather oxygen_breather);

		// Token: 0x0600871F RID: 34591
		void OnClearOxygenBreather(OxygenBreather oxygen_breather);

		// Token: 0x06008720 RID: 34592
		bool ConsumeGas(OxygenBreather oxygen_breather, float amount);

		// Token: 0x06008721 RID: 34593
		bool ShouldEmitCO2();

		// Token: 0x06008722 RID: 34594
		bool ShouldStoreCO2();
	}
}
