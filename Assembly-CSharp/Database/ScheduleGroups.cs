using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CAE RID: 3246
	public class ScheduleGroups : ResourceSet<ScheduleGroup>
	{
		// Token: 0x060065DD RID: 26077 RVA: 0x00270A98 File Offset: 0x0026EC98
		public ScheduleGroup Add(string id, int defaultSegments, string name, string description, string notificationTooltip, List<ScheduleBlockType> allowedTypes, bool alarm = false)
		{
			ScheduleGroup scheduleGroup = new ScheduleGroup(id, this, defaultSegments, name, description, notificationTooltip, allowedTypes, alarm);
			this.allGroups.Add(scheduleGroup);
			return scheduleGroup;
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x00270AC4 File Offset: 0x0026ECC4
		public ScheduleGroups(ResourceSet parent)
			: base("ScheduleGroups", parent)
		{
			this.allGroups = new List<ScheduleGroup>();
			this.Hygene = this.Add("Hygene", 1, UI.SCHEDULEGROUPS.HYGENE.NAME, UI.SCHEDULEGROUPS.HYGENE.DESCRIPTION, UI.SCHEDULEGROUPS.HYGENE.NOTIFICATION_TOOLTIP, new List<ScheduleBlockType>
			{
				Db.Get().ScheduleBlockTypes.Hygiene,
				Db.Get().ScheduleBlockTypes.Work
			}, false);
			this.Worktime = this.Add("Worktime", 18, UI.SCHEDULEGROUPS.WORKTIME.NAME, UI.SCHEDULEGROUPS.WORKTIME.DESCRIPTION, UI.SCHEDULEGROUPS.WORKTIME.NOTIFICATION_TOOLTIP, new List<ScheduleBlockType> { Db.Get().ScheduleBlockTypes.Work }, true);
			this.Recreation = this.Add("Recreation", 2, UI.SCHEDULEGROUPS.RECREATION.NAME, UI.SCHEDULEGROUPS.RECREATION.DESCRIPTION, UI.SCHEDULEGROUPS.RECREATION.NOTIFICATION_TOOLTIP, new List<ScheduleBlockType>
			{
				Db.Get().ScheduleBlockTypes.Hygiene,
				Db.Get().ScheduleBlockTypes.Eat,
				Db.Get().ScheduleBlockTypes.Recreation,
				Db.Get().ScheduleBlockTypes.Work
			}, false);
			this.Sleep = this.Add("Sleep", 3, UI.SCHEDULEGROUPS.SLEEP.NAME, UI.SCHEDULEGROUPS.SLEEP.DESCRIPTION, UI.SCHEDULEGROUPS.SLEEP.NOTIFICATION_TOOLTIP, new List<ScheduleBlockType> { Db.Get().ScheduleBlockTypes.Sleep }, false);
			int num = 0;
			foreach (ScheduleGroup scheduleGroup in this.allGroups)
			{
				num += scheduleGroup.defaultSegments;
			}
			Debug.Assert(num == 24, "Default schedule groups must add up to exactly 1 cycle!");
		}

		// Token: 0x060065DF RID: 26079 RVA: 0x00270CC4 File Offset: 0x0026EEC4
		public ScheduleGroup FindGroupForScheduleTypes(List<ScheduleBlockType> types)
		{
			foreach (ScheduleGroup scheduleGroup in this.allGroups)
			{
				if (Schedule.AreScheduleTypesIdentical(scheduleGroup.allowedTypes, types))
				{
					return scheduleGroup;
				}
			}
			return null;
		}

		// Token: 0x04004A0A RID: 18954
		public List<ScheduleGroup> allGroups;

		// Token: 0x04004A0B RID: 18955
		public ScheduleGroup Hygene;

		// Token: 0x04004A0C RID: 18956
		public ScheduleGroup Worktime;

		// Token: 0x04004A0D RID: 18957
		public ScheduleGroup Recreation;

		// Token: 0x04004A0E RID: 18958
		public ScheduleGroup Sleep;
	}
}
