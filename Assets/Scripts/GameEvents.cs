using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<bool> OnPause;

    public static void SetPause(bool isPause)
    {
        OnPause?.Invoke(isPause);
    }
}
