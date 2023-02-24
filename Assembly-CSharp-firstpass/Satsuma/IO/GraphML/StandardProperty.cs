using System;
using System.Globalization;
using System.Xml.Linq;

namespace Satsuma.IO.GraphML
{
	// Token: 0x0200028F RID: 655
	public sealed class StandardProperty<T> : DictionaryProperty<T>
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x0004DF44 File Offset: 0x0004C144
		public StandardProperty()
		{
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004DF4C File Offset: 0x0004C14C
		internal StandardProperty(XElement xKey)
			: this()
		{
			XAttribute xattribute = xKey.Attribute("attr.type");
			if (xattribute == null || xattribute.Value != StandardProperty<T>.TypeString)
			{
				throw new ArgumentException("Key not compatible with property.");
			}
			this.LoadFromKeyElement(xKey);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004DF98 File Offset: 0x0004C198
		private static StandardType ParseType(Type t)
		{
			if (t == typeof(bool))
			{
				return StandardType.Bool;
			}
			if (t == typeof(double))
			{
				return StandardType.Double;
			}
			if (t == typeof(float))
			{
				return StandardType.Float;
			}
			if (t == typeof(int))
			{
				return StandardType.Int;
			}
			if (t == typeof(long))
			{
				return StandardType.Long;
			}
			if (t == typeof(string))
			{
				return StandardType.String;
			}
			throw new ArgumentException("Invalid type for a standard GraphML property.");
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0004E028 File Offset: 0x0004C228
		private static string TypeToGraphML(StandardType type)
		{
			switch (type)
			{
			case StandardType.Bool:
				return "boolean";
			case StandardType.Double:
				return "double";
			case StandardType.Float:
				return "float";
			case StandardType.Int:
				return "int";
			case StandardType.Long:
				return "long";
			default:
				return "string";
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004E074 File Offset: 0x0004C274
		private static object ParseValue(string value)
		{
			switch (StandardProperty<T>.Type)
			{
			case StandardType.Bool:
				return value == "true";
			case StandardType.Double:
				return double.Parse(value, CultureInfo.InvariantCulture);
			case StandardType.Float:
				return float.Parse(value, CultureInfo.InvariantCulture);
			case StandardType.Int:
				return int.Parse(value, CultureInfo.InvariantCulture);
			case StandardType.Long:
				return long.Parse(value, CultureInfo.InvariantCulture);
			default:
				return value;
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004E0F9 File Offset: 0x0004C2F9
		public override XElement GetKeyElement()
		{
			XElement keyElement = base.GetKeyElement();
			keyElement.SetAttributeValue("attr.type", StandardProperty<T>.TypeString);
			return keyElement;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004E116 File Offset: 0x0004C316
		protected override T ReadValue(XElement x)
		{
			return (T)((object)StandardProperty<T>.ParseValue(x.Value));
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004E128 File Offset: 0x0004C328
		protected override XElement WriteValue(T value)
		{
			return new XElement("dummy", value.ToString());
		}

		// Token: 0x04000A4D RID: 2637
		private static readonly StandardType Type = StandardProperty<T>.ParseType(typeof(T));

		// Token: 0x04000A4E RID: 2638
		private static readonly string TypeString = StandardProperty<T>.TypeToGraphML(StandardProperty<T>.Type);
	}
}
