using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
public class QuestData : ScriptableObject
{
    public string questId;
    public string title;
    public string description;
    public string objectiveText;
    public int rewardExp;
    public int rewardMoney;

    [TextArea] public string[] startDialogue;
    [TextArea] public string[] progressDialogue;
    [TextArea] public string[] completeDialogue;
}
