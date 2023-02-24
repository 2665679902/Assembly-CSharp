using System;
using STRINGS;

// Token: 0x02000941 RID: 2369
public class ClustercraftInteriorDoor : KMonoBehaviour, ISidescreenButtonControl
{
	// Token: 0x1700050B RID: 1291
	// (get) Token: 0x060045BF RID: 17855 RVA: 0x00188C82 File Offset: 0x00186E82
	public string SidescreenButtonText
	{
		get
		{
			return UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.LABEL;
		}
	}

	// Token: 0x1700050C RID: 1292
	// (get) Token: 0x060045C0 RID: 17856 RVA: 0x00188C8E File Offset: 0x00186E8E
	public string SidescreenButtonTooltip
	{
		get
		{
			return this.SidescreenButtonInteractable() ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.LABEL : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.INVALID;
		}
	}

	// Token: 0x060045C1 RID: 17857 RVA: 0x00188CA9 File Offset: 0x00186EA9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.ClusterCraftInteriorDoors.Add(this);
	}

	// Token: 0x060045C2 RID: 17858 RVA: 0x00188CBC File Offset: 0x00186EBC
	protected override void OnCleanUp()
	{
		Components.ClusterCraftInteriorDoors.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060045C3 RID: 17859 RVA: 0x00188CCF File Offset: 0x00186ECF
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x060045C4 RID: 17860 RVA: 0x00188CD4 File Offset: 0x00186ED4
	public bool SidescreenButtonInteractable()
	{
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		return myWorld.ParentWorldId != (int)ClusterManager.INVALID_WORLD_IDX && myWorld.ParentWorldId != myWorld.id;
	}

	// Token: 0x060045C5 RID: 17861 RVA: 0x00188D0D File Offset: 0x00186F0D
	public void OnSidescreenButtonPressed()
	{
		ClusterManager.Instance.SetActiveWorld(base.gameObject.GetMyWorld().ParentWorldId);
	}

	// Token: 0x060045C6 RID: 17862 RVA: 0x00188D29 File Offset: 0x00186F29
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x060045C7 RID: 17863 RVA: 0x00188D2D File Offset: 0x00186F2D
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}
}
