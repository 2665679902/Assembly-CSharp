using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000935 RID: 2357
[AddComponentMenu("KMonoBehaviour/scripts/ArtifactPOIConfigurator")]
public class ArtifactPOIConfigurator : KMonoBehaviour
{
	// Token: 0x06004503 RID: 17667 RVA: 0x00185414 File Offset: 0x00183614
	public static ArtifactPOIConfigurator.ArtifactPOIType FindType(HashedString typeId)
	{
		ArtifactPOIConfigurator.ArtifactPOIType artifactPOIType = null;
		if (typeId != HashedString.Invalid)
		{
			artifactPOIType = ArtifactPOIConfigurator._poiTypes.Find((ArtifactPOIConfigurator.ArtifactPOIType t) => t.id == typeId);
		}
		if (artifactPOIType == null)
		{
			global::Debug.LogError(string.Format("Tried finding a harvestable poi with id {0} but it doesn't exist!", typeId.ToString()));
		}
		return artifactPOIType;
	}

	// Token: 0x06004504 RID: 17668 RVA: 0x0018547D File Offset: 0x0018367D
	public ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration MakeConfiguration()
	{
		return this.CreateRandomInstance(this.presetType, this.presetMin, this.presetMax);
	}

	// Token: 0x06004505 RID: 17669 RVA: 0x00185498 File Offset: 0x00183698
	private ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration CreateRandomInstance(HashedString typeId, float min, float max)
	{
		int globalWorldSeed = SaveLoader.Instance.clusterDetailSave.globalWorldSeed;
		ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
		Vector3 position = ClusterGrid.Instance.GetPosition(component);
		KRandom krandom = new KRandom(globalWorldSeed + (int)position.x + (int)position.y);
		return new ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration
		{
			typeId = typeId,
			rechargeRoll = this.Roll(krandom, min, max)
		};
	}

	// Token: 0x06004506 RID: 17670 RVA: 0x001854F8 File Offset: 0x001836F8
	private float Roll(KRandom randomSource, float min, float max)
	{
		return (float)(randomSource.NextDouble() * (double)(max - min)) + min;
	}

	// Token: 0x04002E0B RID: 11787
	private static List<ArtifactPOIConfigurator.ArtifactPOIType> _poiTypes;

	// Token: 0x04002E0C RID: 11788
	public static ArtifactPOIConfigurator.ArtifactPOIType defaultArtifactPoiType = new ArtifactPOIConfigurator.ArtifactPOIType("HarvestablePOIArtifacts", null, false, 30000f, 60000f, "EXPANSION1_ID");

	// Token: 0x04002E0D RID: 11789
	public HashedString presetType;

	// Token: 0x04002E0E RID: 11790
	public float presetMin;

	// Token: 0x04002E0F RID: 11791
	public float presetMax = 1f;

	// Token: 0x0200171D RID: 5917
	public class ArtifactPOIType
	{
		// Token: 0x0600899E RID: 35230 RVA: 0x002F96A0 File Offset: 0x002F78A0
		public ArtifactPOIType(string id, string harvestableArtifactID = null, bool destroyOnHarvest = false, float poiRechargeTimeMin = 30000f, float poiRechargeTimeMax = 60000f, string dlcID = "EXPANSION1_ID")
		{
			this.id = id;
			this.idHash = id;
			this.harvestableArtifactID = harvestableArtifactID;
			this.destroyOnHarvest = destroyOnHarvest;
			this.poiRechargeTimeMin = poiRechargeTimeMin;
			this.poiRechargeTimeMax = poiRechargeTimeMax;
			this.dlcID = dlcID;
			if (ArtifactPOIConfigurator._poiTypes == null)
			{
				ArtifactPOIConfigurator._poiTypes = new List<ArtifactPOIConfigurator.ArtifactPOIType>();
			}
			ArtifactPOIConfigurator._poiTypes.Add(this);
		}

		// Token: 0x04006C03 RID: 27651
		public string id;

		// Token: 0x04006C04 RID: 27652
		public HashedString idHash;

		// Token: 0x04006C05 RID: 27653
		public string harvestableArtifactID;

		// Token: 0x04006C06 RID: 27654
		public bool destroyOnHarvest;

		// Token: 0x04006C07 RID: 27655
		public float poiRechargeTimeMin;

		// Token: 0x04006C08 RID: 27656
		public float poiRechargeTimeMax;

		// Token: 0x04006C09 RID: 27657
		public string dlcID;

		// Token: 0x04006C0A RID: 27658
		public List<string> orbitalObject = new List<string> { Db.Get().OrbitalTypeCategories.gravitas.Id };
	}

	// Token: 0x0200171E RID: 5918
	[Serializable]
	public class ArtifactPOIInstanceConfiguration
	{
		// Token: 0x0600899F RID: 35231 RVA: 0x002F9730 File Offset: 0x002F7930
		private void Init()
		{
			if (this.didInit)
			{
				return;
			}
			this.didInit = true;
			this.poiRechargeTime = MathUtil.ReRange(this.rechargeRoll, 0f, 1f, this.poiType.poiRechargeTimeMin, this.poiType.poiRechargeTimeMax);
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060089A0 RID: 35232 RVA: 0x002F977E File Offset: 0x002F797E
		public ArtifactPOIConfigurator.ArtifactPOIType poiType
		{
			get
			{
				return ArtifactPOIConfigurator.FindType(this.typeId);
			}
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x002F978B File Offset: 0x002F798B
		public bool DestroyOnHarvest()
		{
			this.Init();
			return this.poiType.destroyOnHarvest;
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x002F979E File Offset: 0x002F799E
		public string GetArtifactID()
		{
			this.Init();
			return this.poiType.harvestableArtifactID;
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x002F97B1 File Offset: 0x002F79B1
		public float GetRechargeTime()
		{
			this.Init();
			return this.poiRechargeTime;
		}

		// Token: 0x04006C0B RID: 27659
		public HashedString typeId;

		// Token: 0x04006C0C RID: 27660
		private bool didInit;

		// Token: 0x04006C0D RID: 27661
		public float rechargeRoll;

		// Token: 0x04006C0E RID: 27662
		private float poiRechargeTime;
	}
}
