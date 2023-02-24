using System;
using System.Collections.Generic;

// Token: 0x02000105 RID: 261
public class StringTable
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x0002337C File Offset: 0x0002157C
	public StringEntry Get(StringKey key0)
	{
		int hash = key0.Hash;
		StringEntry stringEntry = null;
		this.Entries.TryGetValue(hash, out stringEntry);
		return stringEntry;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x000233A4 File Offset: 0x000215A4
	public void Add(int idx, string[] value)
	{
		string text = value[idx];
		int hashCode = text.GetHashCode();
		this.KeyNames[hashCode] = text;
		if (idx == value.Length - 2)
		{
			StringEntry stringEntry = new StringEntry(value[idx + 1]);
			this.Entries[hashCode] = stringEntry;
			return;
		}
		StringTable stringTable = null;
		if (!this.SubTables.TryGetValue(hashCode, out stringTable))
		{
			stringTable = new StringTable();
			this.SubTables[hashCode] = stringTable;
		}
		stringTable.Add(idx + 1, value);
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0002341C File Offset: 0x0002161C
	public void Print(string parent_path)
	{
		foreach (KeyValuePair<int, StringEntry> keyValuePair in this.Entries)
		{
			Debug.Log(string.Concat(new string[]
			{
				parent_path,
				".",
				this.KeyNames[keyValuePair.Key],
				".",
				keyValuePair.Value.String
			}));
		}
		string text = parent_path;
		if (text != "")
		{
			text += ".";
		}
		foreach (KeyValuePair<int, StringTable> keyValuePair2 in this.SubTables)
		{
			keyValuePair2.Value.Print(text + this.KeyNames[keyValuePair2.Key]);
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0002352C File Offset: 0x0002172C
	public void VisitEntries(StringTable.EntryVisitor visit)
	{
		foreach (KeyValuePair<int, StringEntry> keyValuePair in this.Entries)
		{
			visit(this.KeyNames[keyValuePair.Key], keyValuePair.Value.String);
		}
	}

	// Token: 0x0400066D RID: 1645
	private Dictionary<int, string> KeyNames = new Dictionary<int, string>();

	// Token: 0x0400066E RID: 1646
	private Dictionary<int, StringTable> SubTables = new Dictionary<int, StringTable>();

	// Token: 0x0400066F RID: 1647
	private Dictionary<int, StringEntry> Entries = new Dictionary<int, StringEntry>();

	// Token: 0x02000A04 RID: 2564
	// (Invoke) Token: 0x06005425 RID: 21541
	public delegate void EntryVisitor(string id, string value);
}
