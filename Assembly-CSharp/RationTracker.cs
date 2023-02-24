using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020008B3 RID: 2227
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/RationTracker")]
public class RationTracker : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06004002 RID: 16386 RVA: 0x0016576C File Offset: 0x0016396C
	public static void DestroyInstance()
	{
		RationTracker.instance = null;
	}

	// Token: 0x06004003 RID: 16387 RVA: 0x00165774 File Offset: 0x00163974
	public static RationTracker Get()
	{
		return RationTracker.instance;
	}

	// Token: 0x06004004 RID: 16388 RVA: 0x0016577B File Offset: 0x0016397B
	protected override void OnPrefabInit()
	{
		RationTracker.instance = this;
	}

	// Token: 0x06004005 RID: 16389 RVA: 0x00165783 File Offset: 0x00163983
	protected override void OnSpawn()
	{
		base.Subscribe<RationTracker>(631075836, RationTracker.OnNewDayDelegate);
	}

	// Token: 0x06004006 RID: 16390 RVA: 0x00165796 File Offset: 0x00163996
	private void OnNewDay(object data)
	{
		this.previousFrame = this.currentFrame;
		this.currentFrame = default(RationTracker.Frame);
	}

	// Token: 0x06004007 RID: 16391 RVA: 0x001657B0 File Offset: 0x001639B0
	public float CountRations(Dictionary<string, float> unitCountByFoodType, WorldInventory inventory, bool excludeUnreachable = true)
	{
		float num = 0f;
		ICollection<Pickupable> pickupables = inventory.GetPickupables(GameTags.Edible, false);
		if (pickupables != null)
		{
			foreach (Pickupable pickupable in pickupables)
			{
				if (!pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
				{
					Edible component = pickupable.GetComponent<Edible>();
					num += component.Calories;
					if (unitCountByFoodType != null)
					{
						if (!unitCountByFoodType.ContainsKey(component.FoodID))
						{
							unitCountByFoodType[component.FoodID] = 0f;
						}
						string foodID = component.FoodID;
						unitCountByFoodType[foodID] += component.Units;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06004008 RID: 16392 RVA: 0x0016587C File Offset: 0x00163A7C
	public float CountRationsByFoodType(string foodID, WorldInventory inventory, bool excludeUnreachable = true)
	{
		float num = 0f;
		ICollection<Pickupable> pickupables = inventory.GetPickupables(GameTags.Edible, false);
		if (pickupables != null)
		{
			foreach (Pickupable pickupable in pickupables)
			{
				if (!pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
				{
					Edible component = pickupable.GetComponent<Edible>();
					if (component.FoodID == foodID)
					{
						num += component.Calories;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06004009 RID: 16393 RVA: 0x00165908 File Offset: 0x00163B08
	public void RegisterCaloriesProduced(float calories)
	{
		this.currentFrame.caloriesProduced = this.currentFrame.caloriesProduced + calories;
	}

	// Token: 0x0600400A RID: 16394 RVA: 0x0016591C File Offset: 0x00163B1C
	public void RegisterRationsConsumed(Edible edible)
	{
		this.currentFrame.caloriesConsumed = this.currentFrame.caloriesConsumed + edible.caloriesConsumed;
		if (!this.caloriesConsumedByFood.ContainsKey(edible.FoodInfo.Id))
		{
			this.caloriesConsumedByFood.Add(edible.FoodInfo.Id, edible.caloriesConsumed);
			return;
		}
		Dictionary<string, float> dictionary = this.caloriesConsumedByFood;
		string id = edible.FoodInfo.Id;
		dictionary[id] += edible.caloriesConsumed;
	}

	// Token: 0x0600400B RID: 16395 RVA: 0x0016599C File Offset: 0x00163B9C
	public float GetCaloiresConsumedByFood(List<string> foodTypes)
	{
		float num = 0f;
		foreach (string text in foodTypes)
		{
			if (this.caloriesConsumedByFood.ContainsKey(text))
			{
				num += this.caloriesConsumedByFood[text];
			}
		}
		return num;
	}

	// Token: 0x0600400C RID: 16396 RVA: 0x00165A08 File Offset: 0x00163C08
	public float GetCaloriesConsumed()
	{
		float num = 0f;
		foreach (KeyValuePair<string, float> keyValuePair in this.caloriesConsumedByFood)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x040029FA RID: 10746
	private static RationTracker instance;

	// Token: 0x040029FB RID: 10747
	[Serialize]
	public RationTracker.Frame currentFrame;

	// Token: 0x040029FC RID: 10748
	[Serialize]
	public RationTracker.Frame previousFrame;

	// Token: 0x040029FD RID: 10749
	[Serialize]
	public Dictionary<string, float> caloriesConsumedByFood = new Dictionary<string, float>();

	// Token: 0x040029FE RID: 10750
	private static readonly EventSystem.IntraObjectHandler<RationTracker> OnNewDayDelegate = new EventSystem.IntraObjectHandler<RationTracker>(delegate(RationTracker component, object data)
	{
		component.OnNewDay(data);
	});

	// Token: 0x02001686 RID: 5766
	public struct Frame
	{
		// Token: 0x04006A11 RID: 27153
		public float caloriesProduced;

		// Token: 0x04006A12 RID: 27154
		public float caloriesConsumed;
	}
}
