using System;

// Token: 0x020003EE RID: 1006
public class PathProberSensor : Sensor
{
	// Token: 0x060014D9 RID: 5337 RVA: 0x0006D794 File Offset: 0x0006B994
	public PathProberSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = sensors.GetComponent<Navigator>();
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x0006D7A9 File Offset: 0x0006B9A9
	public override void Update()
	{
		this.navigator.UpdateProbe(false);
	}

	// Token: 0x04000BB0 RID: 2992
	private Navigator navigator;
}
