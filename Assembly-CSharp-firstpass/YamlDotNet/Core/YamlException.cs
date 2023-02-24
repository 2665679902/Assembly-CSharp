using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	public class YamlException : Exception
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00043E76 File Offset: 0x00042076
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x00043E7E File Offset: 0x0004207E
		public Mark Start { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00043E87 File Offset: 0x00042087
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x00043E8F File Offset: 0x0004208F
		public Mark End { get; private set; }

		// Token: 0x0600109E RID: 4254 RVA: 0x00043E98 File Offset: 0x00042098
		public YamlException()
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00043EA0 File Offset: 0x000420A0
		public YamlException(string message)
			: base(message)
		{
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00043EA9 File Offset: 0x000420A9
		public YamlException(Mark start, Mark end, string message)
			: this(start, end, message, null)
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00043EB5 File Offset: 0x000420B5
		public YamlException(Mark start, Mark end, string message, Exception innerException)
			: base(string.Format("({0}) - ({1}): {2}", start, end, message), innerException)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00043EDA File Offset: 0x000420DA
		public YamlException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
