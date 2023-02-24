using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
public abstract class AsyncCsvLoader<LoaderType, CsvEntryType> : GlobalAsyncLoader<LoaderType> where LoaderType : class where CsvEntryType : Resource, new()
{
	// Token: 0x060004FA RID: 1274 RVA: 0x00018880 File Offset: 0x00016A80
	public AsyncCsvLoader(TextAsset asset)
	{
		this.text = asset.text;
		this.name = asset.name;
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x000188A0 File Offset: 0x00016AA0
	public override void Run()
	{
		this.entries = new ResourceLoader<CsvEntryType>(this.text, this.name).resources.ToArray();
		this.text = null;
		this.name = null;
	}

	// Token: 0x04000517 RID: 1303
	private string text;

	// Token: 0x04000518 RID: 1304
	private string name;

	// Token: 0x04000519 RID: 1305
	public CsvEntryType[] entries;
}
