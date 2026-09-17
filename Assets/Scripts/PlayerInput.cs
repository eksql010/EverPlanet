using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalAxis {  get; private set; }
    public float verticalAxis { get; private set; }

    private void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }
}
