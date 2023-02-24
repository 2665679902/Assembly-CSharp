using System;

// Token: 0x02000508 RID: 1288
public class Accessory : Resource
{
	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000A518C File Offset: 0x000A338C
	// (set) Token: 0x06001ECA RID: 7882 RVA: 0x000A5194 File Offset: 0x000A3394
	public KAnim.Build.Symbol symbol { get; private set; }

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000A519D File Offset: 0x000A339D
	// (set) Token: 0x06001ECC RID: 7884 RVA: 0x000A51A5 File Offset: 0x000A33A5
	public HashedString batchSource { get; private set; }

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000A51AE File Offset: 0x000A33AE
	// (set) Token: 0x06001ECE RID: 7886 RVA: 0x000A51B6 File Offset: 0x000A33B6
	public AccessorySlot slot { get; private set; }

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000A51BF File Offset: 0x000A33BF
	// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x000A51C7 File Offset: 0x000A33C7
	public KAnimFile animFile { get; private set; }

	// Token: 0x06001ED1 RID: 7889 RVA: 0x000A51D0 File Offset: 0x000A33D0
	public Accessory(string id, ResourceSet parent, AccessorySlot slot, HashedString batchSource, KAnim.Build.Symbol symbol, KAnimFile animFile = null, KAnimFile defaultAnimFile = null)
		: base(id, parent, null)
	{
		this.slot = slot;
		this.symbol = symbol;
		this.batchSource = batchSource;
		this.animFile = animFile;
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x000A51FA File Offset: 0x000A33FA
	public bool IsDefault()
	{
		return this.animFile == this.slot.defaultAnimFile;
	}
}
