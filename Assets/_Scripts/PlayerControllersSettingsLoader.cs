using System.IO;
using UnityEngine;

public class PlayerControllersSettingsLoader
{
    private readonly string _settingsPath = "Assets/StreamingAssets/playerControllersSettings.json";

    public PlayerControllersSettings LoadPlayerControllersSettings()
    {
        if (!File.Exists(_settingsPath))
        {
            return null;
        }

        string json = File.ReadAllText(_settingsPath);

        Debug.Log($"Loaded controller settings:\n{json}");

        PlayerControllersSettings settings = JsonUtility.FromJson<PlayerControllersSettings>(json);

        return settings;
    }
}
