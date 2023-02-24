using System;
using System.Xml.Linq;

namespace Satsuma.IO.GraphML
{
	// Token: 0x02000292 RID: 658
	public sealed class NodeGraphicsProperty : DictionaryProperty<NodeGraphics>
	{
		// Token: 0x06001459 RID: 5209 RVA: 0x0004E506 File Offset: 0x0004C706
		public NodeGraphicsProperty()
		{
			base.Domain = PropertyDomain.Node;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004E518 File Offset: 0x0004C718
		internal NodeGraphicsProperty(XElement xKey)
			: this()
		{
			XAttribute xattribute = xKey.Attribute("yfiles.type");
			if (xattribute == null || xattribute.Value != "nodegraphics")
			{
				throw new ArgumentException("Key not compatible with property.");
			}
			this.LoadFromKeyElement(xKey);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004E563 File Offset: 0x0004C763
		public override XElement GetKeyElement()
		{
			XElement keyElement = base.GetKeyElement();
			keyElement.SetAttributeValue("yfiles.type", "nodegraphics");
			return keyElement;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004E580 File Offset: 0x0004C780
		protected override NodeGraphics ReadValue(XElement x)
		{
			return new NodeGraphics(x);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0004E588 File Offset: 0x0004C788
		protected override XElement WriteValue(NodeGraphics value)
		{
			return value.ToXml();
		}
	}
}
