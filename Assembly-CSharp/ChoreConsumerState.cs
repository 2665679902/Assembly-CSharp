using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020003A3 RID: 931
public class ChoreConsumerState
{
	// Token: 0x060012FE RID: 4862 RVA: 0x00064D04 File Offset: 0x00062F04
	public ChoreConsumerState(ChoreConsumer consumer)
	{
		this.consumer = consumer;
		this.navigator = consumer.GetComponent<Navigator>();
		this.prefabid = consumer.GetComponent<KPrefabID>();
		this.ownable = consumer.GetComponent<Ownable>();
		this.gameObject = consumer.gameObject;
		this.solidTransferArm = consumer.GetComponent<SolidTransferArm>();
		this.hasSolidTransferArm = this.solidTransferArm != null;
		this.resume = consumer.GetComponent<MinionResume>();
		this.choreDriver = consumer.GetComponent<ChoreDriver>();
		this.schedulable = consumer.GetComponent<Schedulable>();
		this.traits = consumer.GetComponent<Traits>();
		this.choreProvider = consumer.GetComponent<ChoreProvider>();
		MinionIdentity component = consumer.GetComponent<MinionIdentity>();
		if (component != null)
		{
			if (component.assignableProxy == null)
			{
				component.assignableProxy = MinionAssignablesProxy.InitAssignableProxy(component.assignableProxy, component);
			}
			this.assignables = component.GetSoleOwner();
			this.equipment = component.GetEquipment();
		}
		else
		{
			this.assignables = consumer.GetComponent<Assignables>();
			this.equipment = consumer.GetComponent<Equipment>();
		}
		this.storage = consumer.GetComponent<Storage>();
		this.consumableConsumer = consumer.GetComponent<ConsumableConsumer>();
		this.worker = consumer.GetComponent<Worker>();
		this.selectable = consumer.GetComponent<KSelectable>();
		if (this.schedulable != null)
		{
			int blockIdx = Schedule.GetBlockIdx();
			this.scheduleBlock = this.schedulable.GetSchedule().GetBlock(blockIdx);
		}
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x00064E60 File Offset: 0x00063060
	public void Refresh()
	{
		if (this.schedulable != null)
		{
			int blockIdx = Schedule.GetBlockIdx();
			Schedule schedule = this.schedulable.GetSchedule();
			if (schedule != null)
			{
				this.scheduleBlock = schedule.GetBlock(blockIdx);
			}
		}
	}

	// Token: 0x04000A39 RID: 2617
	public KPrefabID prefabid;

	// Token: 0x04000A3A RID: 2618
	public GameObject gameObject;

	// Token: 0x04000A3B RID: 2619
	public ChoreConsumer consumer;

	// Token: 0x04000A3C RID: 2620
	public ChoreProvider choreProvider;

	// Token: 0x04000A3D RID: 2621
	public Navigator navigator;

	// Token: 0x04000A3E RID: 2622
	public Ownable ownable;

	// Token: 0x04000A3F RID: 2623
	public Assignables assignables;

	// Token: 0x04000A40 RID: 2624
	public MinionResume resume;

	// Token: 0x04000A41 RID: 2625
	public ChoreDriver choreDriver;

	// Token: 0x04000A42 RID: 2626
	public Schedulable schedulable;

	// Token: 0x04000A43 RID: 2627
	public Traits traits;

	// Token: 0x04000A44 RID: 2628
	public Equipment equipment;

	// Token: 0x04000A45 RID: 2629
	public Storage storage;

	// Token: 0x04000A46 RID: 2630
	public ConsumableConsumer consumableConsumer;

	// Token: 0x04000A47 RID: 2631
	public KSelectable selectable;

	// Token: 0x04000A48 RID: 2632
	public Worker worker;

	// Token: 0x04000A49 RID: 2633
	public SolidTransferArm solidTransferArm;

	// Token: 0x04000A4A RID: 2634
	public bool hasSolidTransferArm;

	// Token: 0x04000A4B RID: 2635
	public ScheduleBlock scheduleBlock;
}
