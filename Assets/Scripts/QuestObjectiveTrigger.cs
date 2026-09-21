using UnityEngine;

public class QuestObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private QuestData quest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            QuestManager.instance.AchieveQuestObjective(quest.questId);
    }
}
