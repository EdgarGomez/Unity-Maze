using UnityEngine;

public class StartZone : MonoBehaviour
{
    [SerializeField]
    private GameObject uiPanel;

    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            uiPanel.SetActive(false);
        }
    }
}