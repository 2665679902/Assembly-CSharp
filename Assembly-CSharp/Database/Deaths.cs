using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000C8E RID: 3214
	public class Deaths : ResourceSet<Death>
	{
		// Token: 0x06006574 RID: 25972 RVA: 0x002690B8 File Offset: 0x002672B8
		public Deaths(ResourceSet parent)
			: base("Deaths", parent)
		{
			this.Generic = new Death("Generic", this, DUPLICANTS.DEATHS.GENERIC.NAME, DUPLICANTS.DEATHS.GENERIC.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Frozen = new Death("Frozen", this, DUPLICANTS.DEATHS.FROZEN.NAME, DUPLICANTS.DEATHS.FROZEN.DESCRIPTION, "death_freeze_trans", "death_freeze_solid");
			this.Suffocation = new Death("Suffocation", this, DUPLICANTS.DEATHS.SUFFOCATION.NAME, DUPLICANTS.DEATHS.SUFFOCATION.DESCRIPTION, "death_suffocation", "dead_on_back");
			this.Starvation = new Death("Starvation", this, DUPLICANTS.DEATHS.STARVATION.NAME, DUPLICANTS.DEATHS.STARVATION.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Overheating = new Death("Overheating", this, DUPLICANTS.DEATHS.OVERHEATING.NAME, DUPLICANTS.DEATHS.OVERHEATING.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Drowned = new Death("Drowned", this, DUPLICANTS.DEATHS.DROWNED.NAME, DUPLICANTS.DEATHS.DROWNED.DESCRIPTION, "death_suffocation", "dead_on_back");
			this.Explosion = new Death("Explosion", this, DUPLICANTS.DEATHS.EXPLOSION.NAME, DUPLICANTS.DEATHS.EXPLOSION.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Slain = new Death("Combat", this, DUPLICANTS.DEATHS.COMBAT.NAME, DUPLICANTS.DEATHS.COMBAT.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.FatalDisease = new Death("FatalDisease", this, DUPLICANTS.DEATHS.FATALDISEASE.NAME, DUPLICANTS.DEATHS.FATALDISEASE.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Radiation = new Death("Radiation", this, DUPLICANTS.DEATHS.RADIATION.NAME, DUPLICANTS.DEATHS.RADIATION.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.HitByHighEnergyParticle = new Death("HitByHighEnergyParticle", this, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.NAME, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.DeadBattery = new Death("DeadBattery", this, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.NAME, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.DESCRIPTION, "dead_on_back", "dead_on_back");
		}

		// Token: 0x0400485C RID: 18524
		public Death Generic;

		// Token: 0x0400485D RID: 18525
		public Death Frozen;

		// Token: 0x0400485E RID: 18526
		public Death Suffocation;

		// Token: 0x0400485F RID: 18527
		public Death Starvation;

		// Token: 0x04004860 RID: 18528
		public Death Slain;

		// Token: 0x04004861 RID: 18529
		public Death Overheating;

		// Token: 0x04004862 RID: 18530
		public Death Drowned;

		// Token: 0x04004863 RID: 18531
		public Death Explosion;

		// Token: 0x04004864 RID: 18532
		public Death FatalDisease;

		// Token: 0x04004865 RID: 18533
		public Death Radiation;

		// Token: 0x04004866 RID: 18534
		public Death HitByHighEnergyParticle;

		// Token: 0x04004867 RID: 18535
		public Death DeadBattery;
	}
}
