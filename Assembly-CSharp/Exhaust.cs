using System;
using UnityEngine;

// Token: 0x02000768 RID: 1896
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Exhaust")]
public class Exhaust : KMonoBehaviour, ISim200ms
{
	// Token: 0x06003409 RID: 13321 RVA: 0x001180DA File Offset: 0x001162DA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Exhaust>(-592767678, Exhaust.OnConduitStateChangedDelegate);
		base.Subscribe<Exhaust>(-111137758, Exhaust.OnConduitStateChangedDelegate);
		base.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.NoWire;
		this.simRenderLoadBalance = true;
	}

	// Token: 0x0600340A RID: 13322 RVA: 0x00118117 File Offset: 0x00116317
	protected override void OnSpawn()
	{
		this.OnConduitStateChanged(null);
	}

	// Token: 0x0600340B RID: 13323 RVA: 0x00118120 File Offset: 0x00116320
	private void OnConduitStateChanged(object data)
	{
		this.operational.SetActive(this.operational.IsOperational && !this.vent.IsBlocked, false);
	}

	// Token: 0x0600340C RID: 13324 RVA: 0x0011814C File Offset: 0x0011634C
	private void CalculateDiseaseTransfer(PrimaryElement item1, PrimaryElement item2, float transfer_rate, out int disease_to_item1, out int disease_to_item2)
	{
		disease_to_item1 = (int)((float)item2.DiseaseCount * transfer_rate);
		disease_to_item2 = (int)((float)item1.DiseaseCount * transfer_rate);
	}

	// Token: 0x0600340D RID: 13325 RVA: 0x00118168 File Offset: 0x00116368
	public void Sim200ms(float dt)
	{
		this.operational.SetFlag(Exhaust.canExhaust, !this.vent.IsBlocked);
		if (!this.operational.IsOperational)
		{
			if (this.isAnimating)
			{
				this.isAnimating = false;
				this.recentlyExhausted = false;
				base.Trigger(-793429877, null);
			}
			return;
		}
		this.UpdateEmission();
		this.elapsedSwitchTime -= dt;
		if (this.elapsedSwitchTime <= 0f)
		{
			this.elapsedSwitchTime = 1f;
			if (this.recentlyExhausted != this.isAnimating)
			{
				this.isAnimating = this.recentlyExhausted;
				base.Trigger(-793429877, null);
			}
			this.recentlyExhausted = false;
		}
	}

	// Token: 0x0600340E RID: 13326 RVA: 0x0011821C File Offset: 0x0011641C
	public bool IsAnimating()
	{
		return this.isAnimating;
	}

	// Token: 0x0600340F RID: 13327 RVA: 0x00118224 File Offset: 0x00116424
	private void UpdateEmission()
	{
		if (this.consumer.ConsumptionRate == 0f)
		{
			return;
		}
		if (this.storage.items.Count == 0)
		{
			return;
		}
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (Grid.Solid[num])
		{
			return;
		}
		ConduitType typeOfConduit = this.consumer.TypeOfConduit;
		if (typeOfConduit != ConduitType.Gas)
		{
			if (typeOfConduit == ConduitType.Liquid)
			{
				this.EmitLiquid(num);
				return;
			}
		}
		else
		{
			this.EmitGas(num);
		}
	}

	// Token: 0x06003410 RID: 13328 RVA: 0x0011829C File Offset: 0x0011649C
	private bool EmitCommon(int cell, PrimaryElement primary_element, Exhaust.EmitDelegate emit)
	{
		if (primary_element.Mass <= 0f)
		{
			return false;
		}
		int num;
		int num2;
		this.CalculateDiseaseTransfer(this.exhaustPE, primary_element, 0.05f, out num, out num2);
		primary_element.ModifyDiseaseCount(-num, "Exhaust transfer");
		primary_element.AddDisease(this.exhaustPE.DiseaseIdx, num2, "Exhaust transfer");
		this.exhaustPE.ModifyDiseaseCount(-num2, "Exhaust transfer");
		this.exhaustPE.AddDisease(primary_element.DiseaseIdx, num, "Exhaust transfer");
		emit(cell, primary_element);
		if (this.vent != null)
		{
			this.vent.UpdateVentedMass(primary_element.ElementID, primary_element.Mass);
		}
		primary_element.KeepZeroMassObject = true;
		primary_element.Mass = 0f;
		primary_element.ModifyDiseaseCount(int.MinValue, "Exhaust.SimUpdate");
		this.recentlyExhausted = true;
		return true;
	}

	// Token: 0x06003411 RID: 13329 RVA: 0x00118374 File Offset: 0x00116574
	private void EmitLiquid(int cell)
	{
		int num = Grid.CellBelow(cell);
		Exhaust.EmitDelegate emitDelegate = ((Grid.IsValidCell(num) && !Grid.Solid[num]) ? Exhaust.emit_particle : Exhaust.emit_element);
		foreach (GameObject gameObject in this.storage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (component.Element.IsLiquid && this.EmitCommon(cell, component, emitDelegate))
			{
				break;
			}
		}
	}

	// Token: 0x06003412 RID: 13330 RVA: 0x00118410 File Offset: 0x00116610
	private void EmitGas(int cell)
	{
		foreach (GameObject gameObject in this.storage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (component.Element.IsGas && this.EmitCommon(cell, component, Exhaust.emit_element))
			{
				break;
			}
		}
	}

	// Token: 0x04002024 RID: 8228
	[MyCmpGet]
	private Vent vent;

	// Token: 0x04002025 RID: 8229
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04002026 RID: 8230
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002027 RID: 8231
	[MyCmpGet]
	private ConduitConsumer consumer;

	// Token: 0x04002028 RID: 8232
	[MyCmpGet]
	private PrimaryElement exhaustPE;

	// Token: 0x04002029 RID: 8233
	private static readonly Operational.Flag canExhaust = new Operational.Flag("canExhaust", Operational.Flag.Type.Requirement);

	// Token: 0x0400202A RID: 8234
	private bool isAnimating;

	// Token: 0x0400202B RID: 8235
	private bool recentlyExhausted;

	// Token: 0x0400202C RID: 8236
	private const float MinSwitchTime = 1f;

	// Token: 0x0400202D RID: 8237
	private float elapsedSwitchTime;

	// Token: 0x0400202E RID: 8238
	private static readonly EventSystem.IntraObjectHandler<Exhaust> OnConduitStateChangedDelegate = new EventSystem.IntraObjectHandler<Exhaust>(delegate(Exhaust component, object data)
	{
		component.OnConduitStateChanged(data);
	});

	// Token: 0x0400202F RID: 8239
	private static Exhaust.EmitDelegate emit_element = delegate(int cell, PrimaryElement primary_element)
	{
		SimMessages.AddRemoveSubstance(cell, primary_element.ElementID, CellEventLogger.Instance.ExhaustSimUpdate, primary_element.Mass, primary_element.Temperature, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, -1);
	};

	// Token: 0x04002030 RID: 8240
	private static Exhaust.EmitDelegate emit_particle = delegate(int cell, PrimaryElement primary_element)
	{
		FallingWater.instance.AddParticle(cell, primary_element.Element.idx, primary_element.Mass, primary_element.Temperature, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, false, true, false);
	};

	// Token: 0x0200145C RID: 5212
	// (Invoke) Token: 0x060080E3 RID: 32995
	private delegate void EmitDelegate(int cell, PrimaryElement primary_element);
}
