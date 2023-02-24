using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x02000793 RID: 1939
[AddComponentMenu("KMonoBehaviour/scripts/GeyserConfigurator")]
public class GeyserConfigurator : KMonoBehaviour
{
	// Token: 0x0600365F RID: 13919 RVA: 0x0012D9B4 File Offset: 0x0012BBB4
	public static GeyserConfigurator.GeyserType FindType(HashedString typeId)
	{
		GeyserConfigurator.GeyserType geyserType = null;
		if (typeId != HashedString.Invalid)
		{
			geyserType = GeyserConfigurator.geyserTypes.Find((GeyserConfigurator.GeyserType t) => t.id == typeId);
		}
		if (geyserType == null)
		{
			global::Debug.LogError(string.Format("Tried finding a geyser with id {0} but it doesn't exist!", typeId.ToString()));
		}
		return geyserType;
	}

	// Token: 0x06003660 RID: 13920 RVA: 0x0012DA1D File Offset: 0x0012BC1D
	public GeyserConfigurator.GeyserInstanceConfiguration MakeConfiguration()
	{
		return this.CreateRandomInstance(this.presetType, this.presetMin, this.presetMax);
	}

	// Token: 0x06003661 RID: 13921 RVA: 0x0012DA38 File Offset: 0x0012BC38
	private GeyserConfigurator.GeyserInstanceConfiguration CreateRandomInstance(HashedString typeId, float min, float max)
	{
		KRandom krandom = new KRandom(SaveLoader.Instance.clusterDetailSave.globalWorldSeed + (int)base.transform.GetPosition().x + (int)base.transform.GetPosition().y);
		return new GeyserConfigurator.GeyserInstanceConfiguration
		{
			typeId = typeId,
			rateRoll = this.Roll(krandom, min, max),
			iterationLengthRoll = this.Roll(krandom, 0f, 1f),
			iterationPercentRoll = this.Roll(krandom, min, max),
			yearLengthRoll = this.Roll(krandom, 0f, 1f),
			yearPercentRoll = this.Roll(krandom, min, max)
		};
	}

	// Token: 0x06003662 RID: 13922 RVA: 0x0012DAE5 File Offset: 0x0012BCE5
	private float Roll(KRandom randomSource, float min, float max)
	{
		return (float)(randomSource.NextDouble() * (double)(max - min)) + min;
	}

	// Token: 0x04002440 RID: 9280
	private static List<GeyserConfigurator.GeyserType> geyserTypes;

	// Token: 0x04002441 RID: 9281
	public HashedString presetType;

	// Token: 0x04002442 RID: 9282
	public float presetMin;

	// Token: 0x04002443 RID: 9283
	public float presetMax = 1f;

	// Token: 0x020014CB RID: 5323
	public enum GeyserShape
	{
		// Token: 0x040064CF RID: 25807
		Gas,
		// Token: 0x040064D0 RID: 25808
		Liquid,
		// Token: 0x040064D1 RID: 25809
		Molten
	}

	// Token: 0x020014CC RID: 5324
	public class GeyserType
	{
		// Token: 0x060081F0 RID: 33264 RVA: 0x002E3D28 File Offset: 0x002E1F28
		public GeyserType(string id, SimHashes element, GeyserConfigurator.GeyserShape shape, float temperature, float minRatePerCycle, float maxRatePerCycle, float maxPressure, float minIterationLength = 60f, float maxIterationLength = 1140f, float minIterationPercent = 0.1f, float maxIterationPercent = 0.9f, float minYearLength = 15000f, float maxYearLength = 135000f, float minYearPercent = 0.4f, float maxYearPercent = 0.8f, float geyserTemperature = 372.15f, string DlcID = "")
		{
			this.id = id;
			this.idHash = id;
			this.element = element;
			this.shape = shape;
			this.temperature = temperature;
			this.minRatePerCycle = minRatePerCycle;
			this.maxRatePerCycle = maxRatePerCycle;
			this.maxPressure = maxPressure;
			this.minIterationLength = minIterationLength;
			this.maxIterationLength = maxIterationLength;
			this.minIterationPercent = minIterationPercent;
			this.maxIterationPercent = maxIterationPercent;
			this.minYearLength = minYearLength;
			this.maxYearLength = maxYearLength;
			this.minYearPercent = minYearPercent;
			this.maxYearPercent = maxYearPercent;
			this.DlcID = DlcID;
			this.geyserTemperature = geyserTemperature;
			if (GeyserConfigurator.geyserTypes == null)
			{
				GeyserConfigurator.geyserTypes = new List<GeyserConfigurator.GeyserType>();
			}
			GeyserConfigurator.geyserTypes.Add(this);
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x002E3DF3 File Offset: 0x002E1FF3
		public GeyserConfigurator.GeyserType AddDisease(SimUtil.DiseaseInfo diseaseInfo)
		{
			this.diseaseInfo = diseaseInfo;
			return this;
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x002E3E00 File Offset: 0x002E2000
		public GeyserType()
		{
			this.id = "Blank";
			this.element = SimHashes.Void;
			this.temperature = 0f;
			this.minRatePerCycle = 0f;
			this.maxRatePerCycle = 0f;
			this.maxPressure = 0f;
			this.minIterationLength = 0f;
			this.maxIterationLength = 0f;
			this.minIterationPercent = 0f;
			this.maxIterationPercent = 0f;
			this.minYearLength = 0f;
			this.maxYearLength = 0f;
			this.minYearPercent = 0f;
			this.maxYearPercent = 0f;
			this.geyserTemperature = 0f;
			this.DlcID = "";
		}

		// Token: 0x040064D2 RID: 25810
		public string id;

		// Token: 0x040064D3 RID: 25811
		public HashedString idHash;

		// Token: 0x040064D4 RID: 25812
		public SimHashes element;

		// Token: 0x040064D5 RID: 25813
		public GeyserConfigurator.GeyserShape shape;

		// Token: 0x040064D6 RID: 25814
		public float temperature;

		// Token: 0x040064D7 RID: 25815
		public float minRatePerCycle;

		// Token: 0x040064D8 RID: 25816
		public float maxRatePerCycle;

		// Token: 0x040064D9 RID: 25817
		public float maxPressure;

		// Token: 0x040064DA RID: 25818
		public SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;

		// Token: 0x040064DB RID: 25819
		public float minIterationLength;

		// Token: 0x040064DC RID: 25820
		public float maxIterationLength;

		// Token: 0x040064DD RID: 25821
		public float minIterationPercent;

		// Token: 0x040064DE RID: 25822
		public float maxIterationPercent;

		// Token: 0x040064DF RID: 25823
		public float minYearLength;

		// Token: 0x040064E0 RID: 25824
		public float maxYearLength;

		// Token: 0x040064E1 RID: 25825
		public float minYearPercent;

		// Token: 0x040064E2 RID: 25826
		public float maxYearPercent;

		// Token: 0x040064E3 RID: 25827
		public float geyserTemperature;

		// Token: 0x040064E4 RID: 25828
		public string DlcID;

		// Token: 0x040064E5 RID: 25829
		public const string BLANK_ID = "Blank";

		// Token: 0x040064E6 RID: 25830
		public const SimHashes BLANK_ELEMENT = SimHashes.Void;

		// Token: 0x040064E7 RID: 25831
		public const string BLANK_DLCID = "";
	}

	// Token: 0x020014CD RID: 5325
	[Serializable]
	public class GeyserInstanceConfiguration
	{
		// Token: 0x060081F3 RID: 33267 RVA: 0x002E3ECE File Offset: 0x002E20CE
		public Geyser.GeyserModification GetModifier()
		{
			return this.modifier;
		}

		// Token: 0x060081F4 RID: 33268 RVA: 0x002E3ED8 File Offset: 0x002E20D8
		public void Init(bool reinit = false)
		{
			if (this.didInit && !reinit)
			{
				return;
			}
			this.didInit = true;
			this.scaledRate = this.Resample(this.rateRoll, this.geyserType.minRatePerCycle, this.geyserType.maxRatePerCycle);
			this.scaledIterationLength = this.Resample(this.iterationLengthRoll, this.geyserType.minIterationLength, this.geyserType.maxIterationLength);
			this.scaledIterationPercent = this.Resample(this.iterationPercentRoll, this.geyserType.minIterationPercent, this.geyserType.maxIterationPercent);
			this.scaledYearLength = this.Resample(this.yearLengthRoll, this.geyserType.minYearLength, this.geyserType.maxYearLength);
			this.scaledYearPercent = this.Resample(this.yearPercentRoll, this.geyserType.minYearPercent, this.geyserType.maxYearPercent);
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x002E3FC0 File Offset: 0x002E21C0
		public void SetModifier(Geyser.GeyserModification modifier)
		{
			this.modifier = modifier;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060081F6 RID: 33270 RVA: 0x002E3FC9 File Offset: 0x002E21C9
		public GeyserConfigurator.GeyserType geyserType
		{
			get
			{
				return GeyserConfigurator.FindType(this.typeId);
			}
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x002E3FD8 File Offset: 0x002E21D8
		private float GetModifiedValue(float geyserVariable, float modifier, Geyser.ModificationMethod method)
		{
			float num = geyserVariable;
			if (method != Geyser.ModificationMethod.Values)
			{
				if (method == Geyser.ModificationMethod.Percentages)
				{
					num += geyserVariable * modifier;
				}
			}
			else
			{
				num += modifier;
			}
			return num;
		}

		// Token: 0x060081F8 RID: 33272 RVA: 0x002E3FFB File Offset: 0x002E21FB
		public float GetMaxPressure()
		{
			return this.GetModifiedValue(this.geyserType.maxPressure, this.modifier.maxPressureModifier, Geyser.maxPressureModificationMethod);
		}

		// Token: 0x060081F9 RID: 33273 RVA: 0x002E401E File Offset: 0x002E221E
		public float GetIterationLength()
		{
			this.Init(false);
			return this.GetModifiedValue(this.scaledIterationLength, this.modifier.iterationDurationModifier, Geyser.IterationDurationModificationMethod);
		}

		// Token: 0x060081FA RID: 33274 RVA: 0x002E4043 File Offset: 0x002E2243
		public float GetIterationPercent()
		{
			this.Init(false);
			return Mathf.Clamp(this.GetModifiedValue(this.scaledIterationPercent, this.modifier.iterationPercentageModifier, Geyser.IterationPercentageModificationMethod), 0f, 1f);
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x002E4077 File Offset: 0x002E2277
		public float GetOnDuration()
		{
			return this.GetIterationLength() * this.GetIterationPercent();
		}

		// Token: 0x060081FC RID: 33276 RVA: 0x002E4086 File Offset: 0x002E2286
		public float GetOffDuration()
		{
			return this.GetIterationLength() * (1f - this.GetIterationPercent());
		}

		// Token: 0x060081FD RID: 33277 RVA: 0x002E409B File Offset: 0x002E229B
		public float GetMassPerCycle()
		{
			this.Init(false);
			return this.GetModifiedValue(this.scaledRate, this.modifier.massPerCycleModifier, Geyser.massModificationMethod);
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x002E40C0 File Offset: 0x002E22C0
		public float GetEmitRate()
		{
			float num = 600f / this.GetIterationLength();
			return this.GetMassPerCycle() / num / this.GetOnDuration();
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x002E40E9 File Offset: 0x002E22E9
		public float GetYearLength()
		{
			this.Init(false);
			return this.GetModifiedValue(this.scaledYearLength, this.modifier.yearDurationModifier, Geyser.yearDurationModificationMethod);
		}

		// Token: 0x06008200 RID: 33280 RVA: 0x002E410E File Offset: 0x002E230E
		public float GetYearPercent()
		{
			this.Init(false);
			return Mathf.Clamp(this.GetModifiedValue(this.scaledYearPercent, this.modifier.yearPercentageModifier, Geyser.yearPercentageModificationMethod), 0f, 1f);
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x002E4142 File Offset: 0x002E2342
		public float GetYearOnDuration()
		{
			return this.GetYearLength() * this.GetYearPercent();
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x002E4151 File Offset: 0x002E2351
		public float GetYearOffDuration()
		{
			return this.GetYearLength() * (1f - this.GetYearPercent());
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x002E4166 File Offset: 0x002E2366
		public SimHashes GetElement()
		{
			if (!this.modifier.modifyElement || this.modifier.newElement == (SimHashes)0)
			{
				return this.geyserType.element;
			}
			return this.modifier.newElement;
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x002E4199 File Offset: 0x002E2399
		public float GetTemperature()
		{
			return this.GetModifiedValue(this.geyserType.temperature, this.modifier.temperatureModifier, Geyser.temperatureModificationMethod);
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x002E41BC File Offset: 0x002E23BC
		public byte GetDiseaseIdx()
		{
			return this.geyserType.diseaseInfo.idx;
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x002E41CE File Offset: 0x002E23CE
		public int GetDiseaseCount()
		{
			return this.geyserType.diseaseInfo.count;
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x002E41E0 File Offset: 0x002E23E0
		public float GetAverageEmission()
		{
			float num = this.GetEmitRate() * this.GetOnDuration();
			return this.GetYearOnDuration() / this.GetIterationLength() * num / this.GetYearLength();
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x002E4214 File Offset: 0x002E2414
		private float Resample(float t, float min, float max)
		{
			float num = 6f;
			float num2 = 0.002472623f;
			float num3 = t * (1f - num2 * 2f) + num2;
			return (-Mathf.Log(1f / num3 - 1f) + num) / (num * 2f) * (max - min) + min;
		}

		// Token: 0x040064E8 RID: 25832
		public HashedString typeId;

		// Token: 0x040064E9 RID: 25833
		public float rateRoll;

		// Token: 0x040064EA RID: 25834
		public float iterationLengthRoll;

		// Token: 0x040064EB RID: 25835
		public float iterationPercentRoll;

		// Token: 0x040064EC RID: 25836
		public float yearLengthRoll;

		// Token: 0x040064ED RID: 25837
		public float yearPercentRoll;

		// Token: 0x040064EE RID: 25838
		public float scaledRate;

		// Token: 0x040064EF RID: 25839
		public float scaledIterationLength;

		// Token: 0x040064F0 RID: 25840
		public float scaledIterationPercent;

		// Token: 0x040064F1 RID: 25841
		public float scaledYearLength;

		// Token: 0x040064F2 RID: 25842
		public float scaledYearPercent;

		// Token: 0x040064F3 RID: 25843
		private bool didInit;

		// Token: 0x040064F4 RID: 25844
		private Geyser.GeyserModification modifier;
	}
}
