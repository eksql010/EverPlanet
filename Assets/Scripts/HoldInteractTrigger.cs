using UnityEngine;
using UnityEngine.Events;

public class HoldInteractTrigger : MonoBehaviour
{
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private HoldProgressUI progressUI;
    [SerializeField] private PlayerInput playerInput;

    public UnityEvent OnHoldComplete;

    private bool playerInRange;
    private bool isHolding;
    private bool isHoldReleased = true;
    private float holdTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        playerInRange = false;
        isHolding = false;
        isHoldReleased = true;
        holdTimer = 0f;

        progressUI.Hide();
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        if (!isHoldReleased)
        {
            if (!playerInput.isInteractHeld)
                isHoldReleased = true;
            return;
        }

        if (playerInput.isInteractHeld)
        {
            if (!isHolding)
            {
                isHolding = true;
                progressUI.Show();
            }

            holdTimer += Time.deltaTime;
            progressUI.SetProgress(holdTimer / holdDuration);

            if (holdTimer >=  holdDuration)
            {
                progressUI.Hide();
                OnHoldComplete?.Invoke();
                holdTimer = 0f;
                isHolding = false;
                isHoldReleased = false;
            }
        }
        else
        {
            if (isHolding)
            {
                isHolding = false;
                progressUI.Hide();
            }

            holdTimer = 0f;
            progressUI.SetProgress(0f);
        }
    }
}
