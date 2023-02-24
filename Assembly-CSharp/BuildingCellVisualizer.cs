using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000454 RID: 1108
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/BuildingCellVisualizer")]
public class BuildingCellVisualizer : KMonoBehaviour
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060017FE RID: 6142 RVA: 0x0007D893 File Offset: 0x0007BA93
	public bool RequiresPowerInput
	{
		get
		{
			return (this.ports & BuildingCellVisualizer.Ports.PowerIn) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060017FF RID: 6143 RVA: 0x0007D8A0 File Offset: 0x0007BAA0
	public bool RequiresPowerOutput
	{
		get
		{
			return (this.ports & BuildingCellVisualizer.Ports.PowerOut) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06001800 RID: 6144 RVA: 0x0007D8AD File Offset: 0x0007BAAD
	public bool RequiresPower
	{
		get
		{
			return (this.ports & (BuildingCellVisualizer.Ports.PowerIn | BuildingCellVisualizer.Ports.PowerOut)) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06001801 RID: 6145 RVA: 0x0007D8BA File Offset: 0x0007BABA
	public bool RequiresGas
	{
		get
		{
			return (this.ports & (BuildingCellVisualizer.Ports.GasIn | BuildingCellVisualizer.Ports.GasOut)) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06001802 RID: 6146 RVA: 0x0007D8C8 File Offset: 0x0007BAC8
	public bool RequiresLiquid
	{
		get
		{
			return (this.ports & (BuildingCellVisualizer.Ports.LiquidIn | BuildingCellVisualizer.Ports.LiquidOut)) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06001803 RID: 6147 RVA: 0x0007D8D6 File Offset: 0x0007BAD6
	public bool RequiresSolid
	{
		get
		{
			return (this.ports & (BuildingCellVisualizer.Ports.SolidIn | BuildingCellVisualizer.Ports.SolidOut)) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06001804 RID: 6148 RVA: 0x0007D8E7 File Offset: 0x0007BAE7
	public bool RequiresUtilityConnection
	{
		get
		{
			return (this.ports & (BuildingCellVisualizer.Ports.GasIn | BuildingCellVisualizer.Ports.GasOut | BuildingCellVisualizer.Ports.LiquidIn | BuildingCellVisualizer.Ports.LiquidOut | BuildingCellVisualizer.Ports.SolidIn | BuildingCellVisualizer.Ports.SolidOut)) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06001805 RID: 6149 RVA: 0x0007D8F8 File Offset: 0x0007BAF8
	public bool RequiresHighEnergyParticles
	{
		get
		{
			return (this.ports & BuildingCellVisualizer.Ports.HighEnergyParticle) > (BuildingCellVisualizer.Ports)0;
		}
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x0007D909 File Offset: 0x0007BB09
	public void ConnectedEventWithDelay(float delay, int connectionCount, int cell, string soundName)
	{
		base.StartCoroutine(this.ConnectedDelay(delay, connectionCount, cell, soundName));
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x0007D91D File Offset: 0x0007BB1D
	private IEnumerator ConnectedDelay(float delay, int connectionCount, int cell, string soundName)
	{
		float startTime = Time.realtimeSinceStartup;
		float currentTime = startTime;
		while (currentTime < startTime + delay)
		{
			currentTime += Time.unscaledDeltaTime;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.ConnectedEvent(cell);
		string sound = GlobalAssets.GetSound(soundName, false);
		if (sound != null)
		{
			Vector3 position = base.transform.GetPosition();
			position.z = 0f;
			EventInstance eventInstance = SoundEvent.BeginOneShot(sound, position, 1f, false);
			eventInstance.setParameterByName("connectedCount", (float)connectionCount, false);
			SoundEvent.EndOneShot(eventInstance);
		}
		yield break;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x0007D94C File Offset: 0x0007BB4C
	public void ConnectedEvent(int cell)
	{
		GameObject gameObject = null;
		if (this.inputVisualizer != null && Grid.PosToCell(this.inputVisualizer) == cell)
		{
			gameObject = this.inputVisualizer;
		}
		else if (this.outputVisualizer != null && Grid.PosToCell(this.outputVisualizer) == cell)
		{
			gameObject = this.outputVisualizer;
		}
		else if (this.secondaryInputVisualizer != null && Grid.PosToCell(this.secondaryInputVisualizer) == cell)
		{
			gameObject = this.secondaryInputVisualizer;
		}
		else if (this.secondaryOutputVisualizer != null && Grid.PosToCell(this.secondaryOutputVisualizer) == cell)
		{
			gameObject = this.secondaryOutputVisualizer;
		}
		if (gameObject == null)
		{
			return;
		}
		SizePulse pulse = gameObject.gameObject.AddComponent<SizePulse>();
		pulse.speed = 20f;
		pulse.multiplier = 0.75f;
		pulse.updateWhenPaused = true;
		SizePulse pulse2 = pulse;
		pulse2.onComplete = (System.Action)Delegate.Combine(pulse2.onComplete, new System.Action(delegate
		{
			UnityEngine.Object.Destroy(pulse);
		}));
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x0007DA64 File Offset: 0x0007BC64
	private void MapBuilding()
	{
		BuildingDef def = this.building.Def;
		if (def.CheckRequiresPowerInput())
		{
			this.ports |= BuildingCellVisualizer.Ports.PowerIn;
		}
		if (def.CheckRequiresPowerOutput())
		{
			this.ports |= BuildingCellVisualizer.Ports.PowerOut;
		}
		if (def.CheckRequiresGasInput())
		{
			this.ports |= BuildingCellVisualizer.Ports.GasIn;
		}
		if (def.CheckRequiresGasOutput())
		{
			this.ports |= BuildingCellVisualizer.Ports.GasOut;
		}
		if (def.CheckRequiresLiquidInput())
		{
			this.ports |= BuildingCellVisualizer.Ports.LiquidIn;
		}
		if (def.CheckRequiresLiquidOutput())
		{
			this.ports |= BuildingCellVisualizer.Ports.LiquidOut;
		}
		if (def.CheckRequiresSolidInput())
		{
			this.ports |= BuildingCellVisualizer.Ports.SolidIn;
		}
		if (def.CheckRequiresSolidOutput())
		{
			this.ports |= BuildingCellVisualizer.Ports.SolidOut;
		}
		if (def.CheckRequiresHighEnergyParticleInput())
		{
			this.ports |= BuildingCellVisualizer.Ports.HighEnergyParticle;
		}
		if (def.CheckRequiresHighEnergyParticleOutput())
		{
			this.ports |= BuildingCellVisualizer.Ports.HighEnergyParticle;
		}
		DiseaseVisualization.Info info = Assets.instance.DiseaseVisualization.GetInfo(def.DiseaseCellVisName);
		if (info.name != null)
		{
			this.diseaseSourceSprite = Assets.instance.DiseaseVisualization.overlaySprite;
			this.diseaseSourceColour = GlobalAssets.Instance.colorSet.GetColorByName(info.overlayColourName);
		}
		foreach (ISecondaryInput secondaryInput in def.BuildingComplete.GetComponents<ISecondaryInput>())
		{
			if (secondaryInput != null)
			{
				if (secondaryInput.HasSecondaryConduitType(ConduitType.Gas))
				{
					this.secondary_ports |= BuildingCellVisualizer.Ports.GasIn;
				}
				if (secondaryInput.HasSecondaryConduitType(ConduitType.Liquid))
				{
					this.secondary_ports |= BuildingCellVisualizer.Ports.LiquidIn;
				}
				if (secondaryInput.HasSecondaryConduitType(ConduitType.Solid))
				{
					this.secondary_ports |= BuildingCellVisualizer.Ports.SolidIn;
				}
			}
		}
		foreach (ISecondaryOutput secondaryOutput in def.BuildingComplete.GetComponents<ISecondaryOutput>())
		{
			if (secondaryOutput != null)
			{
				if (secondaryOutput.HasSecondaryConduitType(ConduitType.Gas))
				{
					this.secondary_ports |= BuildingCellVisualizer.Ports.GasOut;
				}
				if (secondaryOutput.HasSecondaryConduitType(ConduitType.Liquid))
				{
					this.secondary_ports |= BuildingCellVisualizer.Ports.LiquidOut;
				}
				if (secondaryOutput.HasSecondaryConduitType(ConduitType.Solid))
				{
					this.secondary_ports |= BuildingCellVisualizer.Ports.SolidOut;
				}
			}
		}
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x0007DC9C File Offset: 0x0007BE9C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.inputVisualizer != null)
		{
			UnityEngine.Object.Destroy(this.inputVisualizer);
		}
		if (this.outputVisualizer != null)
		{
			UnityEngine.Object.Destroy(this.outputVisualizer);
		}
		if (this.secondaryInputVisualizer != null)
		{
			UnityEngine.Object.Destroy(this.secondaryInputVisualizer);
		}
		if (this.secondaryOutputVisualizer != null)
		{
			UnityEngine.Object.Destroy(this.secondaryOutputVisualizer);
		}
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x0007DD14 File Offset: 0x0007BF14
	private Color GetWireColor(int cell)
	{
		GameObject gameObject = Grid.Objects[cell, 26];
		if (gameObject == null)
		{
			return Color.white;
		}
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		if (!(component != null))
		{
			return Color.white;
		}
		return component.TintColour;
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x0007DD60 File Offset: 0x0007BF60
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (this.resources == null)
		{
			this.resources = BuildingCellVisualizerResources.Instance();
		}
		if (this.icons == null)
		{
			this.icons = new Dictionary<GameObject, Image>();
		}
		this.enableRaycast = this.building as BuildingComplete != null;
		this.MapBuilding();
		Components.BuildingCellVisualizers.Add(this);
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x0007DDC7 File Offset: 0x0007BFC7
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		Components.BuildingCellVisualizers.Remove(this);
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x0007DDDC File Offset: 0x0007BFDC
	public void DrawIcons(HashedString mode)
	{
		if (base.gameObject.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
		{
			this.DisableIcons();
			return;
		}
		if (mode == OverlayModes.Power.ID)
		{
			if (this.RequiresPower)
			{
				bool flag = this.building as BuildingPreview != null;
				BuildingEnabledButton component = this.building.GetComponent<BuildingEnabledButton>();
				int powerInputCell = this.building.GetPowerInputCell();
				if (this.RequiresPowerInput)
				{
					int circuitID = (int)Game.Instance.circuitManager.GetCircuitID(powerInputCell);
					Color color = ((component != null && !component.IsEnabled) ? Color.gray : Color.white);
					Sprite sprite = ((!flag && circuitID != 65535) ? this.resources.electricityConnectedIcon : this.resources.electricityInputIcon);
					this.DrawUtilityIcon(powerInputCell, sprite, ref this.inputVisualizer, color, this.GetWireColor(powerInputCell), 1f, false);
				}
				if (this.RequiresPowerOutput)
				{
					int powerOutputCell = this.building.GetPowerOutputCell();
					int circuitID2 = (int)Game.Instance.circuitManager.GetCircuitID(powerOutputCell);
					Color color2 = (this.building.Def.UseWhitePowerOutputConnectorColour ? Color.white : this.resources.electricityOutputColor);
					Color32 color3 = ((component != null && !component.IsEnabled) ? Color.gray : color2);
					Sprite sprite2 = ((!flag && circuitID2 != 65535) ? this.resources.electricityConnectedIcon : this.resources.electricityInputIcon);
					this.DrawUtilityIcon(powerOutputCell, sprite2, ref this.outputVisualizer, color3, this.GetWireColor(powerOutputCell), 1f, false);
					return;
				}
			}
			else
			{
				bool flag2 = true;
				Switch component2 = base.GetComponent<Switch>();
				if (component2 != null)
				{
					int num = Grid.PosToCell(base.transform.GetPosition());
					Color32 color4 = (component2.IsHandlerOn() ? this.resources.switchColor : this.resources.switchOffColor);
					this.DrawUtilityIcon(num, this.resources.switchIcon, ref this.outputVisualizer, color4, Color.white, 1f, false);
					flag2 = false;
				}
				else
				{
					WireUtilityNetworkLink component3 = base.GetComponent<WireUtilityNetworkLink>();
					if (component3 != null)
					{
						int num2;
						int num3;
						component3.GetCells(out num2, out num3);
						this.DrawUtilityIcon(num2, (Game.Instance.circuitManager.GetCircuitID(num2) == ushort.MaxValue) ? this.resources.electricityBridgeIcon : this.resources.electricityConnectedIcon, ref this.inputVisualizer, this.resources.electricityInputColor, Color.white, 1f, false);
						this.DrawUtilityIcon(num3, (Game.Instance.circuitManager.GetCircuitID(num3) == ushort.MaxValue) ? this.resources.electricityBridgeIcon : this.resources.electricityConnectedIcon, ref this.outputVisualizer, this.resources.electricityInputColor, Color.white, 1f, false);
						flag2 = false;
					}
				}
				if (flag2)
				{
					this.DisableIcons();
					return;
				}
			}
		}
		else if (mode == OverlayModes.GasConduits.ID)
		{
			if (!this.RequiresGas && (this.secondary_ports & (BuildingCellVisualizer.Ports.GasIn | BuildingCellVisualizer.Ports.GasOut)) == (BuildingCellVisualizer.Ports)0)
			{
				this.DisableIcons();
				return;
			}
			if ((this.ports & BuildingCellVisualizer.Ports.GasIn) != (BuildingCellVisualizer.Ports)0)
			{
				bool flag3 = null != Grid.Objects[this.building.GetUtilityInputCell(), 12];
				BuildingCellVisualizerResources.ConnectedDisconnectedColours input = this.resources.gasIOColours.input;
				Color color5 = (flag3 ? input.connected : input.disconnected);
				this.DrawUtilityIcon(this.building.GetUtilityInputCell(), this.resources.gasInputIcon, ref this.inputVisualizer, color5);
			}
			if ((this.ports & BuildingCellVisualizer.Ports.GasOut) != (BuildingCellVisualizer.Ports)0)
			{
				bool flag4 = null != Grid.Objects[this.building.GetUtilityOutputCell(), 12];
				BuildingCellVisualizerResources.ConnectedDisconnectedColours output = this.resources.gasIOColours.output;
				Color color6 = (flag4 ? output.connected : output.disconnected);
				this.DrawUtilityIcon(this.building.GetUtilityOutputCell(), this.resources.gasOutputIcon, ref this.outputVisualizer, color6);
			}
			if ((this.secondary_ports & BuildingCellVisualizer.Ports.GasIn) != (BuildingCellVisualizer.Ports)0)
			{
				ISecondaryInput[] components = this.building.GetComponents<ISecondaryInput>();
				CellOffset cellOffset = CellOffset.none;
				ISecondaryInput[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					cellOffset = array[i].GetSecondaryConduitOffset(ConduitType.Gas);
					if (cellOffset != CellOffset.none)
					{
						break;
					}
				}
				Color color7 = BuildingCellVisualizer.secondInputColour;
				if ((this.ports & BuildingCellVisualizer.Ports.GasIn) == (BuildingCellVisualizer.Ports)0)
				{
					bool flag5 = null != Grid.Objects[Grid.OffsetCell(Grid.PosToCell(this.building.transform.GetPosition()), cellOffset), 12];
					BuildingCellVisualizerResources.ConnectedDisconnectedColours input2 = this.resources.gasIOColours.input;
					color7 = (flag5 ? input2.connected : input2.disconnected);
				}
				int visualizerCell = this.GetVisualizerCell(this.building, cellOffset);
				this.DrawUtilityIcon(visualizerCell, this.resources.gasInputIcon, ref this.secondaryInputVisualizer, color7, Color.white, 1.5f, false);
			}
			if ((this.secondary_ports & BuildingCellVisualizer.Ports.GasOut) != (BuildingCellVisualizer.Ports)0)
			{
				ISecondaryOutput[] components2 = this.building.GetComponents<ISecondaryOutput>();
				CellOffset cellOffset2 = CellOffset.none;
				ISecondaryOutput[] array2 = components2;
				for (int i = 0; i < array2.Length; i++)
				{
					cellOffset2 = array2[i].GetSecondaryConduitOffset(ConduitType.Gas);
					if (cellOffset2 != CellOffset.none)
					{
						break;
					}
				}
				Color color8 = BuildingCellVisualizer.secondOutputColour;
				if ((this.ports & BuildingCellVisualizer.Ports.GasOut) == (BuildingCellVisualizer.Ports)0)
				{
					bool flag6 = null != Grid.Objects[Grid.OffsetCell(Grid.PosToCell(this.building.transform.GetPosition()), cellOffset2), 12];
					BuildingCellVisualizerResources.ConnectedDisconnectedColours output2 = this.resources.gasIOColours.output;
					color8 = (flag6 ? output2.connected : output2.disconnected);
				}
				int visualizerCell2 = this.GetVisualizerCell(this.building, cellOffset2);
				this.DrawUtilityIcon(visualizerCell2, this.resources.gasOutputIcon, ref this.secondaryOutputVisualizer, color8, Color.white, 1.5f, false);
				return;
			}
		}
		else if (mode == OverlayModes.LiquidConduits.ID)
		{
			if (!this.RequiresLiquid && (this.secondary_ports & (BuildingCellVisualizer.Ports.LiquidIn | BuildingCellVisualizer.Ports.LiquidOut)) == (BuildingCellVisualizer.Ports)0)
			{
				this.DisableIcons();
				return;
			}
			if ((this.ports & BuildingCellVisualizer.Ports.LiquidIn) != (BuildingCellVisualizer.Ports)0)
			{
				bool flag7 = null != Grid.Objects[this.building.GetUtilityInputCell(), 16];
				BuildingCellVisualizerResources.ConnectedDisconnectedColours input3 = this.resources.liquidIOColours.input;
				Color color9 = (flag7 ? input3.connected : input3.disconnected);
				this.DrawUtilityIcon(this.building.GetUtilityInputCell(), this.resources.liquidInputIcon, ref this.inputVisualizer, color9);
			}
			if ((this.ports & BuildingCellVisualizer.Ports.LiquidOut) != (BuildingCellVisualizer.Ports)0)
			{
				bool flag8 = null != Grid.Objects[this.building.GetUtilityOutputCell(), 16];
				BuildingCellVisualizerResources.ConnectedDisconnectedColours output3 = this.resources.liquidIOColours.output;
				Color color10 = (flag8 ? output3.connected : output3.disconnected);
				this.DrawUtilityIcon(this.building.GetUtilityOutputCell(), this.resources.liquidOutputIcon, ref this.outputVisualizer, color10);
			}
			if ((this.secondary_ports & BuildingCellVisualizer.Ports.LiquidIn) != (BuildingCellVisualizer.Ports)0)
			{
				ISecondaryInput[] components3 = this.building.GetComponents<ISecondaryInput>();
				CellOffset cellOffset3 = CellOffset.none;
				ISecondaryInput[] array = components3;
				for (int i = 0; i < array.Length; i++)
				{
					cellOffset3 = array[i].GetSecondaryConduitOffset(ConduitType.Liquid);
					if (cellOffset3 != CellOffset.none)
					{
						break;
					}
				}
				Color color11 = BuildingCellVisualizer.secondInputColour;
				if ((this.ports & BuildingCellVisualizer.Ports.LiquidIn) == (BuildingCellVisualizer.Ports)0)
				{
					bool flag9 = null != Grid.Objects[Grid.OffsetCell(Grid.PosToCell(this.building.transform.GetPosition()), cellOffset3), 16];
					BuildingCellVisualizerResources.ConnectedDisconnectedColours input4 = this.resources.liquidIOColours.input;
					color11 = (flag9 ? input4.connected : input4.disconnected);
				}
				int visualizerCell3 = this.GetVisualizerCell(this.building, cellOffset3);
				this.DrawUtilityIcon(visualizerCell3, this.resources.liquidInputIcon, ref this.secondaryInputVisualizer, color11, Color.white, 1.5f, false);
			}
			if ((this.secondary_ports & BuildingCellVisualizer.Ports.LiquidOut) != (BuildingCellVisualizer.Ports)0)
			{
				ISecondaryOutput[] components4 = this.building.GetComponents<ISecondaryOutput>();
				CellOffset cellOffset4 = CellOffset.none;
				ISecondaryOutput[] array2 = components4;
				for (int i = 0; i < array2.Length; i++)
				{
					cellOffset4 = array2[i].GetSecondaryConduitOffset(ConduitType.Liquid);
					if (cellOffset4 != CellOffset.none)
					{
						break;
					}
				}
				Color color12 = BuildingCellVisualizer.secondOutputColour;
				if ((this.ports & BuildingCellVisualizer.Ports.LiquidOut) == (BuildingCellVisualizer.Ports)0)
				{
					bool flag10 = null != Grid.Objects[Grid.OffsetCell(Grid.PosToCell(this.building.transform.GetPosition()), cellOffset4), 16];
					BuildingCellVisualizerResources.ConnectedDisconnectedColours output4 = this.resources.liquidIOColours.output;
					color12 = (flag10 ? output4.connected : output4.disconnected);
				}
				int visualizerCell4 = this.GetVisualizerCell(this.building, cellOffset4);
				this.DrawUtilityIcon(visualizerCell4, this.resources.liquidOutputIcon, ref this.secondaryOutputVisualizer, color12, Color.white, 1.5f, false);
				return;
			}
		}
		else if (mode == OverlayModes.SolidConveyor.ID)
		{
			if (!this.RequiresSolid && (this.secondary_ports & (BuildingCellVisualizer.Ports.SolidIn | BuildingCellVisualizer.Ports.SolidOut)) == (BuildingCellVisualizer.Ports)0)
			{
				this.DisableIcons();
				return;
			}
			if ((this.ports & BuildingCellVisualizer.Ports.SolidIn) != (BuildingCellVisualizer.Ports)0)
			{
				bool flag11 = null != Grid.Objects[this.building.GetUtilityInputCell(), 20];
				BuildingCellVisualizerResources.ConnectedDisconnectedColours input5 = this.resources.liquidIOColours.input;
				Color color13 = (flag11 ? input5.connected : input5.disconnected);
				this.DrawUtilityIcon(this.building.GetUtilityInputCell(), this.resources.liquidInputIcon, ref this.inputVisualizer, color13);
			}
			if ((this.ports & BuildingCellVisualizer.Ports.SolidOut) != (BuildingCellVisualizer.Ports)0)
			{
				bool flag12 = null != Grid.Objects[this.building.GetUtilityOutputCell(), 20];
				BuildingCellVisualizerResources.ConnectedDisconnectedColours output5 = this.resources.liquidIOColours.output;
				Color color14 = (flag12 ? output5.connected : output5.disconnected);
				this.DrawUtilityIcon(this.building.GetUtilityOutputCell(), this.resources.liquidOutputIcon, ref this.outputVisualizer, color14);
			}
			if ((this.secondary_ports & BuildingCellVisualizer.Ports.SolidIn) != (BuildingCellVisualizer.Ports)0)
			{
				ISecondaryInput[] components5 = this.building.GetComponents<ISecondaryInput>();
				CellOffset cellOffset5 = CellOffset.none;
				ISecondaryInput[] array = components5;
				for (int i = 0; i < array.Length; i++)
				{
					cellOffset5 = array[i].GetSecondaryConduitOffset(ConduitType.Solid);
					if (cellOffset5 != CellOffset.none)
					{
						break;
					}
				}
				Color color15 = BuildingCellVisualizer.secondInputColour;
				if ((this.ports & BuildingCellVisualizer.Ports.SolidIn) == (BuildingCellVisualizer.Ports)0)
				{
					bool flag13 = null != Grid.Objects[Grid.OffsetCell(Grid.PosToCell(this.building.transform.GetPosition()), cellOffset5), 20];
					BuildingCellVisualizerResources.ConnectedDisconnectedColours input6 = this.resources.liquidIOColours.input;
					color15 = (flag13 ? input6.connected : input6.disconnected);
				}
				int visualizerCell5 = this.GetVisualizerCell(this.building, cellOffset5);
				this.DrawUtilityIcon(visualizerCell5, this.resources.liquidInputIcon, ref this.secondaryInputVisualizer, color15, Color.white, 1.5f, false);
			}
			if ((this.secondary_ports & BuildingCellVisualizer.Ports.SolidOut) != (BuildingCellVisualizer.Ports)0)
			{
				ISecondaryOutput[] components6 = this.building.GetComponents<ISecondaryOutput>();
				CellOffset cellOffset6 = CellOffset.none;
				ISecondaryOutput[] array2 = components6;
				for (int i = 0; i < array2.Length; i++)
				{
					cellOffset6 = array2[i].GetSecondaryConduitOffset(ConduitType.Solid);
					if (cellOffset6 != CellOffset.none)
					{
						break;
					}
				}
				Color color16 = BuildingCellVisualizer.secondOutputColour;
				if ((this.ports & BuildingCellVisualizer.Ports.SolidOut) == (BuildingCellVisualizer.Ports)0)
				{
					bool flag14 = null != Grid.Objects[Grid.OffsetCell(Grid.PosToCell(this.building.transform.GetPosition()), cellOffset6), 20];
					BuildingCellVisualizerResources.ConnectedDisconnectedColours output6 = this.resources.liquidIOColours.output;
					color16 = (flag14 ? output6.connected : output6.disconnected);
				}
				int visualizerCell6 = this.GetVisualizerCell(this.building, cellOffset6);
				this.DrawUtilityIcon(visualizerCell6, this.resources.liquidOutputIcon, ref this.secondaryOutputVisualizer, color16, Color.white, 1.5f, false);
				return;
			}
		}
		else if (mode == OverlayModes.Disease.ID)
		{
			if (this.diseaseSourceSprite != null)
			{
				int utilityOutputCell = this.building.GetUtilityOutputCell();
				this.DrawUtilityIcon(utilityOutputCell, this.diseaseSourceSprite, ref this.inputVisualizer, this.diseaseSourceColour);
				return;
			}
		}
		else if (mode == OverlayModes.Radiation.ID && this.RequiresHighEnergyParticles)
		{
			int num4 = 3;
			if (this.building.Def.UseHighEnergyParticleInputPort)
			{
				int highEnergyParticleInputCell = this.building.GetHighEnergyParticleInputCell();
				this.DrawUtilityIcon(highEnergyParticleInputCell, this.resources.highEnergyParticleInputIcon, ref this.inputVisualizer, this.resources.highEnergyParticleInputColour, Color.white, (float)num4, true);
			}
			if (this.building.Def.UseHighEnergyParticleOutputPort)
			{
				int highEnergyParticleOutputCell = this.building.GetHighEnergyParticleOutputCell();
				IHighEnergyParticleDirection component4 = this.building.GetComponent<IHighEnergyParticleDirection>();
				Sprite sprite3 = this.resources.highEnergyParticleOutputIcons[0];
				if (component4 != null)
				{
					int directionIndex = EightDirectionUtil.GetDirectionIndex(component4.Direction);
					sprite3 = this.resources.highEnergyParticleOutputIcons[directionIndex];
				}
				this.DrawUtilityIcon(highEnergyParticleOutputCell, sprite3, ref this.outputVisualizer, this.resources.highEnergyParticleOutputColour, Color.white, (float)num4, true);
			}
		}
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x0007EB30 File Offset: 0x0007CD30
	private int GetVisualizerCell(Building building, CellOffset offset)
	{
		CellOffset rotatedOffset = building.GetRotatedOffset(offset);
		return Grid.OffsetCell(building.GetCell(), rotatedOffset);
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x0007EB54 File Offset: 0x0007CD54
	public void DisableIcons()
	{
		if (this.inputVisualizer != null && this.inputVisualizer.activeInHierarchy)
		{
			this.inputVisualizer.SetActive(false);
		}
		if (this.outputVisualizer != null && this.outputVisualizer.activeInHierarchy)
		{
			this.outputVisualizer.SetActive(false);
		}
		if (this.secondaryInputVisualizer != null && this.secondaryInputVisualizer.activeInHierarchy)
		{
			this.secondaryInputVisualizer.SetActive(false);
		}
		if (this.secondaryOutputVisualizer != null && this.secondaryOutputVisualizer.activeInHierarchy)
		{
			this.secondaryOutputVisualizer.SetActive(false);
		}
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x0007EBFD File Offset: 0x0007CDFD
	private void DrawUtilityIcon(int cell, Sprite icon_img, ref GameObject visualizerObj)
	{
		this.DrawUtilityIcon(cell, icon_img, ref visualizerObj, Color.white, Color.white, 1.5f, false);
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x0007EC18 File Offset: 0x0007CE18
	private void DrawUtilityIcon(int cell, Sprite icon_img, ref GameObject visualizerObj, Color tint)
	{
		this.DrawUtilityIcon(cell, icon_img, ref visualizerObj, tint, Color.white, 1.5f, false);
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x0007EC30 File Offset: 0x0007CE30
	private void DrawUtilityIcon(int cell, Sprite icon_img, ref GameObject visualizerObj, Color tint, Color connectorColor, float scaleMultiplier = 1.5f, bool hideBG = false)
	{
		Vector3 vector = Grid.CellToPosCCC(cell, Grid.SceneLayer.Building);
		if (visualizerObj == null)
		{
			visualizerObj = global::Util.KInstantiate(Assets.UIPrefabs.ResourceVisualizer, GameScreenManager.Instance.worldSpaceCanvas, null);
			visualizerObj.transform.SetAsFirstSibling();
			this.icons.Add(visualizerObj, visualizerObj.transform.GetChild(0).GetComponent<Image>());
		}
		if (!visualizerObj.gameObject.activeInHierarchy)
		{
			visualizerObj.gameObject.SetActive(true);
		}
		visualizerObj.GetComponent<Image>().enabled = !hideBG;
		this.icons[visualizerObj].raycastTarget = this.enableRaycast;
		this.icons[visualizerObj].sprite = icon_img;
		visualizerObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = tint;
		visualizerObj.transform.SetPosition(vector);
		if (visualizerObj.GetComponent<SizePulse>() == null)
		{
			visualizerObj.transform.localScale = Vector3.one * scaleMultiplier;
		}
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x0007ED41 File Offset: 0x0007CF41
	public Image GetOutputIcon()
	{
		if (!(this.outputVisualizer == null))
		{
			return this.outputVisualizer.transform.GetChild(0).GetComponent<Image>();
		}
		return null;
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x0007ED69 File Offset: 0x0007CF69
	public Image GetInputIcon()
	{
		if (!(this.inputVisualizer == null))
		{
			return this.inputVisualizer.transform.GetChild(0).GetComponent<Image>();
		}
		return null;
	}

	// Token: 0x04000D54 RID: 3412
	private BuildingCellVisualizerResources resources;

	// Token: 0x04000D55 RID: 3413
	[MyCmpReq]
	private Building building;

	// Token: 0x04000D56 RID: 3414
	[SerializeField]
	public static Color32 secondOutputColour = new Color(0.9843137f, 0.6901961f, 0.23137255f);

	// Token: 0x04000D57 RID: 3415
	[SerializeField]
	public static Color32 secondInputColour = new Color(0.9843137f, 0.6901961f, 0.23137255f);

	// Token: 0x04000D58 RID: 3416
	private const BuildingCellVisualizer.Ports POWER_PORTS = BuildingCellVisualizer.Ports.PowerIn | BuildingCellVisualizer.Ports.PowerOut;

	// Token: 0x04000D59 RID: 3417
	private const BuildingCellVisualizer.Ports GAS_PORTS = BuildingCellVisualizer.Ports.GasIn | BuildingCellVisualizer.Ports.GasOut;

	// Token: 0x04000D5A RID: 3418
	private const BuildingCellVisualizer.Ports LIQUID_PORTS = BuildingCellVisualizer.Ports.LiquidIn | BuildingCellVisualizer.Ports.LiquidOut;

	// Token: 0x04000D5B RID: 3419
	private const BuildingCellVisualizer.Ports SOLID_PORTS = BuildingCellVisualizer.Ports.SolidIn | BuildingCellVisualizer.Ports.SolidOut;

	// Token: 0x04000D5C RID: 3420
	private const BuildingCellVisualizer.Ports MATTER_PORTS = BuildingCellVisualizer.Ports.GasIn | BuildingCellVisualizer.Ports.GasOut | BuildingCellVisualizer.Ports.LiquidIn | BuildingCellVisualizer.Ports.LiquidOut | BuildingCellVisualizer.Ports.SolidIn | BuildingCellVisualizer.Ports.SolidOut;

	// Token: 0x04000D5D RID: 3421
	private BuildingCellVisualizer.Ports ports;

	// Token: 0x04000D5E RID: 3422
	private BuildingCellVisualizer.Ports secondary_ports;

	// Token: 0x04000D5F RID: 3423
	private Sprite diseaseSourceSprite;

	// Token: 0x04000D60 RID: 3424
	private Color32 diseaseSourceColour;

	// Token: 0x04000D61 RID: 3425
	private GameObject inputVisualizer;

	// Token: 0x04000D62 RID: 3426
	private GameObject outputVisualizer;

	// Token: 0x04000D63 RID: 3427
	private GameObject secondaryInputVisualizer;

	// Token: 0x04000D64 RID: 3428
	private GameObject secondaryOutputVisualizer;

	// Token: 0x04000D65 RID: 3429
	private bool enableRaycast;

	// Token: 0x04000D66 RID: 3430
	private Dictionary<GameObject, Image> icons;

	// Token: 0x0200106D RID: 4205
	[Flags]
	private enum Ports
	{
		// Token: 0x04005786 RID: 22406
		PowerIn = 1,
		// Token: 0x04005787 RID: 22407
		PowerOut = 2,
		// Token: 0x04005788 RID: 22408
		GasIn = 4,
		// Token: 0x04005789 RID: 22409
		GasOut = 8,
		// Token: 0x0400578A RID: 22410
		LiquidIn = 16,
		// Token: 0x0400578B RID: 22411
		LiquidOut = 32,
		// Token: 0x0400578C RID: 22412
		SolidIn = 64,
		// Token: 0x0400578D RID: 22413
		SolidOut = 128,
		// Token: 0x0400578E RID: 22414
		HighEnergyParticle = 256
	}
}
