using System;
using System.Diagnostics;
using ProcGen;

// Token: 0x02000127 RID: 295
[DebuggerDisplay("{content} {bandSize} {maxValue}")]
[Serializable]
public class ElementGradient : Gradient<string>
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x00027210 File Offset: 0x00025410
	public ElementGradient()
		: base(null, 0f)
	{
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0002721E File Offset: 0x0002541E
	public ElementGradient(string content, float bandSize, SampleDescriber.Override overrides)
		: base(content, bandSize)
	{
		this.overrides = overrides;
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002722F File Offset: 0x0002542F
	// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00027237 File Offset: 0x00025437
	public SampleDescriber.Override overrides { get; set; }

	// Token: 0x06000A39 RID: 2617 RVA: 0x00027240 File Offset: 0x00025440
	public void Mod(WorldTrait.ElementBandModifier mod)
	{
		global::Debug.Assert(mod.element == base.content);
		base.bandSize *= mod.bandMultiplier;
		if (this.overrides == null)
		{
			this.overrides = new SampleDescriber.Override();
		}
		this.overrides.ModMultiplyMass(mod.massMultiplier);
	}
}
