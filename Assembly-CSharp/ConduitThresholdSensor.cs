using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200059A RID: 1434
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class ConduitThresholdSensor : ConduitSensor
{
	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06002330 RID: 9008
	public abstract float CurrentValue { get; }

	// Token: 0x06002331 RID: 9009 RVA: 0x000BEA33 File Offset: 0x000BCC33
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<ConduitThresholdSensor>(-905833192, ConduitThresholdSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x000BEA4C File Offset: 0x000BCC4C
	private void OnCopySettings(object data)
	{
		ConduitThresholdSensor component = ((GameObject)data).GetComponent<ConduitThresholdSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x000BEA88 File Offset: 0x000BCC88
	protected override void ConduitUpdate(float dt)
	{
		if (this.GetContainedMass() <= 0f && !this.dirty)
		{
			return;
		}
		float currentValue = this.CurrentValue;
		this.dirty = false;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x000BEB14 File Offset: 0x000BCD14
	private float GetContainedMass()
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			return Conduit.GetFlowManager(this.conduitType).GetContents(num).mass;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents.pickupableHandle);
		if (pickupable != null)
		{
			return pickupable.PrimaryElement.Mass;
		}
		return 0f;
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06002335 RID: 9013 RVA: 0x000BEB87 File Offset: 0x000BCD87
	// (set) Token: 0x06002336 RID: 9014 RVA: 0x000BEB8F File Offset: 0x000BCD8F
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
			this.dirty = true;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06002337 RID: 9015 RVA: 0x000BEB9F File Offset: 0x000BCD9F
	// (set) Token: 0x06002338 RID: 9016 RVA: 0x000BEBA7 File Offset: 0x000BCDA7
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
			this.dirty = true;
		}
	}

	// Token: 0x04001442 RID: 5186
	[SerializeField]
	[Serialize]
	protected float threshold;

	// Token: 0x04001443 RID: 5187
	[SerializeField]
	[Serialize]
	protected bool activateAboveThreshold = true;

	// Token: 0x04001444 RID: 5188
	[Serialize]
	private bool dirty = true;

	// Token: 0x04001445 RID: 5189
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001446 RID: 5190
	private static readonly EventSystem.IntraObjectHandler<ConduitThresholdSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ConduitThresholdSensor>(delegate(ConduitThresholdSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
