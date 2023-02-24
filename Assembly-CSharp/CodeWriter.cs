using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000342 RID: 834
public class CodeWriter
{
	// Token: 0x060010A3 RID: 4259 RVA: 0x0005A5DF File Offset: 0x000587DF
	public CodeWriter(string path)
	{
		this.Path = path;
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0005A5F9 File Offset: 0x000587F9
	public void Comment(string text)
	{
		this.Lines.Add("// " + text);
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0005A614 File Offset: 0x00058814
	public void BeginPartialClass(string class_name, string parent_name = null)
	{
		string text = "public partial class " + class_name;
		if (parent_name != null)
		{
			text = text + " : " + parent_name;
		}
		this.Line(text);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0005A660 File Offset: 0x00058860
	public void BeginClass(string class_name, string parent_name = null)
	{
		string text = "public class " + class_name;
		if (parent_name != null)
		{
			text = text + " : " + parent_name;
		}
		this.Line(text);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0005A6A9 File Offset: 0x000588A9
	public void EndClass()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0005A6C4 File Offset: 0x000588C4
	public void BeginNameSpace(string name)
	{
		this.Line("namespace " + name);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0005A6F0 File Offset: 0x000588F0
	public void EndNameSpace()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0005A70B File Offset: 0x0005890B
	public void BeginArrayStructureInitialization(string name)
	{
		this.Line("new " + name);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0005A737 File Offset: 0x00058937
	public void EndArrayStructureInitialization(bool last_item)
	{
		this.Indent--;
		if (!last_item)
		{
			this.Line("},");
			return;
		}
		this.Line("}");
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0005A761 File Offset: 0x00058961
	public void BeginArraArrayInitialization(string array_type, string array_name)
	{
		this.Line(array_name + " = new " + array_type + "[]");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0005A793 File Offset: 0x00058993
	public void EndArrayArrayInitialization(bool last_item)
	{
		this.Indent--;
		if (last_item)
		{
			this.Line("}");
			return;
		}
		this.Line("},");
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0005A7BD File Offset: 0x000589BD
	public void BeginConstructor(string name)
	{
		this.Line("public " + name + "()");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0005A7EE File Offset: 0x000589EE
	public void EndConstructor()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0005A809 File Offset: 0x00058A09
	public void BeginArrayAssignment(string array_type, string array_name)
	{
		this.Line(array_name + " = new " + array_type + "[]");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0005A83B File Offset: 0x00058A3B
	public void EndArrayAssignment()
	{
		this.Indent--;
		this.Line("};");
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0005A856 File Offset: 0x00058A56
	public void FieldAssignment(string field_name, string value)
	{
		this.Line(field_name + " = " + value + ";");
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0005A86F File Offset: 0x00058A6F
	public void BeginStructureDelegateFieldInitializer(string name)
	{
		this.Line(name + "=delegate()");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0005A89B File Offset: 0x00058A9B
	public void EndStructureDelegateFieldInitializer()
	{
		this.Indent--;
		this.Line("},");
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0005A8B6 File Offset: 0x00058AB6
	public void BeginIf(string condition)
	{
		this.Line("if(" + condition + ")");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0005A8E8 File Offset: 0x00058AE8
	public void BeginElseIf(string condition)
	{
		this.Indent--;
		this.Line("}");
		this.Line("else if(" + condition + ")");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0005A93D File Offset: 0x00058B3D
	public void EndIf()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0005A958 File Offset: 0x00058B58
	public void BeginFunctionDeclaration(string name, string parameter, string return_type)
	{
		this.Line(string.Concat(new string[] { "public ", return_type, " ", name, "(", parameter, ")" }));
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0005A9BC File Offset: 0x00058BBC
	public void BeginFunctionDeclaration(string name, string return_type)
	{
		this.Line(string.Concat(new string[] { "public ", return_type, " ", name, "()" }));
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0005AA13 File Offset: 0x00058C13
	public void EndFunctionDeclaration()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0005AA30 File Offset: 0x00058C30
	private void InternalNamedParameter(string name, string value, bool last_parameter)
	{
		string text = "";
		if (!last_parameter)
		{
			text = ",";
		}
		this.Line(name + ":" + value + text);
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0005AA5F File Offset: 0x00058C5F
	public void NamedParameterBool(string name, bool value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value.ToString().ToLower(), last_parameter);
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0005AA75 File Offset: 0x00058C75
	public void NamedParameterInt(string name, int value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value.ToString(), last_parameter);
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0005AA86 File Offset: 0x00058C86
	public void NamedParameterFloat(string name, float value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value.ToString() + "f", last_parameter);
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0005AAA1 File Offset: 0x00058CA1
	public void NamedParameterString(string name, string value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value, last_parameter);
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x0005AAAC File Offset: 0x00058CAC
	public void BeginFunctionCall(string name)
	{
		this.Line(name);
		this.Line("(");
		this.Indent++;
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0005AACE File Offset: 0x00058CCE
	public void EndFunctionCall()
	{
		this.Indent--;
		this.Line(");");
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0005AAEC File Offset: 0x00058CEC
	public void FunctionCall(string function_name, params string[] parameters)
	{
		string text = function_name + "(";
		for (int i = 0; i < parameters.Length; i++)
		{
			text += parameters[i];
			if (i != parameters.Length - 1)
			{
				text += ", ";
			}
		}
		this.Line(text + ");");
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0005AB42 File Offset: 0x00058D42
	public void StructureFieldInitializer(string field, string value)
	{
		this.Line(field + " = " + value + ",");
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0005AB5C File Offset: 0x00058D5C
	public void StructureArrayFieldInitializer(string field, string field_type, params string[] values)
	{
		string text = field + " = new " + field_type + "[]{ ";
		for (int i = 0; i < values.Length; i++)
		{
			text += values[i];
			if (i < values.Length - 1)
			{
				text += ", ";
			}
		}
		text += " },";
		this.Line(text);
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0005ABBC File Offset: 0x00058DBC
	public void Line(string text = "")
	{
		for (int i = 0; i < this.Indent; i++)
		{
			text = "\t" + text;
		}
		this.Lines.Add(text);
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0005ABF3 File Offset: 0x00058DF3
	public void Flush()
	{
		File.WriteAllLines(this.Path, this.Lines.ToArray());
	}

	// Token: 0x0400090B RID: 2315
	private List<string> Lines = new List<string>();

	// Token: 0x0400090C RID: 2316
	private string Path;

	// Token: 0x0400090D RID: 2317
	private int Indent;
}
