using System;
using System.Collections.Generic;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000D15 RID: 3349
	public class ModErrorsScreen : KScreen
	{
		// Token: 0x060067D3 RID: 26579 RVA: 0x00281C04 File Offset: 0x0027FE04
		public static bool ShowErrors(List<Event> events)
		{
			if (Global.Instance.modManager.events.Count == 0)
			{
				return false;
			}
			GameObject gameObject = GameObject.Find("Canvas");
			ModErrorsScreen modErrorsScreen = Util.KInstantiateUI<ModErrorsScreen>(Global.Instance.modErrorsPrefab, gameObject, false);
			modErrorsScreen.Initialize(events);
			modErrorsScreen.gameObject.SetActive(true);
			return true;
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x00281C58 File Offset: 0x0027FE58
		private void Initialize(List<Event> events)
		{
			foreach (Event @event in events)
			{
				HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.entryPrefab, this.entryParent.gameObject, true);
				LocText reference = hierarchyReferences.GetReference<LocText>("Title");
				LocText reference2 = hierarchyReferences.GetReference<LocText>("Description");
				KButton reference3 = hierarchyReferences.GetReference<KButton>("Details");
				string text;
				string text2;
				Event.GetUIStrings(@event.event_type, out text, out text2);
				reference.text = text;
				reference.GetComponent<ToolTip>().toolTip = text2;
				reference2.text = @event.mod.title;
				ToolTip component = reference2.GetComponent<ToolTip>();
				if (component != null)
				{
					ToolTip toolTip = component;
					Label mod = @event.mod;
					toolTip.toolTip = mod.ToString();
				}
				reference3.isInteractable = false;
				Mod mod2 = Global.Instance.modManager.FindMod(@event.mod);
				if (mod2 != null)
				{
					if (component != null && !string.IsNullOrEmpty(mod2.description))
					{
						component.toolTip = mod2.description;
					}
					if (mod2.on_managed != null)
					{
						reference3.onClick += mod2.on_managed;
						reference3.isInteractable = true;
					}
				}
			}
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00281DB8 File Offset: 0x0027FFB8
		protected override void OnActivate()
		{
			base.OnActivate();
			this.closeButtonTitle.onClick += this.Deactivate;
			this.closeButton.onClick += this.Deactivate;
		}

		// Token: 0x04004C19 RID: 19481
		[SerializeField]
		private KButton closeButtonTitle;

		// Token: 0x04004C1A RID: 19482
		[SerializeField]
		private KButton closeButton;

		// Token: 0x04004C1B RID: 19483
		[SerializeField]
		private GameObject entryPrefab;

		// Token: 0x04004C1C RID: 19484
		[SerializeField]
		private Transform entryParent;
	}
}
