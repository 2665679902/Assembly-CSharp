using System;
using UnityEngine;

// Token: 0x02000ABB RID: 2747
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/InfoDescription")]
public class InfoDescription : KMonoBehaviour
{
	// Token: 0x17000638 RID: 1592
	// (get) Token: 0x060053FE RID: 21502 RVA: 0x001E8490 File Offset: 0x001E6690
	// (set) Token: 0x060053FD RID: 21501 RVA: 0x001E8469 File Offset: 0x001E6669
	public string DescriptionLocString
	{
		get
		{
			return this.descriptionLocString;
		}
		set
		{
			this.descriptionLocString = value;
			if (this.descriptionLocString != null)
			{
				this.description = Strings.Get(this.descriptionLocString);
			}
		}
	}

	// Token: 0x060053FF RID: 21503 RVA: 0x001E8498 File Offset: 0x001E6698
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (!string.IsNullOrEmpty(this.nameLocString))
		{
			this.displayName = Strings.Get(this.nameLocString);
		}
		if (!string.IsNullOrEmpty(this.descriptionLocString))
		{
			this.description = Strings.Get(this.descriptionLocString);
		}
	}

	// Token: 0x04003914 RID: 14612
	public string nameLocString = "";

	// Token: 0x04003915 RID: 14613
	private string descriptionLocString = "";

	// Token: 0x04003916 RID: 14614
	public string description;

	// Token: 0x04003917 RID: 14615
	public string displayName;
}
