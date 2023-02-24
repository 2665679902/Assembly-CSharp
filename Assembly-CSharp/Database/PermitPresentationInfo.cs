using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CA1 RID: 3233
	public struct PermitPresentationInfo
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x0026E4A2 File Offset: 0x0026C6A2
		// (set) Token: 0x060065B8 RID: 26040 RVA: 0x0026E4AA File Offset: 0x0026C6AA
		public string facadeFor { readonly get; private set; }

		// Token: 0x060065B9 RID: 26041 RVA: 0x0026E4B3 File Offset: 0x0026C6B3
		public static Sprite GetUnknownSprite()
		{
			return Assets.GetSprite("unknown");
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x0026E4C4 File Offset: 0x0026C6C4
		public void SetFacadeForPrefabName(string prefabName)
		{
			this.facadeFor = UI.KLEI_INVENTORY_SCREEN.ITEM_FACADE_FOR.Replace("{ConfigProperName}", prefabName);
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x0026E4DC File Offset: 0x0026C6DC
		public void SetFacadeForPrefabID(string prefabId)
		{
			this.facadeFor = UI.KLEI_INVENTORY_SCREEN.ITEM_FACADE_FOR.Replace("{ConfigProperName}", Assets.GetPrefab(prefabId).GetProperName());
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x0026E503 File Offset: 0x0026C703
		public void SetFacadeForText(string text)
		{
			this.facadeFor = text;
		}

		// Token: 0x040049B1 RID: 18865
		public Sprite sprite;
	}
}
