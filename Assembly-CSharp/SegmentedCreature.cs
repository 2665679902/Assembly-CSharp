using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class SegmentedCreature : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>
{
	// Token: 0x060003D8 RID: 984 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.freeMovement.idle;
		this.root.Enter(new StateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State.Callback(this.SetRetractedPath));
		this.retracted.DefaultState(this.retracted.pre).Enter(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "idle_loop", KAnim.PlayMode.Loop, false, 0);
		}).Exit(new StateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State.Callback(this.SetRetractedPath));
		this.retracted.pre.Update(new Action<SegmentedCreature.Instance, float>(this.UpdateRetractedPre), UpdateRate.SIM_EVERY_TICK, false);
		this.retracted.loop.ParamTransition<bool>(this.isRetracted, this.freeMovement, (SegmentedCreature.Instance smi, bool p) => !this.isRetracted.Get(smi)).Update(new Action<SegmentedCreature.Instance, float>(this.UpdateRetractedLoop), UpdateRate.SIM_EVERY_TICK, false);
		this.freeMovement.DefaultState(this.freeMovement.idle).ParamTransition<bool>(this.isRetracted, this.retracted, (SegmentedCreature.Instance smi, bool p) => this.isRetracted.Get(smi)).Update(new Action<SegmentedCreature.Instance, float>(this.UpdateFreeMovement), UpdateRate.SIM_EVERY_TICK, false);
		this.freeMovement.idle.Transition(this.freeMovement.moving, (SegmentedCreature.Instance smi) => smi.GetComponent<Navigator>().IsMoving(), UpdateRate.SIM_200ms).Enter(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "idle_loop", KAnim.PlayMode.Loop, true, 0);
		});
		this.freeMovement.moving.Transition(this.freeMovement.idle, (SegmentedCreature.Instance smi) => !smi.GetComponent<Navigator>().IsMoving(), UpdateRate.SIM_200ms).Enter(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "walking_pre", KAnim.PlayMode.Once, false, 0);
			this.PlayBodySegmentsAnim(smi, "walking_loop", KAnim.PlayMode.Loop, false, smi.def.animFrameOffset);
		}).Exit(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "walking_pst", KAnim.PlayMode.Once, true, 0);
		});
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001DA60 File Offset: 0x0001BC60
	private void PlayBodySegmentsAnim(SegmentedCreature.Instance smi, string animName, KAnim.PlayMode playMode, bool queue = false, int frameOffset = 0)
	{
		LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode();
		int num = 0;
		while (linkedListNode != null)
		{
			if (queue)
			{
				linkedListNode.Value.animController.Queue(animName, playMode, 1f, 0f);
			}
			else
			{
				linkedListNode.Value.animController.Play(animName, playMode, 1f, 0f);
			}
			if (frameOffset > 0)
			{
				float num2 = (float)linkedListNode.Value.animController.GetCurrentNumFrames();
				float num3 = (float)num * ((float)frameOffset / num2);
				linkedListNode.Value.animController.SetElapsedTime(num3);
			}
			num++;
			linkedListNode = linkedListNode.Next;
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0001DB08 File Offset: 0x0001BD08
	private void UpdateRetractedPre(SegmentedCreature.Instance smi, float dt)
	{
		this.UpdateHeadPosition(smi);
		bool flag = true;
		for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value.distanceToPreviousSegment = Mathf.Max(smi.def.minSegmentSpacing, linkedListNode.Value.distanceToPreviousSegment - dt * smi.def.retractionSegmentSpeed);
			if (linkedListNode.Value.distanceToPreviousSegment > smi.def.minSegmentSpacing)
			{
				flag = false;
			}
		}
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		LinkedListNode<SegmentedCreature.PathNode> linkedListNode2 = smi.path.First;
		Vector3 forward = value.Forward;
		Quaternion rotation = value.Rotation;
		int num = 0;
		while (linkedListNode2 != null)
		{
			Vector3 vector = value.Position - smi.def.pathSpacing * (float)num * forward;
			linkedListNode2.Value.position = Vector3.Lerp(linkedListNode2.Value.position, vector, dt * smi.def.retractionPathSpeed);
			linkedListNode2.Value.rotation = Quaternion.Slerp(linkedListNode2.Value.rotation, rotation, dt * smi.def.retractionPathSpeed);
			num++;
			linkedListNode2 = linkedListNode2.Next;
		}
		this.UpdateBodyPosition(smi);
		if (flag)
		{
			smi.GoTo(this.retracted.loop);
		}
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0001DC55 File Offset: 0x0001BE55
	private void UpdateRetractedLoop(SegmentedCreature.Instance smi, float dt)
	{
		this.UpdateHeadPosition(smi);
		this.SetRetractedPath(smi);
		this.UpdateBodyPosition(smi);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x0001DC70 File Offset: 0x0001BE70
	private void SetRetractedPath(SegmentedCreature.Instance smi)
	{
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		LinkedListNode<SegmentedCreature.PathNode> linkedListNode = smi.path.First;
		Vector3 position = value.Position;
		Quaternion rotation = value.Rotation;
		Vector3 forward = value.Forward;
		int num = 0;
		while (linkedListNode != null)
		{
			linkedListNode.Value.position = position - smi.def.pathSpacing * (float)num * forward;
			linkedListNode.Value.rotation = rotation;
			num++;
			linkedListNode = linkedListNode.Next;
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
	private void UpdateFreeMovement(SegmentedCreature.Instance smi, float dt)
	{
		float num = this.UpdateHeadPosition(smi);
		this.AdjustBodySegmentsSpacing(smi, num);
		this.UpdateBodyPosition(smi);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0001DD14 File Offset: 0x0001BF14
	private float UpdateHeadPosition(SegmentedCreature.Instance smi)
	{
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		if (value.Position == smi.previousHeadPosition)
		{
			return 0f;
		}
		SegmentedCreature.PathNode value2 = smi.path.First.Value;
		SegmentedCreature.PathNode pathNode = smi.path.First.Next.Value;
		float magnitude = (value2.position - pathNode.position).magnitude;
		float magnitude2 = (value.Position - pathNode.position).magnitude;
		float num = magnitude2 - magnitude;
		value2.position = value.Position;
		value2.rotation = value.Rotation;
		smi.previousHeadPosition = value2.position;
		Vector3 normalized = (value2.position - pathNode.position).normalized;
		int num2 = Mathf.FloorToInt(magnitude2 / smi.def.pathSpacing);
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector = pathNode.position + normalized * smi.def.pathSpacing;
			LinkedListNode<SegmentedCreature.PathNode> last = smi.path.Last;
			last.Value.position = vector;
			last.Value.rotation = value2.rotation;
			float num3 = magnitude2 - (float)i * smi.def.pathSpacing;
			float num4 = num3 - smi.def.pathSpacing / num3;
			last.Value.rotation = Quaternion.Lerp(value2.rotation, pathNode.rotation, num4);
			smi.path.RemoveLast();
			smi.path.AddAfter(smi.path.First, last);
			pathNode = last.Value;
		}
		return num;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0001DED8 File Offset: 0x0001C0D8
	private void AdjustBodySegmentsSpacing(SegmentedCreature.Instance smi, float spacing)
	{
		if (spacing == 0f)
		{
			return;
		}
		for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value.distanceToPreviousSegment += spacing;
			if (linkedListNode.Value.distanceToPreviousSegment < smi.def.minSegmentSpacing)
			{
				spacing = linkedListNode.Value.distanceToPreviousSegment - smi.def.minSegmentSpacing;
				linkedListNode.Value.distanceToPreviousSegment = smi.def.minSegmentSpacing;
			}
			else
			{
				if (linkedListNode.Value.distanceToPreviousSegment <= smi.def.maxSegmentSpacing)
				{
					break;
				}
				spacing = linkedListNode.Value.distanceToPreviousSegment - smi.def.maxSegmentSpacing;
				linkedListNode.Value.distanceToPreviousSegment = smi.def.maxSegmentSpacing;
			}
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001DFAC File Offset: 0x0001C1AC
	private void UpdateBodyPosition(SegmentedCreature.Instance smi)
	{
		LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode();
		LinkedListNode<SegmentedCreature.PathNode> linkedListNode2 = smi.path.First;
		float num = 0f;
		float num2 = smi.LengthPercentage();
		int num3 = 0;
		while (linkedListNode != null)
		{
			float num4 = linkedListNode.Value.distanceToPreviousSegment;
			float num5 = 0f;
			while (linkedListNode2.Next != null)
			{
				num5 = (linkedListNode2.Value.position - linkedListNode2.Next.Value.position).magnitude - num;
				if (num4 < num5)
				{
					break;
				}
				num4 -= num5;
				num = 0f;
				linkedListNode2 = linkedListNode2.Next;
			}
			if (linkedListNode2.Next == null)
			{
				linkedListNode.Value.SetPosition(linkedListNode2.Value.position);
				linkedListNode.Value.SetRotation(smi.path.Last.Value.rotation);
			}
			else
			{
				SegmentedCreature.PathNode value = linkedListNode2.Value;
				SegmentedCreature.PathNode value2 = linkedListNode2.Next.Value;
				linkedListNode.Value.SetPosition(linkedListNode2.Value.position + (linkedListNode2.Next.Value.position - linkedListNode2.Value.position).normalized * num4);
				linkedListNode.Value.SetRotation(Quaternion.Slerp(value.rotation, value2.rotation, num4 / num5));
				num = num4;
			}
			linkedListNode.Value.animController.FlipX = linkedListNode.Previous.Value.Position.x < linkedListNode.Value.Position.x;
			linkedListNode.Value.animController.animScale = smi.baseAnimScale + smi.baseAnimScale * smi.def.compressedMaxScale * ((float)(smi.def.numBodySegments - num3) / (float)smi.def.numBodySegments) * (1f - num2);
			linkedListNode = linkedListNode.Next;
			num3++;
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
	private void DrawDebug(SegmentedCreature.Instance smi, float dt)
	{
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		DrawUtil.Arrow(value.Position, value.Position + value.Up, 0.05f, Color.red, 0f);
		DrawUtil.Arrow(value.Position, value.Position + value.Forward * 0.06f, 0.05f, Color.cyan, 0f);
		int num = 0;
		foreach (SegmentedCreature.PathNode pathNode in smi.path)
		{
			Color color = Color.HSVToRGB((float)num / (float)smi.def.numPathNodes, 1f, 1f);
			DrawUtil.Gnomon(pathNode.position, 0.05f, Color.cyan, 0f);
			DrawUtil.Arrow(pathNode.position, pathNode.position + pathNode.rotation * Vector3.up * 0.5f, 0.025f, color, 0f);
			num++;
		}
		for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.segments.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			DrawUtil.Circle(linkedListNode.Value.Position, 0.05f, Color.white, new Vector3?(Vector3.forward), 0f);
			DrawUtil.Gnomon(linkedListNode.Value.Position, 0.05f, Color.white, 0f);
		}
	}

	// Token: 0x04000276 RID: 630
	public SegmentedCreature.RectractStates retracted;

	// Token: 0x04000277 RID: 631
	public SegmentedCreature.FreeMovementStates freeMovement;

	// Token: 0x04000278 RID: 632
	private StateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.BoolParameter isRetracted;

	// Token: 0x02000EAC RID: 3756
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040051F4 RID: 20980
		public HashedString segmentTrackerSymbol;

		// Token: 0x040051F5 RID: 20981
		public Vector3 headOffset = Vector3.zero;

		// Token: 0x040051F6 RID: 20982
		public Vector3 bodyPivot = Vector3.zero;

		// Token: 0x040051F7 RID: 20983
		public Vector3 tailPivot = Vector3.zero;

		// Token: 0x040051F8 RID: 20984
		public int numBodySegments;

		// Token: 0x040051F9 RID: 20985
		public float minSegmentSpacing;

		// Token: 0x040051FA RID: 20986
		public float maxSegmentSpacing;

		// Token: 0x040051FB RID: 20987
		public int numPathNodes;

		// Token: 0x040051FC RID: 20988
		public float pathSpacing;

		// Token: 0x040051FD RID: 20989
		public KAnimFile midAnim;

		// Token: 0x040051FE RID: 20990
		public KAnimFile tailAnim;

		// Token: 0x040051FF RID: 20991
		public string movingAnimName;

		// Token: 0x04005200 RID: 20992
		public string idleAnimName;

		// Token: 0x04005201 RID: 20993
		public float retractionSegmentSpeed = 1f;

		// Token: 0x04005202 RID: 20994
		public float retractionPathSpeed = 1f;

		// Token: 0x04005203 RID: 20995
		public float compressedMaxScale = 1.2f;

		// Token: 0x04005204 RID: 20996
		public int animFrameOffset;

		// Token: 0x04005205 RID: 20997
		public HashSet<HashedString> retractWhenStartingAnimNames = new HashSet<HashedString> { "trapped", "trussed", "escape", "drown_pre", "drown_loop", "drown_pst" };

		// Token: 0x04005206 RID: 20998
		public HashSet<HashedString> retractWhenEndingAnimNames = new HashSet<HashedString> { "floor_floor_2_0", "grooming_pst", "fall" };
	}

	// Token: 0x02000EAD RID: 3757
	public class RectractStates : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State
	{
		// Token: 0x04005207 RID: 20999
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State pre;

		// Token: 0x04005208 RID: 21000
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State loop;
	}

	// Token: 0x02000EAE RID: 3758
	public class FreeMovementStates : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State
	{
		// Token: 0x04005209 RID: 21001
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State idle;

		// Token: 0x0400520A RID: 21002
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State moving;

		// Token: 0x0400520B RID: 21003
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State layEgg;

		// Token: 0x0400520C RID: 21004
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State poop;

		// Token: 0x0400520D RID: 21005
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State dead;
	}

	// Token: 0x02000EAF RID: 3759
	public new class Instance : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.GameInstance
	{
		// Token: 0x06006CDA RID: 27866 RVA: 0x00298D7C File Offset: 0x00296F7C
		public Instance(IStateMachineTarget master, SegmentedCreature.Def def)
			: base(master, def)
		{
			global::Debug.Assert((float)def.numBodySegments * def.maxSegmentSpacing < (float)def.numPathNodes * def.pathSpacing);
			this.CreateSegments();
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x00298DD0 File Offset: 0x00296FD0
		private void CreateSegments()
		{
			float num = Grid.GetLayerZ(Grid.SceneLayer.Creatures) + (float)SegmentedCreature.Instance.creatureBatchSlot * 0.01f;
			SegmentedCreature.Instance.creatureBatchSlot = (SegmentedCreature.Instance.creatureBatchSlot + 1) % 10;
			SegmentedCreature.CreatureSegment value = this.segments.AddFirst(new SegmentedCreature.CreatureSegment(base.gameObject, num, base.smi.def.headOffset, Vector3.zero)).Value;
			base.gameObject.SetActive(false);
			value.animController = base.GetComponent<KBatchedAnimController>();
			value.animController.SetSymbolVisiblity(base.smi.def.segmentTrackerSymbol, false);
			value.symbol = base.smi.def.segmentTrackerSymbol;
			value.SetPosition(base.transform.position);
			base.gameObject.SetActive(true);
			this.baseAnimScale = value.animController.animScale;
			value.animController.onAnimEnter += this.AnimEntered;
			value.animController.onAnimComplete += this.AnimComplete;
			for (int i = 0; i < base.def.numBodySegments; i++)
			{
				GameObject gameObject = new GameObject(base.gameObject.GetProperName() + string.Format(" Segment {0}", i));
				gameObject.SetActive(false);
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = value.Position;
				KAnimFile kanimFile = base.def.midAnim;
				Vector3 vector = base.def.bodyPivot;
				if (i == base.def.numBodySegments - 1)
				{
					kanimFile = base.def.tailAnim;
					vector = base.def.tailPivot;
				}
				KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
				kbatchedAnimController.AnimFiles = new KAnimFile[] { kanimFile };
				kbatchedAnimController.isMovable = true;
				kbatchedAnimController.SetSymbolVisiblity(base.smi.def.segmentTrackerSymbol, false);
				kbatchedAnimController.sceneLayer = value.animController.sceneLayer;
				SegmentedCreature.CreatureSegment creatureSegment = new SegmentedCreature.CreatureSegment(gameObject, num + (float)(i + 1) * 0.0001f, Vector3.zero, vector);
				creatureSegment.animController = kbatchedAnimController;
				creatureSegment.symbol = base.smi.def.segmentTrackerSymbol;
				creatureSegment.distanceToPreviousSegment = base.smi.def.minSegmentSpacing;
				creatureSegment.animLink = new KAnimLink(value.animController, kbatchedAnimController);
				this.segments.AddLast(creatureSegment);
				gameObject.SetActive(true);
			}
			for (int j = 0; j < base.def.numPathNodes; j++)
			{
				this.path.AddLast(new SegmentedCreature.PathNode(value.Position));
			}
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x0029908C File Offset: 0x0029728C
		public void AnimEntered(HashedString name)
		{
			if (base.smi.def.retractWhenStartingAnimNames.Contains(name))
			{
				base.smi.sm.isRetracted.Set(true, base.smi, false);
				return;
			}
			base.smi.sm.isRetracted.Set(false, base.smi, false);
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x002990EE File Offset: 0x002972EE
		public void AnimComplete(HashedString name)
		{
			if (base.smi.def.retractWhenEndingAnimNames.Contains(name))
			{
				base.smi.sm.isRetracted.Set(true, base.smi, false);
			}
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x00299126 File Offset: 0x00297326
		public LinkedListNode<SegmentedCreature.CreatureSegment> GetHeadSegmentNode()
		{
			return base.smi.segments.First;
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x00299138 File Offset: 0x00297338
		public LinkedListNode<SegmentedCreature.CreatureSegment> GetFirstBodySegmentNode()
		{
			return base.smi.segments.First.Next;
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x00299150 File Offset: 0x00297350
		public float LengthPercentage()
		{
			float num = 0f;
			for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = this.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				num += linkedListNode.Value.distanceToPreviousSegment;
			}
			float num2 = this.MinLength();
			float num3 = this.MaxLength();
			return Mathf.Clamp(num - num2, 0f, num3) / (num3 - num2);
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x002991A4 File Offset: 0x002973A4
		public float MinLength()
		{
			return base.smi.def.minSegmentSpacing * (float)base.smi.def.numBodySegments;
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x002991C8 File Offset: 0x002973C8
		public float MaxLength()
		{
			return base.smi.def.maxSegmentSpacing * (float)base.smi.def.numBodySegments;
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x002991EC File Offset: 0x002973EC
		protected override void OnCleanUp()
		{
			this.GetHeadSegmentNode().Value.animController.onAnimEnter -= this.AnimEntered;
			this.GetHeadSegmentNode().Value.animController.onAnimComplete -= this.AnimComplete;
			for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = this.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value.CleanUp();
			}
		}

		// Token: 0x0400520E RID: 21006
		private const int NUM_CREATURE_SLOTS = 10;

		// Token: 0x0400520F RID: 21007
		private static int creatureBatchSlot;

		// Token: 0x04005210 RID: 21008
		public float baseAnimScale;

		// Token: 0x04005211 RID: 21009
		public Vector3 previousHeadPosition;

		// Token: 0x04005212 RID: 21010
		public float previousDist;

		// Token: 0x04005213 RID: 21011
		public LinkedList<SegmentedCreature.PathNode> path = new LinkedList<SegmentedCreature.PathNode>();

		// Token: 0x04005214 RID: 21012
		public LinkedList<SegmentedCreature.CreatureSegment> segments = new LinkedList<SegmentedCreature.CreatureSegment>();
	}

	// Token: 0x02000EB0 RID: 3760
	public class PathNode
	{
		// Token: 0x06006CE4 RID: 27876 RVA: 0x00299259 File Offset: 0x00297459
		public PathNode(Vector3 position)
		{
			this.position = position;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04005215 RID: 21013
		public Vector3 position;

		// Token: 0x04005216 RID: 21014
		public Quaternion rotation;
	}

	// Token: 0x02000EB1 RID: 3761
	public class CreatureSegment
	{
		// Token: 0x06006CE5 RID: 27877 RVA: 0x00299273 File Offset: 0x00297473
		public CreatureSegment(GameObject go, float zRelativeOffset, Vector3 offset, Vector3 pivot)
		{
			this.m_transform = go.transform;
			this.zRelativeOffset = zRelativeOffset;
			this.offset = offset;
			this.pivot = pivot;
			this.SetPosition(go.transform.position);
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06006CE6 RID: 27878 RVA: 0x002992B0 File Offset: 0x002974B0
		public Vector3 Position
		{
			get
			{
				Vector3 vector = this.offset;
				vector.x *= (float)(this.animController.FlipX ? (-1) : 1);
				if (vector != Vector3.zero)
				{
					vector = this.Rotation * vector;
				}
				if (this.symbol.IsValid)
				{
					bool flag;
					Vector3 vector2 = this.animController.GetSymbolTransform(this.symbol, out flag).GetColumn(3);
					vector2.z = this.zRelativeOffset;
					return vector2 + vector;
				}
				return this.m_transform.position + vector;
			}
		}

		// Token: 0x06006CE7 RID: 27879 RVA: 0x00299353 File Offset: 0x00297553
		public void SetPosition(Vector3 value)
		{
			value.z = this.zRelativeOffset;
			this.m_transform.position = value;
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x0029936E File Offset: 0x0029756E
		public void SetRotation(Quaternion rotation)
		{
			this.m_transform.rotation = rotation;
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x0029937C File Offset: 0x0029757C
		public Quaternion Rotation
		{
			get
			{
				if (this.symbol.IsValid)
				{
					bool flag;
					Vector3 vector = this.animController.GetSymbolLocalTransform(this.symbol, out flag).MultiplyVector(Vector3.right);
					if (!this.animController.FlipX)
					{
						vector.y *= -1f;
					}
					return Quaternion.FromToRotation(Vector3.right, vector);
				}
				return this.m_transform.rotation;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06006CEA RID: 27882 RVA: 0x002993EB File Offset: 0x002975EB
		public Vector3 Forward
		{
			get
			{
				return this.Rotation * (this.animController.FlipX ? Vector3.left : Vector3.right);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06006CEB RID: 27883 RVA: 0x00299411 File Offset: 0x00297611
		public Vector3 Up
		{
			get
			{
				return this.Rotation * Vector3.up;
			}
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x00299423 File Offset: 0x00297623
		public void CleanUp()
		{
			UnityEngine.Object.Destroy(this.m_transform.gameObject);
		}

		// Token: 0x04005217 RID: 21015
		public KBatchedAnimController animController;

		// Token: 0x04005218 RID: 21016
		public KAnimLink animLink;

		// Token: 0x04005219 RID: 21017
		public float distanceToPreviousSegment;

		// Token: 0x0400521A RID: 21018
		public HashedString symbol;

		// Token: 0x0400521B RID: 21019
		public Vector3 offset;

		// Token: 0x0400521C RID: 21020
		public Vector3 pivot;

		// Token: 0x0400521D RID: 21021
		public float zRelativeOffset;

		// Token: 0x0400521E RID: 21022
		private Transform m_transform;
	}
}
