using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x02000751 RID: 1873
public class EquipmentDef : Def
{
	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x06003398 RID: 13208 RVA: 0x00116060 File Offset: 0x00114260
	public override string Name
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".NAME");
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06003399 RID: 13209 RVA: 0x00116086 File Offset: 0x00114286
	public string GenericName
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".GENERICNAME");
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x0600339A RID: 13210 RVA: 0x001160AC File Offset: 0x001142AC
	public string WornName
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".WORN_NAME");
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x0600339B RID: 13211 RVA: 0x001160D2 File Offset: 0x001142D2
	public string WornDesc
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".WORN_DESC");
		}
	}

	// Token: 0x04001F9C RID: 8092
	public string Id;

	// Token: 0x04001F9D RID: 8093
	public string Slot;

	// Token: 0x04001F9E RID: 8094
	public string FabricatorId;

	// Token: 0x04001F9F RID: 8095
	public float FabricationTime;

	// Token: 0x04001FA0 RID: 8096
	public string RecipeTechUnlock;

	// Token: 0x04001FA1 RID: 8097
	public SimHashes OutputElement;

	// Token: 0x04001FA2 RID: 8098
	public Dictionary<string, float> InputElementMassMap;

	// Token: 0x04001FA3 RID: 8099
	public float Mass;

	// Token: 0x04001FA4 RID: 8100
	public KAnimFile Anim;

	// Token: 0x04001FA5 RID: 8101
	public string SnapOn;

	// Token: 0x04001FA6 RID: 8102
	public string SnapOn1;

	// Token: 0x04001FA7 RID: 8103
	public KAnimFile BuildOverride;

	// Token: 0x04001FA8 RID: 8104
	public int BuildOverridePriority;

	// Token: 0x04001FA9 RID: 8105
	public bool IsBody;

	// Token: 0x04001FAA RID: 8106
	public List<AttributeModifier> AttributeModifiers;

	// Token: 0x04001FAB RID: 8107
	public string RecipeDescription;

	// Token: 0x04001FAC RID: 8108
	public List<Effect> EffectImmunites = new List<Effect>();

	// Token: 0x04001FAD RID: 8109
	public Action<Equippable> OnEquipCallBack;

	// Token: 0x04001FAE RID: 8110
	public Action<Equippable> OnUnequipCallBack;

	// Token: 0x04001FAF RID: 8111
	public EntityTemplates.CollisionShape CollisionShape;

	// Token: 0x04001FB0 RID: 8112
	public float width;

	// Token: 0x04001FB1 RID: 8113
	public float height = 0.325f;

	// Token: 0x04001FB2 RID: 8114
	public Tag[] AdditionalTags;

	// Token: 0x04001FB3 RID: 8115
	public string wornID;

	// Token: 0x04001FB4 RID: 8116
	public List<Descriptor> additionalDescriptors = new List<Descriptor>();
}
