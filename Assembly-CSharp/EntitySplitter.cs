﻿using System;
using Klei;
using UnityEngine;

// Token: 0x0200047C RID: 1148
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/EntitySplitter")]
public class EntitySplitter : KMonoBehaviour
{
	// Token: 0x060019A5 RID: 6565 RVA: 0x00089A38 File Offset: 0x00087C38
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Pickupable pickupable = base.GetComponent<Pickupable>();
		if (pickupable == null)
		{
			global::Debug.LogError(base.name + " does not have a pickupable component!");
		}
		Pickupable pickupable2 = pickupable;
		pickupable2.OnTake = (Func<float, Pickupable>)Delegate.Combine(pickupable2.OnTake, new Func<float, Pickupable>((float amount) => EntitySplitter.Split(pickupable, amount, null)));
		Rottable.Instance rottable = base.gameObject.GetSMI<Rottable.Instance>();
		pickupable.absorbable = true;
		pickupable.CanAbsorb = (Pickupable other) => EntitySplitter.CanFirstAbsorbSecond(pickupable, rottable, other, this.maxStackSize);
		base.Subscribe<EntitySplitter>(-2064133523, EntitySplitter.OnAbsorbDelegate);
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x00089AF4 File Offset: 0x00087CF4
	private static bool CanFirstAbsorbSecond(Pickupable pickupable, Rottable.Instance rottable, Pickupable other, float maxStackSize)
	{
		if (other == null)
		{
			return false;
		}
		KPrefabID component = pickupable.GetComponent<KPrefabID>();
		KPrefabID component2 = other.GetComponent<KPrefabID>();
		if (component == null)
		{
			return false;
		}
		if (component2 == null)
		{
			return false;
		}
		if (component.PrefabTag != component2.PrefabTag)
		{
			return false;
		}
		if (pickupable.TotalAmount + other.TotalAmount > maxStackSize)
		{
			return false;
		}
		if (pickupable.PrimaryElement.Mass + other.PrimaryElement.Mass > maxStackSize)
		{
			return false;
		}
		if (rottable != null)
		{
			Rottable.Instance smi = other.GetSMI<Rottable.Instance>();
			if (smi == null)
			{
				return false;
			}
			if (!rottable.IsRotLevelStackable(smi))
			{
				return false;
			}
		}
		bool flag = component.HasTag(GameTags.SpicedFood);
		if (flag != component2.HasTag(GameTags.SpicedFood))
		{
			return false;
		}
		Edible component3 = component.GetComponent<Edible>();
		Edible component4 = component.GetComponent<Edible>();
		if (flag && !component3.CanAbsorb(component4))
		{
			return false;
		}
		if (component.HasTag(GameTags.Seed) || component.HasTag(GameTags.CropSeed) || component.HasTag(GameTags.Compostable))
		{
			MutantPlant component5 = pickupable.GetComponent<MutantPlant>();
			MutantPlant component6 = other.GetComponent<MutantPlant>();
			if (component5 != null || component6 != null)
			{
				if (component5 == null != (component6 == null))
				{
					return false;
				}
				if (component.HasTag(GameTags.UnidentifiedSeed) != component2.HasTag(GameTags.UnidentifiedSeed))
				{
					return false;
				}
				if (component5.SubSpeciesID != component6.SubSpeciesID)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x00089C5C File Offset: 0x00087E5C
	public static Pickupable Split(Pickupable pickupable, float amount, GameObject prefab = null)
	{
		if (amount >= pickupable.TotalAmount && prefab == null)
		{
			return pickupable;
		}
		Storage storage = pickupable.storage;
		if (prefab == null)
		{
			prefab = Assets.GetPrefab(pickupable.GetComponent<KPrefabID>().PrefabTag);
		}
		GameObject gameObject = null;
		if (pickupable.transform.parent != null)
		{
			gameObject = pickupable.transform.parent.gameObject;
		}
		GameObject gameObject2 = GameUtil.KInstantiate(prefab, pickupable.transform.GetPosition(), Grid.SceneLayer.Ore, gameObject, null, 0);
		global::Debug.Assert(gameObject2 != null, "WTH, the GO is null, shouldn't happen on instantiate");
		Pickupable component = gameObject2.GetComponent<Pickupable>();
		if (component == null)
		{
			global::Debug.LogError("Edible::OnTake() No Pickupable component for " + gameObject2.name, gameObject2);
		}
		gameObject2.SetActive(true);
		component.TotalAmount = Mathf.Min(amount, pickupable.TotalAmount);
		component.PrimaryElement.Temperature = pickupable.PrimaryElement.Temperature;
		bool keepZeroMassObject = pickupable.PrimaryElement.KeepZeroMassObject;
		pickupable.PrimaryElement.KeepZeroMassObject = true;
		pickupable.TotalAmount -= amount;
		component.Trigger(1335436905, pickupable);
		pickupable.PrimaryElement.KeepZeroMassObject = keepZeroMassObject;
		pickupable.TotalAmount += 0f;
		if (storage != null)
		{
			storage.Trigger(-1697596308, pickupable.gameObject);
			storage.Trigger(-778359855, storage);
		}
		IExtendSplitting[] components = pickupable.GetComponents<IExtendSplitting>();
		if (components != null)
		{
			for (int i = 0; i < components.Length; i++)
			{
				components[i].OnSplitTick(component);
			}
		}
		return component;
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x00089DE8 File Offset: 0x00087FE8
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable != null)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			PrimaryElement primaryElement = pickupable.PrimaryElement;
			if (primaryElement != null)
			{
				float num = 0f;
				float mass = component.Mass;
				float mass2 = primaryElement.Mass;
				if (mass > 0f && mass2 > 0f)
				{
					num = SimUtil.CalculateFinalTemperature(mass, component.Temperature, mass2, primaryElement.Temperature);
				}
				else if (primaryElement.Mass > 0f)
				{
					num = primaryElement.Temperature;
				}
				component.SetMassTemperature(mass + mass2, num);
				if (CameraController.Instance != null)
				{
					string sound = GlobalAssets.GetSound("Ore_absorb", false);
					Vector3 position = pickupable.transform.GetPosition();
					position.z = 0f;
					if (sound != null && CameraController.Instance.IsAudibleSound(position, sound))
					{
						KFMOD.PlayOneShot(sound, position, 1f);
					}
				}
			}
		}
	}

	// Token: 0x04000E5A RID: 3674
	public float maxStackSize = PrimaryElement.MAX_MASS;

	// Token: 0x04000E5B RID: 3675
	private static readonly EventSystem.IntraObjectHandler<EntitySplitter> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<EntitySplitter>(delegate(EntitySplitter component, object data)
	{
		component.OnAbsorb(data);
	});
}
