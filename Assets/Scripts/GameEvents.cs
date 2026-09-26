using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<bool> OnPause;
    public static event Action<QuestData> OnQuestAccepted;
    public static event Action<string> OnQuestObjectiveAchieved;

    public static void SetPause(bool isPause) => OnPause?.Invoke(isPause);
    public static void QuestAccepted(QuestData quest) => OnQuestAccepted?.Invoke(quest);
    public static void QuestObjectiveAchieved(string questId) => OnQuestObjectiveAchieved?.Invoke(questId);
}
