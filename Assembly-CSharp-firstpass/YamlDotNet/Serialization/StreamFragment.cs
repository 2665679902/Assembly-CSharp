using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A0 RID: 416
	public sealed class StreamFragment : IYamlConvertible
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000386CE File Offset: 0x000368CE
		public IList<ParsingEvent> Events
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000386D8 File Offset: 0x000368D8
		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			this.events.Clear();
			int num = 0;
			while (parser.MoveNext())
			{
				ParsingEvent parsingEvent = parser.Current;
				this.events.Add(parsingEvent);
				num += parser.Current.NestingIncrease;
				if (num <= 0)
				{
					Debug.Assert(num == 0);
					return;
				}
			}
			throw new InvalidOperationException("The parser has reached the end before deserialization completed.");
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00038734 File Offset: 0x00036934
		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			foreach (ParsingEvent parsingEvent in this.events)
			{
				emitter.Emit(parsingEvent);
			}
		}

		// Token: 0x04000809 RID: 2057
		private readonly List<ParsingEvent> events = new List<ParsingEvent>();
	}
}
