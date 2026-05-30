using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class UIScreenConstantsGenerator : EditorWindow
{
    private UIFactorySO uiFactory;

    [MenuItem("Tools/Generate UIScreen Constants")]
    public static void ShowWindow()
    {
        GetWindow<UIScreenConstantsGenerator>("UIScreen Generator");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("UI Factory ScriptableObject", EditorStyles.boldLabel);
        uiFactory = (UIFactorySO)EditorGUILayout.ObjectField(uiFactory, typeof(UIFactorySO), false);

        if (uiFactory == null)
        {
            EditorGUILayout.HelpBox("Assign a UIFactorySO ScriptableObject.", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Generate UIScreenConstants Class"))
        {
            GenerateUIScreenConstants(uiFactory);
        }
    }

    private void GenerateUIScreenConstants(UIFactorySO factory)
    {
        string folderPath = "Assets/_Scripts/UI/Generated"; // You can change this
        string filePath = Path.Combine(folderPath, "UIScreenConstants.cs");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("/// Auto-generated on " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " by UIScreenConstantsGenerator");
        builder.AppendLine("public static class UIScreenConstants");
        builder.AppendLine("{");

        HashSet<string> added = new HashSet<string>();

        foreach (var screen in factory.screens)
        {
            if (string.IsNullOrEmpty(screen.screenID)) continue;

            string safeName = MakeSafeVariableName(screen.screenID);

            if (added.Contains(safeName))
            {
                Debug.LogWarning($"Duplicate screenID '{screen.screenID}' resolved to same constant name '{safeName}'. Skipping.");
                continue;
            }

            builder.AppendLine($"    public const string {safeName} = \"{screen.screenID}\";");
            added.Add(safeName);
        }

        builder.AppendLine("}");

        File.WriteAllText(filePath, builder.ToString());
        AssetDatabase.Refresh();
        Debug.Log("UIScreenConstants.cs generated at: " + filePath);
    }

    private string MakeSafeVariableName(string input)
    {
        // Remove whitespace and invalid characters
        string safe = input.Replace(" ", "").Replace("-", "_");

        // Make sure it starts with a valid character
        if (!char.IsLetter(safe, 0))
            safe = "_" + safe;

        return safe;
    }
}
