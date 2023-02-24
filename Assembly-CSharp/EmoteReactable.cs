using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class EmoteReactable : Reactable
{
	// Token: 0x0600146A RID: 5226 RVA: 0x0006BD70 File Offset: 0x00069F70
	public EmoteReactable(GameObject gameObject, HashedString id, ChoreType chore_type, int range_width = 15, int range_height = 8, float globalCooldown = 0f, float localCooldown = 20f, float lifeSpan = float.PositiveInfinity, float max_initial_delay = 0f)
		: base(gameObject, id, chore_type, range_width, range_height, true, globalCooldown, localCooldown, lifeSpan, max_initial_delay, ObjectLayer.NumLayers)
	{
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0006BD9C File Offset: 0x00069F9C
	public EmoteReactable SetEmote(Emote emote)
	{
		this.emote = emote;
		return this;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x0006BDA8 File Offset: 0x00069FA8
	public EmoteReactable RegisterEmoteStepCallbacks(HashedString stepName, Action<GameObject> startedCb, Action<GameObject> finishedCb)
	{
		if (this.callbackHandles == null)
		{
			this.callbackHandles = new HandleVector<EmoteStep.Callbacks>.Handle[this.emote.StepCount];
		}
		int stepIndex = this.emote.GetStepIndex(stepName);
		this.callbackHandles[stepIndex] = this.emote[stepIndex].RegisterCallbacks(startedCb, finishedCb);
		return this;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0006BE00 File Offset: 0x0006A000
	public EmoteReactable SetExpression(Expression expression)
	{
		this.expression = expression;
		return this;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0006BE0A File Offset: 0x0006A00A
	public EmoteReactable SetThought(Thought thought)
	{
		this.thought = thought;
		return this;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0006BE14 File Offset: 0x0006A014
	public EmoteReactable SetOverideAnimSet(string animSet)
	{
		this.overrideAnimSet = Assets.GetAnim(animSet);
		return this;
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0006BE28 File Offset: 0x0006A028
	public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
	{
		if (this.reactor != null || new_reactor == null)
		{
			return false;
		}
		Navigator component = new_reactor.GetComponent<Navigator>();
		return !(component == null) && component.IsMoving() && (-257 & (1 << (int)component.CurrentNavType)) != 0 && this.gameObject != new_reactor;
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x0006BE8C File Offset: 0x0006A08C
	public override void Update(float dt)
	{
		if (this.emote == null || !this.emote.IsValidStep(this.currentStep))
		{
			return;
		}
		if (this.gameObject != null && this.reactor != null)
		{
			Facing component = this.reactor.GetComponent<Facing>();
			if (component != null)
			{
				component.Face(this.gameObject.transform.GetPosition());
			}
		}
		float timeout = this.emote[this.currentStep].timeout;
		if (timeout > 0f && timeout < this.elapsed)
		{
			this.NextStep(null);
			return;
		}
		this.elapsed += dt;
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x0006BF40 File Offset: 0x0006A140
	protected override void InternalBegin()
	{
		this.kbac = this.reactor.GetComponent<KBatchedAnimController>();
		this.emote.ApplyAnimOverrides(this.kbac, this.overrideAnimSet);
		if (this.expression != null)
		{
			this.reactor.GetComponent<FaceGraph>().AddExpression(this.expression);
		}
		if (this.thought != null)
		{
			this.reactor.GetSMI<ThoughtGraph.Instance>().AddThought(this.thought);
		}
		this.NextStep(null);
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
	protected override void InternalEnd()
	{
		if (this.kbac != null)
		{
			this.kbac.onAnimComplete -= this.NextStep;
			this.emote.RemoveAnimOverrides(this.kbac, this.overrideAnimSet);
			this.kbac = null;
		}
		if (this.reactor != null)
		{
			if (this.expression != null)
			{
				this.reactor.GetComponent<FaceGraph>().RemoveExpression(this.expression);
			}
			if (this.thought != null)
			{
				this.reactor.GetSMI<ThoughtGraph.Instance>().RemoveThought(this.thought);
			}
		}
		this.currentStep = -1;
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x0006C064 File Offset: 0x0006A264
	protected override void InternalCleanup()
	{
		if (this.emote == null || this.callbackHandles == null)
		{
			return;
		}
		int num = 0;
		while (this.emote.IsValidStep(num))
		{
			this.emote[num].UnregisterCallbacks(this.callbackHandles[num]);
			num++;
		}
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0006C0B8 File Offset: 0x0006A2B8
	private void NextStep(HashedString finishedAnim)
	{
		if (this.emote.IsValidStep(this.currentStep) && this.emote[this.currentStep].timeout <= 0f)
		{
			this.kbac.onAnimComplete -= this.NextStep;
			if (this.callbackHandles != null)
			{
				this.emote[this.currentStep].OnStepFinished(this.callbackHandles[this.currentStep], this.reactor);
			}
		}
		this.currentStep++;
		if (!this.emote.IsValidStep(this.currentStep) || this.kbac == null)
		{
			base.End();
			return;
		}
		EmoteStep emoteStep = this.emote[this.currentStep];
		if (emoteStep.anim != HashedString.Invalid)
		{
			this.kbac.Play(emoteStep.anim, emoteStep.mode, 1f, 0f);
			if (this.kbac.IsStopped())
			{
				emoteStep.timeout = 0.25f;
			}
		}
		if (emoteStep.timeout <= 0f)
		{
			this.kbac.onAnimComplete += this.NextStep;
		}
		else
		{
			this.elapsed = 0f;
		}
		if (this.callbackHandles != null)
		{
			emoteStep.OnStepStarted(this.callbackHandles[this.currentStep], this.reactor);
		}
	}

	// Token: 0x04000B70 RID: 2928
	private KBatchedAnimController kbac;

	// Token: 0x04000B71 RID: 2929
	public Expression expression;

	// Token: 0x04000B72 RID: 2930
	public Thought thought;

	// Token: 0x04000B73 RID: 2931
	public Emote emote;

	// Token: 0x04000B74 RID: 2932
	private HandleVector<EmoteStep.Callbacks>.Handle[] callbackHandles;

	// Token: 0x04000B75 RID: 2933
	protected KAnimFile overrideAnimSet;

	// Token: 0x04000B76 RID: 2934
	private int currentStep = -1;

	// Token: 0x04000B77 RID: 2935
	private float elapsed;
}
