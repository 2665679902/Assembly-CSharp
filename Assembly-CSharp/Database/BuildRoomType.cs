using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CCF RID: 3279
	public class BuildRoomType : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600666F RID: 26223 RVA: 0x0027608F File Offset: 0x0027428F
		public BuildRoomType(RoomType roomType)
		{
			this.roomType = roomType;
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x002760A0 File Offset: 0x002742A0
		public override bool Success()
		{
			using (List<Room>.Enumerator enumerator = Game.Instance.roomProber.rooms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.roomType == this.roomType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x00276108 File Offset: 0x00274308
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.roomType = Db.Get().RoomTypes.Get(text);
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x00276132 File Offset: 0x00274332
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_A_ROOM, this.roomType.Name);
		}

		// Token: 0x04004ACA RID: 19146
		private RoomType roomType;
	}
}
