using System;
using UnityEngine;

// global even everyone can listen or send from here
public static class GameEvents
{
    public static event Action<Vector3> OnEnemyDied;
    public static event Action<int> OnWaveCompleted;
    public static event Action<int> OnScoreChanged;

    public static void RaiseEnemyDied(Vector3 position)
    {
        OnEnemyDied?.Invoke(position);
    }

    public static void RaiseWaveCompleted(int waveNumber)
    {
        OnWaveCompleted?.Invoke(waveNumber);
        
    }

    public static void RaiseScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);

    }
}