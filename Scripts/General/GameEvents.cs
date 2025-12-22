using System;
using Godot;


public class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;
    public static event Action OnWinGame;

    public static void RaiseStartGame() => OnStartGame?.Invoke();

    public static void RaiseEndGame() => OnEndGame?.Invoke();
    public static void RaiseWinGame() => OnWinGame?.Invoke();


    public static event Action SwitchActivated;
    public static event Action SwitchDeactivated;

    public static void RaiseSwitchActivated() => SwitchActivated?.Invoke();
    public static void RaiseSwitchDectivated() => SwitchDeactivated?.Invoke();
}
