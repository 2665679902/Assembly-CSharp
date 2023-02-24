using System;
using System.Globalization;
using System.Xml.Linq;

namespace Satsuma.IO.GraphML
{
	// Token: 0x02000291 RID: 657
	public sealed class NodeGraphics
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0004E16B File Offset: 0x0004C36B
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x0004E173 File Offset: 0x0004C373
		public double X { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0004E17C File Offset: 0x0004C37C
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x0004E184 File Offset: 0x0004C384
		public double Y { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0004E18D File Offset: 0x0004C38D
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x0004E195 File Offset: 0x0004C395
		public double Width { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0004E19E File Offset: 0x0004C39E
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x0004E1A6 File Offset: 0x0004C3A6
		public double Height { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0004E1AF File Offset: 0x0004C3AF
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x0004E1B7 File Offset: 0x0004C3B7
		public NodeShape Shape { get; set; }

		// Token: 0x06001453 RID: 5203 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		public NodeGraphics()
		{
			this.X = (this.Y = 0.0);
			this.Width = (this.Height = 10.0);
			this.Shape = NodeShape.Rectangle;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004E271 File Offset: 0x0004C471
		private NodeShape ParseShape(string s)
		{
			return (NodeShape)Math.Max(0, Array.IndexOf<string>(this.nodeShapeToString, s));
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004E285 File Offset: 0x0004C485
		private string ShapeToGraphML(NodeShape shape)
		{
			return this.nodeShapeToString[(int)shape];
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004E290 File Offset: 0x0004C490
		public NodeGraphics(XElement xData)
		{
			XElement xelement = Utils.ElementLocal(xData, "Geometry");
			if (xelement != null)
			{
				this.X = double.Parse(xelement.Attribute("x").Value, CultureInfo.InvariantCulture);
				this.Y = double.Parse(xelement.Attribute("y").Value, CultureInfo.InvariantCulture);
				this.Width = double.Parse(xelement.Attribute("width").Value, CultureInfo.InvariantCulture);
				this.Height = double.Parse(xelement.Attribute("height").Value, CultureInfo.InvariantCulture);
			}
			XElement xelement2 = Utils.ElementLocal(xData, "Shape");
			if (xelement2 != null)
			{
				this.Shape = this.ParseShape(xelement2.Attribute("type").Value);
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004E3E0 File Offset: 0x0004C5E0
		public XElement ToXml()
		{
			return new XElement("dummy", new XElement(GraphMLFormat.xmlnsY + "ShapeNode", new object[]
			{
				new XElement(GraphMLFormat.xmlnsY + "Geometry", new object[]
				{
					new XAttribute("x", this.X.ToString(CultureInfo.InvariantCulture)),
					new XAttribute("y", this.Y.ToString(CultureInfo.InvariantCulture)),
					new XAttribute("width", this.Width.ToString(CultureInfo.InvariantCulture)),
					new XAttribute("height", this.Height.ToString(CultureInfo.InvariantCulture))
				}),
				new XElement(GraphMLFormat.xmlnsY + "Shape", new XAttribute("type", this.ShapeToGraphML(this.Shape)))
			}));
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004E4F9 File Offset: 0x0004C6F9
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x04000A60 RID: 2656
		private readonly string[] nodeShapeToString = new string[]
		{
			"rectangle", "roundrectangle", "ellipse", "parallelogram", "hexagon", "triangle", "rectangle3d", "octagon", "diamond", "trapezoid",
			"trapezoid2"
		};
	}
}
