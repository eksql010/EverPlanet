using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
public class QuestData : ScriptableObject
{
    public string questId;
    public string title;
    public string description;
    public int rewardExp;
    public int rewardMoney;

    [TextArea] public string[] introDialogue;               // 미수락 상태에서 말 걸 때
    [TextArea] public string[] inProgressDialogue;          // 퀘스트 진행 중 말 걸 때
    [TextArea] public string[] objectiveCompleteDialogue;   // 목표 달성 후 말 걸 때
    [TextArea] public string[] completedDialogue;           // 완료 후 말 걸 때
}
