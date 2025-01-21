using UnityEngine;

public class BombCable : MonoBehaviour
{
    [SerializeField] private GameObject cableVisual;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AudioSource audioSource;

    private bool hasExploded = false;

    private void Start()
    {
        explosionEffect.Stop();
        explosionEffect.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasExploded && other.CompareTag("Player"))
        {
            hasExploded = true;
            audioSource.Play();
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            //cableVisual.SetActive(false);
            explosionEffect.gameObject.SetActive(true);
            explosionEffect.Play();

            float explosionDuration = explosionEffect.main.duration;
            Invoke("KillPlayer", explosionDuration);
        }
    }

    private void KillPlayer()
    {
        if (FindObjectOfType<PlayerController>() is PlayerController player)
        {
            player.Die();
        }
        Destroy(gameObject);
    }
}
