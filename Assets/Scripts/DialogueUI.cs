using System;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance {  get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingInterval = 0.05f;

    private string[] pages;
    private int pageIndex;
    private bool isTyping;
    private Coroutine typingCoroutine;
    private Action onFinished;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        
    }
}
