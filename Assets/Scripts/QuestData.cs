using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
public class QuestData : ScriptableObject
{
    public string questId;
    public string title;
    public string description;
    public int rewardExp;
    public int rewardMoney;
}
