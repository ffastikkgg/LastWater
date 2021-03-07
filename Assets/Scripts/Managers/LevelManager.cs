using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    public bool win { get; set; }

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }


    private void Start()
    {
        win = false;
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void Update()
    {
        if (CurrentWave == 2)
        {
            win = true;
            VictoryShow();
        }
    }

    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
    }

    private void WaveCompleted()
    {
        CurrentWave++;
        AchievementsManager.Instance.AddProgress("Waves10", 1);
        AchievementsManager.Instance.AddProgress("Waves20", 1);
        AchievementsManager.Instance.AddProgress("Waves50", 1);
    }


    private void VictoryShow()
    {
        UIManager.Instance.ShowVictoryPanel();
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        Spawner.OnWaveCompleted += WaveCompleted;
        
        
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }

}
