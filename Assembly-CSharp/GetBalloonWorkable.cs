using System;
using Database;
using UnityEngine;

// Token: 0x020005C7 RID: 1479
[AddComponentMenu("KMonoBehaviour/Workable/GetBalloonWorkable")]
public class GetBalloonWorkable : Workable
{
	// Token: 0x060024CC RID: 9420 RVA: 0x000C70D0 File Offset: 0x000C52D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.faceTargetWhenWorking = true;
		this.workerStatusItem = null;
		this.workingStatusItem = null;
		this.workAnims = GetBalloonWorkable.GET_BALLOON_ANIMS;
		this.workingPstComplete = new HashedString[] { GetBalloonWorkable.PST_ANIM };
		this.workingPstFailed = new HashedString[] { GetBalloonWorkable.PST_ANIM };
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x000C7134 File Offset: 0x000C5334
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		BalloonOverrideSymbol balloonOverride = this.balloonArtist.GetBalloonOverride();
		if (balloonOverride.animFile.IsNone())
		{
			worker.gameObject.GetComponent<SymbolOverrideController>().AddSymbolOverride("body", Assets.GetAnim("balloon_anim_kanim").GetData().build.GetSymbol("body"), 0);
			return;
		}
		worker.gameObject.GetComponent<SymbolOverrideController>().AddSymbolOverride("body", balloonOverride.symbol.Unwrap(), 0);
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x000C71D0 File Offset: 0x000C53D0
	protected override void OnCompleteWork(Worker worker)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("EquippableBalloon"), worker.transform.GetPosition());
		gameObject.GetComponent<Equippable>().Assign(worker.GetComponent<MinionIdentity>());
		gameObject.GetComponent<Equippable>().isEquipped = true;
		gameObject.SetActive(true);
		base.OnCompleteWork(worker);
		BalloonOverrideSymbol balloonOverride = this.balloonArtist.GetBalloonOverride();
		this.balloonArtist.GiveBalloon(balloonOverride);
		gameObject.GetComponent<EquippableBalloon>().SetBalloonOverride(balloonOverride);
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x000C724A File Offset: 0x000C544A
	public override Vector3 GetFacingTarget()
	{
		return this.balloonArtist.master.transform.GetPosition();
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x000C7261 File Offset: 0x000C5461
	public void SetBalloonArtist(BalloonArtistChore.StatesInstance chore)
	{
		this.balloonArtist = chore;
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x000C726A File Offset: 0x000C546A
	public BalloonArtistChore.StatesInstance GetBalloonArtist()
	{
		return this.balloonArtist;
	}

	// Token: 0x04001533 RID: 5427
	private static readonly HashedString[] GET_BALLOON_ANIMS = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04001534 RID: 5428
	private static readonly HashedString PST_ANIM = new HashedString("working_pst");

	// Token: 0x04001535 RID: 5429
	private BalloonArtistChore.StatesInstance balloonArtist;

	// Token: 0x04001536 RID: 5430
	private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

	// Token: 0x04001537 RID: 5431
	private const int TARGET_OVERRIDE_PRIORITY = 0;
}
