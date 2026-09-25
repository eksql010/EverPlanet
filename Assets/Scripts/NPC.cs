using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestData quest;
    [SerializeField] private QuestDialogueWindow dialogueWindow;

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

        dialogueWindow.Open();
    }
}
