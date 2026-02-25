using UnityEngine;
using UnityEngine.InputSystem;

public class DolphinDive : MonoBehaviour
{
    private AdvancedMoveController moveController;

    private void Awake()
    {
        moveController = GetComponent<AdvancedMoveController>();
    }

    public void OnDolphinDive()
    {
        if (moveController.isGrounded)
        {
            Debug.Log("Jump pressed.");
        }
        else
        {
            Debug.Log("Double jump would trigger");
        }

    }
}