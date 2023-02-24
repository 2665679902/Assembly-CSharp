using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000998 RID: 2456
public struct StructureTemperaturePayload
{
	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x060048B9 RID: 18617 RVA: 0x001975A9 File Offset: 0x001957A9
	// (set) Token: 0x060048BA RID: 18618 RVA: 0x001975B1 File Offset: 0x001957B1
	public PrimaryElement primaryElement
	{
		get
		{
			return this.primaryElementBacking;
		}
		set
		{
			if (this.primaryElementBacking != value)
			{
				this.primaryElementBacking = value;
				this.overheatable = this.primaryElementBacking.GetComponent<Overheatable>();
			}
		}
	}

	// Token: 0x060048BB RID: 18619 RVA: 0x001975DC File Offset: 0x001957DC
	public StructureTemperaturePayload(GameObject go)
	{
		this.simHandleCopy = -1;
		this.enabled = true;
		this.bypass = false;
		this.overrideExtents = false;
		this.overriddenExtents = default(Extents);
		this.primaryElementBacking = go.GetComponent<PrimaryElement>();
		this.overheatable = ((this.primaryElementBacking != null) ? this.primaryElementBacking.GetComponent<Overheatable>() : null);
		this.building = go.GetComponent<Building>();
		this.operational = go.GetComponent<Operational>();
		this.pendingEnergyModifications = 0f;
		this.maxTemperature = 10000f;
		this.energySourcesKW = null;
		this.isActiveStatusItemSet = false;
	}

	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x060048BC RID: 18620 RVA: 0x0019767C File Offset: 0x0019587C
	public float TotalEnergyProducedKW
	{
		get
		{
			if (this.energySourcesKW == null || this.energySourcesKW.Count == 0)
			{
				return 0f;
			}
			float num = 0f;
			for (int i = 0; i < this.energySourcesKW.Count; i++)
			{
				num += this.energySourcesKW[i].value;
			}
			return num;
		}
	}

	// Token: 0x060048BD RID: 18621 RVA: 0x001976D5 File Offset: 0x001958D5
	public void OverrideExtents(Extents newExtents)
	{
		this.overrideExtents = true;
		this.overriddenExtents = newExtents;
	}

	// Token: 0x060048BE RID: 18622 RVA: 0x001976E5 File Offset: 0x001958E5
	public Extents GetExtents()
	{
		if (!this.overrideExtents)
		{
			return this.building.GetExtents();
		}
		return this.overriddenExtents;
	}

	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x060048BF RID: 18623 RVA: 0x00197701 File Offset: 0x00195901
	public float Temperature
	{
		get
		{
			return this.primaryElement.Temperature;
		}
	}

	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x060048C0 RID: 18624 RVA: 0x0019770E File Offset: 0x0019590E
	public float ExhaustKilowatts
	{
		get
		{
			return this.building.Def.ExhaustKilowattsWhenActive;
		}
	}

	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x060048C1 RID: 18625 RVA: 0x00197720 File Offset: 0x00195920
	public float OperatingKilowatts
	{
		get
		{
			if (!(this.operational != null) || !this.operational.IsActive)
			{
				return 0f;
			}
			return this.building.Def.SelfHeatKilowattsWhenActive;
		}
	}

	// Token: 0x04002FD5 RID: 12245
	public int simHandleCopy;

	// Token: 0x04002FD6 RID: 12246
	public bool enabled;

	// Token: 0x04002FD7 RID: 12247
	public bool bypass;

	// Token: 0x04002FD8 RID: 12248
	public bool isActiveStatusItemSet;

	// Token: 0x04002FD9 RID: 12249
	public bool overrideExtents;

	// Token: 0x04002FDA RID: 12250
	private PrimaryElement primaryElementBacking;

	// Token: 0x04002FDB RID: 12251
	public Overheatable overheatable;

	// Token: 0x04002FDC RID: 12252
	public Building building;

	// Token: 0x04002FDD RID: 12253
	public Operational operational;

	// Token: 0x04002FDE RID: 12254
	public List<StructureTemperaturePayload.EnergySource> energySourcesKW;

	// Token: 0x04002FDF RID: 12255
	public float pendingEnergyModifications;

	// Token: 0x04002FE0 RID: 12256
	public float maxTemperature;

	// Token: 0x04002FE1 RID: 12257
	public Extents overriddenExtents;

	// Token: 0x02001793 RID: 6035
	public class EnergySource
	{
		// Token: 0x06008B46 RID: 35654 RVA: 0x002FF315 File Offset: 0x002FD515
		public EnergySource(float kj, string source)
		{
			this.source = source;
			this.kw_accumulator = new RunningAverage(float.MinValue, float.MaxValue, Mathf.RoundToInt(186f), true);
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06008B47 RID: 35655 RVA: 0x002FF344 File Offset: 0x002FD544
		public float value
		{
			get
			{
				return this.kw_accumulator.AverageValue;
			}
		}

		// Token: 0x06008B48 RID: 35656 RVA: 0x002FF351 File Offset: 0x002FD551
		public void Accumulate(float value)
		{
			this.kw_accumulator.AddSample(value);
		}

		// Token: 0x04006D7B RID: 28027
		public string source;

		// Token: 0x04006D7C RID: 28028
		public RunningAverage kw_accumulator;
	}
}
