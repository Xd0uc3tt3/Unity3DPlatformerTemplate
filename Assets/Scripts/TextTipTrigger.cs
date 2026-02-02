using UnityEngine;
using TMPro;

public class TextTipTrigger : MonoBehaviour
{
    [SerializeField] private GameObject textTip;

    private void Start()
    {
        if (textTip != null)
        {
            textTip.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textTip.SetActive(true);
        }
    }
}

