using System;
using System.Collections.Generic;

// Token: 0x02000989 RID: 2441
public class RocketProcessConditionDisplayTarget : KMonoBehaviour, IProcessConditionSet, ISim1000ms
{
	// Token: 0x06004857 RID: 18519 RVA: 0x0019601E File Offset: 0x0019421E
	public List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		if (this.craftModuleInterface == null)
		{
			this.craftModuleInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		}
		return this.craftModuleInterface.GetConditionSet(conditionType);
	}

	// Token: 0x06004858 RID: 18520 RVA: 0x0019604C File Offset: 0x0019424C
	public void Sim1000ms(float dt)
	{
		bool flag = false;
		using (List<ProcessCondition>.Enumerator enumerator = this.GetConditionSet(ProcessCondition.ProcessConditionType.All).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.EvaluateCondition() == ProcessCondition.Status.Failure)
				{
					flag = true;
					if (this.statusHandle == Guid.Empty)
					{
						this.statusHandle = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.RocketChecklistIncomplete, null);
						break;
					}
					break;
				}
			}
		}
		if (!flag && this.statusHandle != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
		}
	}

	// Token: 0x04002F93 RID: 12179
	private CraftModuleInterface craftModuleInterface;

	// Token: 0x04002F94 RID: 12180
	private Guid statusHandle = Guid.Empty;
}
