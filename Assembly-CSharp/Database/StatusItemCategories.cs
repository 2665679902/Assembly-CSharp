using System;

namespace Database
{
	// Token: 0x02000CB6 RID: 3254
	public class StatusItemCategories : ResourceSet<StatusItemCategory>
	{
		// Token: 0x060065FD RID: 26109 RVA: 0x002722C8 File Offset: 0x002704C8
		public StatusItemCategories(ResourceSet parent)
			: base("StatusItemCategories", parent)
		{
			this.Main = new StatusItemCategory("Main", this, "Main");
			this.Role = new StatusItemCategory("Role", this, "Role");
			this.Power = new StatusItemCategory("Power", this, "Power");
			this.Toilet = new StatusItemCategory("Toilet", this, "Toilet");
			this.Research = new StatusItemCategory("Research", this, "Research");
			this.Hitpoints = new StatusItemCategory("Hitpoints", this, "Hitpoints");
			this.Suffocation = new StatusItemCategory("Suffocation", this, "Suffocation");
			this.WoundEffects = new StatusItemCategory("WoundEffects", this, "WoundEffects");
			this.EntityReceptacle = new StatusItemCategory("EntityReceptacle", this, "EntityReceptacle");
			this.PreservationState = new StatusItemCategory("PreservationState", this, "PreservationState");
			this.PreservationTemperature = new StatusItemCategory("PreservationTemperature", this, "PreservationTemperature");
			this.PreservationAtmosphere = new StatusItemCategory("PreservationAtmosphere", this, "PreservationAtmosphere");
			this.ExhaustTemperature = new StatusItemCategory("ExhaustTemperature", this, "ExhaustTemperature");
			this.OperatingEnergy = new StatusItemCategory("OperatingEnergy", this, "OperatingEnergy");
			this.AccessControl = new StatusItemCategory("AccessControl", this, "AccessControl");
			this.RequiredRoom = new StatusItemCategory("RequiredRoom", this, "RequiredRoom");
			this.Yield = new StatusItemCategory("Yield", this, "Yield");
			this.Heat = new StatusItemCategory("Heat", this, "Heat");
			this.Stored = new StatusItemCategory("Stored", this, "Stored");
			this.Ownable = new StatusItemCategory("Ownable", this, "Ownable");
		}

		// Token: 0x04004A51 RID: 19025
		public StatusItemCategory Main;

		// Token: 0x04004A52 RID: 19026
		public StatusItemCategory Role;

		// Token: 0x04004A53 RID: 19027
		public StatusItemCategory Power;

		// Token: 0x04004A54 RID: 19028
		public StatusItemCategory Toilet;

		// Token: 0x04004A55 RID: 19029
		public StatusItemCategory Research;

		// Token: 0x04004A56 RID: 19030
		public StatusItemCategory Hitpoints;

		// Token: 0x04004A57 RID: 19031
		public StatusItemCategory Suffocation;

		// Token: 0x04004A58 RID: 19032
		public StatusItemCategory WoundEffects;

		// Token: 0x04004A59 RID: 19033
		public StatusItemCategory EntityReceptacle;

		// Token: 0x04004A5A RID: 19034
		public StatusItemCategory PreservationState;

		// Token: 0x04004A5B RID: 19035
		public StatusItemCategory PreservationAtmosphere;

		// Token: 0x04004A5C RID: 19036
		public StatusItemCategory PreservationTemperature;

		// Token: 0x04004A5D RID: 19037
		public StatusItemCategory ExhaustTemperature;

		// Token: 0x04004A5E RID: 19038
		public StatusItemCategory OperatingEnergy;

		// Token: 0x04004A5F RID: 19039
		public StatusItemCategory AccessControl;

		// Token: 0x04004A60 RID: 19040
		public StatusItemCategory RequiredRoom;

		// Token: 0x04004A61 RID: 19041
		public StatusItemCategory Yield;

		// Token: 0x04004A62 RID: 19042
		public StatusItemCategory Heat;

		// Token: 0x04004A63 RID: 19043
		public StatusItemCategory Stored;

		// Token: 0x04004A64 RID: 19044
		public StatusItemCategory Ownable;
	}
}
