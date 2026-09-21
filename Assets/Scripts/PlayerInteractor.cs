using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private IInteractable curTarget;

    private void Update()
    {
        if (curTarget != null && input.isInteractPressed)
            curTarget.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
            curTarget = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (curTarget != null && interactable == curTarget)
            curTarget = null;
    }
}
