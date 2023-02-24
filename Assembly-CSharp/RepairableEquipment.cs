using System;
using KSerialization;

// Token: 0x020008D4 RID: 2260
public class RepairableEquipment : KMonoBehaviour
{
	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x06004101 RID: 16641 RVA: 0x0016C3AA File Offset: 0x0016A5AA
	// (set) Token: 0x06004102 RID: 16642 RVA: 0x0016C3B7 File Offset: 0x0016A5B7
	public EquipmentDef def
	{
		get
		{
			return this.defHandle.Get<EquipmentDef>();
		}
		set
		{
			this.defHandle.Set<EquipmentDef>(value);
		}
	}

	// Token: 0x06004103 RID: 16643 RVA: 0x0016C3C8 File Offset: 0x0016A5C8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.def.AdditionalTags != null)
		{
			foreach (Tag tag in this.def.AdditionalTags)
			{
				base.GetComponent<KPrefabID>().AddTag(tag, false);
			}
		}
	}

	// Token: 0x06004104 RID: 16644 RVA: 0x0016C418 File Offset: 0x0016A618
	protected override void OnSpawn()
	{
		if (!this.facadeID.IsNullOrWhiteSpace())
		{
			KAnim.Build.Symbol symbol = Db.GetEquippableFacades().Get(this.facadeID).AnimFile.GetData().build.GetSymbol("object");
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			component.TryRemoveSymbolOverride("object", 0);
			component.AddSymbolOverride("object", symbol, 0);
		}
	}

	// Token: 0x04002B60 RID: 11104
	public DefHandle defHandle;

	// Token: 0x04002B61 RID: 11105
	[Serialize]
	public string facadeID;
}
