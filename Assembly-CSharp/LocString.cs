using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020007F7 RID: 2039
[Serializable]
public class LocString
{
	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00146A38 File Offset: 0x00144C38
	public string text
	{
		get
		{
			return this._text;
		}
	}

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x00146A40 File Offset: 0x00144C40
	public StringKey key
	{
		get
		{
			return this._key;
		}
	}

	// Token: 0x06003AD8 RID: 15064 RVA: 0x00146A48 File Offset: 0x00144C48
	public LocString(string text)
	{
		this._text = text;
		this._key = default(StringKey);
	}

	// Token: 0x06003AD9 RID: 15065 RVA: 0x00146A63 File Offset: 0x00144C63
	public LocString(string text, string keystring)
	{
		this._text = text;
		this._key = new StringKey(keystring);
	}

	// Token: 0x06003ADA RID: 15066 RVA: 0x00146A7E File Offset: 0x00144C7E
	public LocString(string text, bool isLocalized)
	{
		this._text = text;
		this._key = default(StringKey);
	}

	// Token: 0x06003ADB RID: 15067 RVA: 0x00146A99 File Offset: 0x00144C99
	public static implicit operator LocString(string text)
	{
		return new LocString(text);
	}

	// Token: 0x06003ADC RID: 15068 RVA: 0x00146AA1 File Offset: 0x00144CA1
	public static implicit operator string(LocString loc_string)
	{
		return loc_string.text;
	}

	// Token: 0x06003ADD RID: 15069 RVA: 0x00146AA9 File Offset: 0x00144CA9
	public override string ToString()
	{
		return Strings.Get(this.key).String;
	}

	// Token: 0x06003ADE RID: 15070 RVA: 0x00146ABB File Offset: 0x00144CBB
	public void SetKey(string key_name)
	{
		this._key = new StringKey(key_name);
	}

	// Token: 0x06003ADF RID: 15071 RVA: 0x00146AC9 File Offset: 0x00144CC9
	public void SetKey(StringKey key)
	{
		this._key = key;
	}

	// Token: 0x06003AE0 RID: 15072 RVA: 0x00146AD2 File Offset: 0x00144CD2
	public string Replace(string search, string replacement)
	{
		return this.ToString().Replace(search, replacement);
	}

	// Token: 0x06003AE1 RID: 15073 RVA: 0x00146AE4 File Offset: 0x00144CE4
	public static void CreateLocStringKeys(Type type, string parent_path = "STRINGS.")
	{
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		string text = parent_path;
		if (text == null)
		{
			text = "";
		}
		text = text + type.Name + ".";
		foreach (FieldInfo fieldInfo in fields)
		{
			if (!(fieldInfo.FieldType != typeof(LocString)))
			{
				string text2 = text + fieldInfo.Name;
				LocString locString = (LocString)fieldInfo.GetValue(null);
				locString.SetKey(text2);
				string text3 = locString.text;
				Strings.Add(new string[] { text2, text3 });
				fieldInfo.SetValue(null, locString);
			}
		}
		Type[] nestedTypes = type.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		for (int i = 0; i < nestedTypes.Length; i++)
		{
			LocString.CreateLocStringKeys(nestedTypes[i], text);
		}
	}

	// Token: 0x06003AE2 RID: 15074 RVA: 0x00146BB0 File Offset: 0x00144DB0
	public static string[] GetStrings(Type type)
	{
		List<string> list = new List<string>();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		for (int i = 0; i < fields.Length; i++)
		{
			LocString locString = (LocString)fields[i].GetValue(null);
			list.Add(locString.text);
		}
		return list.ToArray();
	}

	// Token: 0x0400268E RID: 9870
	[SerializeField]
	private string _text;

	// Token: 0x0400268F RID: 9871
	[SerializeField]
	private StringKey _key;

	// Token: 0x04002690 RID: 9872
	public const BindingFlags data_member_fields = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
}
