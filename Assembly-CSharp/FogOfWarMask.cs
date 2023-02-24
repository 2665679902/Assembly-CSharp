using System;
using UnityEngine;

// Token: 0x0200077D RID: 1917
[AddComponentMenu("KMonoBehaviour/scripts/FogOfWarMask")]
public class FogOfWarMask : KMonoBehaviour
{
	// Token: 0x060034C3 RID: 13507 RVA: 0x0011CA5D File Offset: 0x0011AC5D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Grid.OnReveal = (Action<int>)Delegate.Combine(Grid.OnReveal, new Action<int>(this.OnReveal));
	}

	// Token: 0x060034C4 RID: 13508 RVA: 0x0011CA85 File Offset: 0x0011AC85
	private void OnReveal(int cell)
	{
		if (Grid.PosToCell(this) == cell)
		{
			Grid.OnReveal = (Action<int>)Delegate.Remove(Grid.OnReveal, new Action<int>(this.OnReveal));
			base.gameObject.DeleteObject();
		}
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x0011CABC File Offset: 0x0011ACBC
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		GameUtil.FloodCollectCells(Grid.PosToCell(this), delegate(int cell)
		{
			Grid.Visible[cell] = 0;
			Grid.PreventFogOfWarReveal[cell] = true;
			return !Grid.Solid[cell];
		}, 300, null, true);
		GameUtil.FloodCollectCells(Grid.PosToCell(this), delegate(int cell)
		{
			bool flag = Grid.PreventFogOfWarReveal[cell];
			if (Grid.Solid[cell] && Grid.Foundation[cell])
			{
				Grid.PreventFogOfWarReveal[cell] = true;
				Grid.Visible[cell] = 0;
				GameObject gameObject = Grid.Objects[cell, 1];
				if (gameObject != null && gameObject.GetComponent<KPrefabID>().PrefabTag.ToString() == "POIBunkerExteriorDoor")
				{
					Grid.PreventFogOfWarReveal[cell] = false;
					Grid.Visible[cell] = byte.MaxValue;
				}
			}
			return flag || Grid.Foundation[cell];
		}, 300, null, true);
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x0011CB33 File Offset: 0x0011AD33
	public static void ClearMask(int cell)
	{
		if (Grid.PreventFogOfWarReveal[cell])
		{
			GameUtil.FloodCollectCells(cell, new Func<int, bool>(FogOfWarMask.RevealFogOfWarMask), 300, null, true);
		}
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x0011CB5C File Offset: 0x0011AD5C
	public static bool RevealFogOfWarMask(int cell)
	{
		bool flag = Grid.PreventFogOfWarReveal[cell];
		if (flag)
		{
			Grid.PreventFogOfWarReveal[cell] = false;
			Grid.Reveal(cell, byte.MaxValue, false);
		}
		return flag;
	}
}
