using System;
using STRINGS;
using TUNING;

namespace Database
{
	// Token: 0x02000C9E RID: 3230
	public class AssignableSlots : ResourceSet<AssignableSlot>
	{
		// Token: 0x060065B1 RID: 26033 RVA: 0x0026E088 File Offset: 0x0026C288
		public AssignableSlots()
		{
			this.Bed = base.Add(new OwnableSlot("Bed", MISC.TAGS.BED));
			this.MessStation = base.Add(new OwnableSlot("MessStation", MISC.TAGS.MESSSTATION));
			this.Clinic = base.Add(new OwnableSlot("Clinic", MISC.TAGS.CLINIC));
			this.MedicalBed = base.Add(new OwnableSlot("MedicalBed", MISC.TAGS.CLINIC));
			this.MedicalBed.showInUI = false;
			this.GeneShuffler = base.Add(new OwnableSlot("GeneShuffler", MISC.TAGS.GENE_SHUFFLER));
			this.GeneShuffler.showInUI = false;
			this.Toilet = base.Add(new OwnableSlot("Toilet", MISC.TAGS.TOILET));
			this.MassageTable = base.Add(new OwnableSlot("MassageTable", MISC.TAGS.MASSAGE_TABLE));
			this.RocketCommandModule = base.Add(new OwnableSlot("RocketCommandModule", MISC.TAGS.COMMAND_MODULE));
			this.HabitatModule = base.Add(new OwnableSlot("HabitatModule", MISC.TAGS.HABITAT_MODULE));
			this.ResetSkillsStation = base.Add(new OwnableSlot("ResetSkillsStation", "ResetSkillsStation"));
			this.WarpPortal = base.Add(new OwnableSlot("WarpPortal", MISC.TAGS.WARP_PORTAL));
			this.WarpPortal.showInUI = false;
			this.Toy = base.Add(new EquipmentSlot(TUNING.EQUIPMENT.TOYS.SLOT, MISC.TAGS.TOY, false));
			this.Suit = base.Add(new EquipmentSlot(TUNING.EQUIPMENT.SUITS.SLOT, MISC.TAGS.SUIT, true));
			this.Tool = base.Add(new EquipmentSlot(TUNING.EQUIPMENT.TOOLS.TOOLSLOT, MISC.TAGS.MULTITOOL, false));
			this.Outfit = base.Add(new EquipmentSlot(TUNING.EQUIPMENT.CLOTHING.SLOT, MISC.TAGS.CLOTHES, true));
		}

		// Token: 0x04004993 RID: 18835
		public AssignableSlot Bed;

		// Token: 0x04004994 RID: 18836
		public AssignableSlot MessStation;

		// Token: 0x04004995 RID: 18837
		public AssignableSlot Clinic;

		// Token: 0x04004996 RID: 18838
		public AssignableSlot GeneShuffler;

		// Token: 0x04004997 RID: 18839
		public AssignableSlot MedicalBed;

		// Token: 0x04004998 RID: 18840
		public AssignableSlot Toilet;

		// Token: 0x04004999 RID: 18841
		public AssignableSlot MassageTable;

		// Token: 0x0400499A RID: 18842
		public AssignableSlot RocketCommandModule;

		// Token: 0x0400499B RID: 18843
		public AssignableSlot HabitatModule;

		// Token: 0x0400499C RID: 18844
		public AssignableSlot ResetSkillsStation;

		// Token: 0x0400499D RID: 18845
		public AssignableSlot WarpPortal;

		// Token: 0x0400499E RID: 18846
		public AssignableSlot Toy;

		// Token: 0x0400499F RID: 18847
		public AssignableSlot Suit;

		// Token: 0x040049A0 RID: 18848
		public AssignableSlot Tool;

		// Token: 0x040049A1 RID: 18849
		public AssignableSlot Outfit;
	}
}
