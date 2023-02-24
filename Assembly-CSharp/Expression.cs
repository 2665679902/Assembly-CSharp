using System;
using System.Diagnostics;

// Token: 0x0200076C RID: 1900
[DebuggerDisplay("{face.hash} {priority}")]
public class Expression : Resource
{
	// Token: 0x06003424 RID: 13348 RVA: 0x001187AB File Offset: 0x001169AB
	public Expression(string id, ResourceSet parent, Face face)
		: base(id, parent, null)
	{
		this.face = face;
	}

	// Token: 0x04002039 RID: 8249
	public Face face;

	// Token: 0x0400203A RID: 8250
	public int priority;
}
