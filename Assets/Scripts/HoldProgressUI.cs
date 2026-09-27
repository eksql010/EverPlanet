using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;
    [SerializeField] private string progressText = "상호작용 중...";

    private void Awake()
    {
        parent.SetActive(false);
    }

    public void Show()
    {
        parent.SetActive(true);
        text.text = progressText;
    }

    public void Hide()
    {
        parent.SetActive(false);
    }

    public void SetProgress(float value) => slider.value = value;
}
