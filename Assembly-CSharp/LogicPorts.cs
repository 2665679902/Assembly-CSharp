using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000803 RID: 2051
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicPorts")]
public class LogicPorts : KMonoBehaviour, IGameObjectEffectDescriptor, IRenderEveryTick
{
	// Token: 0x06003B5B RID: 15195 RVA: 0x00149503 File Offset: 0x00147703
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.autoRegisterSimRender = false;
	}

	// Token: 0x06003B5C RID: 15196 RVA: 0x00149514 File Offset: 0x00147714
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.isPhysical = base.GetComponent<BuildingComplete>() != null;
		if (!this.isPhysical && base.GetComponent<BuildingUnderConstruction>() == null)
		{
			OverlayScreen instance = OverlayScreen.Instance;
			instance.OnOverlayChanged = (Action<HashedString>)Delegate.Combine(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
			this.OnOverlayChanged(OverlayScreen.Instance.mode);
			this.CreateVisualizers();
			SimAndRenderScheduler.instance.Add(this, false);
			return;
		}
		if (this.isPhysical)
		{
			this.UpdateMissingWireIcon();
			this.CreatePhysicalPorts(false);
			return;
		}
		this.CreateVisualizers();
	}

	// Token: 0x06003B5D RID: 15197 RVA: 0x001495B8 File Offset: 0x001477B8
	protected override void OnCleanUp()
	{
		OverlayScreen instance = OverlayScreen.Instance;
		instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
		this.DestroyVisualizers();
		if (this.isPhysical)
		{
			this.DestroyPhysicalPorts();
		}
		base.OnCleanUp();
	}

	// Token: 0x06003B5E RID: 15198 RVA: 0x00149605 File Offset: 0x00147805
	public void RenderEveryTick(float dt)
	{
		this.CreateVisualizers();
	}

	// Token: 0x06003B5F RID: 15199 RVA: 0x0014960D File Offset: 0x0014780D
	public void HackRefreshVisualizers()
	{
		this.CreateVisualizers();
	}

	// Token: 0x06003B60 RID: 15200 RVA: 0x00149618 File Offset: 0x00147818
	private void CreateVisualizers()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		bool flag = num != this.cell;
		this.cell = num;
		if (!flag)
		{
			Rotatable component = base.GetComponent<Rotatable>();
			if (component != null)
			{
				Orientation orientation = component.GetOrientation();
				flag = orientation != this.orientation;
				this.orientation = orientation;
			}
		}
		if (!flag)
		{
			return;
		}
		this.DestroyVisualizers();
		if (this.outputPortInfo != null)
		{
			this.outputPorts = new List<ILogicUIElement>();
			for (int i = 0; i < this.outputPortInfo.Length; i++)
			{
				LogicPorts.Port port = this.outputPortInfo[i];
				LogicPortVisualizer logicPortVisualizer = new LogicPortVisualizer(this.GetActualCell(port.cellOffset), port.spriteType);
				this.outputPorts.Add(logicPortVisualizer);
				Game.Instance.logicCircuitManager.AddVisElem(logicPortVisualizer);
			}
		}
		if (this.inputPortInfo != null)
		{
			this.inputPorts = new List<ILogicUIElement>();
			for (int j = 0; j < this.inputPortInfo.Length; j++)
			{
				LogicPorts.Port port2 = this.inputPortInfo[j];
				LogicPortVisualizer logicPortVisualizer2 = new LogicPortVisualizer(this.GetActualCell(port2.cellOffset), port2.spriteType);
				this.inputPorts.Add(logicPortVisualizer2);
				Game.Instance.logicCircuitManager.AddVisElem(logicPortVisualizer2);
			}
		}
	}

	// Token: 0x06003B61 RID: 15201 RVA: 0x00149768 File Offset: 0x00147968
	private void DestroyVisualizers()
	{
		if (this.outputPorts != null)
		{
			foreach (ILogicUIElement logicUIElement in this.outputPorts)
			{
				Game.Instance.logicCircuitManager.RemoveVisElem(logicUIElement);
			}
		}
		if (this.inputPorts != null)
		{
			foreach (ILogicUIElement logicUIElement2 in this.inputPorts)
			{
				Game.Instance.logicCircuitManager.RemoveVisElem(logicUIElement2);
			}
		}
	}

	// Token: 0x06003B62 RID: 15202 RVA: 0x00149820 File Offset: 0x00147A20
	private void CreatePhysicalPorts(bool forceCreate = false)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (num == this.cell && !forceCreate)
		{
			return;
		}
		this.cell = num;
		this.DestroyVisualizers();
		if (this.outputPortInfo != null)
		{
			this.outputPorts = new List<ILogicUIElement>();
			for (int i = 0; i < this.outputPortInfo.Length; i++)
			{
				LogicPorts.Port info2 = this.outputPortInfo[i];
				LogicEventSender logicEventSender = new LogicEventSender(info2.id, this.GetActualCell(info2.cellOffset), delegate(int new_value)
				{
					if (this != null)
					{
						this.OnLogicValueChanged(info2.id, new_value);
					}
				}, new Action<int, bool>(this.OnLogicNetworkConnectionChanged), info2.spriteType);
				this.outputPorts.Add(logicEventSender);
				Game.Instance.logicCircuitManager.AddVisElem(logicEventSender);
				Game.Instance.logicCircuitSystem.AddToNetworks(logicEventSender.GetLogicUICell(), logicEventSender, true);
			}
			if (this.serializedOutputValues != null && this.serializedOutputValues.Length == this.outputPorts.Count)
			{
				for (int j = 0; j < this.outputPorts.Count; j++)
				{
					(this.outputPorts[j] as LogicEventSender).SetValue(this.serializedOutputValues[j]);
				}
			}
		}
		this.serializedOutputValues = null;
		if (this.inputPortInfo != null)
		{
			this.inputPorts = new List<ILogicUIElement>();
			for (int k = 0; k < this.inputPortInfo.Length; k++)
			{
				LogicPorts.Port info = this.inputPortInfo[k];
				LogicEventHandler logicEventHandler = new LogicEventHandler(this.GetActualCell(info.cellOffset), delegate(int new_value)
				{
					if (this != null)
					{
						this.OnLogicValueChanged(info.id, new_value);
					}
				}, new Action<int, bool>(this.OnLogicNetworkConnectionChanged), info.spriteType);
				this.inputPorts.Add(logicEventHandler);
				Game.Instance.logicCircuitManager.AddVisElem(logicEventHandler);
				Game.Instance.logicCircuitSystem.AddToNetworks(logicEventHandler.GetLogicUICell(), logicEventHandler, true);
			}
		}
	}

	// Token: 0x06003B63 RID: 15203 RVA: 0x00149A48 File Offset: 0x00147C48
	private bool ShowMissingWireIcon()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		if (this.outputPortInfo != null)
		{
			for (int i = 0; i < this.outputPortInfo.Length; i++)
			{
				LogicPorts.Port port = this.outputPortInfo[i];
				if (port.requiresConnection)
				{
					int portCell = this.GetPortCell(port.id);
					if (logicCircuitManager.GetNetworkForCell(portCell) == null)
					{
						return true;
					}
				}
			}
		}
		if (this.inputPortInfo != null)
		{
			for (int j = 0; j < this.inputPortInfo.Length; j++)
			{
				LogicPorts.Port port2 = this.inputPortInfo[j];
				if (port2.requiresConnection)
				{
					int portCell2 = this.GetPortCell(port2.id);
					if (logicCircuitManager.GetNetworkForCell(portCell2) == null)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003B64 RID: 15204 RVA: 0x00149AFB File Offset: 0x00147CFB
	public void OnMove()
	{
		this.DestroyPhysicalPorts();
		this.CreatePhysicalPorts(false);
	}

	// Token: 0x06003B65 RID: 15205 RVA: 0x00149B0A File Offset: 0x00147D0A
	private void OnLogicNetworkConnectionChanged(int cell, bool connected)
	{
		this.UpdateMissingWireIcon();
	}

	// Token: 0x06003B66 RID: 15206 RVA: 0x00149B12 File Offset: 0x00147D12
	private void UpdateMissingWireIcon()
	{
		LogicCircuitManager.ToggleNoWireConnected(this.ShowMissingWireIcon(), base.gameObject);
	}

	// Token: 0x06003B67 RID: 15207 RVA: 0x00149B28 File Offset: 0x00147D28
	private void DestroyPhysicalPorts()
	{
		if (this.outputPorts != null)
		{
			foreach (ILogicUIElement logicUIElement in this.outputPorts)
			{
				ILogicEventSender logicEventSender = (ILogicEventSender)logicUIElement;
				Game.Instance.logicCircuitSystem.RemoveFromNetworks(logicEventSender.GetLogicCell(), logicEventSender, true);
			}
		}
		if (this.inputPorts != null)
		{
			for (int i = 0; i < this.inputPorts.Count; i++)
			{
				LogicEventHandler logicEventHandler = this.inputPorts[i] as LogicEventHandler;
				if (logicEventHandler != null)
				{
					Game.Instance.logicCircuitSystem.RemoveFromNetworks(logicEventHandler.GetLogicCell(), logicEventHandler, true);
				}
			}
		}
	}

	// Token: 0x06003B68 RID: 15208 RVA: 0x00149BE4 File Offset: 0x00147DE4
	private void OnLogicValueChanged(HashedString port_id, int new_value)
	{
		if (base.gameObject != null)
		{
			base.gameObject.Trigger(-801688580, new LogicValueChanged
			{
				portID = port_id,
				newValue = new_value
			});
		}
	}

	// Token: 0x06003B69 RID: 15209 RVA: 0x00149C30 File Offset: 0x00147E30
	private int GetActualCell(CellOffset offset)
	{
		Rotatable component = base.GetComponent<Rotatable>();
		if (component != null)
		{
			offset = component.GetRotatedCellOffset(offset);
		}
		return Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), offset);
	}

	// Token: 0x06003B6A RID: 15210 RVA: 0x00149C6C File Offset: 0x00147E6C
	public bool TryGetPortAtCell(int cell, out LogicPorts.Port port, out bool isInput)
	{
		foreach (LogicPorts.Port port2 in this.inputPortInfo)
		{
			if (this.GetActualCell(port2.cellOffset) == cell)
			{
				port = port2;
				isInput = true;
				return true;
			}
		}
		foreach (LogicPorts.Port port3 in this.outputPortInfo)
		{
			if (this.GetActualCell(port3.cellOffset) == cell)
			{
				port = port3;
				isInput = false;
				return true;
			}
		}
		port = default(LogicPorts.Port);
		isInput = false;
		return false;
	}

	// Token: 0x06003B6B RID: 15211 RVA: 0x00149CF4 File Offset: 0x00147EF4
	public void SendSignal(HashedString port_id, int new_value)
	{
		if (this.outputPortInfo != null && this.outputPorts == null)
		{
			this.CreatePhysicalPorts(true);
		}
		foreach (ILogicUIElement logicUIElement in this.outputPorts)
		{
			LogicEventSender logicEventSender = (LogicEventSender)logicUIElement;
			if (logicEventSender.ID == port_id)
			{
				logicEventSender.SetValue(new_value);
				break;
			}
		}
	}

	// Token: 0x06003B6C RID: 15212 RVA: 0x00149D74 File Offset: 0x00147F74
	public int GetPortCell(HashedString port_id)
	{
		foreach (LogicPorts.Port port in this.inputPortInfo)
		{
			if (port.id == port_id)
			{
				return this.GetActualCell(port.cellOffset);
			}
		}
		foreach (LogicPorts.Port port2 in this.outputPortInfo)
		{
			if (port2.id == port_id)
			{
				return this.GetActualCell(port2.cellOffset);
			}
		}
		return -1;
	}

	// Token: 0x06003B6D RID: 15213 RVA: 0x00149DF4 File Offset: 0x00147FF4
	public int GetInputValue(HashedString port_id)
	{
		int num = 0;
		while (num < this.inputPortInfo.Length && this.inputPorts != null)
		{
			if (this.inputPortInfo[num].id == port_id)
			{
				LogicEventHandler logicEventHandler = this.inputPorts[num] as LogicEventHandler;
				if (logicEventHandler == null)
				{
					return 0;
				}
				return logicEventHandler.Value;
			}
			else
			{
				num++;
			}
		}
		return 0;
	}

	// Token: 0x06003B6E RID: 15214 RVA: 0x00149E54 File Offset: 0x00148054
	public int GetOutputValue(HashedString port_id)
	{
		for (int i = 0; i < this.outputPorts.Count; i++)
		{
			LogicEventSender logicEventSender = this.outputPorts[i] as LogicEventSender;
			if (logicEventSender == null)
			{
				return 0;
			}
			if (logicEventSender.ID == port_id)
			{
				return logicEventSender.GetLogicValue();
			}
		}
		return 0;
	}

	// Token: 0x06003B6F RID: 15215 RVA: 0x00149EA4 File Offset: 0x001480A4
	public bool IsPortConnected(HashedString port_id)
	{
		int portCell = this.GetPortCell(port_id);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell) != null;
	}

	// Token: 0x06003B70 RID: 15216 RVA: 0x00149ECC File Offset: 0x001480CC
	private void OnOverlayChanged(HashedString mode)
	{
		if (mode == OverlayModes.Logic.ID)
		{
			base.enabled = true;
			this.CreateVisualizers();
			return;
		}
		base.enabled = false;
		this.DestroyVisualizers();
	}

	// Token: 0x06003B71 RID: 15217 RVA: 0x00149EF8 File Offset: 0x001480F8
	public LogicWire.BitDepth GetConnectedWireBitDepth(HashedString port_id)
	{
		LogicWire.BitDepth bitDepth = LogicWire.BitDepth.NumRatings;
		int portCell = this.GetPortCell(port_id);
		GameObject gameObject = Grid.Objects[portCell, 31];
		if (gameObject != null)
		{
			LogicWire component = gameObject.GetComponent<LogicWire>();
			if (component != null)
			{
				bitDepth = component.MaxBitDepth;
			}
		}
		return bitDepth;
	}

	// Token: 0x06003B72 RID: 15218 RVA: 0x00149F40 File Offset: 0x00148140
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		LogicPorts component = go.GetComponent<LogicPorts>();
		if (component != null)
		{
			if (component.inputPortInfo != null && component.inputPortInfo.Length != 0)
			{
				Descriptor descriptor = new Descriptor(UI.LOGIC_PORTS.INPUT_PORTS, UI.LOGIC_PORTS.INPUT_PORTS_TOOLTIP, Descriptor.DescriptorType.Effect, false);
				list.Add(descriptor);
				foreach (LogicPorts.Port port in component.inputPortInfo)
				{
					string text = string.Format(UI.LOGIC_PORTS.INPUT_PORT_TOOLTIP, port.activeDescription, port.inactiveDescription);
					descriptor = new Descriptor(port.description, text, Descriptor.DescriptorType.Effect, false);
					descriptor.IncreaseIndent();
					list.Add(descriptor);
				}
			}
			if (component.outputPortInfo != null && component.outputPortInfo.Length != 0)
			{
				Descriptor descriptor2 = new Descriptor(UI.LOGIC_PORTS.OUTPUT_PORTS, UI.LOGIC_PORTS.OUTPUT_PORTS_TOOLTIP, Descriptor.DescriptorType.Effect, false);
				list.Add(descriptor2);
				foreach (LogicPorts.Port port2 in component.outputPortInfo)
				{
					string text2 = string.Format(UI.LOGIC_PORTS.OUTPUT_PORT_TOOLTIP, port2.activeDescription, port2.inactiveDescription);
					descriptor2 = new Descriptor(port2.description, text2, Descriptor.DescriptorType.Effect, false);
					descriptor2.IncreaseIndent();
					list.Add(descriptor2);
				}
			}
		}
		return list;
	}

	// Token: 0x06003B73 RID: 15219 RVA: 0x0014A0A8 File Offset: 0x001482A8
	[OnSerializing]
	private void OnSerializing()
	{
		if (this.isPhysical && this.outputPorts != null)
		{
			this.serializedOutputValues = new int[this.outputPorts.Count];
			for (int i = 0; i < this.outputPorts.Count; i++)
			{
				LogicEventSender logicEventSender = this.outputPorts[i] as LogicEventSender;
				this.serializedOutputValues[i] = logicEventSender.GetLogicValue();
			}
		}
	}

	// Token: 0x06003B74 RID: 15220 RVA: 0x0014A111 File Offset: 0x00148311
	[OnSerialized]
	private void OnSerialized()
	{
		this.serializedOutputValues = null;
	}

	// Token: 0x040026CB RID: 9931
	[SerializeField]
	public LogicPorts.Port[] outputPortInfo;

	// Token: 0x040026CC RID: 9932
	[SerializeField]
	public LogicPorts.Port[] inputPortInfo;

	// Token: 0x040026CD RID: 9933
	public List<ILogicUIElement> outputPorts;

	// Token: 0x040026CE RID: 9934
	public List<ILogicUIElement> inputPorts;

	// Token: 0x040026CF RID: 9935
	private int cell = -1;

	// Token: 0x040026D0 RID: 9936
	private Orientation orientation = Orientation.NumRotations;

	// Token: 0x040026D1 RID: 9937
	[Serialize]
	private int[] serializedOutputValues;

	// Token: 0x040026D2 RID: 9938
	private bool isPhysical;

	// Token: 0x02001557 RID: 5463
	[Serializable]
	public struct Port
	{
		// Token: 0x060083A9 RID: 33705 RVA: 0x002E7E62 File Offset: 0x002E6062
		public Port(HashedString id, CellOffset cell_offset, string description, string activeDescription, string inactiveDescription, bool show_wire_missing_icon, LogicPortSpriteType sprite_type, bool display_custom_name = false)
		{
			this.id = id;
			this.cellOffset = cell_offset;
			this.description = description;
			this.activeDescription = activeDescription;
			this.inactiveDescription = inactiveDescription;
			this.requiresConnection = show_wire_missing_icon;
			this.spriteType = sprite_type;
			this.displayCustomName = display_custom_name;
		}

		// Token: 0x060083AA RID: 33706 RVA: 0x002E7EA1 File Offset: 0x002E60A1
		public static LogicPorts.Port InputPort(HashedString id, CellOffset cell_offset, string description, string activeDescription, string inactiveDescription, bool show_wire_missing_icon = false, bool display_custom_name = false)
		{
			return new LogicPorts.Port(id, cell_offset, description, activeDescription, inactiveDescription, show_wire_missing_icon, LogicPortSpriteType.Input, display_custom_name);
		}

		// Token: 0x060083AB RID: 33707 RVA: 0x002E7EB3 File Offset: 0x002E60B3
		public static LogicPorts.Port OutputPort(HashedString id, CellOffset cell_offset, string description, string activeDescription, string inactiveDescription, bool show_wire_missing_icon = false, bool display_custom_name = false)
		{
			return new LogicPorts.Port(id, cell_offset, description, activeDescription, inactiveDescription, show_wire_missing_icon, LogicPortSpriteType.Output, display_custom_name);
		}

		// Token: 0x060083AC RID: 33708 RVA: 0x002E7EC5 File Offset: 0x002E60C5
		public static LogicPorts.Port RibbonInputPort(HashedString id, CellOffset cell_offset, string description, string activeDescription, string inactiveDescription, bool show_wire_missing_icon = false, bool display_custom_name = false)
		{
			return new LogicPorts.Port(id, cell_offset, description, activeDescription, inactiveDescription, show_wire_missing_icon, LogicPortSpriteType.RibbonInput, display_custom_name);
		}

		// Token: 0x060083AD RID: 33709 RVA: 0x002E7ED7 File Offset: 0x002E60D7
		public static LogicPorts.Port RibbonOutputPort(HashedString id, CellOffset cell_offset, string description, string activeDescription, string inactiveDescription, bool show_wire_missing_icon = false, bool display_custom_name = false)
		{
			return new LogicPorts.Port(id, cell_offset, description, activeDescription, inactiveDescription, show_wire_missing_icon, LogicPortSpriteType.RibbonOutput, display_custom_name);
		}

		// Token: 0x04006676 RID: 26230
		public HashedString id;

		// Token: 0x04006677 RID: 26231
		public CellOffset cellOffset;

		// Token: 0x04006678 RID: 26232
		public string description;

		// Token: 0x04006679 RID: 26233
		public string activeDescription;

		// Token: 0x0400667A RID: 26234
		public string inactiveDescription;

		// Token: 0x0400667B RID: 26235
		public bool requiresConnection;

		// Token: 0x0400667C RID: 26236
		public LogicPortSpriteType spriteType;

		// Token: 0x0400667D RID: 26237
		public bool displayCustomName;
	}
}
