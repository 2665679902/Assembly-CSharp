using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Satsuma.IO.GraphML
{
	// Token: 0x02000293 RID: 659
	public sealed class GraphMLFormat
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0004E590 File Offset: 0x0004C790
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x0004E598 File Offset: 0x0004C798
		public IGraph Graph { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0004E5A1 File Offset: 0x0004C7A1
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0004E5A9 File Offset: 0x0004C7A9
		public IList<GraphMLProperty> Properties { get; private set; }

		// Token: 0x06001462 RID: 5218 RVA: 0x0004E5B4 File Offset: 0x0004C7B4
		public GraphMLFormat()
		{
			this.Properties = new List<GraphMLProperty>();
			List<Func<XElement, GraphMLProperty>> list = new List<Func<XElement, GraphMLProperty>>();
			list.Add((XElement x) => new StandardProperty<bool>(x));
			list.Add((XElement x) => new StandardProperty<double>(x));
			list.Add((XElement x) => new StandardProperty<float>(x));
			list.Add((XElement x) => new StandardProperty<int>(x));
			list.Add((XElement x) => new StandardProperty<long>(x));
			list.Add((XElement x) => new StandardProperty<string>(x));
			list.Add((XElement x) => new NodeGraphicsProperty(x));
			this.PropertyLoaders = list;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
		public void RegisterPropertyLoader(Func<XElement, GraphMLProperty> loader)
		{
			this.PropertyLoaders.Add(loader);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		private static void ReadProperties(Dictionary<string, GraphMLProperty> propertyById, XElement x, object obj)
		{
			foreach (XElement xelement in Utils.ElementsLocal(x, "data"))
			{
				GraphMLProperty graphMLProperty;
				if (propertyById.TryGetValue(xelement.Attribute("key").Value, out graphMLProperty))
				{
					graphMLProperty.ReadData(x, obj);
				}
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004E764 File Offset: 0x0004C964
		public void Load(XDocument doc)
		{
			if (this.Graph == null)
			{
				this.Graph = new CustomGraph();
			}
			IBuildableGraph buildableGraph = (IBuildableGraph)this.Graph;
			buildableGraph.Clear();
			XElement root = doc.Root;
			this.Properties.Clear();
			Dictionary<string, GraphMLProperty> dictionary = new Dictionary<string, GraphMLProperty>();
			foreach (XElement xelement in Utils.ElementsLocal(root, "key"))
			{
				foreach (Func<XElement, GraphMLProperty> func in this.PropertyLoaders)
				{
					try
					{
						GraphMLProperty graphMLProperty = func(xelement);
						this.Properties.Add(graphMLProperty);
						dictionary[graphMLProperty.Id] = graphMLProperty;
						break;
					}
					catch (ArgumentException)
					{
					}
				}
			}
			XElement xelement2 = Utils.ElementLocal(root, "graph");
			Directedness directedness = ((xelement2.Attribute("edgedefault").Value == "directed") ? Directedness.Directed : Directedness.Undirected);
			GraphMLFormat.ReadProperties(dictionary, xelement2, this.Graph);
			Dictionary<string, Node> dictionary2 = new Dictionary<string, Node>();
			foreach (XElement xelement3 in Utils.ElementsLocal(xelement2, "node"))
			{
				Node node = buildableGraph.AddNode();
				dictionary2[xelement3.Attribute("id").Value] = node;
				GraphMLFormat.ReadProperties(dictionary, xelement3, node);
			}
			foreach (XElement xelement4 in Utils.ElementsLocal(xelement2, "edge"))
			{
				Node node2 = dictionary2[xelement4.Attribute("source").Value];
				Node node3 = dictionary2[xelement4.Attribute("target").Value];
				Directedness directedness2 = directedness;
				XAttribute xattribute = xelement4.Attribute("directed");
				if (xattribute != null)
				{
					directedness2 = ((xattribute.Value == "true") ? Directedness.Directed : Directedness.Undirected);
				}
				Arc arc = buildableGraph.AddArc(node2, node3, directedness2);
				GraphMLFormat.ReadProperties(dictionary, xelement4, arc);
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004E9FC File Offset: 0x0004CBFC
		public void Load(XmlReader xml)
		{
			XDocument xdocument = XDocument.Load(xml);
			this.Load(xdocument);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004EA18 File Offset: 0x0004CC18
		public void Load(TextReader reader)
		{
			using (XmlReader xmlReader = XmlReader.Create(reader))
			{
				this.Load(xmlReader);
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004EA50 File Offset: 0x0004CC50
		public void Load(string filename)
		{
			using (StreamReader streamReader = new StreamReader(filename))
			{
				this.Load(streamReader);
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0004EA88 File Offset: 0x0004CC88
		private void DefinePropertyValues(XmlWriter xml, object obj)
		{
			foreach (GraphMLProperty graphMLProperty in this.Properties)
			{
				XElement xelement = graphMLProperty.WriteData(obj);
				if (xelement != null)
				{
					xelement.Name = GraphMLFormat.xmlns + "data";
					xelement.SetAttributeValue("key", graphMLProperty.Id);
					xelement.WriteTo(xml);
				}
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0004EB0C File Offset: 0x0004CD0C
		private void Save(XmlWriter xml)
		{
			xml.WriteStartDocument();
			xml.WriteStartElement("graphml", GraphMLFormat.xmlns.NamespaceName);
			xml.WriteAttributeString("xmlns", "xsi", null, GraphMLFormat.xmlnsXsi.NamespaceName);
			xml.WriteAttributeString("xmlns", "y", null, GraphMLFormat.xmlnsY.NamespaceName);
			xml.WriteAttributeString("xmlns", "yed", null, GraphMLFormat.xmlnsYed.NamespaceName);
			xml.WriteAttributeString("xsi", "schemaLocation", null, "http://graphml.graphdrawing.org/xmlns\nhttp://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd");
			for (int i = 0; i < this.Properties.Count; i++)
			{
				GraphMLProperty graphMLProperty = this.Properties[i];
				graphMLProperty.Id = "d" + i.ToString();
				graphMLProperty.GetKeyElement().WriteTo(xml);
			}
			xml.WriteStartElement("graph", GraphMLFormat.xmlns.NamespaceName);
			xml.WriteAttributeString("id", "G");
			xml.WriteAttributeString("edgedefault", "directed");
			xml.WriteAttributeString("parse.nodes", this.Graph.NodeCount().ToString(CultureInfo.InvariantCulture));
			xml.WriteAttributeString("parse.edges", this.Graph.ArcCount(ArcFilter.All).ToString(CultureInfo.InvariantCulture));
			xml.WriteAttributeString("parse.order", "nodesfirst");
			this.DefinePropertyValues(xml, this.Graph);
			foreach (Node node in this.Graph.Nodes())
			{
				xml.WriteStartElement("node", GraphMLFormat.xmlns.NamespaceName);
				xml.WriteAttributeString("id", node.Id.ToString(CultureInfo.InvariantCulture));
				this.DefinePropertyValues(xml, node);
				xml.WriteEndElement();
			}
			foreach (Arc arc in this.Graph.Arcs(ArcFilter.All))
			{
				xml.WriteStartElement("edge", GraphMLFormat.xmlns.NamespaceName);
				xml.WriteAttributeString("id", arc.Id.ToString(CultureInfo.InvariantCulture));
				if (this.Graph.IsEdge(arc))
				{
					xml.WriteAttributeString("directed", "false");
				}
				xml.WriteAttributeString("source", this.Graph.U(arc).Id.ToString(CultureInfo.InvariantCulture));
				xml.WriteAttributeString("target", this.Graph.V(arc).Id.ToString(CultureInfo.InvariantCulture));
				this.DefinePropertyValues(xml, arc);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();
			xml.WriteEndElement();
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0004EE18 File Offset: 0x0004D018
		public void Save(TextWriter writer)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(writer))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0004EE50 File Offset: 0x0004D050
		public void Save(string filename)
		{
			using (StreamWriter streamWriter = new StreamWriter(filename))
			{
				this.Save(streamWriter);
			}
		}

		// Token: 0x04000A61 RID: 2657
		internal static readonly XNamespace xmlns = "http://graphml.graphdrawing.org/xmlns";

		// Token: 0x04000A62 RID: 2658
		private static readonly XNamespace xmlnsXsi = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04000A63 RID: 2659
		internal static readonly XNamespace xmlnsY = "http://www.yworks.com/xml/graphml";

		// Token: 0x04000A64 RID: 2660
		private static readonly XNamespace xmlnsYed = "http://www.yworks.com/xml/yed/3";

		// Token: 0x04000A65 RID: 2661
		private const string xsiSchemaLocation = "http://graphml.graphdrawing.org/xmlns\nhttp://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd";

		// Token: 0x04000A68 RID: 2664
		private readonly List<Func<XElement, GraphMLProperty>> PropertyLoaders;
	}
}
