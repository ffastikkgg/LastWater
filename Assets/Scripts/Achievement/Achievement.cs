using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;

    public bool IsUnlocked { get; set;}

    private int CurrentProgress;

    public void AddProgress(int amount)
    {
        CurrentProgress += amount;
        AchievementsManager.OnProgressUpdated?.Invoke(this);
        CheckUnlockStatus();
    }

    private void CheckUnlockStatus()
    {
        if (CurrentProgress >= ProgressToUnlock)
        {
            UnlockAchievement();        
        }

    }

    private void UnlockAchievement()
    {
        IsUnlocked = true;
        AchievementsManager.OnAchievementUnlock?.Invoke(this);
    }

    public string GetProgress()
    {
        return $"{CurrentProgress}/{ProgressToUnlock}";
    }

    public string GetProgressCompleted()
    {
        return $"{ProgressToUnlock}/{ProgressToUnlock}";
    }

    private void OnEnable()
    {
        IsUnlocked = false;
        CurrentProgress = 0;
    }

}
