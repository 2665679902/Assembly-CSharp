using System;
using UnityEngine;

// Token: 0x020004CD RID: 1229
[AddComponentMenu("KMonoBehaviour/scripts/SkillPerkMissingComplainer")]
public class SkillPerkMissingComplainer : KMonoBehaviour
{
	// Token: 0x06001C7D RID: 7293 RVA: 0x00097CBF File Offset: 0x00095EBF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			this.skillUpdateHandle = Game.Instance.Subscribe(-1523247426, new Action<object>(this.UpdateStatusItem));
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x00097CFD File Offset: 0x00095EFD
	protected override void OnCleanUp()
	{
		if (this.skillUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.skillUpdateHandle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00097D20 File Offset: 0x00095F20
	protected virtual void UpdateStatusItem(object data = null)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (component == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			return;
		}
		bool flag = MinionResume.AnyMinionHasPerk(this.requiredSkillPerk, this.GetMyWorldId());
		if (!flag && this.workStatusItemHandle == Guid.Empty)
		{
			this.workStatusItemHandle = component.AddStatusItem(Db.Get().BuildingStatusItems.ColonyLacksRequiredSkillPerk, this.requiredSkillPerk);
			return;
		}
		if (flag && this.workStatusItemHandle != Guid.Empty)
		{
			component.RemoveStatusItem(this.workStatusItemHandle, false);
			this.workStatusItemHandle = Guid.Empty;
		}
	}

	// Token: 0x04001008 RID: 4104
	public string requiredSkillPerk;

	// Token: 0x04001009 RID: 4105
	private int skillUpdateHandle = -1;

	// Token: 0x0400100A RID: 4106
	private Guid workStatusItemHandle;
}
