using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalAxis { get; private set; }
    public float verticalAxis { get; private set; }
    public bool isInteractPressed { get; private set; }

    private bool isPause;

    private void OnEnable()
    {
        GameEvents.OnPause += HandlePauseChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnPause -= HandlePauseChanged;
    }

    private void HandlePauseChanged(bool isPaused) => isPause = isPaused;

    private void Update()
    {
        if (isPause)
        {
            isInteractPressed = false;
            return;
        }

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        isInteractPressed = Input.GetButtonDown("Interact");
    }
}
