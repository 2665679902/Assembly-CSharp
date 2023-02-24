using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200064C RID: 1612
public class StickerBomb : StateMachineComponent<StickerBomb.StatesInstance>
{
	// Token: 0x06002AE2 RID: 10978 RVA: 0x000E2690 File Offset: 0x000E0890
	protected override void OnSpawn()
	{
		if (this.stickerName.IsNullOrWhiteSpace())
		{
			global::Debug.LogError("Missing sticker db entry for " + this.stickerType);
		}
		else
		{
			DbStickerBomb dbStickerBomb = Db.GetStickerBombs().Get(this.stickerName);
			base.GetComponent<KBatchedAnimController>().SwapAnims(new KAnimFile[] { dbStickerBomb.animFile });
		}
		this.cellOffsets = StickerBomb.BuildCellOffsets(base.transform.GetPosition());
		base.smi.destroyTime = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.STICKER_BOMBER.STICKER_DURATION;
		base.smi.StartSM();
		Extents extents = base.GetComponent<OccupyArea>().GetExtents();
		Extents extents2 = new Extents(extents.x - 1, extents.y - 1, extents.width + 2, extents.height + 2);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("StickerBomb.OnSpawn", base.gameObject, extents2, GameScenePartitioner.Instance.objectLayers[2], new Action<object>(this.OnFoundationCellChanged));
		base.OnSpawn();
	}

	// Token: 0x06002AE3 RID: 10979 RVA: 0x000E2798 File Offset: 0x000E0998
	[OnDeserialized]
	public void OnDeserialized()
	{
		if (this.stickerName.IsNullOrWhiteSpace() && !this.stickerType.IsNullOrWhiteSpace())
		{
			string[] array = this.stickerType.Split(new char[] { '_' });
			if (array.Length == 2)
			{
				this.stickerName = array[1];
			}
		}
	}

	// Token: 0x06002AE4 RID: 10980 RVA: 0x000E27E5 File Offset: 0x000E09E5
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002AE5 RID: 10981 RVA: 0x000E27FD File Offset: 0x000E09FD
	private void OnFoundationCellChanged(object data)
	{
		if (!StickerBomb.CanPlaceSticker(this.cellOffsets))
		{
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06002AE6 RID: 10982 RVA: 0x000E2818 File Offset: 0x000E0A18
	public static List<int> BuildCellOffsets(Vector3 position)
	{
		List<int> list = new List<int>();
		bool flag = position.x % 1f < 0.5f;
		bool flag2 = position.y % 1f > 0.5f;
		int num = Grid.PosToCell(position);
		list.Add(num);
		if (flag)
		{
			list.Add(Grid.CellLeft(num));
			if (flag2)
			{
				list.Add(Grid.CellAbove(num));
				list.Add(Grid.CellUpLeft(num));
			}
			else
			{
				list.Add(Grid.CellBelow(num));
				list.Add(Grid.CellDownLeft(num));
			}
		}
		else
		{
			list.Add(Grid.CellRight(num));
			if (flag2)
			{
				list.Add(Grid.CellAbove(num));
				list.Add(Grid.CellUpRight(num));
			}
			else
			{
				list.Add(Grid.CellBelow(num));
				list.Add(Grid.CellDownRight(num));
			}
		}
		return list;
	}

	// Token: 0x06002AE7 RID: 10983 RVA: 0x000E28E8 File Offset: 0x000E0AE8
	public static bool CanPlaceSticker(List<int> offsets)
	{
		using (List<int>.Enumerator enumerator = offsets.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Grid.IsCellOpenToSpace(enumerator.Current))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06002AE8 RID: 10984 RVA: 0x000E293C File Offset: 0x000E0B3C
	public void SetStickerType(string newStickerType)
	{
		if (newStickerType == null)
		{
			newStickerType = "sticker";
		}
		DbStickerBomb randomSticker = Db.GetStickerBombs().GetRandomSticker();
		this.stickerName = randomSticker.Id;
		this.stickerType = string.Format("{0}_{1}", newStickerType, randomSticker.Name);
		base.GetComponent<KBatchedAnimController>().SwapAnims(new KAnimFile[] { randomSticker.animFile });
	}

	// Token: 0x0400197D RID: 6525
	[Serialize]
	public string stickerType;

	// Token: 0x0400197E RID: 6526
	[Serialize]
	public string stickerName;

	// Token: 0x0400197F RID: 6527
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001980 RID: 6528
	private List<int> cellOffsets;

	// Token: 0x020012F7 RID: 4855
	public class StatesInstance : GameStateMachine<StickerBomb.States, StickerBomb.StatesInstance, StickerBomb, object>.GameInstance
	{
		// Token: 0x06007C0F RID: 31759 RVA: 0x002CE66B File Offset: 0x002CC86B
		public StatesInstance(StickerBomb master)
			: base(master)
		{
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x002CE674 File Offset: 0x002CC874
		public string GetStickerAnim(string type)
		{
			return string.Format("{0}_{1}", type, base.master.stickerType);
		}

		// Token: 0x04005F26 RID: 24358
		[Serialize]
		public float destroyTime;
	}

	// Token: 0x020012F8 RID: 4856
	public class States : GameStateMachine<StickerBomb.States, StickerBomb.StatesInstance, StickerBomb>
	{
		// Token: 0x06007C11 RID: 31761 RVA: 0x002CE68C File Offset: 0x002CC88C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Transition(this.destroy, (StickerBomb.StatesInstance smi) => GameClock.Instance.GetTime() >= smi.destroyTime, UpdateRate.SIM_200ms).DefaultState(this.idle);
			this.idle.PlayAnim((StickerBomb.StatesInstance smi) => smi.GetStickerAnim("idle"), KAnim.PlayMode.Once).ScheduleGoTo((StickerBomb.StatesInstance smi) => (float)UnityEngine.Random.Range(20, 30), this.sparkle);
			this.sparkle.PlayAnim((StickerBomb.StatesInstance smi) => smi.GetStickerAnim("sparkle"), KAnim.PlayMode.Once).OnAnimQueueComplete(this.idle);
			this.destroy.Enter(delegate(StickerBomb.StatesInstance smi)
			{
				Util.KDestroyGameObject(smi.master);
			});
		}

		// Token: 0x04005F27 RID: 24359
		public GameStateMachine<StickerBomb.States, StickerBomb.StatesInstance, StickerBomb, object>.State destroy;

		// Token: 0x04005F28 RID: 24360
		public GameStateMachine<StickerBomb.States, StickerBomb.StatesInstance, StickerBomb, object>.State sparkle;

		// Token: 0x04005F29 RID: 24361
		public GameStateMachine<StickerBomb.States, StickerBomb.StatesInstance, StickerBomb, object>.State idle;
	}
}
