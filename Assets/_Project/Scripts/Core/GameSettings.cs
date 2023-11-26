using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static float GameSessionTime { get; private set; } = 2f;
    public static float EnemySpawnTime { get; private set; } = 6f;

    public static void SetGameSessionTime(float gameSessionTime)
    {
        GameSessionTime = gameSessionTime;
    }

    public static void SetEnemySpawnTime(float enemySpawnTime)
    {
        EnemySpawnTime = enemySpawnTime;
    }
}
