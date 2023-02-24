using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000A0D RID: 2573
public class MinionSelectScreen : CharacterSelectionController
{
	// Token: 0x06004D98 RID: 19864 RVA: 0x001B5ED8 File Offset: 0x001B40D8
	protected override void OnPrefabInit()
	{
		base.IsStarterMinion = true;
		base.OnPrefabInit();
		if (MusicManager.instance.SongIsPlaying("Music_FrontEnd"))
		{
			MusicManager.instance.SetSongParameter("Music_FrontEnd", "songSection", 2f, true);
		}
		GameObject gameObject = GameObject.Find("ScreenSpaceOverlayCanvas");
		GameObject gameObject2 = global::Util.KInstantiateUI(this.wattsonMessagePrefab.gameObject, gameObject, false);
		gameObject2.name = "WattsonMessage";
		gameObject2.SetActive(false);
		Game.Instance.Subscribe(-1992507039, new Action<object>(this.OnBaseAlreadyCreated));
		this.backButton.onClick += delegate
		{
			LoadScreen.ForceStopGame();
			App.LoadScene("frontend");
		};
		this.InitializeContainers();
	}

	// Token: 0x06004D99 RID: 19865 RVA: 0x001B5F98 File Offset: 0x001B4198
	public void SetProceedButtonActive(bool state, string tooltip = null)
	{
		if (state)
		{
			base.EnableProceedButton();
		}
		else
		{
			base.DisableProceedButton();
		}
		ToolTip component = this.proceedButton.GetComponent<ToolTip>();
		if (component != null)
		{
			if (tooltip != null)
			{
				component.toolTip = tooltip;
				return;
			}
			component.ClearMultiStringTooltip();
		}
	}

	// Token: 0x06004D9A RID: 19866 RVA: 0x001B5FDC File Offset: 0x001B41DC
	protected override void OnSpawn()
	{
		this.OnDeliverableAdded();
		base.EnableProceedButton();
		this.proceedButton.GetComponentInChildren<LocText>().text = UI.IMMIGRANTSCREEN.EMBARK;
		this.containers.ForEach(delegate(ITelepadDeliverableContainer container)
		{
			CharacterContainer characterContainer = container as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.DisableSelectButton();
			}
		});
	}

	// Token: 0x06004D9B RID: 19867 RVA: 0x001B603C File Offset: 0x001B423C
	protected override void OnProceed()
	{
		global::Util.KInstantiateUI(this.newBasePrefab.gameObject, GameScreenManager.Instance.ssOverlayCanvas, false);
		MusicManager.instance.StopSong("Music_FrontEnd", true, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().NewBaseSetupSnapshot);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndWorldGenerationSnapshot, STOP_MODE.ALLOWFADEOUT);
		this.selectedDeliverables.Clear();
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = (CharacterContainer)telepadDeliverableContainer;
			this.selectedDeliverables.Add(characterContainer.Stats);
		}
		NewBaseScreen.Instance.Init(SaveLoader.Instance.ClusterLayout, this.selectedDeliverables.ToArray());
		if (this.OnProceedEvent != null)
		{
			this.OnProceedEvent();
		}
		Game.Instance.Trigger(-838649377, null);
		BuildWatermark.Instance.gameObject.SetActive(false);
		this.Deactivate();
	}

	// Token: 0x06004D9C RID: 19868 RVA: 0x001B615C File Offset: 0x001B435C
	private void OnBaseAlreadyCreated(object data)
	{
		Game.Instance.StopFE();
		Game.Instance.StartBE();
		Game.Instance.SetGameStarted();
		this.Deactivate();
	}

	// Token: 0x06004D9D RID: 19869 RVA: 0x001B6182 File Offset: 0x001B4382
	private void ReshuffleAll()
	{
		if (this.OnReshuffleEvent != null)
		{
			this.OnReshuffleEvent(base.IsStarterMinion);
		}
	}

	// Token: 0x06004D9E RID: 19870 RVA: 0x001B61A0 File Offset: 0x001B43A0
	public override void OnPressBack()
	{
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = telepadDeliverableContainer as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.ForceStopEditingTitle();
			}
		}
	}

	// Token: 0x04003326 RID: 13094
	[SerializeField]
	private NewBaseScreen newBasePrefab;

	// Token: 0x04003327 RID: 13095
	[SerializeField]
	private WattsonMessage wattsonMessagePrefab;

	// Token: 0x04003328 RID: 13096
	public const string WattsonGameObjName = "WattsonMessage";

	// Token: 0x04003329 RID: 13097
	public KButton backButton;
}
