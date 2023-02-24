using System;
using System.Collections.Generic;
using System.IO;
using KMod;
using TUNING;
using UnityEngine;

// Token: 0x02000274 RID: 628
public static class ModUtil
{
	// Token: 0x06000C89 RID: 3209 RVA: 0x00046903 File Offset: 0x00044B03
	public static void AddBuildingToPlanScreen(HashedString category, string building_id)
	{
		ModUtil.AddBuildingToPlanScreen(category, building_id, "uncategorized");
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x00046911 File Offset: 0x00044B11
	public static void AddBuildingToPlanScreen(HashedString category, string building_id, string subcategoryID)
	{
		ModUtil.AddBuildingToPlanScreen(category, building_id, subcategoryID, null, ModUtil.BuildingOrdering.After);
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00046920 File Offset: 0x00044B20
	public static void AddBuildingToPlanScreen(HashedString category, string building_id, string subcategoryID, string relativeBuildingId, ModUtil.BuildingOrdering ordering = ModUtil.BuildingOrdering.After)
	{
		int num = BUILDINGS.PLANORDER.FindIndex((PlanScreen.PlanInfo x) => x.category == category);
		if (num < 0)
		{
			global::Debug.LogWarning(string.Format("Mod: Unable to add '{0}' as category '{1}' does not exist", building_id, category));
			return;
		}
		List<KeyValuePair<string, string>> buildingAndSubcategoryData = BUILDINGS.PLANORDER[num].buildingAndSubcategoryData;
		KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(building_id, subcategoryID);
		if (relativeBuildingId == null)
		{
			buildingAndSubcategoryData.Add(keyValuePair);
			return;
		}
		int num2 = buildingAndSubcategoryData.FindIndex((KeyValuePair<string, string> x) => x.Key == relativeBuildingId);
		if (num2 == -1)
		{
			buildingAndSubcategoryData.Add(keyValuePair);
			global::Debug.LogWarning(string.Concat(new string[] { "Mod: Building '", relativeBuildingId, "' doesn't exist, inserting '", building_id, "' at the end of the list instead" }));
			return;
		}
		int num3 = ((ordering == ModUtil.BuildingOrdering.After) ? (num2 + 1) : Mathf.Max(num2 - 1, 0));
		buildingAndSubcategoryData.Insert(num3, keyValuePair);
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00046A18 File Offset: 0x00044C18
	[Obsolete("Use PlanScreen instead")]
	public static void AddBuildingToHotkeyBuildMenu(HashedString category, string building_id, global::Action hotkey)
	{
		BuildMenu.DisplayInfo info = BuildMenu.OrderedBuildings.GetInfo(category);
		if (info.category != category)
		{
			return;
		}
		(info.data as IList<BuildMenu.BuildingInfo>).Add(new BuildMenu.BuildingInfo(building_id, hotkey));
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00046A58 File Offset: 0x00044C58
	public static KAnimFile AddKAnimMod(string name, KAnimFile.Mod anim_mod)
	{
		KAnimFile kanimFile = ScriptableObject.CreateInstance<KAnimFile>();
		kanimFile.mod = anim_mod;
		kanimFile.name = name;
		AnimCommandFile animCommandFile = new AnimCommandFile();
		KAnimGroupFile.GroupFile groupFile = new KAnimGroupFile.GroupFile();
		groupFile.groupID = animCommandFile.GetGroupName(kanimFile);
		groupFile.commandDirectory = "assets/" + name;
		animCommandFile.AddGroupFile(groupFile);
		if (KAnimGroupFile.GetGroupFile().AddAnimMod(groupFile, animCommandFile, kanimFile) == KAnimGroupFile.AddModResult.Added)
		{
			Assets.ModLoadedKAnims.Add(kanimFile);
		}
		return kanimFile;
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00046AC8 File Offset: 0x00044CC8
	public static KAnimFile AddKAnim(string name, TextAsset anim_file, TextAsset build_file, IList<Texture2D> textures)
	{
		KAnimFile kanimFile = ScriptableObject.CreateInstance<KAnimFile>();
		kanimFile.Initialize(anim_file, build_file, textures);
		kanimFile.name = name;
		AnimCommandFile animCommandFile = new AnimCommandFile();
		KAnimGroupFile.GroupFile groupFile = new KAnimGroupFile.GroupFile();
		groupFile.groupID = animCommandFile.GetGroupName(kanimFile);
		groupFile.commandDirectory = "assets/" + name;
		animCommandFile.AddGroupFile(groupFile);
		KAnimGroupFile.GetGroupFile().AddAnimFile(groupFile, animCommandFile, kanimFile);
		Assets.ModLoadedKAnims.Add(kanimFile);
		return kanimFile;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00046B38 File Offset: 0x00044D38
	public static KAnimFile AddKAnim(string name, TextAsset anim_file, TextAsset build_file, Texture2D texture)
	{
		return ModUtil.AddKAnim(name, anim_file, build_file, new List<Texture2D> { texture });
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00046B5C File Offset: 0x00044D5C
	public static Substance CreateSubstance(string name, Element.State state, KAnimFile kanim, Material material, Color32 colour, Color32 ui_colour, Color32 conduit_colour)
	{
		return new Substance
		{
			name = name,
			nameTag = TagManager.Create(name),
			elementID = (SimHashes)Hash.SDBMLower(name),
			anim = kanim,
			colour = colour,
			uiColour = ui_colour,
			conduitColour = conduit_colour,
			material = material,
			renderedByWorld = ((state & Element.State.Solid) == Element.State.Solid)
		};
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00046BBF File Offset: 0x00044DBF
	public static void RegisterForTranslation(Type locstring_tree_root)
	{
		Localization.RegisterForTranslation(locstring_tree_root);
		Localization.GenerateStringsTemplate(locstring_tree_root, Path.Combine(Manager.GetDirectory(), "strings_templates"));
	}

	// Token: 0x02000EEE RID: 3822
	public enum BuildingOrdering
	{
		// Token: 0x040052BC RID: 21180
		Before,
		// Token: 0x040052BD RID: 21181
		After
	}
}
