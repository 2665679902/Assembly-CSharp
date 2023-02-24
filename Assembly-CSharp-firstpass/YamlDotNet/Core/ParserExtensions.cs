using System;
using System.Globalization;
using System.IO;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	// Token: 0x0200020E RID: 526
	public static class ParserExtensions
	{
		// Token: 0x0600102E RID: 4142 RVA: 0x000418D8 File Offset: 0x0003FAD8
		public static T Expect<T>(this IParser parser) where T : ParsingEvent
		{
			T t = parser.Allow<T>();
			if (t == null)
			{
				ParsingEvent parsingEvent = parser.Current;
				throw new YamlException(parsingEvent.Start, parsingEvent.End, string.Format(CultureInfo.InvariantCulture, "Expected '{0}', got '{1}' (at {2}).", typeof(T).Name, parsingEvent.GetType().Name, parsingEvent.Start));
			}
			return t;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004193B File Offset: 0x0003FB3B
		public static bool Accept<T>(this IParser parser) where T : ParsingEvent
		{
			if (parser.Current == null && !parser.MoveNext())
			{
				throw new EndOfStreamException();
			}
			return parser.Current is T;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00041964 File Offset: 0x0003FB64
		public static T Allow<T>(this IParser parser) where T : ParsingEvent
		{
			if (!parser.Accept<T>())
			{
				return default(T);
			}
			T t = (T)((object)parser.Current);
			parser.MoveNext();
			return t;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00041998 File Offset: 0x0003FB98
		public static T Peek<T>(this IParser parser) where T : ParsingEvent
		{
			if (!parser.Accept<T>())
			{
				return default(T);
			}
			return (T)((object)parser.Current);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000419C4 File Offset: 0x0003FBC4
		public static void SkipThisAndNestedEvents(this IParser parser)
		{
			int num = 0;
			do
			{
				num += parser.Peek<ParsingEvent>().NestingIncrease;
				parser.MoveNext();
			}
			while (num > 0);
		}
	}
}
