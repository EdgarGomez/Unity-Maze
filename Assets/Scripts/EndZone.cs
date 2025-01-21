using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text endMessageText;
    [SerializeField] private GameManagerSO gameManager;


    private void Start()
    {
        endPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            gameManager.EndGame();
            timer.StopTimer();
            string finalTime = timer.GetCurrentTimeFormatted();
            endMessageText.text = $"Congratulations!\n\n You made it.\n You beat The Maze in {finalTime}.\n\n You can restart The Maze by pressing R.";
            endPanel.SetActive(true);
            timer.gameObject.SetActive(false);
            other.enabled = false;
        }
    }

    private void Update()
    {
        if (endPanel.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
    }
}
