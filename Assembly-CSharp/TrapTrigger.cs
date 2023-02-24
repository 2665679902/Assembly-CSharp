using System;
using UnityEngine;

// Token: 0x020009AE RID: 2478
public class TrapTrigger : KMonoBehaviour
{
	// Token: 0x06004990 RID: 18832 RVA: 0x0019C1D0 File Offset: 0x0019A3D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameObject gameObject = base.gameObject;
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Trap", gameObject, Grid.PosToCell(gameObject), GameScenePartitioner.Instance.trapsLayer, new Action<object>(this.OnCreatureOnTrap));
		foreach (GameObject gameObject2 in this.storage.items)
		{
			this.SetStoredPosition(gameObject2);
			KBoxCollider2D component = gameObject2.GetComponent<KBoxCollider2D>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
	}

	// Token: 0x06004991 RID: 18833 RVA: 0x0019C280 File Offset: 0x0019A480
	private void SetStoredPosition(GameObject go)
	{
		Vector3 vector = Grid.CellToPosCBC(Grid.PosToCell(base.transform.GetPosition()), Grid.SceneLayer.BuildingBack);
		vector.x += this.trappedOffset.x;
		vector.y += this.trappedOffset.y;
		go.transform.SetPosition(vector);
		go.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.BuildingBack);
	}

	// Token: 0x06004992 RID: 18834 RVA: 0x0019C2EC File Offset: 0x0019A4EC
	public void OnCreatureOnTrap(object data)
	{
		if (!this.storage.IsEmpty())
		{
			return;
		}
		Trappable trappable = (Trappable)data;
		if (trappable.HasTag(GameTags.Stored))
		{
			return;
		}
		if (trappable.HasTag(GameTags.Trapped))
		{
			return;
		}
		if (trappable.HasTag(GameTags.Creatures.Bagged))
		{
			return;
		}
		bool flag = false;
		foreach (Tag tag in this.trappableCreatures)
		{
			if (trappable.HasTag(tag))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		this.storage.Store(trappable.gameObject, true, false, true, false);
		this.SetStoredPosition(trappable.gameObject);
		base.Trigger(-358342870, trappable.gameObject);
	}

	// Token: 0x06004993 RID: 18835 RVA: 0x0019C39D File Offset: 0x0019A59D
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x0400305D RID: 12381
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400305E RID: 12382
	public Tag[] trappableCreatures;

	// Token: 0x0400305F RID: 12383
	public Vector2 trappedOffset = Vector2.zero;

	// Token: 0x04003060 RID: 12384
	[MyCmpReq]
	private Storage storage;
}
