using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000A07 RID: 2567
public class ImmigrantScreen : CharacterSelectionController
{
	// Token: 0x06004D0A RID: 19722 RVA: 0x001B1B14 File Offset: 0x001AFD14
	public static void DestroyInstance()
	{
		ImmigrantScreen.instance = null;
	}

	// Token: 0x170005C3 RID: 1475
	// (get) Token: 0x06004D0B RID: 19723 RVA: 0x001B1B1C File Offset: 0x001AFD1C
	public Telepad Telepad
	{
		get
		{
			return this.telepad;
		}
	}

	// Token: 0x06004D0C RID: 19724 RVA: 0x001B1B24 File Offset: 0x001AFD24
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06004D0D RID: 19725 RVA: 0x001B1B2C File Offset: 0x001AFD2C
	protected override void OnSpawn()
	{
		this.activateOnSpawn = false;
		base.ConsumeMouseScroll = false;
		base.OnSpawn();
		base.IsStarterMinion = false;
		this.rejectButton.onClick += this.OnRejectAll;
		this.confirmRejectionBtn.onClick += this.OnRejectionConfirmed;
		this.cancelRejectionBtn.onClick += this.OnRejectionCancelled;
		ImmigrantScreen.instance = this;
		this.title.text = UI.IMMIGRANTSCREEN.IMMIGRANTSCREENTITLE;
		this.proceedButton.GetComponentInChildren<LocText>().text = UI.IMMIGRANTSCREEN.PROCEEDBUTTON;
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.Show(false);
	}

	// Token: 0x06004D0E RID: 19726 RVA: 0x001B1BEC File Offset: 0x001AFDEC
	protected override void OnShow(bool show)
	{
		if (show)
		{
			KFMOD.PlayUISound(GlobalAssets.GetSound("Dialog_Popup", false));
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot);
			MusicManager.instance.PlaySong("Music_SelectDuplicant", false);
			this.hasShown = true;
		}
		else
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot, STOP_MODE.ALLOWFADEOUT);
			if (MusicManager.instance.SongIsPlaying("Music_SelectDuplicant"))
			{
				MusicManager.instance.StopSong("Music_SelectDuplicant", true, STOP_MODE.ALLOWFADEOUT);
			}
			if (Immigration.Instance.ImmigrantsAvailable && this.hasShown)
			{
				AudioMixer.instance.Start(AudioMixerSnapshots.Get().PortalLPDimmedSnapshot);
			}
		}
		base.OnShow(show);
	}

	// Token: 0x06004D0F RID: 19727 RVA: 0x001B1CA2 File Offset: 0x001AFEA2
	public void DebugShuffleOptions()
	{
		this.OnRejectionConfirmed();
		Immigration.Instance.timeBeforeSpawn = 0f;
	}

	// Token: 0x06004D10 RID: 19728 RVA: 0x001B1CB9 File Offset: 0x001AFEB9
	public override void OnPressBack()
	{
		if (this.rejectConfirmationScreen.activeSelf)
		{
			this.OnRejectionCancelled();
			return;
		}
		base.OnPressBack();
	}

	// Token: 0x06004D11 RID: 19729 RVA: 0x001B1CD5 File Offset: 0x001AFED5
	public override void Deactivate()
	{
		this.Show(false);
	}

	// Token: 0x06004D12 RID: 19730 RVA: 0x001B1CDE File Offset: 0x001AFEDE
	public static void InitializeImmigrantScreen(Telepad telepad)
	{
		ImmigrantScreen.instance.Initialize(telepad);
		ImmigrantScreen.instance.Show(true);
	}

	// Token: 0x06004D13 RID: 19731 RVA: 0x001B1CF8 File Offset: 0x001AFEF8
	private void Initialize(Telepad telepad)
	{
		this.InitializeContainers();
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = telepadDeliverableContainer as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.SetReshufflingState(false);
			}
		}
		this.telepad = telepad;
	}

	// Token: 0x06004D14 RID: 19732 RVA: 0x001B1D68 File Offset: 0x001AFF68
	protected override void OnProceed()
	{
		this.telepad.OnAcceptDelivery(this.selectedDeliverables[0]);
		this.Show(false);
		this.containers.ForEach(delegate(ITelepadDeliverableContainer cc)
		{
			UnityEngine.Object.Destroy(cc.GetGameObject());
		});
		this.containers.Clear();
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().PortalLPDimmedSnapshot, STOP_MODE.ALLOWFADEOUT);
		MusicManager.instance.PlaySong("Stinger_NewDuplicant", false);
	}

	// Token: 0x06004D15 RID: 19733 RVA: 0x001B1E04 File Offset: 0x001B0004
	private void OnRejectAll()
	{
		this.rejectConfirmationScreen.transform.SetAsLastSibling();
		this.rejectConfirmationScreen.SetActive(true);
	}

	// Token: 0x06004D16 RID: 19734 RVA: 0x001B1E22 File Offset: 0x001B0022
	private void OnRejectionCancelled()
	{
		this.rejectConfirmationScreen.SetActive(false);
	}

	// Token: 0x06004D17 RID: 19735 RVA: 0x001B1E30 File Offset: 0x001B0030
	private void OnRejectionConfirmed()
	{
		this.telepad.RejectAll();
		this.containers.ForEach(delegate(ITelepadDeliverableContainer cc)
		{
			UnityEngine.Object.Destroy(cc.GetGameObject());
		});
		this.containers.Clear();
		this.rejectConfirmationScreen.SetActive(false);
		this.Show(false);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().PortalLPDimmedSnapshot, STOP_MODE.ALLOWFADEOUT);
	}

	// Token: 0x040032CD RID: 13005
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040032CE RID: 13006
	[SerializeField]
	private KButton rejectButton;

	// Token: 0x040032CF RID: 13007
	[SerializeField]
	private LocText title;

	// Token: 0x040032D0 RID: 13008
	[SerializeField]
	private GameObject rejectConfirmationScreen;

	// Token: 0x040032D1 RID: 13009
	[SerializeField]
	private KButton confirmRejectionBtn;

	// Token: 0x040032D2 RID: 13010
	[SerializeField]
	private KButton cancelRejectionBtn;

	// Token: 0x040032D3 RID: 13011
	public static ImmigrantScreen instance;

	// Token: 0x040032D4 RID: 13012
	private Telepad telepad;

	// Token: 0x040032D5 RID: 13013
	private bool hasShown;
}
