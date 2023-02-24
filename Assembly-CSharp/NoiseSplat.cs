using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public class NoiseSplat : IUniformGridObject
{
	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x0600176F RID: 5999 RVA: 0x0007A492 File Offset: 0x00078692
	// (set) Token: 0x06001770 RID: 6000 RVA: 0x0007A49A File Offset: 0x0007869A
	public int dB { get; private set; }

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06001771 RID: 6001 RVA: 0x0007A4A3 File Offset: 0x000786A3
	// (set) Token: 0x06001772 RID: 6002 RVA: 0x0007A4AB File Offset: 0x000786AB
	public float deathTime { get; private set; }

	// Token: 0x06001773 RID: 6003 RVA: 0x0007A4B4 File Offset: 0x000786B4
	public string GetName()
	{
		return this.provider.GetName();
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x0007A4C1 File Offset: 0x000786C1
	public IPolluter GetProvider()
	{
		return this.provider;
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x0007A4C9 File Offset: 0x000786C9
	public Vector2 PosMin()
	{
		return new Vector2(this.position.x - (float)this.radius, this.position.y - (float)this.radius);
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x0007A4F6 File Offset: 0x000786F6
	public Vector2 PosMax()
	{
		return new Vector2(this.position.x + (float)this.radius, this.position.y + (float)this.radius);
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x0007A524 File Offset: 0x00078724
	public NoiseSplat(NoisePolluter setProvider, float death_time = 0f)
	{
		this.deathTime = death_time;
		this.dB = 0;
		this.radius = 5;
		if (setProvider.dB != null)
		{
			this.dB = (int)setProvider.dB.GetTotalValue();
		}
		int num = Grid.PosToCell(setProvider.gameObject);
		if (!NoisePolluter.IsNoiseableCell(num))
		{
			this.dB = 0;
		}
		if (this.dB == 0)
		{
			return;
		}
		setProvider.Clear();
		OccupyArea occupyArea = setProvider.occupyArea;
		this.baseExtents = occupyArea.GetExtents();
		this.provider = setProvider;
		this.position = setProvider.transform.GetPosition();
		if (setProvider.dBRadius != null)
		{
			this.radius = (int)setProvider.dBRadius.GetTotalValue();
		}
		if (this.radius == 0)
		{
			return;
		}
		int num2 = 0;
		int num3 = 0;
		Grid.CellToXY(num, out num2, out num3);
		int widthInCells = occupyArea.GetWidthInCells();
		int heightInCells = occupyArea.GetHeightInCells();
		Vector2I vector2I = new Vector2I(num2 - this.radius, num3 - this.radius);
		Vector2I vector2I2 = vector2I + new Vector2I(this.radius * 2 + widthInCells, this.radius * 2 + heightInCells);
		vector2I = Vector2I.Max(vector2I, Vector2I.zero);
		vector2I2 = Vector2I.Min(vector2I2, new Vector2I(Grid.WidthInCells - 1, Grid.HeightInCells - 1));
		this.effectExtents = new Extents(vector2I.x, vector2I.y, vector2I2.x - vector2I.x, vector2I2.y - vector2I.y);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("NoiseSplat.SplatCollectNoisePolluters", setProvider.gameObject, this.effectExtents, GameScenePartitioner.Instance.noisePolluterLayer, setProvider.onCollectNoisePollutersCallback);
		this.solidChangedPartitionerEntry = GameScenePartitioner.Instance.Add("NoiseSplat.SplatSolidCheck", setProvider.gameObject, this.effectExtents, GameScenePartitioner.Instance.solidChangedLayer, setProvider.refreshPartionerCallback);
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x0007A70C File Offset: 0x0007890C
	public NoiseSplat(IPolluter setProvider, float death_time = 0f)
	{
		this.deathTime = death_time;
		this.provider = setProvider;
		this.provider.Clear();
		this.position = this.provider.GetPosition();
		this.dB = this.provider.GetNoise();
		int num = Grid.PosToCell(this.position);
		if (!NoisePolluter.IsNoiseableCell(num))
		{
			this.dB = 0;
		}
		if (this.dB == 0)
		{
			return;
		}
		this.radius = this.provider.GetRadius();
		if (this.radius == 0)
		{
			return;
		}
		int num2 = 0;
		int num3 = 0;
		Grid.CellToXY(num, out num2, out num3);
		Vector2I vector2I = new Vector2I(num2 - this.radius, num3 - this.radius);
		Vector2I vector2I2 = vector2I + new Vector2I(this.radius * 2, this.radius * 2);
		vector2I = Vector2I.Max(vector2I, Vector2I.zero);
		vector2I2 = Vector2I.Min(vector2I2, new Vector2I(Grid.WidthInCells - 1, Grid.HeightInCells - 1));
		this.effectExtents = new Extents(vector2I.x, vector2I.y, vector2I2.x - vector2I.x, vector2I2.y - vector2I.y);
		this.baseExtents = new Extents(num2, num3, 1, 1);
		this.AddNoise();
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x0007A855 File Offset: 0x00078A55
	public void Clear()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
		this.RemoveNoise();
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x0007A880 File Offset: 0x00078A80
	private void AddNoise()
	{
		int num = Grid.PosToCell(this.position);
		int num2 = this.effectExtents.x + this.effectExtents.width;
		int num3 = this.effectExtents.y + this.effectExtents.height;
		int num4 = this.effectExtents.x;
		int num5 = this.effectExtents.y;
		int num6 = 0;
		int num7 = 0;
		Grid.CellToXY(num, out num6, out num7);
		num2 = Math.Min(num2, Grid.WidthInCells);
		num3 = Math.Min(num3, Grid.HeightInCells);
		num4 = Math.Max(0, num4);
		num5 = Math.Max(0, num5);
		for (int i = num5; i < num3; i++)
		{
			for (int j = num4; j < num2; j++)
			{
				if (Grid.VisibilityTest(num6, num7, j, i, false))
				{
					int num8 = Grid.XYToCell(j, i);
					float dbforCell = this.GetDBForCell(num8);
					if (dbforCell > 0f)
					{
						float num9 = AudioEventManager.DBToLoudness(dbforCell);
						Grid.Loudness[num8] += num9;
						Pair<int, float> pair = new Pair<int, float>(num8, num9);
						this.decibels.Add(pair);
					}
				}
			}
		}
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x0007A998 File Offset: 0x00078B98
	public float GetDBForCell(int cell)
	{
		Vector2 vector = Grid.CellToPos2D(cell);
		float num = Mathf.Floor(Vector2.Distance(this.position, vector));
		if (vector.x >= (float)this.baseExtents.x && vector.x < (float)(this.baseExtents.x + this.baseExtents.width) && vector.y >= (float)this.baseExtents.y && vector.y < (float)(this.baseExtents.y + this.baseExtents.height))
		{
			num = 0f;
		}
		return Mathf.Round((float)this.dB - (float)this.dB * num * 0.05f);
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x0007AA50 File Offset: 0x00078C50
	private void RemoveNoise()
	{
		for (int i = 0; i < this.decibels.Count; i++)
		{
			Pair<int, float> pair = this.decibels[i];
			float num = Math.Max(0f, Grid.Loudness[pair.first] - pair.second);
			Grid.Loudness[pair.first] = ((num < 1f) ? 0f : num);
		}
		this.decibels.Clear();
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x0007AAC8 File Offset: 0x00078CC8
	public float GetLoudness(int cell)
	{
		float num = 0f;
		for (int i = 0; i < this.decibels.Count; i++)
		{
			Pair<int, float> pair = this.decibels[i];
			if (pair.first == cell)
			{
				num = pair.second;
				break;
			}
		}
		return num;
	}

	// Token: 0x04000CFF RID: 3327
	public const float noiseFalloff = 0.05f;

	// Token: 0x04000D02 RID: 3330
	private IPolluter provider;

	// Token: 0x04000D03 RID: 3331
	private Vector2 position;

	// Token: 0x04000D04 RID: 3332
	private int radius;

	// Token: 0x04000D05 RID: 3333
	private Extents effectExtents;

	// Token: 0x04000D06 RID: 3334
	private Extents baseExtents;

	// Token: 0x04000D07 RID: 3335
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000D08 RID: 3336
	private HandleVector<int>.Handle solidChangedPartitionerEntry;

	// Token: 0x04000D09 RID: 3337
	private List<Pair<int, float>> decibels = new List<Pair<int, float>>();
}
