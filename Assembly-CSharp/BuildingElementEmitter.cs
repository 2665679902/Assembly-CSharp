using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000567 RID: 1383
[AddComponentMenu("KMonoBehaviour/scripts/BuildingElementEmitter")]
public class BuildingElementEmitter : KMonoBehaviour, IGameObjectEffectDescriptor, IElementEmitter, ISim200ms
{
	// Token: 0x17000194 RID: 404
	// (get) Token: 0x0600215B RID: 8539 RVA: 0x000B5E8C File Offset: 0x000B408C
	public float AverageEmitRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x0600215C RID: 8540 RVA: 0x000B5EA3 File Offset: 0x000B40A3
	public float EmitRate
	{
		get
		{
			return this.emitRate;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x0600215D RID: 8541 RVA: 0x000B5EAB File Offset: 0x000B40AB
	public SimHashes Element
	{
		get
		{
			return this.element;
		}
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x000B5EB3 File Offset: 0x000B40B3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.accumulator = Game.Instance.accumulators.Add("Element", this);
		base.Subscribe<BuildingElementEmitter>(824508782, BuildingElementEmitter.OnActiveChangedDelegate);
		this.SimRegister();
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x000B5EED File Offset: 0x000B40ED
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.accumulator);
		this.SimUnregister();
		base.OnCleanUp();
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x000B5F11 File Offset: 0x000B4111
	private void OnActiveChanged(object data)
	{
		this.simActive = ((Operational)data).IsActive;
		this.dirty = true;
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x000B5F2B File Offset: 0x000B412B
	public void Sim200ms(float dt)
	{
		this.UnsafeUpdate(dt);
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x000B5F34 File Offset: 0x000B4134
	private unsafe void UnsafeUpdate(float dt)
	{
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		this.UpdateSimState();
		int handleIndex = Sim.GetHandleIndex(this.simHandle);
		Sim.EmittedMassInfo emittedMassInfo = Game.Instance.simData.emittedMassEntries[handleIndex];
		if (emittedMassInfo.mass > 0f)
		{
			Game.Instance.accumulators.Accumulate(this.accumulator, emittedMassInfo.mass);
			if (this.element == SimHashes.Oxygen)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, emittedMassInfo.mass, base.gameObject.GetProperName(), null);
			}
		}
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x000B5FD4 File Offset: 0x000B41D4
	private void UpdateSimState()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		if (this.simActive)
		{
			if (this.element != (SimHashes)0 && this.emitRate > 0f)
			{
				int num = Grid.PosToCell(new Vector3(base.transform.GetPosition().x + this.modifierOffset.x, base.transform.GetPosition().y + this.modifierOffset.y, 0f));
				SimMessages.ModifyElementEmitter(this.simHandle, num, (int)this.emitRange, this.element, 0.2f, this.emitRate * 0.2f, this.temperature, float.MaxValue, this.emitDiseaseIdx, this.emitDiseaseCount);
			}
			this.statusHandle = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.EmittingElement, this);
			return;
		}
		SimMessages.ModifyElementEmitter(this.simHandle, 0, 0, SimHashes.Vacuum, 0f, 0f, 0f, 0f, byte.MaxValue, 0);
		this.statusHandle = base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, this);
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x000B610C File Offset: 0x000B430C
	private void SimRegister()
	{
		if (base.isSpawned && this.simHandle == -1)
		{
			this.simHandle = -2;
			SimMessages.AddElementEmitter(float.MaxValue, Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(BuildingElementEmitter.OnSimRegisteredCallback), this, "BuildingElementEmitter").index, -1, -1);
		}
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x000B6167 File Offset: 0x000B4367
	private void SimUnregister()
	{
		if (this.simHandle != -1)
		{
			if (Sim.IsValidHandle(this.simHandle))
			{
				SimMessages.RemoveElementEmitter(-1, this.simHandle);
			}
			this.simHandle = -1;
		}
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x000B6192 File Offset: 0x000B4392
	private static void OnSimRegisteredCallback(int handle, object data)
	{
		((BuildingElementEmitter)data).OnSimRegistered(handle);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x000B61A0 File Offset: 0x000B43A0
	private void OnSimRegistered(int handle)
	{
		if (this != null)
		{
			this.simHandle = handle;
			return;
		}
		SimMessages.RemoveElementEmitter(-1, handle);
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x000B61BC File Offset: 0x000B43BC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(this.element).tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_FIXEDTEMP, text, GameUtil.GetFormattedMass(this.EmitRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_FIXEDTEMP, text, GameUtil.GetFormattedMass(this.EmitRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x04001327 RID: 4903
	[SerializeField]
	public float emitRate = 0.3f;

	// Token: 0x04001328 RID: 4904
	[SerializeField]
	[Serialize]
	public float temperature = 293f;

	// Token: 0x04001329 RID: 4905
	[SerializeField]
	[HashedEnum]
	public SimHashes element = SimHashes.Oxygen;

	// Token: 0x0400132A RID: 4906
	[SerializeField]
	public Vector2 modifierOffset;

	// Token: 0x0400132B RID: 4907
	[SerializeField]
	public byte emitRange = 1;

	// Token: 0x0400132C RID: 4908
	[SerializeField]
	public byte emitDiseaseIdx = byte.MaxValue;

	// Token: 0x0400132D RID: 4909
	[SerializeField]
	public int emitDiseaseCount;

	// Token: 0x0400132E RID: 4910
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x0400132F RID: 4911
	private int simHandle = -1;

	// Token: 0x04001330 RID: 4912
	private bool simActive;

	// Token: 0x04001331 RID: 4913
	private bool dirty = true;

	// Token: 0x04001332 RID: 4914
	private Guid statusHandle;

	// Token: 0x04001333 RID: 4915
	private static readonly EventSystem.IntraObjectHandler<BuildingElementEmitter> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<BuildingElementEmitter>(delegate(BuildingElementEmitter component, object data)
	{
		component.OnActiveChanged(data);
	});
}
