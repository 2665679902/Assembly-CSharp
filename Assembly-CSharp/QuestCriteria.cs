using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008A7 RID: 2215
public class QuestCriteria
{
	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x06003FCC RID: 16332 RVA: 0x00165021 File Offset: 0x00163221
	// (set) Token: 0x06003FCD RID: 16333 RVA: 0x00165029 File Offset: 0x00163229
	public string Text { get; private set; }

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06003FCE RID: 16334 RVA: 0x00165032 File Offset: 0x00163232
	// (set) Token: 0x06003FCF RID: 16335 RVA: 0x0016503A File Offset: 0x0016323A
	public string Tooltip { get; private set; }

	// Token: 0x06003FD0 RID: 16336 RVA: 0x00165044 File Offset: 0x00163244
	public QuestCriteria(Tag id, float[] targetValues = null, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.None)
	{
		global::Debug.Assert(targetValues == null || (targetValues.Length != 0 && targetValues.Length <= 32));
		this.CriteriaId = id;
		this.EvaluationBehaviors = flags;
		this.TargetValues = targetValues;
		this.AcceptedTags = acceptedTags;
		this.RequiredCount = requiredCount;
	}

	// Token: 0x06003FD1 RID: 16337 RVA: 0x001650A0 File Offset: 0x001632A0
	public bool ValueSatisfies(float value, int valueHandle)
	{
		if (float.IsNaN(value))
		{
			return false;
		}
		float num = ((this.TargetValues == null) ? 0f : this.TargetValues[valueHandle]);
		return this.ValueSatisfies_Internal(value, num);
	}

	// Token: 0x06003FD2 RID: 16338 RVA: 0x001650D7 File Offset: 0x001632D7
	protected virtual bool ValueSatisfies_Internal(float current, float target)
	{
		return true;
	}

	// Token: 0x06003FD3 RID: 16339 RVA: 0x001650DA File Offset: 0x001632DA
	public bool IsSatisfied(uint satisfactionState, uint satisfactionMask)
	{
		return (satisfactionState & satisfactionMask) == satisfactionMask;
	}

	// Token: 0x06003FD4 RID: 16340 RVA: 0x001650E4 File Offset: 0x001632E4
	public void PopulateStrings(string prefix)
	{
		string text = this.CriteriaId.Name.ToUpperInvariant();
		StringEntry stringEntry;
		if (Strings.TryGet(prefix + "CRITERIA." + text + ".NAME", out stringEntry))
		{
			this.Text = stringEntry.String;
		}
		if (Strings.TryGet(prefix + "CRITERIA." + text + ".TOOLTIP", out stringEntry))
		{
			this.Tooltip = stringEntry.String;
		}
	}

	// Token: 0x06003FD5 RID: 16341 RVA: 0x00165151 File Offset: 0x00163351
	public uint GetSatisfactionMask()
	{
		if (this.TargetValues == null)
		{
			return 1U;
		}
		return (uint)Mathf.Pow(2f, (float)(this.TargetValues.Length - 1));
	}

	// Token: 0x06003FD6 RID: 16342 RVA: 0x00165173 File Offset: 0x00163373
	public uint GetValueMask(int valueHandle)
	{
		if (this.TargetValues == null)
		{
			return 1U;
		}
		if (!QuestCriteria.HasBehavior(this.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackArea))
		{
			valueHandle %= this.TargetValues.Length;
		}
		return 1U << valueHandle;
	}

	// Token: 0x06003FD7 RID: 16343 RVA: 0x0016519F File Offset: 0x0016339F
	public static bool HasBehavior(QuestCriteria.BehaviorFlags flags, QuestCriteria.BehaviorFlags behavior)
	{
		return (flags & behavior) == behavior;
	}

	// Token: 0x040029D8 RID: 10712
	public const int MAX_VALUES = 32;

	// Token: 0x040029D9 RID: 10713
	public const int INVALID_VALUE = -1;

	// Token: 0x040029DA RID: 10714
	public readonly Tag CriteriaId;

	// Token: 0x040029DB RID: 10715
	public readonly QuestCriteria.BehaviorFlags EvaluationBehaviors;

	// Token: 0x040029DC RID: 10716
	public readonly float[] TargetValues;

	// Token: 0x040029DD RID: 10717
	public readonly int RequiredCount = 1;

	// Token: 0x040029DE RID: 10718
	public readonly HashSet<Tag> AcceptedTags;

	// Token: 0x02001680 RID: 5760
	public enum BehaviorFlags
	{
		// Token: 0x04006A01 RID: 27137
		None,
		// Token: 0x04006A02 RID: 27138
		TrackArea,
		// Token: 0x04006A03 RID: 27139
		AllowsRegression,
		// Token: 0x04006A04 RID: 27140
		TrackValues = 4,
		// Token: 0x04006A05 RID: 27141
		TrackItems = 8,
		// Token: 0x04006A06 RID: 27142
		UniqueItems = 24
	}
}
