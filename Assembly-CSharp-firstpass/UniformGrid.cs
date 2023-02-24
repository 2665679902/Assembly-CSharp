using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class UniformGrid<T> where T : IUniformGridObject
{
	// Token: 0x0600094E RID: 2382 RVA: 0x00024EA5 File Offset: 0x000230A5
	public UniformGrid()
	{
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00024EAD File Offset: 0x000230AD
	public UniformGrid(int width, int height, int cellWidth, int cellHeight)
	{
		this.Reset(width, height, cellWidth, cellHeight);
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00024EC0 File Offset: 0x000230C0
	public void Reset(int width, int height, int cellWidth, int cellHeight)
	{
		this.cellWidth = cellWidth;
		this.cellHeight = cellHeight;
		this.numXCells = (int)Math.Ceiling((double)((float)width / (float)cellWidth));
		this.numYCells = (int)Math.Ceiling((double)((float)height / (float)cellHeight));
		int num = this.numXCells * this.numYCells;
		this.cells = new List<T>[num];
		this.items = new List<T>();
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00024F26 File Offset: 0x00023126
	public void Clear()
	{
		this.cellWidth = 0;
		this.cellHeight = 0;
		this.numXCells = 0;
		this.numYCells = 0;
		this.cells = null;
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00024F4C File Offset: 0x0002314C
	public void Add(T item)
	{
		ref Vector2 ptr = item.PosMin();
		Vector2 vector = item.PosMax();
		int num = (int)Math.Max(ptr.x / (float)this.cellWidth, 0f);
		int num2 = (int)Math.Max(vector.y / (float)this.cellHeight, 0f);
		int num3 = Math.Min(this.numXCells - 1, (int)Math.Ceiling((double)(vector.x / (float)this.cellWidth)));
		int num4 = Math.Min(this.numYCells - 1, (int)Math.Ceiling((double)(vector.y / (float)this.cellHeight)));
		for (int i = num2; i <= num4; i++)
		{
			for (int j = num; j <= num3; j++)
			{
				int num5 = i * this.numXCells + j;
				List<T> list = this.cells[num5];
				if (list == null)
				{
					list = new List<T>();
					this.cells[num5] = list;
				}
				list.Add(item);
				this.items.Add(item);
			}
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00025050 File Offset: 0x00023250
	public void Remove(T item)
	{
		ref Vector2 ptr = item.PosMin();
		Vector2 vector = item.PosMax();
		int num = (int)Math.Max(ptr.x / (float)this.cellWidth, 0f);
		int num2 = (int)Math.Max(vector.y / (float)this.cellHeight, 0f);
		int num3 = Math.Min(this.numXCells - 1, (int)Math.Ceiling((double)(vector.x / (float)this.cellWidth)));
		int num4 = Math.Min(this.numYCells - 1, (int)Math.Ceiling((double)(vector.y / (float)this.cellHeight)));
		for (int i = num2; i <= num4; i++)
		{
			for (int j = num; j <= num3; j++)
			{
				List<T> list = this.cells[i * this.numXCells + j];
				if (list != null && list.IndexOf(item) != -1)
				{
					list.Remove(item);
					this.items.Remove(item);
				}
			}
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0002514C File Offset: 0x0002334C
	public IEnumerable GetAllIntersecting(IUniformGridObject item)
	{
		Vector2 vector = item.PosMin();
		Vector2 vector2 = item.PosMax();
		return this.GetAllIntersecting(vector, vector2);
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0002516F File Offset: 0x0002336F
	public IEnumerable GetAllIntersecting(Vector2 pos)
	{
		return this.GetAllIntersecting(pos, pos);
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0002517C File Offset: 0x0002337C
	public IEnumerable GetAllIntersecting(Vector2 min, Vector2 max)
	{
		HashSet<T> hashSet = new HashSet<T>();
		this.GetAllIntersecting(min, max, hashSet);
		return hashSet;
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0002519C File Offset: 0x0002339C
	public void GetAllIntersecting(Vector2 min, Vector2 max, ICollection<T> results)
	{
		int num = Math.Max(0, Math.Min((int)(min.x / (float)this.cellWidth), this.numXCells - 1));
		int num2 = Math.Max(0, Math.Min((int)Math.Ceiling((double)(max.x / (float)this.cellWidth)), this.numXCells - 1));
		int num3 = Math.Max(0, Math.Min((int)(min.y / (float)this.cellHeight), this.numYCells - 1));
		int num4 = Math.Max(0, Math.Min((int)Math.Ceiling((double)(max.y / (float)this.cellHeight)), this.numYCells - 1));
		for (int i = num3; i <= num4; i++)
		{
			for (int j = num; j <= num2; j++)
			{
				List<T> list = this.cells[i * this.numXCells + j];
				if (list != null)
				{
					for (int k = 0; k < list.Count; k++)
					{
						results.Add(list[k]);
					}
				}
			}
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0002529A File Offset: 0x0002349A
	public ICollection<T> GetAllItems()
	{
		return this.items;
	}

	// Token: 0x04000690 RID: 1680
	private List<T>[] cells;

	// Token: 0x04000691 RID: 1681
	private List<T> items;

	// Token: 0x04000692 RID: 1682
	private int cellWidth;

	// Token: 0x04000693 RID: 1683
	private int cellHeight;

	// Token: 0x04000694 RID: 1684
	private int numXCells;

	// Token: 0x04000695 RID: 1685
	private int numYCells;
}
