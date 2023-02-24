using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200070A RID: 1802
public abstract class ColonyDiagnostic : ISim4000ms
{
	// Token: 0x06003174 RID: 12660 RVA: 0x00107F28 File Offset: 0x00106128
	public ColonyDiagnostic(int worldID, string name)
	{
		this.worldID = worldID;
		this.name = name;
		this.id = base.GetType().Name;
		this.colors = new Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, Color>();
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening, Constants.NEGATIVE_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Bad, Constants.NEGATIVE_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Warning, Constants.NEGATIVE_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Concern, Constants.WARNING_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, Constants.NEUTRAL_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion, Constants.NEUTRAL_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Tutorial, Constants.NEUTRAL_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Good, Constants.POSITIVE_COLOR);
		SimAndRenderScheduler.instance.Add(this, true);
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06003175 RID: 12661 RVA: 0x0010803C File Offset: 0x0010623C
	// (set) Token: 0x06003176 RID: 12662 RVA: 0x00108044 File Offset: 0x00106244
	public int worldID { get; protected set; }

	// Token: 0x06003177 RID: 12663 RVA: 0x0010804D File Offset: 0x0010624D
	public virtual string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06003178 RID: 12664 RVA: 0x00108054 File Offset: 0x00106254
	public void OnCleanUp()
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06003179 RID: 12665 RVA: 0x00108061 File Offset: 0x00106261
	public void Sim4000ms(float dt)
	{
		this.SetResult(ColonyDiagnosticUtility.IgnoreFirstUpdate ? ColonyDiagnosticUtility.NoDataResult : this.Evaluate());
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x00108080 File Offset: 0x00106280
	public DiagnosticCriterion[] GetCriteria()
	{
		DiagnosticCriterion[] array = new DiagnosticCriterion[this.criteria.Values.Count];
		this.criteria.Values.CopyTo(array, 0);
		return array;
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x0600317B RID: 12667 RVA: 0x001080B6 File Offset: 0x001062B6
	// (set) Token: 0x0600317C RID: 12668 RVA: 0x001080BE File Offset: 0x001062BE
	public ColonyDiagnostic.DiagnosticResult LatestResult
	{
		get
		{
			return this.latestResult;
		}
		private set
		{
			this.latestResult = value;
		}
	}

	// Token: 0x0600317D RID: 12669 RVA: 0x001080C7 File Offset: 0x001062C7
	public virtual string GetAverageValueString()
	{
		if (this.tracker != null)
		{
			return this.tracker.FormatValueString(Mathf.Round(this.tracker.GetAverageValue(this.trackerSampleCountSeconds)));
		}
		return "";
	}

	// Token: 0x0600317E RID: 12670 RVA: 0x001080F8 File Offset: 0x001062F8
	public virtual string GetCurrentValueString()
	{
		return "";
	}

	// Token: 0x0600317F RID: 12671 RVA: 0x001080FF File Offset: 0x001062FF
	protected void AddCriterion(string id, DiagnosticCriterion criterion)
	{
		if (!this.criteria.ContainsKey(id))
		{
			criterion.SetID(id);
			this.criteria.Add(id, criterion);
		}
	}

	// Token: 0x06003180 RID: 12672 RVA: 0x00108124 File Offset: 0x00106324
	public virtual ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, "", null);
		foreach (KeyValuePair<string, DiagnosticCriterion> keyValuePair in this.criteria)
		{
			if (ColonyDiagnosticUtility.Instance.IsCriteriaEnabled(this.worldID, this.id, keyValuePair.Key))
			{
				ColonyDiagnostic.DiagnosticResult diagnosticResult2 = keyValuePair.Value.Evaluate();
				if (diagnosticResult2.opinion < diagnosticResult.opinion)
				{
					diagnosticResult.opinion = diagnosticResult2.opinion;
					diagnosticResult.Message = diagnosticResult2.Message;
					diagnosticResult.clickThroughTarget = diagnosticResult2.clickThroughTarget;
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003181 RID: 12673 RVA: 0x001081E4 File Offset: 0x001063E4
	public void SetResult(ColonyDiagnostic.DiagnosticResult result)
	{
		this.LatestResult = result;
	}

	// Token: 0x04001E2F RID: 7727
	public string name;

	// Token: 0x04001E30 RID: 7728
	public string id;

	// Token: 0x04001E31 RID: 7729
	public string icon = "icon_errand_operate";

	// Token: 0x04001E32 RID: 7730
	private Dictionary<string, DiagnosticCriterion> criteria = new Dictionary<string, DiagnosticCriterion>();

	// Token: 0x04001E33 RID: 7731
	public ColonyDiagnostic.PresentationSetting presentationSetting;

	// Token: 0x04001E34 RID: 7732
	private ColonyDiagnostic.DiagnosticResult latestResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_DATA, null);

	// Token: 0x04001E35 RID: 7733
	public Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, Color> colors = new Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, Color>();

	// Token: 0x04001E36 RID: 7734
	public Tracker tracker;

	// Token: 0x04001E37 RID: 7735
	protected float trackerSampleCountSeconds = 4f;

	// Token: 0x02001420 RID: 5152
	public enum PresentationSetting
	{
		// Token: 0x0400629A RID: 25242
		AverageValue,
		// Token: 0x0400629B RID: 25243
		CurrentValue
	}

	// Token: 0x02001421 RID: 5153
	public struct DiagnosticResult
	{
		// Token: 0x06008025 RID: 32805 RVA: 0x002DE7CF File Offset: 0x002DC9CF
		public DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion opinion, string message, global::Tuple<Vector3, GameObject> clickThroughTarget = null)
		{
			this.message = message;
			this.opinion = opinion;
			this.clickThroughTarget = null;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06008027 RID: 32807 RVA: 0x002DE7F0 File Offset: 0x002DC9F0
		// (set) Token: 0x06008026 RID: 32806 RVA: 0x002DE7E6 File Offset: 0x002DC9E6
		public string Message
		{
			get
			{
				switch (this.opinion)
				{
				case ColonyDiagnostic.DiagnosticResult.Opinion.Bad:
					return string.Concat(new string[]
					{
						"<color=",
						Constants.NEGATIVE_COLOR_STR,
						">",
						this.message,
						"</color>"
					});
				case ColonyDiagnostic.DiagnosticResult.Opinion.Warning:
					return string.Concat(new string[]
					{
						"<color=",
						Constants.NEGATIVE_COLOR_STR,
						">",
						this.message,
						"</color>"
					});
				case ColonyDiagnostic.DiagnosticResult.Opinion.Concern:
					return string.Concat(new string[]
					{
						"<color=",
						Constants.WARNING_COLOR_STR,
						">",
						this.message,
						"</color>"
					});
				case ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion:
				case ColonyDiagnostic.DiagnosticResult.Opinion.Normal:
					return string.Concat(new string[]
					{
						"<color=",
						Constants.WHITE_COLOR_STR,
						">",
						this.message,
						"</color>"
					});
				case ColonyDiagnostic.DiagnosticResult.Opinion.Good:
					return string.Concat(new string[]
					{
						"<color=",
						Constants.POSITIVE_COLOR_STR,
						">",
						this.message,
						"</color>"
					});
				}
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x0400629C RID: 25244
		public ColonyDiagnostic.DiagnosticResult.Opinion opinion;

		// Token: 0x0400629D RID: 25245
		public global::Tuple<Vector3, GameObject> clickThroughTarget;

		// Token: 0x0400629E RID: 25246
		private string message;

		// Token: 0x02002044 RID: 8260
		public enum Opinion
		{
			// Token: 0x04008FD1 RID: 36817
			Unset,
			// Token: 0x04008FD2 RID: 36818
			DuplicantThreatening,
			// Token: 0x04008FD3 RID: 36819
			Bad,
			// Token: 0x04008FD4 RID: 36820
			Warning,
			// Token: 0x04008FD5 RID: 36821
			Concern,
			// Token: 0x04008FD6 RID: 36822
			Suggestion,
			// Token: 0x04008FD7 RID: 36823
			Tutorial,
			// Token: 0x04008FD8 RID: 36824
			Normal,
			// Token: 0x04008FD9 RID: 36825
			Good
		}
	}
}
