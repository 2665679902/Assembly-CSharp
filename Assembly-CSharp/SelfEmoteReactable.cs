using System;
using UnityEngine;

// Token: 0x020003DF RID: 991
public class SelfEmoteReactable : EmoteReactable
{
	// Token: 0x06001492 RID: 5266 RVA: 0x0006C874 File Offset: 0x0006AA74
	public SelfEmoteReactable(GameObject gameObject, HashedString id, ChoreType chore_type, float globalCooldown = 0f, float localCooldown = 20f, float lifeSpan = float.PositiveInfinity, float max_initial_delay = 0f)
		: base(gameObject, id, chore_type, 3, 3, globalCooldown, localCooldown, lifeSpan, max_initial_delay)
	{
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x0006C894 File Offset: 0x0006AA94
	public override bool InternalCanBegin(GameObject reactor, Navigator.ActiveTransition transition)
	{
		if (reactor != this.gameObject)
		{
			return false;
		}
		Navigator component = reactor.GetComponent<Navigator>();
		return !(component == null) && component.IsMoving();
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x0006C8CC File Offset: 0x0006AACC
	public void PairEmote(EmoteChore emoteChore)
	{
		this.chore = emoteChore;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x0006C8D8 File Offset: 0x0006AAD8
	protected override void InternalEnd()
	{
		if (this.chore != null && this.chore.driver != null)
		{
			this.chore.PairReactable(null);
			this.chore.Cancel("Reactable ended");
			this.chore = null;
		}
		base.InternalEnd();
	}

	// Token: 0x04000B8F RID: 2959
	private EmoteChore chore;
}
