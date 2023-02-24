using System;
using UnityEngine;

// Token: 0x02000800 RID: 2048
internal class LogicEventSender : ILogicEventSender, ILogicNetworkConnection, ILogicUIElement, IUniformGridObject
{
	// Token: 0x06003B31 RID: 15153 RVA: 0x0014883A File Offset: 0x00146A3A
	public LogicEventSender(HashedString id, int cell, Action<int> on_value_changed, Action<int, bool> on_connection_changed, LogicPortSpriteType sprite_type)
	{
		this.id = id;
		this.cell = cell;
		this.onValueChanged = on_value_changed;
		this.onConnectionChanged = on_connection_changed;
		this.spriteType = sprite_type;
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x06003B32 RID: 15154 RVA: 0x00148867 File Offset: 0x00146A67
	public HashedString ID
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06003B33 RID: 15155 RVA: 0x0014886F File Offset: 0x00146A6F
	public int GetLogicCell()
	{
		return this.cell;
	}

	// Token: 0x06003B34 RID: 15156 RVA: 0x00148877 File Offset: 0x00146A77
	public int GetLogicValue()
	{
		return this.logicValue;
	}

	// Token: 0x06003B35 RID: 15157 RVA: 0x0014887F File Offset: 0x00146A7F
	public int GetLogicUICell()
	{
		return this.GetLogicCell();
	}

	// Token: 0x06003B36 RID: 15158 RVA: 0x00148887 File Offset: 0x00146A87
	public LogicPortSpriteType GetLogicPortSpriteType()
	{
		return this.spriteType;
	}

	// Token: 0x06003B37 RID: 15159 RVA: 0x0014888F File Offset: 0x00146A8F
	public Vector2 PosMin()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06003B38 RID: 15160 RVA: 0x001488A1 File Offset: 0x00146AA1
	public Vector2 PosMax()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06003B39 RID: 15161 RVA: 0x001488B3 File Offset: 0x00146AB3
	public void SetValue(int value)
	{
		this.logicValue = value;
		this.onValueChanged(value);
	}

	// Token: 0x06003B3A RID: 15162 RVA: 0x001488C8 File Offset: 0x00146AC8
	public void LogicTick()
	{
	}

	// Token: 0x06003B3B RID: 15163 RVA: 0x001488CA File Offset: 0x00146ACA
	public void OnLogicNetworkConnectionChanged(bool connected)
	{
		if (this.onConnectionChanged != null)
		{
			this.onConnectionChanged(this.cell, connected);
		}
	}

	// Token: 0x040026B1 RID: 9905
	private HashedString id;

	// Token: 0x040026B2 RID: 9906
	private int cell;

	// Token: 0x040026B3 RID: 9907
	private int logicValue;

	// Token: 0x040026B4 RID: 9908
	private Action<int> onValueChanged;

	// Token: 0x040026B5 RID: 9909
	private Action<int, bool> onConnectionChanged;

	// Token: 0x040026B6 RID: 9910
	private LogicPortSpriteType spriteType;
}
