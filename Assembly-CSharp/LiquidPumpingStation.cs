using System;
using Klei;
using UnityEngine;

// Token: 0x020005DE RID: 1502
[AddComponentMenu("KMonoBehaviour/Workable/LiquidPumpingStation")]
public class LiquidPumpingStation : Workable, ISim200ms
{
	// Token: 0x06002591 RID: 9617 RVA: 0x000CB20D File Offset: 0x000C940D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.resetProgressOnStop = true;
		this.showProgressBar = false;
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x000CB224 File Offset: 0x000C9424
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.infos = new LiquidPumpingStation.LiquidInfo[LiquidPumpingStation.liquidOffsets.Length * 2];
		this.RefreshStatusItem();
		this.Sim200ms(0f);
		base.SetWorkTime(10f);
		this.RefreshDepthAvailable();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
		foreach (GameObject gameObject in base.GetComponent<Storage>().items)
		{
			if (!(gameObject == null) && gameObject != null)
			{
				gameObject.DeleteObject();
			}
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x000CB308 File Offset: 0x000C9508
	private void RefreshDepthAvailable()
	{
		int num = PumpingStationGuide.GetDepthAvailable(Grid.PosToCell(this), base.gameObject);
		int num2 = 4;
		if (num > this.depthAvailable)
		{
			KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
			for (int i = 1; i <= num2; i++)
			{
				component.SetSymbolVisiblity("pipe" + i.ToString(), i <= num);
			}
			PumpingStationGuide.OccupyArea(base.gameObject, num);
			this.depthAvailable = num;
		}
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x000CB37C File Offset: 0x000C957C
	public void Sim200ms(float dt)
	{
		if (this.session != null)
		{
			return;
		}
		int num = this.infoCount;
		for (int i = 0; i < this.infoCount; i++)
		{
			this.infos[i].amount = 0f;
		}
		if (base.GetComponent<Operational>().IsOperational)
		{
			int num2 = Grid.PosToCell(this);
			for (int j = 0; j < LiquidPumpingStation.liquidOffsets.Length; j++)
			{
				if (this.depthAvailable >= Math.Abs(LiquidPumpingStation.liquidOffsets[j].y))
				{
					int num3 = Grid.OffsetCell(num2, LiquidPumpingStation.liquidOffsets[j]);
					bool flag = false;
					Element element = Grid.Element[num3];
					if (element.IsLiquid)
					{
						float num4 = Grid.Mass[num3];
						for (int k = 0; k < this.infoCount; k++)
						{
							if (this.infos[k].element == element)
							{
								LiquidPumpingStation.LiquidInfo[] array = this.infos;
								int num5 = k;
								array[num5].amount = array[num5].amount + num4;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this.infos[this.infoCount].amount = num4;
							this.infos[this.infoCount].element = element;
							this.infoCount++;
						}
					}
				}
			}
		}
		int l = 0;
		while (l < this.infoCount)
		{
			LiquidPumpingStation.LiquidInfo liquidInfo = this.infos[l];
			if (liquidInfo.amount <= 1f)
			{
				if (liquidInfo.source != null)
				{
					liquidInfo.source.DeleteObject();
				}
				this.infos[l] = this.infos[this.infoCount - 1];
				this.infoCount--;
			}
			else
			{
				if (liquidInfo.source == null)
				{
					liquidInfo.source = base.GetComponent<Storage>().AddLiquid(liquidInfo.element.id, liquidInfo.amount, liquidInfo.element.defaultValues.temperature, byte.MaxValue, 0, false, true).GetComponent<SubstanceChunk>();
					Pickupable component = liquidInfo.source.GetComponent<Pickupable>();
					component.GetComponent<KPrefabID>().AddTag(GameTags.LiquidSource, false);
					component.SetOffsets(new CellOffset[]
					{
						new CellOffset(0, 1)
					});
					component.targetWorkable = this;
					Pickupable pickupable = component;
					pickupable.OnReservationsChanged = (System.Action)Delegate.Combine(pickupable.OnReservationsChanged, new System.Action(this.OnReservationsChanged));
				}
				liquidInfo.source.GetComponent<Pickupable>().TotalAmount = liquidInfo.amount;
				this.infos[l] = liquidInfo;
				l++;
			}
		}
		if (num != this.infoCount)
		{
			this.RefreshStatusItem();
		}
		this.RefreshDepthAvailable();
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x000CB650 File Offset: 0x000C9850
	private void RefreshStatusItem()
	{
		if (this.infoCount > 0)
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.PumpingStation, this);
			return;
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmptyPumpingStation, this);
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x000CB6C0 File Offset: 0x000C98C0
	public string ResolveString(string base_string)
	{
		string text = "";
		for (int i = 0; i < this.infoCount; i++)
		{
			if (this.infos[i].source != null)
			{
				text = string.Concat(new string[]
				{
					text,
					"\n",
					this.infos[i].element.name,
					": ",
					GameUtil.GetFormattedMass(this.infos[i].amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
				});
			}
		}
		return base_string.Replace("{Liquids}", text);
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x000CB763 File Offset: 0x000C9963
	public static bool IsLiquidAccessible(Element element)
	{
		return true;
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x000CB766 File Offset: 0x000C9966
	public override float GetPercentComplete()
	{
		if (this.session != null)
		{
			return this.session.GetPercentComplete();
		}
		return 0f;
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x000CB784 File Offset: 0x000C9984
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		Pickupable.PickupableStartWorkInfo pickupableStartWorkInfo = (Pickupable.PickupableStartWorkInfo)worker.startWorkInfo;
		float amount = pickupableStartWorkInfo.amount;
		Element element = pickupableStartWorkInfo.originalPickupable.GetComponent<PrimaryElement>().Element;
		this.session = new LiquidPumpingStation.WorkSession(Grid.PosToCell(this), element.id, pickupableStartWorkInfo.originalPickupable.GetComponent<SubstanceChunk>(), amount, base.gameObject);
		this.meter.SetPositionPercent(0f);
		this.meter.SetSymbolTint(new KAnimHashedString("meter_target"), element.substance.colour);
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x000CB818 File Offset: 0x000C9A18
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		if (this.session != null)
		{
			this.session.Cleanup();
			this.session = null;
		}
		base.GetComponent<KAnimControllerBase>().Play("on", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x000CB868 File Offset: 0x000C9A68
	private void OnReservationsChanged()
	{
		bool flag = false;
		for (int i = 0; i < this.infoCount; i++)
		{
			if (this.infos[i].source != null && this.infos[i].source.GetComponent<Pickupable>().ReservedAmount > 0f)
			{
				flag = true;
				break;
			}
		}
		for (int j = 0; j < this.infoCount; j++)
		{
			if (this.infos[j].source != null)
			{
				FetchableMonitor.Instance smi = this.infos[j].source.GetSMI<FetchableMonitor.Instance>();
				if (smi != null)
				{
					smi.SetForceUnfetchable(flag);
				}
			}
		}
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x000CB914 File Offset: 0x000C9B14
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		if (this.session != null)
		{
			Storage component = worker.GetComponent<Storage>();
			float consumedAmount = this.session.GetConsumedAmount();
			if (consumedAmount > 0f)
			{
				SubstanceChunk source = this.session.GetSource();
				SimUtil.DiseaseInfo diseaseInfo = ((this.session != null) ? this.session.GetDiseaseInfo() : SimUtil.DiseaseInfo.Invalid);
				PrimaryElement component2 = source.GetComponent<PrimaryElement>();
				Pickupable component3 = LiquidSourceManager.Instance.CreateChunk(component2.Element, consumedAmount, this.session.GetTemperature(), diseaseInfo.idx, diseaseInfo.count, base.transform.GetPosition()).GetComponent<Pickupable>();
				component3.TotalAmount = consumedAmount;
				component3.Trigger(1335436905, source.GetComponent<Pickupable>());
				worker.workCompleteData = component3;
				this.Sim200ms(0f);
				if (component3 != null)
				{
					component.Store(component3.gameObject, false, false, true, false);
				}
			}
			this.session.Cleanup();
			this.session = null;
		}
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x000CBA16 File Offset: 0x000C9C16
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.session != null)
		{
			this.meter.SetPositionPercent(this.session.GetPercentComplete());
			if (this.session.GetLastTickAmount() <= 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600259E RID: 9630 RVA: 0x000CBA4C File Offset: 0x000C9C4C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.session != null)
		{
			this.session.Cleanup();
			this.session = null;
		}
		for (int i = 0; i < this.infoCount; i++)
		{
			if (this.infos[i].source != null)
			{
				this.infos[i].source.DeleteObject();
			}
		}
	}

	// Token: 0x040015EA RID: 5610
	private static readonly CellOffset[] liquidOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0),
		new CellOffset(0, -1),
		new CellOffset(1, -1),
		new CellOffset(0, -2),
		new CellOffset(1, -2),
		new CellOffset(0, -3),
		new CellOffset(1, -3),
		new CellOffset(0, -4),
		new CellOffset(1, -4)
	};

	// Token: 0x040015EB RID: 5611
	private LiquidPumpingStation.LiquidInfo[] infos;

	// Token: 0x040015EC RID: 5612
	private int infoCount;

	// Token: 0x040015ED RID: 5613
	private int depthAvailable = -1;

	// Token: 0x040015EE RID: 5614
	private LiquidPumpingStation.WorkSession session;

	// Token: 0x040015EF RID: 5615
	private MeterController meter;

	// Token: 0x0200123E RID: 4670
	private class WorkSession
	{
		// Token: 0x0600797D RID: 31101 RVA: 0x002C3F98 File Offset: 0x002C2198
		public WorkSession(int cell, SimHashes element, SubstanceChunk source, float amount_to_pickup, GameObject pump)
		{
			this.cell = cell;
			this.element = element;
			this.source = source;
			this.amountToPickup = amount_to_pickup;
			this.temperature = ElementLoader.FindElementByHash(element).defaultValues.temperature;
			this.diseaseInfo = SimUtil.DiseaseInfo.Invalid;
			this.amountPerTick = 40f;
			this.pump = pump;
			this.lastTickAmount = this.amountPerTick;
			this.ConsumeMass();
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x002C400E File Offset: 0x002C220E
		private void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
		{
			((LiquidPumpingStation.WorkSession)data).OnSimConsume(mass_cb_info);
		}

		// Token: 0x0600797F RID: 31103 RVA: 0x002C401C File Offset: 0x002C221C
		private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
		{
			if (this.consumedAmount == 0f)
			{
				this.temperature = mass_cb_info.temperature;
			}
			else
			{
				this.temperature = GameUtil.GetFinalTemperature(this.temperature, this.consumedAmount, mass_cb_info.temperature, mass_cb_info.mass);
			}
			this.consumedAmount += mass_cb_info.mass;
			this.lastTickAmount = mass_cb_info.mass;
			this.diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(this.diseaseInfo.idx, this.diseaseInfo.count, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount);
			if (this.consumedAmount >= this.amountToPickup)
			{
				this.amountPerTick = 0f;
				this.lastTickAmount = 0f;
			}
			this.ConsumeMass();
		}

		// Token: 0x06007980 RID: 31104 RVA: 0x002C40E0 File Offset: 0x002C22E0
		private void ConsumeMass()
		{
			if (this.amountPerTick > 0f)
			{
				float num = Mathf.Min(this.amountPerTick, this.amountToPickup - this.consumedAmount);
				num = Mathf.Max(num, 1f);
				HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(this.OnSimConsumeCallback), this, "LiquidPumpingStation");
				int depthAvailable = PumpingStationGuide.GetDepthAvailable(this.cell, this.pump);
				SimMessages.ConsumeMass(Grid.OffsetCell(this.cell, new CellOffset(0, -depthAvailable)), this.element, num, (byte)(depthAvailable + 1), handle.index);
			}
		}

		// Token: 0x06007981 RID: 31105 RVA: 0x002C4180 File Offset: 0x002C2380
		public float GetPercentComplete()
		{
			return this.consumedAmount / this.amountToPickup;
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x002C418F File Offset: 0x002C238F
		public float GetLastTickAmount()
		{
			return this.lastTickAmount;
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x002C4197 File Offset: 0x002C2397
		public SimUtil.DiseaseInfo GetDiseaseInfo()
		{
			return this.diseaseInfo;
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x002C419F File Offset: 0x002C239F
		public SubstanceChunk GetSource()
		{
			return this.source;
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x002C41A7 File Offset: 0x002C23A7
		public float GetConsumedAmount()
		{
			return this.consumedAmount;
		}

		// Token: 0x06007986 RID: 31110 RVA: 0x002C41AF File Offset: 0x002C23AF
		public float GetTemperature()
		{
			if (this.temperature <= 0f)
			{
				global::Debug.LogWarning("TODO(YOG): Fix bad temperature in liquid pumping station.");
				return ElementLoader.FindElementByHash(this.element).defaultValues.temperature;
			}
			return this.temperature;
		}

		// Token: 0x06007987 RID: 31111 RVA: 0x002C41E4 File Offset: 0x002C23E4
		public void Cleanup()
		{
			this.amountPerTick = 0f;
			this.diseaseInfo = SimUtil.DiseaseInfo.Invalid;
		}

		// Token: 0x04005D5A RID: 23898
		private int cell;

		// Token: 0x04005D5B RID: 23899
		private float amountToPickup;

		// Token: 0x04005D5C RID: 23900
		private float consumedAmount;

		// Token: 0x04005D5D RID: 23901
		private float temperature;

		// Token: 0x04005D5E RID: 23902
		private float amountPerTick;

		// Token: 0x04005D5F RID: 23903
		private SimHashes element;

		// Token: 0x04005D60 RID: 23904
		private float lastTickAmount;

		// Token: 0x04005D61 RID: 23905
		private SubstanceChunk source;

		// Token: 0x04005D62 RID: 23906
		private SimUtil.DiseaseInfo diseaseInfo;

		// Token: 0x04005D63 RID: 23907
		private GameObject pump;
	}

	// Token: 0x0200123F RID: 4671
	private struct LiquidInfo
	{
		// Token: 0x04005D64 RID: 23908
		public float amount;

		// Token: 0x04005D65 RID: 23909
		public Element element;

		// Token: 0x04005D66 RID: 23910
		public SubstanceChunk source;
	}
}
