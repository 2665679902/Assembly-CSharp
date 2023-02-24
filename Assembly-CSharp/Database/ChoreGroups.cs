using System;
using Klei.AI;
using STRINGS;

namespace Database
{
	// Token: 0x02000C86 RID: 3206
	public class ChoreGroups : ResourceSet<ChoreGroup>
	{
		// Token: 0x06006555 RID: 25941 RVA: 0x00263A78 File Offset: 0x00261C78
		private ChoreGroup Add(string id, string name, Klei.AI.Attribute attribute, string sprite, int default_personal_priority, bool user_prioritizable = true)
		{
			ChoreGroup choreGroup = new ChoreGroup(id, name, attribute, sprite, default_personal_priority, user_prioritizable);
			base.Add(choreGroup);
			return choreGroup;
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x00263AA0 File Offset: 0x00261CA0
		public ChoreGroups(ResourceSet parent)
			: base("ChoreGroups", parent)
		{
			this.Combat = this.Add("Combat", DUPLICANTS.CHOREGROUPS.COMBAT.NAME, Db.Get().Attributes.Digging, "icon_errand_combat", 5, true);
			this.LifeSupport = this.Add("LifeSupport", DUPLICANTS.CHOREGROUPS.LIFESUPPORT.NAME, Db.Get().Attributes.LifeSupport, "icon_errand_life_support", 5, true);
			this.Toggle = this.Add("Toggle", DUPLICANTS.CHOREGROUPS.TOGGLE.NAME, Db.Get().Attributes.Toggle, "icon_errand_toggle", 5, true);
			this.MedicalAid = this.Add("MedicalAid", DUPLICANTS.CHOREGROUPS.MEDICALAID.NAME, Db.Get().Attributes.Caring, "icon_errand_care", 4, true);
			if (DlcManager.FeatureClusterSpaceEnabled())
			{
				this.Rocketry = this.Add("Rocketry", DUPLICANTS.CHOREGROUPS.ROCKETRY.NAME, Db.Get().Attributes.SpaceNavigation, "icon_errand_rocketry", 4, true);
			}
			this.Basekeeping = this.Add("Basekeeping", DUPLICANTS.CHOREGROUPS.BASEKEEPING.NAME, Db.Get().Attributes.Strength, "icon_errand_tidy", 4, true);
			this.Cook = this.Add("Cook", DUPLICANTS.CHOREGROUPS.COOK.NAME, Db.Get().Attributes.Cooking, "icon_errand_cook", 3, true);
			this.Art = this.Add("Art", DUPLICANTS.CHOREGROUPS.ART.NAME, Db.Get().Attributes.Art, "icon_errand_art", 3, true);
			this.Research = this.Add("Research", DUPLICANTS.CHOREGROUPS.RESEARCH.NAME, Db.Get().Attributes.Learning, "icon_errand_research", 3, true);
			this.MachineOperating = this.Add("MachineOperating", DUPLICANTS.CHOREGROUPS.MACHINEOPERATING.NAME, Db.Get().Attributes.Machinery, "icon_errand_operate", 3, true);
			this.Farming = this.Add("Farming", DUPLICANTS.CHOREGROUPS.FARMING.NAME, Db.Get().Attributes.Botanist, "icon_errand_farm", 3, true);
			this.Ranching = this.Add("Ranching", DUPLICANTS.CHOREGROUPS.RANCHING.NAME, Db.Get().Attributes.Ranching, "icon_errand_ranch", 3, true);
			this.Build = this.Add("Build", DUPLICANTS.CHOREGROUPS.BUILD.NAME, Db.Get().Attributes.Construction, "icon_errand_toggle", 2, true);
			this.Dig = this.Add("Dig", DUPLICANTS.CHOREGROUPS.DIG.NAME, Db.Get().Attributes.Digging, "icon_errand_dig", 2, true);
			this.Hauling = this.Add("Hauling", DUPLICANTS.CHOREGROUPS.HAULING.NAME, Db.Get().Attributes.Strength, "icon_errand_supply", 1, true);
			this.Storage = this.Add("Storage", DUPLICANTS.CHOREGROUPS.STORAGE.NAME, Db.Get().Attributes.Strength, "icon_errand_storage", 1, true);
			this.Recreation = this.Add("Recreation", DUPLICANTS.CHOREGROUPS.RECREATION.NAME, Db.Get().Attributes.Strength, "icon_errand_storage", 1, false);
			Debug.Assert(true);
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00263E08 File Offset: 0x00262008
		public ChoreGroup FindByHash(HashedString id)
		{
			ChoreGroup choreGroup = null;
			foreach (ChoreGroup choreGroup2 in Db.Get().ChoreGroups.resources)
			{
				if (choreGroup2.IdHash == id)
				{
					choreGroup = choreGroup2;
					break;
				}
			}
			return choreGroup;
		}

		// Token: 0x04004782 RID: 18306
		public ChoreGroup Build;

		// Token: 0x04004783 RID: 18307
		public ChoreGroup Basekeeping;

		// Token: 0x04004784 RID: 18308
		public ChoreGroup Cook;

		// Token: 0x04004785 RID: 18309
		public ChoreGroup Art;

		// Token: 0x04004786 RID: 18310
		public ChoreGroup Dig;

		// Token: 0x04004787 RID: 18311
		public ChoreGroup Research;

		// Token: 0x04004788 RID: 18312
		public ChoreGroup Farming;

		// Token: 0x04004789 RID: 18313
		public ChoreGroup Ranching;

		// Token: 0x0400478A RID: 18314
		public ChoreGroup Hauling;

		// Token: 0x0400478B RID: 18315
		public ChoreGroup Storage;

		// Token: 0x0400478C RID: 18316
		public ChoreGroup MachineOperating;

		// Token: 0x0400478D RID: 18317
		public ChoreGroup MedicalAid;

		// Token: 0x0400478E RID: 18318
		public ChoreGroup Combat;

		// Token: 0x0400478F RID: 18319
		public ChoreGroup LifeSupport;

		// Token: 0x04004790 RID: 18320
		public ChoreGroup Toggle;

		// Token: 0x04004791 RID: 18321
		public ChoreGroup Recreation;

		// Token: 0x04004792 RID: 18322
		public ChoreGroup Rocketry;
	}
}
