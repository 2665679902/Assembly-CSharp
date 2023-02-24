using System;
using UnityEngine;

// Token: 0x020007A0 RID: 1952
[AddComponentMenu("KMonoBehaviour/scripts/GridVisibility")]
public class GridVisibility : KMonoBehaviour
{
	// Token: 0x0600370F RID: 14095 RVA: 0x00132AC0 File Offset: 0x00130CC0
	protected override void OnSpawn()
	{
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "GridVisibility.OnSpawn");
		this.OnCellChange();
		base.gameObject.GetMyWorld().SetDiscovered(false);
	}

	// Token: 0x06003710 RID: 14096 RVA: 0x00132AFC File Offset: 0x00130CFC
	private void OnCellChange()
	{
		if (base.gameObject.HasTag(GameTags.Dead))
		{
			return;
		}
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		if (!Grid.Revealed[num])
		{
			int num2;
			int num3;
			Grid.PosToXY(base.transform.GetPosition(), out num2, out num3);
			GridVisibility.Reveal(num2, num3, this.radius, this.innerRadius);
			Grid.Revealed[num] = true;
		}
		FogOfWarMask.ClearMask(num);
	}

	// Token: 0x06003711 RID: 14097 RVA: 0x00132B74 File Offset: 0x00130D74
	public static void Reveal(int baseX, int baseY, int radius, float innerRadius)
	{
		int num = (int)Grid.WorldIdx[baseY * Grid.WidthInCells + baseX];
		for (int i = -radius; i <= radius; i++)
		{
			for (int j = -radius; j <= radius; j++)
			{
				int num2 = baseY + i;
				int num3 = baseX + j;
				if (num2 >= 0 && Grid.HeightInCells - 1 >= num2 && num3 >= 0 && Grid.WidthInCells - 1 >= num3)
				{
					int num4 = num2 * Grid.WidthInCells + num3;
					if (Grid.Visible[num4] < 255 && num == (int)Grid.WorldIdx[num4])
					{
						Vector2 vector = new Vector2((float)j, (float)i);
						float num5 = Mathf.Lerp(1f, 0f, (vector.magnitude - innerRadius) / ((float)radius - innerRadius));
						Grid.Reveal(num4, (byte)(255f * num5), false);
					}
				}
			}
		}
	}

	// Token: 0x06003712 RID: 14098 RVA: 0x00132C3F File Offset: 0x00130E3F
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
	}

	// Token: 0x040024F4 RID: 9460
	public int radius = 18;

	// Token: 0x040024F5 RID: 9461
	public float innerRadius = 16.5f;
}
