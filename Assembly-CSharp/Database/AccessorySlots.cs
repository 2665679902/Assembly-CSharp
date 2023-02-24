using System;

namespace Database
{
	// Token: 0x02000C72 RID: 3186
	public class AccessorySlots : ResourceSet<AccessorySlot>
	{
		// Token: 0x0600650F RID: 25871 RVA: 0x0025B82C File Offset: 0x00259A2C
		public AccessorySlots(ResourceSet parent)
			: base("AccessorySlots", parent)
		{
			parent = Db.Get().Accessories;
			KAnimFile anim = Assets.GetAnim("head_swap_kanim");
			KAnimFile anim2 = Assets.GetAnim("body_comp_default_kanim");
			KAnimFile anim3 = Assets.GetAnim("body_swap_kanim");
			KAnimFile anim4 = Assets.GetAnim("hair_swap_kanim");
			KAnimFile anim5 = Assets.GetAnim("hat_swap_kanim");
			this.Eyes = new AccessorySlot("Eyes", this, anim, 0);
			this.Hair = new AccessorySlot("Hair", this, anim4, 0);
			this.HeadShape = new AccessorySlot("HeadShape", this, anim, 0);
			this.Mouth = new AccessorySlot("Mouth", this, anim, 0);
			this.Hat = new AccessorySlot("Hat", this, anim5, 4);
			this.HatHair = new AccessorySlot("Hat_Hair", this, anim4, 0);
			this.HeadEffects = new AccessorySlot("HeadFX", this, anim, 0);
			this.Body = new AccessorySlot("Torso", this, new KAnimHashedString("torso"), anim3, null, 0);
			this.Arm = new AccessorySlot("Arm_Sleeve", this, new KAnimHashedString("arm_sleeve"), anim3, null, 0);
			this.ArmLower = new AccessorySlot("Arm_Lower_Sleeve", this, new KAnimHashedString("arm_lower_sleeve"), anim3, null, 0);
			this.Belt = new AccessorySlot("Belt", this, new KAnimHashedString("belt"), anim2, null, 0);
			this.Neck = new AccessorySlot("Neck", this, new KAnimHashedString("neck"), anim2, null, 0);
			this.Pelvis = new AccessorySlot("Pelvis", this, new KAnimHashedString("pelvis"), anim2, null, 0);
			this.Foot = new AccessorySlot("Foot", this, new KAnimHashedString("foot"), anim2, Assets.GetAnim("shoes_basic_black_kanim"), 0);
			this.Leg = new AccessorySlot("Leg", this, new KAnimHashedString("leg"), anim2, null, 0);
			this.Necklace = new AccessorySlot("Necklace", this, new KAnimHashedString("necklace"), anim2, null, 0);
			this.Cuff = new AccessorySlot("Cuff", this, new KAnimHashedString("cuff"), anim2, null, 0);
			this.Hand = new AccessorySlot("Hand", this, new KAnimHashedString("hand_paint"), anim2, null, 0);
			this.Skirt = new AccessorySlot("Skirt", this, new KAnimHashedString("skirt"), anim3, null, 0);
			this.ArmLowerSkin = new AccessorySlot("Arm_Lower", this, new KAnimHashedString("arm_lower"), anim3, null, 0);
			this.ArmUpperSkin = new AccessorySlot("Arm_Upper", this, new KAnimHashedString("arm_upper"), anim3, null, 0);
			this.LegSkin = new AccessorySlot("Leg_Skin", this, new KAnimHashedString("leg_skin"), anim3, null, 0);
			foreach (AccessorySlot accessorySlot in this.resources)
			{
				accessorySlot.AddAccessories(accessorySlot.AnimFile, parent);
			}
			Db.Get().Accessories.AddCustomAccessories(Assets.GetAnim(LonelyMinionConfig.BodyAnimFile), parent, this);
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x0025BB64 File Offset: 0x00259D64
		public AccessorySlot Find(KAnimHashedString symbol_name)
		{
			foreach (AccessorySlot accessorySlot in Db.Get().AccessorySlots.resources)
			{
				if (symbol_name == accessorySlot.targetSymbolId)
				{
					return accessorySlot;
				}
			}
			return null;
		}

		// Token: 0x040045D1 RID: 17873
		public AccessorySlot Eyes;

		// Token: 0x040045D2 RID: 17874
		public AccessorySlot Hair;

		// Token: 0x040045D3 RID: 17875
		public AccessorySlot HeadShape;

		// Token: 0x040045D4 RID: 17876
		public AccessorySlot Mouth;

		// Token: 0x040045D5 RID: 17877
		public AccessorySlot Body;

		// Token: 0x040045D6 RID: 17878
		public AccessorySlot Arm;

		// Token: 0x040045D7 RID: 17879
		public AccessorySlot ArmLower;

		// Token: 0x040045D8 RID: 17880
		public AccessorySlot Hat;

		// Token: 0x040045D9 RID: 17881
		public AccessorySlot HatHair;

		// Token: 0x040045DA RID: 17882
		public AccessorySlot HeadEffects;

		// Token: 0x040045DB RID: 17883
		public AccessorySlot Belt;

		// Token: 0x040045DC RID: 17884
		public AccessorySlot Neck;

		// Token: 0x040045DD RID: 17885
		public AccessorySlot Pelvis;

		// Token: 0x040045DE RID: 17886
		public AccessorySlot Leg;

		// Token: 0x040045DF RID: 17887
		public AccessorySlot Foot;

		// Token: 0x040045E0 RID: 17888
		public AccessorySlot Skirt;

		// Token: 0x040045E1 RID: 17889
		public AccessorySlot Necklace;

		// Token: 0x040045E2 RID: 17890
		public AccessorySlot Cuff;

		// Token: 0x040045E3 RID: 17891
		public AccessorySlot Hand;

		// Token: 0x040045E4 RID: 17892
		public AccessorySlot ArmLowerSkin;

		// Token: 0x040045E5 RID: 17893
		public AccessorySlot ArmUpperSkin;

		// Token: 0x040045E6 RID: 17894
		public AccessorySlot LegSkin;
	}
}
