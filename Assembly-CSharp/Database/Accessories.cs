using System;

namespace Database
{
	// Token: 0x02000C71 RID: 3185
	public class Accessories : ResourceSet<Accessory>
	{
		// Token: 0x0600650C RID: 25868 RVA: 0x0025B69C File Offset: 0x0025989C
		public Accessories(ResourceSet parent)
			: base("Accessories", parent)
		{
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x0025B6AC File Offset: 0x002598AC
		public void AddAccessories(string id, KAnimFile anim_file)
		{
			if (anim_file != null)
			{
				KAnim.Build build = anim_file.GetData().build;
				for (int i = 0; i < build.symbols.Length; i++)
				{
					string text = HashCache.Get().Get(build.symbols[i].hash);
					AccessorySlot accessorySlot = Db.Get().AccessorySlots.Find(text);
					if (accessorySlot != null)
					{
						Accessory accessory = new Accessory(id + text, this, accessorySlot, anim_file.batchTag, build.symbols[i], anim_file, null);
						accessorySlot.accessories.Add(accessory);
						HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);
					}
				}
			}
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x0025B764 File Offset: 0x00259964
		public void AddCustomAccessories(KAnimFile anim_file, ResourceSet parent, AccessorySlots slots)
		{
			if (anim_file != null)
			{
				KAnim.Build build = anim_file.GetData().build;
				for (int i = 0; i < build.symbols.Length; i++)
				{
					string symbol_name = HashCache.Get().Get(build.symbols[i].hash);
					AccessorySlot accessorySlot = slots.resources.Find((AccessorySlot slot) => symbol_name.IndexOf(slot.Id, 0, StringComparison.OrdinalIgnoreCase) != -1);
					if (accessorySlot != null)
					{
						Accessory accessory = new Accessory(symbol_name, parent, accessorySlot, anim_file.batchTag, build.symbols[i], anim_file, null);
						accessorySlot.accessories.Add(accessory);
						HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);
					}
				}
			}
		}
	}
}
