using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    public event Action<QuestData> OnQuestAccepted;
    public event Action<QuestData> OnObjectiveComplete;
    public event Action<QuestData> OnQuestCompleted;

    private Dictionary<string, QuestProgress> quests = new Dictionary<string, QuestProgress>();
    private Dictionary<string, Transform> objectiveLocations = new();   // 퀘스트 상호작용 오브젝트 위치 알 때 사용

    private void Awake()
    {
        instance = this;
    }

    public QuestState GetState(string questId)
    {
        return quests.TryGetValue(questId, out var questProgress) ? questProgress.state : QuestState.NotStarted;
    }

    public void AcceptQuest(QuestData questData)
    {
        quests[questData.questId] = new QuestProgress { data = questData, state = QuestState.InProgress };
        OnQuestAccepted?.Invoke(questData);
        GameEvents.QuestAccepted(questData);

        Debug.Log("퀘스트 수락 : " + questData.title);
    }

    public void AchieveQuestObjective(string questId)
    {
        if (!quests.TryGetValue(questId, out var questProgress) || questProgress.state != QuestState.InProgress)
            return;

        questProgress.state = QuestState.ObjectiveComplete;
        OnObjectiveComplete?.Invoke(questProgress.data);
        GameEvents.QuestObjectiveAchieved(questId);

        Debug.Log("퀘스트 목표 달성 : " + questProgress.data.title);
    }

    public void CompleteQuest(string questId)
    {
        if (!quests.TryGetValue(questId, out var questProgress) || questProgress.state != QuestState.ObjectiveComplete)
            return;

        questProgress.state = QuestState.Completed;
        OnQuestCompleted?.Invoke(questProgress.data);

        Debug.Log("퀘스트 완료 : " + questProgress.data.title);
        Debug.Log("퀘스트 보상 : " + "+경험치(" + questProgress.data.rewardExp + ")" + " +마블(" + questProgress.data.rewardMoney + ")");
    }

    public void RegisterObjectiveLocation(string questId, Transform location)
    {
        objectiveLocations[questId] = location;
    }

    public Transform GetObjectiveLocation(string questId)
    {
        return objectiveLocations.TryGetValue(questId, out var transform) ? transform : null;
    }
}
