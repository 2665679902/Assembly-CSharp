using System;

// Token: 0x02000B77 RID: 2935
public class SaveActive : KScreen
{
	// Token: 0x06005C41 RID: 23617 RVA: 0x0021C179 File Offset: 0x0021A379
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.SetAutoSaveCallbacks(new Game.SavingPreCB(this.ActiveateSaveIndicator), new Game.SavingActiveCB(this.SetActiveSaveIndicator), new Game.SavingPostCB(this.DeactivateSaveIndicator));
	}

	// Token: 0x06005C42 RID: 23618 RVA: 0x0021C1AF File Offset: 0x0021A3AF
	private void DoCallBack(HashedString name)
	{
		this.controller.onAnimComplete -= this.DoCallBack;
		this.readyForSaveCallback();
		this.readyForSaveCallback = null;
	}

	// Token: 0x06005C43 RID: 23619 RVA: 0x0021C1DA File Offset: 0x0021A3DA
	private void ActiveateSaveIndicator(Game.CansaveCB cb)
	{
		this.readyForSaveCallback = cb;
		this.controller.onAnimComplete += this.DoCallBack;
		this.controller.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06005C44 RID: 23620 RVA: 0x0021C21A File Offset: 0x0021A41A
	private void SetActiveSaveIndicator()
	{
		this.controller.Play("working_loop", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06005C45 RID: 23621 RVA: 0x0021C23C File Offset: 0x0021A43C
	private void DeactivateSaveIndicator()
	{
		this.controller.Play("working_pst", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06005C46 RID: 23622 RVA: 0x0021C25E File Offset: 0x0021A45E
	public override void OnKeyDown(KButtonEvent e)
	{
	}

	// Token: 0x04003F05 RID: 16133
	[MyCmpGet]
	private KBatchedAnimController controller;

	// Token: 0x04003F06 RID: 16134
	private Game.CansaveCB readyForSaveCallback;
}
