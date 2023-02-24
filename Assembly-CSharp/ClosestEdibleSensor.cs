using System;
using System.Collections.Generic;

// Token: 0x020003EB RID: 1003
public class ClosestEdibleSensor : Sensor
{
	// Token: 0x060014D0 RID: 5328 RVA: 0x0006D50B File Offset: 0x0006B70B
	public ClosestEdibleSensor(Sensors sensors)
		: base(sensors)
	{
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x0006D514 File Offset: 0x0006B714
	public override void Update()
	{
		HashSet<Tag> forbiddenTagSet = base.GetComponent<ConsumableConsumer>().forbiddenTagSet;
		Pickupable pickupable = Game.Instance.fetchManager.FindEdibleFetchTarget(base.GetComponent<Storage>(), forbiddenTagSet, GameTags.Edible);
		bool flag = this.edibleInReachButNotPermitted;
		Edible edible = null;
		bool flag2 = false;
		if (pickupable != null)
		{
			edible = pickupable.GetComponent<Edible>();
			flag2 = true;
			flag = false;
		}
		else
		{
			flag = Game.Instance.fetchManager.FindEdibleFetchTarget(base.GetComponent<Storage>(), new HashSet<Tag>(), GameTags.Edible) != null;
		}
		if (edible != this.edible || this.hasEdible != flag2)
		{
			this.edible = edible;
			this.hasEdible = flag2;
			this.edibleInReachButNotPermitted = flag;
			base.Trigger(86328522, this.edible);
		}
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x0006D5D1 File Offset: 0x0006B7D1
	public Edible GetEdible()
	{
		return this.edible;
	}

	// Token: 0x04000BA6 RID: 2982
	private Edible edible;

	// Token: 0x04000BA7 RID: 2983
	private bool hasEdible;

	// Token: 0x04000BA8 RID: 2984
	public bool edibleInReachButNotPermitted;
}
