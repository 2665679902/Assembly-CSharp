using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A42 RID: 2626
public class EasingAnimations : MonoBehaviour
{
	// Token: 0x170005D1 RID: 1489
	// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x001C5A0B File Offset: 0x001C3C0B
	public bool IsPlaying
	{
		get
		{
			return this.animationCoroutine != null;
		}
	}

	// Token: 0x06004FA2 RID: 20386 RVA: 0x001C5A16 File Offset: 0x001C3C16
	private void Start()
	{
		if (this.animationMap == null || this.animationMap.Count == 0)
		{
			this.Initialize();
		}
	}

	// Token: 0x06004FA3 RID: 20387 RVA: 0x001C5A34 File Offset: 0x001C3C34
	private void Initialize()
	{
		this.animationMap = new Dictionary<string, EasingAnimations.AnimationScales>();
		foreach (EasingAnimations.AnimationScales animationScales in this.scales)
		{
			this.animationMap.Add(animationScales.name, animationScales);
		}
	}

	// Token: 0x06004FA4 RID: 20388 RVA: 0x001C5A7C File Offset: 0x001C3C7C
	public void PlayAnimation(string animationName, float delay = 0f)
	{
		if (this.animationMap == null || this.animationMap.Count == 0)
		{
			this.Initialize();
		}
		if (!this.animationMap.ContainsKey(animationName))
		{
			return;
		}
		if (this.animationCoroutine != null)
		{
			base.StopCoroutine(this.animationCoroutine);
		}
		this.currentAnimation = this.animationMap[animationName];
		this.currentAnimation.currentScale = this.currentAnimation.startScale;
		base.transform.localScale = Vector3.one * this.currentAnimation.currentScale;
		this.animationCoroutine = base.StartCoroutine(this.ExecuteAnimation(delay));
	}

	// Token: 0x06004FA5 RID: 20389 RVA: 0x001C5B22 File Offset: 0x001C3D22
	private IEnumerator ExecuteAnimation(float delay)
	{
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < startTime + delay)
		{
			yield return SequenceUtil.WaitForNextFrame;
		}
		startTime = Time.realtimeSinceStartup;
		bool keepAnimating = true;
		while (keepAnimating)
		{
			float num = Time.realtimeSinceStartup - startTime;
			this.currentAnimation.currentScale = this.GetEasing(num * this.currentAnimation.easingMultiplier);
			if (this.currentAnimation.endScale > this.currentAnimation.startScale)
			{
				keepAnimating = this.currentAnimation.currentScale < this.currentAnimation.endScale - 0.025f;
			}
			else
			{
				keepAnimating = this.currentAnimation.currentScale > this.currentAnimation.endScale + 0.025f;
			}
			if (!keepAnimating)
			{
				this.currentAnimation.currentScale = this.currentAnimation.endScale;
			}
			base.transform.localScale = Vector3.one * this.currentAnimation.currentScale;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.animationCoroutine = null;
		if (this.OnAnimationDone != null)
		{
			this.OnAnimationDone(this.currentAnimation.name);
		}
		yield break;
	}

	// Token: 0x06004FA6 RID: 20390 RVA: 0x001C5B38 File Offset: 0x001C3D38
	private float GetEasing(float t)
	{
		EasingAnimations.AnimationScales.AnimationType type = this.currentAnimation.type;
		if (type == EasingAnimations.AnimationScales.AnimationType.EaseOutBack)
		{
			return this.EaseOutBack(this.currentAnimation.currentScale, this.currentAnimation.endScale, t);
		}
		if (type == EasingAnimations.AnimationScales.AnimationType.EaseInBack)
		{
			return this.EaseInBack(this.currentAnimation.currentScale, this.currentAnimation.endScale, t);
		}
		return this.EaseInOutBack(this.currentAnimation.currentScale, this.currentAnimation.endScale, t);
	}

	// Token: 0x06004FA7 RID: 20391 RVA: 0x001C5BB4 File Offset: 0x001C3DB4
	public float EaseInOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value /= 0.5f;
		if (value < 1f)
		{
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
		}
		value -= 2f;
		num *= 1.525f;
		return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
	}

	// Token: 0x06004FA8 RID: 20392 RVA: 0x001C5C30 File Offset: 0x001C3E30
	public float EaseInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1f;
		float num = 1.70158f;
		return end * value * value * ((num + 1f) * value - num) + start;
	}

	// Token: 0x06004FA9 RID: 20393 RVA: 0x001C5C64 File Offset: 0x001C3E64
	public float EaseOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value -= 1f;
		return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
	}

	// Token: 0x0400354E RID: 13646
	public EasingAnimations.AnimationScales[] scales;

	// Token: 0x0400354F RID: 13647
	private EasingAnimations.AnimationScales currentAnimation;

	// Token: 0x04003550 RID: 13648
	private Coroutine animationCoroutine;

	// Token: 0x04003551 RID: 13649
	private Dictionary<string, EasingAnimations.AnimationScales> animationMap;

	// Token: 0x04003552 RID: 13650
	public Action<string> OnAnimationDone;

	// Token: 0x020018CE RID: 6350
	[Serializable]
	public struct AnimationScales
	{
		// Token: 0x0400725F RID: 29279
		public string name;

		// Token: 0x04007260 RID: 29280
		public float startScale;

		// Token: 0x04007261 RID: 29281
		public float endScale;

		// Token: 0x04007262 RID: 29282
		public EasingAnimations.AnimationScales.AnimationType type;

		// Token: 0x04007263 RID: 29283
		public float easingMultiplier;

		// Token: 0x04007264 RID: 29284
		[HideInInspector]
		public float currentScale;

		// Token: 0x020020FA RID: 8442
		public enum AnimationType
		{
			// Token: 0x040092C1 RID: 37569
			EaseInOutBack,
			// Token: 0x040092C2 RID: 37570
			EaseOutBack,
			// Token: 0x040092C3 RID: 37571
			EaseInBack
		}
	}
}
