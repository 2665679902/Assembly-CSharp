using System;
using UnityEngine;

// Token: 0x02000971 RID: 2417
public class ClusterMapTravelAnimator : GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer>
{
	// Token: 0x060047C5 RID: 18373 RVA: 0x00193C44 File Offset: 0x00191E44
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.idle;
		this.root.OnTargetLost(this.entityTarget, null);
		this.idle.Target(this.entityTarget).Transition(this.grounded, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.IsGrounded), UpdateRate.SIM_200ms).Transition(this.surfaceTransitioning, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.IsSurfaceTransitioning), UpdateRate.SIM_200ms)
			.EventHandlerTransition(GameHashes.ClusterLocationChanged, (ClusterMapTravelAnimator.StatesInstance smi) => Game.Instance, this.repositioning, new Func<ClusterMapTravelAnimator.StatesInstance, object, bool>(this.ClusterChangedAtMyLocation))
			.EventTransition(GameHashes.ClusterDestinationChanged, this.traveling, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.IsTraveling))
			.Target(this.masterTarget);
		this.grounded.Transition(this.surfaceTransitioning, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.IsSurfaceTransitioning), UpdateRate.SIM_200ms);
		this.surfaceTransitioning.Update(delegate(ClusterMapTravelAnimator.StatesInstance smi, float dt)
		{
			this.DoOrientToPath(smi);
		}, UpdateRate.SIM_200ms, false).Transition(this.repositioning, GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Not(new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.IsSurfaceTransitioning)), UpdateRate.SIM_200ms);
		this.repositioning.Transition(this.traveling.orientToIdle, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.DoReposition), UpdateRate.RENDER_EVERY_TICK);
		this.traveling.DefaultState(this.traveling.orientToPath);
		this.traveling.travelIdle.Target(this.entityTarget).EventHandlerTransition(GameHashes.ClusterLocationChanged, (ClusterMapTravelAnimator.StatesInstance smi) => Game.Instance, this.repositioning, new Func<ClusterMapTravelAnimator.StatesInstance, object, bool>(this.ClusterChangedAtMyLocation)).EventTransition(GameHashes.ClusterDestinationChanged, this.traveling.orientToIdle, GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Not(new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.IsTraveling)))
			.EventTransition(GameHashes.ClusterDestinationChanged, this.traveling.orientToPath, GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Not(new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.DoOrientToPath)))
			.EventTransition(GameHashes.ClusterLocationChanged, this.traveling.move, GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Not(new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.DoMove)))
			.Target(this.masterTarget);
		this.traveling.orientToPath.Transition(this.traveling.travelIdle, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.DoOrientToPath), UpdateRate.RENDER_EVERY_TICK).Target(this.entityTarget).EventHandlerTransition(GameHashes.ClusterLocationChanged, (ClusterMapTravelAnimator.StatesInstance smi) => Game.Instance, this.repositioning, new Func<ClusterMapTravelAnimator.StatesInstance, object, bool>(this.ClusterChangedAtMyLocation))
			.Target(this.masterTarget);
		this.traveling.move.Transition(this.traveling.travelIdle, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.DoMove), UpdateRate.RENDER_EVERY_TICK);
		this.traveling.orientToIdle.Transition(this.idle, new StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.Transition.ConditionCallback(this.DoOrientToIdle), UpdateRate.RENDER_EVERY_TICK);
	}

	// Token: 0x060047C6 RID: 18374 RVA: 0x00193F37 File Offset: 0x00192137
	private bool IsTraveling(ClusterMapTravelAnimator.StatesInstance smi)
	{
		return smi.entity.GetComponent<ClusterTraveler>().IsTraveling();
	}

	// Token: 0x060047C7 RID: 18375 RVA: 0x00193F4C File Offset: 0x0019214C
	private bool IsSurfaceTransitioning(ClusterMapTravelAnimator.StatesInstance smi)
	{
		Clustercraft clustercraft = smi.entity as Clustercraft;
		return clustercraft != null && (clustercraft.Status == Clustercraft.CraftStatus.Landing || clustercraft.Status == Clustercraft.CraftStatus.Launching);
	}

	// Token: 0x060047C8 RID: 18376 RVA: 0x00193F84 File Offset: 0x00192184
	private bool IsGrounded(ClusterMapTravelAnimator.StatesInstance smi)
	{
		Clustercraft clustercraft = smi.entity as Clustercraft;
		return clustercraft != null && clustercraft.Status == Clustercraft.CraftStatus.Grounded;
	}

	// Token: 0x060047C9 RID: 18377 RVA: 0x00193FB4 File Offset: 0x001921B4
	private bool DoReposition(ClusterMapTravelAnimator.StatesInstance smi)
	{
		Vector3 position = ClusterGrid.Instance.GetPosition(smi.entity);
		return smi.MoveTowards(position, Time.unscaledDeltaTime);
	}

	// Token: 0x060047CA RID: 18378 RVA: 0x00193FE0 File Offset: 0x001921E0
	private bool DoMove(ClusterMapTravelAnimator.StatesInstance smi)
	{
		Vector3 position = ClusterGrid.Instance.GetPosition(smi.entity);
		return smi.MoveTowards(position, Time.unscaledDeltaTime);
	}

	// Token: 0x060047CB RID: 18379 RVA: 0x0019400C File Offset: 0x0019220C
	private bool DoOrientToPath(ClusterMapTravelAnimator.StatesInstance smi)
	{
		float pathAngle = smi.GetComponent<ClusterMapVisualizer>().GetPathAngle();
		return smi.RotateTowards(pathAngle, Time.unscaledDeltaTime);
	}

	// Token: 0x060047CC RID: 18380 RVA: 0x00194031 File Offset: 0x00192231
	private bool DoOrientToIdle(ClusterMapTravelAnimator.StatesInstance smi)
	{
		return smi.RotateTowards(0f, Time.unscaledDeltaTime);
	}

	// Token: 0x060047CD RID: 18381 RVA: 0x00194044 File Offset: 0x00192244
	private bool ClusterChangedAtMyLocation(ClusterMapTravelAnimator.StatesInstance smi, object data)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = (ClusterLocationChangedEvent)data;
		return clusterLocationChangedEvent.oldLocation == smi.entity.Location || clusterLocationChangedEvent.newLocation == smi.entity.Location;
	}

	// Token: 0x04002F61 RID: 12129
	public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State idle;

	// Token: 0x04002F62 RID: 12130
	public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State grounded;

	// Token: 0x04002F63 RID: 12131
	public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State repositioning;

	// Token: 0x04002F64 RID: 12132
	public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State surfaceTransitioning;

	// Token: 0x04002F65 RID: 12133
	public ClusterMapTravelAnimator.TravelingStates traveling;

	// Token: 0x04002F66 RID: 12134
	public StateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.TargetParameter entityTarget;

	// Token: 0x0200177F RID: 6015
	private class Tuning : TuningData<ClusterMapTravelAnimator.Tuning>
	{
		// Token: 0x04006D3B RID: 27963
		public float visualizerTransitionSpeed = 1f;

		// Token: 0x04006D3C RID: 27964
		public float visualizerRotationSpeed = 1f;
	}

	// Token: 0x02001780 RID: 6016
	public class TravelingStates : GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State
	{
		// Token: 0x04006D3D RID: 27965
		public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State travelIdle;

		// Token: 0x04006D3E RID: 27966
		public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State orientToPath;

		// Token: 0x04006D3F RID: 27967
		public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State move;

		// Token: 0x04006D40 RID: 27968
		public GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.State orientToIdle;
	}

	// Token: 0x02001781 RID: 6017
	public class StatesInstance : GameStateMachine<ClusterMapTravelAnimator, ClusterMapTravelAnimator.StatesInstance, ClusterMapVisualizer, object>.GameInstance
	{
		// Token: 0x06008B1F RID: 35615 RVA: 0x002FE9E7 File Offset: 0x002FCBE7
		public StatesInstance(ClusterMapVisualizer master, ClusterGridEntity entity)
			: base(master)
		{
			this.entity = entity;
			base.sm.entityTarget.Set(entity, this);
		}

		// Token: 0x06008B20 RID: 35616 RVA: 0x002FEA0C File Offset: 0x002FCC0C
		public bool MoveTowards(Vector3 targetPosition, float dt)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			ClusterMapVisualizer component2 = base.GetComponent<ClusterMapVisualizer>();
			Vector3 localPosition = component.GetLocalPosition();
			Vector3 vector = targetPosition - localPosition;
			Vector3 normalized = vector.normalized;
			float magnitude = vector.magnitude;
			float num = TuningData<ClusterMapTravelAnimator.Tuning>.Get().visualizerTransitionSpeed * dt;
			if (num < magnitude)
			{
				Vector3 vector2 = normalized * num;
				component.SetLocalPosition(localPosition + vector2);
				component2.RefreshPathDrawing();
				return false;
			}
			component.SetLocalPosition(targetPosition);
			component2.RefreshPathDrawing();
			return true;
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x002FEA90 File Offset: 0x002FCC90
		public bool RotateTowards(float targetAngle, float dt)
		{
			ClusterMapVisualizer component = base.GetComponent<ClusterMapVisualizer>();
			float num = targetAngle - this.simpleAngle;
			if (num > 180f)
			{
				num -= 360f;
			}
			else if (num < -180f)
			{
				num += 360f;
			}
			float num2 = TuningData<ClusterMapTravelAnimator.Tuning>.Get().visualizerRotationSpeed * dt;
			if (num > 0f && num2 < num)
			{
				this.simpleAngle += num2;
				component.SetAnimRotation(this.simpleAngle);
				return false;
			}
			if (num < 0f && -num2 > num)
			{
				this.simpleAngle -= num2;
				component.SetAnimRotation(this.simpleAngle);
				return false;
			}
			this.simpleAngle = targetAngle;
			component.SetAnimRotation(this.simpleAngle);
			return true;
		}

		// Token: 0x04006D41 RID: 27969
		public ClusterGridEntity entity;

		// Token: 0x04006D42 RID: 27970
		private float simpleAngle;
	}
}
