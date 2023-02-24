﻿using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AB0 RID: 2736
[AddComponentMenu("KMonoBehaviour/scripts/HarvestableOverlayWidget")]
public class HarvestableOverlayWidget : KMonoBehaviour
{
	// Token: 0x060053D4 RID: 21460 RVA: 0x001E7964 File Offset: 0x001E5B64
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.condition_sprites.Add(WiltCondition.Condition.AtmosphereElement, this.sprite_atmosphere);
		this.condition_sprites.Add(WiltCondition.Condition.Darkness, this.sprite_light);
		this.condition_sprites.Add(WiltCondition.Condition.Drowning, this.sprite_liquid);
		this.condition_sprites.Add(WiltCondition.Condition.DryingOut, this.sprite_liquid);
		this.condition_sprites.Add(WiltCondition.Condition.Fertilized, this.sprite_resource);
		this.condition_sprites.Add(WiltCondition.Condition.IlluminationComfort, this.sprite_light);
		this.condition_sprites.Add(WiltCondition.Condition.Irrigation, this.sprite_liquid);
		this.condition_sprites.Add(WiltCondition.Condition.Pressure, this.sprite_pressure);
		this.condition_sprites.Add(WiltCondition.Condition.Temperature, this.sprite_temperature);
		this.condition_sprites.Add(WiltCondition.Condition.Receptacle, this.sprite_receptacle);
		for (int i = 0; i < this.horizontal_containers.Length; i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.horizontal_container_prefab, this.vertical_container, false);
			this.horizontal_containers[i] = gameObject;
		}
		for (int j = 0; j < 13; j++)
		{
			if (this.condition_sprites.ContainsKey((WiltCondition.Condition)j))
			{
				GameObject gameObject2 = Util.KInstantiateUI(this.icon_gameobject_prefab, base.gameObject, false);
				gameObject2.GetComponent<Image>().sprite = this.condition_sprites[(WiltCondition.Condition)j];
				this.condition_icons.Add((WiltCondition.Condition)j, gameObject2);
			}
		}
	}

	// Token: 0x060053D5 RID: 21461 RVA: 0x001E7AB0 File Offset: 0x001E5CB0
	public void Refresh(HarvestDesignatable target_harvestable)
	{
		Image image = this.bar.GetComponent<HierarchyReferences>().GetReference("Fill") as Image;
		AmountInstance amountInstance = Db.Get().Amounts.Maturity.Lookup(target_harvestable);
		if (amountInstance != null)
		{
			float num = amountInstance.value / amountInstance.GetMax();
			image.rectTransform.offsetMin = new Vector2(image.rectTransform.offsetMin.x, 3f);
			if (this.bar.activeSelf != !target_harvestable.CanBeHarvested())
			{
				this.bar.SetActive(!target_harvestable.CanBeHarvested());
			}
			float num2 = (target_harvestable.CanBeHarvested() ? 3f : (19f - 19f * num + 3f));
			image.rectTransform.offsetMax = new Vector2(image.rectTransform.offsetMax.x, -num2);
		}
		else if (this.bar.activeSelf)
		{
			this.bar.SetActive(false);
		}
		WiltCondition component = target_harvestable.GetComponent<WiltCondition>();
		if (component != null)
		{
			for (int i = 0; i < this.horizontal_containers.Length; i++)
			{
				this.horizontal_containers[i].SetActive(false);
			}
			foreach (KeyValuePair<WiltCondition.Condition, GameObject> keyValuePair in this.condition_icons)
			{
				keyValuePair.Value.SetActive(false);
			}
			if (!component.IsWilting())
			{
				this.vertical_container.SetActive(false);
				image.color = HarvestableOverlayWidget.growing_color;
				return;
			}
			this.vertical_container.SetActive(true);
			image.color = HarvestableOverlayWidget.wilting_color;
			List<WiltCondition.Condition> list = component.CurrentWiltSources();
			if (list.Count > 0)
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (this.condition_icons.ContainsKey(list[j]))
					{
						this.condition_icons[list[j]].SetActive(true);
						this.horizontal_containers[j / 2].SetActive(true);
						this.condition_icons[list[j]].transform.SetParent(this.horizontal_containers[j / 2].transform);
					}
				}
				return;
			}
		}
		else
		{
			image.color = HarvestableOverlayWidget.growing_color;
			this.vertical_container.SetActive(false);
		}
	}

	// Token: 0x040038EF RID: 14575
	[SerializeField]
	private GameObject vertical_container;

	// Token: 0x040038F0 RID: 14576
	[SerializeField]
	private GameObject bar;

	// Token: 0x040038F1 RID: 14577
	private const int icons_per_row = 2;

	// Token: 0x040038F2 RID: 14578
	private const float bar_fill_range = 19f;

	// Token: 0x040038F3 RID: 14579
	private const float bar_fill_offset = 3f;

	// Token: 0x040038F4 RID: 14580
	private static Color growing_color = new Color(0.9843137f, 0.6901961f, 0.23137255f, 1f);

	// Token: 0x040038F5 RID: 14581
	private static Color wilting_color = new Color(0.5647059f, 0.5647059f, 0.5647059f, 1f);

	// Token: 0x040038F6 RID: 14582
	[SerializeField]
	private Sprite sprite_liquid;

	// Token: 0x040038F7 RID: 14583
	[SerializeField]
	private Sprite sprite_atmosphere;

	// Token: 0x040038F8 RID: 14584
	[SerializeField]
	private Sprite sprite_pressure;

	// Token: 0x040038F9 RID: 14585
	[SerializeField]
	private Sprite sprite_temperature;

	// Token: 0x040038FA RID: 14586
	[SerializeField]
	private Sprite sprite_resource;

	// Token: 0x040038FB RID: 14587
	[SerializeField]
	private Sprite sprite_light;

	// Token: 0x040038FC RID: 14588
	[SerializeField]
	private Sprite sprite_receptacle;

	// Token: 0x040038FD RID: 14589
	[SerializeField]
	private GameObject horizontal_container_prefab;

	// Token: 0x040038FE RID: 14590
	private GameObject[] horizontal_containers = new GameObject[6];

	// Token: 0x040038FF RID: 14591
	[SerializeField]
	private GameObject icon_gameobject_prefab;

	// Token: 0x04003900 RID: 14592
	private Dictionary<WiltCondition.Condition, GameObject> condition_icons = new Dictionary<WiltCondition.Condition, GameObject>();

	// Token: 0x04003901 RID: 14593
	private Dictionary<WiltCondition.Condition, Sprite> condition_sprites = new Dictionary<WiltCondition.Condition, Sprite>();
}
