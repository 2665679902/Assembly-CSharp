﻿using System;
using Klei;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;

// Token: 0x020006A8 RID: 1704
public class ConduitDiseaseManager : KCompactedVector<ConduitDiseaseManager.Data>
{
	// Token: 0x06002E3E RID: 11838 RVA: 0x000F3F2C File Offset: 0x000F212C
	private static ElemGrowthInfo GetGrowthInfo(byte disease_idx, ushort elem_idx)
	{
		ElemGrowthInfo elemGrowthInfo;
		if (disease_idx != 255)
		{
			elemGrowthInfo = Db.Get().Diseases[(int)disease_idx].elemGrowthInfo[(int)elem_idx];
		}
		else
		{
			elemGrowthInfo = Disease.DEFAULT_GROWTH_INFO;
		}
		return elemGrowthInfo;
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x000F3F66 File Offset: 0x000F2166
	public ConduitDiseaseManager(ConduitTemperatureManager temperature_manager)
		: base(0)
	{
		this.temperatureManager = temperature_manager;
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x000F3F78 File Offset: 0x000F2178
	public HandleVector<int>.Handle Allocate(HandleVector<int>.Handle temperature_handle, ref ConduitFlow.ConduitContents contents)
	{
		ushort elementIndex = ElementLoader.GetElementIndex(contents.element);
		ConduitDiseaseManager.Data data = new ConduitDiseaseManager.Data(temperature_handle, elementIndex, contents.mass, contents.diseaseIdx, contents.diseaseCount);
		return base.Allocate(data);
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x000F3FB4 File Offset: 0x000F21B4
	public void SetData(HandleVector<int>.Handle handle, ref ConduitFlow.ConduitContents contents)
	{
		ConduitDiseaseManager.Data data = base.GetData(handle);
		data.diseaseCount = contents.diseaseCount;
		if (contents.diseaseIdx != data.diseaseIdx)
		{
			data.diseaseIdx = contents.diseaseIdx;
			ushort elementIndex = ElementLoader.GetElementIndex(contents.element);
			data.growthInfo = ConduitDiseaseManager.GetGrowthInfo(contents.diseaseIdx, elementIndex);
		}
		base.SetData(handle, data);
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x000F4018 File Offset: 0x000F2218
	public void Sim200ms(float dt)
	{
		using (new KProfiler.Region("ConduitDiseaseManager.SimUpdate", null))
		{
			for (int i = 0; i < this.data.Count; i++)
			{
				ConduitDiseaseManager.Data data = this.data[i];
				if (data.diseaseIdx != 255)
				{
					float num = data.accumulatedError;
					num += data.growthInfo.CalculateDiseaseCountDelta(data.diseaseCount, data.mass, dt);
					Disease disease = Db.Get().Diseases[(int)data.diseaseIdx];
					float num2 = Disease.HalfLifeToGrowthRate(Disease.CalculateRangeHalfLife(this.temperatureManager.GetTemperature(data.temperatureHandle), ref disease.temperatureRange, ref disease.temperatureHalfLives), dt);
					num += (float)data.diseaseCount * num2 - (float)data.diseaseCount;
					int num3 = (int)num;
					data.accumulatedError = num - (float)num3;
					data.diseaseCount += num3;
					if (data.diseaseCount <= 0)
					{
						data.diseaseCount = 0;
						data.diseaseIdx = byte.MaxValue;
						data.accumulatedError = 0f;
					}
					this.data[i] = data;
				}
			}
		}
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x000F4168 File Offset: 0x000F2368
	public void ModifyDiseaseCount(HandleVector<int>.Handle h, int disease_count_delta)
	{
		ConduitDiseaseManager.Data data = base.GetData(h);
		data.diseaseCount = Math.Max(0, data.diseaseCount + disease_count_delta);
		if (data.diseaseCount == 0)
		{
			data.diseaseIdx = byte.MaxValue;
		}
		base.SetData(h, data);
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x000F41B0 File Offset: 0x000F23B0
	public void AddDisease(HandleVector<int>.Handle h, byte disease_idx, int disease_count)
	{
		ConduitDiseaseManager.Data data = base.GetData(h);
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(disease_idx, disease_count, data.diseaseIdx, data.diseaseCount);
		data.diseaseIdx = diseaseInfo.idx;
		data.diseaseCount = diseaseInfo.count;
		base.SetData(h, data);
	}

	// Token: 0x04001BED RID: 7149
	private ConduitTemperatureManager temperatureManager;

	// Token: 0x02001374 RID: 4980
	public struct Data
	{
		// Token: 0x06007DF7 RID: 32247 RVA: 0x002D7304 File Offset: 0x002D5504
		public Data(HandleVector<int>.Handle temperature_handle, ushort elem_idx, float mass, byte disease_idx, int disease_count)
		{
			this.diseaseIdx = disease_idx;
			this.elemIdx = elem_idx;
			this.mass = mass;
			this.diseaseCount = disease_count;
			this.accumulatedError = 0f;
			this.temperatureHandle = temperature_handle;
			this.growthInfo = ConduitDiseaseManager.GetGrowthInfo(disease_idx, elem_idx);
		}

		// Token: 0x04006095 RID: 24725
		public byte diseaseIdx;

		// Token: 0x04006096 RID: 24726
		public ushort elemIdx;

		// Token: 0x04006097 RID: 24727
		public int diseaseCount;

		// Token: 0x04006098 RID: 24728
		public float accumulatedError;

		// Token: 0x04006099 RID: 24729
		public float mass;

		// Token: 0x0400609A RID: 24730
		public HandleVector<int>.Handle temperatureHandle;

		// Token: 0x0400609B RID: 24731
		public ElemGrowthInfo growthInfo;
	}
}
