using TMPro;
using UnityEngine;

public class DialoguePager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;

    private string[] pages;
    private int pageIndex;

    public void Play(string[] newPages)
    {
        pages = newPages;
        ShowPage(0);
    }

    public void Next()
    {
        if (pageIndex < pages.Length - 1)
            ShowPage(pageIndex + 1);
    }

    public void Prev()
    {
        if (pageIndex > 0)
            ShowPage(pageIndex - 1);
    }

    public void Skip()
    {
        ShowPage(pages.Length - 1);
    }

    private void ShowPage(int index)
    {
        pageIndex = index;

        dialogueText.text = pages[pageIndex];
    }

}
