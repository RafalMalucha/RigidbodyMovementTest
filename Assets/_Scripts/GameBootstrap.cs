using System;
using UnityEngine;

public class GameBootstrap
{
    public static MessageBus MessageBus { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        Debug.Log("init test");
        MessageBus = new MessageBus();
    }
}
