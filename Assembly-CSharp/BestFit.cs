using System;
using System.Collections.Generic;
using ProcGen;
using TUNING;

// Token: 0x02000C36 RID: 3126
public class BestFit
{
	// Token: 0x060062E4 RID: 25316 RVA: 0x002486E0 File Offset: 0x002468E0
	public static Vector2I BestFitWorlds(List<WorldPlacement> worldsToArrange, bool ignoreBestFitY = false)
	{
		List<BestFit.Rect> list = new List<BestFit.Rect>();
		Vector2I vector2I = default(Vector2I);
		List<WorldPlacement> list2 = new List<WorldPlacement>(worldsToArrange);
		list2.Sort((WorldPlacement a, WorldPlacement b) => b.height.CompareTo(a.height));
		int height = list2[0].height;
		foreach (WorldPlacement worldPlacement in list2)
		{
			Vector2I vector2I2 = default(Vector2I);
			while (!BestFit.UnoccupiedSpace(new BestFit.Rect(vector2I2.x, vector2I2.y, worldPlacement.width, worldPlacement.height), list))
			{
				if (ignoreBestFitY)
				{
					vector2I2.x++;
				}
				else if (vector2I2.y + worldPlacement.height >= height + 32)
				{
					vector2I2.y = 0;
					vector2I2.x++;
				}
				else
				{
					vector2I2.y++;
				}
			}
			vector2I.x = Math.Max(worldPlacement.width + vector2I2.x, vector2I.x);
			vector2I.y = Math.Max(worldPlacement.height + vector2I2.y, vector2I.y);
			list.Add(new BestFit.Rect(vector2I2.x, vector2I2.y, worldPlacement.width, worldPlacement.height));
			worldPlacement.SetPosition(vector2I2);
		}
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			vector2I.x += 136;
			vector2I.y = Math.Max(vector2I.y, 136);
		}
		return vector2I;
	}

	// Token: 0x060062E5 RID: 25317 RVA: 0x0024889C File Offset: 0x00246A9C
	private static bool UnoccupiedSpace(BestFit.Rect RectA, List<BestFit.Rect> placed)
	{
		foreach (BestFit.Rect rect in placed)
		{
			if (RectA.X1 < rect.X2 && RectA.X2 > rect.X1 && RectA.Y1 < rect.Y2 && RectA.Y2 > rect.Y1)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060062E6 RID: 25318 RVA: 0x0024892C File Offset: 0x00246B2C
	public static Vector2I GetGridOffset(IList<WorldContainer> existingWorlds, Vector2I newWorldSize, out Vector2I newWorldOffset)
	{
		List<BestFit.Rect> list = new List<BestFit.Rect>();
		foreach (WorldContainer worldContainer in existingWorlds)
		{
			list.Add(new BestFit.Rect(worldContainer.WorldOffset.x, worldContainer.WorldOffset.y, worldContainer.WorldSize.x, worldContainer.WorldSize.y));
		}
		Vector2I vector2I = new Vector2I(Grid.WidthInCells, 0);
		int widthInCells = Grid.WidthInCells;
		Vector2I vector2I2 = default(Vector2I);
		while (!BestFit.UnoccupiedSpace(new BestFit.Rect(vector2I2.x, vector2I2.y, newWorldSize.x, newWorldSize.y), list))
		{
			if (vector2I2.x + newWorldSize.x >= widthInCells)
			{
				vector2I2.x = 0;
				vector2I2.y++;
			}
			else
			{
				vector2I2.x++;
			}
		}
		Debug.Assert(vector2I2.x + newWorldSize.x <= Grid.WidthInCells, "BestFit is trying to expand the grid width, this is unsupported and will break the SIM.");
		vector2I.y = Math.Max(newWorldSize.y + vector2I2.y, Grid.HeightInCells);
		newWorldOffset = vector2I2;
		return vector2I;
	}

	// Token: 0x060062E7 RID: 25319 RVA: 0x00248A70 File Offset: 0x00246C70
	public static int CountRocketInteriors(IList<WorldContainer> existingWorlds)
	{
		int num = 0;
		List<BestFit.Rect> list = new List<BestFit.Rect>();
		foreach (WorldContainer worldContainer in existingWorlds)
		{
			list.Add(new BestFit.Rect(worldContainer.WorldOffset.x, worldContainer.WorldOffset.y, worldContainer.WorldSize.x, worldContainer.WorldSize.y));
		}
		Vector2I rocket_INTERIOR_SIZE = ROCKETRY.ROCKET_INTERIOR_SIZE;
		Vector2I vector2I;
		while (BestFit.PlaceWorld(list, rocket_INTERIOR_SIZE, out vector2I))
		{
			num++;
			list.Add(new BestFit.Rect(vector2I.x, vector2I.y, rocket_INTERIOR_SIZE.x, rocket_INTERIOR_SIZE.y));
		}
		return num;
	}

	// Token: 0x060062E8 RID: 25320 RVA: 0x00248B38 File Offset: 0x00246D38
	private static bool PlaceWorld(List<BestFit.Rect> placedWorlds, Vector2I newWorldSize, out Vector2I newWorldOffset)
	{
		Vector2I vector2I = new Vector2I(Grid.WidthInCells, 0);
		int widthInCells = Grid.WidthInCells;
		Vector2I vector2I2 = default(Vector2I);
		while (!BestFit.UnoccupiedSpace(new BestFit.Rect(vector2I2.x, vector2I2.y, newWorldSize.x, newWorldSize.y), placedWorlds))
		{
			if (vector2I2.x + newWorldSize.x >= widthInCells)
			{
				vector2I2.x = 0;
				vector2I2.y++;
			}
			else
			{
				vector2I2.x++;
			}
		}
		vector2I.y = Math.Max(newWorldSize.y + vector2I2.y, Grid.HeightInCells);
		newWorldOffset = vector2I2;
		return vector2I2.x + newWorldSize.x <= Grid.WidthInCells && vector2I2.y + newWorldSize.y <= Grid.HeightInCells;
	}

	// Token: 0x02001AC4 RID: 6852
	private struct Rect
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600940E RID: 37902 RVA: 0x0031B387 File Offset: 0x00319587
		public int X1
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600940F RID: 37903 RVA: 0x0031B38F File Offset: 0x0031958F
		public int X2
		{
			get
			{
				return this.x + this.width + 2;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06009410 RID: 37904 RVA: 0x0031B3A0 File Offset: 0x003195A0
		public int Y1
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06009411 RID: 37905 RVA: 0x0031B3A8 File Offset: 0x003195A8
		public int Y2
		{
			get
			{
				return this.y + this.height + 2;
			}
		}

		// Token: 0x06009412 RID: 37906 RVA: 0x0031B3B9 File Offset: 0x003195B9
		public Rect(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		// Token: 0x040078AC RID: 30892
		private int x;

		// Token: 0x040078AD RID: 30893
		private int y;

		// Token: 0x040078AE RID: 30894
		private int width;

		// Token: 0x040078AF RID: 30895
		private int height;
	}
}
