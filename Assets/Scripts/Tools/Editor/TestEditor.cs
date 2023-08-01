using UnityEditor;
using UnityEngine;

public class MyEditorScript : EditorWindow
{
    private string _scriptPath;
    private long _metaLastModified;

    private void OnEnable()
    {
        _scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
        _metaLastModified = new System.IO.FileInfo(_scriptPath + ".meta").LastWriteTime.Ticks;
    }

    private void Update()
    {
        // Check if the .meta file has been modified since the last update
        long currentMetaLastModified = new System.IO.FileInfo(_scriptPath + ".meta").LastWriteTime.Ticks;
        if (currentMetaLastModified != _metaLastModified)
        {
            // .meta file has changed, do something here
            Debug.Log("Script has changed!");

            // Update the last known modification time
            _metaLastModified = currentMetaLastModified;
        }
    }
}