using System;
using System.Xml.Linq;

namespace Satsuma.IO.GraphML
{
	// Token: 0x0200028C RID: 652
	public abstract class GraphMLProperty
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0004DC69 File Offset: 0x0004BE69
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0004DC71 File Offset: 0x0004BE71
		public string Name { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0004DC7A File Offset: 0x0004BE7A
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0004DC82 File Offset: 0x0004BE82
		public PropertyDomain Domain { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0004DC8B File Offset: 0x0004BE8B
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x0004DC93 File Offset: 0x0004BE93
		public string Id { get; set; }

		// Token: 0x0600142C RID: 5164 RVA: 0x0004DC9C File Offset: 0x0004BE9C
		protected GraphMLProperty()
		{
			this.Domain = PropertyDomain.All;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004DCAB File Offset: 0x0004BEAB
		protected static string DomainToGraphML(PropertyDomain domain)
		{
			switch (domain)
			{
			case PropertyDomain.Node:
				return "node";
			case PropertyDomain.Arc:
				return "arc";
			case PropertyDomain.Graph:
				return "graph";
			default:
				return "all";
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004DCDA File Offset: 0x0004BEDA
		protected static PropertyDomain ParseDomain(string s)
		{
			if (s != null)
			{
				if (s == "node")
				{
					return PropertyDomain.Node;
				}
				if (s == "edge")
				{
					return PropertyDomain.Arc;
				}
				if (s == "graph")
				{
					return PropertyDomain.Graph;
				}
			}
			return PropertyDomain.All;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004DD10 File Offset: 0x0004BF10
		protected virtual void LoadFromKeyElement(XElement xKey)
		{
			XAttribute xattribute = xKey.Attribute("attr.name");
			this.Name = ((xattribute == null) ? null : xattribute.Value);
			this.Domain = GraphMLProperty.ParseDomain(xKey.Attribute("for").Value);
			this.Id = xKey.Attribute("id").Value;
			XElement xelement = Utils.ElementLocal(xKey, "default");
			this.ReadData(xelement, null);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004DD90 File Offset: 0x0004BF90
		public virtual XElement GetKeyElement()
		{
			XElement xelement = new XElement(GraphMLFormat.xmlns + "key");
			xelement.SetAttributeValue("attr.name", this.Name);
			xelement.SetAttributeValue("for", GraphMLProperty.DomainToGraphML(this.Domain));
			xelement.SetAttributeValue("id", this.Id);
			XElement xelement2 = this.WriteData(null);
			if (xelement2 != null)
			{
				xelement2.Name = GraphMLFormat.xmlns + "default";
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x06001431 RID: 5169
		public abstract void ReadData(XElement x, object key);

		// Token: 0x06001432 RID: 5170
		public abstract XElement WriteData(object key);
	}
}
