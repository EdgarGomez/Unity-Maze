using TMPro;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text endMessageText;

    private void Start()
    {
        endPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Time.timeScale = 0f;
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }
}
