using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class ResourceLoader<T> where T : Resource, new()
{
	// Token: 0x0600085B RID: 2139 RVA: 0x000219BF File Offset: 0x0001FBBF
	public IEnumerator<T> GetEnumerator()
	{
		return this.resources.GetEnumerator();
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x000219D1 File Offset: 0x0001FBD1
	public ResourceLoader()
	{
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000219E4 File Offset: 0x0001FBE4
	public ResourceLoader(TextAsset file)
	{
		this.Load(file);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x000219FE File Offset: 0x0001FBFE
	public ResourceLoader(string text, string name)
	{
		this.Load(text, name);
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00021A1C File Offset: 0x0001FC1C
	public void Load(string text, string name)
	{
		string[,] array = CSVReader.SplitCsvGrid(text, name);
		int length = array.GetLength(1);
		for (int i = 1; i < length; i++)
		{
			if (!array[0, i].IsNullOrWhiteSpace())
			{
				T t = new T();
				CSVUtil.ParseData<T>(t, array, i);
				if (!t.Disabled)
				{
					this.resources.Add(t);
				}
			}
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00021A80 File Offset: 0x0001FC80
	public virtual void Load(TextAsset file)
	{
		if (file == null)
		{
			global::Debug.LogWarning("Missing resource file of type: " + typeof(T).Name);
			return;
		}
		this.Load(file.text, file.name);
	}

	// Token: 0x0400063D RID: 1597
	public List<T> resources = new List<T>();
}
