using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F6 RID: 1526
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonReader")]
public class LogicRibbonReader : KMonoBehaviour, ILogicRibbonBitSelector, IRender200ms
{
	// Token: 0x06002740 RID: 10048 RVA: 0x000D2A34 File Offset: 0x000D0C34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicRibbonReader>(-801688580, LogicRibbonReader.OnLogicValueChangedDelegate);
		this.ports = base.GetComponent<LogicPorts>();
		this.kbac = base.GetComponent<KBatchedAnimController>();
		this.kbac.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x000D2A90 File Offset: 0x000D0C90
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicRibbonReader>(-905833192, LogicRibbonReader.OnCopySettingsDelegate);
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x000D2AAC File Offset: 0x000D0CAC
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != LogicRibbonReader.INPUT_PORT_ID)
		{
			return;
		}
		this.currentValue = logicValueChanged.newValue;
		this.UpdateLogicCircuit();
		this.UpdateVisuals();
	}

	// Token: 0x06002743 RID: 10051 RVA: 0x000D2AEC File Offset: 0x000D0CEC
	private void OnCopySettings(object data)
	{
		LogicRibbonReader component = ((GameObject)data).GetComponent<LogicRibbonReader>();
		if (component != null)
		{
			this.SetBitSelection(component.selectedBit);
		}
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x000D2B1C File Offset: 0x000D0D1C
	private void UpdateLogicCircuit()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		LogicWire.BitDepth bitDepth = LogicWire.BitDepth.NumRatings;
		int portCell = component.GetPortCell(LogicRibbonReader.OUTPUT_PORT_ID);
		GameObject gameObject = Grid.Objects[portCell, 31];
		if (gameObject != null)
		{
			LogicWire component2 = gameObject.GetComponent<LogicWire>();
			if (component2 != null)
			{
				bitDepth = component2.MaxBitDepth;
			}
		}
		if (bitDepth != LogicWire.BitDepth.OneBit && bitDepth == LogicWire.BitDepth.FourBit)
		{
			int num = this.currentValue >> this.selectedBit;
			component.SendSignal(LogicRibbonReader.OUTPUT_PORT_ID, num);
		}
		else
		{
			int num = this.currentValue & (1 << this.selectedBit);
			component.SendSignal(LogicRibbonReader.OUTPUT_PORT_ID, (num > 0) ? 1 : 0);
		}
		this.UpdateVisuals();
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x000D2BC5 File Offset: 0x000D0DC5
	public void Render200ms(float dt)
	{
		this.UpdateVisuals();
	}

	// Token: 0x06002746 RID: 10054 RVA: 0x000D2BCD File Offset: 0x000D0DCD
	public void SetBitSelection(int bit)
	{
		this.selectedBit = bit;
		this.UpdateLogicCircuit();
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x000D2BDC File Offset: 0x000D0DDC
	public int GetBitSelection()
	{
		return this.selectedBit;
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x000D2BE4 File Offset: 0x000D0DE4
	public int GetBitDepth()
	{
		return this.bitDepth;
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06002749 RID: 10057 RVA: 0x000D2BEC File Offset: 0x000D0DEC
	public string SideScreenTitle
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_READER_TITLE";
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x0600274A RID: 10058 RVA: 0x000D2BF3 File Offset: 0x000D0DF3
	public string SideScreenDescription
	{
		get
		{
			return UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_READER_DESCRIPTION;
		}
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x000D2BFF File Offset: 0x000D0DFF
	public bool SideScreenDisplayWriterDescription()
	{
		return false;
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x000D2C02 File Offset: 0x000D0E02
	public bool SideScreenDisplayReaderDescription()
	{
		return true;
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x000D2C08 File Offset: 0x000D0E08
	public bool IsBitActive(int bit)
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonReader.INPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork != null && logicCircuitNetwork.IsBitActive(bit);
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x000D2C54 File Offset: 0x000D0E54
	public int GetInputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetInputValue(LogicRibbonReader.INPUT_PORT_ID);
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x000D2C80 File Offset: 0x000D0E80
	public int GetOutputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetOutputValue(LogicRibbonReader.OUTPUT_PORT_ID);
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x000D2CAC File Offset: 0x000D0EAC
	private LogicCircuitNetwork GetInputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonReader.INPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x000D2CEC File Offset: 0x000D0EEC
	private LogicCircuitNetwork GetOutputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonReader.OUTPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x000D2D2C File Offset: 0x000D0F2C
	public void UpdateVisuals()
	{
		bool inputNetwork = this.GetInputNetwork() != null;
		LogicCircuitNetwork outputNetwork = this.GetOutputNetwork();
		this.GetInputValue();
		int num = 0;
		if (inputNetwork)
		{
			num += 4;
			this.kbac.SetSymbolTint(this.BIT_ONE_SYMBOL, this.IsBitActive(0) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(this.BIT_TWO_SYMBOL, this.IsBitActive(1) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(this.BIT_THREE_SYMBOL, this.IsBitActive(2) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(this.BIT_FOUR_SYMBOL, this.IsBitActive(3) ? this.colorOn : this.colorOff);
		}
		if (outputNetwork != null)
		{
			num++;
			this.kbac.SetSymbolTint(this.OUTPUT_SYMBOL, LogicCircuitNetwork.IsBitActive(0, this.GetOutputValue()) ? this.colorOn : this.colorOff);
		}
		this.kbac.Play(num.ToString() + "_" + (this.GetBitSelection() + 1).ToString(), KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001727 RID: 5927
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicRibbonReaderInput");

	// Token: 0x04001728 RID: 5928
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LogicRibbonReaderOutput");

	// Token: 0x04001729 RID: 5929
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400172A RID: 5930
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonReader> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicRibbonReader>(delegate(LogicRibbonReader component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x0400172B RID: 5931
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonReader> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicRibbonReader>(delegate(LogicRibbonReader component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400172C RID: 5932
	private KAnimHashedString BIT_ONE_SYMBOL = "bit1_bloom";

	// Token: 0x0400172D RID: 5933
	private KAnimHashedString BIT_TWO_SYMBOL = "bit2_bloom";

	// Token: 0x0400172E RID: 5934
	private KAnimHashedString BIT_THREE_SYMBOL = "bit3_bloom";

	// Token: 0x0400172F RID: 5935
	private KAnimHashedString BIT_FOUR_SYMBOL = "bit4_bloom";

	// Token: 0x04001730 RID: 5936
	private KAnimHashedString OUTPUT_SYMBOL = "output_light_bloom";

	// Token: 0x04001731 RID: 5937
	private KBatchedAnimController kbac;

	// Token: 0x04001732 RID: 5938
	private Color colorOn = new Color(0.34117648f, 0.7254902f, 0.36862746f);

	// Token: 0x04001733 RID: 5939
	private Color colorOff = new Color(0.9529412f, 0.2901961f, 0.2784314f);

	// Token: 0x04001734 RID: 5940
	private LogicPorts ports;

	// Token: 0x04001735 RID: 5941
	public int bitDepth = 4;

	// Token: 0x04001736 RID: 5942
	[Serialize]
	public int selectedBit;

	// Token: 0x04001737 RID: 5943
	[Serialize]
	private int currentValue;
}
