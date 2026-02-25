using UnityEngine;
using UnityEngine.InputSystem;

public class DolphinDive : MonoBehaviour
{
    private AdvancedMoveController moveController;
    private Rigidbody rb;

    [SerializeField] private float forwardForce = 20f;
    [SerializeField] private float upwardForce = 5f;
    [SerializeField] private float cooldownTime = 0.2f;

    private bool canDolphinDive = true;
    private float lastDiveTime;

    private void Awake()
    {
        moveController = GetComponent<AdvancedMoveController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (moveController.isGrounded)
        {
            canDolphinDive = true;
        }
    }

    public void OnDolphinDive()
    {
        if (moveController.isGrounded)
        {
            return;
        }
        if (!canDolphinDive || Time.time < lastDiveTime + cooldownTime)
        {
            return;
        }
        else
        {
            ExecuteDive();
        }

    }

    private void ExecuteDive()
    {
        canDolphinDive = false;
        lastDiveTime = Time.time;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;

        Vector3 diveDirection = transform.forward * forwardForce + Vector3.up * upwardForce;

        rb.AddForce(diveDirection, ForceMode.Impulse);
    }
}