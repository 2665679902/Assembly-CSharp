using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FMOD.Studio;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200090B RID: 2315
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleManager")]
public class ScheduleManager : KMonoBehaviour, ISim33ms
{
	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06004377 RID: 17271 RVA: 0x0017D504 File Offset: 0x0017B704
	// (remove) Token: 0x06004378 RID: 17272 RVA: 0x0017D53C File Offset: 0x0017B73C
	public event Action<List<Schedule>> onSchedulesChanged;

	// Token: 0x06004379 RID: 17273 RVA: 0x0017D571 File Offset: 0x0017B771
	public static void DestroyInstance()
	{
		ScheduleManager.Instance = null;
	}

	// Token: 0x0600437A RID: 17274 RVA: 0x0017D579 File Offset: 0x0017B779
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.schedules.Count == 0)
		{
			this.AddDefaultSchedule(true);
		}
	}

	// Token: 0x0600437B RID: 17275 RVA: 0x0017D58F File Offset: 0x0017B78F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.schedules = new List<Schedule>();
		ScheduleManager.Instance = this;
	}

	// Token: 0x0600437C RID: 17276 RVA: 0x0017D5A8 File Offset: 0x0017B7A8
	protected override void OnSpawn()
	{
		if (this.schedules.Count == 0)
		{
			this.AddDefaultSchedule(true);
		}
		foreach (Schedule schedule in this.schedules)
		{
			schedule.ClearNullReferences();
		}
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			Schedulable component = minionIdentity.GetComponent<Schedulable>();
			if (this.GetSchedule(component) == null)
			{
				this.schedules[0].Assign(component);
			}
		}
		Components.LiveMinionIdentities.OnAdd += this.OnAddDupe;
		Components.LiveMinionIdentities.OnRemove += this.OnRemoveDupe;
	}

	// Token: 0x0600437D RID: 17277 RVA: 0x0017D698 File Offset: 0x0017B898
	private void OnAddDupe(MinionIdentity minion)
	{
		Schedulable component = minion.GetComponent<Schedulable>();
		if (this.GetSchedule(component) == null)
		{
			this.schedules[0].Assign(component);
		}
	}

	// Token: 0x0600437E RID: 17278 RVA: 0x0017D6C8 File Offset: 0x0017B8C8
	private void OnRemoveDupe(MinionIdentity minion)
	{
		Schedulable component = minion.GetComponent<Schedulable>();
		Schedule schedule = this.GetSchedule(component);
		if (schedule != null)
		{
			schedule.Unassign(component);
		}
	}

	// Token: 0x0600437F RID: 17279 RVA: 0x0017D6F0 File Offset: 0x0017B8F0
	public void OnStoredDupeDestroyed(StoredMinionIdentity dupe)
	{
		foreach (Schedule schedule in this.schedules)
		{
			schedule.Unassign(dupe.gameObject.GetComponent<Schedulable>());
		}
	}

	// Token: 0x06004380 RID: 17280 RVA: 0x0017D74C File Offset: 0x0017B94C
	public void AddDefaultSchedule(bool alarmOn)
	{
		Schedule schedule = this.AddSchedule(Db.Get().ScheduleGroups.allGroups, UI.SCHEDULESCREEN.SCHEDULE_NAME_DEFAULT, alarmOn);
		if (Game.Instance.FastWorkersModeActive)
		{
			for (int i = 0; i < 21; i++)
			{
				schedule.SetGroup(i, Db.Get().ScheduleGroups.Worktime);
			}
			schedule.SetGroup(21, Db.Get().ScheduleGroups.Recreation);
			schedule.SetGroup(22, Db.Get().ScheduleGroups.Recreation);
			schedule.SetGroup(23, Db.Get().ScheduleGroups.Sleep);
		}
	}

	// Token: 0x06004381 RID: 17281 RVA: 0x0017D7F0 File Offset: 0x0017B9F0
	public Schedule AddSchedule(List<ScheduleGroup> groups, string name = null, bool alarmOn = false)
	{
		this.scheduleNameIncrementor++;
		if (name == null)
		{
			name = string.Format(UI.SCHEDULESCREEN.SCHEDULE_NAME_FORMAT, this.scheduleNameIncrementor.ToString());
		}
		Schedule schedule = new Schedule(name, groups, alarmOn);
		this.schedules.Add(schedule);
		if (this.onSchedulesChanged != null)
		{
			this.onSchedulesChanged(this.schedules);
		}
		return schedule;
	}

	// Token: 0x06004382 RID: 17282 RVA: 0x0017D85C File Offset: 0x0017BA5C
	public void DeleteSchedule(Schedule schedule)
	{
		if (this.schedules.Count == 1)
		{
			return;
		}
		List<Ref<Schedulable>> assigned = schedule.GetAssigned();
		this.schedules.Remove(schedule);
		foreach (Ref<Schedulable> @ref in assigned)
		{
			this.schedules[0].Assign(@ref.Get());
		}
		if (this.onSchedulesChanged != null)
		{
			this.onSchedulesChanged(this.schedules);
		}
	}

	// Token: 0x06004383 RID: 17283 RVA: 0x0017D8F4 File Offset: 0x0017BAF4
	public Schedule GetSchedule(Schedulable schedulable)
	{
		foreach (Schedule schedule in this.schedules)
		{
			if (schedule.IsAssigned(schedulable))
			{
				return schedule;
			}
		}
		return null;
	}

	// Token: 0x06004384 RID: 17284 RVA: 0x0017D950 File Offset: 0x0017BB50
	public List<Schedule> GetSchedules()
	{
		return this.schedules;
	}

	// Token: 0x06004385 RID: 17285 RVA: 0x0017D958 File Offset: 0x0017BB58
	public bool IsAllowed(Schedulable schedulable, ScheduleBlockType schedule_block_type)
	{
		int blockIdx = Schedule.GetBlockIdx();
		Schedule schedule = this.GetSchedule(schedulable);
		return schedule != null && schedule.GetBlock(blockIdx).IsAllowed(schedule_block_type);
	}

	// Token: 0x06004386 RID: 17286 RVA: 0x0017D988 File Offset: 0x0017BB88
	public void Sim33ms(float dt)
	{
		int blockIdx = Schedule.GetBlockIdx();
		if (blockIdx != this.lastIdx)
		{
			foreach (Schedule schedule in this.schedules)
			{
				schedule.Tick();
			}
			this.lastIdx = blockIdx;
		}
	}

	// Token: 0x06004387 RID: 17287 RVA: 0x0017D9F0 File Offset: 0x0017BBF0
	public void PlayScheduleAlarm(Schedule schedule, ScheduleBlock block, bool forwards)
	{
		Notification notification = new Notification(string.Format(MISC.NOTIFICATIONS.SCHEDULE_CHANGED.NAME, schedule.name, block.name), NotificationType.Good, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.SCHEDULE_CHANGED.TOOLTIP.Replace("{0}", schedule.name).Replace("{1}", block.name).Replace("{2}", Db.Get().ScheduleGroups.Get(block.GroupId).notificationTooltip), null, true, 0f, null, null, null, true, false, false);
		base.GetComponent<Notifier>().Add(notification, "");
		base.StartCoroutine(this.PlayScheduleTone(schedule, forwards));
	}

	// Token: 0x06004388 RID: 17288 RVA: 0x0017DA7B File Offset: 0x0017BC7B
	private IEnumerator PlayScheduleTone(Schedule schedule, bool forwards)
	{
		int[] tones = schedule.GetTones();
		int num2;
		for (int i = 0; i < tones.Length; i = num2 + 1)
		{
			int num = (forwards ? i : (tones.Length - 1 - i));
			this.PlayTone(tones[num], forwards);
			yield return SequenceUtil.WaitForSeconds(TuningData<ScheduleManager.Tuning>.Get().toneSpacingSeconds);
			num2 = i;
		}
		yield break;
	}

	// Token: 0x06004389 RID: 17289 RVA: 0x0017DA98 File Offset: 0x0017BC98
	private void PlayTone(int pitch, bool forwards)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("WorkChime_tone", false), Vector3.zero, 1f);
		eventInstance.setParameterByName("WorkChime_pitch", (float)pitch, false);
		eventInstance.setParameterByName("WorkChime_start", (float)(forwards ? 1 : 0), false);
		KFMOD.EndOneShot(eventInstance);
	}

	// Token: 0x04002CFA RID: 11514
	[Serialize]
	private List<Schedule> schedules;

	// Token: 0x04002CFB RID: 11515
	[Serialize]
	private int lastIdx;

	// Token: 0x04002CFC RID: 11516
	[Serialize]
	private int scheduleNameIncrementor;

	// Token: 0x04002CFE RID: 11518
	public static ScheduleManager Instance;

	// Token: 0x020016EB RID: 5867
	public class Tuning : TuningData<ScheduleManager.Tuning>
	{
		// Token: 0x04006B5A RID: 27482
		public float toneSpacingSeconds;

		// Token: 0x04006B5B RID: 27483
		public int minToneIndex;

		// Token: 0x04006B5C RID: 27484
		public int maxToneIndex;

		// Token: 0x04006B5D RID: 27485
		public int firstLastToneSpacing;
	}
}
