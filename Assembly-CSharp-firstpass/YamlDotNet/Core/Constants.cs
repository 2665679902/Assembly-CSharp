using System;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	// Token: 0x020001FD RID: 509
	internal static class Constants
	{
		// Token: 0x04000881 RID: 2177
		public static readonly TagDirective[] DefaultTagDirectives = new TagDirective[]
		{
			new TagDirective("!", "!"),
			new TagDirective("!!", "tag:yaml.org,2002:")
		};

		// Token: 0x04000882 RID: 2178
		public const int MajorVersion = 1;

		// Token: 0x04000883 RID: 2179
		public const int MinorVersion = 1;

		// Token: 0x04000884 RID: 2180
		public const char HandleCharacter = '!';

		// Token: 0x04000885 RID: 2181
		public const string DefaultHandle = "!";
	}
}
