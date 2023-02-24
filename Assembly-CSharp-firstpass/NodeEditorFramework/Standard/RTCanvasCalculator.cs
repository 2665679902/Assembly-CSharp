using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeEditorFramework.Standard
{
	// Token: 0x02000496 RID: 1174
	public class RTCanvasCalculator : MonoBehaviour
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x00068EE4 File Offset: 0x000670E4
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x00068EEC File Offset: 0x000670EC
		public NodeCanvas canvas { get; private set; }

		// Token: 0x060032A7 RID: 12967 RVA: 0x00068EF5 File Offset: 0x000670F5
		private void Start()
		{
			this.LoadCanvas(this.canvasPath);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x00068F04 File Offset: 0x00067104
		public void AssureCanvas()
		{
			if (this.canvas == null)
			{
				this.LoadCanvas(this.canvasPath);
				if (this.canvas == null)
				{
					throw new UnityException("No canvas specified to calculate on " + base.name + "!");
				}
			}
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x00068F54 File Offset: 0x00067154
		public void LoadCanvas(string path)
		{
			this.canvasPath = path;
			if (!string.IsNullOrEmpty(this.canvasPath))
			{
				this.canvas = NodeEditorSaveManager.LoadNodeCanvas(this.canvasPath, true);
				this.CalculateCanvas();
				return;
			}
			this.canvas = null;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x00068F8A File Offset: 0x0006718A
		public void CalculateCanvas()
		{
			this.AssureCanvas();
			NodeEditor.RecalculateAll(this.canvas);
			this.DebugOutputResults();
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x00068FA4 File Offset: 0x000671A4
		private void DebugOutputResults()
		{
			this.AssureCanvas();
			foreach (Node node in this.getOutputNodes())
			{
				string text = "(OUT) " + node.name + ": ";
				if (node.Outputs.Count == 0)
				{
					using (List<NodeInput>.Enumerator enumerator2 = node.Inputs.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							NodeInput nodeInput = enumerator2.Current;
							text = string.Concat(new string[]
							{
								text,
								nodeInput.typeID,
								" ",
								nodeInput.IsValueNull ? "NULL" : nodeInput.GetValue().ToString(),
								"; "
							});
						}
						goto IL_138;
					}
					goto IL_BE;
				}
				goto IL_BE;
				IL_138:
				global::Debug.Log(text);
				continue;
				IL_BE:
				foreach (NodeOutput nodeOutput in node.Outputs)
				{
					text = string.Concat(new string[]
					{
						text,
						nodeOutput.typeID,
						" ",
						nodeOutput.IsValueNull ? "NULL" : nodeOutput.GetValue().ToString(),
						"; "
					});
				}
				goto IL_138;
			}
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x00069158 File Offset: 0x00067358
		public List<Node> getInputNodes()
		{
			this.AssureCanvas();
			return this.canvas.nodes.Where(delegate(Node node)
			{
				if (node.Inputs.Count != 0 || node.Outputs.Count == 0)
				{
					return node.Inputs.TrueForAll((NodeInput input) => input.connection == null);
				}
				return true;
			}).ToList<Node>();
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x00069194 File Offset: 0x00067394
		public List<Node> getOutputNodes()
		{
			this.AssureCanvas();
			return this.canvas.nodes.Where(delegate(Node node)
			{
				if (node.Outputs.Count != 0 || node.Inputs.Count == 0)
				{
					return node.Outputs.TrueForAll((NodeOutput output) => output.connections.Count == 0);
				}
				return true;
			}).ToList<Node>();
		}

		// Token: 0x0400118E RID: 4494
		public string canvasPath;
	}
}
