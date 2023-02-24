using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020005D4 RID: 1492
[AddComponentMenu("KMonoBehaviour/scripts/ItemPedestal")]
public class ItemPedestal : KMonoBehaviour
{
	// Token: 0x0600253C RID: 9532 RVA: 0x000C93C8 File Offset: 0x000C75C8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ItemPedestal>(-731304873, ItemPedestal.OnOccupantChangedDelegate);
		if (this.receptacle.Occupant)
		{
			KBatchedAnimController component = this.receptacle.Occupant.GetComponent<KBatchedAnimController>();
			if (component)
			{
				component.enabled = true;
				component.sceneLayer = Grid.SceneLayer.Move;
			}
			this.OnOccupantChanged(this.receptacle.Occupant);
		}
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x000C9438 File Offset: 0x000C7638
	private void OnOccupantChanged(object data)
	{
		Attributes attributes = this.GetAttributes();
		if (this.decorModifier != null)
		{
			attributes.Remove(this.decorModifier);
			attributes.Remove(this.decorRadiusModifier);
			this.decorModifier = null;
			this.decorRadiusModifier = null;
		}
		if (data != null)
		{
			GameObject gameObject = (GameObject)data;
			UnityEngine.Object component = gameObject.GetComponent<DecorProvider>();
			float num = 5f;
			float num2 = 3f;
			if (component != null)
			{
				num = Mathf.Max(Db.Get().BuildingAttributes.Decor.Lookup(gameObject).GetTotalValue() * 2f, 5f);
				num2 = Db.Get().BuildingAttributes.DecorRadius.Lookup(gameObject).GetTotalValue() + 2f;
			}
			string text = string.Format(BUILDINGS.PREFABS.ITEMPEDESTAL.DISPLAYED_ITEM_FMT, gameObject.GetComponent<KPrefabID>().PrefabTag.ProperName());
			this.decorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, num, text, false, false, true);
			this.decorRadiusModifier = new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, num2, text, false, false, true);
			attributes.Add(this.decorModifier);
			attributes.Add(this.decorRadiusModifier);
		}
	}

	// Token: 0x04001598 RID: 5528
	[MyCmpReq]
	protected SingleEntityReceptacle receptacle;

	// Token: 0x04001599 RID: 5529
	[MyCmpReq]
	private DecorProvider decorProvider;

	// Token: 0x0400159A RID: 5530
	private const float MINIMUM_DECOR = 5f;

	// Token: 0x0400159B RID: 5531
	private const float STORED_DECOR_MODIFIER = 2f;

	// Token: 0x0400159C RID: 5532
	private const int RADIUS_BONUS = 2;

	// Token: 0x0400159D RID: 5533
	private AttributeModifier decorModifier;

	// Token: 0x0400159E RID: 5534
	private AttributeModifier decorRadiusModifier;

	// Token: 0x0400159F RID: 5535
	private static readonly EventSystem.IntraObjectHandler<ItemPedestal> OnOccupantChangedDelegate = new EventSystem.IntraObjectHandler<ItemPedestal>(delegate(ItemPedestal component, object data)
	{
		component.OnOccupantChanged(data);
	});
}
