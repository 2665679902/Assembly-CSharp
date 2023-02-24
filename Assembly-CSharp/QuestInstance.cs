using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020008A6 RID: 2214
[SerializationConfig(MemberSerialization.OptIn)]
public class QuestInstance : ISaveLoadable
{
	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x00164532 File Offset: 0x00162732
	public HashedString Id
	{
		get
		{
			return this.quest.IdHash;
		}
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x0016453F File Offset: 0x0016273F
	public string Name
	{
		get
		{
			return this.quest.Name;
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x0016454C File Offset: 0x0016274C
	public string CompletionText
	{
		get
		{
			return this.quest.CompletionText;
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x00164559 File Offset: 0x00162759
	public bool IsStarted
	{
		get
		{
			return this.currentState > Quest.State.NotStarted;
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x00164564 File Offset: 0x00162764
	public bool IsComplete
	{
		get
		{
			return this.currentState == Quest.State.Completed;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x0016456F File Offset: 0x0016276F
	// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x00164577 File Offset: 0x00162777
	public float CurrentProgress { get; private set; }

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x00164580 File Offset: 0x00162780
	public Quest.State CurrentState
	{
		get
		{
			return this.currentState;
		}
	}

	// Token: 0x06003FB9 RID: 16313 RVA: 0x00164588 File Offset: 0x00162788
	public QuestInstance(Quest quest)
	{
		this.quest = quest;
		this.criteriaStates = new Dictionary<int, QuestInstance.CriteriaState>(quest.Criteria.Length);
		for (int i = 0; i < quest.Criteria.Length; i++)
		{
			QuestCriteria questCriteria = quest.Criteria[i];
			QuestInstance.CriteriaState criteriaState = new QuestInstance.CriteriaState
			{
				Handle = i
			};
			if (questCriteria.TargetValues != null)
			{
				if ((questCriteria.EvaluationBehaviors & QuestCriteria.BehaviorFlags.TrackItems) == QuestCriteria.BehaviorFlags.TrackItems)
				{
					criteriaState.SatisfyingItems = new Tag[questCriteria.TargetValues.Length * questCriteria.RequiredCount];
				}
				if ((questCriteria.EvaluationBehaviors & QuestCriteria.BehaviorFlags.TrackValues) == QuestCriteria.BehaviorFlags.TrackValues)
				{
					criteriaState.CurrentValues = new float[questCriteria.TargetValues.Length * questCriteria.RequiredCount];
				}
			}
			this.criteriaStates[questCriteria.CriteriaId.GetHash()] = criteriaState;
		}
	}

	// Token: 0x06003FBA RID: 16314 RVA: 0x00164658 File Offset: 0x00162858
	public void Initialize(Quest quest)
	{
		this.quest = quest;
		this.UpdateQuestProgress(false);
	}

	// Token: 0x06003FBB RID: 16315 RVA: 0x00164668 File Offset: 0x00162868
	public bool HasCriteria(HashedString criteriaId)
	{
		return this.criteriaStates.ContainsKey(criteriaId.HashValue);
	}

	// Token: 0x06003FBC RID: 16316 RVA: 0x0016467C File Offset: 0x0016287C
	public bool HasBehavior(QuestCriteria.BehaviorFlags behavior)
	{
		bool flag = false;
		int num = 0;
		while (!flag && num < this.quest.Criteria.Length)
		{
			flag = (this.quest.Criteria[num].EvaluationBehaviors & behavior) > QuestCriteria.BehaviorFlags.None;
			num++;
		}
		return flag;
	}

	// Token: 0x06003FBD RID: 16317 RVA: 0x001646C0 File Offset: 0x001628C0
	public int GetTargetCount(HashedString criteriaId)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState))
		{
			return 0;
		}
		return this.quest.Criteria[criteriaState.Handle].RequiredCount;
	}

	// Token: 0x06003FBE RID: 16318 RVA: 0x001646FC File Offset: 0x001628FC
	public int GetCurrentCount(HashedString criteriaId)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState))
		{
			return 0;
		}
		return criteriaState.CurrentCount;
	}

	// Token: 0x06003FBF RID: 16319 RVA: 0x00164728 File Offset: 0x00162928
	public float GetCurrentValue(HashedString criteriaId, int valueHandle = 0)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState) || criteriaState.CurrentValues == null)
		{
			return float.NaN;
		}
		return criteriaState.CurrentValues[valueHandle];
	}

	// Token: 0x06003FC0 RID: 16320 RVA: 0x00164764 File Offset: 0x00162964
	public float GetTargetValue(HashedString criteriaId, int valueHandle = 0)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState))
		{
			return float.NaN;
		}
		if (this.quest.Criteria[criteriaState.Handle].TargetValues == null)
		{
			return float.NaN;
		}
		return this.quest.Criteria[criteriaState.Handle].TargetValues[valueHandle];
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x001647C8 File Offset: 0x001629C8
	public Tag GetSatisfyingItem(HashedString criteriaId, int valueHandle = 0)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState) || criteriaState.SatisfyingItems == null)
		{
			return default(Tag);
		}
		return criteriaState.SatisfyingItems[valueHandle];
	}

	// Token: 0x06003FC2 RID: 16322 RVA: 0x0016480C File Offset: 0x00162A0C
	public float GetAreaAverage(HashedString criteriaId)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState))
		{
			return float.NaN;
		}
		if (!QuestCriteria.HasBehavior(this.quest.Criteria[criteriaState.Handle].EvaluationBehaviors, (QuestCriteria.BehaviorFlags)5))
		{
			return float.NaN;
		}
		float num = 0f;
		for (int i = 0; i < criteriaState.CurrentValues.Length; i++)
		{
			num += criteriaState.CurrentValues[i];
		}
		return num / (float)criteriaState.CurrentValues.Length;
	}

	// Token: 0x06003FC3 RID: 16323 RVA: 0x0016488C File Offset: 0x00162A8C
	public bool IsItemRedundant(HashedString criteriaId, Tag item)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState) || criteriaState.SatisfyingItems == null)
		{
			return false;
		}
		bool flag = false;
		int num = 0;
		while (!flag && num < criteriaState.SatisfyingItems.Length)
		{
			flag = criteriaState.SatisfyingItems[num] == item;
			num++;
		}
		return flag;
	}

	// Token: 0x06003FC4 RID: 16324 RVA: 0x001648E8 File Offset: 0x00162AE8
	public bool IsCriteriaSatisfied(HashedString id)
	{
		QuestInstance.CriteriaState criteriaState;
		return this.criteriaStates.TryGetValue(id.HashValue, out criteriaState) && this.quest.Criteria[criteriaState.Handle].IsSatisfied(criteriaState.SatisfactionState, this.GetSatisfactionMask(criteriaState));
	}

	// Token: 0x06003FC5 RID: 16325 RVA: 0x00164934 File Offset: 0x00162B34
	public bool IsCriteriaSatisfied(Tag id)
	{
		QuestInstance.CriteriaState criteriaState;
		return this.criteriaStates.TryGetValue(id.GetHash(), out criteriaState) && this.quest.Criteria[criteriaState.Handle].IsSatisfied(criteriaState.SatisfactionState, this.GetSatisfactionMask(criteriaState));
	}

	// Token: 0x06003FC6 RID: 16326 RVA: 0x00164980 File Offset: 0x00162B80
	public void TrackAreaForCriteria(HashedString criteriaId, Extents area)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(criteriaId.HashValue, out criteriaState))
		{
			return;
		}
		int num = area.width * area.height;
		QuestCriteria questCriteria = this.quest.Criteria[criteriaState.Handle];
		global::Debug.Assert(num <= 32);
		if (QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackValues))
		{
			criteriaState.CurrentValues = new float[num];
		}
		if (QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackItems))
		{
			criteriaState.SatisfyingItems = new Tag[num];
		}
		this.criteriaStates[criteriaId.HashValue] = criteriaState;
	}

	// Token: 0x06003FC7 RID: 16327 RVA: 0x00164A1C File Offset: 0x00162C1C
	private uint GetSatisfactionMask(QuestInstance.CriteriaState state)
	{
		QuestCriteria questCriteria = this.quest.Criteria[state.Handle];
		if (QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackArea))
		{
			int num = 0;
			if (state.SatisfyingItems != null)
			{
				num = state.SatisfyingItems.Length;
			}
			else if (state.CurrentValues != null)
			{
				num = state.CurrentValues.Length;
			}
			return (uint)(Mathf.Pow(2f, (float)num) - 1f);
		}
		return questCriteria.GetSatisfactionMask();
	}

	// Token: 0x06003FC8 RID: 16328 RVA: 0x00164A8C File Offset: 0x00162C8C
	public int TrackProgress(Quest.ItemData data, out bool dataSatisfies, out bool itemIsRedundant)
	{
		dataSatisfies = false;
		itemIsRedundant = false;
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(data.CriteriaId.HashValue, out criteriaState))
		{
			return -1;
		}
		int valueHandle = data.ValueHandle;
		QuestCriteria questCriteria = this.quest.Criteria[criteriaState.Handle];
		dataSatisfies = this.DataSatisfiesCriteria(data, ref valueHandle);
		if (valueHandle == -1)
		{
			return valueHandle;
		}
		bool flag = QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.AllowsRegression);
		bool flag2 = QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackItems);
		Tag tag = (flag2 ? criteriaState.SatisfyingItems[valueHandle] : default(Tag));
		if (dataSatisfies)
		{
			itemIsRedundant = QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.UniqueItems) && this.IsItemRedundant(data.CriteriaId, data.SatisfyingItem);
			if (itemIsRedundant)
			{
				return valueHandle;
			}
			tag = data.SatisfyingItem;
			criteriaState.SatisfactionState |= questCriteria.GetValueMask(valueHandle);
		}
		else if (flag)
		{
			criteriaState.SatisfactionState &= ~questCriteria.GetValueMask(valueHandle);
		}
		if (QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackValues))
		{
			criteriaState.CurrentValues[valueHandle] = data.CurrentValue;
		}
		if (flag2)
		{
			criteriaState.SatisfyingItems[valueHandle] = tag;
		}
		bool flag3 = this.IsCriteriaSatisfied(data.CriteriaId);
		bool flag4 = questCriteria.IsSatisfied(criteriaState.SatisfactionState, this.GetSatisfactionMask(criteriaState));
		if (flag3 != flag4)
		{
			criteriaState.CurrentCount += (flag3 ? (-1) : 1);
			if (flag4 && criteriaState.CurrentCount < questCriteria.RequiredCount)
			{
				criteriaState.SatisfactionState = 0U;
			}
		}
		this.criteriaStates[data.CriteriaId.HashValue] = criteriaState;
		this.UpdateQuestProgress(true);
		return valueHandle;
	}

	// Token: 0x06003FC9 RID: 16329 RVA: 0x00164C28 File Offset: 0x00162E28
	public bool DataSatisfiesCriteria(Quest.ItemData data, ref int valueHandle)
	{
		QuestInstance.CriteriaState criteriaState;
		if (!this.criteriaStates.TryGetValue(data.CriteriaId.HashValue, out criteriaState))
		{
			return false;
		}
		QuestCriteria questCriteria = this.quest.Criteria[criteriaState.Handle];
		bool flag = questCriteria.AcceptedTags == null || (data.QualifyingTag.IsValid && questCriteria.AcceptedTags.Contains(data.QualifyingTag));
		if (flag && questCriteria.TargetValues == null)
		{
			valueHandle = 0;
		}
		if (!flag || valueHandle != -1)
		{
			return flag && questCriteria.ValueSatisfies(data.CurrentValue, valueHandle);
		}
		if (QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackArea))
		{
			valueHandle = data.LocalCellId;
		}
		int num = -1;
		bool flag2 = QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackValues);
		bool flag3 = false;
		int num2 = 0;
		while (!flag3 && num2 < questCriteria.TargetValues.Length)
		{
			if (questCriteria.ValueSatisfies(data.CurrentValue, num2))
			{
				flag3 = true;
				num = num2;
				break;
			}
			if (flag2 && (num == -1 || criteriaState.CurrentValues[num] > criteriaState.CurrentValues[num2]))
			{
				num = num2;
			}
			num2++;
		}
		if (valueHandle == -1 && num != -1)
		{
			valueHandle = questCriteria.RequiredCount * num + Mathf.Min(criteriaState.CurrentCount, questCriteria.RequiredCount - 1);
		}
		return flag3;
	}

	// Token: 0x06003FCA RID: 16330 RVA: 0x00164D60 File Offset: 0x00162F60
	private void UpdateQuestProgress(bool startQuest = false)
	{
		if (!this.IsStarted && !startQuest)
		{
			return;
		}
		float currentProgress = this.CurrentProgress;
		Quest.State state = this.currentState;
		this.currentState = Quest.State.InProgress;
		this.CurrentProgress = 0f;
		float num = 0f;
		for (int i = 0; i < this.quest.Criteria.Length; i++)
		{
			QuestCriteria questCriteria = this.quest.Criteria[i];
			QuestInstance.CriteriaState criteriaState = this.criteriaStates[questCriteria.CriteriaId.GetHash()];
			float num2 = (float)((questCriteria.TargetValues != null) ? questCriteria.TargetValues.Length : 1);
			num += (float)questCriteria.RequiredCount;
			this.CurrentProgress += (float)criteriaState.CurrentCount;
			if (!this.IsCriteriaSatisfied(questCriteria.CriteriaId))
			{
				float num3 = 0f;
				int num4 = 0;
				while (questCriteria.TargetValues != null && (float)num4 < num2)
				{
					if ((criteriaState.SatisfactionState & questCriteria.GetValueMask(num4)) == 0U)
					{
						if (QuestCriteria.HasBehavior(questCriteria.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackValues))
						{
							int num5 = questCriteria.RequiredCount * num4 + Mathf.Min(criteriaState.CurrentCount, questCriteria.RequiredCount - 1);
							num3 += Mathf.Max(0f, criteriaState.CurrentValues[num5] / questCriteria.TargetValues[num4]);
						}
					}
					else
					{
						num3 += 1f;
					}
					num4++;
				}
				this.CurrentProgress += num3 / num2;
			}
		}
		this.CurrentProgress = Mathf.Clamp01(this.CurrentProgress / num);
		if (this.CurrentProgress == 1f)
		{
			this.currentState = Quest.State.Completed;
		}
		float num6 = this.CurrentProgress - currentProgress;
		if (state != this.currentState || Mathf.Abs(num6) > Mathf.Epsilon)
		{
			Action<QuestInstance, Quest.State, float> questProgressChanged = this.QuestProgressChanged;
			if (questProgressChanged == null)
			{
				return;
			}
			questProgressChanged(this, state, num6);
		}
	}

	// Token: 0x06003FCB RID: 16331 RVA: 0x00164F3C File Offset: 0x0016313C
	public ICheckboxListGroupControl.CheckboxItem[] GetCheckBoxData(Func<int, string, QuestInstance, string> resolveToolTip = null)
	{
		ICheckboxListGroupControl.CheckboxItem[] array = new ICheckboxListGroupControl.CheckboxItem[this.quest.Criteria.Length];
		for (int i = 0; i < this.quest.Criteria.Length; i++)
		{
			QuestCriteria c = this.quest.Criteria[i];
			array[i] = new ICheckboxListGroupControl.CheckboxItem
			{
				text = c.Text,
				isOn = this.IsCriteriaSatisfied(c.CriteriaId),
				tooltip = c.Tooltip
			};
			if (resolveToolTip != null)
			{
				array[i].resolveTooltipCallback = (string tooltip, object owner) => resolveToolTip(c.CriteriaId.GetHash(), c.Tooltip, this);
			}
		}
		return array;
	}

	// Token: 0x040029D3 RID: 10707
	public Action<QuestInstance, Quest.State, float> QuestProgressChanged;

	// Token: 0x040029D5 RID: 10709
	private Quest quest;

	// Token: 0x040029D6 RID: 10710
	[Serialize]
	private Dictionary<int, QuestInstance.CriteriaState> criteriaStates;

	// Token: 0x040029D7 RID: 10711
	[Serialize]
	private Quest.State currentState;

	// Token: 0x0200167D RID: 5757
	private struct CriteriaState
	{
		// Token: 0x060087D9 RID: 34777 RVA: 0x002F3FEC File Offset: 0x002F21EC
		public static bool ItemAlreadySatisfying(QuestInstance.CriteriaState state, Tag item)
		{
			bool flag = false;
			int num = 0;
			while (state.SatisfyingItems != null && num < state.SatisfyingItems.Length)
			{
				if (state.SatisfyingItems[num] == item)
				{
					flag = true;
					break;
				}
				num++;
			}
			return flag;
		}

		// Token: 0x040069F7 RID: 27127
		public int Handle;

		// Token: 0x040069F8 RID: 27128
		public int CurrentCount;

		// Token: 0x040069F9 RID: 27129
		public uint SatisfactionState;

		// Token: 0x040069FA RID: 27130
		public Tag[] SatisfyingItems;

		// Token: 0x040069FB RID: 27131
		public float[] CurrentValues;
	}
}
