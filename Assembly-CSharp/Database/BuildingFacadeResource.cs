using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C84 RID: 3204
	public class BuildingFacadeResource : PermitResource
	{
		// Token: 0x0600654C RID: 25932 RVA: 0x0025FC66 File Offset: 0x0025DE66
		public BuildingFacadeResource(string Id, string Name, string Description, PermitRarity Rarity, string PrefabID, string AnimFile, Dictionary<string, string> workables = null)
			: base(Id, Name, Description, PermitCategory.Building, Rarity)
		{
			this.Id = Id;
			this.PrefabID = PrefabID;
			this.AnimFile = AnimFile;
			this.InteractFile = workables;
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x0025FC94 File Offset: 0x0025DE94
		public BuildingFacadeResource(string Id, string Name, string Description, PermitRarity Rarity, string PrefabID, string AnimFile, List<FacadeInfo.workable> workables = null)
			: base(Id, Name, Description, PermitCategory.Building, Rarity)
		{
			this.Id = Id;
			this.PrefabID = PrefabID;
			this.AnimFile = AnimFile;
			this.InteractFile = new Dictionary<string, string>();
			if (workables != null)
			{
				foreach (FacadeInfo.workable workable in workables)
				{
					this.InteractFile.Add(workable.workableName, workable.workableAnim);
				}
			}
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x0025FD28 File Offset: 0x0025DF28
		public void Init()
		{
			GameObject prefab = Assets.GetPrefab(this.PrefabID);
			if (prefab == null)
			{
				global::Debug.LogWarning("Missing prefab id " + this.PrefabID + " for facade " + this.Name);
				return;
			}
			prefab.AddOrGet<BuildingFacade>();
			BuildingDef def = prefab.GetComponent<Building>().Def;
			if (def != null)
			{
				def.AddFacade(this.Id);
			}
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x0025FD98 File Offset: 0x0025DF98
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(this.AnimFile), "ui", false, "");
			permitPresentationInfo.SetFacadeForPrefabID(this.PrefabID);
			return permitPresentationInfo;
		}

		// Token: 0x0400467F RID: 18047
		public string PrefabID;

		// Token: 0x04004680 RID: 18048
		public string AnimFile;

		// Token: 0x04004681 RID: 18049
		public Dictionary<string, string> InteractFile;
	}
}
