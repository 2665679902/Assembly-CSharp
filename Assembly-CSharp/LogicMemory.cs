using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F1 RID: 1521
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicMemory")]
public class LogicMemory : KMonoBehaviour
{
	// Token: 0x060026F4 RID: 9972 RVA: 0x000D1DCC File Offset: 0x000CFFCC
	protected override void OnSpawn()
	{
		if (LogicMemory.infoStatusItem == null)
		{
			LogicMemory.infoStatusItem = new StatusItem("StoredValue", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			LogicMemory.infoStatusItem.resolveStringCallback = new Func<string, object, string>(LogicMemory.ResolveInfoStatusItemString);
		}
		base.Subscribe<LogicMemory>(-801688580, LogicMemory.OnLogicValueChangedDelegate);
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x000D1E30 File Offset: 0x000D0030
	public void OnLogicValueChanged(object data)
	{
		if (this.ports == null || base.gameObject == null || this == null)
		{
			return;
		}
		if (((LogicValueChanged)data).portID != LogicMemory.READ_PORT_ID)
		{
			int inputValue = this.ports.GetInputValue(LogicMemory.SET_PORT_ID);
			int inputValue2 = this.ports.GetInputValue(LogicMemory.RESET_PORT_ID);
			int num = this.value;
			if (LogicCircuitNetwork.IsBitActive(0, inputValue2))
			{
				num = 0;
			}
			else if (LogicCircuitNetwork.IsBitActive(0, inputValue))
			{
				num = 1;
			}
			if (num != this.value)
			{
				this.value = num;
				this.ports.SendSignal(LogicMemory.READ_PORT_ID, this.value);
				KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
				if (component != null)
				{
					component.Play(LogicCircuitNetwork.IsBitActive(0, this.value) ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
				}
			}
		}
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x000D1F24 File Offset: 0x000D0124
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		int outputValue = ((LogicMemory)data).ports.GetOutputValue(LogicMemory.READ_PORT_ID);
		return string.Format(BUILDINGS.PREFABS.LOGICMEMORY.STATUS_ITEM_VALUE, outputValue);
	}

	// Token: 0x04001703 RID: 5891
	[MyCmpGet]
	private LogicPorts ports;

	// Token: 0x04001704 RID: 5892
	[Serialize]
	private int value;

	// Token: 0x04001705 RID: 5893
	private static StatusItem infoStatusItem;

	// Token: 0x04001706 RID: 5894
	public static readonly HashedString READ_PORT_ID = new HashedString("LogicMemoryRead");

	// Token: 0x04001707 RID: 5895
	public static readonly HashedString SET_PORT_ID = new HashedString("LogicMemorySet");

	// Token: 0x04001708 RID: 5896
	public static readonly HashedString RESET_PORT_ID = new HashedString("LogicMemoryReset");

	// Token: 0x04001709 RID: 5897
	private static readonly EventSystem.IntraObjectHandler<LogicMemory> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicMemory>(delegate(LogicMemory component, object data)
	{
		component.OnLogicValueChanged(data);
	});
}
