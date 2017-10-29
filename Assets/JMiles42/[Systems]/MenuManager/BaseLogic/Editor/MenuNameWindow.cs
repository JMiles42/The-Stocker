﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JMiles42.Editor;
using JMiles42.Extensions;
using JMiles42.Systems.MenuManager;
using UnityEditor;
using UnityEngine;

public class MenuNameWindow: Window<MenuNameWindow> {
	private const string WindowTitle = "Menu UI";

	protected static string TagFilePath { get; } = FileStrings.ASSETS_GENERATED_SCRIPTS + "/" + WindowTitle + FileStrings.SCRIPTS_FILE_EXTENSION;

	[MenuItem("JMiles42/" + WindowTitle)]
	static void Init() {
		GetWindow();

		if (File.Exists(TagFilePath))
			NamesList = new List<string>(File.ReadAllLines(TagFilePath));
		else {
			NamesList = new List<string>();
		}

		window.titleContent = new GUIContent(WindowTitle);
		InitList();
	}

	protected static List<string> NamesList { get; set; }

	protected override void DrawGUI() {
		if (NamesList.IsNullOrEmpty())
			NamesList = InitList();
		using (new GUILayout.VerticalScope(GUI.skin.box))
			DrawList();
		using (new GUILayout.VerticalScope(GUI.skin.box))
			DrawButtons();
	}

	private static void DrawButtons() {
		using (new GUILayout.HorizontalScope()) {
			if (JMilesGUILayoutEvents.Button("Generate Class")) {
				GenerateClassFile();
			}
			if (JMilesGUILayoutEvents.Button("Search For New Menus")) {
				NamesList = InitList();
			}
		}
	}

	public static List<string> InitList() {
		var list = new List<string>(
								    typeof (SimpleMenu<>).Assembly.GetTypes().
														  Where(t => t.BaseType != null && t.BaseType.IsGenericType && (t.BaseType.GetGenericTypeDefinition() == typeof (SimpleMenu<>))).
														  Select(s => s.ToString()));
		return list;
	}

	protected void DrawList() {
		foreach (var nam in NamesList) {
			using (new GUILayout.VerticalScope(GUI.skin.box))
				GUILayout.Label(nam);
		}
	}

	public static void GenerateClassFile() { GenerateClassFile(NamesList); }

	public static void GenerateClassFile(IEnumerable<string> strs) {
		var list = ConvertNamesListToCode(strs);
		ScriptGenerators.WriteFile(TagFilePath, list);
		AssetDatabase.Refresh();
	}

	private static string ConvertNamesListToCode(IEnumerable<string> strs) {
		var sb = new StringBuilder(
								   @"namespace JMiles42.Systems.MenuManager
{
	[System.Serializable]
	public partial class MenuManager
	{
");

		foreach (var name in strs)
			sb.AppendFormat("\t\tpublic {0} {0};\n", name);
		sb.AppendLine();
		var l = strs.ToList();
		l.Insert(0, "None");
		sb.Append(ScriptGenerators.CreateEnumString("MenuTypes", l));
		sb.Append(
				  @"	}
}");
		return sb.ToString();
	}
}