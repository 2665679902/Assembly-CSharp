using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace YamlDotNet.Samples.Helpers
{
	// Token: 0x020001F7 RID: 503
	public class ExampleRunner : MonoBehaviour
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x0003D8A0 File Offset: 0x0003BAA0
		public static string[] GetAllTestNames()
		{
			List<string> list = new List<string>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.Namespace == "YamlDotNet.Samples" && type.IsClass)
				{
					bool flag = false;
					foreach (MethodInfo methodInfo in type.GetMethods())
					{
						if (methodInfo.Name == "Main" && (SampleAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(SampleAttribute)) != null)
						{
							list.Add(type.Name);
							break;
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003D960 File Offset: 0x0003BB60
		public static string[] GetAllTestTitles()
		{
			List<string> list = new List<string>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.Namespace == "YamlDotNet.Samples" && type.IsClass)
				{
					bool flag = false;
					foreach (MethodInfo methodInfo in type.GetMethods())
					{
						if (methodInfo.Name == "Main")
						{
							SampleAttribute sampleAttribute = (SampleAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(SampleAttribute));
							if (sampleAttribute != null)
							{
								list.Add(sampleAttribute.Title);
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003DA24 File Offset: 0x0003BC24
		private void Start()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.Namespace == "YamlDotNet.Samples" && type.IsClass && Array.IndexOf<string>(this.disabledTests, type.Name) == -1)
				{
					bool flag = false;
					foreach (MethodInfo methodInfo in type.GetMethods())
					{
						if (methodInfo.Name == "Main")
						{
							SampleAttribute sampleAttribute = (SampleAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(SampleAttribute));
							if (sampleAttribute != null)
							{
								this.helper.WriteLine("{0} - {1}", new object[] { sampleAttribute.Title, sampleAttribute.Description });
								object obj = type.GetConstructor(new Type[] { typeof(ExampleRunner.StringTestOutputHelper) }).Invoke(new object[] { this.helper });
								methodInfo.Invoke(obj, new object[0]);
								global::Debug.Log(this.helper.ToString());
								this.helper.Clear();
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x0400087B RID: 2171
		private ExampleRunner.StringTestOutputHelper helper = new ExampleRunner.StringTestOutputHelper();

		// Token: 0x0400087C RID: 2172
		public string[] disabledTests = new string[0];

		// Token: 0x02000A63 RID: 2659
		private class StringTestOutputHelper : ITestOutputHelper
		{
			// Token: 0x060055A6 RID: 21926 RVA: 0x0009F308 File Offset: 0x0009D508
			public void WriteLine()
			{
				this.output.AppendLine();
			}

			// Token: 0x060055A7 RID: 21927 RVA: 0x0009F316 File Offset: 0x0009D516
			public void WriteLine(string value)
			{
				this.output.AppendLine(value);
			}

			// Token: 0x060055A8 RID: 21928 RVA: 0x0009F325 File Offset: 0x0009D525
			public void WriteLine(string format, params object[] args)
			{
				this.output.AppendFormat(format, args);
				this.output.AppendLine();
			}

			// Token: 0x060055A9 RID: 21929 RVA: 0x0009F341 File Offset: 0x0009D541
			public override string ToString()
			{
				return this.output.ToString();
			}

			// Token: 0x060055AA RID: 21930 RVA: 0x0009F34E File Offset: 0x0009D54E
			public void Clear()
			{
				this.output = new StringBuilder();
			}

			// Token: 0x04002365 RID: 9061
			private StringBuilder output = new StringBuilder();
		}
	}
}
