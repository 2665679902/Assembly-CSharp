using System;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000B74 RID: 2932
[AddComponentMenu("KMonoBehaviour/scripts/RocketThrustWidget")]
public class RocketThrustWidget : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06005BFC RID: 23548 RVA: 0x00219902 File Offset: 0x00217B02
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x06005BFD RID: 23549 RVA: 0x00219904 File Offset: 0x00217B04
	public void Draw(CommandModule commandModule)
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = this.graphBar.gameObject.GetComponent<RectTransform>();
		}
		this.commandModule = commandModule;
		this.totalWidth = this.rectTransform.rect.width;
		this.UpdateGraphDotPos(commandModule);
	}

	// Token: 0x06005BFE RID: 23550 RVA: 0x0021995C File Offset: 0x00217B5C
	private void UpdateGraphDotPos(CommandModule rocket)
	{
		this.totalWidth = this.rectTransform.rect.width;
		float num = Mathf.Lerp(0f, this.totalWidth, rocket.rocketStats.GetTotalMass() / this.maxMass);
		num = Mathf.Clamp(num, 0f, this.totalWidth);
		this.graphDot.rectTransform.SetLocalPosition(new Vector3(num, 0f, 0f));
		this.graphDotText.text = "-" + Util.FormatWholeNumber(rocket.rocketStats.GetTotalThrust() - rocket.rocketStats.GetRocketMaxDistance()) + "km";
	}

	// Token: 0x06005BFF RID: 23551 RVA: 0x00219A10 File Offset: 0x00217C10
	private void Update()
	{
		if (this.mouseOver)
		{
			if (this.rectTransform == null)
			{
				this.rectTransform = this.graphBar.gameObject.GetComponent<RectTransform>();
			}
			Vector3 position = this.rectTransform.GetPosition();
			Vector2 size = this.rectTransform.rect.size;
			float num = KInputManager.GetMousePos().x - position.x + size.x / 2f;
			num = Mathf.Clamp(num, 0f, this.totalWidth);
			this.hoverMarker.rectTransform.SetLocalPosition(new Vector3(num, 0f, 0f));
			float num2 = Mathf.Lerp(0f, this.maxMass, num / this.totalWidth);
			float totalThrust = this.commandModule.rocketStats.GetTotalThrust();
			float rocketMaxDistance = this.commandModule.rocketStats.GetRocketMaxDistance();
			this.hoverTooltip.SetSimpleTooltip(string.Concat(new string[]
			{
				UI.STARMAP.ROCKETWEIGHT.MASS,
				GameUtil.GetFormattedMass(num2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"),
				"\n",
				UI.STARMAP.ROCKETWEIGHT.MASSPENALTY,
				Util.FormatWholeNumber(ROCKETRY.CalculateMassWithPenalty(num2)),
				UI.UNITSUFFIXES.DISTANCE.KILOMETER,
				"\n\n",
				UI.STARMAP.ROCKETWEIGHT.CURRENTMASS,
				GameUtil.GetFormattedMass(this.commandModule.rocketStats.GetTotalMass(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"),
				"\n",
				UI.STARMAP.ROCKETWEIGHT.CURRENTMASSPENALTY,
				Util.FormatWholeNumber(totalThrust - rocketMaxDistance),
				UI.UNITSUFFIXES.DISTANCE.KILOMETER
			}));
		}
	}

	// Token: 0x06005C00 RID: 23552 RVA: 0x00219BC9 File Offset: 0x00217DC9
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver = true;
		this.hoverMarker.SetAlpha(1f);
	}

	// Token: 0x06005C01 RID: 23553 RVA: 0x00219BE2 File Offset: 0x00217DE2
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseOver = false;
		this.hoverMarker.SetAlpha(0f);
	}

	// Token: 0x04003ECA RID: 16074
	public Image graphBar;

	// Token: 0x04003ECB RID: 16075
	public Image graphDot;

	// Token: 0x04003ECC RID: 16076
	public LocText graphDotText;

	// Token: 0x04003ECD RID: 16077
	public Image hoverMarker;

	// Token: 0x04003ECE RID: 16078
	public ToolTip hoverTooltip;

	// Token: 0x04003ECF RID: 16079
	public RectTransform markersContainer;

	// Token: 0x04003ED0 RID: 16080
	public Image markerTemplate;

	// Token: 0x04003ED1 RID: 16081
	private RectTransform rectTransform;

	// Token: 0x04003ED2 RID: 16082
	private float maxMass = 20000f;

	// Token: 0x04003ED3 RID: 16083
	private float totalWidth = 5f;

	// Token: 0x04003ED4 RID: 16084
	private bool mouseOver;

	// Token: 0x04003ED5 RID: 16085
	public CommandModule commandModule;
}
