using UnityEngine;

public class QuestDialogueWindow : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
        GameEvents.SetPause(true);

        Debug.Log("Äù½ºÆ® ´ë»çÃ¢ ¿ÀÇÂ");
    }

    public void Close()
    {
        gameObject.SetActive(false);
        GameEvents.SetPause(false);
    }
}
