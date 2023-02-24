﻿using System;
using System.Diagnostics;
using Klei.AI;
using UnityEngine;

namespace Klei
{
	// Token: 0x02000D59 RID: 3417
	public static class SimUtil
	{
		// Token: 0x06006859 RID: 26713 RVA: 0x0028A684 File Offset: 0x00288884
		public static float CalculateEnergyFlow(float source_temp, float source_thermal_conductivity, float dest_temp, float dest_thermal_conductivity, float surface_area = 1f, float thickness = 1f)
		{
			return (source_temp - dest_temp) * Math.Min(source_thermal_conductivity, dest_thermal_conductivity) * (surface_area / thickness);
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x0028A698 File Offset: 0x00288898
		public static float CalculateEnergyFlow(int cell, float dest_temp, float dest_specific_heat_capacity, float dest_thermal_conductivity, float surface_area = 1f, float thickness = 1f)
		{
			if (Grid.Mass[cell] <= 0f)
			{
				return 0f;
			}
			Element element = Grid.Element[cell];
			if (element.IsVacuum)
			{
				return 0f;
			}
			float num = Grid.Temperature[cell];
			float thermalConductivity = element.thermalConductivity;
			return SimUtil.CalculateEnergyFlow(num, thermalConductivity, dest_temp, dest_thermal_conductivity, surface_area, thickness) * 0.001f;
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x0028A6F7 File Offset: 0x002888F7
		public static float ClampEnergyTransfer(float dt, float source_temp, float source_mass, float source_specific_heat_capacity, float dest_temp, float dest_mass, float dest_specific_heat_capacity, float max_watts_transferred)
		{
			return SimUtil.ClampEnergyTransfer(dt, source_temp, source_mass * source_specific_heat_capacity, dest_temp, dest_mass * dest_specific_heat_capacity, max_watts_transferred);
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x0028A70C File Offset: 0x0028890C
		public static float ClampEnergyTransfer(float dt, float source_temp, float source_heat_capacity, float dest_temp, float dest_heat_capacity, float max_watts_transferred)
		{
			float num = max_watts_transferred * dt / 1000f;
			SimUtil.CheckValidValue(num);
			float num2 = Math.Min(source_temp, dest_temp);
			float num3 = Math.Max(source_temp, dest_temp);
			float num4 = source_temp - num / source_heat_capacity;
			float num5 = dest_temp + num / dest_heat_capacity;
			SimUtil.CheckValidValue(num4);
			SimUtil.CheckValidValue(num5);
			num4 = Mathf.Clamp(num4, num2, num3);
			float num6 = Mathf.Clamp(num5, num2, num3);
			float num7 = Math.Abs(num4 - source_temp);
			float num8 = Math.Abs(num6 - dest_temp);
			float num9 = num7 * source_heat_capacity;
			float num10 = num8 * dest_heat_capacity;
			float num11 = ((max_watts_transferred < 0f) ? (-1f) : 1f);
			float num12 = Math.Min(num9, num10) * num11;
			SimUtil.CheckValidValue(num12);
			return num12;
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x0028A7A7 File Offset: 0x002889A7
		private static float GetMassAreaScale(Element element)
		{
			if (!element.IsGas)
			{
				return 0.01f;
			}
			return 10f;
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x0028A7BC File Offset: 0x002889BC
		public static float CalculateEnergyFlowCreatures(int cell, float creature_temperature, float creature_shc, float creature_thermal_conductivity, float creature_surface_area = 1f, float creature_surface_thickness = 1f)
		{
			return SimUtil.CalculateEnergyFlow(cell, creature_temperature, creature_shc, creature_thermal_conductivity, creature_surface_area, creature_surface_thickness);
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x0028A7CB File Offset: 0x002889CB
		public static float EnergyFlowToTemperatureDelta(float kilojoules, float specific_heat_capacity, float mass)
		{
			if (kilojoules * specific_heat_capacity * mass == 0f)
			{
				return 0f;
			}
			return kilojoules / (specific_heat_capacity * mass);
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x0028A7E4 File Offset: 0x002889E4
		public static float CalculateFinalTemperature(float mass1, float temp1, float mass2, float temp2)
		{
			float num = mass1 + mass2;
			if (num == 0f)
			{
				return 0f;
			}
			float num2 = mass1 * temp1;
			float num3 = mass2 * temp2;
			float num4 = (num2 + num3) / num;
			float num5;
			float num6;
			if (temp1 > temp2)
			{
				num5 = temp2;
				num6 = temp1;
			}
			else
			{
				num5 = temp1;
				num6 = temp2;
			}
			return Math.Max(num5, Math.Min(num6, num4));
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x0028A82F File Offset: 0x00288A2F
		[Conditional("STRICT_CHECKING")]
		public static void CheckValidValue(float value)
		{
			if (!float.IsNaN(value))
			{
				float.IsInfinity(value);
			}
		}

		// Token: 0x06006862 RID: 26722 RVA: 0x0028A840 File Offset: 0x00288A40
		public static SimUtil.DiseaseInfo CalculateFinalDiseaseInfo(SimUtil.DiseaseInfo a, SimUtil.DiseaseInfo b)
		{
			return SimUtil.CalculateFinalDiseaseInfo(a.idx, a.count, b.idx, b.count);
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x0028A860 File Offset: 0x00288A60
		public static SimUtil.DiseaseInfo CalculateFinalDiseaseInfo(byte src1_idx, int src1_count, byte src2_idx, int src2_count)
		{
			SimUtil.DiseaseInfo diseaseInfo = default(SimUtil.DiseaseInfo);
			if (src1_idx == src2_idx)
			{
				diseaseInfo.idx = src1_idx;
				diseaseInfo.count = src1_count + src2_count;
			}
			else if (src1_idx == 255)
			{
				diseaseInfo.idx = src2_idx;
				diseaseInfo.count = src2_count;
			}
			else if (src2_idx == 255)
			{
				diseaseInfo.idx = src1_idx;
				diseaseInfo.count = src1_count;
			}
			else
			{
				Disease disease = Db.Get().Diseases[(int)src1_idx];
				Disease disease2 = Db.Get().Diseases[(int)src2_idx];
				float num = disease.strength * (float)src1_count;
				float num2 = disease2.strength * (float)src2_count;
				if (num > num2)
				{
					int num3 = (int)((float)src2_count - num / num2 * (float)src1_count);
					if (num3 < 0)
					{
						diseaseInfo.idx = src1_idx;
						diseaseInfo.count = -num3;
					}
					else
					{
						diseaseInfo.idx = src2_idx;
						diseaseInfo.count = num3;
					}
				}
				else
				{
					int num4 = (int)((float)src1_count - num2 / num * (float)src2_count);
					if (num4 < 0)
					{
						diseaseInfo.idx = src2_idx;
						diseaseInfo.count = -num4;
					}
					else
					{
						diseaseInfo.idx = src1_idx;
						diseaseInfo.count = num4;
					}
				}
			}
			if (diseaseInfo.count <= 0)
			{
				diseaseInfo.count = 0;
				diseaseInfo.idx = byte.MaxValue;
			}
			return diseaseInfo;
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x0028A990 File Offset: 0x00288B90
		public static byte DiseaseCountToAlpha254(int count)
		{
			float num = Mathf.Log((float)count, 10f);
			num /= SimUtil.MAX_DISEASE_LOG_RANGE;
			num = Math.Max(0f, Math.Min(1f, num));
			num -= SimUtil.MIN_DISEASE_LOG_SUBTRACTION / SimUtil.MAX_DISEASE_LOG_RANGE;
			num = Math.Max(0f, num);
			num /= 1f - SimUtil.MIN_DISEASE_LOG_SUBTRACTION / SimUtil.MAX_DISEASE_LOG_RANGE;
			return (byte)(num * 254f);
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x0028A9FE File Offset: 0x00288BFE
		public static float DiseaseCountToAlpha(int count)
		{
			return (float)SimUtil.DiseaseCountToAlpha254(count) / 255f;
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x0028AA10 File Offset: 0x00288C10
		public static SimUtil.DiseaseInfo GetPercentOfDisease(PrimaryElement pe, float percent)
		{
			return new SimUtil.DiseaseInfo
			{
				idx = pe.DiseaseIdx,
				count = (int)((float)pe.DiseaseCount * percent)
			};
		}

		// Token: 0x04004EA5 RID: 20133
		private const int MAX_ALPHA_COUNT = 1000000;

		// Token: 0x04004EA6 RID: 20134
		private static float MIN_DISEASE_LOG_SUBTRACTION = 2f;

		// Token: 0x04004EA7 RID: 20135
		private static float MAX_DISEASE_LOG_RANGE = 6f;

		// Token: 0x02001E3E RID: 7742
		public struct DiseaseInfo
		{
			// Token: 0x04008823 RID: 34851
			public byte idx;

			// Token: 0x04008824 RID: 34852
			public int count;

			// Token: 0x04008825 RID: 34853
			public static readonly SimUtil.DiseaseInfo Invalid = new SimUtil.DiseaseInfo
			{
				idx = byte.MaxValue,
				count = 0
			};
		}
	}
}
