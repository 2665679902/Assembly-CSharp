using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class Bouncer : MonoBehaviour
{
	// Token: 0x06000318 RID: 792 RVA: 0x00010F3F File Offset: 0x0000F13F
	public void Bounce()
	{
		if (base.gameObject.activeInHierarchy && !this.m_bouncing)
		{
			base.StartCoroutine(this.DoBounce());
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00010F63 File Offset: 0x0000F163
	public bool IsBouncing()
	{
		return this.m_bouncing;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00010F6B File Offset: 0x0000F16B
	private IEnumerator DoBounce()
	{
		this.m_bouncing = true;
		float completion = 0f;
		int bouncesCompleted = 0;
		Vector3 startPos = base.gameObject.transform.position;
		yield return new WaitForEndOfFrame();
		while (bouncesCompleted < this.numBounces)
		{
			float num = 1f / Mathf.Pow(2f, (float)bouncesCompleted);
			Vector3 iterationTarget = this.bounceTarget * num;
			float num2 = 1f / (float)(bouncesCompleted + 1);
			float iterationDuration = this.durationSecs * num2;
			completion = 0f;
			while (completion < 1f)
			{
				Vector3 position = base.gameObject.transform.position;
				float num3 = Mathf.Min(Time.unscaledDeltaTime, 0.3f);
				completion = Mathf.Min(completion + num3 / iterationDuration, 1f);
				Vector3 vector = Bouncer.BounceSpline(completion) * iterationTarget;
				if (this.bounceTarget.x != 0f)
				{
					position.x = startPos.x + vector.x;
				}
				if (this.bounceTarget.y != 0f)
				{
					position.y = startPos.y + vector.y;
				}
				base.gameObject.transform.SetPosition(position);
				yield return new WaitForEndOfFrame();
			}
			int num4 = bouncesCompleted;
			bouncesCompleted = num4 + 1;
			iterationTarget = default(Vector3);
		}
		Vector3 position2 = base.gameObject.transform.position;
		if (this.bounceTarget.x != 0f)
		{
			position2.x = startPos.x;
		}
		if (this.bounceTarget.y != 0f)
		{
			position2.y = startPos.y;
		}
		base.gameObject.transform.SetPosition(position2);
		this.m_bouncing = false;
		yield break;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00010F7A File Offset: 0x0000F17A
	private static float BounceSpline(float k)
	{
		if (k < 0.5f)
		{
			return Bouncer.QuadOut(k * 2f);
		}
		return 1f - Bouncer.QuadIn(k * 2f - 1f);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00010FA9 File Offset: 0x0000F1A9
	private static float QuadOut(float k)
	{
		return k * (2f - k);
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00010FB4 File Offset: 0x0000F1B4
	private static float QuadIn(float k)
	{
		return k * k;
	}

	// Token: 0x040003D5 RID: 981
	private bool m_bouncing;

	// Token: 0x040003D6 RID: 982
	public float durationSecs = 0.3f;

	// Token: 0x040003D7 RID: 983
	public Vector3 bounceTarget;

	// Token: 0x040003D8 RID: 984
	public int numBounces = 1;
}
