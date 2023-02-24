using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005EA RID: 1514
[SkipSaveFileSerialization]
public class LogicGateVisualizer : LogicGateBase
{
	// Token: 0x06002672 RID: 9842 RVA: 0x000D02C3 File Offset: 0x000CE4C3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x000D02D1 File Offset: 0x000CE4D1
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.Unregister();
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x000D02E0 File Offset: 0x000CE4E0
	private void Register()
	{
		this.Unregister();
		this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellOne, false));
		if (base.RequiresFourOutputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellTwo, false));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellThree, false));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellFour, false));
		}
		this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellOne, true));
		if (base.RequiresTwoInputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellTwo, true));
		}
		else if (base.RequiresFourInputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellTwo, true));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellThree, true));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellFour, true));
		}
		if (base.RequiresControlInputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.ControlCellOne, true));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.ControlCellTwo, true));
		}
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		foreach (LogicGateVisualizer.IOVisualizer iovisualizer in this.visChildren)
		{
			logicCircuitManager.AddVisElem(iovisualizer);
		}
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x000D0464 File Offset: 0x000CE664
	private void Unregister()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		foreach (LogicGateVisualizer.IOVisualizer iovisualizer in this.visChildren)
		{
			logicCircuitManager.RemoveVisElem(iovisualizer);
		}
		this.visChildren.Clear();
	}

	// Token: 0x040016C8 RID: 5832
	private List<LogicGateVisualizer.IOVisualizer> visChildren = new List<LogicGateVisualizer.IOVisualizer>();

	// Token: 0x0200124B RID: 4683
	private class IOVisualizer : ILogicUIElement, IUniformGridObject
	{
		// Token: 0x060079A4 RID: 31140 RVA: 0x002C4307 File Offset: 0x002C2507
		public IOVisualizer(int cell, bool input)
		{
			this.cell = cell;
			this.input = input;
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x002C431D File Offset: 0x002C251D
		public int GetLogicUICell()
		{
			return this.cell;
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x002C4325 File Offset: 0x002C2525
		public LogicPortSpriteType GetLogicPortSpriteType()
		{
			if (!this.input)
			{
				return LogicPortSpriteType.Output;
			}
			return LogicPortSpriteType.Input;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x002C4332 File Offset: 0x002C2532
		public Vector2 PosMin()
		{
			return Grid.CellToPos2D(this.cell);
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x002C4344 File Offset: 0x002C2544
		public Vector2 PosMax()
		{
			return this.PosMin();
		}

		// Token: 0x04005D8C RID: 23948
		private int cell;

		// Token: 0x04005D8D RID: 23949
		private bool input;
	}
}
