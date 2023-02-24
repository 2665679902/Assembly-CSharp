using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C8B RID: 3211
	public class ClothingOutfitResource : Resource
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x00267CAC File Offset: 0x00265EAC
		// (set) Token: 0x0600656D RID: 25965 RVA: 0x00267CB4 File Offset: 0x00265EB4
		public string[] itemsInOutfit { get; private set; }

		// Token: 0x0600656E RID: 25966 RVA: 0x00267CBD File Offset: 0x00265EBD
		public ClothingOutfitResource(string id, string[] items_in_outfit)
			: base(id, null, null)
		{
			this.itemsInOutfit = items_in_outfit;
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x00267CCF File Offset: 0x00265ECF
		public ClothingOutfitResource(string id, string[] items_in_outfit, LocString name)
			: base(name, null, null)
		{
			this.itemsInOutfit = items_in_outfit;
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x00267CE6 File Offset: 0x00265EE6
		public global::Tuple<Sprite, Color> GetUISprite()
		{
			Sprite sprite = Assets.GetSprite("unknown");
			return new global::Tuple<Sprite, Color>(sprite, (sprite != null) ? Color.white : Color.clear);
		}
	}
}
