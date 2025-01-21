using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private int buttonId;
    [SerializeField] private AudioSource audioSource;


    public void ButtonPressed()
    {
        audioSource.Play();
        gameManager.ButtonPressed(buttonId);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.ButtonPressed(buttonId);
        }
    }
}
