using System;
using KSerialization;

// Token: 0x020004DB RID: 1243
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class SubstanceSource : KMonoBehaviour
{
	// Token: 0x06001D75 RID: 7541 RVA: 0x0009D410 File Offset: 0x0009B610
	protected override void OnPrefabInit()
	{
		this.pickupable.SetWorkTime(SubstanceSource.MaxPickupTime);
	}

	// Token: 0x06001D76 RID: 7542 RVA: 0x0009D422 File Offset: 0x0009B622
	protected override void OnSpawn()
	{
		this.pickupable.SetWorkTime(10f);
	}

	// Token: 0x06001D77 RID: 7543
	protected abstract CellOffset[] GetOffsetGroup();

	// Token: 0x06001D78 RID: 7544
	protected abstract IChunkManager GetChunkManager();

	// Token: 0x06001D79 RID: 7545 RVA: 0x0009D434 File Offset: 0x0009B634
	public SimHashes GetElementID()
	{
		return this.primaryElement.ElementID;
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x0009D444 File Offset: 0x0009B644
	public Tag GetElementTag()
	{
		Tag tag = Tag.Invalid;
		if (base.gameObject != null && this.primaryElement != null && this.primaryElement.Element != null)
		{
			tag = this.primaryElement.Element.tag;
		}
		return tag;
	}

	// Token: 0x06001D7B RID: 7547 RVA: 0x0009D494 File Offset: 0x0009B694
	public Tag GetMaterialCategoryTag()
	{
		Tag tag = Tag.Invalid;
		if (base.gameObject != null && this.primaryElement != null && this.primaryElement.Element != null)
		{
			tag = this.primaryElement.Element.GetMaterialCategoryTag();
		}
		return tag;
	}

	// Token: 0x040010A1 RID: 4257
	private bool enableRefresh;

	// Token: 0x040010A2 RID: 4258
	private static readonly float MaxPickupTime = 8f;

	// Token: 0x040010A3 RID: 4259
	[MyCmpReq]
	public Pickupable pickupable;

	// Token: 0x040010A4 RID: 4260
	[MyCmpReq]
	private PrimaryElement primaryElement;
}
