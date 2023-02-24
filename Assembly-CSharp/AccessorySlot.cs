using System;
using System.Collections.Generic;

// Token: 0x02000509 RID: 1289
public class AccessorySlot : Resource
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000A5212 File Offset: 0x000A3412
	// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x000A521A File Offset: 0x000A341A
	public KAnimHashedString targetSymbolId { get; private set; }

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000A5223 File Offset: 0x000A3423
	// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x000A522B File Offset: 0x000A342B
	public List<Accessory> accessories { get; private set; }

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000A5234 File Offset: 0x000A3434
	public KAnimFile AnimFile
	{
		get
		{
			return this.file;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000A523C File Offset: 0x000A343C
	// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x000A5244 File Offset: 0x000A3444
	public KAnimFile defaultAnimFile { get; private set; }

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000A524D File Offset: 0x000A344D
	// (set) Token: 0x06001EDB RID: 7899 RVA: 0x000A5255 File Offset: 0x000A3455
	public int overrideLayer { get; private set; }

	// Token: 0x06001EDC RID: 7900 RVA: 0x000A5260 File Offset: 0x000A3460
	public AccessorySlot(string id, ResourceSet parent, KAnimFile swap_build, int overrideLayer = 0)
		: base(id, parent, null)
	{
		if (swap_build == null)
		{
			Debug.LogErrorFormat("AccessorySlot {0} missing swap_build", new object[] { id });
		}
		this.targetSymbolId = new KAnimHashedString("snapTo_" + id.ToLower());
		this.accessories = new List<Accessory>();
		this.file = swap_build;
		this.overrideLayer = overrideLayer;
		this.defaultAnimFile = swap_build;
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x000A52D0 File Offset: 0x000A34D0
	public AccessorySlot(string id, ResourceSet parent, KAnimHashedString target_symbol_id, KAnimFile swap_build, KAnimFile defaultAnimFile = null, int overrideLayer = 0)
		: base(id, parent, null)
	{
		if (swap_build == null)
		{
			Debug.LogErrorFormat("AccessorySlot {0} missing swap_build", new object[] { id });
		}
		this.targetSymbolId = target_symbol_id;
		this.accessories = new List<Accessory>();
		this.file = swap_build;
		this.defaultAnimFile = ((defaultAnimFile != null) ? defaultAnimFile : swap_build);
		this.overrideLayer = overrideLayer;
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x000A533C File Offset: 0x000A353C
	public void AddAccessories(KAnimFile default_build, ResourceSet parent)
	{
		KAnim.Build build = default_build.GetData().build;
		default_build.GetData().build.GetSymbol(this.targetSymbolId);
		string text = this.Id.ToLower();
		for (int i = 0; i < build.symbols.Length; i++)
		{
			string text2 = HashCache.Get().Get(build.symbols[i].hash);
			if (text2.StartsWith(text))
			{
				Accessory accessory = new Accessory(text2, parent, this, this.file.batchTag, build.symbols[i], default_build, null);
				this.accessories.Add(accessory);
				HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);
			}
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x000A53F5 File Offset: 0x000A35F5
	public Accessory Lookup(string id)
	{
		return this.Lookup(new HashedString(id));
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x000A5404 File Offset: 0x000A3604
	public Accessory Lookup(HashedString full_id)
	{
		if (!full_id.IsValid)
		{
			return null;
		}
		return this.accessories.Find((Accessory a) => a.IdHash == full_id);
	}

	// Token: 0x04001177 RID: 4471
	private KAnimFile file;
}
