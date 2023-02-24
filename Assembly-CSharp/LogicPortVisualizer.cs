using System;
using UnityEngine;

// Token: 0x02000804 RID: 2052
public class LogicPortVisualizer : ILogicUIElement, IUniformGridObject
{
	// Token: 0x06003B76 RID: 15222 RVA: 0x0014A130 File Offset: 0x00148330
	public LogicPortVisualizer(int cell, LogicPortSpriteType sprite_type)
	{
		this.cell = cell;
		this.spriteType = sprite_type;
	}

	// Token: 0x06003B77 RID: 15223 RVA: 0x0014A146 File Offset: 0x00148346
	public int GetLogicUICell()
	{
		return this.cell;
	}

	// Token: 0x06003B78 RID: 15224 RVA: 0x0014A14E File Offset: 0x0014834E
	public Vector2 PosMin()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06003B79 RID: 15225 RVA: 0x0014A160 File Offset: 0x00148360
	public Vector2 PosMax()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06003B7A RID: 15226 RVA: 0x0014A172 File Offset: 0x00148372
	public LogicPortSpriteType GetLogicPortSpriteType()
	{
		return this.spriteType;
	}

	// Token: 0x040026D3 RID: 9939
	private int cell;

	// Token: 0x040026D4 RID: 9940
	private LogicPortSpriteType spriteType;
}
