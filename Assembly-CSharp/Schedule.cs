using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200090A RID: 2314
[SerializationConfig(MemberSerialization.OptIn)]
public class Schedule : ISaveLoadable, IListableOption
{
	// Token: 0x06004365 RID: 17253 RVA: 0x0017CFCC File Offset: 0x0017B1CC
	public static int GetBlockIdx()
	{
		return Math.Min((int)(GameClock.Instance.GetCurrentCycleAsPercentage() * 24f), 23);
	}

	// Token: 0x06004366 RID: 17254 RVA: 0x0017CFE6 File Offset: 0x0017B1E6
	public static int GetLastBlockIdx()
	{
		return (Schedule.GetBlockIdx() + 24 - 1) % 24;
	}

	// Token: 0x06004367 RID: 17255 RVA: 0x0017CFF5 File Offset: 0x0017B1F5
	public void ClearNullReferences()
	{
		this.assigned.RemoveAll((Ref<Schedulable> x) => x.Get() == null);
	}

	// Token: 0x06004368 RID: 17256 RVA: 0x0017D024 File Offset: 0x0017B224
	public Schedule(string name, List<ScheduleGroup> defaultGroups, bool alarmActivated)
	{
		this.name = name;
		this.alarmActivated = alarmActivated;
		this.blocks = new List<ScheduleBlock>(24);
		this.assigned = new List<Ref<Schedulable>>();
		this.tones = this.GenerateTones();
		this.SetBlocksToGroupDefaults(defaultGroups);
	}

	// Token: 0x06004369 RID: 17257 RVA: 0x0017D078 File Offset: 0x0017B278
	public void SetBlocksToGroupDefaults(List<ScheduleGroup> defaultGroups)
	{
		this.blocks.Clear();
		int num = 0;
		for (int i = 0; i < defaultGroups.Count; i++)
		{
			ScheduleGroup scheduleGroup = defaultGroups[i];
			for (int j = 0; j < scheduleGroup.defaultSegments; j++)
			{
				this.blocks.Add(new ScheduleBlock(scheduleGroup.Name, scheduleGroup.allowedTypes, scheduleGroup.Id));
				num++;
			}
		}
		global::Debug.Assert(num == 24);
		this.Changed();
	}

	// Token: 0x0600436A RID: 17258 RVA: 0x0017D0F4 File Offset: 0x0017B2F4
	public void Tick()
	{
		ScheduleBlock block = this.GetBlock(Schedule.GetBlockIdx());
		ScheduleBlock block2 = this.GetBlock(Schedule.GetLastBlockIdx());
		if (!Schedule.AreScheduleTypesIdentical(block.allowed_types, block2.allowed_types))
		{
			ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(block.allowed_types);
			ScheduleGroup scheduleGroup2 = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(block2.allowed_types);
			if (this.alarmActivated && scheduleGroup2.alarm != scheduleGroup.alarm)
			{
				ScheduleManager.Instance.PlayScheduleAlarm(this, block, scheduleGroup.alarm);
			}
			foreach (Ref<Schedulable> @ref in this.GetAssigned())
			{
				@ref.Get().OnScheduleBlocksChanged(this);
			}
		}
		foreach (Ref<Schedulable> ref2 in this.GetAssigned())
		{
			ref2.Get().OnScheduleBlocksTick(this);
		}
	}

	// Token: 0x0600436B RID: 17259 RVA: 0x0017D214 File Offset: 0x0017B414
	string IListableOption.GetProperName()
	{
		return this.name;
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x0017D21C File Offset: 0x0017B41C
	public int[] GenerateTones()
	{
		int minToneIndex = TuningData<ScheduleManager.Tuning>.Get().minToneIndex;
		int maxToneIndex = TuningData<ScheduleManager.Tuning>.Get().maxToneIndex;
		int firstLastToneSpacing = TuningData<ScheduleManager.Tuning>.Get().firstLastToneSpacing;
		int[] array = new int[4];
		array[0] = UnityEngine.Random.Range(minToneIndex, maxToneIndex - firstLastToneSpacing + 1);
		array[1] = UnityEngine.Random.Range(minToneIndex, maxToneIndex + 1);
		array[2] = UnityEngine.Random.Range(minToneIndex, maxToneIndex + 1);
		array[3] = UnityEngine.Random.Range(array[0] + firstLastToneSpacing, maxToneIndex + 1);
		return array;
	}

	// Token: 0x0600436D RID: 17261 RVA: 0x0017D288 File Offset: 0x0017B488
	public List<Ref<Schedulable>> GetAssigned()
	{
		if (this.assigned == null)
		{
			this.assigned = new List<Ref<Schedulable>>();
		}
		return this.assigned;
	}

	// Token: 0x0600436E RID: 17262 RVA: 0x0017D2A3 File Offset: 0x0017B4A3
	public int[] GetTones()
	{
		if (this.tones == null)
		{
			this.tones = this.GenerateTones();
		}
		return this.tones;
	}

	// Token: 0x0600436F RID: 17263 RVA: 0x0017D2BF File Offset: 0x0017B4BF
	public void SetGroup(int idx, ScheduleGroup group)
	{
		if (0 <= idx && idx < this.blocks.Count)
		{
			this.blocks[idx] = new ScheduleBlock(group.Name, group.allowedTypes, group.Id);
			this.Changed();
		}
	}

	// Token: 0x06004370 RID: 17264 RVA: 0x0017D2FC File Offset: 0x0017B4FC
	private void Changed()
	{
		foreach (Ref<Schedulable> @ref in this.GetAssigned())
		{
			@ref.Get().OnScheduleChanged(this);
		}
		if (this.onChanged != null)
		{
			this.onChanged(this);
		}
	}

	// Token: 0x06004371 RID: 17265 RVA: 0x0017D368 File Offset: 0x0017B568
	public List<ScheduleBlock> GetBlocks()
	{
		return this.blocks;
	}

	// Token: 0x06004372 RID: 17266 RVA: 0x0017D370 File Offset: 0x0017B570
	public ScheduleBlock GetBlock(int idx)
	{
		return this.blocks[idx];
	}

	// Token: 0x06004373 RID: 17267 RVA: 0x0017D37E File Offset: 0x0017B57E
	public void Assign(Schedulable schedulable)
	{
		if (!this.IsAssigned(schedulable))
		{
			this.GetAssigned().Add(new Ref<Schedulable>(schedulable));
		}
		this.Changed();
	}

	// Token: 0x06004374 RID: 17268 RVA: 0x0017D3A0 File Offset: 0x0017B5A0
	public void Unassign(Schedulable schedulable)
	{
		for (int i = 0; i < this.GetAssigned().Count; i++)
		{
			if (this.GetAssigned()[i].Get() == schedulable)
			{
				this.GetAssigned().RemoveAt(i);
				break;
			}
		}
		this.Changed();
	}

	// Token: 0x06004375 RID: 17269 RVA: 0x0017D3F0 File Offset: 0x0017B5F0
	public bool IsAssigned(Schedulable schedulable)
	{
		using (List<Ref<Schedulable>>.Enumerator enumerator = this.GetAssigned().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get() == schedulable)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06004376 RID: 17270 RVA: 0x0017D450 File Offset: 0x0017B650
	public static bool AreScheduleTypesIdentical(List<ScheduleBlockType> a, List<ScheduleBlockType> b)
	{
		if (a.Count != b.Count)
		{
			return false;
		}
		foreach (ScheduleBlockType scheduleBlockType in a)
		{
			bool flag = false;
			foreach (ScheduleBlockType scheduleBlockType2 in b)
			{
				if (scheduleBlockType.IdHash == scheduleBlockType2.IdHash)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04002CF4 RID: 11508
	[Serialize]
	private List<ScheduleBlock> blocks;

	// Token: 0x04002CF5 RID: 11509
	[Serialize]
	private List<Ref<Schedulable>> assigned;

	// Token: 0x04002CF6 RID: 11510
	[Serialize]
	public string name;

	// Token: 0x04002CF7 RID: 11511
	[Serialize]
	public bool alarmActivated = true;

	// Token: 0x04002CF8 RID: 11512
	[Serialize]
	private int[] tones;

	// Token: 0x04002CF9 RID: 11513
	public Action<Schedule> onChanged;
}
