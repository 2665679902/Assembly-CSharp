using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000B7A RID: 2938
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleBlockButton")]
public class ScheduleBlockButton : KMonoBehaviour
{
	// Token: 0x17000673 RID: 1651
	// (get) Token: 0x06005C63 RID: 23651 RVA: 0x0021CAB9 File Offset: 0x0021ACB9
	// (set) Token: 0x06005C64 RID: 23652 RVA: 0x0021CAC1 File Offset: 0x0021ACC1
	public int idx { get; private set; }

	// Token: 0x06005C65 RID: 23653 RVA: 0x0021CACC File Offset: 0x0021ACCC
	public void Setup(int idx, Dictionary<string, ColorStyleSetting> paintStyles, int totalBlocks)
	{
		this.idx = idx;
		this.paintStyles = paintStyles;
		if (idx < TRAITS.EARLYBIRD_SCHEDULEBLOCK)
		{
			base.GetComponent<HierarchyReferences>().GetReference<RectTransform>("MorningIcon").gameObject.SetActive(true);
		}
		else if (idx >= totalBlocks - 3)
		{
			base.GetComponent<HierarchyReferences>().GetReference<RectTransform>("NightIcon").gameObject.SetActive(true);
		}
		base.gameObject.name = "ScheduleBlock_" + idx.ToString();
	}

	// Token: 0x06005C66 RID: 23654 RVA: 0x0021CB4C File Offset: 0x0021AD4C
	public void SetBlockTypes(List<ScheduleBlockType> blockTypes)
	{
		ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(blockTypes);
		if (scheduleGroup != null && this.paintStyles.ContainsKey(scheduleGroup.Id))
		{
			this.image.colorStyleSetting = this.paintStyles[scheduleGroup.Id];
			this.image.ApplyColorStyleSetting();
			this.toolTip.SetSimpleTooltip(scheduleGroup.GetTooltip());
			return;
		}
		this.toolTip.SetSimpleTooltip("UNKNOWN");
	}

	// Token: 0x04003F1F RID: 16159
	[SerializeField]
	private KImage image;

	// Token: 0x04003F20 RID: 16160
	[SerializeField]
	private ToolTip toolTip;

	// Token: 0x04003F22 RID: 16162
	private Dictionary<string, ColorStyleSetting> paintStyles;
}
