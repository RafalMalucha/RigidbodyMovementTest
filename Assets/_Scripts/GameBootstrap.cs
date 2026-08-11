using System;
using UnityEngine;

public class GameBootstrap
{
    public static MessageBus MessageBus { get; private set; }
    public static PlayerControllersSettings PlayerControllersSettings { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void CreateMessageBus()
    {
        Debug.Log("init test");
        MessageBus = new MessageBus();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void CreatePlayerControllersSettingsObject()
    {
        PlayerControllersSettingsLoader loader = new PlayerControllersSettingsLoader();
        PlayerControllersSettings = loader.LoadPlayerControllersSettings();
    }
}
