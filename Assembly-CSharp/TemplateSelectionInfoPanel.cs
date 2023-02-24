using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000C0B RID: 3083
[AddComponentMenu("KMonoBehaviour/scripts/TemplateSelectionInfoPanel")]
public class TemplateSelectionInfoPanel : KMonoBehaviour, IRender1000ms
{
	// Token: 0x060061B4 RID: 25012 RVA: 0x002412F8 File Offset: 0x0023F4F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.details.Length; i++)
		{
			Util.KInstantiateUI(this.prefab_detail_label, this.current_detail_container, true);
		}
		this.RefreshDetails();
		this.save_button.onClick += this.SaveCurrentDetails;
	}

	// Token: 0x060061B5 RID: 25013 RVA: 0x00241350 File Offset: 0x0023F550
	public void SaveCurrentDetails()
	{
		string text = "";
		for (int i = 0; i < this.details.Length; i++)
		{
			text = text + this.details[i](DebugBaseTemplateButton.Instance.SelectedCells) + "\n";
		}
		text += "\n\n";
		text += this.saved_detail_label.text;
		this.saved_detail_label.text = text;
	}

	// Token: 0x060061B6 RID: 25014 RVA: 0x002413C3 File Offset: 0x0023F5C3
	public void Render1000ms(float dt)
	{
		this.RefreshDetails();
	}

	// Token: 0x060061B7 RID: 25015 RVA: 0x002413CC File Offset: 0x0023F5CC
	public void RefreshDetails()
	{
		for (int i = 0; i < this.details.Length; i++)
		{
			this.current_detail_container.transform.GetChild(i).GetComponent<LocText>().text = this.details[i](DebugBaseTemplateButton.Instance.SelectedCells);
		}
	}

	// Token: 0x060061B8 RID: 25016 RVA: 0x00241420 File Offset: 0x0023F620
	private static string TotalMass(List<int> cells)
	{
		float num = 0f;
		foreach (int num2 in cells)
		{
			num += Grid.Mass[num2];
		}
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.TOTAL_MASS, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x060061B9 RID: 25017 RVA: 0x00241498 File Offset: 0x0023F698
	private static string AverageMass(List<int> cells)
	{
		float num = 0f;
		foreach (int num2 in cells)
		{
			num += Grid.Mass[num2];
		}
		num /= (float)cells.Count;
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.AVERAGE_MASS, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x060061BA RID: 25018 RVA: 0x0024151C File Offset: 0x0023F71C
	private static string AverageTemperature(List<int> cells)
	{
		float num = 0f;
		foreach (int num2 in cells)
		{
			num += Grid.Temperature[num2];
		}
		num /= (float)cells.Count;
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.AVERAGE_TEMPERATURE, GameUtil.GetFormattedTemperature(num, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
	}

	// Token: 0x060061BB RID: 25019 RVA: 0x0024159C File Offset: 0x0023F79C
	private static string TotalJoules(List<int> cells)
	{
		List<GameObject> list = new List<GameObject>();
		float num = 0f;
		foreach (int num2 in cells)
		{
			num += Grid.Element[num2].specificHeatCapacity * Grid.Temperature[num2] * (Grid.Mass[num2] * 1000f);
			num += TemplateSelectionInfoPanel.GetCellEntityEnergy(num2, ref list);
		}
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.TOTAL_JOULES, GameUtil.GetFormattedJoules(num, "F5", GameUtil.TimeSlice.None));
	}

	// Token: 0x060061BC RID: 25020 RVA: 0x00241644 File Offset: 0x0023F844
	private static float GetCellEntityEnergy(int cell, ref List<GameObject> ignoreObjects)
	{
		float num = 0f;
		for (int i = 0; i < 44; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (!(gameObject == null) && !ignoreObjects.Contains(gameObject))
			{
				ignoreObjects.Add(gameObject);
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && component.Element != null)
				{
					float num2 = component.Mass;
					Building component2 = gameObject.GetComponent<Building>();
					if (component2 != null)
					{
						num2 = component2.Def.MassForTemperatureModification;
					}
					float num3 = num2 * 1000f * component.Element.specificHeatCapacity * component.Temperature;
					num += num3;
					Storage[] components = gameObject.GetComponents<Storage>();
					if (components != null)
					{
						float num4 = 0f;
						Storage[] array = components;
						for (int j = 0; j < array.Length; j++)
						{
							foreach (GameObject gameObject2 in array[j].items)
							{
								PrimaryElement component3 = gameObject2.GetComponent<PrimaryElement>();
								if (!(component3 == null))
								{
									num4 += component3.Mass * 1000f * component3.Element.specificHeatCapacity * component3.Temperature;
								}
							}
						}
						num += num4;
					}
					Conduit component4 = gameObject.GetComponent<Conduit>();
					if (component4 != null)
					{
						ConduitFlow.ConduitContents contents = component4.GetFlowManager().GetContents(cell);
						if (contents.mass > 0f)
						{
							Element element = ElementLoader.FindElementByHash(contents.element);
							float num5 = contents.mass * 1000f * element.specificHeatCapacity * contents.temperature;
							num += num5;
						}
					}
					if (gameObject.GetComponent<SolidConduit>() != null)
					{
						SolidConduitFlow solidConduitFlow = Game.Instance.solidConduitFlow;
						SolidConduitFlow.ConduitContents contents2 = solidConduitFlow.GetContents(cell);
						if (contents2.pickupableHandle.IsValid())
						{
							Pickupable pickupable = solidConduitFlow.GetPickupable(contents2.pickupableHandle);
							if (pickupable)
							{
								PrimaryElement component5 = pickupable.GetComponent<PrimaryElement>();
								if (component5.Mass > 0f)
								{
									float num6 = component5.Mass * 1000f * component5.Element.specificHeatCapacity * component5.Temperature;
									num += num6;
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x060061BD RID: 25021 RVA: 0x0024189C File Offset: 0x0023FA9C
	private static string JoulesPerKilogram(List<int> cells)
	{
		float num = 0f;
		float num2 = 0f;
		foreach (int num3 in cells)
		{
			num += Grid.Element[num3].specificHeatCapacity * Grid.Temperature[num3] * (Grid.Mass[num3] * 1000f);
			num2 += Grid.Mass[num3];
		}
		num /= num2;
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.JOULES_PER_KILOGRAM, GameUtil.GetFormattedJoules(num, "F1", GameUtil.TimeSlice.None));
	}

	// Token: 0x060061BE RID: 25022 RVA: 0x0024194C File Offset: 0x0023FB4C
	private static string TotalRadiation(List<int> cells)
	{
		float num = 0f;
		foreach (int num2 in cells)
		{
			num += Grid.Radiation[num2];
		}
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.TOTAL_RADS, GameUtil.GetFormattedRads(num, GameUtil.TimeSlice.None));
	}

	// Token: 0x060061BF RID: 25023 RVA: 0x002419C0 File Offset: 0x0023FBC0
	private static string AverageRadiation(List<int> cells)
	{
		float num = 0f;
		foreach (int num2 in cells)
		{
			num += Grid.Radiation[num2];
		}
		num /= (float)cells.Count;
		return string.Format(UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.SELECTION_INFO_PANEL.AVERAGE_RADS, GameUtil.GetFormattedRads(num, GameUtil.TimeSlice.None));
	}

	// Token: 0x060061C0 RID: 25024 RVA: 0x00241A3C File Offset: 0x0023FC3C
	private static string MassPerElement(List<int> cells)
	{
		TemplateSelectionInfoPanel.mass_per_element.Clear();
		foreach (int num in cells)
		{
			bool flag = false;
			for (int i = 0; i < TemplateSelectionInfoPanel.mass_per_element.Count; i++)
			{
				if (TemplateSelectionInfoPanel.mass_per_element[i].first == Grid.Element[num])
				{
					TemplateSelectionInfoPanel.mass_per_element[i].second += Grid.Mass[num];
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				TemplateSelectionInfoPanel.mass_per_element.Add(new global::Tuple<Element, float>(Grid.Element[num], Grid.Mass[num]));
			}
		}
		TemplateSelectionInfoPanel.mass_per_element.Sort(delegate(global::Tuple<Element, float> a, global::Tuple<Element, float> b)
		{
			if (a.second > b.second)
			{
				return -1;
			}
			if (b.second > a.second)
			{
				return 1;
			}
			return 0;
		});
		string text = "";
		foreach (global::Tuple<Element, float> tuple in TemplateSelectionInfoPanel.mass_per_element)
		{
			text = string.Concat(new string[]
			{
				text,
				tuple.first.name,
				": ",
				GameUtil.GetFormattedMass(tuple.second, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"),
				"\n"
			});
		}
		return text;
	}

	// Token: 0x0400438A RID: 17290
	[SerializeField]
	private GameObject prefab_detail_label;

	// Token: 0x0400438B RID: 17291
	[SerializeField]
	private GameObject current_detail_container;

	// Token: 0x0400438C RID: 17292
	[SerializeField]
	private LocText saved_detail_label;

	// Token: 0x0400438D RID: 17293
	[SerializeField]
	private KButton save_button;

	// Token: 0x0400438E RID: 17294
	private Func<List<int>, string>[] details = new Func<List<int>, string>[]
	{
		new Func<List<int>, string>(TemplateSelectionInfoPanel.TotalMass),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.AverageMass),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.AverageTemperature),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.TotalJoules),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.JoulesPerKilogram),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.MassPerElement),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.TotalRadiation),
		new Func<List<int>, string>(TemplateSelectionInfoPanel.AverageRadiation)
	};

	// Token: 0x0400438F RID: 17295
	private static List<global::Tuple<Element, float>> mass_per_element = new List<global::Tuple<Element, float>>();
}
