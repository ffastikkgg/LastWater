using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : Singleton<AchievementsManager>
{
    public static Action<Achievement> OnAchievementUnlock;
    public static Action<Achievement> OnProgressUpdated;

    [SerializeField] private AchievementCard achievementCardPrefab;
    [SerializeField] private Transform achievementPanelContainer;
    [SerializeField] private Achievement[] achievements;

    private void Start()
    {
        LoadAchievements(); 
    }

    private void LoadAchievements()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            AchievementCard card = Instantiate(achievementCardPrefab, achievementPanelContainer);
            card.SetupAchievement(achievements[i]);
        }
    }

    public void AddProgress(string achievementID, int amount)
    {
        Achievement achievementWanted = AchievementExists(achievementID);
        if (achievementWanted != null)
        {
            achievementWanted.AddProgress(amount);
        }
    }

    private Achievement AchievementExists(string achievementID)
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (achievements[i].ID == achievementID)
            {
                return achievements[i];
            }
        }
        return null;
    }
}
