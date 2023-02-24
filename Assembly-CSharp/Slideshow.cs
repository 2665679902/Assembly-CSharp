using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BFC RID: 3068
[AddComponentMenu("KMonoBehaviour/scripts/Slideshow")]
public class Slideshow : KMonoBehaviour
{
	// Token: 0x06006106 RID: 24838 RVA: 0x0023AD34 File Offset: 0x00238F34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.timeUntilNextSlide = this.timePerSlide;
		if (this.transparentIfEmpty && this.sprites != null && this.sprites.Length == 0)
		{
			this.imageTarget.color = Color.clear;
		}
		if (this.isExpandable)
		{
			this.button = base.GetComponent<KButton>();
			this.button.onClick += delegate
			{
				if (this.onBeforePlay != null)
				{
					this.onBeforePlay();
				}
				SlideshowUpdateType slideshowUpdateType = this.updateType;
				if (slideshowUpdateType == SlideshowUpdateType.preloadedSprites)
				{
					VideoScreen.Instance.PlaySlideShow(this.sprites);
					return;
				}
				if (slideshowUpdateType != SlideshowUpdateType.loadOnDemand)
				{
					return;
				}
				VideoScreen.Instance.PlaySlideShow(this.files);
			};
		}
		if (this.nextButton != null)
		{
			this.nextButton.onClick += delegate
			{
				this.nextSlide();
			};
		}
		if (this.prevButton != null)
		{
			this.prevButton.onClick += delegate
			{
				this.prevSlide();
			};
		}
		if (this.pauseButton != null)
		{
			this.pauseButton.onClick += delegate
			{
				this.SetPaused(!this.paused);
			};
		}
		if (this.closeButton != null)
		{
			this.closeButton.onClick += delegate
			{
				VideoScreen.Instance.Stop();
				if (this.onEndingPlay != null)
				{
					this.onEndingPlay();
				}
			};
		}
	}

	// Token: 0x06006107 RID: 24839 RVA: 0x0023AE3C File Offset: 0x0023903C
	public void SetPaused(bool state)
	{
		this.paused = state;
		if (this.pauseIcon != null)
		{
			this.pauseIcon.gameObject.SetActive(!this.paused);
		}
		if (this.unpauseIcon != null)
		{
			this.unpauseIcon.gameObject.SetActive(this.paused);
		}
		if (this.prevButton != null)
		{
			this.prevButton.gameObject.SetActive(this.paused);
		}
		if (this.nextButton != null)
		{
			this.nextButton.gameObject.SetActive(this.paused);
		}
	}

	// Token: 0x06006108 RID: 24840 RVA: 0x0023AEE4 File Offset: 0x002390E4
	private void resetSlide(bool enable)
	{
		this.timeUntilNextSlide = this.timePerSlide;
		this.currentSlide = 0;
		if (enable)
		{
			this.imageTarget.color = Color.white;
			return;
		}
		if (this.transparentIfEmpty)
		{
			this.imageTarget.color = Color.clear;
		}
	}

	// Token: 0x06006109 RID: 24841 RVA: 0x0023AF30 File Offset: 0x00239130
	private Sprite loadSlide(string file)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		Texture2D texture2D = new Texture2D(512, 768);
		texture2D.filterMode = FilterMode.Point;
		texture2D.LoadImage(File.ReadAllBytes(file));
		return Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2((float)texture2D.width, (float)texture2D.height)), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect);
	}

	// Token: 0x0600610A RID: 24842 RVA: 0x0023AFA0 File Offset: 0x002391A0
	public void SetFiles(string[] files, int loadFrame = -1)
	{
		if (files == null)
		{
			return;
		}
		this.files = files;
		bool flag = files.Length != 0 && files[0] != null;
		this.resetSlide(flag);
		if (flag)
		{
			int num = ((loadFrame != -1) ? loadFrame : (files.Length - 1));
			string text = files[num];
			Sprite sprite = this.loadSlide(text);
			this.setSlide(sprite);
			this.currentSlideImage = sprite;
		}
	}

	// Token: 0x0600610B RID: 24843 RVA: 0x0023AFF8 File Offset: 0x002391F8
	public void updateSize(Sprite sprite)
	{
		Vector2 fittedSize = this.GetFittedSize(sprite, 960f, 960f);
		base.GetComponent<RectTransform>().sizeDelta = fittedSize;
	}

	// Token: 0x0600610C RID: 24844 RVA: 0x0023B023 File Offset: 0x00239223
	public void SetSprites(Sprite[] sprites)
	{
		if (sprites == null)
		{
			return;
		}
		this.sprites = sprites;
		this.resetSlide(sprites.Length != 0 && sprites[0] != null);
		if (sprites.Length != 0 && sprites[0] != null)
		{
			this.setSlide(sprites[0]);
		}
	}

	// Token: 0x0600610D RID: 24845 RVA: 0x0023B060 File Offset: 0x00239260
	public Vector2 GetFittedSize(Sprite sprite, float maxWidth, float maxHeight)
	{
		if (sprite == null || sprite.texture == null)
		{
			return Vector2.zero;
		}
		int width = sprite.texture.width;
		int height = sprite.texture.height;
		float num = maxWidth / (float)width;
		float num2 = maxHeight / (float)height;
		if (num < num2)
		{
			return new Vector2((float)width * num, (float)height * num);
		}
		return new Vector2((float)width * num2, (float)height * num2);
	}

	// Token: 0x0600610E RID: 24846 RVA: 0x0023B0CB File Offset: 0x002392CB
	public void setSlide(Sprite slide)
	{
		if (slide == null)
		{
			return;
		}
		this.imageTarget.texture = slide.texture;
		this.updateSize(slide);
	}

	// Token: 0x0600610F RID: 24847 RVA: 0x0023B0EF File Offset: 0x002392EF
	public void nextSlide()
	{
		this.setSlideIndex(this.currentSlide + 1);
	}

	// Token: 0x06006110 RID: 24848 RVA: 0x0023B0FF File Offset: 0x002392FF
	public void prevSlide()
	{
		this.setSlideIndex(this.currentSlide - 1);
	}

	// Token: 0x06006111 RID: 24849 RVA: 0x0023B110 File Offset: 0x00239310
	private void setSlideIndex(int slideIndex)
	{
		this.timeUntilNextSlide = this.timePerSlide;
		SlideshowUpdateType slideshowUpdateType = this.updateType;
		if (slideshowUpdateType != SlideshowUpdateType.preloadedSprites)
		{
			if (slideshowUpdateType != SlideshowUpdateType.loadOnDemand)
			{
				return;
			}
			if (slideIndex < 0)
			{
				slideIndex = this.files.Length + slideIndex;
			}
			this.currentSlide = slideIndex % this.files.Length;
			if (this.currentSlide == this.files.Length - 1)
			{
				this.timeUntilNextSlide *= this.timeFactorForLastSlide;
			}
			if (this.playInThumbnail)
			{
				if (this.currentSlideImage != null)
				{
					UnityEngine.Object.Destroy(this.currentSlideImage.texture);
					UnityEngine.Object.Destroy(this.currentSlideImage);
					GC.Collect();
				}
				this.currentSlideImage = this.loadSlide(this.files[this.currentSlide]);
				this.setSlide(this.currentSlideImage);
			}
		}
		else
		{
			if (slideIndex < 0)
			{
				slideIndex = this.sprites.Length + slideIndex;
			}
			this.currentSlide = slideIndex % this.sprites.Length;
			if (this.currentSlide == this.sprites.Length - 1)
			{
				this.timeUntilNextSlide *= this.timeFactorForLastSlide;
			}
			if (this.playInThumbnail)
			{
				this.setSlide(this.sprites[this.currentSlide]);
				return;
			}
		}
	}

	// Token: 0x06006112 RID: 24850 RVA: 0x0023B23C File Offset: 0x0023943C
	private void Update()
	{
		if (this.updateType == SlideshowUpdateType.preloadedSprites && (this.sprites == null || this.sprites.Length == 0))
		{
			return;
		}
		if (this.updateType == SlideshowUpdateType.loadOnDemand && (this.files == null || this.files.Length == 0))
		{
			return;
		}
		if (this.paused)
		{
			return;
		}
		this.timeUntilNextSlide -= Time.unscaledDeltaTime;
		if (this.timeUntilNextSlide <= 0f)
		{
			this.nextSlide();
		}
	}

	// Token: 0x040042D0 RID: 17104
	public RawImage imageTarget;

	// Token: 0x040042D1 RID: 17105
	private string[] files;

	// Token: 0x040042D2 RID: 17106
	private Sprite currentSlideImage;

	// Token: 0x040042D3 RID: 17107
	private Sprite[] sprites;

	// Token: 0x040042D4 RID: 17108
	public float timePerSlide = 1f;

	// Token: 0x040042D5 RID: 17109
	public float timeFactorForLastSlide = 3f;

	// Token: 0x040042D6 RID: 17110
	private int currentSlide;

	// Token: 0x040042D7 RID: 17111
	private float timeUntilNextSlide;

	// Token: 0x040042D8 RID: 17112
	private bool paused;

	// Token: 0x040042D9 RID: 17113
	public bool playInThumbnail;

	// Token: 0x040042DA RID: 17114
	public SlideshowUpdateType updateType;

	// Token: 0x040042DB RID: 17115
	[SerializeField]
	private bool isExpandable;

	// Token: 0x040042DC RID: 17116
	[SerializeField]
	private KButton button;

	// Token: 0x040042DD RID: 17117
	[SerializeField]
	private bool transparentIfEmpty = true;

	// Token: 0x040042DE RID: 17118
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040042DF RID: 17119
	[SerializeField]
	private KButton prevButton;

	// Token: 0x040042E0 RID: 17120
	[SerializeField]
	private KButton nextButton;

	// Token: 0x040042E1 RID: 17121
	[SerializeField]
	private KButton pauseButton;

	// Token: 0x040042E2 RID: 17122
	[SerializeField]
	private Image pauseIcon;

	// Token: 0x040042E3 RID: 17123
	[SerializeField]
	private Image unpauseIcon;

	// Token: 0x040042E4 RID: 17124
	public Slideshow.onBeforeAndEndPlayDelegate onBeforePlay;

	// Token: 0x040042E5 RID: 17125
	public Slideshow.onBeforeAndEndPlayDelegate onEndingPlay;

	// Token: 0x02001A99 RID: 6809
	// (Invoke) Token: 0x0600939A RID: 37786
	public delegate void onBeforeAndEndPlayDelegate();
}
