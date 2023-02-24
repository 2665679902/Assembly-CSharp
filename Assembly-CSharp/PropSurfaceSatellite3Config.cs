using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class PropSurfaceSatellite3Config : IEntityConfig
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x0004E2B8 File Offset: 0x0004C4B8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0004E2C0 File Offset: 0x0004C4C0
	public GameObject CreatePrefab()
	{
		string id = PropSurfaceSatellite3Config.ID;
		string text = STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE3.NAME;
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE3.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("satellite3_kanim"), "off", Grid.SceneLayer.Building, 6, 6, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
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
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0004E3A0 File Offset: 0x0004C5A0
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

	// Token: 0x06000E70 RID: 3696 RVA: 0x0004E454 File Offset: 0x0004C654
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-372600542, delegate(object locker)
		{
			this.OnLockerLooted(inst);
		});
		RadiationEmitter component = inst.GetComponent<RadiationEmitter>();
		if (component != null)
		{
			component.SetEmitting(true);
		}
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0004E4AE File Offset: 0x0004C6AE
	private void OnLockerLooted(GameObject inst)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(ArtifactSelector.Instance.GetUniqueArtifactID(ArtifactType.Any)), inst.transform.position);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.TerrestrialArtifact, true);
		gameObject.SetActive(true);
	}

	// Token: 0x040007F1 RID: 2033
	public static string ID = "PropSurfaceSatellite3";
}
