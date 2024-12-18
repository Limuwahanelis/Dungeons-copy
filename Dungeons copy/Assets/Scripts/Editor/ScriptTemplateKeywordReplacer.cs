using UnityEditor;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

public class ScriptTemplateKeywordReplacer : UnityEditor.AssetModificationProcessor
{
    //If there would be more than one keyword to replace, add a Dictionary

    public static void OnWillCreateAsset(string metaFilePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(metaFilePath);

        if (!fileName.EndsWith(".cs"))
            return;


        string actualFilePath = $"{Path.GetDirectoryName(metaFilePath)}{Path.DirectorySeparatorChar}{fileName}";
        string fileNameNoExtensions = Path.GetFileNameWithoutExtension(fileName);

        string content = File.ReadAllText(actualFilePath);
        string newcontent = content.Replace("#PROJECTNAME#", PlayerSettings.productName);
       
        string customEditorSufix = "Editor";
        string stateTypeSufix = "State";
        int index1 = fileNameNoExtensions.IndexOf(customEditorSufix);
        int index2 = fileNameNoExtensions.IndexOf(stateTypeSufix);

        if (index1 != -1) 
        {
            newcontent = newcontent.Replace("#EDITORNAME#", fileNameNoExtensions.Substring(0, index1));
        }
        if (index2 != -1)
        {
            newcontent = newcontent.Replace("#STATETYPE#", fileNameNoExtensions.Substring(0, index2));
        }
        if (content != newcontent)
        {
            File.WriteAllText(actualFilePath, newcontent);
            AssetDatabase.Refresh();
        }
    }
}