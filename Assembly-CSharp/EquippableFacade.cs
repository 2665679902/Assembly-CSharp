using System;
using KSerialization;

// Token: 0x02000756 RID: 1878
public class EquippableFacade : KMonoBehaviour
{
	// Token: 0x060033B3 RID: 13235 RVA: 0x00116774 File Offset: 0x00114974
	public static void AddFacadeToEquippable(Equippable equippable, string facadeID)
	{
		EquippableFacade equippableFacade = equippable.gameObject.AddOrGet<EquippableFacade>();
		equippableFacade.FacadeID = facadeID;
		equippableFacade.BuildOverride = Db.GetEquippableFacades().Get(facadeID).BuildOverride;
		equippableFacade.ApplyAnimOverride();
	}

	// Token: 0x060033B4 RID: 13236 RVA: 0x001167A3 File Offset: 0x001149A3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OverrideName();
		this.ApplyAnimOverride();
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x060033B5 RID: 13237 RVA: 0x001167B7 File Offset: 0x001149B7
	// (set) Token: 0x060033B6 RID: 13238 RVA: 0x001167BF File Offset: 0x001149BF
	public string FacadeID
	{
		get
		{
			return this._facadeID;
		}
		private set
		{
			this._facadeID = value;
			this.OverrideName();
		}
	}

	// Token: 0x060033B7 RID: 13239 RVA: 0x001167CE File Offset: 0x001149CE
	public void ApplyAnimOverride()
	{
		if (this.FacadeID.IsNullOrWhiteSpace())
		{
			return;
		}
		base.GetComponent<KBatchedAnimController>().SwapAnims(new KAnimFile[] { Db.GetEquippableFacades().Get(this.FacadeID).AnimFile });
	}

	// Token: 0x060033B8 RID: 13240 RVA: 0x00116807 File Offset: 0x00114A07
	private void OverrideName()
	{
		base.GetComponent<KSelectable>().SetName(EquippableFacade.GetNameOverride(base.GetComponent<Equippable>().def.Id, this.FacadeID));
	}

	// Token: 0x060033B9 RID: 13241 RVA: 0x0011682F File Offset: 0x00114A2F
	public static string GetNameOverride(string defID, string facadeID)
	{
		if (facadeID.IsNullOrWhiteSpace())
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + defID.ToUpper() + ".NAME");
		}
		return Db.GetEquippableFacades().Get(facadeID).Name;
	}

	// Token: 0x04001FBF RID: 8127
	[Serialize]
	private string _facadeID;

	// Token: 0x04001FC0 RID: 8128
	[Serialize]
	public string BuildOverride;
}
