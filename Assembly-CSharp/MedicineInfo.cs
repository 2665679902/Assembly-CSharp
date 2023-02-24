using System;
using System.Collections.Generic;

// Token: 0x0200080E RID: 2062
[Serializable]
public class MedicineInfo
{
	// Token: 0x06003BCD RID: 15309 RVA: 0x0014B624 File Offset: 0x00149824
	public MedicineInfo(string id, string effect, MedicineInfo.MedicineType medicineType, string doctorStationId, string[] curedDiseases = null)
	{
		Debug.Assert(!string.IsNullOrEmpty(effect) || (curedDiseases != null && curedDiseases.Length != 0), "Medicine should have an effect or cure diseases");
		this.id = id;
		this.effect = effect;
		this.medicineType = medicineType;
		this.doctorStationId = doctorStationId;
		if (curedDiseases != null)
		{
			this.curedSicknesses = new List<string>(curedDiseases);
			return;
		}
		this.curedSicknesses = new List<string>();
	}

	// Token: 0x06003BCE RID: 15310 RVA: 0x0014B693 File Offset: 0x00149893
	public Tag GetSupplyTag()
	{
		return MedicineInfo.GetSupplyTagForStation(this.doctorStationId);
	}

	// Token: 0x06003BCF RID: 15311 RVA: 0x0014B6A0 File Offset: 0x001498A0
	public static Tag GetSupplyTagForStation(string stationID)
	{
		Tag tag = TagManager.Create(stationID + GameTags.MedicalSupplies.Name);
		Assets.AddCountableTag(tag);
		return tag;
	}

	// Token: 0x040026F7 RID: 9975
	public string id;

	// Token: 0x040026F8 RID: 9976
	public string effect;

	// Token: 0x040026F9 RID: 9977
	public MedicineInfo.MedicineType medicineType;

	// Token: 0x040026FA RID: 9978
	public List<string> curedSicknesses;

	// Token: 0x040026FB RID: 9979
	public string doctorStationId;

	// Token: 0x02001564 RID: 5476
	public enum MedicineType
	{
		// Token: 0x04006693 RID: 26259
		Booster,
		// Token: 0x04006694 RID: 26260
		CureAny,
		// Token: 0x04006695 RID: 26261
		CureSpecific
	}
}
