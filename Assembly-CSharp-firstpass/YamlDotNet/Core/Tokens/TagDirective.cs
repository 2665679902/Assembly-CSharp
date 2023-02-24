using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200022D RID: 557
	[Serializable]
	public class TagDirective : Token
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00044197 File Offset: 0x00042397
		public string Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0004419F File Offset: 0x0004239F
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000441A7 File Offset: 0x000423A7
		public TagDirective(string handle, string prefix)
			: this(handle, prefix, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x000441BC File Offset: 0x000423BC
		public TagDirective(string handle, string prefix, Mark start, Mark end)
			: base(start, end)
		{
			if (string.IsNullOrEmpty(handle))
			{
				throw new ArgumentNullException("handle", "Tag handle must not be empty.");
			}
			if (!TagDirective.tagHandleValidator.IsMatch(handle))
			{
				throw new ArgumentException("Tag handle must start and end with '!' and contain alphanumerical characters only.", "handle");
			}
			this.handle = handle;
			if (string.IsNullOrEmpty(prefix))
			{
				throw new ArgumentNullException("prefix", "Tag prefix must not be empty.");
			}
			this.prefix = prefix;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00044230 File Offset: 0x00042430
		public override bool Equals(object obj)
		{
			TagDirective tagDirective = obj as TagDirective;
			return tagDirective != null && this.handle.Equals(tagDirective.handle) && this.prefix.Equals(tagDirective.prefix);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0004426D File Offset: 0x0004246D
		public override int GetHashCode()
		{
			return this.handle.GetHashCode() ^ this.prefix.GetHashCode();
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00044286 File Offset: 0x00042486
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} => {1}", this.handle, this.prefix);
		}

		// Token: 0x04000914 RID: 2324
		private readonly string handle;

		// Token: 0x04000915 RID: 2325
		private readonly string prefix;

		// Token: 0x04000916 RID: 2326
		private static readonly Regex tagHandleValidator = new Regex("^!([0-9A-Za-z_\\-]*!)?$", RegexOptions.None);
	}
}
