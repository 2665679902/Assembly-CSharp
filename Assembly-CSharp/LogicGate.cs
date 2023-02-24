using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005E9 RID: 1513
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicGate : LogicGateBase, ILogicEventSender, ILogicNetworkConnection
{
	// Token: 0x06002655 RID: 9813 RVA: 0x000CE2B8 File Offset: 0x000CC4B8
	protected override void OnSpawn()
	{
		this.inputOne = new LogicEventHandler(base.InputCellOne, new Action<int>(this.UpdateState), null, LogicPortSpriteType.Input);
		if (base.RequiresTwoInputs)
		{
			this.inputTwo = new LogicEventHandler(base.InputCellTwo, new Action<int>(this.UpdateState), null, LogicPortSpriteType.Input);
		}
		else if (base.RequiresFourInputs)
		{
			this.inputTwo = new LogicEventHandler(base.InputCellTwo, new Action<int>(this.UpdateState), null, LogicPortSpriteType.Input);
			this.inputThree = new LogicEventHandler(base.InputCellThree, new Action<int>(this.UpdateState), null, LogicPortSpriteType.Input);
			this.inputFour = new LogicEventHandler(base.InputCellFour, new Action<int>(this.UpdateState), null, LogicPortSpriteType.Input);
		}
		if (base.RequiresControlInputs)
		{
			this.controlOne = new LogicEventHandler(base.ControlCellOne, new Action<int>(this.UpdateState), null, LogicPortSpriteType.ControlInput);
			this.controlTwo = new LogicEventHandler(base.ControlCellTwo, new Action<int>(this.UpdateState), null, LogicPortSpriteType.ControlInput);
		}
		if (base.RequiresFourOutputs)
		{
			this.outputTwo = new LogicPortVisualizer(base.OutputCellTwo, LogicPortSpriteType.Output);
			this.outputThree = new LogicPortVisualizer(base.OutputCellThree, LogicPortSpriteType.Output);
			this.outputFour = new LogicPortVisualizer(base.OutputCellFour, LogicPortSpriteType.Output);
			this.outputTwoSender = new LogicEventSender(LogicGateBase.OUTPUT_TWO_PORT_ID, base.OutputCellTwo, delegate(int new_value)
			{
				if (this != null)
				{
					this.OnAdditionalOutputsLogicValueChanged(LogicGateBase.OUTPUT_TWO_PORT_ID, new_value);
				}
			}, null, LogicPortSpriteType.Output);
			this.outputThreeSender = new LogicEventSender(LogicGateBase.OUTPUT_THREE_PORT_ID, base.OutputCellThree, delegate(int new_value)
			{
				if (this != null)
				{
					this.OnAdditionalOutputsLogicValueChanged(LogicGateBase.OUTPUT_THREE_PORT_ID, new_value);
				}
			}, null, LogicPortSpriteType.Output);
			this.outputFourSender = new LogicEventSender(LogicGateBase.OUTPUT_FOUR_PORT_ID, base.OutputCellFour, delegate(int new_value)
			{
				if (this != null)
				{
					this.OnAdditionalOutputsLogicValueChanged(LogicGateBase.OUTPUT_FOUR_PORT_ID, new_value);
				}
			}, null, LogicPortSpriteType.Output);
		}
		base.Subscribe<LogicGate>(774203113, LogicGate.OnBuildingBrokenDelegate);
		base.Subscribe<LogicGate>(-1735440190, LogicGate.OnBuildingFullyRepairedDelegate);
		BuildingHP component = base.GetComponent<BuildingHP>();
		if (component == null || !component.IsBroken)
		{
			this.Connect();
		}
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x000CE4A5 File Offset: 0x000CC6A5
	protected override void OnCleanUp()
	{
		this.cleaningUp = true;
		this.Disconnect();
		base.Unsubscribe<LogicGate>(774203113, LogicGate.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<LogicGate>(-1735440190, LogicGate.OnBuildingFullyRepairedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x06002657 RID: 9815 RVA: 0x000CE4DC File Offset: 0x000CC6DC
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x06002658 RID: 9816 RVA: 0x000CE4E4 File Offset: 0x000CC6E4
	private void OnBuildingFullyRepaired(object data)
	{
		this.Connect();
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x000CE4EC File Offset: 0x000CC6EC
	private void Connect()
	{
		if (!this.connected)
		{
			LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
			UtilityNetworkManager<LogicCircuitNetwork, LogicWire> logicCircuitSystem = Game.Instance.logicCircuitSystem;
			this.connected = true;
			int outputCellOne = base.OutputCellOne;
			logicCircuitSystem.AddToNetworks(outputCellOne, this, true);
			this.outputOne = new LogicPortVisualizer(outputCellOne, LogicPortSpriteType.Output);
			logicCircuitManager.AddVisElem(this.outputOne);
			if (base.RequiresFourOutputs)
			{
				this.outputTwo = new LogicPortVisualizer(base.OutputCellTwo, LogicPortSpriteType.Output);
				logicCircuitSystem.AddToNetworks(base.OutputCellTwo, this.outputTwoSender, true);
				logicCircuitManager.AddVisElem(this.outputTwo);
				this.outputThree = new LogicPortVisualizer(base.OutputCellThree, LogicPortSpriteType.Output);
				logicCircuitSystem.AddToNetworks(base.OutputCellThree, this.outputThreeSender, true);
				logicCircuitManager.AddVisElem(this.outputThree);
				this.outputFour = new LogicPortVisualizer(base.OutputCellFour, LogicPortSpriteType.Output);
				logicCircuitSystem.AddToNetworks(base.OutputCellFour, this.outputFourSender, true);
				logicCircuitManager.AddVisElem(this.outputFour);
			}
			int inputCellOne = base.InputCellOne;
			logicCircuitSystem.AddToNetworks(inputCellOne, this.inputOne, true);
			logicCircuitManager.AddVisElem(this.inputOne);
			if (base.RequiresTwoInputs)
			{
				int inputCellTwo = base.InputCellTwo;
				logicCircuitSystem.AddToNetworks(inputCellTwo, this.inputTwo, true);
				logicCircuitManager.AddVisElem(this.inputTwo);
			}
			else if (base.RequiresFourInputs)
			{
				logicCircuitSystem.AddToNetworks(base.InputCellTwo, this.inputTwo, true);
				logicCircuitManager.AddVisElem(this.inputTwo);
				logicCircuitSystem.AddToNetworks(base.InputCellThree, this.inputThree, true);
				logicCircuitManager.AddVisElem(this.inputThree);
				logicCircuitSystem.AddToNetworks(base.InputCellFour, this.inputFour, true);
				logicCircuitManager.AddVisElem(this.inputFour);
			}
			if (base.RequiresControlInputs)
			{
				logicCircuitSystem.AddToNetworks(base.ControlCellOne, this.controlOne, true);
				logicCircuitManager.AddVisElem(this.controlOne);
				logicCircuitSystem.AddToNetworks(base.ControlCellTwo, this.controlTwo, true);
				logicCircuitManager.AddVisElem(this.controlTwo);
			}
			this.RefreshAnimation();
		}
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x000CE6E8 File Offset: 0x000CC8E8
	private void Disconnect()
	{
		if (this.connected)
		{
			LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
			UtilityNetworkManager<LogicCircuitNetwork, LogicWire> logicCircuitSystem = Game.Instance.logicCircuitSystem;
			this.connected = false;
			int outputCellOne = base.OutputCellOne;
			logicCircuitSystem.RemoveFromNetworks(outputCellOne, this, true);
			logicCircuitManager.RemoveVisElem(this.outputOne);
			this.outputOne = null;
			if (base.RequiresFourOutputs)
			{
				logicCircuitSystem.RemoveFromNetworks(base.OutputCellTwo, this.outputTwoSender, true);
				logicCircuitManager.RemoveVisElem(this.outputTwo);
				this.outputTwo = null;
				logicCircuitSystem.RemoveFromNetworks(base.OutputCellThree, this.outputThreeSender, true);
				logicCircuitManager.RemoveVisElem(this.outputThree);
				this.outputThree = null;
				logicCircuitSystem.RemoveFromNetworks(base.OutputCellFour, this.outputFourSender, true);
				logicCircuitManager.RemoveVisElem(this.outputFour);
				this.outputFour = null;
			}
			int inputCellOne = base.InputCellOne;
			logicCircuitSystem.RemoveFromNetworks(inputCellOne, this.inputOne, true);
			logicCircuitManager.RemoveVisElem(this.inputOne);
			this.inputOne = null;
			if (base.RequiresTwoInputs)
			{
				int inputCellTwo = base.InputCellTwo;
				logicCircuitSystem.RemoveFromNetworks(inputCellTwo, this.inputTwo, true);
				logicCircuitManager.RemoveVisElem(this.inputTwo);
				this.inputTwo = null;
			}
			else if (base.RequiresFourInputs)
			{
				logicCircuitSystem.RemoveFromNetworks(base.InputCellTwo, this.inputTwo, true);
				logicCircuitManager.RemoveVisElem(this.inputTwo);
				this.inputTwo = null;
				logicCircuitSystem.RemoveFromNetworks(base.InputCellThree, this.inputThree, true);
				logicCircuitManager.RemoveVisElem(this.inputThree);
				this.inputThree = null;
				logicCircuitSystem.RemoveFromNetworks(base.InputCellFour, this.inputFour, true);
				logicCircuitManager.RemoveVisElem(this.inputFour);
				this.inputFour = null;
			}
			if (base.RequiresControlInputs)
			{
				logicCircuitSystem.RemoveFromNetworks(base.ControlCellOne, this.controlOne, true);
				logicCircuitManager.RemoveVisElem(this.controlOne);
				this.controlOne = null;
				logicCircuitSystem.RemoveFromNetworks(base.ControlCellTwo, this.controlTwo, true);
				logicCircuitManager.RemoveVisElem(this.controlTwo);
				this.controlTwo = null;
			}
			this.RefreshAnimation();
		}
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x000CE8EC File Offset: 0x000CCAEC
	private void UpdateState(int new_value)
	{
		if (this.cleaningUp)
		{
			return;
		}
		int value = this.inputOne.Value;
		int num = ((this.inputTwo != null) ? this.inputTwo.Value : 0);
		int num2 = ((this.inputThree != null) ? this.inputThree.Value : 0);
		int num3 = ((this.inputFour != null) ? this.inputFour.Value : 0);
		int num4 = ((this.controlOne != null) ? this.controlOne.Value : 0);
		int num5 = ((this.controlTwo != null) ? this.controlTwo.Value : 0);
		if (base.RequiresFourInputs && base.RequiresControlInputs)
		{
			this.outputValueOne = 0;
			if (this.op == LogicGateBase.Op.Multiplexer)
			{
				if (!LogicCircuitNetwork.IsBitActive(0, num5))
				{
					if (!LogicCircuitNetwork.IsBitActive(0, num4))
					{
						this.outputValueOne = value;
					}
					else
					{
						this.outputValueOne = num;
					}
				}
				else if (!LogicCircuitNetwork.IsBitActive(0, num4))
				{
					this.outputValueOne = num2;
				}
				else
				{
					this.outputValueOne = num3;
				}
			}
		}
		if (base.RequiresFourOutputs && base.RequiresControlInputs)
		{
			this.outputValueOne = 0;
			this.outputValueTwo = 0;
			this.outputTwoSender.SetValue(0);
			this.outputValueThree = 0;
			this.outputThreeSender.SetValue(0);
			this.outputValueFour = 0;
			this.outputFourSender.SetValue(0);
			if (this.op == LogicGateBase.Op.Demultiplexer)
			{
				if (!LogicCircuitNetwork.IsBitActive(0, num4))
				{
					if (!LogicCircuitNetwork.IsBitActive(0, num5))
					{
						this.outputValueOne = value;
					}
					else
					{
						this.outputValueTwo = value;
						this.outputTwoSender.SetValue(value);
					}
				}
				else if (!LogicCircuitNetwork.IsBitActive(0, num5))
				{
					this.outputValueThree = value;
					this.outputThreeSender.SetValue(value);
				}
				else
				{
					this.outputValueFour = value;
					this.outputFourSender.SetValue(value);
				}
			}
		}
		switch (this.op)
		{
		case LogicGateBase.Op.And:
			this.outputValueOne = value & num;
			break;
		case LogicGateBase.Op.Or:
			this.outputValueOne = value | num;
			break;
		case LogicGateBase.Op.Not:
		{
			LogicWire.BitDepth bitDepth = LogicWire.BitDepth.NumRatings;
			int inputCellOne = base.InputCellOne;
			GameObject gameObject = Grid.Objects[inputCellOne, 31];
			if (gameObject != null)
			{
				LogicWire component = gameObject.GetComponent<LogicWire>();
				if (component != null)
				{
					bitDepth = component.MaxBitDepth;
				}
			}
			if (bitDepth != LogicWire.BitDepth.OneBit && bitDepth == LogicWire.BitDepth.FourBit)
			{
				uint num6 = (uint)value;
				num6 = ~num6;
				num6 &= 15U;
				this.outputValueOne = (int)num6;
			}
			else
			{
				this.outputValueOne = ((value == 0) ? 1 : 0);
			}
			break;
		}
		case LogicGateBase.Op.Xor:
			this.outputValueOne = value ^ num;
			break;
		case LogicGateBase.Op.CustomSingle:
			this.outputValueOne = this.GetCustomValue(value, num);
			break;
		}
		this.RefreshAnimation();
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x000CEB80 File Offset: 0x000CCD80
	private void OnAdditionalOutputsLogicValueChanged(HashedString port_id, int new_value)
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

	// Token: 0x0600265D RID: 9821 RVA: 0x000CEBC9 File Offset: 0x000CCDC9
	public virtual void LogicTick()
	{
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x000CEBCB File Offset: 0x000CCDCB
	protected virtual int GetCustomValue(int val1, int val2)
	{
		return val1;
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x000CEBD0 File Offset: 0x000CCDD0
	public int GetPortValue(LogicGateBase.PortId port)
	{
		switch (port)
		{
		case LogicGateBase.PortId.InputOne:
			return this.inputOne.Value;
		case LogicGateBase.PortId.InputTwo:
			if (base.RequiresTwoInputs || base.RequiresFourInputs)
			{
				return this.inputTwo.Value;
			}
			return 0;
		case LogicGateBase.PortId.InputThree:
			if (!base.RequiresFourInputs)
			{
				return 0;
			}
			return this.inputThree.Value;
		case LogicGateBase.PortId.InputFour:
			if (!base.RequiresFourInputs)
			{
				return 0;
			}
			return this.inputFour.Value;
		case LogicGateBase.PortId.OutputOne:
			return this.outputValueOne;
		case LogicGateBase.PortId.OutputTwo:
			return this.outputValueTwo;
		case LogicGateBase.PortId.OutputThree:
			return this.outputValueThree;
		case LogicGateBase.PortId.OutputFour:
			return this.outputValueFour;
		case LogicGateBase.PortId.ControlOne:
			return this.controlOne.Value;
		case LogicGateBase.PortId.ControlTwo:
			return this.controlTwo.Value;
		default:
			return this.outputValueOne;
		}
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x000CECA0 File Offset: 0x000CCEA0
	public bool GetPortConnected(LogicGateBase.PortId port)
	{
		if ((port == LogicGateBase.PortId.InputTwo && !base.RequiresTwoInputs && !base.RequiresFourInputs) || (port == LogicGateBase.PortId.InputThree && !base.RequiresFourInputs) || (port == LogicGateBase.PortId.InputFour && !base.RequiresFourInputs))
		{
			return false;
		}
		int num = base.PortCell(port);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(num) != null;
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x000CECF6 File Offset: 0x000CCEF6
	public void SetPortDescriptions(LogicGate.LogicGateDescriptions descriptions)
	{
		this.descriptions = descriptions;
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x000CED00 File Offset: 0x000CCF00
	public LogicGate.LogicGateDescriptions.Description GetPortDescription(LogicGateBase.PortId port)
	{
		switch (port)
		{
		case LogicGateBase.PortId.InputOne:
			if (this.descriptions.inputOne != null)
			{
				return this.descriptions.inputOne;
			}
			if (!base.RequiresTwoInputs && !base.RequiresFourInputs)
			{
				return LogicGate.INPUT_ONE_SINGLE_DESCRIPTION;
			}
			return LogicGate.INPUT_ONE_MULTI_DESCRIPTION;
		case LogicGateBase.PortId.InputTwo:
			if (this.descriptions.inputTwo == null)
			{
				return LogicGate.INPUT_TWO_DESCRIPTION;
			}
			return this.descriptions.inputTwo;
		case LogicGateBase.PortId.InputThree:
			if (this.descriptions.inputThree == null)
			{
				return LogicGate.INPUT_THREE_DESCRIPTION;
			}
			return this.descriptions.inputThree;
		case LogicGateBase.PortId.InputFour:
			if (this.descriptions.inputFour == null)
			{
				return LogicGate.INPUT_FOUR_DESCRIPTION;
			}
			return this.descriptions.inputFour;
		case LogicGateBase.PortId.OutputOne:
			if (this.descriptions.inputOne != null)
			{
				return this.descriptions.inputOne;
			}
			if (!base.RequiresFourOutputs)
			{
				return LogicGate.OUTPUT_ONE_SINGLE_DESCRIPTION;
			}
			return LogicGate.OUTPUT_ONE_MULTI_DESCRIPTION;
		case LogicGateBase.PortId.OutputTwo:
			if (this.descriptions.outputTwo == null)
			{
				return LogicGate.OUTPUT_TWO_DESCRIPTION;
			}
			return this.descriptions.outputTwo;
		case LogicGateBase.PortId.OutputThree:
			if (this.descriptions.outputThree == null)
			{
				return LogicGate.OUTPUT_THREE_DESCRIPTION;
			}
			return this.descriptions.outputThree;
		case LogicGateBase.PortId.OutputFour:
			if (this.descriptions.outputFour == null)
			{
				return LogicGate.OUTPUT_FOUR_DESCRIPTION;
			}
			return this.descriptions.outputFour;
		case LogicGateBase.PortId.ControlOne:
			if (this.descriptions.controlOne == null)
			{
				return LogicGate.CONTROL_ONE_DESCRIPTION;
			}
			return this.descriptions.controlOne;
		case LogicGateBase.PortId.ControlTwo:
			if (this.descriptions.controlTwo == null)
			{
				return LogicGate.CONTROL_TWO_DESCRIPTION;
			}
			return this.descriptions.controlTwo;
		default:
			return this.descriptions.outputOne;
		}
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x000CEEA5 File Offset: 0x000CD0A5
	public int GetLogicValue()
	{
		return this.outputValueOne;
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x000CEEAD File Offset: 0x000CD0AD
	public int GetLogicCell()
	{
		return this.GetLogicUICell();
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x000CEEB5 File Offset: 0x000CD0B5
	public int GetLogicUICell()
	{
		return base.OutputCellOne;
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x000CEEBD File Offset: 0x000CD0BD
	public bool IsLogicInput()
	{
		return false;
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x000CEEC0 File Offset: 0x000CD0C0
	private LogicEventHandler GetInputFromControlValue(int val)
	{
		switch (val)
		{
		case 1:
			return this.inputTwo;
		case 2:
			return this.inputThree;
		case 3:
			return this.inputFour;
		}
		return this.inputOne;
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x000CEEF5 File Offset: 0x000CD0F5
	private void ShowSymbolConditionally(bool showAnything, bool active, KBatchedAnimController kbac, KAnimHashedString ifTrue, KAnimHashedString ifFalse)
	{
		if (!showAnything)
		{
			kbac.SetSymbolVisiblity(ifTrue, false);
			kbac.SetSymbolVisiblity(ifFalse, false);
			return;
		}
		kbac.SetSymbolVisiblity(ifTrue, active);
		kbac.SetSymbolVisiblity(ifFalse, !active);
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x000CEF22 File Offset: 0x000CD122
	private void TintSymbolConditionally(bool tintAnything, bool condition, KBatchedAnimController kbac, KAnimHashedString symbol, Color ifTrue, Color ifFalse)
	{
		if (tintAnything)
		{
			kbac.SetSymbolTint(symbol, condition ? ifTrue : ifFalse);
			return;
		}
		kbac.SetSymbolTint(symbol, Color.white);
	}

	// Token: 0x0600266A RID: 9834 RVA: 0x000CEF46 File Offset: 0x000CD146
	private void SetBloomSymbolShowing(bool showing, KBatchedAnimController kbac, KAnimHashedString symbol, KAnimHashedString bloomSymbol)
	{
		kbac.SetSymbolVisiblity(bloomSymbol, showing);
		kbac.SetSymbolVisiblity(symbol, !showing);
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x000CEF5C File Offset: 0x000CD15C
	protected void RefreshAnimation()
	{
		if (this.cleaningUp)
		{
			return;
		}
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (this.op == LogicGateBase.Op.Multiplexer)
		{
			int num = LogicCircuitNetwork.GetBitValue(0, this.controlOne.Value) + LogicCircuitNetwork.GetBitValue(0, this.controlTwo.Value) * 2;
			if (this.lastAnimState != num)
			{
				if (this.lastAnimState == -1)
				{
					component.Play(num.ToString(), KAnim.PlayMode.Once, 1f, 0f);
				}
				else
				{
					component.Play(this.lastAnimState.ToString() + "_" + num.ToString(), KAnim.PlayMode.Once, 1f, 0f);
				}
			}
			this.lastAnimState = num;
			LogicEventHandler inputFromControlValue = this.GetInputFromControlValue(num);
			KAnimHashedString[] array = LogicGate.multiplexerSymbolPaths[num];
			LogicCircuitNetwork logicCircuitNetwork = Game.Instance.logicCircuitSystem.GetNetworkForCell(inputFromControlValue.GetLogicCell()) as LogicCircuitNetwork;
			UtilityNetwork networkForCell = Game.Instance.logicCircuitSystem.GetNetworkForCell(base.InputCellOne);
			UtilityNetwork networkForCell2 = Game.Instance.logicCircuitSystem.GetNetworkForCell(base.InputCellTwo);
			UtilityNetwork networkForCell3 = Game.Instance.logicCircuitSystem.GetNetworkForCell(base.InputCellThree);
			UtilityNetwork networkForCell4 = Game.Instance.logicCircuitSystem.GetNetworkForCell(base.InputCellFour);
			this.ShowSymbolConditionally(networkForCell != null, this.inputOne.Value == 0, component, LogicGate.INPUT1_SYMBOL_BLM_RED, LogicGate.INPUT1_SYMBOL_BLM_GRN);
			this.ShowSymbolConditionally(networkForCell2 != null, this.inputTwo.Value == 0, component, LogicGate.INPUT2_SYMBOL_BLM_RED, LogicGate.INPUT2_SYMBOL_BLM_GRN);
			this.ShowSymbolConditionally(networkForCell3 != null, this.inputThree.Value == 0, component, LogicGate.INPUT3_SYMBOL_BLM_RED, LogicGate.INPUT3_SYMBOL_BLM_GRN);
			this.ShowSymbolConditionally(networkForCell4 != null, this.inputFour.Value == 0, component, LogicGate.INPUT4_SYMBOL_BLM_RED, LogicGate.INPUT4_SYMBOL_BLM_GRN);
			this.ShowSymbolConditionally(logicCircuitNetwork != null, inputFromControlValue.Value == 0, component, LogicGate.OUTPUT1_SYMBOL_BLM_RED, LogicGate.OUTPUT1_SYMBOL_BLM_GRN);
			this.TintSymbolConditionally(networkForCell != null, this.inputOne.Value == 0, component, LogicGate.INPUT1_SYMBOL, this.inactiveTintColor, this.activeTintColor);
			this.TintSymbolConditionally(networkForCell2 != null, this.inputTwo.Value == 0, component, LogicGate.INPUT2_SYMBOL, this.inactiveTintColor, this.activeTintColor);
			this.TintSymbolConditionally(networkForCell3 != null, this.inputThree.Value == 0, component, LogicGate.INPUT3_SYMBOL, this.inactiveTintColor, this.activeTintColor);
			this.TintSymbolConditionally(networkForCell4 != null, this.inputFour.Value == 0, component, LogicGate.INPUT4_SYMBOL, this.inactiveTintColor, this.activeTintColor);
			this.TintSymbolConditionally(Game.Instance.logicCircuitSystem.GetNetworkForCell(base.OutputCellOne) != null && logicCircuitNetwork != null, inputFromControlValue.Value == 0, component, LogicGate.OUTPUT1_SYMBOL, this.inactiveTintColor, this.activeTintColor);
			for (int i = 0; i < LogicGate.multiplexerSymbols.Length; i++)
			{
				KAnimHashedString kanimHashedString = LogicGate.multiplexerSymbols[i];
				KAnimHashedString kanimHashedString2 = LogicGate.multiplexerBloomSymbols[i];
				bool flag = Array.IndexOf<KAnimHashedString>(array, kanimHashedString2) != -1 && logicCircuitNetwork != null;
				this.SetBloomSymbolShowing(flag, component, kanimHashedString, kanimHashedString2);
				if (flag)
				{
					component.SetSymbolTint(kanimHashedString2, (inputFromControlValue.Value == 0) ? this.inactiveTintColor : this.activeTintColor);
				}
			}
			return;
		}
		if (this.op == LogicGateBase.Op.Demultiplexer)
		{
			int num2 = LogicCircuitNetwork.GetBitValue(0, this.controlOne.Value) * 2 + LogicCircuitNetwork.GetBitValue(0, this.controlTwo.Value);
			if (this.lastAnimState != num2)
			{
				if (this.lastAnimState == -1)
				{
					component.Play(num2.ToString(), KAnim.PlayMode.Once, 1f, 0f);
				}
				else
				{
					component.Play(this.lastAnimState.ToString() + "_" + num2.ToString(), KAnim.PlayMode.Once, 1f, 0f);
				}
			}
			this.lastAnimState = num2;
			KAnimHashedString[] array2 = LogicGate.demultiplexerSymbolPaths[num2];
			LogicCircuitNetwork logicCircuitNetwork2 = Game.Instance.logicCircuitSystem.GetNetworkForCell(this.inputOne.GetLogicCell()) as LogicCircuitNetwork;
			for (int j = 0; j < LogicGate.demultiplexerSymbols.Length; j++)
			{
				KAnimHashedString kanimHashedString3 = LogicGate.demultiplexerSymbols[j];
				KAnimHashedString kanimHashedString4 = LogicGate.demultiplexerBloomSymbols[j];
				bool flag2 = Array.IndexOf<KAnimHashedString>(array2, kanimHashedString4) != -1 && logicCircuitNetwork2 != null;
				this.SetBloomSymbolShowing(flag2, component, kanimHashedString3, kanimHashedString4);
				if (flag2)
				{
					component.SetSymbolTint(kanimHashedString4, (this.inputOne.Value == 0) ? this.inactiveTintColor : this.activeTintColor);
				}
			}
			this.ShowSymbolConditionally(logicCircuitNetwork2 != null, this.inputOne.Value == 0, component, LogicGate.INPUT1_SYMBOL_BLM_RED, LogicGate.INPUT1_SYMBOL_BLM_GRN);
			if (logicCircuitNetwork2 != null)
			{
				component.SetSymbolTint(LogicGate.INPUT1_SYMBOL_BLOOM, (this.inputOne.Value == 0) ? this.inactiveTintColor : this.activeTintColor);
			}
			int[] array3 = new int[] { base.OutputCellOne, base.OutputCellTwo, base.OutputCellThree, base.OutputCellFour };
			for (int k = 0; k < LogicGate.demultiplexerOutputSymbols.Length; k++)
			{
				KAnimHashedString kanimHashedString5 = LogicGate.demultiplexerOutputSymbols[k];
				bool flag3 = Array.IndexOf<KAnimHashedString>(array2, kanimHashedString5) == -1 || this.inputOne.Value == 0;
				UtilityNetwork networkForCell5 = Game.Instance.logicCircuitSystem.GetNetworkForCell(array3[k]);
				this.TintSymbolConditionally(logicCircuitNetwork2 != null && networkForCell5 != null, flag3, component, kanimHashedString5, this.inactiveTintColor, this.activeTintColor);
				this.ShowSymbolConditionally(logicCircuitNetwork2 != null && networkForCell5 != null, flag3, component, LogicGate.demultiplexerOutputRedSymbols[k], LogicGate.demultiplexerOutputGreenSymbols[k]);
			}
			return;
		}
		if (this.op == LogicGateBase.Op.And || this.op == LogicGateBase.Op.Xor || this.op == LogicGateBase.Op.Not || this.op == LogicGateBase.Op.Or)
		{
			int outputCellOne = base.OutputCellOne;
			if (!(Game.Instance.logicCircuitSystem.GetNetworkForCell(outputCellOne) is LogicCircuitNetwork))
			{
				component.Play("off", KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			if (base.RequiresTwoInputs)
			{
				int num3 = this.inputOne.Value * 2 + this.inputTwo.Value;
				if (this.lastAnimState != num3)
				{
					if (this.lastAnimState == -1)
					{
						component.Play(num3.ToString(), KAnim.PlayMode.Once, 1f, 0f);
					}
					else
					{
						component.Play(this.lastAnimState.ToString() + "_" + num3.ToString(), KAnim.PlayMode.Once, 1f, 0f);
					}
					this.lastAnimState = num3;
					return;
				}
			}
			else
			{
				int value = this.inputOne.Value;
				if (this.lastAnimState != value)
				{
					if (this.lastAnimState == -1)
					{
						component.Play(value.ToString(), KAnim.PlayMode.Once, 1f, 0f);
					}
					else
					{
						component.Play(this.lastAnimState.ToString() + "_" + value.ToString(), KAnim.PlayMode.Once, 1f, 0f);
					}
					this.lastAnimState = value;
					return;
				}
			}
		}
		else
		{
			int outputCellOne2 = base.OutputCellOne;
			if (!(Game.Instance.logicCircuitSystem.GetNetworkForCell(outputCellOne2) is LogicCircuitNetwork))
			{
				component.Play("off", KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			if (base.RequiresTwoInputs)
			{
				component.Play("on_" + (this.inputOne.Value + this.inputTwo.Value * 2 + this.outputValueOne * 4).ToString(), KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			component.Play("on_" + (this.inputOne.Value + this.outputValueOne * 4).ToString(), KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x000CF761 File Offset: 0x000CD961
	public void OnLogicNetworkConnectionChanged(bool connected)
	{
	}

	// Token: 0x0400165E RID: 5726
	private static readonly LogicGate.LogicGateDescriptions.Description INPUT_ONE_SINGLE_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_SINGLE_INPUT_ONE_NAME,
		active = UI.LOGIC_PORTS.GATE_SINGLE_INPUT_ONE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_SINGLE_INPUT_ONE_INACTIVE
	};

	// Token: 0x0400165F RID: 5727
	private static readonly LogicGate.LogicGateDescriptions.Description INPUT_ONE_MULTI_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_INPUT_ONE_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_INPUT_ONE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_INPUT_ONE_INACTIVE
	};

	// Token: 0x04001660 RID: 5728
	private static readonly LogicGate.LogicGateDescriptions.Description INPUT_TWO_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_INPUT_TWO_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_INPUT_TWO_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_INPUT_TWO_INACTIVE
	};

	// Token: 0x04001661 RID: 5729
	private static readonly LogicGate.LogicGateDescriptions.Description INPUT_THREE_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_INPUT_THREE_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_INPUT_THREE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_INPUT_THREE_INACTIVE
	};

	// Token: 0x04001662 RID: 5730
	private static readonly LogicGate.LogicGateDescriptions.Description INPUT_FOUR_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_INPUT_FOUR_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_INPUT_FOUR_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_INPUT_FOUR_INACTIVE
	};

	// Token: 0x04001663 RID: 5731
	private static readonly LogicGate.LogicGateDescriptions.Description OUTPUT_ONE_SINGLE_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_NAME,
		active = UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_INACTIVE
	};

	// Token: 0x04001664 RID: 5732
	private static readonly LogicGate.LogicGateDescriptions.Description OUTPUT_ONE_MULTI_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_ONE_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_ONE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_ONE_INACTIVE
	};

	// Token: 0x04001665 RID: 5733
	private static readonly LogicGate.LogicGateDescriptions.Description OUTPUT_TWO_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_TWO_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_TWO_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_TWO_INACTIVE
	};

	// Token: 0x04001666 RID: 5734
	private static readonly LogicGate.LogicGateDescriptions.Description OUTPUT_THREE_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_THREE_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_THREE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_THREE_INACTIVE
	};

	// Token: 0x04001667 RID: 5735
	private static readonly LogicGate.LogicGateDescriptions.Description OUTPUT_FOUR_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_FOUR_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_FOUR_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTI_OUTPUT_FOUR_INACTIVE
	};

	// Token: 0x04001668 RID: 5736
	private static readonly LogicGate.LogicGateDescriptions.Description CONTROL_ONE_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTIPLEXER_CONTROL_ONE_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTIPLEXER_CONTROL_ONE_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTIPLEXER_CONTROL_ONE_INACTIVE
	};

	// Token: 0x04001669 RID: 5737
	private static readonly LogicGate.LogicGateDescriptions.Description CONTROL_TWO_DESCRIPTION = new LogicGate.LogicGateDescriptions.Description
	{
		name = UI.LOGIC_PORTS.GATE_MULTIPLEXER_CONTROL_TWO_NAME,
		active = UI.LOGIC_PORTS.GATE_MULTIPLEXER_CONTROL_TWO_ACTIVE,
		inactive = UI.LOGIC_PORTS.GATE_MULTIPLEXER_CONTROL_TWO_INACTIVE
	};

	// Token: 0x0400166A RID: 5738
	private LogicGate.LogicGateDescriptions descriptions;

	// Token: 0x0400166B RID: 5739
	private LogicEventSender[] additionalOutputs;

	// Token: 0x0400166C RID: 5740
	private const bool IS_CIRCUIT_ENDPOINT = true;

	// Token: 0x0400166D RID: 5741
	private bool connected;

	// Token: 0x0400166E RID: 5742
	protected bool cleaningUp;

	// Token: 0x0400166F RID: 5743
	private int lastAnimState = -1;

	// Token: 0x04001670 RID: 5744
	[Serialize]
	protected int outputValueOne;

	// Token: 0x04001671 RID: 5745
	[Serialize]
	protected int outputValueTwo;

	// Token: 0x04001672 RID: 5746
	[Serialize]
	protected int outputValueThree;

	// Token: 0x04001673 RID: 5747
	[Serialize]
	protected int outputValueFour;

	// Token: 0x04001674 RID: 5748
	private LogicEventHandler inputOne;

	// Token: 0x04001675 RID: 5749
	private LogicEventHandler inputTwo;

	// Token: 0x04001676 RID: 5750
	private LogicEventHandler inputThree;

	// Token: 0x04001677 RID: 5751
	private LogicEventHandler inputFour;

	// Token: 0x04001678 RID: 5752
	private LogicPortVisualizer outputOne;

	// Token: 0x04001679 RID: 5753
	private LogicPortVisualizer outputTwo;

	// Token: 0x0400167A RID: 5754
	private LogicPortVisualizer outputThree;

	// Token: 0x0400167B RID: 5755
	private LogicPortVisualizer outputFour;

	// Token: 0x0400167C RID: 5756
	private LogicEventSender outputTwoSender;

	// Token: 0x0400167D RID: 5757
	private LogicEventSender outputThreeSender;

	// Token: 0x0400167E RID: 5758
	private LogicEventSender outputFourSender;

	// Token: 0x0400167F RID: 5759
	private LogicEventHandler controlOne;

	// Token: 0x04001680 RID: 5760
	private LogicEventHandler controlTwo;

	// Token: 0x04001681 RID: 5761
	private static readonly EventSystem.IntraObjectHandler<LogicGate> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<LogicGate>(delegate(LogicGate component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x04001682 RID: 5762
	private static readonly EventSystem.IntraObjectHandler<LogicGate> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<LogicGate>(delegate(LogicGate component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});

	// Token: 0x04001683 RID: 5763
	private static KAnimHashedString INPUT1_SYMBOL = "input1";

	// Token: 0x04001684 RID: 5764
	private static KAnimHashedString INPUT2_SYMBOL = "input2";

	// Token: 0x04001685 RID: 5765
	private static KAnimHashedString INPUT3_SYMBOL = "input3";

	// Token: 0x04001686 RID: 5766
	private static KAnimHashedString INPUT4_SYMBOL = "input4";

	// Token: 0x04001687 RID: 5767
	private static KAnimHashedString OUTPUT1_SYMBOL = "output1";

	// Token: 0x04001688 RID: 5768
	private static KAnimHashedString OUTPUT2_SYMBOL = "output2";

	// Token: 0x04001689 RID: 5769
	private static KAnimHashedString OUTPUT3_SYMBOL = "output3";

	// Token: 0x0400168A RID: 5770
	private static KAnimHashedString OUTPUT4_SYMBOL = "output4";

	// Token: 0x0400168B RID: 5771
	private static KAnimHashedString INPUT1_SYMBOL_BLM_RED = "input1_red_bloom";

	// Token: 0x0400168C RID: 5772
	private static KAnimHashedString INPUT1_SYMBOL_BLM_GRN = "input1_green_bloom";

	// Token: 0x0400168D RID: 5773
	private static KAnimHashedString INPUT2_SYMBOL_BLM_RED = "input2_red_bloom";

	// Token: 0x0400168E RID: 5774
	private static KAnimHashedString INPUT2_SYMBOL_BLM_GRN = "input2_green_bloom";

	// Token: 0x0400168F RID: 5775
	private static KAnimHashedString INPUT3_SYMBOL_BLM_RED = "input3_red_bloom";

	// Token: 0x04001690 RID: 5776
	private static KAnimHashedString INPUT3_SYMBOL_BLM_GRN = "input3_green_bloom";

	// Token: 0x04001691 RID: 5777
	private static KAnimHashedString INPUT4_SYMBOL_BLM_RED = "input4_red_bloom";

	// Token: 0x04001692 RID: 5778
	private static KAnimHashedString INPUT4_SYMBOL_BLM_GRN = "input4_green_bloom";

	// Token: 0x04001693 RID: 5779
	private static KAnimHashedString OUTPUT1_SYMBOL_BLM_RED = "output1_red_bloom";

	// Token: 0x04001694 RID: 5780
	private static KAnimHashedString OUTPUT1_SYMBOL_BLM_GRN = "output1_green_bloom";

	// Token: 0x04001695 RID: 5781
	private static KAnimHashedString OUTPUT2_SYMBOL_BLM_RED = "output2_red_bloom";

	// Token: 0x04001696 RID: 5782
	private static KAnimHashedString OUTPUT2_SYMBOL_BLM_GRN = "output2_green_bloom";

	// Token: 0x04001697 RID: 5783
	private static KAnimHashedString OUTPUT3_SYMBOL_BLM_RED = "output3_red_bloom";

	// Token: 0x04001698 RID: 5784
	private static KAnimHashedString OUTPUT3_SYMBOL_BLM_GRN = "output3_green_bloom";

	// Token: 0x04001699 RID: 5785
	private static KAnimHashedString OUTPUT4_SYMBOL_BLM_RED = "output4_red_bloom";

	// Token: 0x0400169A RID: 5786
	private static KAnimHashedString OUTPUT4_SYMBOL_BLM_GRN = "output4_green_bloom";

	// Token: 0x0400169B RID: 5787
	private static KAnimHashedString LINE_LEFT_1_SYMBOL = "line_left_1";

	// Token: 0x0400169C RID: 5788
	private static KAnimHashedString LINE_LEFT_2_SYMBOL = "line_left_2";

	// Token: 0x0400169D RID: 5789
	private static KAnimHashedString LINE_LEFT_3_SYMBOL = "line_left_3";

	// Token: 0x0400169E RID: 5790
	private static KAnimHashedString LINE_LEFT_4_SYMBOL = "line_left_4";

	// Token: 0x0400169F RID: 5791
	private static KAnimHashedString LINE_RIGHT_1_SYMBOL = "line_right_1";

	// Token: 0x040016A0 RID: 5792
	private static KAnimHashedString LINE_RIGHT_2_SYMBOL = "line_right_2";

	// Token: 0x040016A1 RID: 5793
	private static KAnimHashedString LINE_RIGHT_3_SYMBOL = "line_right_3";

	// Token: 0x040016A2 RID: 5794
	private static KAnimHashedString LINE_RIGHT_4_SYMBOL = "line_right_4";

	// Token: 0x040016A3 RID: 5795
	private static KAnimHashedString FLIPPER_1_SYMBOL = "flipper1";

	// Token: 0x040016A4 RID: 5796
	private static KAnimHashedString FLIPPER_2_SYMBOL = "flipper2";

	// Token: 0x040016A5 RID: 5797
	private static KAnimHashedString FLIPPER_3_SYMBOL = "flipper3";

	// Token: 0x040016A6 RID: 5798
	private static KAnimHashedString INPUT_SYMBOL = "input";

	// Token: 0x040016A7 RID: 5799
	private static KAnimHashedString OUTPUT_SYMBOL = "output";

	// Token: 0x040016A8 RID: 5800
	private static KAnimHashedString INPUT1_SYMBOL_BLOOM = "input1_bloom";

	// Token: 0x040016A9 RID: 5801
	private static KAnimHashedString INPUT2_SYMBOL_BLOOM = "input2_bloom";

	// Token: 0x040016AA RID: 5802
	private static KAnimHashedString INPUT3_SYMBOL_BLOOM = "input3_bloom";

	// Token: 0x040016AB RID: 5803
	private static KAnimHashedString INPUT4_SYMBOL_BLOOM = "input4_bloom";

	// Token: 0x040016AC RID: 5804
	private static KAnimHashedString OUTPUT1_SYMBOL_BLOOM = "output1_bloom";

	// Token: 0x040016AD RID: 5805
	private static KAnimHashedString OUTPUT2_SYMBOL_BLOOM = "output2_bloom";

	// Token: 0x040016AE RID: 5806
	private static KAnimHashedString OUTPUT3_SYMBOL_BLOOM = "output3_bloom";

	// Token: 0x040016AF RID: 5807
	private static KAnimHashedString OUTPUT4_SYMBOL_BLOOM = "output4_bloom";

	// Token: 0x040016B0 RID: 5808
	private static KAnimHashedString LINE_LEFT_1_SYMBOL_BLOOM = "line_left_1_bloom";

	// Token: 0x040016B1 RID: 5809
	private static KAnimHashedString LINE_LEFT_2_SYMBOL_BLOOM = "line_left_2_bloom";

	// Token: 0x040016B2 RID: 5810
	private static KAnimHashedString LINE_LEFT_3_SYMBOL_BLOOM = "line_left_3_bloom";

	// Token: 0x040016B3 RID: 5811
	private static KAnimHashedString LINE_LEFT_4_SYMBOL_BLOOM = "line_left_4_bloom";

	// Token: 0x040016B4 RID: 5812
	private static KAnimHashedString LINE_RIGHT_1_SYMBOL_BLOOM = "line_right_1_bloom";

	// Token: 0x040016B5 RID: 5813
	private static KAnimHashedString LINE_RIGHT_2_SYMBOL_BLOOM = "line_right_2_bloom";

	// Token: 0x040016B6 RID: 5814
	private static KAnimHashedString LINE_RIGHT_3_SYMBOL_BLOOM = "line_right_3_bloom";

	// Token: 0x040016B7 RID: 5815
	private static KAnimHashedString LINE_RIGHT_4_SYMBOL_BLOOM = "line_right_4_bloom";

	// Token: 0x040016B8 RID: 5816
	private static KAnimHashedString FLIPPER_1_SYMBOL_BLOOM = "flipper1_bloom";

	// Token: 0x040016B9 RID: 5817
	private static KAnimHashedString FLIPPER_2_SYMBOL_BLOOM = "flipper2_bloom";

	// Token: 0x040016BA RID: 5818
	private static KAnimHashedString FLIPPER_3_SYMBOL_BLOOM = "flipper3_bloom";

	// Token: 0x040016BB RID: 5819
	private static KAnimHashedString INPUT_SYMBOL_BLOOM = "input_bloom";

	// Token: 0x040016BC RID: 5820
	private static KAnimHashedString OUTPUT_SYMBOL_BLOOM = "output_bloom";

	// Token: 0x040016BD RID: 5821
	private static KAnimHashedString[][] multiplexerSymbolPaths = new KAnimHashedString[][]
	{
		new KAnimHashedString[]
		{
			LogicGate.LINE_LEFT_1_SYMBOL_BLOOM,
			LogicGate.FLIPPER_1_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_1_SYMBOL_BLOOM,
			LogicGate.FLIPPER_3_SYMBOL_BLOOM,
			LogicGate.OUTPUT_SYMBOL_BLOOM
		},
		new KAnimHashedString[]
		{
			LogicGate.LINE_LEFT_2_SYMBOL_BLOOM,
			LogicGate.FLIPPER_1_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_1_SYMBOL_BLOOM,
			LogicGate.FLIPPER_3_SYMBOL_BLOOM,
			LogicGate.OUTPUT_SYMBOL_BLOOM
		},
		new KAnimHashedString[]
		{
			LogicGate.LINE_LEFT_3_SYMBOL_BLOOM,
			LogicGate.FLIPPER_2_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_2_SYMBOL_BLOOM,
			LogicGate.FLIPPER_3_SYMBOL_BLOOM,
			LogicGate.OUTPUT_SYMBOL_BLOOM
		},
		new KAnimHashedString[]
		{
			LogicGate.LINE_LEFT_4_SYMBOL_BLOOM,
			LogicGate.FLIPPER_2_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_2_SYMBOL_BLOOM,
			LogicGate.FLIPPER_3_SYMBOL_BLOOM,
			LogicGate.OUTPUT_SYMBOL_BLOOM
		}
	};

	// Token: 0x040016BE RID: 5822
	private static KAnimHashedString[] multiplexerSymbols = new KAnimHashedString[]
	{
		LogicGate.LINE_LEFT_1_SYMBOL,
		LogicGate.LINE_LEFT_2_SYMBOL,
		LogicGate.LINE_LEFT_3_SYMBOL,
		LogicGate.LINE_LEFT_4_SYMBOL,
		LogicGate.LINE_RIGHT_1_SYMBOL,
		LogicGate.LINE_RIGHT_2_SYMBOL,
		LogicGate.FLIPPER_1_SYMBOL,
		LogicGate.FLIPPER_2_SYMBOL,
		LogicGate.FLIPPER_3_SYMBOL,
		LogicGate.OUTPUT_SYMBOL
	};

	// Token: 0x040016BF RID: 5823
	private static KAnimHashedString[] multiplexerBloomSymbols = new KAnimHashedString[]
	{
		LogicGate.LINE_LEFT_1_SYMBOL_BLOOM,
		LogicGate.LINE_LEFT_2_SYMBOL_BLOOM,
		LogicGate.LINE_LEFT_3_SYMBOL_BLOOM,
		LogicGate.LINE_LEFT_4_SYMBOL_BLOOM,
		LogicGate.LINE_RIGHT_1_SYMBOL_BLOOM,
		LogicGate.LINE_RIGHT_2_SYMBOL_BLOOM,
		LogicGate.FLIPPER_1_SYMBOL_BLOOM,
		LogicGate.FLIPPER_2_SYMBOL_BLOOM,
		LogicGate.FLIPPER_3_SYMBOL_BLOOM,
		LogicGate.OUTPUT_SYMBOL_BLOOM
	};

	// Token: 0x040016C0 RID: 5824
	private static KAnimHashedString[][] demultiplexerSymbolPaths = new KAnimHashedString[][]
	{
		new KAnimHashedString[]
		{
			LogicGate.INPUT_SYMBOL_BLOOM,
			LogicGate.LINE_LEFT_1_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_1_SYMBOL_BLOOM,
			LogicGate.OUTPUT1_SYMBOL
		},
		new KAnimHashedString[]
		{
			LogicGate.INPUT_SYMBOL_BLOOM,
			LogicGate.LINE_LEFT_1_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_2_SYMBOL_BLOOM,
			LogicGate.OUTPUT2_SYMBOL
		},
		new KAnimHashedString[]
		{
			LogicGate.INPUT_SYMBOL_BLOOM,
			LogicGate.LINE_LEFT_2_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_3_SYMBOL_BLOOM,
			LogicGate.OUTPUT3_SYMBOL
		},
		new KAnimHashedString[]
		{
			LogicGate.INPUT_SYMBOL_BLOOM,
			LogicGate.LINE_LEFT_2_SYMBOL_BLOOM,
			LogicGate.LINE_RIGHT_4_SYMBOL_BLOOM,
			LogicGate.OUTPUT4_SYMBOL
		}
	};

	// Token: 0x040016C1 RID: 5825
	private static KAnimHashedString[] demultiplexerSymbols = new KAnimHashedString[]
	{
		LogicGate.INPUT_SYMBOL,
		LogicGate.LINE_LEFT_1_SYMBOL,
		LogicGate.LINE_LEFT_2_SYMBOL,
		LogicGate.LINE_RIGHT_1_SYMBOL,
		LogicGate.LINE_RIGHT_2_SYMBOL,
		LogicGate.LINE_RIGHT_3_SYMBOL,
		LogicGate.LINE_RIGHT_4_SYMBOL
	};

	// Token: 0x040016C2 RID: 5826
	private static KAnimHashedString[] demultiplexerBloomSymbols = new KAnimHashedString[]
	{
		LogicGate.INPUT_SYMBOL_BLOOM,
		LogicGate.LINE_LEFT_1_SYMBOL_BLOOM,
		LogicGate.LINE_LEFT_2_SYMBOL_BLOOM,
		LogicGate.LINE_RIGHT_1_SYMBOL_BLOOM,
		LogicGate.LINE_RIGHT_2_SYMBOL_BLOOM,
		LogicGate.LINE_RIGHT_3_SYMBOL_BLOOM,
		LogicGate.LINE_RIGHT_4_SYMBOL_BLOOM
	};

	// Token: 0x040016C3 RID: 5827
	private static KAnimHashedString[] demultiplexerOutputSymbols = new KAnimHashedString[]
	{
		LogicGate.OUTPUT1_SYMBOL,
		LogicGate.OUTPUT2_SYMBOL,
		LogicGate.OUTPUT3_SYMBOL,
		LogicGate.OUTPUT4_SYMBOL
	};

	// Token: 0x040016C4 RID: 5828
	private static KAnimHashedString[] demultiplexerOutputRedSymbols = new KAnimHashedString[]
	{
		LogicGate.OUTPUT1_SYMBOL_BLM_RED,
		LogicGate.OUTPUT2_SYMBOL_BLM_RED,
		LogicGate.OUTPUT3_SYMBOL_BLM_RED,
		LogicGate.OUTPUT4_SYMBOL_BLM_RED
	};

	// Token: 0x040016C5 RID: 5829
	private static KAnimHashedString[] demultiplexerOutputGreenSymbols = new KAnimHashedString[]
	{
		LogicGate.OUTPUT1_SYMBOL_BLM_GRN,
		LogicGate.OUTPUT2_SYMBOL_BLM_GRN,
		LogicGate.OUTPUT3_SYMBOL_BLM_GRN,
		LogicGate.OUTPUT4_SYMBOL_BLM_GRN
	};

	// Token: 0x040016C6 RID: 5830
	private Color activeTintColor = new Color(0.5411765f, 0.9882353f, 0.29803923f);

	// Token: 0x040016C7 RID: 5831
	private Color inactiveTintColor = Color.red;

	// Token: 0x02001249 RID: 4681
	public class LogicGateDescriptions
	{
		// Token: 0x04005D81 RID: 23937
		public LogicGate.LogicGateDescriptions.Description inputOne;

		// Token: 0x04005D82 RID: 23938
		public LogicGate.LogicGateDescriptions.Description inputTwo;

		// Token: 0x04005D83 RID: 23939
		public LogicGate.LogicGateDescriptions.Description inputThree;

		// Token: 0x04005D84 RID: 23940
		public LogicGate.LogicGateDescriptions.Description inputFour;

		// Token: 0x04005D85 RID: 23941
		public LogicGate.LogicGateDescriptions.Description outputOne;

		// Token: 0x04005D86 RID: 23942
		public LogicGate.LogicGateDescriptions.Description outputTwo;

		// Token: 0x04005D87 RID: 23943
		public LogicGate.LogicGateDescriptions.Description outputThree;

		// Token: 0x04005D88 RID: 23944
		public LogicGate.LogicGateDescriptions.Description outputFour;

		// Token: 0x04005D89 RID: 23945
		public LogicGate.LogicGateDescriptions.Description controlOne;

		// Token: 0x04005D8A RID: 23946
		public LogicGate.LogicGateDescriptions.Description controlTwo;

		// Token: 0x02001FC3 RID: 8131
		public class Description
		{
			// Token: 0x04008D90 RID: 36240
			public string name;

			// Token: 0x04008D91 RID: 36241
			public string active;

			// Token: 0x04008D92 RID: 36242
			public string inactive;
		}
	}
}
