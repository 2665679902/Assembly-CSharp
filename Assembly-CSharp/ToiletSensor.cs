using System;

// Token: 0x020003F3 RID: 1011
public class ToiletSensor : Sensor
{
	// Token: 0x060014F4 RID: 5364 RVA: 0x0006DBA3 File Offset: 0x0006BDA3
	public ToiletSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0006DBB8 File Offset: 0x0006BDB8
	public override void Update()
	{
		IUsable usable = null;
		int num = int.MaxValue;
		bool flag = false;
		foreach (IUsable usable2 in Components.Toilets.Items)
		{
			if (usable2.IsUsable())
			{
				flag = true;
				int navigationCost = this.navigator.GetNavigationCost(Grid.PosToCell(usable2.transform.GetPosition()));
				if (navigationCost != -1 && navigationCost < num)
				{
					usable = usable2;
					num = navigationCost;
				}
			}
		}
		bool flag2 = Components.Toilets.Count > 0;
		if (usable != this.toilet || flag2 != this.areThereAnyToilets || this.areThereAnyUsableToilets != flag)
		{
			this.toilet = usable;
			this.areThereAnyToilets = flag2;
			this.areThereAnyUsableToilets = flag;
			base.Trigger(-752545459, null);
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x0006DC98 File Offset: 0x0006BE98
	public bool AreThereAnyToilets()
	{
		return this.areThereAnyToilets;
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x0006DCA0 File Offset: 0x0006BEA0
	public bool AreThereAnyUsableToilets()
	{
		return this.areThereAnyUsableToilets;
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x0006DCA8 File Offset: 0x0006BEA8
	public IUsable GetNearestUsableToilet()
	{
		return this.toilet;
	}

	// Token: 0x04000BBB RID: 3003
	private Navigator navigator;

	// Token: 0x04000BBC RID: 3004
	private IUsable toilet;

	// Token: 0x04000BBD RID: 3005
	private bool areThereAnyToilets;

	// Token: 0x04000BBE RID: 3006
	private bool areThereAnyUsableToilets;
}
