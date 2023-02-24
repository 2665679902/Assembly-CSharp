using System;
using STRINGS;
using UnityEngine;

// Token: 0x020006B8 RID: 1720
[AddComponentMenu("KMonoBehaviour/scripts/CopyBuildingSettings")]
public class CopyBuildingSettings : KMonoBehaviour
{
	// Token: 0x06002EC8 RID: 11976 RVA: 0x000F76FD File Offset: 0x000F58FD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<CopyBuildingSettings>(493375141, CopyBuildingSettings.OnRefreshUserMenuDelegate);
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x000F7718 File Offset: 0x000F5918
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_mirror", UI.USERMENUACTIONS.COPY_BUILDING_SETTINGS.NAME, new System.Action(this.ActivateCopyTool), global::Action.BuildingUtility1, null, null, null, UI.USERMENUACTIONS.COPY_BUILDING_SETTINGS.TOOLTIP, true), 1f);
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x000F7772 File Offset: 0x000F5972
	private void ActivateCopyTool()
	{
		CopySettingsTool.Instance.SetSourceObject(base.gameObject);
		PlayerController.Instance.ActivateTool(CopySettingsTool.Instance);
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x000F7794 File Offset: 0x000F5994
	public static bool ApplyCopy(int targetCell, GameObject sourceGameObject)
	{
		ObjectLayer objectLayer = ObjectLayer.Building;
		Building component = sourceGameObject.GetComponent<BuildingComplete>();
		if (component != null)
		{
			objectLayer = component.Def.ObjectLayer;
		}
		GameObject gameObject = Grid.Objects[targetCell, (int)objectLayer];
		if (gameObject == null)
		{
			return false;
		}
		if (gameObject == sourceGameObject)
		{
			return false;
		}
		KPrefabID component2 = sourceGameObject.GetComponent<KPrefabID>();
		if (component2 == null)
		{
			return false;
		}
		KPrefabID component3 = gameObject.GetComponent<KPrefabID>();
		if (component3 == null)
		{
			return false;
		}
		CopyBuildingSettings component4 = sourceGameObject.GetComponent<CopyBuildingSettings>();
		if (component4 == null)
		{
			return false;
		}
		CopyBuildingSettings component5 = gameObject.GetComponent<CopyBuildingSettings>();
		if (component5 == null)
		{
			return false;
		}
		if (component4.copyGroupTag != Tag.Invalid)
		{
			if (component4.copyGroupTag != component5.copyGroupTag)
			{
				return false;
			}
		}
		else if (component3.PrefabID() != component2.PrefabID())
		{
			return false;
		}
		component3.Trigger(-905833192, sourceGameObject);
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, UI.COPIED_SETTINGS, gameObject.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
		return true;
	}

	// Token: 0x04001C35 RID: 7221
	[MyCmpReq]
	private KPrefabID id;

	// Token: 0x04001C36 RID: 7222
	public Tag copyGroupTag;

	// Token: 0x04001C37 RID: 7223
	private static readonly EventSystem.IntraObjectHandler<CopyBuildingSettings> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<CopyBuildingSettings>(delegate(CopyBuildingSettings component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
