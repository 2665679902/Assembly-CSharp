using System;
using System.Diagnostics;

// Token: 0x02000628 RID: 1576
[DebuggerDisplay("{name}")]
public class PowerTransformer : Generator
{
	// Token: 0x06002952 RID: 10578 RVA: 0x000DA782 File Offset: 0x000D8982
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.battery = base.GetComponent<Battery>();
		base.Subscribe<PowerTransformer>(-592767678, PowerTransformer.OnOperationalChangedDelegate);
		this.UpdateJoulesLostPerSecond();
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x000DA7AD File Offset: 0x000D89AD
	public override void ApplyDeltaJoules(float joules_delta, bool can_over_power = false)
	{
		this.battery.ConsumeEnergy(-joules_delta);
		base.ApplyDeltaJoules(joules_delta, can_over_power);
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x000DA7C4 File Offset: 0x000D89C4
	public override void ConsumeEnergy(float joules)
	{
		this.battery.ConsumeEnergy(joules);
		base.ConsumeEnergy(joules);
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x000DA7D9 File Offset: 0x000D89D9
	private void OnOperationalChanged(object data)
	{
		this.UpdateJoulesLostPerSecond();
	}

	// Token: 0x06002956 RID: 10582 RVA: 0x000DA7E1 File Offset: 0x000D89E1
	private void UpdateJoulesLostPerSecond()
	{
		if (this.operational.IsOperational)
		{
			this.battery.joulesLostPerSecond = 0f;
			return;
		}
		this.battery.joulesLostPerSecond = 3.3333333f;
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x000DA814 File Offset: 0x000D8A14
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		float num = (this.operational.IsOperational ? Math.Min(this.battery.JoulesAvailable, base.WattageRating * dt) : 0f);
		base.AssignJoulesAvailable(num);
		ushort circuitID = this.battery.CircuitID;
		ushort circuitID2 = base.CircuitID;
		bool flag = circuitID == circuitID2 && circuitID != ushort.MaxValue;
		if (this.mLoopDetected != flag)
		{
			this.mLoopDetected = flag;
			this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.PowerLoopDetected, this.mLoopDetected, this);
		}
	}

	// Token: 0x0400185B RID: 6235
	private Battery battery;

	// Token: 0x0400185C RID: 6236
	private bool mLoopDetected;

	// Token: 0x0400185D RID: 6237
	private static readonly EventSystem.IntraObjectHandler<PowerTransformer> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<PowerTransformer>(delegate(PowerTransformer component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
