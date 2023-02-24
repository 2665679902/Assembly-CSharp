using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200053B RID: 1339
public class MultitoolController : GameStateMachine<MultitoolController, MultitoolController.Instance, Worker>
{
	// Token: 0x06002009 RID: 8201 RVA: 0x000AEF10 File Offset: 0x000AD110
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre;
		base.Target(this.worker);
		this.root.ToggleSnapOn("dig");
		this.pre.Enter(delegate(MultitoolController.Instance smi)
		{
			smi.PlayPre();
			this.worker.Get<Facing>(smi).Face(smi.workable.transform.GetPosition());
		}).OnAnimQueueComplete(this.loop);
		this.loop.Enter("PlayLoop", delegate(MultitoolController.Instance smi)
		{
			smi.PlayLoop();
		}).Enter("CreateHitEffect", delegate(MultitoolController.Instance smi)
		{
			smi.CreateHitEffect();
		}).Exit("DestroyHitEffect", delegate(MultitoolController.Instance smi)
		{
			smi.DestroyHitEffect();
		})
			.EventTransition(GameHashes.WorkerPlayPostAnim, this.pst, (MultitoolController.Instance smi) => smi.GetComponent<Worker>().state == Worker.State.PendingCompletion);
		this.pst.Enter("PlayPost", delegate(MultitoolController.Instance smi)
		{
			smi.PlayPost();
		});
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x000AF048 File Offset: 0x000AD248
	public static string[] GetAnimationStrings(Workable workable, Worker worker, string toolString = "dig")
	{
		global::Debug.Assert(toolString != "build");
		string[][][] array;
		if (!MultitoolController.TOOL_ANIM_SETS.TryGetValue(toolString, out array))
		{
			array = new string[MultitoolController.ANIM_BASE.Length][][];
			MultitoolController.TOOL_ANIM_SETS[toolString] = array;
			for (int i = 0; i < array.Length; i++)
			{
				string[][] array2 = MultitoolController.ANIM_BASE[i];
				string[][] array3 = new string[array2.Length][];
				array[i] = array3;
				for (int j = 0; j < array3.Length; j++)
				{
					string[] array4 = array2[j];
					string[] array5 = new string[array4.Length];
					array3[j] = array5;
					for (int k = 0; k < array5.Length; k++)
					{
						array5[k] = array4[k].Replace("{verb}", toolString);
					}
				}
			}
		}
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		MultitoolController.GetTargetPoints(workable, worker, out zero2, out zero);
		Vector2 normalized = new Vector2(zero.x - zero2.x, zero.y - zero2.y).normalized;
		float num = Vector2.Angle(new Vector2(0f, -1f), normalized);
		float num2 = Mathf.Lerp(0f, 1f, num / 180f);
		int num3 = array.Length;
		int num4 = (int)(num2 * (float)num3);
		num4 = Math.Min(num4, num3 - 1);
		NavType currentNavType = worker.GetComponent<Navigator>().CurrentNavType;
		int num5 = 0;
		if (currentNavType == NavType.Ladder)
		{
			num5 = 1;
		}
		else if (currentNavType == NavType.Pole)
		{
			num5 = 2;
		}
		else if (currentNavType == NavType.Hover)
		{
			num5 = 3;
		}
		return array[num4][num5];
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x000AF1CB File Offset: 0x000AD3CB
	private static void GetTargetPoints(Workable workable, Worker worker, out Vector3 source, out Vector3 target)
	{
		target = workable.GetTargetPoint();
		source = worker.transform.GetPosition();
		source.y += 0.7f;
	}

	// Token: 0x04001252 RID: 4690
	public GameStateMachine<MultitoolController, MultitoolController.Instance, Worker, object>.State pre;

	// Token: 0x04001253 RID: 4691
	public GameStateMachine<MultitoolController, MultitoolController.Instance, Worker, object>.State loop;

	// Token: 0x04001254 RID: 4692
	public GameStateMachine<MultitoolController, MultitoolController.Instance, Worker, object>.State pst;

	// Token: 0x04001255 RID: 4693
	public StateMachine<MultitoolController, MultitoolController.Instance, Worker, object>.TargetParameter worker;

	// Token: 0x04001256 RID: 4694
	private static readonly string[][][] ANIM_BASE = new string[][][]
	{
		new string[][]
		{
			new string[] { "{verb}_dn_pre", "{verb}_dn_loop", "{verb}_dn_pst" },
			new string[] { "ladder_{verb}_dn_pre", "ladder_{verb}_dn_loop", "ladder_{verb}_dn_pst" },
			new string[] { "pole_{verb}_dn_pre", "pole_{verb}_dn_loop", "pole_{verb}_dn_pst" },
			new string[] { "jetpack_{verb}_dn_pre", "jetpack_{verb}_dn_loop", "jetpack_{verb}_dn_pst" }
		},
		new string[][]
		{
			new string[] { "{verb}_diag_dn_pre", "{verb}_diag_dn_loop", "{verb}_diag_dn_pst" },
			new string[] { "ladder_{verb}_diag_dn_pre", "ladder_{verb}_loop_diag_dn", "ladder_{verb}_diag_dn_pst" },
			new string[] { "pole_{verb}_diag_dn_pre", "pole_{verb}_loop_diag_dn", "pole_{verb}_diag_dn_pst" },
			new string[] { "jetpack_{verb}_diag_dn_pre", "jetpack_{verb}_diag_dn_loop", "jetpack_{verb}_diag_dn_pst" }
		},
		new string[][]
		{
			new string[] { "{verb}_fwd_pre", "{verb}_fwd_loop", "{verb}_fwd_pst" },
			new string[] { "ladder_{verb}_pre", "ladder_{verb}_loop", "ladder_{verb}_pst" },
			new string[] { "pole_{verb}_pre", "pole_{verb}_loop", "pole_{verb}_pst" },
			new string[] { "jetpack_{verb}_fwd_pre", "jetpack_{verb}_fwd_loop", "jetpack_{verb}_fwd_pst" }
		},
		new string[][]
		{
			new string[] { "{verb}_diag_up_pre", "{verb}_diag_up_loop", "{verb}_diag_up_pst" },
			new string[] { "ladder_{verb}_diag_up_pre", "ladder_{verb}_loop_diag_up", "ladder_{verb}_diag_up_pst" },
			new string[] { "pole_{verb}_diag_up_pre", "pole_{verb}_loop_diag_up", "pole_{verb}_diag_up_pst" },
			new string[] { "jetpack_{verb}_diag_up_pre", "jetpack_{verb}_diag_up_loop", "jetpack_{verb}_diag_up_pst" }
		},
		new string[][]
		{
			new string[] { "{verb}_up_pre", "{verb}_up_loop", "{verb}_up_pst" },
			new string[] { "ladder_{verb}_up_pre", "ladder_{verb}_up_loop", "ladder_{verb}_up_pst" },
			new string[] { "pole_{verb}_up_pre", "pole_{verb}_up_loop", "pole_{verb}_up_pst" },
			new string[] { "jetpack_{verb}_up_pre", "jetpack_{verb}_up_loop", "jetpack_{verb}_up_pst" }
		}
	};

	// Token: 0x04001257 RID: 4695
	private static Dictionary<string, string[][][]> TOOL_ANIM_SETS = new Dictionary<string, string[][][]>();

	// Token: 0x0200116A RID: 4458
	public new class Instance : GameStateMachine<MultitoolController, MultitoolController.Instance, Worker, object>.GameInstance
	{
		// Token: 0x0600767E RID: 30334 RVA: 0x002B7ED0 File Offset: 0x002B60D0
		public Instance(Workable workable, Worker worker, HashedString context, GameObject hit_effect)
			: base(worker)
		{
			this.hitEffectPrefab = hit_effect;
			worker.GetComponent<AnimEventHandler>().SetContext(context);
			base.sm.worker.Set(worker, base.smi);
			this.workable = workable;
			this.anims = MultitoolController.GetAnimationStrings(workable, worker, "dig");
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x002B7F28 File Offset: 0x002B6128
		public void PlayPre()
		{
			base.sm.worker.Get<KAnimControllerBase>(base.smi).Play(this.anims[0], KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x002B7F60 File Offset: 0x002B6160
		public void PlayLoop()
		{
			if (base.sm.worker.Get<KAnimControllerBase>(base.smi).currentAnim != this.anims[1])
			{
				base.sm.worker.Get<KAnimControllerBase>(base.smi).Play(this.anims[1], KAnim.PlayMode.Loop, 1f, 0f);
			}
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x002B7FD0 File Offset: 0x002B61D0
		public void PlayPost()
		{
			if (base.sm.worker.Get<KAnimControllerBase>(base.smi).currentAnim != this.anims[2])
			{
				base.sm.worker.Get<KAnimControllerBase>(base.smi).Play(this.anims[2], KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x06007682 RID: 30338 RVA: 0x002B8040 File Offset: 0x002B6240
		public void UpdateHitEffectTarget()
		{
			if (this.hitEffect == null)
			{
				return;
			}
			Worker worker = base.sm.worker.Get<Worker>(base.smi);
			AnimEventHandler component = worker.GetComponent<AnimEventHandler>();
			Vector3 targetPoint = this.workable.GetTargetPoint();
			worker.GetComponent<Facing>().Face(this.workable.transform.GetPosition());
			this.anims = MultitoolController.GetAnimationStrings(this.workable, worker, "dig");
			this.PlayLoop();
			component.SetTargetPos(targetPoint);
			component.UpdateWorkTarget(this.workable.GetTargetPoint());
			this.hitEffect.transform.SetPosition(targetPoint);
		}

		// Token: 0x06007683 RID: 30339 RVA: 0x002B80E8 File Offset: 0x002B62E8
		public void CreateHitEffect()
		{
			Worker worker = base.sm.worker.Get<Worker>(base.smi);
			if (worker == null || this.workable == null)
			{
				return;
			}
			if (Grid.PosToCell(this.workable) != Grid.PosToCell(worker))
			{
				worker.Trigger(-673283254, null);
			}
			Diggable diggable = this.workable as Diggable;
			if (diggable)
			{
				Element targetElement = diggable.GetTargetElement();
				worker.Trigger(-1762453998, targetElement);
			}
			if (this.hitEffectPrefab == null)
			{
				return;
			}
			if (this.hitEffect != null)
			{
				this.DestroyHitEffect();
			}
			AnimEventHandler component = worker.GetComponent<AnimEventHandler>();
			Vector3 targetPoint = this.workable.GetTargetPoint();
			component.SetTargetPos(targetPoint);
			this.hitEffect = GameUtil.KInstantiate(this.hitEffectPrefab, targetPoint, Grid.SceneLayer.FXFront2, null, 0);
			KBatchedAnimController component2 = this.hitEffect.GetComponent<KBatchedAnimController>();
			this.hitEffect.SetActive(true);
			component2.sceneLayer = Grid.SceneLayer.FXFront2;
			component2.enabled = false;
			component2.enabled = true;
			component.UpdateWorkTarget(this.workable.GetTargetPoint());
		}

		// Token: 0x06007684 RID: 30340 RVA: 0x002B81F8 File Offset: 0x002B63F8
		public void DestroyHitEffect()
		{
			Worker worker = base.sm.worker.Get<Worker>(base.smi);
			if (worker != null)
			{
				worker.Trigger(-1559999068, null);
				worker.Trigger(939543986, null);
			}
			if (this.hitEffectPrefab == null)
			{
				return;
			}
			if (this.hitEffect == null)
			{
				return;
			}
			this.hitEffect.DeleteObject();
		}

		// Token: 0x04005AC2 RID: 23234
		public Workable workable;

		// Token: 0x04005AC3 RID: 23235
		private GameObject hitEffectPrefab;

		// Token: 0x04005AC4 RID: 23236
		private GameObject hitEffect;

		// Token: 0x04005AC5 RID: 23237
		private string[] anims;

		// Token: 0x04005AC6 RID: 23238
		private bool inPlace;
	}

	// Token: 0x0200116B RID: 4459
	private enum DigDirection
	{
		// Token: 0x04005AC8 RID: 23240
		dig_down,
		// Token: 0x04005AC9 RID: 23241
		dig_up
	}
}
