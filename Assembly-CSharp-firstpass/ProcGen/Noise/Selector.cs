using System;
using LibNoiseDotNet.Graphics.Tools.Noise;
using LibNoiseDotNet.Graphics.Tools.Noise.Modifier;

namespace ProcGen.Noise
{
	// Token: 0x020004EE RID: 1262
	public class Selector : NoiseBase
	{
		// Token: 0x0600367A RID: 13946 RVA: 0x00077830 File Offset: 0x00075A30
		public override Type GetObjectType()
		{
			return typeof(Selector);
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x0007783C File Offset: 0x00075A3C
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x00077844 File Offset: 0x00075A44
		public Selector.SelectType selectType { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x0007784D File Offset: 0x00075A4D
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x00077855 File Offset: 0x00075A55
		public float lower { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x0007785E File Offset: 0x00075A5E
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x00077866 File Offset: 0x00075A66
		public float upper { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x0007786F File Offset: 0x00075A6F
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x00077877 File Offset: 0x00075A77
		public float edge { get; set; }

		// Token: 0x06003683 RID: 13955 RVA: 0x00077880 File Offset: 0x00075A80
		public Selector()
		{
			this.selectType = Selector.SelectType.Blend;
			this.lower = 0f;
			this.upper = 1f;
			this.edge = 0.02f;
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000778B0 File Offset: 0x00075AB0
		public IModule3D CreateModule()
		{
			if (this.selectType == Selector.SelectType.Blend)
			{
				return new Blend();
			}
			Select select = new Select();
			select.SetBounds(this.lower, this.upper);
			select.EdgeFalloff = this.edge;
			return select;
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000778E4 File Offset: 0x00075AE4
		public IModule3D CreateModule(IModule3D selectModule, IModule3D leftModule, IModule3D rightModule)
		{
			if (this.selectType == Selector.SelectType.Blend)
			{
				return new Blend(selectModule, rightModule, leftModule);
			}
			return new Select(selectModule, rightModule, leftModule, this.lower, this.upper, this.edge);
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x00077914 File Offset: 0x00075B14
		public void SetSouces(IModule3D target, IModule3D controlModule, IModule3D rightModule, IModule3D leftModule)
		{
			if (this.selectType == Selector.SelectType.Blend)
			{
				Blend blend = target as Blend;
				blend.ControlModule = controlModule;
				blend.RightModule = rightModule;
				blend.LeftModule = leftModule;
				return;
			}
			if (this.selectType == Selector.SelectType.Select)
			{
				Select select = target as Select;
				select.ControlModule = controlModule;
				select.RightModule = rightModule;
				select.LeftModule = leftModule;
			}
		}

		// Token: 0x02000B15 RID: 2837
		public enum SelectType
		{
			// Token: 0x04002611 RID: 9745
			_UNSET_,
			// Token: 0x04002612 RID: 9746
			Blend,
			// Token: 0x04002613 RID: 9747
			Select
		}
	}
}
