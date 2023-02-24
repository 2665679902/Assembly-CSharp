using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C95 RID: 3221
	public class EquippableFacadeResource : PermitResource
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600658F RID: 25999 RVA: 0x0026B04E File Offset: 0x0026924E
		// (set) Token: 0x06006590 RID: 26000 RVA: 0x0026B056 File Offset: 0x00269256
		public string BuildOverride { get; private set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06006591 RID: 26001 RVA: 0x0026B05F File Offset: 0x0026925F
		// (set) Token: 0x06006592 RID: 26002 RVA: 0x0026B067 File Offset: 0x00269267
		public string DefID { get; private set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x0026B070 File Offset: 0x00269270
		// (set) Token: 0x06006594 RID: 26004 RVA: 0x0026B078 File Offset: 0x00269278
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x06006595 RID: 26005 RVA: 0x0026B081 File Offset: 0x00269281
		public EquippableFacadeResource(string id, string name, string buildOverride, string defID, string animFile)
			: base(id, name, "n/a", PermitCategory.Equipment, PermitRarity.Unknown)
		{
			this.DefID = defID;
			this.BuildOverride = buildOverride;
			this.AnimFile = Assets.GetAnim(animFile);
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x0026B0B4 File Offset: 0x002692B4
		public global::Tuple<Sprite, Color> GetUISprite()
		{
			if (this.AnimFile == null)
			{
				global::Debug.LogError("Facade AnimFile is null: " + this.DefID);
			}
			Sprite uispriteFromMultiObjectAnim = Def.GetUISpriteFromMultiObjectAnim(this.AnimFile, "ui", false, "");
			return new global::Tuple<Sprite, Color>(uispriteFromMultiObjectAnim, (uispriteFromMultiObjectAnim != null) ? Color.white : Color.clear);
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0026B114 File Offset: 0x00269314
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = this.GetUISprite().first;
			GameObject gameObject = Assets.TryGetPrefab(this.DefID);
			if (gameObject == null || !gameObject)
			{
				permitPresentationInfo.SetFacadeForPrefabID(this.DefID);
			}
			else
			{
				permitPresentationInfo.SetFacadeForPrefabName(gameObject.GetProperName());
			}
			return permitPresentationInfo;
		}
	}
}
