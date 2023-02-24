using System;
using FMODUnity;
using KSerialization;
using UnityEngine;

// Token: 0x02000578 RID: 1400
[SerializationConfig(MemberSerialization.OptIn)]
public class AutoMiner : StateMachineComponent<AutoMiner.Instance>, ISim1000ms
{
	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000B8279 File Offset: 0x000B6479
	private bool HasDigCell
	{
		get
		{
			return this.dig_cell != Grid.InvalidCell;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000B828B File Offset: 0x000B648B
	private bool RotationComplete
	{
		get
		{
			return this.HasDigCell && this.rotation_complete;
		}
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x000B829D File Offset: 0x000B649D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.simRenderLoadBalance = true;
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x000B82AC File Offset: 0x000B64AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.hitEffectPrefab = Assets.GetPrefab("fx_dig_splash");
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		string text = component.name + ".gun";
		this.arm_go = new GameObject(text);
		this.arm_go.SetActive(false);
		this.arm_go.transform.parent = component.transform;
		this.looping_sounds = this.arm_go.AddComponent<LoopingSounds>();
		string sound = GlobalAssets.GetSound(this.rotateSoundName, false);
		this.rotateSound = RuntimeManager.PathToEventReference(sound);
		this.arm_go.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		this.arm_anim_ctrl = this.arm_go.AddComponent<KBatchedAnimController>();
		this.arm_anim_ctrl.AnimFiles = new KAnimFile[] { component.AnimFiles[0] };
		this.arm_anim_ctrl.initialAnim = "gun";
		this.arm_anim_ctrl.isMovable = true;
		this.arm_anim_ctrl.sceneLayer = Grid.SceneLayer.TransferArm;
		component.SetSymbolVisiblity("gun_target", false);
		bool flag;
		Vector3 vector = component.GetSymbolTransform(new HashedString("gun_target"), out flag).GetColumn(3);
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
		this.arm_go.transform.SetPosition(vector);
		this.arm_go.SetActive(true);
		this.link = new KAnimLink(component, this.arm_anim_ctrl);
		base.Subscribe<AutoMiner>(-592767678, AutoMiner.OnOperationalChangedDelegate);
		this.RotateArm(this.rotatable.GetRotatedOffset(Quaternion.Euler(0f, 0f, -45f) * Vector3.up), true, 0f);
		this.StopDig();
		base.smi.StartSM();
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x000B847A File Offset: 0x000B667A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x060021DB RID: 8667 RVA: 0x000B8482 File Offset: 0x000B6682
	public void Sim1000ms(float dt)
	{
		if (!this.operational.IsOperational)
		{
			return;
		}
		this.RefreshDiggableCell();
		this.operational.SetActive(this.HasDigCell, false);
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x000B84AA File Offset: 0x000B66AA
	private void OnOperationalChanged(object data)
	{
		if (!(bool)data)
		{
			this.dig_cell = Grid.InvalidCell;
			this.rotation_complete = false;
		}
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x000B84C8 File Offset: 0x000B66C8
	public void UpdateRotation(float dt)
	{
		if (this.HasDigCell)
		{
			Vector3 vector = Grid.CellToPosCCC(this.dig_cell, Grid.SceneLayer.TileMain);
			vector.z = 0f;
			Vector3 position = this.arm_go.transform.GetPosition();
			position.z = 0f;
			Vector3 vector2 = Vector3.Normalize(vector - position);
			this.RotateArm(vector2, false, dt);
		}
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x000B852A File Offset: 0x000B672A
	private Element GetTargetElement()
	{
		if (this.HasDigCell)
		{
			return Grid.Element[this.dig_cell];
		}
		return null;
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x000B8544 File Offset: 0x000B6744
	public void StartDig()
	{
		Element targetElement = this.GetTargetElement();
		base.Trigger(-1762453998, targetElement);
		this.CreateHitEffect();
		this.arm_anim_ctrl.Play("gun_digging", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x000B858A File Offset: 0x000B678A
	public void StopDig()
	{
		base.Trigger(939543986, null);
		this.DestroyHitEffect();
		this.arm_anim_ctrl.Play("gun", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x000B85C0 File Offset: 0x000B67C0
	public void UpdateDig(float dt)
	{
		if (!this.HasDigCell)
		{
			return;
		}
		if (!this.rotation_complete)
		{
			return;
		}
		Diggable.DoDigTick(this.dig_cell, dt);
		float num = Grid.Damage[this.dig_cell];
		this.mining_sounds.SetPercentComplete(num);
		Vector3 vector = Grid.CellToPosCCC(this.dig_cell, Grid.SceneLayer.FXFront2);
		vector.z = 0f;
		Vector3 position = this.arm_go.transform.GetPosition();
		position.z = 0f;
		float sqrMagnitude = (vector - position).sqrMagnitude;
		this.arm_anim_ctrl.GetBatchInstanceData().SetClipRadius(position.x, position.y, sqrMagnitude, true);
		if (!AutoMiner.ValidDigCell(this.dig_cell))
		{
			this.dig_cell = Grid.InvalidCell;
			this.rotation_complete = false;
		}
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x000B868C File Offset: 0x000B688C
	private void CreateHitEffect()
	{
		if (this.hitEffectPrefab == null)
		{
			return;
		}
		if (this.hitEffect != null)
		{
			this.DestroyHitEffect();
		}
		Vector3 vector = Grid.CellToPosCCC(this.dig_cell, Grid.SceneLayer.FXFront2);
		this.hitEffect = GameUtil.KInstantiate(this.hitEffectPrefab, vector, Grid.SceneLayer.FXFront2, null, 0);
		this.hitEffect.SetActive(true);
		KBatchedAnimController component = this.hitEffect.GetComponent<KBatchedAnimController>();
		component.sceneLayer = Grid.SceneLayer.FXFront2;
		component.initialMode = KAnim.PlayMode.Loop;
		component.enabled = false;
		component.enabled = true;
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x000B8713 File Offset: 0x000B6913
	private void DestroyHitEffect()
	{
		if (this.hitEffectPrefab == null)
		{
			return;
		}
		if (this.hitEffect != null)
		{
			this.hitEffect.DeleteObject();
			this.hitEffect = null;
		}
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x000B8744 File Offset: 0x000B6944
	private void RefreshDiggableCell()
	{
		CellOffset rotatedCellOffset = this.vision_offset;
		if (this.rotatable)
		{
			rotatedCellOffset = this.rotatable.GetRotatedCellOffset(this.vision_offset);
		}
		int num = Grid.PosToCell(base.transform.gameObject);
		int num2 = Grid.OffsetCell(num, rotatedCellOffset);
		int num3;
		int num4;
		Grid.CellToXY(num2, out num3, out num4);
		float num5 = float.MaxValue;
		int num6 = Grid.InvalidCell;
		Vector3 vector = Grid.CellToPos(num2);
		bool flag = false;
		for (int i = 0; i < this.height; i++)
		{
			for (int j = 0; j < this.width; j++)
			{
				CellOffset rotatedCellOffset2 = new CellOffset(this.x + j, this.y + i);
				if (this.rotatable)
				{
					rotatedCellOffset2 = this.rotatable.GetRotatedCellOffset(rotatedCellOffset2);
				}
				int num7 = Grid.OffsetCell(num, rotatedCellOffset2);
				if (Grid.IsValidCell(num7))
				{
					int num8;
					int num9;
					Grid.CellToXY(num7, out num8, out num9);
					if (Grid.IsValidCell(num7) && AutoMiner.ValidDigCell(num7) && Grid.TestLineOfSight(num3, num4, num8, num9, new Func<int, bool>(AutoMiner.DigBlockingCB), false))
					{
						if (num7 == this.dig_cell)
						{
							flag = true;
						}
						Vector3 vector2 = Grid.CellToPos(num7);
						float num10 = Vector3.Distance(vector, vector2);
						if (num10 < num5)
						{
							num5 = num10;
							num6 = num7;
						}
					}
				}
			}
		}
		if (!flag && this.dig_cell != num6)
		{
			this.dig_cell = num6;
			this.rotation_complete = false;
		}
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x000B88B2 File Offset: 0x000B6AB2
	private static bool ValidDigCell(int cell)
	{
		return Grid.Solid[cell] && !Grid.Foundation[cell] && Grid.Element[cell].hardness < 150;
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x000B88E8 File Offset: 0x000B6AE8
	public static bool DigBlockingCB(int cell)
	{
		return Grid.Foundation[cell] || Grid.Element[cell].hardness >= 150;
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x000B8910 File Offset: 0x000B6B10
	private void RotateArm(Vector3 target_dir, bool warp, float dt)
	{
		if (this.rotation_complete)
		{
			return;
		}
		float num = MathUtil.AngleSigned(Vector3.up, target_dir, Vector3.forward) - this.arm_rot;
		num = MathUtil.Wrap(-180f, 180f, num);
		this.rotation_complete = Mathf.Approximately(num, 0f);
		float num2 = num;
		if (warp)
		{
			this.rotation_complete = true;
		}
		else
		{
			num2 = Mathf.Clamp(num2, -this.turn_rate * dt, this.turn_rate * dt);
		}
		this.arm_rot += num2;
		this.arm_rot = MathUtil.Wrap(-180f, 180f, this.arm_rot);
		this.arm_go.transform.rotation = Quaternion.Euler(0f, 0f, this.arm_rot);
		if (!this.rotation_complete)
		{
			this.StartRotateSound();
			this.looping_sounds.SetParameter(this.rotateSound, AutoMiner.HASH_ROTATION, this.arm_rot);
			return;
		}
		this.StopRotateSound();
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x000B8A05 File Offset: 0x000B6C05
	private void StartRotateSound()
	{
		if (!this.rotate_sound_playing)
		{
			this.looping_sounds.StartSound(this.rotateSound);
			this.rotate_sound_playing = true;
		}
	}

	// Token: 0x060021E9 RID: 8681 RVA: 0x000B8A28 File Offset: 0x000B6C28
	private void StopRotateSound()
	{
		if (this.rotate_sound_playing)
		{
			this.looping_sounds.StopSound(this.rotateSound);
			this.rotate_sound_playing = false;
		}
	}

	// Token: 0x04001380 RID: 4992
	private static HashedString HASH_ROTATION = "rotation";

	// Token: 0x04001381 RID: 4993
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001382 RID: 4994
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001383 RID: 4995
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001384 RID: 4996
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001385 RID: 4997
	[MyCmpReq]
	private MiningSounds mining_sounds;

	// Token: 0x04001386 RID: 4998
	public int x;

	// Token: 0x04001387 RID: 4999
	public int y;

	// Token: 0x04001388 RID: 5000
	public int width;

	// Token: 0x04001389 RID: 5001
	public int height;

	// Token: 0x0400138A RID: 5002
	public CellOffset vision_offset;

	// Token: 0x0400138B RID: 5003
	private KBatchedAnimController arm_anim_ctrl;

	// Token: 0x0400138C RID: 5004
	private GameObject arm_go;

	// Token: 0x0400138D RID: 5005
	private LoopingSounds looping_sounds;

	// Token: 0x0400138E RID: 5006
	private string rotateSoundName = "AutoMiner_rotate";

	// Token: 0x0400138F RID: 5007
	private EventReference rotateSound;

	// Token: 0x04001390 RID: 5008
	private KAnimLink link;

	// Token: 0x04001391 RID: 5009
	private float arm_rot = 45f;

	// Token: 0x04001392 RID: 5010
	private float turn_rate = 180f;

	// Token: 0x04001393 RID: 5011
	private bool rotation_complete;

	// Token: 0x04001394 RID: 5012
	private bool rotate_sound_playing;

	// Token: 0x04001395 RID: 5013
	private GameObject hitEffectPrefab;

	// Token: 0x04001396 RID: 5014
	private GameObject hitEffect;

	// Token: 0x04001397 RID: 5015
	private int dig_cell = Grid.InvalidCell;

	// Token: 0x04001398 RID: 5016
	private static readonly EventSystem.IntraObjectHandler<AutoMiner> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<AutoMiner>(delegate(AutoMiner component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x0200119F RID: 4511
	public class Instance : GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.GameInstance
	{
		// Token: 0x06007746 RID: 30534 RVA: 0x002BAFF8 File Offset: 0x002B91F8
		public Instance(AutoMiner master)
			: base(master)
		{
		}
	}

	// Token: 0x020011A0 RID: 4512
	public class States : GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner>
	{
		// Token: 0x06007747 RID: 30535 RVA: 0x002BB004 File Offset: 0x002B9204
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (AutoMiner.Instance smi) => smi.GetComponent<Operational>().IsOperational);
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (AutoMiner.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.on.idle.PlayAnim("on").EventTransition(GameHashes.ActiveChanged, this.on.moving, (AutoMiner.Instance smi) => smi.GetComponent<Operational>().IsActive);
			this.on.moving.Exit(delegate(AutoMiner.Instance smi)
			{
				smi.master.StopRotateSound();
			}).PlayAnim("working").EventTransition(GameHashes.ActiveChanged, this.on.idle, (AutoMiner.Instance smi) => !smi.GetComponent<Operational>().IsActive)
				.Update(delegate(AutoMiner.Instance smi, float dt)
				{
					smi.master.UpdateRotation(dt);
				}, UpdateRate.SIM_33ms, false)
				.Transition(this.on.digging, new StateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.Transition.ConditionCallback(AutoMiner.States.RotationComplete), UpdateRate.SIM_200ms);
			this.on.digging.Enter(delegate(AutoMiner.Instance smi)
			{
				smi.master.StartDig();
			}).Exit(delegate(AutoMiner.Instance smi)
			{
				smi.master.StopDig();
			}).PlayAnim("working")
				.EventTransition(GameHashes.ActiveChanged, this.on.idle, (AutoMiner.Instance smi) => !smi.GetComponent<Operational>().IsActive)
				.Update(delegate(AutoMiner.Instance smi, float dt)
				{
					smi.master.UpdateDig(dt);
				}, UpdateRate.SIM_200ms, false)
				.Transition(this.on.moving, GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.Not(new StateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.Transition.ConditionCallback(AutoMiner.States.RotationComplete)), UpdateRate.SIM_200ms);
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x002BB280 File Offset: 0x002B9480
		public static bool RotationComplete(AutoMiner.Instance smi)
		{
			return smi.master.RotationComplete;
		}

		// Token: 0x04005B6B RID: 23403
		public StateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.BoolParameter transferring;

		// Token: 0x04005B6C RID: 23404
		public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State off;

		// Token: 0x04005B6D RID: 23405
		public AutoMiner.States.ReadyStates on;

		// Token: 0x02001F90 RID: 8080
		public class ReadyStates : GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State
		{
			// Token: 0x04008C67 RID: 35943
			public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State idle;

			// Token: 0x04008C68 RID: 35944
			public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State moving;

			// Token: 0x04008C69 RID: 35945
			public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State digging;
		}
	}
}
