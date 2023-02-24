using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Satsuma.IO
{
	// Token: 0x02000289 RID: 649
	public sealed class SimpleGraphFormat
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0004CEE7 File Offset: 0x0004B0E7
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0004CEEF File Offset: 0x0004B0EF
		public IGraph Graph { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0004CEF8 File Offset: 0x0004B0F8
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0004CF00 File Offset: 0x0004B100
		public IList<Dictionary<Arc, string>> Extensions { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0004CF09 File Offset: 0x0004B109
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0004CF11 File Offset: 0x0004B111
		public int StartIndex { get; set; }

		// Token: 0x06001411 RID: 5137 RVA: 0x0004CF1A File Offset: 0x0004B11A
		public SimpleGraphFormat()
		{
			this.Extensions = new List<Dictionary<Arc, string>>();
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0004CF30 File Offset: 0x0004B130
		public Node[] Load(TextReader reader, Directedness directedness)
		{
			if (this.Graph == null)
			{
				this.Graph = new CustomGraph();
			}
			IBuildableGraph buildableGraph = (IBuildableGraph)this.Graph;
			buildableGraph.Clear();
			Regex regex = new Regex("\\s+");
			string[] array = regex.Split(reader.ReadLine());
			int num = int.Parse(array[0], CultureInfo.InvariantCulture);
			int num2 = int.Parse(array[1], CultureInfo.InvariantCulture);
			Node[] array2 = new Node[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = buildableGraph.AddNode();
			}
			this.Extensions.Clear();
			for (int j = 0; j < num2; j++)
			{
				array = regex.Split(reader.ReadLine());
				int num3 = (int)(long.Parse(array[0], CultureInfo.InvariantCulture) - (long)this.StartIndex);
				int num4 = (int)(long.Parse(array[1], CultureInfo.InvariantCulture) - (long)this.StartIndex);
				Arc arc = buildableGraph.AddArc(array2[num3], array2[num4], directedness);
				int num5 = array.Length - 2;
				for (int k = 0; k < num5 - this.Extensions.Count; k++)
				{
					this.Extensions.Add(new Dictionary<Arc, string>());
				}
				for (int l = 0; l < num5; l++)
				{
					this.Extensions[l][arc] = array[2 + l];
				}
			}
			return array2;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0004D09C File Offset: 0x0004B29C
		public Node[] Load(string filename, Directedness directedness)
		{
			Node[] array;
			using (StreamReader streamReader = new StreamReader(filename))
			{
				array = this.Load(streamReader, directedness);
			}
			return array;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0004D0D8 File Offset: 0x0004B2D8
		public void Save(TextWriter writer)
		{
			Regex regex = new Regex("\\s");
			writer.WriteLine(this.Graph.NodeCount().ToString() + " " + this.Graph.ArcCount(ArcFilter.All).ToString());
			Dictionary<Node, long> dictionary = new Dictionary<Node, long>();
			long num = (long)this.StartIndex;
			foreach (Arc arc in this.Graph.Arcs(ArcFilter.All))
			{
				Node node = this.Graph.U(arc);
				long num2;
				if (!dictionary.TryGetValue(node, out num2))
				{
					Dictionary<Node, long> dictionary2 = dictionary;
					Node node2 = node;
					long num3 = num;
					num = num3 + 1L;
					num2 = num3;
					dictionary2[node2] = num3;
				}
				Node node3 = this.Graph.V(arc);
				long num4;
				if (!dictionary.TryGetValue(node3, out num4))
				{
					Dictionary<Node, long> dictionary3 = dictionary;
					Node node4 = node3;
					long num5 = num;
					num = num5 + 1L;
					num4 = num5;
					dictionary3[node4] = num5;
				}
				writer.Write(num2.ToString() + " " + num4.ToString());
				foreach (Dictionary<Arc, string> dictionary4 in this.Extensions)
				{
					string text;
					dictionary4.TryGetValue(arc, out text);
					if (string.IsNullOrEmpty(text) || regex.IsMatch(text))
					{
						throw new ArgumentException("Extension value is empty or contains whitespaces.");
					}
					writer.Write(" " + dictionary4[arc]);
				}
				writer.WriteLine();
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0004D294 File Offset: 0x0004B494
		public void Save(string filename)
		{
			using (StreamWriter streamWriter = new StreamWriter(filename))
			{
				this.Save(streamWriter);
			}
		}
	}
}
