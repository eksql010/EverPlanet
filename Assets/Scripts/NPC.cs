using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestDialogueWindow dialogueWindow;
    [SerializeField, TextArea] private string greetingDialogue;

    [SerializeField] private QuestData[] giveQuests;            // 이 NPC가 시작 시켜주는 퀘스트
    [SerializeField] private QuestData[] receiveQuests;         // 이 NPC가 보고를 받는 퀘스트
    [SerializeField] private QuestData[] talkObjectiveQuests;   // 이 NPC와 대화하는 것 자체가 목표인 퀘스트

    public string GreetingDialogue => greetingDialogue;
    public QuestData[] GiveQuests => giveQuests;
    public QuestData[] ReceiveQuests => receiveQuests;


    private void Start()
    {
        // 이 NPC와 대화가 퀘스트 목표면 화살표 UI가 가리킬 위치로 등록
        //  foreach (var quest in talkObjectiveQuests)
        //      QuestManager.instance.RegisterObjectiveLocation(quest.questId, transform);
    }

    private void OnEnable()
    {
        GameEvents.OnQuestAccepted += HandleQuestAccepted;
    }

    private void OnDisable()
    {
        GameEvents.OnQuestAccepted -= HandleQuestAccepted;
    }

    private void HandleQuestAccepted(QuestData quest)
    {
        if (IsTalkObjective(quest))
            QuestManager.instance.AchieveQuestObjective(quest.questId);
    }

    public void Interact()
    {
        dialogueWindow.Open(this);
    }

    public List<QuestData> GetVisibleQuests()
    {
        var result = new List<QuestData>();

        foreach (var quest in giveQuests)
        {
            if (QuestManager.instance.GetState(quest.questId) == QuestState.NotStarted)
                result.Add(quest);
        }

        foreach (var quest in receiveQuests)
        {
            QuestState state = QuestManager.instance.GetState(quest.questId);

            if (state == QuestState.InProgress || state == QuestState.ObjectiveComplete)
                result.Add(quest);
        }

        return result;
    }

    public bool IsTalkObjective(QuestData quest) => talkObjectiveQuests.Contains(quest);
}
