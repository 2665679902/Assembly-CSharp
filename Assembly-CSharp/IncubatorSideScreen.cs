using System;
using UnityEngine;

// Token: 0x02000BBB RID: 3003
public class IncubatorSideScreen : ReceptacleSideScreen
{
	// Token: 0x06005E65 RID: 24165 RVA: 0x00227DDF File Offset: 0x00225FDF
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<EggIncubator>() != null;
	}

	// Token: 0x06005E66 RID: 24166 RVA: 0x00227DF0 File Offset: 0x00225FF0
	protected override void SetResultDescriptions(GameObject go)
	{
		string text = "";
		InfoDescription component = go.GetComponent<InfoDescription>();
		if (component)
		{
			text += component.description;
		}
		this.descriptionLabel.SetText(text);
	}

	// Token: 0x06005E67 RID: 24167 RVA: 0x00227E2B File Offset: 0x0022602B
	protected override bool RequiresAvailableAmountToDeposit()
	{
		return false;
	}

	// Token: 0x06005E68 RID: 24168 RVA: 0x00227E2E File Offset: 0x0022602E
	protected override Sprite GetEntityIcon(Tag prefabTag)
	{
		return Def.GetUISprite(Assets.GetPrefab(prefabTag), "ui", false).first;
	}

	// Token: 0x06005E69 RID: 24169 RVA: 0x00227E48 File Offset: 0x00226048
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		EggIncubator incubator = target.GetComponent<EggIncubator>();
		this.continuousToggle.ChangeState(incubator.autoReplaceEntity ? 0 : 1);
		this.continuousToggle.onClick = delegate
		{
			incubator.autoReplaceEntity = !incubator.autoReplaceEntity;
			this.continuousToggle.ChangeState(incubator.autoReplaceEntity ? 0 : 1);
		};
	}

	// Token: 0x04004092 RID: 16530
	public DescriptorPanel RequirementsDescriptorPanel;

	// Token: 0x04004093 RID: 16531
	public DescriptorPanel HarvestDescriptorPanel;

	// Token: 0x04004094 RID: 16532
	public DescriptorPanel EffectsDescriptorPanel;

	// Token: 0x04004095 RID: 16533
	public MultiToggle continuousToggle;
}
