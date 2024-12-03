using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 100f;
    public float movementSpeed = 5f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float turn = Input.GetAxis("Horizontal");
        float movement = Input.GetAxis("Vertical");

        // Girar al personaje en el eje Y
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        // Calcular el vector de movimiento en el espacio local del personaje
        Vector3 movimientoDireccion = transform.TransformDirection(Vector3.forward) * movement * movementSpeed;
        // Ajustar el movimiento por el tiempo transcurrido entre frames para una velocidad consistente
        movimientoDireccion *= Time.deltaTime;
        // Mover al personaje usando el CharacterController.
        characterController.Move(movimientoDireccion);
    }
}