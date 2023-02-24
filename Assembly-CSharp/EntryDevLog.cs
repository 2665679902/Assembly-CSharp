using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x02000A96 RID: 2710
public class EntryDevLog
{
	// Token: 0x06005320 RID: 21280 RVA: 0x001E24B0 File Offset: 0x001E06B0
	[Conditional("UNITY_EDITOR")]
	public void AddModificationRecord(EntryDevLog.ModificationRecord.ActionType actionType, string target, object newValue)
	{
		string text = this.TrimAuthor();
		this.modificationRecords.Add(new EntryDevLog.ModificationRecord(actionType, target, newValue, text));
	}

	// Token: 0x06005321 RID: 21281 RVA: 0x001E24D8 File Offset: 0x001E06D8
	[Conditional("UNITY_EDITOR")]
	public void InsertModificationRecord(int index, EntryDevLog.ModificationRecord.ActionType actionType, string target, object newValue)
	{
		string text = this.TrimAuthor();
		this.modificationRecords.Insert(index, new EntryDevLog.ModificationRecord(actionType, target, newValue, text));
	}

	// Token: 0x06005322 RID: 21282 RVA: 0x001E2504 File Offset: 0x001E0704
	private string TrimAuthor()
	{
		string text = "";
		string[] array = new string[] { "Invoke", "CreateInstance", "AwakeInternal", "Internal", "<>", "YamlDotNet", "Deserialize" };
		string[] array2 = new string[]
		{
			".ctor", "Trigger", "AddContentContainerRange", "AddContentContainer", "InsertContentContainer", "KInstantiateUI", "Start", "InitializeComponentAwake", "TrimAuthor", "InsertModificationRecord",
			"AddModificationRecord", "SetValue", "Write"
		};
		StackTrace stackTrace = new StackTrace();
		int i = 0;
		int num = 0;
		int num2 = 3;
		while (i < num2)
		{
			num++;
			if (stackTrace.FrameCount <= num)
			{
				break;
			}
			MethodBase method = stackTrace.GetFrame(num).GetMethod();
			bool flag = false;
			for (int j = 0; j < array.Length; j++)
			{
				flag = flag || method.Name.Contains(array[j]);
			}
			for (int k = 0; k < array2.Length; k++)
			{
				flag = flag || method.Name.Contains(array2[k]);
			}
			if (!flag && !stackTrace.GetFrame(num).GetMethod().Name.StartsWith("set_") && !stackTrace.GetFrame(num).GetMethod().Name.StartsWith("Instantiate"))
			{
				if (i != 0)
				{
					text += " < ";
				}
				i++;
				text += stackTrace.GetFrame(num).GetMethod().Name;
			}
		}
		return text;
	}

	// Token: 0x0400384A RID: 14410
	[SerializeField]
	public List<EntryDevLog.ModificationRecord> modificationRecords = new List<EntryDevLog.ModificationRecord>();

	// Token: 0x0200191B RID: 6427
	public class ModificationRecord
	{
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06008F3E RID: 36670 RVA: 0x0030FF63 File Offset: 0x0030E163
		// (set) Token: 0x06008F3F RID: 36671 RVA: 0x0030FF6B File Offset: 0x0030E16B
		public EntryDevLog.ModificationRecord.ActionType actionType { get; private set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06008F40 RID: 36672 RVA: 0x0030FF74 File Offset: 0x0030E174
		// (set) Token: 0x06008F41 RID: 36673 RVA: 0x0030FF7C File Offset: 0x0030E17C
		public string target { get; private set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06008F42 RID: 36674 RVA: 0x0030FF85 File Offset: 0x0030E185
		// (set) Token: 0x06008F43 RID: 36675 RVA: 0x0030FF8D File Offset: 0x0030E18D
		public object newValue { get; private set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06008F44 RID: 36676 RVA: 0x0030FF96 File Offset: 0x0030E196
		// (set) Token: 0x06008F45 RID: 36677 RVA: 0x0030FF9E File Offset: 0x0030E19E
		public string author { get; private set; }

		// Token: 0x06008F46 RID: 36678 RVA: 0x0030FFA7 File Offset: 0x0030E1A7
		public ModificationRecord(EntryDevLog.ModificationRecord.ActionType actionType, string target, object newValue, string author)
		{
			this.target = target;
			this.newValue = newValue;
			this.author = author;
			this.actionType = actionType;
		}

		// Token: 0x020020FF RID: 8447
		public enum ActionType
		{
			// Token: 0x040092D8 RID: 37592
			Created,
			// Token: 0x040092D9 RID: 37593
			ChangeSubEntry,
			// Token: 0x040092DA RID: 37594
			ChangeContent,
			// Token: 0x040092DB RID: 37595
			ValueChange,
			// Token: 0x040092DC RID: 37596
			YAMLData
		}
	}
}
