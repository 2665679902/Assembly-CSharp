using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007CC RID: 1996
public class FilteredDragTool : DragTool
{
	// Token: 0x0600391F RID: 14623 RVA: 0x0013C76D File Offset: 0x0013A96D
	public bool IsActiveLayer(string layer)
	{
		return this.currentFilterTargets[ToolParameterMenu.FILTERLAYERS.ALL] == ToolParameterMenu.ToggleState.On || (this.currentFilterTargets.ContainsKey(layer.ToUpper()) && this.currentFilterTargets[layer.ToUpper()] == ToolParameterMenu.ToggleState.On);
	}

	// Token: 0x06003920 RID: 14624 RVA: 0x0013C7AC File Offset: 0x0013A9AC
	public bool IsActiveLayer(ObjectLayer layer)
	{
		if (this.currentFilterTargets.ContainsKey(ToolParameterMenu.FILTERLAYERS.ALL) && this.currentFilterTargets[ToolParameterMenu.FILTERLAYERS.ALL] == ToolParameterMenu.ToggleState.On)
		{
			return true;
		}
		bool flag = false;
		foreach (KeyValuePair<string, ToolParameterMenu.ToggleState> keyValuePair in this.currentFilterTargets)
		{
			if (keyValuePair.Value == ToolParameterMenu.ToggleState.On && this.GetObjectLayerFromFilterLayer(keyValuePair.Key) == layer)
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003921 RID: 14625 RVA: 0x0013C840 File Offset: 0x0013AA40
	protected virtual void GetDefaultFilters(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		filters.Add(ToolParameterMenu.FILTERLAYERS.ALL, ToolParameterMenu.ToggleState.On);
		filters.Add(ToolParameterMenu.FILTERLAYERS.WIRES, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.LIQUIDCONDUIT, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.GASCONDUIT, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.SOLIDCONDUIT, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.BUILDINGS, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.LOGIC, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.BACKWALL, ToolParameterMenu.ToggleState.Off);
	}

	// Token: 0x06003922 RID: 14626 RVA: 0x0013C8AD File Offset: 0x0013AAAD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ResetFilter(this.filterTargets);
	}

	// Token: 0x06003923 RID: 14627 RVA: 0x0013C8C1 File Offset: 0x0013AAC1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		OverlayScreen instance = OverlayScreen.Instance;
		instance.OnOverlayChanged = (Action<HashedString>)Delegate.Combine(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
	}

	// Token: 0x06003924 RID: 14628 RVA: 0x0013C8EF File Offset: 0x0013AAEF
	protected override void OnCleanUp()
	{
		OverlayScreen instance = OverlayScreen.Instance;
		instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
		base.OnCleanUp();
	}

	// Token: 0x06003925 RID: 14629 RVA: 0x0013C91D File Offset: 0x0013AB1D
	public void ResetFilter()
	{
		this.ResetFilter(this.filterTargets);
	}

	// Token: 0x06003926 RID: 14630 RVA: 0x0013C92B File Offset: 0x0013AB2B
	protected void ResetFilter(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		filters.Clear();
		this.GetDefaultFilters(filters);
		this.currentFilterTargets = filters;
	}

	// Token: 0x06003927 RID: 14631 RVA: 0x0013C941 File Offset: 0x0013AB41
	protected override void OnActivateTool()
	{
		this.active = true;
		base.OnActivateTool();
		this.OnOverlayChanged(OverlayScreen.Instance.mode);
	}

	// Token: 0x06003928 RID: 14632 RVA: 0x0013C960 File Offset: 0x0013AB60
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		this.active = false;
		ToolMenu.Instance.toolParameterMenu.ClearMenu();
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x06003929 RID: 14633 RVA: 0x0013C980 File Offset: 0x0013AB80
	public virtual string GetFilterLayerFromGameObject(GameObject input)
	{
		BuildingComplete component = input.GetComponent<BuildingComplete>();
		BuildingUnderConstruction component2 = input.GetComponent<BuildingUnderConstruction>();
		if (component)
		{
			return this.GetFilterLayerFromObjectLayer(component.Def.ObjectLayer);
		}
		if (component2)
		{
			return this.GetFilterLayerFromObjectLayer(component2.Def.ObjectLayer);
		}
		if (input.GetComponent<Clearable>() != null || input.GetComponent<Moppable>() != null)
		{
			return "CleanAndClear";
		}
		if (input.GetComponent<Diggable>() != null)
		{
			return "DigPlacer";
		}
		return "Default";
	}

	// Token: 0x0600392A RID: 14634 RVA: 0x0013CA0C File Offset: 0x0013AC0C
	public string GetFilterLayerFromObjectLayer(ObjectLayer gamer_layer)
	{
		if (gamer_layer > ObjectLayer.FoundationTile)
		{
			switch (gamer_layer)
			{
			case ObjectLayer.GasConduit:
			case ObjectLayer.GasConduitConnection:
				return "GasPipes";
			case ObjectLayer.GasConduitTile:
			case ObjectLayer.ReplacementGasConduit:
			case ObjectLayer.LiquidConduitTile:
			case ObjectLayer.ReplacementLiquidConduit:
				goto IL_AC;
			case ObjectLayer.LiquidConduit:
			case ObjectLayer.LiquidConduitConnection:
				return "LiquidPipes";
			case ObjectLayer.SolidConduit:
				break;
			default:
				switch (gamer_layer)
				{
				case ObjectLayer.SolidConduitConnection:
					break;
				case ObjectLayer.LadderTile:
				case ObjectLayer.ReplacementLadder:
				case ObjectLayer.WireTile:
				case ObjectLayer.ReplacementWire:
					goto IL_AC;
				case ObjectLayer.Wire:
				case ObjectLayer.WireConnectors:
					return "Wires";
				case ObjectLayer.LogicGate:
				case ObjectLayer.LogicWire:
					return "Logic";
				default:
					if (gamer_layer == ObjectLayer.Gantry)
					{
						goto IL_7C;
					}
					goto IL_AC;
				}
				break;
			}
			return "SolidConduits";
		}
		if (gamer_layer != ObjectLayer.Building)
		{
			if (gamer_layer == ObjectLayer.Backwall)
			{
				return "BackWall";
			}
			if (gamer_layer != ObjectLayer.FoundationTile)
			{
				goto IL_AC;
			}
			return "Tiles";
		}
		IL_7C:
		return "Buildings";
		IL_AC:
		return "Default";
	}

	// Token: 0x0600392B RID: 14635 RVA: 0x0013CACC File Offset: 0x0013ACCC
	private ObjectLayer GetObjectLayerFromFilterLayer(string filter_layer)
	{
		string text = filter_layer.ToLower();
		if (text != null)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			ObjectLayer objectLayer;
			if (num <= 2200975418U)
			{
				if (num <= 388608975U)
				{
					if (num != 25076977U)
					{
						if (num != 388608975U)
						{
							goto IL_12D;
						}
						if (!(text == "solidconduits"))
						{
							goto IL_12D;
						}
						objectLayer = ObjectLayer.SolidConduit;
					}
					else
					{
						if (!(text == "wires"))
						{
							goto IL_12D;
						}
						objectLayer = ObjectLayer.Wire;
					}
				}
				else if (num != 614364310U)
				{
					if (num != 2200975418U)
					{
						goto IL_12D;
					}
					if (!(text == "backwall"))
					{
						goto IL_12D;
					}
					objectLayer = ObjectLayer.Backwall;
				}
				else
				{
					if (!(text == "liquidpipes"))
					{
						goto IL_12D;
					}
					objectLayer = ObjectLayer.LiquidConduit;
				}
			}
			else if (num <= 2875565775U)
			{
				if (num != 2366751346U)
				{
					if (num != 2875565775U)
					{
						goto IL_12D;
					}
					if (!(text == "gaspipes"))
					{
						goto IL_12D;
					}
					objectLayer = ObjectLayer.GasConduit;
				}
				else
				{
					if (!(text == "buildings"))
					{
						goto IL_12D;
					}
					objectLayer = ObjectLayer.Building;
				}
			}
			else if (num != 3464443665U)
			{
				if (num != 4178729166U)
				{
					goto IL_12D;
				}
				if (!(text == "tiles"))
				{
					goto IL_12D;
				}
				objectLayer = ObjectLayer.FoundationTile;
			}
			else
			{
				if (!(text == "logic"))
				{
					goto IL_12D;
				}
				objectLayer = ObjectLayer.LogicWire;
			}
			return objectLayer;
		}
		IL_12D:
		throw new ArgumentException("Invalid filter layer: " + filter_layer);
	}

	// Token: 0x0600392C RID: 14636 RVA: 0x0013CC18 File Offset: 0x0013AE18
	private void OnOverlayChanged(HashedString overlay)
	{
		if (!this.active)
		{
			return;
		}
		string text = null;
		if (overlay == OverlayModes.Power.ID)
		{
			text = ToolParameterMenu.FILTERLAYERS.WIRES;
		}
		else if (overlay == OverlayModes.LiquidConduits.ID)
		{
			text = ToolParameterMenu.FILTERLAYERS.LIQUIDCONDUIT;
		}
		else if (overlay == OverlayModes.GasConduits.ID)
		{
			text = ToolParameterMenu.FILTERLAYERS.GASCONDUIT;
		}
		else if (overlay == OverlayModes.SolidConveyor.ID)
		{
			text = ToolParameterMenu.FILTERLAYERS.SOLIDCONDUIT;
		}
		else if (overlay == OverlayModes.Logic.ID)
		{
			text = ToolParameterMenu.FILTERLAYERS.LOGIC;
		}
		this.currentFilterTargets = this.filterTargets;
		if (text != null)
		{
			using (List<string>.Enumerator enumerator = new List<string>(this.filterTargets.Keys).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text2 = enumerator.Current;
					this.filterTargets[text2] = ToolParameterMenu.ToggleState.Disabled;
					if (text2 == text)
					{
						this.filterTargets[text2] = ToolParameterMenu.ToggleState.On;
					}
				}
				goto IL_102;
			}
		}
		if (this.overlayFilterTargets.Count == 0)
		{
			this.ResetFilter(this.overlayFilterTargets);
		}
		this.currentFilterTargets = this.overlayFilterTargets;
		IL_102:
		ToolMenu.Instance.toolParameterMenu.PopulateMenu(this.currentFilterTargets);
	}

	// Token: 0x040025CC RID: 9676
	private Dictionary<string, ToolParameterMenu.ToggleState> filterTargets = new Dictionary<string, ToolParameterMenu.ToggleState>();

	// Token: 0x040025CD RID: 9677
	private Dictionary<string, ToolParameterMenu.ToggleState> overlayFilterTargets = new Dictionary<string, ToolParameterMenu.ToggleState>();

	// Token: 0x040025CE RID: 9678
	private Dictionary<string, ToolParameterMenu.ToggleState> currentFilterTargets;

	// Token: 0x040025CF RID: 9679
	private bool active;
}
