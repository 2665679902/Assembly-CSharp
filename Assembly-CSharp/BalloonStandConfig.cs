using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class BalloonStandConfig : IEntityConfig
{
	// Token: 0x06000B81 RID: 2945 RVA: 0x00041774 File Offset: 0x0003F974
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0004177C File Offset: 0x0003F97C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(BalloonStandConfig.ID, BalloonStandConfig.ID, false);
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim("anim_interacts_balloon_receiver_kanim") };
		GetBalloonWorkable getBalloonWorkable = gameObject.AddOrGet<GetBalloonWorkable>();
		getBalloonWorkable.workTime = 2f;
		getBalloonWorkable.workLayer = Grid.SceneLayer.BuildingFront;
		getBalloonWorkable.overrideAnims = array;
		getBalloonWorkable.synchronizeAnims = false;
		return gameObject;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x000417DA File Offset: 0x0003F9DA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x000417DC File Offset: 0x0003F9DC
	public void OnSpawn(GameObject inst)
	{
		GetBalloonWorkable component = inst.GetComponent<GetBalloonWorkable>();
		WorkChore<GetBalloonWorkable> workChore = new WorkChore<GetBalloonWorkable>(Db.Get().ChoreTypes.JoyReaction, component, null, true, new Action<Chore>(this.MakeNewBalloonChore), null, null, true, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, true, PriorityScreen.PriorityClass.high, 5, true, true);
		workChore.AddPrecondition(this.HasNoBalloon, workChore);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, workChore);
		component.GetBalloonArtist().NextBalloonOverride();
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0004185C File Offset: 0x0003FA5C
	private void MakeNewBalloonChore(Chore chore)
	{
		GetBalloonWorkable component = chore.target.GetComponent<GetBalloonWorkable>();
		WorkChore<GetBalloonWorkable> workChore = new WorkChore<GetBalloonWorkable>(Db.Get().ChoreTypes.JoyReaction, component, null, true, new Action<Chore>(this.MakeNewBalloonChore), null, null, true, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, true, PriorityScreen.PriorityClass.high, 5, true, true);
		workChore.AddPrecondition(this.HasNoBalloon, workChore);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, workChore);
		component.GetBalloonArtist().NextBalloonOverride();
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x000418E0 File Offset: 0x0003FAE0
	public BalloonStandConfig()
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "HasNoBalloon";
		precondition.description = "Duplicant doesn't have a balloon already";
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !(context.consumerState.consumer == null) && !context.consumerState.gameObject.GetComponent<Effects>().HasEffect("HasBalloon");
		};
		this.HasNoBalloon = precondition;
		base..ctor();
	}

	// Token: 0x040006D9 RID: 1753
	public static readonly string ID = "BalloonStand";

	// Token: 0x040006DA RID: 1754
	private Chore.Precondition HasNoBalloon;
}
