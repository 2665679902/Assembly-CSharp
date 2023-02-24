using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000A7C RID: 2684
[AddComponentMenu("KMonoBehaviour/scripts/CreatureFeeder")]
public class CreatureFeeder : KMonoBehaviour
{
	// Token: 0x0600521F RID: 21023 RVA: 0x001DA826 File Offset: 0x001D8A26
	protected override void OnSpawn()
	{
		Components.CreatureFeeders.Add(this.GetMyWorldId(), this);
		base.Subscribe<CreatureFeeder>(-1452790913, CreatureFeeder.OnAteFromStorageDelegate);
	}

	// Token: 0x06005220 RID: 21024 RVA: 0x001DA84A File Offset: 0x001D8A4A
	protected override void OnCleanUp()
	{
		Components.CreatureFeeders.Remove(this.GetMyWorldId(), this);
	}

	// Token: 0x06005221 RID: 21025 RVA: 0x001DA85D File Offset: 0x001D8A5D
	private void OnAteFromStorage(object data)
	{
		if (string.IsNullOrEmpty(this.effectId))
		{
			return;
		}
		(data as GameObject).GetComponent<Effects>().Add(this.effectId, true);
	}

	// Token: 0x0400375B RID: 14171
	public string effectId;

	// Token: 0x0400375C RID: 14172
	private static readonly EventSystem.IntraObjectHandler<CreatureFeeder> OnAteFromStorageDelegate = new EventSystem.IntraObjectHandler<CreatureFeeder>(delegate(CreatureFeeder component, object data)
	{
		component.OnAteFromStorage(data);
	});
}
