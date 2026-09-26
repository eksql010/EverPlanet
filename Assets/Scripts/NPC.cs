using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestDialogueWindow dialogueWindow;
    [SerializeField, TextArea] private string greetingDialogue;
    [SerializeField] private QuestData[] startQuests;

    public string GreetingDialogue => greetingDialogue;

    public void Interact()
    {
        //  QuestState state = QuestManager.instance.GetState(quest.questId);
        //  
        //  if (state == QuestState.NotStarted)
        //      QuestManager.instance.AcceptQuest(quest);
        //  else if (state == QuestState.ObjectiveComplete)
        //      QuestManager.instance.CompleteQuest(quest.questId);
        //  else
        //      Debug.Log("현재 퀘스트 상태 : " + state.ToString());

        dialogueWindow.Open(this);
    }

    public List<QuestData> GetVisibleQuests()
    {
        var result = new List<QuestData>();

        foreach (var quest in startQuests)
        {
            if (QuestManager.instance.GetState(quest.questId) == QuestState.NotStarted)
                result.Add(quest);
        }

        return result;
    }
}
