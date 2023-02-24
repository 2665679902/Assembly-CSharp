using System;
using UnityEngine;

// Token: 0x02000877 RID: 2167
public class SocialChoreTracker
{
	// Token: 0x06003E49 RID: 15945 RVA: 0x0015C640 File Offset: 0x0015A840
	public SocialChoreTracker(GameObject owner, CellOffset[] chore_offsets)
	{
		this.owner = owner;
		this.choreOffsets = chore_offsets;
		this.chores = new Chore[this.choreOffsets.Length];
		Extents extents = new Extents(Grid.PosToCell(owner), this.choreOffsets);
		this.validNavCellChangedPartitionerEntry = GameScenePartitioner.Instance.Add("PrintingPodSocialize", owner, extents, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnCellChanged));
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x0015C6B4 File Offset: 0x0015A8B4
	public void Update(bool update = true)
	{
		if (this.updating)
		{
			return;
		}
		this.updating = true;
		int num = 0;
		for (int i = 0; i < this.choreOffsets.Length; i++)
		{
			CellOffset cellOffset = this.choreOffsets[i];
			Chore chore = this.chores[i];
			if (update && num < this.choreCount && this.IsOffsetValid(cellOffset))
			{
				num++;
				if (chore == null || chore.isComplete)
				{
					this.chores[i] = ((this.CreateChoreCB != null) ? this.CreateChoreCB(i) : null);
				}
			}
			else if (chore != null)
			{
				chore.Cancel("locator invalidated");
				this.chores[i] = null;
			}
		}
		this.updating = false;
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x0015C765 File Offset: 0x0015A965
	private void OnCellChanged(object data)
	{
		if (this.owner.HasTag(GameTags.Operational))
		{
			this.Update(true);
		}
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x0015C780 File Offset: 0x0015A980
	public void Clear()
	{
		GameScenePartitioner.Instance.Free(ref this.validNavCellChangedPartitionerEntry);
		this.Update(false);
	}

	// Token: 0x06003E4D RID: 15949 RVA: 0x0015C79C File Offset: 0x0015A99C
	private bool IsOffsetValid(CellOffset offset)
	{
		int num = Grid.OffsetCell(Grid.PosToCell(this.owner), offset);
		int num2 = Grid.CellBelow(num);
		return GameNavGrids.FloorValidator.IsWalkableCell(num, num2, true);
	}

	// Token: 0x040028C4 RID: 10436
	public Func<int, Chore> CreateChoreCB;

	// Token: 0x040028C5 RID: 10437
	public int choreCount;

	// Token: 0x040028C6 RID: 10438
	private GameObject owner;

	// Token: 0x040028C7 RID: 10439
	private CellOffset[] choreOffsets;

	// Token: 0x040028C8 RID: 10440
	private Chore[] chores;

	// Token: 0x040028C9 RID: 10441
	private HandleVector<int>.Handle validNavCellChangedPartitionerEntry;

	// Token: 0x040028CA RID: 10442
	private bool updating;
}
