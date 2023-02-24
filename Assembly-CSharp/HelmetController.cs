using System;
using UnityEngine;

// Token: 0x02000758 RID: 1880
[AddComponentMenu("KMonoBehaviour/scripts/HelmetController")]
public class HelmetController : KMonoBehaviour
{
	// Token: 0x060033C5 RID: 13253 RVA: 0x001169DD File Offset: 0x00114BDD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HelmetController>(-1617557748, HelmetController.OnEquippedDelegate);
		base.Subscribe<HelmetController>(-170173755, HelmetController.OnUnequippedDelegate);
	}

	// Token: 0x060033C6 RID: 13254 RVA: 0x00116A08 File Offset: 0x00114C08
	private KBatchedAnimController GetAssigneeController()
	{
		Equippable component = base.GetComponent<Equippable>();
		if (component.assignee != null)
		{
			GameObject assigneeGameObject = this.GetAssigneeGameObject(component.assignee);
			if (assigneeGameObject)
			{
				return assigneeGameObject.GetComponent<KBatchedAnimController>();
			}
		}
		return null;
	}

	// Token: 0x060033C7 RID: 13255 RVA: 0x00116A44 File Offset: 0x00114C44
	private GameObject GetAssigneeGameObject(IAssignableIdentity ass_id)
	{
		GameObject gameObject = null;
		MinionAssignablesProxy minionAssignablesProxy = ass_id as MinionAssignablesProxy;
		if (minionAssignablesProxy)
		{
			gameObject = minionAssignablesProxy.GetTargetGameObject();
		}
		else
		{
			MinionIdentity minionIdentity = ass_id as MinionIdentity;
			if (minionIdentity)
			{
				gameObject = minionIdentity.gameObject;
			}
		}
		return gameObject;
	}

	// Token: 0x060033C8 RID: 13256 RVA: 0x00116A84 File Offset: 0x00114C84
	private void OnEquipped(object data)
	{
		Equippable component = base.GetComponent<Equippable>();
		this.ShowHelmet();
		GameObject assigneeGameObject = this.GetAssigneeGameObject(component.assignee);
		assigneeGameObject.Subscribe(961737054, new Action<object>(this.OnBeginRecoverBreath));
		assigneeGameObject.Subscribe(-2037519664, new Action<object>(this.OnEndRecoverBreath));
		assigneeGameObject.Subscribe(1347184327, new Action<object>(this.OnPathAdvanced));
		this.in_tube = false;
		this.is_flying = false;
		this.owner_navigator = assigneeGameObject.GetComponent<Navigator>();
	}

	// Token: 0x060033C9 RID: 13257 RVA: 0x00116B10 File Offset: 0x00114D10
	private void OnUnequipped(object data)
	{
		this.owner_navigator = null;
		Equippable component = base.GetComponent<Equippable>();
		if (component != null)
		{
			this.HideHelmet();
			if (component.assignee != null)
			{
				GameObject assigneeGameObject = this.GetAssigneeGameObject(component.assignee);
				if (assigneeGameObject)
				{
					assigneeGameObject.Unsubscribe(961737054, new Action<object>(this.OnBeginRecoverBreath));
					assigneeGameObject.Unsubscribe(-2037519664, new Action<object>(this.OnEndRecoverBreath));
					assigneeGameObject.Unsubscribe(1347184327, new Action<object>(this.OnPathAdvanced));
				}
			}
		}
	}

	// Token: 0x060033CA RID: 13258 RVA: 0x00116B9C File Offset: 0x00114D9C
	private void ShowHelmet()
	{
		KBatchedAnimController assigneeController = this.GetAssigneeController();
		if (assigneeController == null)
		{
			return;
		}
		KAnimHashedString kanimHashedString = new KAnimHashedString("snapTo_neck");
		if (!string.IsNullOrEmpty(this.anim_file))
		{
			KAnimFile anim = Assets.GetAnim(this.anim_file);
			assigneeController.GetComponent<SymbolOverrideController>().AddSymbolOverride(kanimHashedString, anim.GetData().build.GetSymbol(kanimHashedString), 6);
		}
		assigneeController.SetSymbolVisiblity(kanimHashedString, true);
		this.is_shown = true;
		this.UpdateJets();
	}

	// Token: 0x060033CB RID: 13259 RVA: 0x00116C20 File Offset: 0x00114E20
	private void HideHelmet()
	{
		this.is_shown = false;
		KBatchedAnimController assigneeController = this.GetAssigneeController();
		if (assigneeController == null)
		{
			return;
		}
		KAnimHashedString kanimHashedString = "snapTo_neck";
		if (!string.IsNullOrEmpty(this.anim_file))
		{
			SymbolOverrideController component = assigneeController.GetComponent<SymbolOverrideController>();
			if (component == null)
			{
				return;
			}
			component.RemoveSymbolOverride(kanimHashedString, 6);
		}
		assigneeController.SetSymbolVisiblity(kanimHashedString, false);
		this.UpdateJets();
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x00116C8A File Offset: 0x00114E8A
	private void UpdateJets()
	{
		if (this.is_shown && this.is_flying)
		{
			this.EnableJets();
			return;
		}
		this.DisableJets();
	}

	// Token: 0x060033CD RID: 13261 RVA: 0x00116CAC File Offset: 0x00114EAC
	private void EnableJets()
	{
		if (!this.has_jets)
		{
			return;
		}
		if (this.jet_go)
		{
			return;
		}
		this.jet_go = this.AddTrackedAnim("jet", Assets.GetAnim("jetsuit_thruster_fx_kanim"), "loop", Grid.SceneLayer.Creatures, "snapTo_neck");
		this.glow_go = this.AddTrackedAnim("glow", Assets.GetAnim("jetsuit_thruster_glow_fx_kanim"), "loop", Grid.SceneLayer.Front, "snapTo_neck");
	}

	// Token: 0x060033CE RID: 13262 RVA: 0x00116D28 File Offset: 0x00114F28
	private void DisableJets()
	{
		if (!this.has_jets)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.jet_go);
		this.jet_go = null;
		UnityEngine.Object.Destroy(this.glow_go);
		this.glow_go = null;
	}

	// Token: 0x060033CF RID: 13263 RVA: 0x00116D58 File Offset: 0x00114F58
	private GameObject AddTrackedAnim(string name, KAnimFile tracked_anim_file, string anim_clip, Grid.SceneLayer layer, string symbol_name)
	{
		KBatchedAnimController assigneeController = this.GetAssigneeController();
		if (assigneeController == null)
		{
			return null;
		}
		string text = assigneeController.name + "." + name;
		GameObject gameObject = new GameObject(text);
		gameObject.SetActive(false);
		gameObject.transform.parent = assigneeController.transform;
		gameObject.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { tracked_anim_file };
		kbatchedAnimController.initialAnim = anim_clip;
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.sceneLayer = layer;
		gameObject.AddComponent<KBatchedAnimTracker>().symbol = symbol_name;
		bool flag;
		Vector3 vector = assigneeController.GetSymbolTransform(symbol_name, out flag).GetColumn(3);
		vector.z = Grid.GetLayerZ(layer);
		gameObject.transform.SetPosition(vector);
		gameObject.SetActive(true);
		kbatchedAnimController.Play(anim_clip, KAnim.PlayMode.Loop, 1f, 0f);
		return gameObject;
	}

	// Token: 0x060033D0 RID: 13264 RVA: 0x00116E53 File Offset: 0x00115053
	private void OnBeginRecoverBreath(object data)
	{
		this.HideHelmet();
	}

	// Token: 0x060033D1 RID: 13265 RVA: 0x00116E5B File Offset: 0x0011505B
	private void OnEndRecoverBreath(object data)
	{
		this.ShowHelmet();
	}

	// Token: 0x060033D2 RID: 13266 RVA: 0x00116E64 File Offset: 0x00115064
	private void OnPathAdvanced(object data)
	{
		if (this.owner_navigator == null)
		{
			return;
		}
		bool flag = this.owner_navigator.CurrentNavType == NavType.Hover;
		bool flag2 = this.owner_navigator.CurrentNavType == NavType.Tube;
		if (flag2 != this.in_tube)
		{
			this.in_tube = flag2;
			if (this.in_tube)
			{
				this.HideHelmet();
			}
			else
			{
				this.ShowHelmet();
			}
		}
		if (flag != this.is_flying)
		{
			this.is_flying = flag;
			this.UpdateJets();
		}
	}

	// Token: 0x04001FC4 RID: 8132
	public string anim_file;

	// Token: 0x04001FC5 RID: 8133
	public bool has_jets;

	// Token: 0x04001FC6 RID: 8134
	private bool is_shown;

	// Token: 0x04001FC7 RID: 8135
	private bool in_tube;

	// Token: 0x04001FC8 RID: 8136
	private bool is_flying;

	// Token: 0x04001FC9 RID: 8137
	private Navigator owner_navigator;

	// Token: 0x04001FCA RID: 8138
	private GameObject jet_go;

	// Token: 0x04001FCB RID: 8139
	private GameObject glow_go;

	// Token: 0x04001FCC RID: 8140
	private static readonly EventSystem.IntraObjectHandler<HelmetController> OnEquippedDelegate = new EventSystem.IntraObjectHandler<HelmetController>(delegate(HelmetController component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x04001FCD RID: 8141
	private static readonly EventSystem.IntraObjectHandler<HelmetController> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<HelmetController>(delegate(HelmetController component, object data)
	{
		component.OnUnequipped(data);
	});
}
