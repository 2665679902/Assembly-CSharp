using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using TemplateClasses;
using UnityEngine;

// Token: 0x020006FF RID: 1791
[Serializable]
public class TemplateContainer
{
	// Token: 0x17000385 RID: 901
	// (get) Token: 0x060030FC RID: 12540 RVA: 0x00103FEC File Offset: 0x001021EC
	// (set) Token: 0x060030FD RID: 12541 RVA: 0x00103FF4 File Offset: 0x001021F4
	public string name { get; set; }

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x060030FE RID: 12542 RVA: 0x00103FFD File Offset: 0x001021FD
	// (set) Token: 0x060030FF RID: 12543 RVA: 0x00104005 File Offset: 0x00102205
	public int priority { get; set; }

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x06003100 RID: 12544 RVA: 0x0010400E File Offset: 0x0010220E
	// (set) Token: 0x06003101 RID: 12545 RVA: 0x00104016 File Offset: 0x00102216
	public TemplateContainer.Info info { get; set; }

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06003102 RID: 12546 RVA: 0x0010401F File Offset: 0x0010221F
	// (set) Token: 0x06003103 RID: 12547 RVA: 0x00104027 File Offset: 0x00102227
	public List<Cell> cells { get; set; }

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06003104 RID: 12548 RVA: 0x00104030 File Offset: 0x00102230
	// (set) Token: 0x06003105 RID: 12549 RVA: 0x00104038 File Offset: 0x00102238
	public List<Prefab> buildings { get; set; }

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06003106 RID: 12550 RVA: 0x00104041 File Offset: 0x00102241
	// (set) Token: 0x06003107 RID: 12551 RVA: 0x00104049 File Offset: 0x00102249
	public List<Prefab> pickupables { get; set; }

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06003108 RID: 12552 RVA: 0x00104052 File Offset: 0x00102252
	// (set) Token: 0x06003109 RID: 12553 RVA: 0x0010405A File Offset: 0x0010225A
	public List<Prefab> elementalOres { get; set; }

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x0600310A RID: 12554 RVA: 0x00104063 File Offset: 0x00102263
	// (set) Token: 0x0600310B RID: 12555 RVA: 0x0010406B File Offset: 0x0010226B
	public List<Prefab> otherEntities { get; set; }

	// Token: 0x0600310C RID: 12556 RVA: 0x00104074 File Offset: 0x00102274
	public void Init(List<Cell> _cells, List<Prefab> _buildings, List<Prefab> _pickupables, List<Prefab> _elementalOres, List<Prefab> _otherEntities)
	{
		if (_cells != null && _cells.Count > 0)
		{
			this.cells = _cells;
		}
		if (_buildings != null && _buildings.Count > 0)
		{
			this.buildings = _buildings;
		}
		if (_pickupables != null && _pickupables.Count > 0)
		{
			this.pickupables = _pickupables;
		}
		if (_elementalOres != null && _elementalOres.Count > 0)
		{
			this.elementalOres = _elementalOres;
		}
		if (_otherEntities != null && _otherEntities.Count > 0)
		{
			this.otherEntities = _otherEntities;
		}
		this.info = new TemplateContainer.Info();
		this.RefreshInfo();
	}

	// Token: 0x0600310D RID: 12557 RVA: 0x001040F7 File Offset: 0x001022F7
	public RectInt GetTemplateBounds(int padding = 0)
	{
		return this.GetTemplateBounds(Vector2I.zero, padding);
	}

	// Token: 0x0600310E RID: 12558 RVA: 0x00104105 File Offset: 0x00102305
	public RectInt GetTemplateBounds(Vector2 position, int padding = 0)
	{
		return this.GetTemplateBounds(new Vector2I((int)position.x, (int)position.y), padding);
	}

	// Token: 0x0600310F RID: 12559 RVA: 0x00104124 File Offset: 0x00102324
	public RectInt GetTemplateBounds(Vector2I position, int padding = 0)
	{
		if ((this.info.min - new Vector2f(0, 0)).sqrMagnitude <= 1E-06f)
		{
			this.RefreshInfo();
		}
		return this.info.GetBounds(position, padding);
	}

	// Token: 0x06003110 RID: 12560 RVA: 0x0010416C File Offset: 0x0010236C
	public void RefreshInfo()
	{
		if (this.cells == null)
		{
			return;
		}
		int num = 1;
		int num2 = -1;
		int num3 = 1;
		int num4 = -1;
		foreach (Cell cell in this.cells)
		{
			if (cell.location_x < num)
			{
				num = cell.location_x;
			}
			if (cell.location_x > num2)
			{
				num2 = cell.location_x;
			}
			if (cell.location_y < num3)
			{
				num3 = cell.location_y;
			}
			if (cell.location_y > num4)
			{
				num4 = cell.location_y;
			}
		}
		this.info.size = new Vector2((float)(1 + (num2 - num)), (float)(1 + (num4 - num3)));
		this.info.min = new Vector2((float)num, (float)num3);
		this.info.area = this.cells.Count;
	}

	// Token: 0x06003111 RID: 12561 RVA: 0x00104264 File Offset: 0x00102464
	public void SaveToYaml(string save_name)
	{
		string text = TemplateCache.RewriteTemplatePath(save_name);
		if (!Directory.Exists(Path.GetDirectoryName(text)))
		{
			Directory.CreateDirectory(Path.GetDirectoryName(text));
		}
		YamlIO.Save<TemplateContainer>(this, text + ".yaml", null);
	}

	// Token: 0x0200141C RID: 5148
	[Serializable]
	public class Info
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06008014 RID: 32788 RVA: 0x002DE6C1 File Offset: 0x002DC8C1
		// (set) Token: 0x06008015 RID: 32789 RVA: 0x002DE6C9 File Offset: 0x002DC8C9
		public Vector2f size { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06008016 RID: 32790 RVA: 0x002DE6D2 File Offset: 0x002DC8D2
		// (set) Token: 0x06008017 RID: 32791 RVA: 0x002DE6DA File Offset: 0x002DC8DA
		public Vector2f min { get; set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06008018 RID: 32792 RVA: 0x002DE6E3 File Offset: 0x002DC8E3
		// (set) Token: 0x06008019 RID: 32793 RVA: 0x002DE6EB File Offset: 0x002DC8EB
		public int area { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600801A RID: 32794 RVA: 0x002DE6F4 File Offset: 0x002DC8F4
		// (set) Token: 0x0600801B RID: 32795 RVA: 0x002DE6FC File Offset: 0x002DC8FC
		public Tag[] tags { get; set; }

		// Token: 0x0600801C RID: 32796 RVA: 0x002DE708 File Offset: 0x002DC908
		public RectInt GetBounds(Vector2I position, int padding)
		{
			return new RectInt(position.x + (int)this.min.x - padding, position.y + (int)this.min.y - padding, (int)this.size.x + padding * 2, (int)this.size.y + padding * 2);
		}
	}
}
