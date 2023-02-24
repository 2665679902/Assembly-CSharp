using System;
using UnityEngine;

// Token: 0x0200047D RID: 1149
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Facing")]
public class Facing : KMonoBehaviour
{
	// Token: 0x060019AB RID: 6571 RVA: 0x00089F0B File Offset: 0x0008810B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.log = new LoggerFS("Facing", 35);
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x00089F28 File Offset: 0x00088128
	public void Face(float target_x)
	{
		float x = base.transform.GetLocalPosition().x;
		if (target_x < x)
		{
			this.facingLeft = true;
			this.UpdateMirror();
			return;
		}
		if (target_x > x)
		{
			this.facingLeft = false;
			this.UpdateMirror();
		}
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x00089F6C File Offset: 0x0008816C
	public void Face(Vector3 target_pos)
	{
		int num = Grid.CellColumn(Grid.PosToCell(base.transform.GetLocalPosition()));
		int num2 = Grid.CellColumn(Grid.PosToCell(target_pos));
		if (num > num2)
		{
			this.facingLeft = true;
			this.UpdateMirror();
			return;
		}
		if (num2 > num)
		{
			this.facingLeft = false;
			this.UpdateMirror();
		}
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x00089FBE File Offset: 0x000881BE
	[ContextMenu("Flip")]
	public void SwapFacing()
	{
		this.facingLeft = !this.facingLeft;
		this.UpdateMirror();
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x00089FD5 File Offset: 0x000881D5
	private void UpdateMirror()
	{
		if (this.kanimController != null && this.kanimController.FlipX != this.facingLeft)
		{
			this.kanimController.FlipX = this.facingLeft;
			bool flag = this.facingLeft;
		}
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x0008A010 File Offset: 0x00088210
	public bool GetFacing()
	{
		return this.facingLeft;
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x0008A018 File Offset: 0x00088218
	public void SetFacing(bool mirror_x)
	{
		this.facingLeft = mirror_x;
		this.UpdateMirror();
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x0008A028 File Offset: 0x00088228
	public int GetFrontCell()
	{
		int num = Grid.PosToCell(this);
		if (this.GetFacing())
		{
			return Grid.CellLeft(num);
		}
		return Grid.CellRight(num);
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x0008A054 File Offset: 0x00088254
	public int GetBackCell()
	{
		int num = Grid.PosToCell(this);
		if (!this.GetFacing())
		{
			return Grid.CellLeft(num);
		}
		return Grid.CellRight(num);
	}

	// Token: 0x04000E5C RID: 3676
	[MyCmpGet]
	private KAnimControllerBase kanimController;

	// Token: 0x04000E5D RID: 3677
	private LoggerFS log;

	// Token: 0x04000E5E RID: 3678
	private bool facingLeft;
}
