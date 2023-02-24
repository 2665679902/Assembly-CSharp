using System;
using Klei;
using UnityEngine;

// Token: 0x02000739 RID: 1849
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/ElementChunk")]
public class ElementChunk : KMonoBehaviour
{
	// Token: 0x0600329E RID: 12958 RVA: 0x0011124F File Offset: 0x0010F44F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GameComps.OreSizeVisualizers.Add(base.gameObject);
		GameComps.ElementSplitters.Add(base.gameObject);
		base.Subscribe<ElementChunk>(-2064133523, ElementChunk.OnAbsorbDelegate);
	}

	// Token: 0x0600329F RID: 12959 RVA: 0x0011128C File Offset: 0x0010F48C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
		base.transform.SetPosition(position);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Element element = component.Element;
		KSelectable component2 = base.GetComponent<KSelectable>();
		Func<Element> func = () => element;
		component2.AddStatusItem(Db.Get().MiscStatusItems.ElementalCategory, func);
		component2.AddStatusItem(Db.Get().MiscStatusItems.OreMass, base.gameObject);
		component2.AddStatusItem(Db.Get().MiscStatusItems.OreTemp, base.gameObject);
	}

	// Token: 0x060032A0 RID: 12960 RVA: 0x00111340 File Offset: 0x0010F540
	protected override void OnCleanUp()
	{
		GameComps.ElementSplitters.Remove(base.gameObject);
		GameComps.OreSizeVisualizers.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x00111368 File Offset: 0x0010F568
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable != null)
		{
			PrimaryElement primaryElement = pickupable.PrimaryElement;
			if (primaryElement != null)
			{
				float mass = primaryElement.Mass;
				if (mass > 0f)
				{
					PrimaryElement component = base.GetComponent<PrimaryElement>();
					float mass2 = component.Mass;
					float num = ((mass2 > 0f) ? SimUtil.CalculateFinalTemperature(mass2, component.Temperature, mass, primaryElement.Temperature) : primaryElement.Temperature);
					component.SetMassTemperature(mass2 + mass, num);
				}
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

	// Token: 0x04001F1F RID: 7967
	private static readonly EventSystem.IntraObjectHandler<ElementChunk> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<ElementChunk>(delegate(ElementChunk component, object data)
	{
		component.OnAbsorb(data);
	});
}
