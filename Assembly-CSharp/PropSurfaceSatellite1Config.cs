using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class PropSurfaceSatellite1Config : IEntityConfig
{
	// Token: 0x06000E61 RID: 3681 RVA: 0x0004DE94 File Offset: 0x0004C094
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0004DE9C File Offset: 0x0004C09C
	public GameObject CreatePrefab()
	{
		string text = "PropSurfaceSatellite1";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE1.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE1.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("satellite1_kanim"), "off", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_clothingfactory_kanim";
		setLocker.dropOffset = new Vector2I(0, 1);
		setLocker.numDataBanks = new int[] { 4, 9 };
		gameObject.AddOrGet<Demolishable>();
		LoreBearerUtil.AddLoreTo(gameObject);
		return gameObject;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0004DF7C File Offset: 0x0004C17C
	public static string[][] GetLockerBaseContents()
	{
		string text = (DlcManager.FeatureClusterSpaceEnabled() ? "OrbitalResearchDatabank" : "ResearchDatabank");
		return new string[][]
		{
			new string[] { text, text, text },
			new string[] { "ColdBreatherSeed", "ColdBreatherSeed", "ColdBreatherSeed" },
			new string[] { "Atmo_Suit", "Glom", "Glom", "Glom" }
		};
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0004E004 File Offset: 0x0004C204
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = PropSurfaceSatellite1Config.GetLockerBaseContents();
		component.ChooseContents();
		OccupyArea component2 = inst.GetComponent<OccupyArea>();
		component2.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		int num = Grid.PosToCell(inst);
		foreach (CellOffset cellOffset in component2.OccupiedCellsOffsets)
		{
			Grid.GravitasFacility[Grid.OffsetCell(num, cellOffset)] = true;
		}
		RadiationEmitter radiationEmitter = inst.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emitRadiusX = 12;
		radiationEmitter.emitRadiusY = 12;
		radiationEmitter.emitRads = 2400f / ((float)radiationEmitter.emitRadiusX / 6f);
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0004E0B8 File Offset: 0x0004C2B8
	public void OnSpawn(GameObject inst)
	{
		RadiationEmitter component = inst.GetComponent<RadiationEmitter>();
		if (component != null)
		{
			component.SetEmitting(true);
		}
	}

	// Token: 0x040007EF RID: 2031
	public const string ID = "PropSurfaceSatellite1";
}
