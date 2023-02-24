using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020004F6 RID: 1270
public class BipedTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06001DE8 RID: 7656 RVA: 0x0009F4A0 File Offset: 0x0009D6A0
	public BipedTransitionLayer(Navigator navigator, float floor_speed, float ladder_speed)
		: base(navigator)
	{
		navigator.Subscribe(1773898642, delegate(object data)
		{
			this.isWalking = true;
		});
		navigator.Subscribe(1597112836, delegate(object data)
		{
			this.isWalking = false;
		});
		this.floorSpeed = floor_speed;
		this.ladderSpeed = ladder_speed;
		this.jetPackSpeed = floor_speed;
		this.movementSpeed = Db.Get().AttributeConverters.MovementSpeed.Lookup(navigator.gameObject);
		this.attributeLevels = navigator.GetComponent<AttributeLevels>();
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x0009F528 File Offset: 0x0009D728
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		float num = 1f;
		bool flag = (transition.start == NavType.Pole || transition.end == NavType.Pole) && transition.y < 0 && transition.x == 0;
		bool flag2 = transition.start == NavType.Tube || transition.end == NavType.Tube;
		bool flag3 = transition.start == NavType.Hover || transition.end == NavType.Hover;
		if (!flag && !flag2 && !flag3)
		{
			if (this.isWalking)
			{
				return;
			}
			num = this.GetMovementSpeedMultiplier(navigator);
		}
		int num2 = Grid.PosToCell(navigator);
		float num3 = 1f;
		bool flag4 = (navigator.flags & PathFinder.PotentialPath.Flags.HasAtmoSuit) > PathFinder.PotentialPath.Flags.None;
		if ((navigator.flags & PathFinder.PotentialPath.Flags.HasJetPack) <= PathFinder.PotentialPath.Flags.None && !flag4 && Grid.IsSubstantialLiquid(num2, 0.35f))
		{
			num3 = 0.5f;
		}
		num *= num3;
		if (transition.x == 0 && (transition.start == NavType.Ladder || transition.start == NavType.Pole) && transition.start == transition.end)
		{
			if (flag)
			{
				transition.speed = 15f * num3;
			}
			else
			{
				transition.speed = this.ladderSpeed * num;
				GameObject gameObject = Grid.Objects[num2, 1];
				if (gameObject != null)
				{
					Ladder component = gameObject.GetComponent<Ladder>();
					if (component != null)
					{
						float num4 = component.upwardsMovementSpeedMultiplier;
						if (transition.y < 0)
						{
							num4 = component.downwardsMovementSpeedMultiplier;
						}
						transition.speed *= num4;
						transition.animSpeed *= num4;
					}
				}
			}
		}
		else if (flag2)
		{
			transition.speed = 18f;
		}
		else if (flag3)
		{
			transition.speed = this.jetPackSpeed;
		}
		else
		{
			transition.speed = this.floorSpeed * num;
		}
		float num5 = num - 1f;
		transition.animSpeed += transition.animSpeed * num5 / 2f;
		if (transition.start == NavType.Floor && transition.end == NavType.Floor)
		{
			int num6 = Grid.CellBelow(num2);
			if (Grid.Foundation[num6])
			{
				GameObject gameObject2 = Grid.Objects[num6, 1];
				if (gameObject2 != null)
				{
					SimCellOccupier component2 = gameObject2.GetComponent<SimCellOccupier>();
					if (component2 != null)
					{
						transition.speed *= component2.movementSpeedMultiplier;
						transition.animSpeed *= component2.movementSpeedMultiplier;
					}
				}
			}
		}
		this.startTime = Time.time;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x0009F794 File Offset: 0x0009D994
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		bool flag = (transition.start == NavType.Pole || transition.end == NavType.Pole) && transition.y < 0 && transition.x == 0;
		bool flag2 = transition.start == NavType.Tube || transition.end == NavType.Tube;
		if (!this.isWalking && !flag && !flag2 && this.attributeLevels != null)
		{
			this.attributeLevels.AddExperience(Db.Get().Attributes.Athletics.Id, Time.time - this.startTime, DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE);
		}
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x0009F834 File Offset: 0x0009DA34
	public float GetMovementSpeedMultiplier(Navigator navigator)
	{
		float num = 1f;
		if (this.movementSpeed != null)
		{
			num += this.movementSpeed.Evaluate();
		}
		return Mathf.Max(0.1f, num);
	}

	// Token: 0x040010C0 RID: 4288
	private bool isWalking;

	// Token: 0x040010C1 RID: 4289
	private float floorSpeed;

	// Token: 0x040010C2 RID: 4290
	private float ladderSpeed;

	// Token: 0x040010C3 RID: 4291
	private float startTime;

	// Token: 0x040010C4 RID: 4292
	private float jetPackSpeed;

	// Token: 0x040010C5 RID: 4293
	private const float tubeSpeed = 18f;

	// Token: 0x040010C6 RID: 4294
	private const float downPoleSpeed = 15f;

	// Token: 0x040010C7 RID: 4295
	private const float WATER_SPEED_PENALTY = 0.5f;

	// Token: 0x040010C8 RID: 4296
	private AttributeConverterInstance movementSpeed;

	// Token: 0x040010C9 RID: 4297
	private AttributeLevels attributeLevels;
}
