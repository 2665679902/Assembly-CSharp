using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020004BF RID: 1215
[AddComponentMenu("KMonoBehaviour/scripts/ReportManager")]
public class ReportManager : KMonoBehaviour
{
	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00095081 File Offset: 0x00093281
	public List<ReportManager.DailyReport> reports
	{
		get
		{
			return this.dailyReports;
		}
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x00095089 File Offset: 0x00093289
	public static void DestroyInstance()
	{
		ReportManager.Instance = null;
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00095091 File Offset: 0x00093291
	// (set) Token: 0x06001C16 RID: 7190 RVA: 0x00095098 File Offset: 0x00093298
	public static ReportManager Instance { get; private set; }

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06001C17 RID: 7191 RVA: 0x000950A0 File Offset: 0x000932A0
	public ReportManager.DailyReport TodaysReport
	{
		get
		{
			return this.todaysReport;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06001C18 RID: 7192 RVA: 0x000950A8 File Offset: 0x000932A8
	public ReportManager.DailyReport YesterdaysReport
	{
		get
		{
			if (this.dailyReports.Count <= 1)
			{
				return null;
			}
			return this.dailyReports[this.dailyReports.Count - 1];
		}
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x000950D2 File Offset: 0x000932D2
	protected override void OnPrefabInit()
	{
		ReportManager.Instance = this;
		base.Subscribe(Game.Instance.gameObject, -1917495436, new Action<object>(this.OnSaveGameReady));
		this.noteStorage = new ReportManager.NoteStorage();
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x00095107 File Offset: 0x00093307
	protected override void OnCleanUp()
	{
		ReportManager.Instance = null;
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0009510F File Offset: 0x0009330F
	[CustomSerialize]
	private void CustomSerialize(BinaryWriter writer)
	{
		writer.Write(0);
		this.noteStorage.Serialize(writer);
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x00095124 File Offset: 0x00093324
	[CustomDeserialize]
	private void CustomDeserialize(IReader reader)
	{
		if (this.noteStorageBytes == null)
		{
			global::Debug.Assert(reader.ReadInt32() == 0);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(reader.RawBytes()));
			binaryReader.BaseStream.Position = (long)reader.Position;
			this.noteStorage.Deserialize(binaryReader);
			reader.SkipBytes((int)binaryReader.BaseStream.Position - reader.Position);
		}
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x0009518F File Offset: 0x0009338F
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.noteStorageBytes != null)
		{
			this.noteStorage.Deserialize(new BinaryReader(new MemoryStream(this.noteStorageBytes)));
			this.noteStorageBytes = null;
		}
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x000951BC File Offset: 0x000933BC
	private void OnSaveGameReady(object data)
	{
		base.Subscribe(GameClock.Instance.gameObject, -722330267, new Action<object>(this.OnNightTime));
		if (this.todaysReport == null)
		{
			this.todaysReport = new ReportManager.DailyReport(this);
			this.todaysReport.day = GameUtil.GetCurrentCycle();
		}
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x0009520F File Offset: 0x0009340F
	public void ReportValue(ReportManager.ReportType reportType, float value, string note = null, string context = null)
	{
		this.TodaysReport.AddData(reportType, value, note, context);
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x00095224 File Offset: 0x00093424
	private void OnNightTime(object data)
	{
		this.dailyReports.Add(this.todaysReport);
		int day = this.todaysReport.day;
		ManagementMenuNotification managementMenuNotification = new ManagementMenuNotification(global::Action.ManageReport, NotificationValence.Good, null, string.Format(UI.ENDOFDAYREPORT.NOTIFICATION_TITLE, day), NotificationType.Good, (List<Notification> n, object d) => string.Format(UI.ENDOFDAYREPORT.NOTIFICATION_TOOLTIP, day), null, true, 0f, delegate(object d)
		{
			ManagementMenu.Instance.OpenReports(day);
		}, null, null, true);
		if (this.notifier == null)
		{
			global::Debug.LogError("Cant notify, null notifier");
		}
		else
		{
			this.notifier.Add(managementMenuNotification, "");
		}
		this.todaysReport = new ReportManager.DailyReport(this);
		this.todaysReport.day = GameUtil.GetCurrentCycle() + 1;
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x000952EC File Offset: 0x000934EC
	public ReportManager.DailyReport FindReport(int day)
	{
		foreach (ReportManager.DailyReport dailyReport in this.dailyReports)
		{
			if (dailyReport.day == day)
			{
				return dailyReport;
			}
		}
		if (this.todaysReport.day == day)
		{
			return this.todaysReport;
		}
		return null;
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x00095360 File Offset: 0x00093560
	public ReportManager()
	{
		Dictionary<ReportManager.ReportType, ReportManager.ReportGroup> dictionary = new Dictionary<ReportManager.ReportType, ReportManager.ReportGroup>();
		dictionary.Add(ReportManager.ReportType.DuplicantHeader, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.DUPLICANT_DETAILS_HEADER, "", "", ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order.Unordered, true, null));
		dictionary.Add(ReportManager.ReportType.CaloriesCreated, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedCalories(v, GameUtil.TimeSlice.None, true), true, 1, UI.ENDOFDAYREPORT.CALORIES_CREATED.NAME, UI.ENDOFDAYREPORT.CALORIES_CREATED.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CALORIES_CREATED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.StressDelta, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v, GameUtil.TimeSlice.None), true, 1, UI.ENDOFDAYREPORT.STRESS_DELTA.NAME, UI.ENDOFDAYREPORT.STRESS_DELTA.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.STRESS_DELTA.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.DiseaseAdded, new ReportManager.ReportGroup(null, false, 1, UI.ENDOFDAYREPORT.DISEASE_ADDED.NAME, UI.ENDOFDAYREPORT.DISEASE_ADDED.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.DISEASE_ADDED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.DiseaseStatus, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedDiseaseAmount((int)v, GameUtil.TimeSlice.None), true, 1, UI.ENDOFDAYREPORT.DISEASE_STATUS.NAME, UI.ENDOFDAYREPORT.DISEASE_STATUS.TOOLTIP, UI.ENDOFDAYREPORT.DISEASE_STATUS.TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.LevelUp, new ReportManager.ReportGroup(null, false, 1, UI.ENDOFDAYREPORT.LEVEL_UP.NAME, UI.ENDOFDAYREPORT.LEVEL_UP.TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ToiletIncident, new ReportManager.ReportGroup(null, false, 1, UI.ENDOFDAYREPORT.TOILET_INCIDENT.NAME, UI.ENDOFDAYREPORT.TOILET_INCIDENT.TOOLTIP, UI.ENDOFDAYREPORT.TOILET_INCIDENT.TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ChoreStatus, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.CHORE_STATUS.NAME, UI.ENDOFDAYREPORT.CHORE_STATUS.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CHORE_STATUS.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.DomesticatedCritters, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.NUMBER_OF_DOMESTICATED_CRITTERS.NAME, UI.ENDOFDAYREPORT.NUMBER_OF_DOMESTICATED_CRITTERS.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NUMBER_OF_DOMESTICATED_CRITTERS.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.WildCritters, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.NUMBER_OF_WILD_CRITTERS.NAME, UI.ENDOFDAYREPORT.NUMBER_OF_WILD_CRITTERS.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NUMBER_OF_WILD_CRITTERS.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.RocketsInFlight, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.ROCKETS_IN_FLIGHT.NAME, UI.ENDOFDAYREPORT.ROCKETS_IN_FLIGHT.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.ROCKETS_IN_FLIGHT.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.TimeSpentHeader, new ReportManager.ReportGroup(null, true, 2, UI.ENDOFDAYREPORT.TIME_DETAILS_HEADER, "", "", ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order.Unordered, true, null));
		dictionary.Add(ReportManager.ReportType.WorkTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.WORK_TIME.NAME, UI.ENDOFDAYREPORT.WORK_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.TravelTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.TRAVEL_TIME.NAME, UI.ENDOFDAYREPORT.TRAVEL_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.PersonalTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.PERSONAL_TIME.NAME, UI.ENDOFDAYREPORT.PERSONAL_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.IdleTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.IDLE_TIME.NAME, UI.ENDOFDAYREPORT.IDLE_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.BaseHeader, new ReportManager.ReportGroup(null, true, 3, UI.ENDOFDAYREPORT.BASE_DETAILS_HEADER, "", "", ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order.Unordered, true, null));
		dictionary.Add(ReportManager.ReportType.OxygenCreated, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedMass(v, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), true, 3, UI.ENDOFDAYREPORT.OXYGEN_CREATED.NAME, UI.ENDOFDAYREPORT.OXYGEN_CREATED.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.OXYGEN_CREATED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.EnergyCreated, new ReportManager.ReportGroup(new ReportManager.FormattingFn(GameUtil.GetFormattedRoundedJoules), true, 3, UI.ENDOFDAYREPORT.ENERGY_USAGE.NAME, UI.ENDOFDAYREPORT.ENERGY_USAGE.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.ENERGY_USAGE.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.EnergyWasted, new ReportManager.ReportGroup(new ReportManager.FormattingFn(GameUtil.GetFormattedRoundedJoules), true, 3, UI.ENDOFDAYREPORT.ENERGY_WASTED.NAME, UI.ENDOFDAYREPORT.NONE, UI.ENDOFDAYREPORT.ENERGY_WASTED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ContaminatedOxygenToilet, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedMass(v, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), false, 3, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_TOILET.NAME, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_TOILET.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_TOILET.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ContaminatedOxygenSublimation, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedMass(v, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), false, 3, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_SUBLIMATION.NAME, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_SUBLIMATION.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_SUBLIMATION.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		this.ReportGroups = dictionary;
		this.dailyReports = new List<ReportManager.DailyReport>();
		base..ctor();
	}

	// Token: 0x04000FB1 RID: 4017
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04000FB2 RID: 4018
	private ReportManager.NoteStorage noteStorage;

	// Token: 0x04000FB3 RID: 4019
	public Dictionary<ReportManager.ReportType, ReportManager.ReportGroup> ReportGroups;

	// Token: 0x04000FB4 RID: 4020
	[Serialize]
	private List<ReportManager.DailyReport> dailyReports;

	// Token: 0x04000FB5 RID: 4021
	[Serialize]
	private ReportManager.DailyReport todaysReport;

	// Token: 0x04000FB6 RID: 4022
	[Serialize]
	private byte[] noteStorageBytes;

	// Token: 0x020010FD RID: 4349
	// (Invoke) Token: 0x06007518 RID: 29976
	public delegate string FormattingFn(float v);

	// Token: 0x020010FE RID: 4350
	// (Invoke) Token: 0x0600751C RID: 29980
	public delegate string GroupFormattingFn(float v, float numEntries);

	// Token: 0x020010FF RID: 4351
	public enum ReportType
	{
		// Token: 0x04005942 RID: 22850
		DuplicantHeader,
		// Token: 0x04005943 RID: 22851
		CaloriesCreated,
		// Token: 0x04005944 RID: 22852
		StressDelta,
		// Token: 0x04005945 RID: 22853
		LevelUp,
		// Token: 0x04005946 RID: 22854
		DiseaseStatus,
		// Token: 0x04005947 RID: 22855
		DiseaseAdded,
		// Token: 0x04005948 RID: 22856
		ToiletIncident,
		// Token: 0x04005949 RID: 22857
		ChoreStatus,
		// Token: 0x0400594A RID: 22858
		TimeSpentHeader,
		// Token: 0x0400594B RID: 22859
		TimeSpent,
		// Token: 0x0400594C RID: 22860
		WorkTime,
		// Token: 0x0400594D RID: 22861
		TravelTime,
		// Token: 0x0400594E RID: 22862
		PersonalTime,
		// Token: 0x0400594F RID: 22863
		IdleTime,
		// Token: 0x04005950 RID: 22864
		BaseHeader,
		// Token: 0x04005951 RID: 22865
		ContaminatedOxygenFlatulence,
		// Token: 0x04005952 RID: 22866
		ContaminatedOxygenToilet,
		// Token: 0x04005953 RID: 22867
		ContaminatedOxygenSublimation,
		// Token: 0x04005954 RID: 22868
		OxygenCreated,
		// Token: 0x04005955 RID: 22869
		EnergyCreated,
		// Token: 0x04005956 RID: 22870
		EnergyWasted,
		// Token: 0x04005957 RID: 22871
		DomesticatedCritters,
		// Token: 0x04005958 RID: 22872
		WildCritters,
		// Token: 0x04005959 RID: 22873
		RocketsInFlight
	}

	// Token: 0x02001100 RID: 4352
	public struct ReportGroup
	{
		// Token: 0x0600751F RID: 29983 RVA: 0x002B4FBC File Offset: 0x002B31BC
		public ReportGroup(ReportManager.FormattingFn formatfn, bool reportIfZero, int group, string stringKey, string positiveTooltip, string negativeTooltip, ReportManager.ReportEntry.Order pos_note_order = ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order neg_note_order = ReportManager.ReportEntry.Order.Unordered, bool is_header = false, ReportManager.GroupFormattingFn group_format_fn = null)
		{
			ReportManager.FormattingFn formattingFn;
			if (formatfn == null)
			{
				formattingFn = (float v) => v.ToString();
			}
			else
			{
				formattingFn = formatfn;
			}
			this.formatfn = formattingFn;
			this.groupFormatfn = group_format_fn;
			this.stringKey = stringKey;
			this.positiveTooltip = positiveTooltip;
			this.negativeTooltip = negativeTooltip;
			this.reportIfZero = reportIfZero;
			this.group = group;
			this.posNoteOrder = pos_note_order;
			this.negNoteOrder = neg_note_order;
			this.isHeader = is_header;
		}

		// Token: 0x0400595A RID: 22874
		public ReportManager.FormattingFn formatfn;

		// Token: 0x0400595B RID: 22875
		public ReportManager.GroupFormattingFn groupFormatfn;

		// Token: 0x0400595C RID: 22876
		public string stringKey;

		// Token: 0x0400595D RID: 22877
		public string positiveTooltip;

		// Token: 0x0400595E RID: 22878
		public string negativeTooltip;

		// Token: 0x0400595F RID: 22879
		public bool reportIfZero;

		// Token: 0x04005960 RID: 22880
		public int group;

		// Token: 0x04005961 RID: 22881
		public bool isHeader;

		// Token: 0x04005962 RID: 22882
		public ReportManager.ReportEntry.Order posNoteOrder;

		// Token: 0x04005963 RID: 22883
		public ReportManager.ReportEntry.Order negNoteOrder;
	}

	// Token: 0x02001101 RID: 4353
	[SerializationConfig(MemberSerialization.OptIn)]
	public class ReportEntry
	{
		// Token: 0x06007520 RID: 29984 RVA: 0x002B503C File Offset: 0x002B323C
		public ReportEntry(ReportManager.ReportType reportType, int note_storage_id, string context, bool is_child = false)
		{
			this.reportType = reportType;
			this.context = context;
			this.isChild = is_child;
			this.accumulate = 0f;
			this.accPositive = 0f;
			this.accNegative = 0f;
			this.noteStorageId = note_storage_id;
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06007521 RID: 29985 RVA: 0x002B5094 File Offset: 0x002B3294
		public float Positive
		{
			get
			{
				return this.accPositive;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06007522 RID: 29986 RVA: 0x002B509C File Offset: 0x002B329C
		public float Negative
		{
			get
			{
				return this.accNegative;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06007523 RID: 29987 RVA: 0x002B50A4 File Offset: 0x002B32A4
		public float Net
		{
			get
			{
				return this.accPositive + this.accNegative;
			}
		}

		// Token: 0x06007524 RID: 29988 RVA: 0x002B50B3 File Offset: 0x002B32B3
		[OnDeserializing]
		private void OnDeserialize()
		{
			this.contextEntries.Clear();
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x002B50C0 File Offset: 0x002B32C0
		public void IterateNotes(Action<ReportManager.ReportEntry.Note> callback)
		{
			ReportManager.Instance.noteStorage.IterateNotes(this.noteStorageId, callback);
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x002B50D8 File Offset: 0x002B32D8
		[OnDeserialized]
		private void OnDeserialized()
		{
			if (this.gameHash != -1)
			{
				this.reportType = (ReportManager.ReportType)this.gameHash;
				this.gameHash = -1;
			}
		}

		// Token: 0x06007527 RID: 29991 RVA: 0x002B50F8 File Offset: 0x002B32F8
		public void AddData(ReportManager.NoteStorage note_storage, float value, string note = null, string dataContext = null)
		{
			this.AddActualData(note_storage, value, note);
			if (dataContext != null)
			{
				ReportManager.ReportEntry reportEntry = null;
				for (int i = 0; i < this.contextEntries.Count; i++)
				{
					if (this.contextEntries[i].context == dataContext)
					{
						reportEntry = this.contextEntries[i];
						break;
					}
				}
				if (reportEntry == null)
				{
					reportEntry = new ReportManager.ReportEntry(this.reportType, note_storage.GetNewNoteId(), dataContext, true);
					this.contextEntries.Add(reportEntry);
				}
				reportEntry.AddActualData(note_storage, value, note);
			}
		}

		// Token: 0x06007528 RID: 29992 RVA: 0x002B5184 File Offset: 0x002B3384
		private void AddActualData(ReportManager.NoteStorage note_storage, float value, string note = null)
		{
			this.accumulate += value;
			if (value > 0f)
			{
				this.accPositive += value;
			}
			else
			{
				this.accNegative += value;
			}
			if (note != null)
			{
				note_storage.Add(this.noteStorageId, value, note);
			}
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x002B51D6 File Offset: 0x002B33D6
		public bool HasContextEntries()
		{
			return this.contextEntries.Count > 0;
		}

		// Token: 0x04005964 RID: 22884
		[Serialize]
		public int noteStorageId;

		// Token: 0x04005965 RID: 22885
		[Serialize]
		public int gameHash = -1;

		// Token: 0x04005966 RID: 22886
		[Serialize]
		public ReportManager.ReportType reportType;

		// Token: 0x04005967 RID: 22887
		[Serialize]
		public string context;

		// Token: 0x04005968 RID: 22888
		[Serialize]
		public float accumulate;

		// Token: 0x04005969 RID: 22889
		[Serialize]
		public float accPositive;

		// Token: 0x0400596A RID: 22890
		[Serialize]
		public float accNegative;

		// Token: 0x0400596B RID: 22891
		[Serialize]
		public ArrayRef<ReportManager.ReportEntry> contextEntries;

		// Token: 0x0400596C RID: 22892
		public bool isChild;

		// Token: 0x02001F79 RID: 8057
		public struct Note
		{
			// Token: 0x06009F09 RID: 40713 RVA: 0x0033FA1F File Offset: 0x0033DC1F
			public Note(float value, string note)
			{
				this.value = value;
				this.note = note;
			}

			// Token: 0x04008BE7 RID: 35815
			public float value;

			// Token: 0x04008BE8 RID: 35816
			public string note;
		}

		// Token: 0x02001F7A RID: 8058
		public enum Order
		{
			// Token: 0x04008BEA RID: 35818
			Unordered,
			// Token: 0x04008BEB RID: 35819
			Ascending,
			// Token: 0x04008BEC RID: 35820
			Descending
		}
	}

	// Token: 0x02001102 RID: 4354
	public class DailyReport
	{
		// Token: 0x0600752A RID: 29994 RVA: 0x002B51E8 File Offset: 0x002B33E8
		public DailyReport(ReportManager manager)
		{
			foreach (KeyValuePair<ReportManager.ReportType, ReportManager.ReportGroup> keyValuePair in manager.ReportGroups)
			{
				this.reportEntries.Add(new ReportManager.ReportEntry(keyValuePair.Key, this.noteStorage.GetNewNoteId(), null, false));
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600752B RID: 29995 RVA: 0x002B526C File Offset: 0x002B346C
		private ReportManager.NoteStorage noteStorage
		{
			get
			{
				return ReportManager.Instance.noteStorage;
			}
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x002B5278 File Offset: 0x002B3478
		public ReportManager.ReportEntry GetEntry(ReportManager.ReportType reportType)
		{
			for (int i = 0; i < this.reportEntries.Count; i++)
			{
				ReportManager.ReportEntry reportEntry = this.reportEntries[i];
				if (reportEntry.reportType == reportType)
				{
					return reportEntry;
				}
			}
			ReportManager.ReportEntry reportEntry2 = new ReportManager.ReportEntry(reportType, this.noteStorage.GetNewNoteId(), null, false);
			this.reportEntries.Add(reportEntry2);
			return reportEntry2;
		}

		// Token: 0x0600752D RID: 29997 RVA: 0x002B52D4 File Offset: 0x002B34D4
		public void AddData(ReportManager.ReportType reportType, float value, string note = null, string context = null)
		{
			this.GetEntry(reportType).AddData(this.noteStorage, value, note, context);
		}

		// Token: 0x0400596D RID: 22893
		[Serialize]
		public int day;

		// Token: 0x0400596E RID: 22894
		[Serialize]
		public List<ReportManager.ReportEntry> reportEntries = new List<ReportManager.ReportEntry>();
	}

	// Token: 0x02001103 RID: 4355
	public class NoteStorage
	{
		// Token: 0x0600752E RID: 29998 RVA: 0x002B52EC File Offset: 0x002B34EC
		public NoteStorage()
		{
			this.noteEntries = new ReportManager.NoteStorage.NoteEntries();
			this.stringTable = new ReportManager.NoteStorage.StringTable();
		}

		// Token: 0x0600752F RID: 29999 RVA: 0x002B530C File Offset: 0x002B350C
		public void Add(int report_entry_id, float value, string note)
		{
			int num = this.stringTable.AddString(note, 6);
			this.noteEntries.Add(report_entry_id, value, num);
		}

		// Token: 0x06007530 RID: 30000 RVA: 0x002B5338 File Offset: 0x002B3538
		public int GetNewNoteId()
		{
			int num = this.nextNoteId + 1;
			this.nextNoteId = num;
			return num;
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x002B5356 File Offset: 0x002B3556
		public void IterateNotes(int report_entry_id, Action<ReportManager.ReportEntry.Note> callback)
		{
			this.noteEntries.IterateNotes(this.stringTable, report_entry_id, callback);
		}

		// Token: 0x06007532 RID: 30002 RVA: 0x002B536B File Offset: 0x002B356B
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(6);
			writer.Write(this.nextNoteId);
			this.stringTable.Serialize(writer);
			this.noteEntries.Serialize(writer);
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x002B5398 File Offset: 0x002B3598
		public void Deserialize(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num < 5)
			{
				return;
			}
			this.nextNoteId = reader.ReadInt32();
			this.stringTable.Deserialize(reader, num);
			this.noteEntries.Deserialize(reader, num);
		}

		// Token: 0x0400596F RID: 22895
		public const int SERIALIZATION_VERSION = 6;

		// Token: 0x04005970 RID: 22896
		private int nextNoteId;

		// Token: 0x04005971 RID: 22897
		private ReportManager.NoteStorage.NoteEntries noteEntries;

		// Token: 0x04005972 RID: 22898
		private ReportManager.NoteStorage.StringTable stringTable;

		// Token: 0x02001F7B RID: 8059
		private class StringTable
		{
			// Token: 0x06009F0A RID: 40714 RVA: 0x0033FA30 File Offset: 0x0033DC30
			public int AddString(string str, int version = 6)
			{
				int num = Hash.SDBMLower(str);
				this.strings[num] = str;
				return num;
			}

			// Token: 0x06009F0B RID: 40715 RVA: 0x0033FA54 File Offset: 0x0033DC54
			public string GetStringByHash(int hash)
			{
				string text = "";
				this.strings.TryGetValue(hash, out text);
				return text;
			}

			// Token: 0x06009F0C RID: 40716 RVA: 0x0033FA78 File Offset: 0x0033DC78
			public void Serialize(BinaryWriter writer)
			{
				writer.Write(this.strings.Count);
				foreach (KeyValuePair<int, string> keyValuePair in this.strings)
				{
					writer.Write(keyValuePair.Value);
				}
			}

			// Token: 0x06009F0D RID: 40717 RVA: 0x0033FAE4 File Offset: 0x0033DCE4
			public void Deserialize(BinaryReader reader, int version)
			{
				int num = reader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string text = reader.ReadString();
					this.AddString(text, version);
				}
			}

			// Token: 0x04008BED RID: 35821
			private Dictionary<int, string> strings = new Dictionary<int, string>();
		}

		// Token: 0x02001F7C RID: 8060
		private class NoteEntries
		{
			// Token: 0x06009F0F RID: 40719 RVA: 0x0033FB28 File Offset: 0x0033DD28
			public void Add(int report_entry_id, float value, int note_id)
			{
				Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary;
				if (!this.entries.TryGetValue(report_entry_id, out dictionary))
				{
					dictionary = new Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>(ReportManager.NoteStorage.NoteEntries.sKeyComparer);
					this.entries[report_entry_id] = dictionary;
				}
				ReportManager.NoteStorage.NoteEntries.NoteEntryKey noteEntryKey = new ReportManager.NoteStorage.NoteEntries.NoteEntryKey
				{
					noteHash = note_id,
					isPositive = (value > 0f)
				};
				if (dictionary.ContainsKey(noteEntryKey))
				{
					Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary2 = dictionary;
					ReportManager.NoteStorage.NoteEntries.NoteEntryKey noteEntryKey2 = noteEntryKey;
					dictionary2[noteEntryKey2] += value;
					return;
				}
				dictionary[noteEntryKey] = value;
			}

			// Token: 0x06009F10 RID: 40720 RVA: 0x0033FBA4 File Offset: 0x0033DDA4
			public void Serialize(BinaryWriter writer)
			{
				writer.Write(this.entries.Count);
				foreach (KeyValuePair<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>> keyValuePair in this.entries)
				{
					writer.Write(keyValuePair.Key);
					writer.Write(keyValuePair.Value.Count);
					foreach (KeyValuePair<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> keyValuePair2 in keyValuePair.Value)
					{
						writer.Write(keyValuePair2.Key.noteHash);
						writer.Write(keyValuePair2.Key.isPositive);
						writer.WriteSingleFast(keyValuePair2.Value);
					}
				}
			}

			// Token: 0x06009F11 RID: 40721 RVA: 0x0033FC94 File Offset: 0x0033DE94
			public void Deserialize(BinaryReader reader, int version)
			{
				if (version < 6)
				{
					OldNoteEntriesV5 oldNoteEntriesV = new OldNoteEntriesV5();
					oldNoteEntriesV.Deserialize(reader);
					foreach (OldNoteEntriesV5.NoteStorageBlock noteStorageBlock in oldNoteEntriesV.storageBlocks)
					{
						for (int i = 0; i < noteStorageBlock.entryCount; i++)
						{
							OldNoteEntriesV5.NoteEntry noteEntry = noteStorageBlock.entries.structs[i];
							this.Add(noteEntry.reportEntryId, noteEntry.value, noteEntry.noteHash);
						}
					}
					return;
				}
				int num = reader.ReadInt32();
				this.entries = new Dictionary<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>>(num);
				for (int j = 0; j < num; j++)
				{
					int num2 = reader.ReadInt32();
					int num3 = reader.ReadInt32();
					Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary = new Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>(num3, ReportManager.NoteStorage.NoteEntries.sKeyComparer);
					this.entries[num2] = dictionary;
					for (int k = 0; k < num3; k++)
					{
						ReportManager.NoteStorage.NoteEntries.NoteEntryKey noteEntryKey = new ReportManager.NoteStorage.NoteEntries.NoteEntryKey
						{
							noteHash = reader.ReadInt32(),
							isPositive = reader.ReadBoolean()
						};
						dictionary[noteEntryKey] = reader.ReadSingle();
					}
				}
			}

			// Token: 0x06009F12 RID: 40722 RVA: 0x0033FDC8 File Offset: 0x0033DFC8
			public void IterateNotes(ReportManager.NoteStorage.StringTable string_table, int report_entry_id, Action<ReportManager.ReportEntry.Note> callback)
			{
				Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary;
				if (this.entries.TryGetValue(report_entry_id, out dictionary))
				{
					foreach (KeyValuePair<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> keyValuePair in dictionary)
					{
						string stringByHash = string_table.GetStringByHash(keyValuePair.Key.noteHash);
						ReportManager.ReportEntry.Note note = new ReportManager.ReportEntry.Note(keyValuePair.Value, stringByHash);
						callback(note);
					}
				}
			}

			// Token: 0x04008BEE RID: 35822
			private static ReportManager.NoteStorage.NoteEntries.NoteEntryKeyComparer sKeyComparer = new ReportManager.NoteStorage.NoteEntries.NoteEntryKeyComparer();

			// Token: 0x04008BEF RID: 35823
			private Dictionary<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>> entries = new Dictionary<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>>();

			// Token: 0x02002DA9 RID: 11689
			public struct NoteEntryKey
			{
				// Token: 0x0400BA44 RID: 47684
				public int noteHash;

				// Token: 0x0400BA45 RID: 47685
				public bool isPositive;
			}

			// Token: 0x02002DAA RID: 11690
			public class NoteEntryKeyComparer : IEqualityComparer<ReportManager.NoteStorage.NoteEntries.NoteEntryKey>
			{
				// Token: 0x0600BEB3 RID: 48819 RVA: 0x003915FB File Offset: 0x0038F7FB
				public bool Equals(ReportManager.NoteStorage.NoteEntries.NoteEntryKey a, ReportManager.NoteStorage.NoteEntries.NoteEntryKey b)
				{
					return a.noteHash == b.noteHash && a.isPositive == b.isPositive;
				}

				// Token: 0x0600BEB4 RID: 48820 RVA: 0x0039161B File Offset: 0x0038F81B
				public int GetHashCode(ReportManager.NoteStorage.NoteEntries.NoteEntryKey a)
				{
					return a.noteHash * (a.isPositive ? 1 : (-1));
				}
			}
		}
	}
}
