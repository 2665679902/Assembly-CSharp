using System;

// Token: 0x020003EF RID: 1007
public class PickupableSensor : Sensor
{
	// Token: 0x060014DB RID: 5339 RVA: 0x0006D7B7 File Offset: 0x0006B9B7
	public PickupableSensor(Sensors sensors)
		: base(sensors)
	{
		this.worker = base.GetComponent<Worker>();
		this.pathProber = base.GetComponent<PathProber>();
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x0006D7D8 File Offset: 0x0006B9D8
	public override void Update()
	{
		GlobalChoreProvider.Instance.UpdateFetches(this.pathProber);
		Game.Instance.fetchManager.UpdatePickups(this.pathProber, this.worker);
	}

	// Token: 0x04000BB1 RID: 2993
	private PathProber pathProber;

	// Token: 0x04000BB2 RID: 2994
	private Worker worker;
}
