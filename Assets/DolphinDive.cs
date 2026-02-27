using UnityEngine;
using UnityEngine.InputSystem;

public class DolphinDive : MonoBehaviour
{
    private AdvancedMoveController moveController;
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float forwardForce = 20f;
    [SerializeField] private float upwardForce = 5f;
    [SerializeField] private float cooldownTime = 0.2f;

    [SerializeField] private float downwardForce = 10f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip diveSound;

    [SerializeField] private string diveTriggerName = "DolphinDive";

    private bool canDolphinDive = true;
    private float lastDiveTime;

    private float diveAirTimer = 0f;
    private bool slamApplied = false;
    private bool diveActive = false;

    private void Awake()
    {
        moveController = GetComponent<AdvancedMoveController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (moveController.isGrounded)
        {
            canDolphinDive = true;
            diveActive = false;
            slamApplied = false;
            diveAirTimer = 0f;
            return;
        }
    }

    public void OnDolphinDive()
    {
        if (moveController.isGrounded)
        {
            PlaySound(jumpSound);
            return;
        }

        if (!canDolphinDive || Time.time < lastDiveTime + cooldownTime)
        {
            return;
        }

        if (diveActive == true)
        {
            ApplyDownwardForce();
            return;
        }

        ExecuteDive();


    }

    private void ExecuteDive()
    {
        canDolphinDive = false;
        lastDiveTime = Time.time;

        diveActive = true;
        slamApplied = false;
        diveAirTimer = 0f;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;

        Vector3 diveDirection = transform.forward * forwardForce + Vector3.up * upwardForce;
        rb.AddForce(diveDirection, ForceMode.Impulse);

        PlaySound(diveSound);

        if (animator != null)
        {
            animator.SetTrigger(diveTriggerName);
        }
    }

    private void ApplyDownwardForce()
    {
        slamApplied = true;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector3.down * downwardForce, ForceMode.Impulse);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null)
        {
            return;
        }

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
        audioSource.pitch = 1f;
    }
}
