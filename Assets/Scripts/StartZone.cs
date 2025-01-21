using UnityEngine;

public class StartZone : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameManagerSO gameManager;


    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            gameManager.StartGame();
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