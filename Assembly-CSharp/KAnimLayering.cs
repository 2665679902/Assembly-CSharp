using System;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class KAnimLayering
{
	// Token: 0x06001621 RID: 5665 RVA: 0x0007204A File Offset: 0x0007024A
	public KAnimLayering(KAnimControllerBase controller, Grid.SceneLayer layer)
	{
		this.controller = controller;
		this.layer = layer;
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x00072068 File Offset: 0x00070268
	public void SetLayer(Grid.SceneLayer layer)
	{
		this.layer = layer;
		if (this.foregroundController != null)
		{
			Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(layer) - this.controller.gameObject.transform.GetPosition().z - 0.1f);
			this.foregroundController.transform.SetLocalPosition(vector);
		}
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000720D4 File Offset: 0x000702D4
	public void SetIsForeground(bool is_foreground)
	{
		this.isForeground = is_foreground;
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000720DD File Offset: 0x000702DD
	public bool GetIsForeground()
	{
		return this.isForeground;
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x000720E5 File Offset: 0x000702E5
	public KAnimLink GetLink()
	{
		return this.link;
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000720F0 File Offset: 0x000702F0
	private static bool IsAnimLayered(KAnimFile[] anims)
	{
		foreach (KAnimFile kanimFile in anims)
		{
			if (!(kanimFile == null))
			{
				KAnimFileData data = kanimFile.GetData();
				if (data.build != null)
				{
					KAnim.Build.Symbol[] symbols = data.build.symbols;
					for (int j = 0; j < symbols.Length; j++)
					{
						if ((symbols[j].flags & 8) != 0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x00072158 File Offset: 0x00070358
	private void HideSymbolsInternal()
	{
		foreach (KAnimFile kanimFile in this.controller.AnimFiles)
		{
			if (!(kanimFile == null))
			{
				KAnimFileData data = kanimFile.GetData();
				if (data.build != null)
				{
					KAnim.Build.Symbol[] symbols = data.build.symbols;
					for (int j = 0; j < symbols.Length; j++)
					{
						if ((symbols[j].flags & 8) != 0 != this.isForeground && !(symbols[j].hash == KAnimLayering.UI))
						{
							this.controller.SetSymbolVisiblity(symbols[j].hash, false);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x00072204 File Offset: 0x00070404
	public void HideSymbols()
	{
		if (EntityPrefabs.Instance == null)
		{
			return;
		}
		if (this.isForeground)
		{
			return;
		}
		KAnimFile[] animFiles = this.controller.AnimFiles;
		bool flag = KAnimLayering.IsAnimLayered(animFiles);
		if (flag && this.layer != Grid.SceneLayer.NoLayer)
		{
			bool flag2 = this.foregroundController == null;
			if (flag2)
			{
				GameObject gameObject = Util.KInstantiate(EntityPrefabs.Instance.ForegroundLayer, this.controller.gameObject, null);
				gameObject.name = this.controller.name + "_fg";
				this.foregroundController = gameObject.GetComponent<KAnimControllerBase>();
				this.link = new KAnimLink(this.controller, this.foregroundController);
			}
			this.foregroundController.AnimFiles = animFiles;
			this.foregroundController.GetLayering().SetIsForeground(true);
			this.foregroundController.initialAnim = this.controller.initialAnim;
			this.Dirty();
			KAnimSynchronizer synchronizer = this.controller.GetSynchronizer();
			if (flag2)
			{
				synchronizer.Add(this.foregroundController);
			}
			else
			{
				this.foregroundController.GetComponent<KBatchedAnimController>().SwapAnims(this.foregroundController.AnimFiles);
			}
			synchronizer.Sync(this.foregroundController);
			Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(this.layer) - this.controller.gameObject.transform.GetPosition().z - 0.1f);
			this.foregroundController.gameObject.transform.SetLocalPosition(vector);
			this.foregroundController.gameObject.SetActive(true);
		}
		else if (!flag && this.foregroundController != null)
		{
			this.controller.GetSynchronizer().Remove(this.foregroundController);
			this.foregroundController.gameObject.DeleteObject();
			this.link = null;
		}
		if (this.foregroundController != null)
		{
			this.HideSymbolsInternal();
			KAnimLayering layering = this.foregroundController.GetLayering();
			if (layering != null)
			{
				layering.HideSymbolsInternal();
			}
		}
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x00072407 File Offset: 0x00070607
	public void RefreshForegroundBatchGroup()
	{
		if (this.foregroundController == null)
		{
			return;
		}
		this.foregroundController.GetComponent<KBatchedAnimController>().SwapAnims(this.foregroundController.AnimFiles);
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x00072434 File Offset: 0x00070634
	public void Dirty()
	{
		if (this.foregroundController == null)
		{
			return;
		}
		this.foregroundController.Offset = this.controller.Offset;
		this.foregroundController.Pivot = this.controller.Pivot;
		this.foregroundController.Rotation = this.controller.Rotation;
		this.foregroundController.FlipX = this.controller.FlipX;
		this.foregroundController.FlipY = this.controller.FlipY;
	}

	// Token: 0x04000C4E RID: 3150
	private bool isForeground;

	// Token: 0x04000C4F RID: 3151
	private KAnimControllerBase controller;

	// Token: 0x04000C50 RID: 3152
	private KAnimControllerBase foregroundController;

	// Token: 0x04000C51 RID: 3153
	private KAnimLink link;

	// Token: 0x04000C52 RID: 3154
	private Grid.SceneLayer layer = Grid.SceneLayer.BuildingFront;

	// Token: 0x04000C53 RID: 3155
	public static readonly KAnimHashedString UI = new KAnimHashedString("ui");
}
