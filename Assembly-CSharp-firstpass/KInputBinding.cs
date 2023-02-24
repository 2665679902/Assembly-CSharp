using System;

// Token: 0x02000028 RID: 40
public class KInputBinding
{
	// Token: 0x0600020B RID: 523 RVA: 0x0000BE14 File Offset: 0x0000A014
	public KInputBinding(KKeyCode key_code, Modifier modifier, global::Action action)
	{
		this.mKeyCode = key_code;
		this.mAction = action;
		this.mModifier = modifier;
	}

	// Token: 0x040001EC RID: 492
	public KKeyCode mKeyCode;

	// Token: 0x040001ED RID: 493
	public global::Action mAction;

	// Token: 0x040001EE RID: 494
	public Modifier mModifier;
}
