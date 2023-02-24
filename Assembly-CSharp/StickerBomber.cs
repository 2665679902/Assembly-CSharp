using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public class StickerBomber : GameStateMachine<StickerBomber, StickerBomber.Instance>
{
	// Token: 0x060013B6 RID: 5046 RVA: 0x00068550 File Offset: 0x00066750
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false).Exit(delegate(StickerBomber.Instance smi)
		{
			smi.nextStickerBomb = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.STICKER_BOMBER.TIME_PER_STICKER_BOMB;
		});
		this.overjoyed.TagTransition(GameTags.Overjoyed, this.neutral, true).DefaultState(this.overjoyed.idle);
		this.overjoyed.idle.Transition(this.overjoyed.place_stickers, (StickerBomber.Instance smi) => GameClock.Instance.GetTime() >= smi.nextStickerBomb, UpdateRate.SIM_200ms);
		this.overjoyed.place_stickers.Exit(delegate(StickerBomber.Instance smi)
		{
			smi.nextStickerBomb = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.STICKER_BOMBER.TIME_PER_STICKER_BOMB;
		}).ToggleReactable((StickerBomber.Instance smi) => smi.CreateReactable()).OnSignal(this.doneStickerBomb, this.overjoyed.idle);
	}

	// Token: 0x04000AB5 RID: 2741
	public StateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.Signal doneStickerBomb;

	// Token: 0x04000AB6 RID: 2742
	public GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000AB7 RID: 2743
	public StickerBomber.OverjoyedStates overjoyed;

	// Token: 0x02000FDF RID: 4063
	public class OverjoyedStates : GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040055AF RID: 21935
		public GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x040055B0 RID: 21936
		public GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State place_stickers;
	}

	// Token: 0x02000FE0 RID: 4064
	public new class Instance : GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060070C6 RID: 28870 RVA: 0x002A7827 File Offset: 0x002A5A27
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x002A7830 File Offset: 0x002A5A30
		public Reactable CreateReactable()
		{
			return new StickerBomber.Instance.StickerBombReactable(base.master.gameObject, base.smi);
		}

		// Token: 0x040055B1 RID: 21937
		[Serialize]
		public float nextStickerBomb;

		// Token: 0x02001EDE RID: 7902
		private class StickerBombReactable : Reactable
		{
			// Token: 0x06009D2F RID: 40239 RVA: 0x0033B8A0 File Offset: 0x00339AA0
			public StickerBombReactable(GameObject gameObject, StickerBomber.Instance stickerBomber)
				: base(gameObject, "StickerBombReactable", Db.Get().ChoreTypes.Build, 2, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
			{
				this.preventChoreInterruption = true;
				this.stickerBomber = stickerBomber;
			}

			// Token: 0x06009D30 RID: 40240 RVA: 0x0033B980 File Offset: 0x00339B80
			public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
			{
				if (this.reactor != null)
				{
					return false;
				}
				if (new_reactor == null)
				{
					return false;
				}
				if (this.gameObject != new_reactor)
				{
					return false;
				}
				Navigator component = new_reactor.GetComponent<Navigator>();
				return !(component == null) && component.CurrentNavType != NavType.Tube && component.CurrentNavType != NavType.Ladder && component.CurrentNavType != NavType.Pole;
			}

			// Token: 0x06009D31 RID: 40241 RVA: 0x0033B9E8 File Offset: 0x00339BE8
			protected override void InternalBegin()
			{
				this.stickersToPlace = UnityEngine.Random.Range(4, 6);
				this.STICKER_PLACE_TIMER = this.TIME_PER_STICKER_PLACED;
				this.placementCell = this.FindPlacementCell();
				if (this.placementCell == 0)
				{
					base.End();
					return;
				}
				this.kbac = this.reactor.GetComponent<KBatchedAnimController>();
				this.kbac.AddAnimOverrides(this.animset, 0f);
				this.kbac.Play(this.pre_anim, KAnim.PlayMode.Once, 1f, 0f);
				this.kbac.Queue(this.loop_anim, KAnim.PlayMode.Loop, 1f, 0f);
			}

			// Token: 0x06009D32 RID: 40242 RVA: 0x0033BA88 File Offset: 0x00339C88
			public override void Update(float dt)
			{
				this.STICKER_PLACE_TIMER -= dt;
				if (this.STICKER_PLACE_TIMER <= 0f)
				{
					this.PlaceSticker();
					this.STICKER_PLACE_TIMER = this.TIME_PER_STICKER_PLACED;
				}
				if (this.stickersPlaced >= this.stickersToPlace)
				{
					this.kbac.Play(this.pst_anim, KAnim.PlayMode.Once, 1f, 0f);
					base.End();
				}
			}

			// Token: 0x06009D33 RID: 40243 RVA: 0x0033BAF4 File Offset: 0x00339CF4
			protected override void InternalEnd()
			{
				if (this.kbac != null)
				{
					this.kbac.RemoveAnimOverrides(this.animset);
					this.kbac = null;
				}
				this.stickerBomber.sm.doneStickerBomb.Trigger(this.stickerBomber);
				this.stickersPlaced = 0;
			}

			// Token: 0x06009D34 RID: 40244 RVA: 0x0033BB4C File Offset: 0x00339D4C
			private int FindPlacementCell()
			{
				int num = Grid.PosToCell(this.reactor.transform.GetPosition() + Vector3.up);
				ListPool<int, PathFinder>.PooledList pooledList = ListPool<int, PathFinder>.Allocate();
				ListPool<int, PathFinder>.PooledList pooledList2 = ListPool<int, PathFinder>.Allocate();
				QueuePool<GameUtil.FloodFillInfo, Comet>.PooledQueue pooledQueue = QueuePool<GameUtil.FloodFillInfo, Comet>.Allocate();
				pooledQueue.Enqueue(new GameUtil.FloodFillInfo
				{
					cell = num,
					depth = 0
				});
				GameUtil.FloodFillConditional(pooledQueue, this.canPlaceStickerCb, pooledList, pooledList2, 2);
				if (pooledList2.Count > 0)
				{
					int random = pooledList2.GetRandom<int>();
					pooledList.Recycle();
					pooledList2.Recycle();
					pooledQueue.Recycle();
					return random;
				}
				pooledList.Recycle();
				pooledList2.Recycle();
				pooledQueue.Recycle();
				return 0;
			}

			// Token: 0x06009D35 RID: 40245 RVA: 0x0033BBF0 File Offset: 0x00339DF0
			private void PlaceSticker()
			{
				this.stickersPlaced++;
				Vector3 vector = Grid.CellToPos(this.placementCell);
				int i = 10;
				while (i > 0)
				{
					i--;
					Vector3 vector2 = vector + new Vector3(UnityEngine.Random.Range(-this.tile_random_range, this.tile_random_range), UnityEngine.Random.Range(-this.tile_random_range, this.tile_random_range), -2.5f);
					if (StickerBomb.CanPlaceSticker(StickerBomb.BuildCellOffsets(vector2)))
					{
						GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("StickerBomb".ToTag()), vector2, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-this.tile_random_rotation, this.tile_random_rotation)), null, null, true, 0);
						StickerBomb component = gameObject.GetComponent<StickerBomb>();
						string stickerType = this.reactor.GetComponent<MinionIdentity>().stickerType;
						component.SetStickerType(stickerType);
						gameObject.SetActive(true);
						i = 0;
					}
				}
			}

			// Token: 0x06009D36 RID: 40246 RVA: 0x0033BCCB File Offset: 0x00339ECB
			protected override void InternalCleanup()
			{
			}

			// Token: 0x04008A46 RID: 35398
			private int stickersToPlace;

			// Token: 0x04008A47 RID: 35399
			private int stickersPlaced;

			// Token: 0x04008A48 RID: 35400
			private int placementCell;

			// Token: 0x04008A49 RID: 35401
			private float tile_random_range = 1f;

			// Token: 0x04008A4A RID: 35402
			private float tile_random_rotation = 90f;

			// Token: 0x04008A4B RID: 35403
			private float TIME_PER_STICKER_PLACED = 0.66f;

			// Token: 0x04008A4C RID: 35404
			private float STICKER_PLACE_TIMER;

			// Token: 0x04008A4D RID: 35405
			private KBatchedAnimController kbac;

			// Token: 0x04008A4E RID: 35406
			private KAnimFile animset = Assets.GetAnim("anim_stickers_kanim");

			// Token: 0x04008A4F RID: 35407
			private HashedString pre_anim = "working_pre";

			// Token: 0x04008A50 RID: 35408
			private HashedString loop_anim = "working_loop";

			// Token: 0x04008A51 RID: 35409
			private HashedString pst_anim = "working_pst";

			// Token: 0x04008A52 RID: 35410
			private StickerBomber.Instance stickerBomber;

			// Token: 0x04008A53 RID: 35411
			private Func<int, bool> canPlaceStickerCb = (int cell) => !Grid.Solid[cell] && (!Grid.IsValidCell(Grid.CellLeft(cell)) || !Grid.Solid[Grid.CellLeft(cell)]) && (!Grid.IsValidCell(Grid.CellRight(cell)) || !Grid.Solid[Grid.CellRight(cell)]) && (!Grid.IsValidCell(Grid.OffsetCell(cell, 0, 1)) || !Grid.Solid[Grid.OffsetCell(cell, 0, 1)]) && (!Grid.IsValidCell(Grid.OffsetCell(cell, 0, -1)) || !Grid.Solid[Grid.OffsetCell(cell, 0, -1)]) && !Grid.IsCellOpenToSpace(cell);
		}
	}
}
