using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000623 RID: 1571
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/PixelPack")]
public class PixelPack : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06002920 RID: 10528 RVA: 0x000D9475 File Offset: 0x000D7675
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<PixelPack>(-905833192, PixelPack.OnCopySettingsDelegate);
	}

	// Token: 0x06002921 RID: 10529 RVA: 0x000D9490 File Offset: 0x000D7690
	private void OnCopySettings(object data)
	{
		PixelPack component = ((GameObject)data).GetComponent<PixelPack>();
		if (component != null)
		{
			for (int i = 0; i < component.colorSettings.Count; i++)
			{
				this.colorSettings[i] = component.colorSettings[i];
			}
		}
		this.UpdateColors();
	}

	// Token: 0x06002922 RID: 10530 RVA: 0x000D94E8 File Offset: 0x000D76E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		base.Subscribe<PixelPack>(-801688580, PixelPack.OnLogicValueChangedDelegate);
		base.Subscribe<PixelPack>(-592767678, PixelPack.OnOperationalChangedDelegate);
		if (this.colorSettings == null)
		{
			PixelPack.ColorPair colorPair = new PixelPack.ColorPair
			{
				activeColor = this.defaultActive,
				standbyColor = this.defaultStandby
			};
			PixelPack.ColorPair colorPair2 = new PixelPack.ColorPair
			{
				activeColor = this.defaultActive,
				standbyColor = this.defaultStandby
			};
			PixelPack.ColorPair colorPair3 = new PixelPack.ColorPair
			{
				activeColor = this.defaultActive,
				standbyColor = this.defaultStandby
			};
			PixelPack.ColorPair colorPair4 = new PixelPack.ColorPair
			{
				activeColor = this.defaultActive,
				standbyColor = this.defaultStandby
			};
			this.colorSettings = new List<PixelPack.ColorPair>();
			this.colorSettings.Add(colorPair);
			this.colorSettings.Add(colorPair2);
			this.colorSettings.Add(colorPair3);
			this.colorSettings.Add(colorPair4);
		}
	}

	// Token: 0x06002923 RID: 10531 RVA: 0x000D9604 File Offset: 0x000D7804
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == PixelPack.PORT_ID)
		{
			this.logicValue = logicValueChanged.newValue;
			this.UpdateColors();
		}
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x000D963C File Offset: 0x000D783C
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			this.UpdateColors();
			this.animController.Play(PixelPack.ON_ANIMS, KAnim.PlayMode.Once);
		}
		else
		{
			this.animController.Play(PixelPack.OFF_ANIMS, KAnim.PlayMode.Once);
		}
		this.operational.SetActive(this.operational.IsOperational, false);
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x000D9698 File Offset: 0x000D7898
	public void UpdateColors()
	{
		if (this.operational.IsOperational)
		{
			LogicPorts component = base.GetComponent<LogicPorts>();
			if (component != null)
			{
				LogicWire.BitDepth connectedWireBitDepth = component.GetConnectedWireBitDepth(PixelPack.PORT_ID);
				if (connectedWireBitDepth == LogicWire.BitDepth.FourBit)
				{
					this.animController.SetSymbolTint(PixelPack.SYMBOL_ONE_NAME, LogicCircuitNetwork.IsBitActive(0, this.logicValue) ? this.colorSettings[0].activeColor : this.colorSettings[0].standbyColor);
					this.animController.SetSymbolTint(PixelPack.SYMBOL_TWO_NAME, LogicCircuitNetwork.IsBitActive(1, this.logicValue) ? this.colorSettings[1].activeColor : this.colorSettings[1].standbyColor);
					this.animController.SetSymbolTint(PixelPack.SYMBOL_THREE_NAME, LogicCircuitNetwork.IsBitActive(2, this.logicValue) ? this.colorSettings[2].activeColor : this.colorSettings[2].standbyColor);
					this.animController.SetSymbolTint(PixelPack.SYMBOL_FOUR_NAME, LogicCircuitNetwork.IsBitActive(3, this.logicValue) ? this.colorSettings[3].activeColor : this.colorSettings[3].standbyColor);
					return;
				}
				if (connectedWireBitDepth == LogicWire.BitDepth.OneBit)
				{
					this.animController.SetSymbolTint(PixelPack.SYMBOL_ONE_NAME, LogicCircuitNetwork.IsBitActive(0, this.logicValue) ? this.colorSettings[0].activeColor : this.colorSettings[0].standbyColor);
					this.animController.SetSymbolTint(PixelPack.SYMBOL_TWO_NAME, LogicCircuitNetwork.IsBitActive(0, this.logicValue) ? this.colorSettings[1].activeColor : this.colorSettings[1].standbyColor);
					this.animController.SetSymbolTint(PixelPack.SYMBOL_THREE_NAME, LogicCircuitNetwork.IsBitActive(0, this.logicValue) ? this.colorSettings[2].activeColor : this.colorSettings[2].standbyColor);
					this.animController.SetSymbolTint(PixelPack.SYMBOL_FOUR_NAME, LogicCircuitNetwork.IsBitActive(0, this.logicValue) ? this.colorSettings[3].activeColor : this.colorSettings[3].standbyColor);
				}
			}
		}
	}

	// Token: 0x04001830 RID: 6192
	protected KBatchedAnimController animController;

	// Token: 0x04001831 RID: 6193
	private static readonly EventSystem.IntraObjectHandler<PixelPack> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<PixelPack>(delegate(PixelPack component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001832 RID: 6194
	private static readonly EventSystem.IntraObjectHandler<PixelPack> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<PixelPack>(delegate(PixelPack component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001833 RID: 6195
	public static readonly HashedString PORT_ID = new HashedString("PixelPackInput");

	// Token: 0x04001834 RID: 6196
	public static readonly HashedString SYMBOL_ONE_NAME = "screen1";

	// Token: 0x04001835 RID: 6197
	public static readonly HashedString SYMBOL_TWO_NAME = "screen2";

	// Token: 0x04001836 RID: 6198
	public static readonly HashedString SYMBOL_THREE_NAME = "screen3";

	// Token: 0x04001837 RID: 6199
	public static readonly HashedString SYMBOL_FOUR_NAME = "screen4";

	// Token: 0x04001838 RID: 6200
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001839 RID: 6201
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400183A RID: 6202
	private static readonly EventSystem.IntraObjectHandler<PixelPack> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<PixelPack>(delegate(PixelPack component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400183B RID: 6203
	public int logicValue;

	// Token: 0x0400183C RID: 6204
	[Serialize]
	public List<PixelPack.ColorPair> colorSettings;

	// Token: 0x0400183D RID: 6205
	private Color defaultActive = new Color(0.34509805f, 0.84705883f, 0.32941177f);

	// Token: 0x0400183E RID: 6206
	private Color defaultStandby = new Color(0.972549f, 0.47058824f, 0.34509805f);

	// Token: 0x0400183F RID: 6207
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on" };

	// Token: 0x04001840 RID: 6208
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "off_pre", "off" };

	// Token: 0x020012A0 RID: 4768
	public struct ColorPair
	{
		// Token: 0x04005E40 RID: 24128
		public Color activeColor;

		// Token: 0x04005E41 RID: 24129
		public Color standbyColor;
	}
}
