using System;

// Token: 0x020003EA RID: 1002
public class BreathableAreaSensor : Sensor
{
	// Token: 0x060014CC RID: 5324 RVA: 0x0006D46C File Offset: 0x0006B66C
	public BreathableAreaSensor(Sensors sensors)
		: base(sensors)
	{
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x0006D478 File Offset: 0x0006B678
	public override void Update()
	{
		if (this.breather == null)
		{
			this.breather = base.GetComponent<OxygenBreather>();
		}
		bool flag = this.isBreathable;
		this.isBreathable = this.breather.IsBreathableElement || this.breather.HasTag(GameTags.InTransitTube);
		if (this.isBreathable != flag)
		{
			if (this.isBreathable)
			{
				base.Trigger(99949694, null);
				return;
			}
			base.Trigger(-1189351068, null);
		}
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x0006D4F6 File Offset: 0x0006B6F6
	public bool IsBreathable()
	{
		return this.isBreathable;
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x0006D4FE File Offset: 0x0006B6FE
	public bool IsUnderwater()
	{
		return this.breather.IsUnderLiquid;
	}

	// Token: 0x04000BA4 RID: 2980
	private bool isBreathable;

	// Token: 0x04000BA5 RID: 2981
	private OxygenBreather breather;
}
