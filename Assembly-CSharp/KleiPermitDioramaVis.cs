using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Database;
using UnityEngine;

// Token: 0x02000AD7 RID: 2775
public class KleiPermitDioramaVis : KMonoBehaviour
{
	// Token: 0x06005516 RID: 21782 RVA: 0x001ED14C File Offset: 0x001EB34C
	protected override void OnPrefabInit()
	{
		this.allVisList = ReflectionUtil.For<KleiPermitDioramaVis>(this).CollectValuesForFieldsThatInheritOrImplement<IKleiPermitDioramaVisTarget>(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		foreach (IKleiPermitDioramaVisTarget kleiPermitDioramaVisTarget in this.allVisList)
		{
			kleiPermitDioramaVisTarget.ConfigureSetup();
		}
	}

	// Token: 0x06005517 RID: 21783 RVA: 0x001ED1AC File Offset: 0x001EB3AC
	public void ConfigureWith(PermitResource permit)
	{
		foreach (IKleiPermitDioramaVisTarget kleiPermitDioramaVisTarget in this.allVisList)
		{
			kleiPermitDioramaVisTarget.GetGameObject().SetActive(false);
		}
		IKleiPermitDioramaVisTarget permitVisTarget = this.GetPermitVisTarget(permit);
		permitVisTarget.GetGameObject().SetActive(true);
		permitVisTarget.ConfigureWith(permit);
	}

	// Token: 0x06005518 RID: 21784 RVA: 0x001ED218 File Offset: 0x001EB418
	public IKleiPermitDioramaVisTarget GetPermitVisTarget(PermitResource permit)
	{
		KleiPermitDioramaVis.lastRenderedPermit = permit;
		if (permit == null)
		{
			return this.fallbackVis.WithError(string.Format("Given invalid permit: {0}", permit));
		}
		if (permit.Category == PermitCategory.Equipment || permit.Category == PermitCategory.DupeTops || permit.Category == PermitCategory.DupeBottoms || permit.Category == PermitCategory.DupeGloves || permit.Category == PermitCategory.DupeShoes || permit.Category == PermitCategory.DupeHats || permit.Category == PermitCategory.DupeAccessories)
		{
			return this.equipmentVis;
		}
		if (permit.Category == PermitCategory.Building)
		{
			bool flag;
			BuildLocationRule buildLocationRule;
			KleiPermitVisUtil.GetBuildLocationRule(permit).Deconstruct(out flag, out buildLocationRule);
			bool flag2 = flag;
			BuildLocationRule buildLocationRule2 = buildLocationRule;
			if (!flag2)
			{
				return this.fallbackVis.WithError("Couldn't get BuildLocationRule on permit with id \"" + permit.Id + "\"");
			}
			switch (buildLocationRule2)
			{
			case BuildLocationRule.OnFloor:
				return this.buildingOnFloorVis;
			case BuildLocationRule.OnCeiling:
			{
				string prefabID = KleiPermitVisUtil.GetBuildingDef(permit).Value.PrefabID;
				if (prefabID == "FlowerVaseHanging" || prefabID == "FlowerVaseHangingFancy")
				{
					return this.buildingPresentationStandHangingVis;
				}
				return this.buildingPresentationStandVis.WithAlignment(Alignment.Top());
			}
			case BuildLocationRule.OnWall:
				return this.buildingPresentationStandVis.WithAlignment(Alignment.Left());
			case BuildLocationRule.InCorner:
				return this.buildingPresentationStandVis.WithAlignment(Alignment.TopLeft());
			case BuildLocationRule.NotInTiles:
				return this.pedestalAndItemVis;
			}
			return this.fallbackVis.WithError(string.Format("No visualization available for building with BuildLocationRule of {0}", buildLocationRule2));
		}
		else if (permit.Category == PermitCategory.Artwork)
		{
			bool flag;
			BuildingDef buildingDef;
			KleiPermitVisUtil.GetBuildingDef(permit).Deconstruct(out flag, out buildingDef);
			bool flag3 = flag;
			BuildingDef buildingDef2 = buildingDef;
			if (!flag3)
			{
				return this.fallbackVis.WithError("Couldn't find building def for Artable " + permit.Id);
			}
			ArtableStage artableStage = (ArtableStage)permit;
			if (KleiPermitDioramaVis.<GetPermitVisTarget>g__Has|14_0<Sculpture>(buildingDef2))
			{
				return this.artableSculptureVis;
			}
			if (KleiPermitDioramaVis.<GetPermitVisTarget>g__Has|14_0<Painting>(buildingDef2))
			{
				return this.artablePaintingVis;
			}
			return this.fallbackVis.WithError("No visualization available for Artable " + permit.Id);
		}
		else
		{
			if (permit.Category != PermitCategory.JoyResponse)
			{
				return this.fallbackVis.WithError("No visualization has been defined for permit with id \"" + permit.Id + "\"");
			}
			if (permit is BalloonArtistFacadeResource)
			{
				return this.joyResponseBalloonVis;
			}
			return this.fallbackVis.WithError("No visualization available for JoyResponse " + permit.Id);
		}
	}

	// Token: 0x0600551A RID: 21786 RVA: 0x001ED46D File Offset: 0x001EB66D
	[CompilerGenerated]
	internal static bool <GetPermitVisTarget>g__Has|14_0<T>(BuildingDef buildingDef) where T : Component
	{
		return !buildingDef.BuildingComplete.GetComponent<T>().IsNullOrDestroyed();
	}

	// Token: 0x040039C9 RID: 14793
	[SerializeField]
	private KleiPermitDioramaVis_Fallback fallbackVis;

	// Token: 0x040039CA RID: 14794
	[SerializeField]
	private KleiPermitDioramaVis_DupeEquipment equipmentVis;

	// Token: 0x040039CB RID: 14795
	[SerializeField]
	private KleiPermitDioramaVis_BuildingOnFloor buildingOnFloorVis;

	// Token: 0x040039CC RID: 14796
	[SerializeField]
	private KleiPermitDioramaVis_BuildingPresentationStand buildingPresentationStandVis;

	// Token: 0x040039CD RID: 14797
	[SerializeField]
	private KleiPermitDioramaVis_BuildingPresentationStandHanging buildingPresentationStandHangingVis;

	// Token: 0x040039CE RID: 14798
	[SerializeField]
	private KleiPermitDioramaVis_BuildingHangingHook buildingHangingHookVis;

	// Token: 0x040039CF RID: 14799
	[SerializeField]
	private KleiPermitDioramaVis_PedestalAndItem pedestalAndItemVis;

	// Token: 0x040039D0 RID: 14800
	[SerializeField]
	private KleiPermitDioramaVis_ArtablePainting artablePaintingVis;

	// Token: 0x040039D1 RID: 14801
	[SerializeField]
	private KleiPermitDioramaVis_ArtableSculpture artableSculptureVis;

	// Token: 0x040039D2 RID: 14802
	[SerializeField]
	private KleiPermitDioramaVis_JoyResponseBalloon joyResponseBalloonVis;

	// Token: 0x040039D3 RID: 14803
	private IReadOnlyList<IKleiPermitDioramaVisTarget> allVisList;

	// Token: 0x040039D4 RID: 14804
	public static PermitResource lastRenderedPermit;
}
