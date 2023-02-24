using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.ValueDeserializers
{
	// Token: 0x020001A8 RID: 424
	public sealed class AliasValueDeserializer : IValueDeserializer
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00038CC3 File Offset: 0x00036EC3
		public AliasValueDeserializer(IValueDeserializer innerDeserializer)
		{
			if (innerDeserializer == null)
			{
				throw new ArgumentNullException("innerDeserializer");
			}
			this.innerDeserializer = innerDeserializer;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00038CE0 File Offset: 0x00036EE0
		public object DeserializeValue(IParser parser, Type expectedType, SerializerState state, IValueDeserializer nestedObjectDeserializer)
		{
			AnchorAlias anchorAlias = parser.Allow<AnchorAlias>();
			if (anchorAlias == null)
			{
				string text = null;
				NodeEvent nodeEvent = parser.Peek<NodeEvent>();
				if (nodeEvent != null && !string.IsNullOrEmpty(nodeEvent.Anchor))
				{
					text = nodeEvent.Anchor;
				}
				object obj = this.innerDeserializer.DeserializeValue(parser, expectedType, state, nestedObjectDeserializer);
				if (text != null)
				{
					AliasValueDeserializer.AliasState aliasState = state.Get<AliasValueDeserializer.AliasState>();
					AliasValueDeserializer.ValuePromise valuePromise;
					if (!aliasState.TryGetValue(text, out valuePromise))
					{
						aliasState.Add(text, new AliasValueDeserializer.ValuePromise(obj));
					}
					else if (!valuePromise.HasValue)
					{
						valuePromise.Value = obj;
					}
					else
					{
						aliasState[text] = new AliasValueDeserializer.ValuePromise(obj);
					}
				}
				return obj;
			}
			AliasValueDeserializer.AliasState aliasState2 = state.Get<AliasValueDeserializer.AliasState>();
			AliasValueDeserializer.ValuePromise valuePromise2;
			if (!aliasState2.TryGetValue(anchorAlias.Value, out valuePromise2))
			{
				valuePromise2 = new AliasValueDeserializer.ValuePromise(anchorAlias);
				aliasState2.Add(anchorAlias.Value, valuePromise2);
			}
			if (!valuePromise2.HasValue)
			{
				return valuePromise2;
			}
			return valuePromise2.Value;
		}

		// Token: 0x04000815 RID: 2069
		private readonly IValueDeserializer innerDeserializer;

		// Token: 0x02000A41 RID: 2625
		private sealed class AliasState : Dictionary<string, AliasValueDeserializer.ValuePromise>, IPostDeserializationCallback
		{
			// Token: 0x060054F7 RID: 21751 RVA: 0x0009E118 File Offset: 0x0009C318
			public void OnDeserialization()
			{
				foreach (AliasValueDeserializer.ValuePromise valuePromise in base.Values)
				{
					if (!valuePromise.HasValue)
					{
						throw new AnchorNotFoundException(valuePromise.Alias.Start, valuePromise.Alias.End, string.Format("Anchor '{0}' not found", valuePromise.Alias.Value));
					}
				}
			}
		}

		// Token: 0x02000A42 RID: 2626
		private sealed class ValuePromise : IValuePromise
		{
			// Token: 0x14000029 RID: 41
			// (add) Token: 0x060054F9 RID: 21753 RVA: 0x0009E1A8 File Offset: 0x0009C3A8
			// (remove) Token: 0x060054FA RID: 21754 RVA: 0x0009E1E0 File Offset: 0x0009C3E0
			public event Action<object> ValueAvailable;

			// Token: 0x17000E5C RID: 3676
			// (get) Token: 0x060054FB RID: 21755 RVA: 0x0009E215 File Offset: 0x0009C415
			// (set) Token: 0x060054FC RID: 21756 RVA: 0x0009E21D File Offset: 0x0009C41D
			public bool HasValue { get; private set; }

			// Token: 0x060054FD RID: 21757 RVA: 0x0009E226 File Offset: 0x0009C426
			public ValuePromise(AnchorAlias alias)
			{
				this.Alias = alias;
			}

			// Token: 0x060054FE RID: 21758 RVA: 0x0009E235 File Offset: 0x0009C435
			public ValuePromise(object value)
			{
				this.HasValue = true;
				this.value = value;
			}

			// Token: 0x17000E5D RID: 3677
			// (get) Token: 0x060054FF RID: 21759 RVA: 0x0009E24B File Offset: 0x0009C44B
			// (set) Token: 0x06005500 RID: 21760 RVA: 0x0009E266 File Offset: 0x0009C466
			public object Value
			{
				get
				{
					if (!this.HasValue)
					{
						throw new InvalidOperationException("Value not set");
					}
					return this.value;
				}
				set
				{
					if (this.HasValue)
					{
						throw new InvalidOperationException("Value already set");
					}
					this.HasValue = true;
					this.value = value;
					if (this.ValueAvailable != null)
					{
						this.ValueAvailable(value);
					}
				}
			}

			// Token: 0x04002307 RID: 8967
			private object value;

			// Token: 0x04002308 RID: 8968
			public readonly AnchorAlias Alias;
		}
	}
}
