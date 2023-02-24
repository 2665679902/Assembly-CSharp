using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD0 RID: 3280
	public class BuildNRoomTypes : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006673 RID: 26227 RVA: 0x0027614E File Offset: 0x0027434E
		public BuildNRoomTypes(RoomType roomType, int numToCreate = 1)
		{
			this.roomType = roomType;
			this.numToCreate = numToCreate;
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x00276164 File Offset: 0x00274364
		public override bool Success()
		{
			int num = 0;
			using (List<Room>.Enumerator enumerator = Game.Instance.roomProber.rooms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.roomType == this.roomType)
					{
						num++;
					}
				}
			}
			return num >= this.numToCreate;
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x002761D8 File Offset: 0x002743D8
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.roomType = Db.Get().RoomTypes.Get(text);
			this.numToCreate = reader.ReadInt32();
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x00276210 File Offset: 0x00274410
		public override string GetProgress(bool complete)
		{
			int num = 0;
			using (List<Room>.Enumerator enumerator = Game.Instance.roomProber.rooms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.roomType == this.roomType)
					{
						num++;
					}
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_N_ROOMS, this.roomType.Name, complete ? this.numToCreate : num, this.numToCreate);
		}

		// Token: 0x04004ACB RID: 19147
		private RoomType roomType;

		// Token: 0x04004ACC RID: 19148
		private int numToCreate;
	}
}
