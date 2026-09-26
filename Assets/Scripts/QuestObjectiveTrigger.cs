using UnityEngine;

public class QuestObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private QuestData quest;

    private void Start()
    {
        QuestManager.instance.RegisterObjectiveLocation(quest.questId, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            QuestManager.instance.AchieveQuestObjective(quest.questId);
    }
}
