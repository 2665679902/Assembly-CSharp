using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E2 RID: 482
	[Serializable]
	public sealed class YamlMappingNode : YamlNode, IEnumerable<KeyValuePair<YamlNode, YamlNode>>, IEnumerable, IYamlConvertible
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003BB73 File Offset: 0x00039D73
		public IDictionary<YamlNode, YamlNode> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003BB7B File Offset: 0x00039D7B
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0003BB83 File Offset: 0x00039D83
		public MappingStyle Style { get; set; }

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003BB8C File Offset: 0x00039D8C
		internal YamlMappingNode(IParser parser, DocumentLoadingState state)
		{
			this.Load(parser, state);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003BBA8 File Offset: 0x00039DA8
		private void Load(IParser parser, DocumentLoadingState state)
		{
			MappingStart mappingStart = parser.Expect<MappingStart>();
			base.Load(mappingStart, state);
			this.Style = mappingStart.Style;
			bool flag = false;
			while (!parser.Accept<MappingEnd>())
			{
				YamlNode yamlNode = YamlNode.ParseNode(parser, state);
				YamlNode yamlNode2 = YamlNode.ParseNode(parser, state);
				try
				{
					this.children.Add(yamlNode, yamlNode2);
				}
				catch (ArgumentException ex)
				{
					throw new YamlException(yamlNode.Start, yamlNode.End, "Duplicate key", ex);
				}
				flag |= yamlNode is YamlAliasNode || yamlNode2 is YamlAliasNode;
			}
			if (flag)
			{
				state.AddNodeWithUnresolvedAliases(this);
			}
			parser.Expect<MappingEnd>();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003BC50 File Offset: 0x00039E50
		public YamlMappingNode()
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003BC63 File Offset: 0x00039E63
		public YamlMappingNode(int dummy)
		{
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003BC76 File Offset: 0x00039E76
		public YamlMappingNode(params KeyValuePair<YamlNode, YamlNode>[] children)
			: this(children)
		{
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003BC80 File Offset: 0x00039E80
		public YamlMappingNode(IEnumerable<KeyValuePair<YamlNode, YamlNode>> children)
		{
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in children)
			{
				this.children.Add(keyValuePair);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003BCE0 File Offset: 0x00039EE0
		public YamlMappingNode(params YamlNode[] children)
			: this(children)
		{
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003BCEC File Offset: 0x00039EEC
		public YamlMappingNode(IEnumerable<YamlNode> children)
		{
			using (IEnumerator<YamlNode> enumerator = children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					YamlNode yamlNode = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						throw new ArgumentException("When constructing a mapping node with a sequence, the number of elements of the sequence must be even.");
					}
					this.Add(yamlNode, enumerator.Current);
				}
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003BD60 File Offset: 0x00039F60
		public void Add(YamlNode key, YamlNode value)
		{
			this.children.Add(key, value);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003BD6F File Offset: 0x00039F6F
		public void Add(string key, YamlNode value)
		{
			this.children.Add(new YamlScalarNode(key), value);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003BD83 File Offset: 0x00039F83
		public void Add(YamlNode key, string value)
		{
			this.children.Add(key, new YamlScalarNode(value));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003BD97 File Offset: 0x00039F97
		public void Add(string key, string value)
		{
			this.children.Add(new YamlScalarNode(key), new YamlScalarNode(value));
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003BDB0 File Offset: 0x00039FB0
		internal override void ResolveAliases(DocumentLoadingState state)
		{
			Dictionary<YamlNode, YamlNode> dictionary = null;
			Dictionary<YamlNode, YamlNode> dictionary2 = null;
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in this.children)
			{
				if (keyValuePair.Key is YamlAliasNode)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<YamlNode, YamlNode>();
					}
					dictionary.Add(keyValuePair.Key, state.GetNode(keyValuePair.Key.Anchor, true, keyValuePair.Key.Start, keyValuePair.Key.End));
				}
				if (keyValuePair.Value is YamlAliasNode)
				{
					if (dictionary2 == null)
					{
						dictionary2 = new Dictionary<YamlNode, YamlNode>();
					}
					dictionary2.Add(keyValuePair.Key, state.GetNode(keyValuePair.Value.Anchor, true, keyValuePair.Value.Start, keyValuePair.Value.End));
				}
			}
			if (dictionary2 != null)
			{
				foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair2 in dictionary2)
				{
					this.children[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			if (dictionary != null)
			{
				foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair3 in dictionary)
				{
					YamlNode yamlNode = this.children[keyValuePair3.Key];
					this.children.Remove(keyValuePair3.Key);
					this.children.Add(keyValuePair3.Value, yamlNode);
				}
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003BF68 File Offset: 0x0003A168
		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			emitter.Emit(new MappingStart(base.Anchor, base.Tag, true, this.Style));
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in this.children)
			{
				keyValuePair.Key.Save(emitter, state);
				keyValuePair.Value.Save(emitter, state);
			}
			emitter.Emit(new MappingEnd());
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003BFF4 File Offset: 0x0003A1F4
		public override void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003C000 File Offset: 0x0003A200
		public override bool Equals(object obj)
		{
			YamlMappingNode yamlMappingNode = obj as YamlMappingNode;
			if (yamlMappingNode == null || !base.Equals(yamlMappingNode) || this.children.Count != yamlMappingNode.children.Count)
			{
				return false;
			}
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in this.children)
			{
				YamlNode yamlNode;
				if (!yamlMappingNode.children.TryGetValue(keyValuePair.Key, out yamlNode) || !YamlNode.SafeEquals(keyValuePair.Value, yamlNode))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003C0A4 File Offset: 0x0003A2A4
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in this.children)
			{
				num = YamlNode.CombineHashCodes(num, YamlNode.GetHashCode(keyValuePair.Key));
				num = YamlNode.CombineHashCodes(num, YamlNode.GetHashCode(keyValuePair.Value));
			}
			return num;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003C118 File Offset: 0x0003A318
		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			level.Increment();
			yield return this;
			foreach (KeyValuePair<YamlNode, YamlNode> child in this.children)
			{
				foreach (YamlNode yamlNode in child.Key.SafeAllNodes(level))
				{
					yield return yamlNode;
				}
				IEnumerator<YamlNode> enumerator2 = null;
				foreach (YamlNode yamlNode2 in child.Value.SafeAllNodes(level))
				{
					yield return yamlNode2;
				}
				enumerator2 = null;
				child = default(KeyValuePair<YamlNode, YamlNode>);
			}
			IEnumerator<KeyValuePair<YamlNode, YamlNode>> enumerator = null;
			level.Decrement();
			yield break;
			yield break;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0003C12F File Offset: 0x0003A32F
		public override YamlNodeType NodeType
		{
			get
			{
				return YamlNodeType.Mapping;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003C134 File Offset: 0x0003A334
		internal override string ToString(RecursionLevel level)
		{
			if (!level.TryIncrement())
			{
				return "WARNING! INFINITE RECURSION!";
			}
			StringBuilder stringBuilder = new StringBuilder("{ ");
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in this.children)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("{ ").Append(keyValuePair.Key.ToString(level)).Append(", ")
					.Append(keyValuePair.Value.ToString(level))
					.Append(" }");
			}
			stringBuilder.Append(" }");
			level.Decrement();
			return stringBuilder.ToString();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003C204 File Offset: 0x0003A404
		public IEnumerator<KeyValuePair<YamlNode, YamlNode>> GetEnumerator()
		{
			return this.children.GetEnumerator();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003C211 File Offset: 0x0003A411
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003C219 File Offset: 0x0003A419
		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			this.Load(parser, new DocumentLoadingState());
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003C227 File Offset: 0x0003A427
		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			this.Emit(emitter, new EmitterState());
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003C238 File Offset: 0x0003A438
		public static YamlMappingNode FromObject(object mapping)
		{
			if (mapping == null)
			{
				throw new ArgumentNullException("mapping");
			}
			YamlMappingNode yamlMappingNode = new YamlMappingNode(0);
			foreach (PropertyInfo propertyInfo in mapping.GetType().GetPublicProperties())
			{
				if (propertyInfo.CanRead && propertyInfo.GetGetMethod().GetParameters().Length == 0)
				{
					object value = propertyInfo.GetValue(mapping, null);
					YamlNode yamlNode = (value as YamlNode) ?? Convert.ToString(value);
					yamlMappingNode.Add(propertyInfo.Name, yamlNode);
				}
			}
			return yamlMappingNode;
		}

		// Token: 0x0400084A RID: 2122
		private readonly IDictionary<YamlNode, YamlNode> children = new Dictionary<YamlNode, YamlNode>();
	}
}
