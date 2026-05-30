using UnityEditor;
using UnityEngine;
using System.Text;
using System.IO;
using System.Collections.Generic;

[CustomEditor(typeof(UIFactorySO))]
public class UIFactorySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("UIScreen Constants Generator", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate UIScreenConstants Class"))
        {
            UIFactorySO factory = (UIFactorySO)target;
            GenerateUIScreenConstants(factory);
        }
    }

    private void GenerateUIScreenConstants(UIFactorySO factory)
    {
        string folderPath = "Assets/_Scripts/UI/Generated"; // Change as needed
        string filePath = Path.Combine(folderPath, "UIScreenConstants.cs");

        // Ensure folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Delete old file if it exists
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // Generate class
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("// Auto-generated on " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " by UIFactorySOEditor");
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

        // Write the new file
        File.WriteAllText(filePath, builder.ToString());
        AssetDatabase.Refresh();

        Debug.Log("UIScreenConstants.cs successfully generated at: " + filePath);
    }

    private string MakeSafeVariableName(string input)
    {
        string safe = input.Replace(" ", "").Replace("-", "_");

        if (!char.IsLetter(safe, 0))
            safe = "_" + safe;

        return safe;
    }
}
