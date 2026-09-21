using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }
    
    private Dictionary<string, QuestProgress> quests = new Dictionary<string, QuestProgress>();

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
        Debug.Log("퀘스트 수락 : " + questData.title);
    }

    public void AchieveQuestObjective(string questId)
    {
        if (!quests.TryGetValue(questId, out var questProgress) || questProgress.state != QuestState.InProgress)
            return;

        questProgress.state = QuestState.ObjectiveComplete;
        Debug.Log("퀘스트 목표 달성 : " + questProgress.data.title);
    }

    public void CompleteQuest(string questId)
    {
        if (!quests.TryGetValue(questId, out var questProgress) || questProgress.state != QuestState.ObjectiveComplete)
            return;

        questProgress.state = QuestState.Completed;
        Debug.Log("퀘스트 완료 : " + questProgress.data.title);
        Debug.Log("퀘스트 보상 : " + "+경험치(" + questProgress.data.rewardExp + ")" + " +마블(" + questProgress.data.rewardMoney + ")");
    }
}
