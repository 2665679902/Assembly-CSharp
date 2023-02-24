using System;
using System.Linq;
using Database;
using UnityEngine;

// Token: 0x02000AE3 RID: 2787
public class KleiPermitDioramaVis_JoyResponseBalloon : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005549 RID: 21833 RVA: 0x001EDAD9 File Offset: 0x001EBCD9
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600554A RID: 21834 RVA: 0x001EDAE4 File Offset: 0x001EBCE4
	public void ConfigureSetup()
	{
		this.minionUI.transform.localScale = Vector3.one * 0.7f;
		this.minionUI.transform.localPosition = new Vector3(this.minionUI.transform.localPosition.x - 73f, this.minionUI.transform.localPosition.y - 152f + 8f, this.minionUI.transform.localPosition.z);
	}

	// Token: 0x0600554B RID: 21835 RVA: 0x001EDB76 File Offset: 0x001EBD76
	public void ConfigureWith(PermitResource permit)
	{
		this.ConfigureWith(Option.Some<BalloonArtistFacadeResource>((BalloonArtistFacadeResource)permit));
	}

	// Token: 0x0600554C RID: 21836 RVA: 0x001EDB8C File Offset: 0x001EBD8C
	public void ConfigureWith(Option<BalloonArtistFacadeResource> permit)
	{
		KleiPermitDioramaVis_JoyResponseBalloon.<>c__DisplayClass10_0 CS$<>8__locals1 = new KleiPermitDioramaVis_JoyResponseBalloon.<>c__DisplayClass10_0();
		CS$<>8__locals1.permit = permit;
		KBatchedAnimController component = this.minionUI.SpawnedAvatar.GetComponent<KBatchedAnimController>();
		CS$<>8__locals1.minionSymbolOverrider = this.minionUI.SpawnedAvatar.GetComponent<SymbolOverrideController>();
		this.minionUI.SetMinion(this.specificPersonality.UnwrapOrElse(() => (from p in Db.Get().Personalities.GetAll(true, true)
			where p.joyTrait == "BalloonArtist"
			select p).GetRandom<Personality>(), null));
		if (!this.didAddAnims)
		{
			this.didAddAnims = true;
			component.AddAnimOverrides(Assets.GetAnim("anim_interacts_balloon_artist_kanim"), 0f);
		}
		component.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
		CS$<>8__locals1.<ConfigureWith>g__DisplayNextBalloon|3();
		Updater[] array = new Updater[2];
		array[0] = Updater.WaitForSeconds(1.3f);
		int num = 1;
		Func<Updater>[] array2 = new Func<Updater>[2];
		array2[0] = () => Updater.WaitForSeconds(1.618f);
		array2[1] = () => Updater.Do(new System.Action(base.<ConfigureWith>g__DisplayNextBalloon|3));
		array[num] = Updater.Loop(array2);
		this.QueueUpdater(Updater.Series(array));
	}

	// Token: 0x0600554D RID: 21837 RVA: 0x001EDCCD File Offset: 0x001EBECD
	public void SetMinion(Personality personality)
	{
		this.specificPersonality = personality;
		if (base.gameObject.activeInHierarchy)
		{
			this.minionUI.SetMinion(personality);
		}
	}

	// Token: 0x0600554E RID: 21838 RVA: 0x001EDCF4 File Offset: 0x001EBEF4
	private void QueueUpdater(Updater updater)
	{
		if (base.gameObject.activeInHierarchy)
		{
			this.RunUpdater(updater);
			return;
		}
		this.updaterToRunOnStart = updater;
	}

	// Token: 0x0600554F RID: 21839 RVA: 0x001EDD17 File Offset: 0x001EBF17
	private void RunUpdater(Updater updater)
	{
		if (this.updaterRoutine != null)
		{
			base.StopCoroutine(this.updaterRoutine);
			this.updaterRoutine = null;
		}
		this.updaterRoutine = base.StartCoroutine(updater);
	}

	// Token: 0x06005550 RID: 21840 RVA: 0x001EDD46 File Offset: 0x001EBF46
	private void OnEnable()
	{
		if (this.updaterToRunOnStart.IsSome())
		{
			this.RunUpdater(this.updaterToRunOnStart.Unwrap());
			this.updaterToRunOnStart = Option.None;
		}
	}

	// Token: 0x040039EF RID: 14831
	private const int FRAMES_TO_MAKE_BALLOON_IN_ANIM = 39;

	// Token: 0x040039F0 RID: 14832
	private const float SECONDS_TO_MAKE_BALLOON_IN_ANIM = 1.3f;

	// Token: 0x040039F1 RID: 14833
	private const float SECONDS_BETWEEN_BALLOONS = 1.618f;

	// Token: 0x040039F2 RID: 14834
	[SerializeField]
	private UIMinion minionUI;

	// Token: 0x040039F3 RID: 14835
	private bool didAddAnims;

	// Token: 0x040039F4 RID: 14836
	private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

	// Token: 0x040039F5 RID: 14837
	private const int TARGET_OVERRIDE_PRIORITY = 0;

	// Token: 0x040039F6 RID: 14838
	private Option<Personality> specificPersonality;

	// Token: 0x040039F7 RID: 14839
	private Option<PermitResource> lastConfiguredPermit;

	// Token: 0x040039F8 RID: 14840
	private Option<Updater> updaterToRunOnStart;

	// Token: 0x040039F9 RID: 14841
	private Coroutine updaterRoutine;
}
