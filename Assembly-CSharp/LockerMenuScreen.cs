using System;
using Database;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000AEA RID: 2794
public class LockerMenuScreen : KModalScreen
{
	// Token: 0x060055A4 RID: 21924 RVA: 0x001EF5B5 File Offset: 0x001ED7B5
	protected override void OnActivate()
	{
		LockerMenuScreen.Instance = this;
		this.Show(false);
	}

	// Token: 0x060055A5 RID: 21925 RVA: 0x001EF5C4 File Offset: 0x001ED7C4
	public override float GetSortKey()
	{
		return 40f;
	}

	// Token: 0x060055A6 RID: 21926 RVA: 0x001EF5CC File Offset: 0x001ED7CC
	protected override void OnPrefabInit()
	{
		LockerMenuScreen.<>c__DisplayClass13_0 CS$<>8__locals1 = new LockerMenuScreen.<>c__DisplayClass13_0();
		CS$<>8__locals1.<>4__this = this;
		base.OnPrefabInit();
		MultiToggle multiToggle = this.buttonInventory;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.kleiInventoryScreen, null);
			MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "inventory", true);
		}));
		MultiToggle multiToggle2 = this.buttonDuplicants;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
		{
			MinionBrowserScreenConfig.Personalities(default(Option<Personality>)).ApplyAndOpenScreen(null);
			MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "dupe", true);
		}));
		MultiToggle multiToggle3 = this.buttonOutfitBroswer;
		multiToggle3.onClick = (System.Action)Delegate.Combine(multiToggle3.onClick, new System.Action(delegate
		{
			OutfitBrowserScreenConfig.Mannequin().ApplyAndOpenScreen();
			MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "wardrobe", true);
		}));
		MultiToggle multiToggle4 = this.buttonClaimItems;
		multiToggle4.onClick = (System.Action)Delegate.Combine(multiToggle4.onClick, new System.Action(delegate
		{
			KleiItemsStatusRefresher.Active = true;
			string text = "https://accounts.klei.com/account/rewards?game=ONI";
			if (KleiAccount.KleiUserID != null)
			{
				text = text + "&expectedKU=" + KleiAccount.KleiUserID;
			}
			Application.OpenURL(text);
			LockerMenuScreen.shouldPreventDisplayingDropsAvailableNotification = true;
			CS$<>8__locals1.<>4__this.dropsAvailableNotification.SetActive(false);
		}));
		this.closeButton.onClick += delegate
		{
			CS$<>8__locals1.<>4__this.Show(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.OnSupplyClosetMenu(false, 1f);
			if (MusicManager.instance.SongIsPlaying("Music_SupplyCloset"))
			{
				MusicManager.instance.StopSong("Music_SupplyCloset", true, STOP_MODE.ALLOWFADEOUT);
			}
		};
		CS$<>8__locals1.defaultColor = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		CS$<>8__locals1.hoverColor = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		CS$<>8__locals1.<OnPrefabInit>g__ConfigureHoverFor|1(this.buttonInventory, UI.LOCKER_MENU.BUTTON_INVENTORY_DESCRIPTION);
		CS$<>8__locals1.<OnPrefabInit>g__ConfigureHoverFor|1(this.buttonDuplicants, UI.LOCKER_MENU.BUTTON_DUPLICANTS_DESCRIPTION);
		CS$<>8__locals1.<OnPrefabInit>g__ConfigureHoverFor|1(this.buttonOutfitBroswer, UI.LOCKER_MENU.BUTTON_OUTFITS_DESCRIPTION);
		CS$<>8__locals1.<OnPrefabInit>g__ConfigureHoverFor|1(this.buttonClaimItems, UI.LOCKER_MENU.BUTTON_CLAIM_DESCRIPTION);
		this.descriptionArea.text = UI.LOCKER_MENU.DEFAULT_DESCRIPTION;
	}

	// Token: 0x060055A7 RID: 21927 RVA: 0x001EF784 File Offset: 0x001ED984
	public override void Show(bool show = true)
	{
		base.Show(show);
		if (show)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot);
			MusicManager.instance.OnSupplyClosetMenu(true, 0.5f);
			MusicManager.instance.PlaySong("Music_SupplyCloset", false);
		}
		if (LockerMenuScreen.shouldPreventDisplayingDropsAvailableNotification)
		{
			this.dropsAvailableNotification.SetActive(false);
			return;
		}
		this.dropsAvailableNotification.SetActive(this.AreAllOwnablePermitsLocked());
	}

	// Token: 0x060055A8 RID: 21928 RVA: 0x001EF7F8 File Offset: 0x001ED9F8
	private bool AreAllOwnablePermitsLocked()
	{
		foreach (PermitResource permitResource in Db.Get().Permits.resources)
		{
			if (permitResource.IsOwnable() && permitResource.IsUnlocked())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060055A9 RID: 21929 RVA: 0x001EF864 File Offset: 0x001EDA64
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.OnSupplyClosetMenu(false, 1f);
			if (MusicManager.instance.SongIsPlaying("Music_SupplyCloset"))
			{
				MusicManager.instance.StopSong("Music_SupplyCloset", true, STOP_MODE.ALLOWFADEOUT);
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x04003A25 RID: 14885
	public static LockerMenuScreen Instance;

	// Token: 0x04003A26 RID: 14886
	[SerializeField]
	private MultiToggle buttonInventory;

	// Token: 0x04003A27 RID: 14887
	[SerializeField]
	private MultiToggle buttonDuplicants;

	// Token: 0x04003A28 RID: 14888
	[SerializeField]
	private MultiToggle buttonOutfitBroswer;

	// Token: 0x04003A29 RID: 14889
	[SerializeField]
	private MultiToggle buttonClaimItems;

	// Token: 0x04003A2A RID: 14890
	[SerializeField]
	private LocText descriptionArea;

	// Token: 0x04003A2B RID: 14891
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003A2C RID: 14892
	[SerializeField]
	private GameObject dropsAvailableNotification;

	// Token: 0x04003A2D RID: 14893
	private const string REDEEM_MYSTERY_BOX_URL = "https://accounts.klei.com/account/rewards?game=ONI";

	// Token: 0x04003A2E RID: 14894
	private const string LOCKER_MENU_MUSIC = "Music_SupplyCloset";

	// Token: 0x04003A2F RID: 14895
	private const string MUSIC_PARAMETER = "SupplyClosetView";

	// Token: 0x04003A30 RID: 14896
	private static bool shouldPreventDisplayingDropsAvailableNotification;
}
