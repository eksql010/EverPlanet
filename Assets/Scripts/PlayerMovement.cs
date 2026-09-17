using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = transform.right * input.horizontalAxis + transform.forward * input.verticalAxis;
        moveDir.Normalize();

        rigid.linearVelocity = moveDir * moveSpeed;
    }
}
