using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class HugMinionReactable : Reactable
{
	// Token: 0x06000381 RID: 897 RVA: 0x0001B674 File Offset: 0x00019874
	public HugMinionReactable(GameObject gameObject)
		: base(gameObject, "HugMinionReactable", Db.Get().ChoreTypes.Hug, 1, 1, true, 1f, 0f, float.PositiveInfinity, 0f, ObjectLayer.Minion)
	{
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001B6BC File Offset: 0x000198BC
	public override bool InternalCanBegin(GameObject newReactor, Navigator.ActiveTransition transition)
	{
		if (this.reactor != null)
		{
			return false;
		}
		Navigator component = newReactor.GetComponent<Navigator>();
		return !(component == null) && component.IsMoving();
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001B6F6 File Offset: 0x000198F6
	public override void Update(float dt)
	{
		this.gameObject.GetComponent<Facing>().SetFacing(this.reactor.GetComponent<Facing>().GetFacing());
	}

	// Token: 0x06000384 RID: 900 RVA: 0x0001B718 File Offset: 0x00019918
	protected override void InternalBegin()
	{
		KAnimControllerBase component = this.reactor.GetComponent<KAnimControllerBase>();
		component.AddAnimOverrides(Assets.GetAnim("anim_react_pip_kanim"), 0f);
		component.Play("hug_dupe_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("hug_dupe_loop", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("hug_dupe_pst", KAnim.PlayMode.Once, 1f, 0f);
		component.onAnimComplete += this.Finish;
		this.gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnimSequence(new HashedString[] { "hug_dupe_pre", "hug_dupe_loop", "hug_dupe_pst" });
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001B7F8 File Offset: 0x000199F8
	private void Finish(HashedString anim)
	{
		if (anim == "hug_dupe_pst")
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KAnimControllerBase>().onAnimComplete -= this.Finish;
				this.ApplyEffects();
			}
			else
			{
				DebugUtil.DevLogError("HugMinionReactable finishing without adding a Hugged effect.");
			}
			base.End();
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x0001B85C File Offset: 0x00019A5C
	private void ApplyEffects()
	{
		this.reactor.GetComponent<Effects>().Add("Hugged", true);
		HugMonitor.Instance smi = this.gameObject.GetSMI<HugMonitor.Instance>();
		if (smi != null)
		{
			smi.EnterHuggingFrenzy();
		}
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0001B895 File Offset: 0x00019A95
	protected override void InternalEnd()
	{
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0001B897 File Offset: 0x00019A97
	protected override void InternalCleanup()
	{
	}
}
