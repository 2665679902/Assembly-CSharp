using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000CD5 RID: 3285
	public class CritterTypesWithTraits : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006688 RID: 26248 RVA: 0x00276770 File Offset: 0x00274970
		public CritterTypesWithTraits(List<Tag> critterTypes)
		{
			foreach (Tag tag in critterTypes)
			{
				if (!this.critterTypesToCheck.ContainsKey(tag))
				{
					this.critterTypesToCheck.Add(tag, false);
				}
			}
			this.hasTrait = false;
			this.trait = GameTags.Creatures.Wild;
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x00276800 File Offset: 0x00274A00
		public override bool Success()
		{
			HashSet<Tag> tamedCritterTypes = SaveGame.Instance.GetComponent<ColonyAchievementTracker>().tamedCritterTypes;
			bool flag = true;
			foreach (KeyValuePair<Tag, bool> keyValuePair in this.critterTypesToCheck)
			{
				flag = flag && tamedCritterTypes.Contains(keyValuePair.Key);
			}
			this.UpdateSavedState();
			return flag;
		}

		// Token: 0x0600668A RID: 26250 RVA: 0x0027687C File Offset: 0x00274A7C
		public void UpdateSavedState()
		{
			this.revisedCritterTypesToCheckState.Clear();
			HashSet<Tag> tamedCritterTypes = SaveGame.Instance.GetComponent<ColonyAchievementTracker>().tamedCritterTypes;
			foreach (KeyValuePair<Tag, bool> keyValuePair in this.critterTypesToCheck)
			{
				this.revisedCritterTypesToCheckState.Add(keyValuePair.Key, tamedCritterTypes.Contains(keyValuePair.Key));
			}
			foreach (KeyValuePair<Tag, bool> keyValuePair2 in this.revisedCritterTypesToCheckState)
			{
				this.critterTypesToCheck[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x00276958 File Offset: 0x00274B58
		public void Deserialize(IReader reader)
		{
			this.critterTypesToCheck = new Dictionary<Tag, bool>();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				bool flag = reader.ReadByte() > 0;
				this.critterTypesToCheck.Add(new Tag(text), flag);
			}
			this.hasTrait = reader.ReadByte() > 0;
			this.trait = GameTags.Creatures.Wild;
		}

		// Token: 0x04004AD1 RID: 19153
		public Dictionary<Tag, bool> critterTypesToCheck = new Dictionary<Tag, bool>();

		// Token: 0x04004AD2 RID: 19154
		private Tag trait;

		// Token: 0x04004AD3 RID: 19155
		private bool hasTrait;

		// Token: 0x04004AD4 RID: 19156
		private Dictionary<Tag, bool> revisedCritterTypesToCheckState = new Dictionary<Tag, bool>();
	}
}
