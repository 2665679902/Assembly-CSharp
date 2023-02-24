using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000947 RID: 2375
[AddComponentMenu("KMonoBehaviour/scripts/HarvestablePOIConfigurator")]
public class HarvestablePOIConfigurator : KMonoBehaviour
{
	// Token: 0x0600462D RID: 17965 RVA: 0x0018B4DC File Offset: 0x001896DC
	public static HarvestablePOIConfigurator.HarvestablePOIType FindType(HashedString typeId)
	{
		HarvestablePOIConfigurator.HarvestablePOIType harvestablePOIType = null;
		if (typeId != HashedString.Invalid)
		{
			harvestablePOIType = HarvestablePOIConfigurator._poiTypes.Find((HarvestablePOIConfigurator.HarvestablePOIType t) => t.id == typeId);
		}
		if (harvestablePOIType == null)
		{
			global::Debug.LogError(string.Format("Tried finding a harvestable poi with id {0} but it doesn't exist!", typeId.ToString()));
		}
		return harvestablePOIType;
	}

	// Token: 0x0600462E RID: 17966 RVA: 0x0018B545 File Offset: 0x00189745
	public HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration MakeConfiguration()
	{
		return this.CreateRandomInstance(this.presetType, this.presetMin, this.presetMax);
	}

	// Token: 0x0600462F RID: 17967 RVA: 0x0018B560 File Offset: 0x00189760
	private HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration CreateRandomInstance(HashedString typeId, float min, float max)
	{
		int globalWorldSeed = SaveLoader.Instance.clusterDetailSave.globalWorldSeed;
		ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
		Vector3 position = ClusterGrid.Instance.GetPosition(component);
		KRandom krandom = new KRandom(globalWorldSeed + (int)position.x + (int)position.y);
		return new HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration
		{
			typeId = typeId,
			capacityRoll = this.Roll(krandom, min, max),
			rechargeRoll = this.Roll(krandom, min, max)
		};
	}

	// Token: 0x06004630 RID: 17968 RVA: 0x0018B5CF File Offset: 0x001897CF
	private float Roll(KRandom randomSource, float min, float max)
	{
		return (float)(randomSource.NextDouble() * (double)(max - min)) + min;
	}

	// Token: 0x04002E77 RID: 11895
	private static List<HarvestablePOIConfigurator.HarvestablePOIType> _poiTypes;

	// Token: 0x04002E78 RID: 11896
	public HashedString presetType;

	// Token: 0x04002E79 RID: 11897
	public float presetMin;

	// Token: 0x04002E7A RID: 11898
	public float presetMax = 1f;

	// Token: 0x02001737 RID: 5943
	public class HarvestablePOIType
	{
		// Token: 0x06008A05 RID: 35333 RVA: 0x002FA414 File Offset: 0x002F8614
		public HarvestablePOIType(string id, Dictionary<SimHashes, float> harvestableElements, float poiCapacityMin = 54000f, float poiCapacityMax = 81000f, float poiRechargeMin = 30000f, float poiRechargeMax = 60000f, bool canProvideArtifacts = true, List<string> orbitalObject = null, int maxNumOrbitingObjects = 20, string dlcID = "EXPANSION1_ID")
		{
			this.id = id;
			this.idHash = id;
			this.harvestableElements = harvestableElements;
			this.poiCapacityMin = poiCapacityMin;
			this.poiCapacityMax = poiCapacityMax;
			this.poiRechargeMin = poiRechargeMin;
			this.poiRechargeMax = poiRechargeMax;
			this.canProvideArtifacts = canProvideArtifacts;
			this.orbitalObject = orbitalObject;
			this.maxNumOrbitingObjects = maxNumOrbitingObjects;
			this.dlcID = dlcID;
			if (HarvestablePOIConfigurator._poiTypes == null)
			{
				HarvestablePOIConfigurator._poiTypes = new List<HarvestablePOIConfigurator.HarvestablePOIType>();
			}
			HarvestablePOIConfigurator._poiTypes.Add(this);
		}

		// Token: 0x04006C5C RID: 27740
		public string id;

		// Token: 0x04006C5D RID: 27741
		public HashedString idHash;

		// Token: 0x04006C5E RID: 27742
		public Dictionary<SimHashes, float> harvestableElements;

		// Token: 0x04006C5F RID: 27743
		public float poiCapacityMin;

		// Token: 0x04006C60 RID: 27744
		public float poiCapacityMax;

		// Token: 0x04006C61 RID: 27745
		public float poiRechargeMin;

		// Token: 0x04006C62 RID: 27746
		public float poiRechargeMax;

		// Token: 0x04006C63 RID: 27747
		public bool canProvideArtifacts;

		// Token: 0x04006C64 RID: 27748
		public string dlcID;

		// Token: 0x04006C65 RID: 27749
		public List<string> orbitalObject;

		// Token: 0x04006C66 RID: 27750
		public int maxNumOrbitingObjects;
	}

	// Token: 0x02001738 RID: 5944
	[Serializable]
	public class HarvestablePOIInstanceConfiguration
	{
		// Token: 0x06008A06 RID: 35334 RVA: 0x002FA49C File Offset: 0x002F869C
		private void Init()
		{
			if (this.didInit)
			{
				return;
			}
			this.didInit = true;
			this.poiTotalCapacity = MathUtil.ReRange(this.capacityRoll, 0f, 1f, this.poiType.poiCapacityMin, this.poiType.poiCapacityMax);
			this.poiRecharge = MathUtil.ReRange(this.rechargeRoll, 0f, 1f, this.poiType.poiRechargeMin, this.poiType.poiRechargeMax);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06008A07 RID: 35335 RVA: 0x002FA51B File Offset: 0x002F871B
		public HarvestablePOIConfigurator.HarvestablePOIType poiType
		{
			get
			{
				return HarvestablePOIConfigurator.FindType(this.typeId);
			}
		}

		// Token: 0x06008A08 RID: 35336 RVA: 0x002FA528 File Offset: 0x002F8728
		public Dictionary<SimHashes, float> GetElementsWithWeights()
		{
			this.Init();
			return this.poiType.harvestableElements;
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x002FA53B File Offset: 0x002F873B
		public bool CanProvideArtifacts()
		{
			this.Init();
			return this.poiType.canProvideArtifacts;
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x002FA54E File Offset: 0x002F874E
		public float GetMaxCapacity()
		{
			this.Init();
			return this.poiTotalCapacity;
		}

		// Token: 0x06008A0B RID: 35339 RVA: 0x002FA55C File Offset: 0x002F875C
		public float GetRechargeTime()
		{
			this.Init();
			return this.poiRecharge;
		}

		// Token: 0x04006C67 RID: 27751
		public HashedString typeId;

		// Token: 0x04006C68 RID: 27752
		private bool didInit;

		// Token: 0x04006C69 RID: 27753
		public float capacityRoll;

		// Token: 0x04006C6A RID: 27754
		public float rechargeRoll;

		// Token: 0x04006C6B RID: 27755
		private float poiTotalCapacity;

		// Token: 0x04006C6C RID: 27756
		private float poiRecharge;
	}
}
