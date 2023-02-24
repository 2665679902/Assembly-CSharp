using System;
using System.Collections.Generic;
using LibNoiseDotNet.Graphics.Tools.Noise;

namespace ProcGen.Noise
{
	// Token: 0x020004F5 RID: 1269
	public class Tree
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x00077F67 File Offset: 0x00076167
		// (set) Token: 0x060036C4 RID: 14020 RVA: 0x00077F6F File Offset: 0x0007616F
		public SampleSettings settings { get; set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x00077F78 File Offset: 0x00076178
		// (set) Token: 0x060036C6 RID: 14022 RVA: 0x00077F80 File Offset: 0x00076180
		public List<NodeLink> links { get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x00077F89 File Offset: 0x00076189
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x00077F91 File Offset: 0x00076191
		public Dictionary<string, Primitive> primitives { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x00077F9A File Offset: 0x0007619A
		// (set) Token: 0x060036CA RID: 14026 RVA: 0x00077FA2 File Offset: 0x000761A2
		public Dictionary<string, Filter> filters { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x00077FAB File Offset: 0x000761AB
		// (set) Token: 0x060036CC RID: 14028 RVA: 0x00077FB3 File Offset: 0x000761B3
		public Dictionary<string, Transformer> transformers { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x00077FBC File Offset: 0x000761BC
		// (set) Token: 0x060036CE RID: 14030 RVA: 0x00077FC4 File Offset: 0x000761C4
		public Dictionary<string, Selector> selectors { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x00077FCD File Offset: 0x000761CD
		// (set) Token: 0x060036D0 RID: 14032 RVA: 0x00077FD5 File Offset: 0x000761D5
		public Dictionary<string, Modifier> modifiers { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x00077FDE File Offset: 0x000761DE
		// (set) Token: 0x060036D2 RID: 14034 RVA: 0x00077FE6 File Offset: 0x000761E6
		public Dictionary<string, Combiner> combiners { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x00077FEF File Offset: 0x000761EF
		// (set) Token: 0x060036D4 RID: 14036 RVA: 0x00077FF7 File Offset: 0x000761F7
		public Dictionary<string, FloatList> floats { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x00078000 File Offset: 0x00076200
		// (set) Token: 0x060036D6 RID: 14038 RVA: 0x00078008 File Offset: 0x00076208
		public Dictionary<string, ControlPointList> controlpoints { get; set; }

		// Token: 0x060036D7 RID: 14039 RVA: 0x00078014 File Offset: 0x00076214
		public Tree()
		{
			this.settings = new SampleSettings();
			this.links = new List<NodeLink>();
			this.primitives = new Dictionary<string, Primitive>();
			this.filters = new Dictionary<string, Filter>();
			this.transformers = new Dictionary<string, Transformer>();
			this.selectors = new Dictionary<string, Selector>();
			this.modifiers = new Dictionary<string, Modifier>();
			this.combiners = new Dictionary<string, Combiner>();
			this.floats = new Dictionary<string, FloatList>();
			this.controlpoints = new Dictionary<string, ControlPointList>();
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000780D8 File Offset: 0x000762D8
		public void ClearEmptyLists()
		{
			if (this.links.Count == 0)
			{
				this.links = null;
			}
			if (this.primitives.Count == 0)
			{
				this.primitives = null;
			}
			if (this.filters.Count == 0)
			{
				this.filters = null;
			}
			if (this.transformers.Count == 0)
			{
				this.transformers = null;
			}
			if (this.selectors.Count == 0)
			{
				this.selectors = null;
			}
			if (this.modifiers.Count == 0)
			{
				this.modifiers = null;
			}
			if (this.combiners.Count == 0)
			{
				this.combiners = null;
			}
			if (this.floats.Count == 0)
			{
				this.floats = null;
			}
			if (this.controlpoints.Count == 0)
			{
				this.controlpoints = null;
			}
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x0007819C File Offset: 0x0007639C
		public void CreateEmptyLists()
		{
			if (this.links == null)
			{
				this.links = new List<NodeLink>();
			}
			if (this.primitives == null)
			{
				this.primitives = new Dictionary<string, Primitive>();
			}
			if (this.filters == null)
			{
				this.filters = new Dictionary<string, Filter>();
			}
			if (this.transformers == null)
			{
				this.transformers = new Dictionary<string, Transformer>();
			}
			if (this.selectors == null)
			{
				this.selectors = new Dictionary<string, Selector>();
			}
			if (this.modifiers == null)
			{
				this.modifiers = new Dictionary<string, Modifier>();
			}
			if (this.combiners == null)
			{
				this.combiners = new Dictionary<string, Combiner>();
			}
			if (this.floats == null)
			{
				this.floats = new Dictionary<string, FloatList>();
			}
			if (this.controlpoints == null)
			{
				this.controlpoints = new Dictionary<string, ControlPointList>();
			}
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x00078254 File Offset: 0x00076454
		private IModule3D GetModuleFromLink(Link link)
		{
			if (link == null)
			{
				return null;
			}
			switch (link.type)
			{
			case Link.Type.Primitive:
				if (this.primitiveLookup.ContainsKey(link.name))
				{
					return this.primitiveLookup[link.name];
				}
				Debug.LogError("Couldnt find [" + link.name + "] in primitives");
				break;
			case Link.Type.Filter:
				if (this.filterLookup.ContainsKey(link.name))
				{
					return this.filterLookup[link.name];
				}
				Debug.LogError("Couldnt find [" + link.name + "] in filters");
				break;
			case Link.Type.Transformer:
				if (this.transformerLookup.ContainsKey(link.name))
				{
					return this.transformerLookup[link.name];
				}
				Debug.LogError("Couldnt find [" + link.name + "] in transformers");
				break;
			case Link.Type.Selector:
				if (this.selectorLookup.ContainsKey(link.name))
				{
					return this.selectorLookup[link.name];
				}
				Debug.LogError("Couldnt find [" + link.name + "] in selectors");
				break;
			case Link.Type.Modifier:
				if (this.modifierLookup.ContainsKey(link.name))
				{
					return this.modifierLookup[link.name];
				}
				Debug.LogError("Couldnt find [" + link.name + "] in modifiers");
				break;
			case Link.Type.Combiner:
				if (this.combinerLookup.ContainsKey(link.name))
				{
					return this.combinerLookup[link.name];
				}
				Debug.LogError("Couldnt find [" + link.name + "] in combiners");
				break;
			case Link.Type.Terminator:
				return null;
			}
			Debug.LogError(string.Concat(new string[]
			{
				"Couldnt find link [",
				link.name,
				"] [",
				link.type.ToString(),
				"]"
			}));
			return null;
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x0007847C File Offset: 0x0007667C
		public IModule3D BuildFinalModule(int globalSeed)
		{
			IModule3D module3D = null;
			this.primitiveLookup.Clear();
			this.filterLookup.Clear();
			this.modifierLookup.Clear();
			this.selectorLookup.Clear();
			this.transformerLookup.Clear();
			this.combinerLookup.Clear();
			foreach (KeyValuePair<string, Primitive> keyValuePair in this.primitives)
			{
				this.primitiveLookup.Add(keyValuePair.Key, keyValuePair.Value.CreateModule(globalSeed));
			}
			foreach (KeyValuePair<string, Filter> keyValuePair2 in this.filters)
			{
				this.filterLookup.Add(keyValuePair2.Key, keyValuePair2.Value.CreateModule());
			}
			foreach (KeyValuePair<string, Modifier> keyValuePair3 in this.modifiers)
			{
				this.modifierLookup.Add(keyValuePair3.Key, keyValuePair3.Value.CreateModule());
			}
			foreach (KeyValuePair<string, Selector> keyValuePair4 in this.selectors)
			{
				this.selectorLookup.Add(keyValuePair4.Key, keyValuePair4.Value.CreateModule());
			}
			foreach (KeyValuePair<string, Transformer> keyValuePair5 in this.transformers)
			{
				this.transformerLookup.Add(keyValuePair5.Key, keyValuePair5.Value.CreateModule());
			}
			foreach (KeyValuePair<string, Combiner> keyValuePair6 in this.combiners)
			{
				this.combinerLookup.Add(keyValuePair6.Key, keyValuePair6.Value.CreateModule());
			}
			for (int i = 0; i < this.links.Count; i++)
			{
				NodeLink nodeLink = this.links[i];
				IModule3D moduleFromLink = this.GetModuleFromLink(nodeLink.target);
				if (nodeLink.target.type == Link.Type.Terminator)
				{
					module3D = this.GetModuleFromLink(nodeLink.source0);
				}
				else
				{
					switch (nodeLink.target.type)
					{
					case Link.Type.Filter:
					{
						IModule3D module3D2 = this.GetModuleFromLink(nodeLink.source0);
						this.filters[nodeLink.target.name].SetSouces(moduleFromLink, module3D2);
						((FilterModule)moduleFromLink).Primitive3D = module3D2;
						break;
					}
					case Link.Type.Transformer:
					{
						IModule3D module3D2 = this.GetModuleFromLink(nodeLink.source0);
						IModule3D module3D3 = this.GetModuleFromLink(nodeLink.source1);
						IModule3D module3D4 = this.GetModuleFromLink(nodeLink.source2);
						IModule3D moduleFromLink2 = this.GetModuleFromLink(nodeLink.source3);
						this.transformers[nodeLink.target.name].SetSouces(moduleFromLink, module3D2, module3D3, module3D4, moduleFromLink2);
						break;
					}
					case Link.Type.Selector:
					{
						IModule3D module3D2 = this.GetModuleFromLink(nodeLink.source0);
						IModule3D module3D3 = this.GetModuleFromLink(nodeLink.source1);
						IModule3D module3D4 = this.GetModuleFromLink(nodeLink.source2);
						this.selectors[nodeLink.target.name].SetSouces(moduleFromLink, module3D4, module3D2, module3D3);
						break;
					}
					case Link.Type.Modifier:
					{
						IModule3D module3D2 = this.GetModuleFromLink(nodeLink.source0);
						ControlPointList controlPointList = null;
						if (nodeLink.source1 != null && nodeLink.source1.type == Link.Type.ControlPoints && this.controlpoints.ContainsKey(nodeLink.source1.name))
						{
							controlPointList = this.controlpoints[nodeLink.source1.name];
						}
						FloatList floatList = null;
						if (nodeLink.source1 != null && nodeLink.source1.type == Link.Type.FloatPoints && this.floats.ContainsKey(nodeLink.source1.name))
						{
							floatList = this.floats[nodeLink.source1.name];
						}
						this.modifiers[nodeLink.target.name].SetSouces(moduleFromLink, module3D2, floatList, controlPointList);
						break;
					}
					case Link.Type.Combiner:
					{
						IModule3D module3D2 = this.GetModuleFromLink(nodeLink.source0);
						IModule3D module3D3 = this.GetModuleFromLink(nodeLink.source1);
						this.combiners[nodeLink.target.name].SetSouces(moduleFromLink, module3D2, module3D3);
						break;
					}
					}
				}
			}
			Debug.Assert(module3D != null, "Missing Terminus module");
			return module3D;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000789B0 File Offset: 0x00076BB0
		public string[] GetPrimitiveNames()
		{
			string[] array = new string[this.primitives.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<string, Primitive> keyValuePair in this.primitives)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x00078A24 File Offset: 0x00076C24
		public string[] GetFilterNames()
		{
			string[] array = new string[this.filters.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<string, Filter> keyValuePair in this.filters)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x040013A0 RID: 5024
		private Dictionary<string, IModule3D> primitiveLookup = new Dictionary<string, IModule3D>();

		// Token: 0x040013A1 RID: 5025
		private Dictionary<string, IModule3D> filterLookup = new Dictionary<string, IModule3D>();

		// Token: 0x040013A2 RID: 5026
		private Dictionary<string, IModule3D> modifierLookup = new Dictionary<string, IModule3D>();

		// Token: 0x040013A3 RID: 5027
		private Dictionary<string, IModule3D> selectorLookup = new Dictionary<string, IModule3D>();

		// Token: 0x040013A4 RID: 5028
		private Dictionary<string, IModule3D> transformerLookup = new Dictionary<string, IModule3D>();

		// Token: 0x040013A5 RID: 5029
		private Dictionary<string, IModule3D> combinerLookup = new Dictionary<string, IModule3D>();
	}
}
