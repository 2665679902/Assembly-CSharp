using System;

namespace TUNING
{
	// Token: 0x02000D26 RID: 3366
	public class MEDICINE
	{
		// Token: 0x04004CF4 RID: 19700
		public const float DEFAULT_MASS = 1f;

		// Token: 0x04004CF5 RID: 19701
		public const float RECUPERATION_DISEASE_MULTIPLIER = 1.1f;

		// Token: 0x04004CF6 RID: 19702
		public const float RECUPERATION_DOCTORED_DISEASE_MULTIPLIER = 1.2f;

		// Token: 0x04004CF7 RID: 19703
		public const float WORK_TIME = 10f;

		// Token: 0x04004CF8 RID: 19704
		public static readonly MedicineInfo BASICBOOSTER = new MedicineInfo("BasicBooster", "Medicine_BasicBooster", MedicineInfo.MedicineType.Booster, null, null);

		// Token: 0x04004CF9 RID: 19705
		public static readonly MedicineInfo INTERMEDIATEBOOSTER = new MedicineInfo("IntermediateBooster", "Medicine_IntermediateBooster", MedicineInfo.MedicineType.Booster, null, null);

		// Token: 0x04004CFA RID: 19706
		public static readonly MedicineInfo BASICCURE = new MedicineInfo("BasicCure", null, MedicineInfo.MedicineType.CureSpecific, null, new string[] { "FoodSickness" });

		// Token: 0x04004CFB RID: 19707
		public static readonly MedicineInfo ANTIHISTAMINE = new MedicineInfo("Antihistamine", "HistamineSuppression", MedicineInfo.MedicineType.CureSpecific, null, new string[] { "Allergies" });

		// Token: 0x04004CFC RID: 19708
		public static readonly MedicineInfo INTERMEDIATECURE = new MedicineInfo("IntermediateCure", null, MedicineInfo.MedicineType.CureSpecific, "DoctorStation", new string[] { "SlimeSickness" });

		// Token: 0x04004CFD RID: 19709
		public static readonly MedicineInfo ADVANCEDCURE = new MedicineInfo("AdvancedCure", null, MedicineInfo.MedicineType.CureSpecific, "AdvancedDoctorStation", new string[] { "ZombieSickness" });

		// Token: 0x04004CFE RID: 19710
		public static readonly MedicineInfo BASICRADPILL = new MedicineInfo("BasicRadPill", "Medicine_BasicRadPill", MedicineInfo.MedicineType.Booster, null, null);

		// Token: 0x04004CFF RID: 19711
		public static readonly MedicineInfo INTERMEDIATERADPILL = new MedicineInfo("IntermediateRadPill", "Medicine_IntermediateRadPill", MedicineInfo.MedicineType.Booster, "AdvancedDoctorStation", null);
	}
}
