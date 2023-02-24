using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F7 RID: 1527
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonWriter")]
public class LogicRibbonWriter : KMonoBehaviour, ILogicRibbonBitSelector, IRender200ms
{
	// Token: 0x06002755 RID: 10069 RVA: 0x000D2F67 File Offset: 0x000D1167
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicRibbonWriter>(-905833192, LogicRibbonWriter.OnCopySettingsDelegate);
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x000D2F80 File Offset: 0x000D1180
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicRibbonWriter>(-801688580, LogicRibbonWriter.OnLogicValueChangedDelegate);
		this.ports = base.GetComponent<LogicPorts>();
		this.kbac = base.GetComponent<KBatchedAnimController>();
		this.kbac.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x000D2FDC File Offset: 0x000D11DC
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != LogicRibbonWriter.INPUT_PORT_ID)
		{
			return;
		}
		this.currentValue = logicValueChanged.newValue;
		this.UpdateLogicCircuit();
		this.UpdateVisuals();
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x000D301C File Offset: 0x000D121C
	private void OnCopySettings(object data)
	{
		LogicRibbonWriter component = ((GameObject)data).GetComponent<LogicRibbonWriter>();
		if (component != null)
		{
			this.SetBitSelection(component.selectedBit);
		}
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x000D304C File Offset: 0x000D124C
	private void UpdateLogicCircuit()
	{
		int num = this.currentValue << this.selectedBit;
		base.GetComponent<LogicPorts>().SendSignal(LogicRibbonWriter.OUTPUT_PORT_ID, num);
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x000D307B File Offset: 0x000D127B
	public void Render200ms(float dt)
	{
		this.UpdateVisuals();
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x000D3084 File Offset: 0x000D1284
	private LogicCircuitNetwork GetInputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonWriter.INPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x000D30C4 File Offset: 0x000D12C4
	private LogicCircuitNetwork GetOutputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonWriter.OUTPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x000D3104 File Offset: 0x000D1304
	public void SetBitSelection(int bit)
	{
		this.selectedBit = bit;
		this.UpdateLogicCircuit();
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x000D3113 File Offset: 0x000D1313
	public int GetBitSelection()
	{
		return this.selectedBit;
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x000D311B File Offset: 0x000D131B
	public int GetBitDepth()
	{
		return this.bitDepth;
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06002760 RID: 10080 RVA: 0x000D3123 File Offset: 0x000D1323
	public string SideScreenTitle
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_WRITER_TITLE";
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06002761 RID: 10081 RVA: 0x000D312A File Offset: 0x000D132A
	public string SideScreenDescription
	{
		get
		{
			return UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_WRITER_DESCRIPTION;
		}
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x000D3136 File Offset: 0x000D1336
	public bool SideScreenDisplayWriterDescription()
	{
		return true;
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x000D3139 File Offset: 0x000D1339
	public bool SideScreenDisplayReaderDescription()
	{
		return false;
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x000D313C File Offset: 0x000D133C
	public bool IsBitActive(int bit)
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonWriter.OUTPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork != null && logicCircuitNetwork.IsBitActive(bit);
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x000D3188 File Offset: 0x000D1388
	public int GetInputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetInputValue(LogicRibbonWriter.INPUT_PORT_ID);
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x000D31B4 File Offset: 0x000D13B4
	public int GetOutputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetOutputValue(LogicRibbonWriter.OUTPUT_PORT_ID);
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000D31E0 File Offset: 0x000D13E0
	public void UpdateVisuals()
	{
		bool inputNetwork = this.GetInputNetwork() != null;
		LogicCircuitNetwork outputNetwork = this.GetOutputNetwork();
		int num = 0;
		if (inputNetwork)
		{
			num++;
			this.kbac.SetSymbolTint(LogicRibbonWriter.INPUT_SYMBOL, LogicCircuitNetwork.IsBitActive(0, this.GetInputValue()) ? this.colorOn : this.colorOff);
		}
		if (outputNetwork != null)
		{
			num += 4;
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_ONE_SYMBOL, this.IsBitActive(0) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_TWO_SYMBOL, this.IsBitActive(1) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_THREE_SYMBOL, this.IsBitActive(2) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_FOUR_SYMBOL, this.IsBitActive(3) ? this.colorOn : this.colorOff);
		}
		this.kbac.Play(num.ToString() + "_" + (this.GetBitSelection() + 1).ToString(), KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001738 RID: 5944
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicRibbonWriterInput");

	// Token: 0x04001739 RID: 5945
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LogicRibbonWriterOutput");

	// Token: 0x0400173A RID: 5946
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400173B RID: 5947
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonWriter> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicRibbonWriter>(delegate(LogicRibbonWriter component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x0400173C RID: 5948
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonWriter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicRibbonWriter>(delegate(LogicRibbonWriter component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400173D RID: 5949
	private LogicPorts ports;

	// Token: 0x0400173E RID: 5950
	public int bitDepth = 4;

	// Token: 0x0400173F RID: 5951
	[Serialize]
	public int selectedBit;

	// Token: 0x04001740 RID: 5952
	[Serialize]
	private int currentValue;

	// Token: 0x04001741 RID: 5953
	private KBatchedAnimController kbac;

	// Token: 0x04001742 RID: 5954
	private Color colorOn = new Color(0.34117648f, 0.7254902f, 0.36862746f);

	// Token: 0x04001743 RID: 5955
	private Color colorOff = new Color(0.9529412f, 0.2901961f, 0.2784314f);

	// Token: 0x04001744 RID: 5956
	private static KAnimHashedString BIT_ONE_SYMBOL = "bit1_bloom";

	// Token: 0x04001745 RID: 5957
	private static KAnimHashedString BIT_TWO_SYMBOL = "bit2_bloom";

	// Token: 0x04001746 RID: 5958
	private static KAnimHashedString BIT_THREE_SYMBOL = "bit3_bloom";

	// Token: 0x04001747 RID: 5959
	private static KAnimHashedString BIT_FOUR_SYMBOL = "bit4_bloom";

	// Token: 0x04001748 RID: 5960
	private static KAnimHashedString INPUT_SYMBOL = "input_light_bloom";
}
