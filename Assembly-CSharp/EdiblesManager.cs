using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200072F RID: 1839
[AddComponentMenu("KMonoBehaviour/scripts/EdiblesManager")]
public class EdiblesManager : KMonoBehaviour
{
	// Token: 0x0600326F RID: 12911 RVA: 0x00110ACC File Offset: 0x0010ECCC
	public static List<EdiblesManager.FoodInfo> GetAllFoodTypes()
	{
		return EdiblesManager.s_allFoodTypes.Where((EdiblesManager.FoodInfo x) => DlcManager.IsContentActive(x.DlcId)).ToList<EdiblesManager.FoodInfo>();
	}

	// Token: 0x06003270 RID: 12912 RVA: 0x00110AFC File Offset: 0x0010ECFC
	public static EdiblesManager.FoodInfo GetFoodInfo(string foodID)
	{
		string text = foodID.Replace("Compost", "");
		EdiblesManager.FoodInfo foodInfo = null;
		EdiblesManager.s_allFoodMap.TryGetValue(text, out foodInfo);
		return foodInfo;
	}

	// Token: 0x06003271 RID: 12913 RVA: 0x00110B2B File Offset: 0x0010ED2B
	public static bool TryGetFoodInfo(string foodID, out EdiblesManager.FoodInfo info)
	{
		info = null;
		if (string.IsNullOrEmpty(foodID))
		{
			return false;
		}
		info = EdiblesManager.GetFoodInfo(foodID);
		return info != null;
	}

	// Token: 0x04001EB3 RID: 7859
	private static List<EdiblesManager.FoodInfo> s_allFoodTypes = new List<EdiblesManager.FoodInfo>();

	// Token: 0x04001EB4 RID: 7860
	private static Dictionary<string, EdiblesManager.FoodInfo> s_allFoodMap = new Dictionary<string, EdiblesManager.FoodInfo>();

	// Token: 0x0200143C RID: 5180
	public class FoodInfo : IConsumableUIItem
	{
		// Token: 0x06008091 RID: 32913 RVA: 0x002DF508 File Offset: 0x002DD708
		public FoodInfo(string id, string dlcId, float caloriesPerUnit, int quality, float preserveTemperatue, float rotTemperature, float spoilTime, bool can_rot)
		{
			this.Id = id;
			this.DlcId = dlcId;
			this.CaloriesPerUnit = caloriesPerUnit;
			this.Quality = quality;
			this.PreserveTemperature = preserveTemperatue;
			this.RotTemperature = rotTemperature;
			this.StaleTime = spoilTime / 2f;
			this.SpoilTime = spoilTime;
			this.CanRot = can_rot;
			this.Name = Strings.Get("STRINGS.ITEMS.FOOD." + id.ToUpper() + ".NAME");
			this.Description = Strings.Get("STRINGS.ITEMS.FOOD." + id.ToUpper() + ".DESC");
			this.Effects = new List<string>();
			EdiblesManager.s_allFoodTypes.Add(this);
			EdiblesManager.s_allFoodMap[this.Id] = this;
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x002DF5D7 File Offset: 0x002DD7D7
		public EdiblesManager.FoodInfo AddEffects(List<string> effects, string[] dlcIds)
		{
			if (DlcManager.IsDlcListValidForCurrentContent(dlcIds))
			{
				this.Effects.AddRange(effects);
			}
			return this;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06008093 RID: 32915 RVA: 0x002DF5EE File Offset: 0x002DD7EE
		public string ConsumableId
		{
			get
			{
				return this.Id;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06008094 RID: 32916 RVA: 0x002DF5F6 File Offset: 0x002DD7F6
		public string ConsumableName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06008095 RID: 32917 RVA: 0x002DF5FE File Offset: 0x002DD7FE
		public int MajorOrder
		{
			get
			{
				return this.Quality;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06008096 RID: 32918 RVA: 0x002DF606 File Offset: 0x002DD806
		public int MinorOrder
		{
			get
			{
				return (int)this.CaloriesPerUnit;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06008097 RID: 32919 RVA: 0x002DF60F File Offset: 0x002DD80F
		public bool Display
		{
			get
			{
				return this.CaloriesPerUnit != 0f;
			}
		}

		// Token: 0x040062D5 RID: 25301
		public string Id;

		// Token: 0x040062D6 RID: 25302
		public string DlcId;

		// Token: 0x040062D7 RID: 25303
		public string Name;

		// Token: 0x040062D8 RID: 25304
		public string Description;

		// Token: 0x040062D9 RID: 25305
		public float CaloriesPerUnit;

		// Token: 0x040062DA RID: 25306
		public float PreserveTemperature;

		// Token: 0x040062DB RID: 25307
		public float RotTemperature;

		// Token: 0x040062DC RID: 25308
		public float StaleTime;

		// Token: 0x040062DD RID: 25309
		public float SpoilTime;

		// Token: 0x040062DE RID: 25310
		public bool CanRot;

		// Token: 0x040062DF RID: 25311
		public int Quality;

		// Token: 0x040062E0 RID: 25312
		public List<string> Effects;
	}
}
