using System;
using System.Collections.Generic;
using Klei.AI;
using Klei.CustomSettings;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000866 RID: 2150
[SkipSaveFileSerialization]
public class QualityOfLifeNeed : Need, ISim4000ms
{
	// Token: 0x06003DB4 RID: 15796 RVA: 0x00158C94 File Offset: 0x00156E94
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.breakBlocks = new List<bool>(24);
		Attributes attributes = base.gameObject.GetAttributes();
		this.expectationAttribute = attributes.Add(Db.Get().Attributes.QualityOfLifeExpectation);
		base.Name = DUPLICANTS.NEEDS.QUALITYOFLIFE.NAME;
		base.ExpectationTooltip = string.Format(DUPLICANTS.NEEDS.QUALITYOFLIFE.EXPECTATION_TOOLTIP, Db.Get().Attributes.QualityOfLifeExpectation.Lookup(this).GetTotalValue());
		this.stressBonus = new Need.ModifierType
		{
			modifier = new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0f, DUPLICANTS.NEEDS.QUALITYOFLIFE.GOOD_MODIFIER, false, false, false)
		};
		this.stressNeutral = new Need.ModifierType
		{
			modifier = new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, -0.008333334f, DUPLICANTS.NEEDS.QUALITYOFLIFE.NEUTRAL_MODIFIER, false, false, true)
		};
		this.stressPenalty = new Need.ModifierType
		{
			modifier = new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0f, DUPLICANTS.NEEDS.QUALITYOFLIFE.BAD_MODIFIER, false, false, false),
			statusItem = Db.Get().DuplicantStatusItems.PoorQualityOfLife
		};
		this.qolAttribute = Db.Get().Attributes.QualityOfLife.Lookup(base.gameObject);
	}

	// Token: 0x06003DB5 RID: 15797 RVA: 0x00158E18 File Offset: 0x00157018
	protected override void OnSpawn()
	{
		base.OnSpawn();
		while (this.breakBlocks.Count < 24)
		{
			this.breakBlocks.Add(false);
		}
		while (this.breakBlocks.Count > 24)
		{
			this.breakBlocks.RemoveAt(this.breakBlocks.Count - 1);
		}
		base.Subscribe<QualityOfLifeNeed>(1714332666, QualityOfLifeNeed.OnScheduleBlocksTickDelegate);
	}

	// Token: 0x06003DB6 RID: 15798 RVA: 0x00158E84 File Offset: 0x00157084
	public void Sim4000ms(float dt)
	{
		if (this.skipUpdate)
		{
			return;
		}
		float num = 0.004166667f;
		float num2 = 0.041666668f;
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Morale);
		if (currentQualitySetting.id == "Disabled")
		{
			base.SetModifier(null);
			return;
		}
		if (currentQualitySetting.id == "Easy")
		{
			num = 0.0033333334f;
			num2 = 0.016666668f;
		}
		else if (currentQualitySetting.id == "Hard")
		{
			num = 0.008333334f;
			num2 = 0.05f;
		}
		else if (currentQualitySetting.id == "VeryHard")
		{
			num = 0.016666668f;
			num2 = 0.083333336f;
		}
		float totalValue = this.qolAttribute.GetTotalValue();
		float totalValue2 = this.expectationAttribute.GetTotalValue();
		float num3 = totalValue2 - totalValue;
		if (totalValue < totalValue2)
		{
			this.stressPenalty.modifier.SetValue(Mathf.Min(num3 * num, num2));
			base.SetModifier(this.stressPenalty);
			return;
		}
		if (totalValue > totalValue2)
		{
			this.stressBonus.modifier.SetValue(Mathf.Max(-num3 * -0.016666668f, -0.033333335f));
			base.SetModifier(this.stressBonus);
			return;
		}
		base.SetModifier(this.stressNeutral);
	}

	// Token: 0x06003DB7 RID: 15799 RVA: 0x00158FBC File Offset: 0x001571BC
	private void OnScheduleBlocksTick(object data)
	{
		Schedule schedule = (Schedule)data;
		ScheduleBlock block = schedule.GetBlock(Schedule.GetLastBlockIdx());
		ScheduleBlock block2 = schedule.GetBlock(Schedule.GetBlockIdx());
		bool flag = block.IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		bool flag2 = block2.IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		this.breakBlocks[Schedule.GetLastBlockIdx()] = flag;
		if (flag && !flag2)
		{
			int num = 0;
			using (List<bool>.Enumerator enumerator = this.breakBlocks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current)
					{
						num++;
					}
				}
			}
			this.ApplyBreakBonus(num);
		}
	}

	// Token: 0x06003DB8 RID: 15800 RVA: 0x00159078 File Offset: 0x00157278
	private void ApplyBreakBonus(int numBlocks)
	{
		string breakBonus = QualityOfLifeNeed.GetBreakBonus(numBlocks);
		if (breakBonus != null)
		{
			base.GetComponent<Effects>().Add(breakBonus, true);
		}
	}

	// Token: 0x06003DB9 RID: 15801 RVA: 0x001590A0 File Offset: 0x001572A0
	public static string GetBreakBonus(int numBlocks)
	{
		int num = numBlocks - 1;
		if (num >= QualityOfLifeNeed.breakLengthEffects.Count)
		{
			return QualityOfLifeNeed.breakLengthEffects[QualityOfLifeNeed.breakLengthEffects.Count - 1];
		}
		if (num >= 0)
		{
			return QualityOfLifeNeed.breakLengthEffects[num];
		}
		return null;
	}

	// Token: 0x04002872 RID: 10354
	private AttributeInstance qolAttribute;

	// Token: 0x04002873 RID: 10355
	public bool skipUpdate;

	// Token: 0x04002874 RID: 10356
	[Serialize]
	private List<bool> breakBlocks;

	// Token: 0x04002875 RID: 10357
	private static readonly EventSystem.IntraObjectHandler<QualityOfLifeNeed> OnScheduleBlocksTickDelegate = new EventSystem.IntraObjectHandler<QualityOfLifeNeed>(delegate(QualityOfLifeNeed component, object data)
	{
		component.OnScheduleBlocksTick(data);
	});

	// Token: 0x04002876 RID: 10358
	private static List<string> breakLengthEffects = new List<string> { "Break1", "Break2", "Break3", "Break4", "Break5" };
}
