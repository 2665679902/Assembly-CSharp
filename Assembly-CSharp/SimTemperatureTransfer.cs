using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200091F RID: 2335
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SimTemperatureTransfer")]
public class SimTemperatureTransfer : KMonoBehaviour
{
	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x0600442A RID: 17450 RVA: 0x00181083 File Offset: 0x0017F283
	// (set) Token: 0x0600442B RID: 17451 RVA: 0x0018108B File Offset: 0x0017F28B
	public float SurfaceArea
	{
		get
		{
			return this.surfaceArea;
		}
		set
		{
			this.surfaceArea = value;
		}
	}

	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x0600442C RID: 17452 RVA: 0x00181094 File Offset: 0x0017F294
	// (set) Token: 0x0600442D RID: 17453 RVA: 0x0018109C File Offset: 0x0017F29C
	public float Thickness
	{
		get
		{
			return this.thickness;
		}
		set
		{
			this.thickness = value;
		}
	}

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x0600442E RID: 17454 RVA: 0x001810A5 File Offset: 0x0017F2A5
	// (set) Token: 0x0600442F RID: 17455 RVA: 0x001810AD File Offset: 0x0017F2AD
	public float GroundTransferScale
	{
		get
		{
			return this.GroundTransferScale;
		}
		set
		{
			this.groundTransferScale = value;
		}
	}

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x06004430 RID: 17456 RVA: 0x001810B6 File Offset: 0x0017F2B6
	public int SimHandle
	{
		get
		{
			return this.simHandle;
		}
	}

	// Token: 0x06004431 RID: 17457 RVA: 0x001810BE File Offset: 0x0017F2BE
	public static void ClearInstanceMap()
	{
		SimTemperatureTransfer.handleInstanceMap.Clear();
	}

	// Token: 0x06004432 RID: 17458 RVA: 0x001810CC File Offset: 0x0017F2CC
	public static void DoOreMeltTransition(int sim_handle)
	{
		SimTemperatureTransfer simTemperatureTransfer = null;
		if (!SimTemperatureTransfer.handleInstanceMap.TryGetValue(sim_handle, out simTemperatureTransfer))
		{
			return;
		}
		if (simTemperatureTransfer == null)
		{
			return;
		}
		if (simTemperatureTransfer.HasTag(GameTags.Sealed))
		{
			return;
		}
		PrimaryElement component = simTemperatureTransfer.GetComponent<PrimaryElement>();
		Element element = component.Element;
		bool flag = component.Temperature >= element.highTemp;
		bool flag2 = component.Temperature <= element.lowTemp;
		DebugUtil.DevAssert(flag || flag2, "An ore got a melt message from the sim but it's still the correct temperature for its state!", component);
		if (flag && element.highTempTransitionTarget == SimHashes.Unobtanium)
		{
			return;
		}
		if (flag2 && element.lowTempTransitionTarget == SimHashes.Unobtanium)
		{
			return;
		}
		if (component.Mass > 0f)
		{
			int num = Grid.PosToCell(simTemperatureTransfer.transform.GetPosition());
			float num2 = component.Mass;
			int num3 = component.DiseaseCount;
			SimHashes simHashes = (flag ? element.highTempTransitionTarget : element.lowTempTransitionTarget);
			SimHashes simHashes2 = (flag ? element.highTempTransitionOreID : element.lowTempTransitionOreID);
			float num4 = (flag ? element.highTempTransitionOreMassConversion : element.lowTempTransitionOreMassConversion);
			if (simHashes2 != (SimHashes)0)
			{
				float num5 = num2 * num4;
				int num6 = (int)((float)num3 * num4);
				if (num5 > 0.001f)
				{
					num2 -= num5;
					num3 -= num6;
					Element element2 = ElementLoader.FindElementByHash(simHashes2);
					if (element2.IsSolid)
					{
						GameObject gameObject = element2.substance.SpawnResource(simTemperatureTransfer.transform.GetPosition(), num5, component.Temperature, component.DiseaseIdx, num6, true, false, true);
						element2.substance.ActivateSubstanceGameObject(gameObject, component.DiseaseIdx, num6);
					}
					else
					{
						SimMessages.AddRemoveSubstance(num, element2.id, CellEventLogger.Instance.OreMelted, num5, component.Temperature, component.DiseaseIdx, num6, true, -1);
					}
				}
			}
			SimMessages.AddRemoveSubstance(num, simHashes, CellEventLogger.Instance.OreMelted, num2, component.Temperature, component.DiseaseIdx, num3, true, -1);
		}
		simTemperatureTransfer.OnCleanUp();
		Util.KDestroyGameObject(simTemperatureTransfer.gameObject);
	}

	// Token: 0x06004433 RID: 17459 RVA: 0x001812BC File Offset: 0x0017F4BC
	protected override void OnPrefabInit()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(SimTemperatureTransfer.OnGetTemperature);
		component.setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(SimTemperatureTransfer.OnSetTemperature);
		component.onDataChanged = (Action<PrimaryElement>)Delegate.Combine(component.onDataChanged, new Action<PrimaryElement>(this.OnDataChanged));
	}

	// Token: 0x06004434 RID: 17460 RVA: 0x00181314 File Offset: 0x0017F514
	protected override void OnSpawn()
	{
		base.OnSpawn();
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Element element = component.Element;
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged), "SimTemperatureTransfer.OnSpawn");
		if (!Grid.IsValidCell(Grid.PosToCell(this)) || component.Element.HasTag(GameTags.Special) || element.specificHeatCapacity == 0f)
		{
			base.enabled = false;
		}
		this.SimRegister();
	}

	// Token: 0x06004435 RID: 17461 RVA: 0x00181390 File Offset: 0x0017F590
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.SimRegister();
		if (Sim.IsValidHandle(this.simHandle))
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			SimTemperatureTransfer.OnSetTemperature(component, component.Temperature);
		}
	}

	// Token: 0x06004436 RID: 17462 RVA: 0x001813BC File Offset: 0x0017F5BC
	protected override void OnCmpDisable()
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			float temperature = component.Temperature;
			component.InternalTemperature = component.Temperature;
			SimMessages.SetElementChunkData(this.simHandle, temperature, 0f);
		}
		base.OnCmpDisable();
	}

	// Token: 0x06004437 RID: 17463 RVA: 0x00181408 File Offset: 0x0017F608
	private void OnCellChanged()
	{
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num))
		{
			base.enabled = false;
			return;
		}
		this.SimRegister();
		if (Sim.IsValidHandle(this.simHandle))
		{
			SimMessages.MoveElementChunk(this.simHandle, num);
		}
	}

	// Token: 0x06004438 RID: 17464 RVA: 0x0018144B File Offset: 0x0017F64B
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged));
		this.SimUnregister();
		base.OnForcedCleanUp();
	}

	// Token: 0x06004439 RID: 17465 RVA: 0x00181475 File Offset: 0x0017F675
	public void ModifyEnergy(float delta_kilojoules)
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			SimMessages.ModifyElementChunkEnergy(this.simHandle, delta_kilojoules);
			return;
		}
		this.pendingEnergyModifications += delta_kilojoules;
	}

	// Token: 0x0600443A RID: 17466 RVA: 0x001814A0 File Offset: 0x0017F6A0
	private unsafe static float OnGetTemperature(PrimaryElement primary_element)
	{
		SimTemperatureTransfer component = primary_element.GetComponent<SimTemperatureTransfer>();
		float num;
		if (Sim.IsValidHandle(component.simHandle))
		{
			int handleIndex = Sim.GetHandleIndex(component.simHandle);
			num = Game.Instance.simData.elementChunks[handleIndex].temperature;
			component.deltaKJ = Game.Instance.simData.elementChunks[handleIndex].deltaKJ;
		}
		else
		{
			num = primary_element.InternalTemperature;
		}
		return num;
	}

	// Token: 0x0600443B RID: 17467 RVA: 0x0018151C File Offset: 0x0017F71C
	private unsafe static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		if (temperature <= 0f)
		{
			KCrashReporter.Assert(false, "STT.OnSetTemperature - Tried to set <= 0 degree temperature");
			temperature = 293f;
		}
		SimTemperatureTransfer component = primary_element.GetComponent<SimTemperatureTransfer>();
		if (Sim.IsValidHandle(component.simHandle))
		{
			float mass = primary_element.Mass;
			float num = ((mass >= 0.01f) ? (mass * primary_element.Element.specificHeatCapacity) : 0f);
			SimMessages.SetElementChunkData(component.simHandle, temperature, num);
			int handleIndex = Sim.GetHandleIndex(component.simHandle);
			Game.Instance.simData.elementChunks[handleIndex].temperature = temperature;
			return;
		}
		primary_element.InternalTemperature = temperature;
	}

	// Token: 0x0600443C RID: 17468 RVA: 0x001815BC File Offset: 0x0017F7BC
	private void OnDataChanged(PrimaryElement primary_element)
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			float num = ((primary_element.Mass >= 0.01f) ? (primary_element.Mass * primary_element.Element.specificHeatCapacity) : 0f);
			SimMessages.SetElementChunkData(this.simHandle, primary_element.Temperature, num);
		}
	}

	// Token: 0x0600443D RID: 17469 RVA: 0x00181610 File Offset: 0x0017F810
	protected void SimRegister()
	{
		if (base.isSpawned && this.simHandle == -1 && base.enabled)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (component.Mass > 0f && !component.Element.IsTemperatureInsulated)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				this.simHandle = -2;
				HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle = Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(SimTemperatureTransfer.OnSimRegisteredCallback), this, "SimTemperatureTransfer.SimRegister");
				float num2 = component.InternalTemperature;
				if (num2 <= 0f)
				{
					component.InternalTemperature = 293f;
					num2 = 293f;
				}
				SimMessages.AddElementChunk(num, component.ElementID, component.Mass, num2, this.surfaceArea, this.thickness, this.groundTransferScale, handle.index);
			}
		}
	}

	// Token: 0x0600443E RID: 17470 RVA: 0x001816EC File Offset: 0x0017F8EC
	protected unsafe void SimUnregister()
	{
		if (this.simHandle != -1 && !KMonoBehaviour.isLoadingScene)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (Sim.IsValidHandle(this.simHandle))
			{
				int handleIndex = Sim.GetHandleIndex(this.simHandle);
				component.InternalTemperature = Game.Instance.simData.elementChunks[handleIndex].temperature;
				SimMessages.RemoveElementChunk(this.simHandle, -1);
				SimTemperatureTransfer.handleInstanceMap.Remove(this.simHandle);
			}
			this.simHandle = -1;
		}
	}

	// Token: 0x0600443F RID: 17471 RVA: 0x00181771 File Offset: 0x0017F971
	private static void OnSimRegisteredCallback(int handle, object data)
	{
		((SimTemperatureTransfer)data).OnSimRegistered(handle);
	}

	// Token: 0x06004440 RID: 17472 RVA: 0x00181780 File Offset: 0x0017F980
	private unsafe void OnSimRegistered(int handle)
	{
		if (this != null && this.simHandle == -2)
		{
			this.simHandle = handle;
			int handleIndex = Sim.GetHandleIndex(handle);
			if (Game.Instance.simData.elementChunks[handleIndex].temperature <= 0f)
			{
				KCrashReporter.Assert(false, "Bad temperature");
			}
			SimTemperatureTransfer.handleInstanceMap[this.simHandle] = this;
			if (this.pendingEnergyModifications > 0f)
			{
				this.ModifyEnergy(this.pendingEnergyModifications);
				this.pendingEnergyModifications = 0f;
			}
			if (this.onSimRegistered != null)
			{
				this.onSimRegistered(this);
			}
			if (!base.enabled)
			{
				this.OnCmpDisable();
				return;
			}
		}
		else
		{
			SimMessages.RemoveElementChunk(handle, -1);
		}
	}

	// Token: 0x04002D7B RID: 11643
	private const float SIM_FREEZE_SPAWN_ORE_PERCENT = 0.8f;

	// Token: 0x04002D7C RID: 11644
	public const float MIN_MASS_FOR_TEMPERATURE_TRANSFER = 0.01f;

	// Token: 0x04002D7D RID: 11645
	public float deltaKJ;

	// Token: 0x04002D7E RID: 11646
	public Action<SimTemperatureTransfer> onSimRegistered;

	// Token: 0x04002D7F RID: 11647
	protected int simHandle = -1;

	// Token: 0x04002D80 RID: 11648
	private float pendingEnergyModifications;

	// Token: 0x04002D81 RID: 11649
	[SerializeField]
	protected float surfaceArea = 10f;

	// Token: 0x04002D82 RID: 11650
	[SerializeField]
	protected float thickness = 0.01f;

	// Token: 0x04002D83 RID: 11651
	[SerializeField]
	protected float groundTransferScale = 0.0625f;

	// Token: 0x04002D84 RID: 11652
	private static Dictionary<int, SimTemperatureTransfer> handleInstanceMap = new Dictionary<int, SimTemperatureTransfer>();
}
