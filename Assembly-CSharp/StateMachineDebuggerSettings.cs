using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class StateMachineDebuggerSettings : ScriptableObject
{
	// Token: 0x06001557 RID: 5463 RVA: 0x0006EC6D File Offset: 0x0006CE6D
	public IEnumerator<StateMachineDebuggerSettings.Entry> GetEnumerator()
	{
		return this.entries.GetEnumerator();
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x0006EC7F File Offset: 0x0006CE7F
	public static StateMachineDebuggerSettings Get()
	{
		if (StateMachineDebuggerSettings._Instance == null)
		{
			StateMachineDebuggerSettings._Instance = Resources.Load<StateMachineDebuggerSettings>("StateMachineDebuggerSettings");
			StateMachineDebuggerSettings._Instance.Initialize();
		}
		return StateMachineDebuggerSettings._Instance;
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x0006ECAC File Offset: 0x0006CEAC
	private void Initialize()
	{
		foreach (Type type in App.GetCurrentDomainTypes())
		{
			if (typeof(StateMachine).IsAssignableFrom(type))
			{
				this.CreateEntry(type);
			}
		}
		this.entries.RemoveAll((StateMachineDebuggerSettings.Entry x) => x.type == null);
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x0006ED3C File Offset: 0x0006CF3C
	public StateMachineDebuggerSettings.Entry CreateEntry(Type type)
	{
		foreach (StateMachineDebuggerSettings.Entry entry in this.entries)
		{
			if (type.FullName == entry.typeName)
			{
				entry.type = type;
				return entry;
			}
		}
		StateMachineDebuggerSettings.Entry entry2 = new StateMachineDebuggerSettings.Entry(type);
		this.entries.Add(entry2);
		return entry2;
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x0006EDBC File Offset: 0x0006CFBC
	public void Clear()
	{
		this.entries.Clear();
		this.Initialize();
	}

	// Token: 0x04000BE6 RID: 3046
	public List<StateMachineDebuggerSettings.Entry> entries = new List<StateMachineDebuggerSettings.Entry>();

	// Token: 0x04000BE7 RID: 3047
	private static StateMachineDebuggerSettings _Instance;

	// Token: 0x02001032 RID: 4146
	[Serializable]
	public class Entry
	{
		// Token: 0x0600727B RID: 29307 RVA: 0x002AD918 File Offset: 0x002ABB18
		public Entry(Type type)
		{
			this.typeName = type.FullName;
			this.type = type;
		}

		// Token: 0x0400569B RID: 22171
		public Type type;

		// Token: 0x0400569C RID: 22172
		public string typeName;

		// Token: 0x0400569D RID: 22173
		public bool breakOnGoTo;

		// Token: 0x0400569E RID: 22174
		public bool enableConsoleLogging;

		// Token: 0x0400569F RID: 22175
		public bool saveHistory;
	}
}
