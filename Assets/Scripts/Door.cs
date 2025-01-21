using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private int doorId;
    [SerializeField] private AudioSource audioSource;

    private bool isDoorOpen = false;
    void Start()
    {
        gameManager.OnButtonPressed += OpenDoor;
    }

    private void OpenDoor(int buttonId)
    {
        if (buttonId == doorId)
        {
            isDoorOpen = !isDoorOpen;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isDoorOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, -2.6f, transform.position.z),
                5 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, 2.5f, transform.position.z),
                5 * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        gameManager.OnButtonPressed -= OpenDoor;
    }
}
