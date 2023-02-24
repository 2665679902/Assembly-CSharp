using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD4 RID: 3284
	public class EquipNDupes : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006684 RID: 26244 RVA: 0x00276601 File Offset: 0x00274801
		public EquipNDupes(AssignableSlot equipmentSlot, int numToEquip)
		{
			this.equipmentSlot = equipmentSlot;
			this.numToEquip = numToEquip;
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x00276618 File Offset: 0x00274818
		public override bool Success()
		{
			int num = 0;
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				Equipment equipment = minionIdentity.GetEquipment();
				if (equipment != null && equipment.IsSlotOccupied(this.equipmentSlot))
				{
					num++;
				}
			}
			return num >= this.numToEquip;
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x00276698 File Offset: 0x00274898
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.equipmentSlot = Db.Get().AssignableSlots.Get(text);
			this.numToEquip = reader.ReadInt32();
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x002766D0 File Offset: 0x002748D0
		public override string GetProgress(bool complete)
		{
			int num = 0;
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				Equipment equipment = minionIdentity.GetEquipment();
				if (equipment != null && equipment.IsSlotOccupied(this.equipmentSlot))
				{
					num++;
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CLOTHE_DUPES, complete ? this.numToEquip : num, this.numToEquip);
		}

		// Token: 0x04004ACF RID: 19151
		private AssignableSlot equipmentSlot;

		// Token: 0x04004AD0 RID: 19152
		private int numToEquip;
	}
}
