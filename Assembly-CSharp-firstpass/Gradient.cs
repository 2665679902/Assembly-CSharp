using System;

// Token: 0x02000126 RID: 294
[Serializable]
public class Gradient<T>
{
	// Token: 0x06000A2E RID: 2606 RVA: 0x000271C7 File Offset: 0x000253C7
	public Gradient(T content, float bandSize)
	{
		this.bandSize = bandSize;
		this.content = content;
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000271DD File Offset: 0x000253DD
	// (set) Token: 0x06000A30 RID: 2608 RVA: 0x000271E5 File Offset: 0x000253E5
	public T content { get; protected set; }

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000271EE File Offset: 0x000253EE
	// (set) Token: 0x06000A32 RID: 2610 RVA: 0x000271F6 File Offset: 0x000253F6
	public float bandSize { get; protected set; }

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000A33 RID: 2611 RVA: 0x000271FF File Offset: 0x000253FF
	// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00027207 File Offset: 0x00025407
	public float maxValue { get; set; }
}
