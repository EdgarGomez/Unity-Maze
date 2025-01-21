using UnityEngine;

public class MovingBarrier : MonoBehaviour
{
    public enum MovementType
    {
        PingPong,
        ResetToStart
    }

    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float initialDelay = 0f;
    [SerializeField] private MovementType movementType;

    private Vector3 targetPosition;
    private bool hasStartedMoving = false;
    private float delayTimer = 0f;

    private void Start()
    {
        transform.position = pointA;
        targetPosition = pointB;
        delayTimer = initialDelay;
    }

    private void Update()
    {
        if (!hasStartedMoving)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0)
            {
                hasStartedMoving = true;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            switch (movementType)
            {
                case MovementType.PingPong:
                    targetPosition = targetPosition == pointA ? pointB : pointA;
                    break;

                case MovementType.ResetToStart:
                    transform.position = pointA;
                    targetPosition = pointB;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.Die();
        }
    }
}
