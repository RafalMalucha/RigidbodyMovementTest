using UnityEngine;
using System.Collections;
using System;

public class CrosshairDataLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadFile("crosshair.json", OnFileLoaded));
    }

    void OnFileLoaded(string loadedText)
    {
        if (string.IsNullOrEmpty(loadedText))
        {
            Debug.LogError("File data was empty or failed to load.");
            return;
        }

        // The data is now safely out of the coroutine.
        // You can parse it here (e.g., JsonUtility, XML, custom parsing)
        Debug.Log("File loaded successfully: " + loadedText);
    }

    IEnumerator LoadFile(string fileName, Action<string> callback)
    {
        string fileContent = "";
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        // Regular file path on most platforms and in Editor
        if (System.IO.File.Exists(filePath))
        {
            fileContent = System.IO.File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError($"File not found at: {filePath}");
        }

        // Send the loaded content back to the caller
        callback?.Invoke(fileContent);
        yield return null;
    }
}
