using UnityEngine;

public class QuestInteractObjective : MonoBehaviour
{
    [SerializeField] private QuestData quest;

    private void Start()
    {
        QuestManager.instance.RegisterObjectiveLocation(quest.questId, transform);
    }

    public void CompleteObjective()
    {
        QuestManager.instance.AchieveQuestObjective(quest.questId);
    }
}
