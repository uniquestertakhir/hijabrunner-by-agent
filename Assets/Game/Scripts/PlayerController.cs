using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float laneOffset = 2f;
    public float laneChangeSpeed = 10f;
    private CharacterController cc;
    private int lane = 1; // 0 left, 1 middle, 2 right
    private float targetX;
    private float verticalVel;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        targetX = transform.position.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            lane = Mathf.Max(0, lane - 1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            lane = Mathf.Min(2, lane + 1);

        targetX = (lane - 1) * laneOffset;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        float deltaX = newX - transform.position.x;

        float speed = Balance.PlayerSpeed;
        float gravity = Balance.Gravity;
        float jump = Balance.JumpForce;

        if (cc.isGrounded)
        {
            verticalVel = -0.1f;
            if (Input.GetKeyDown(KeyCode.Space))
                verticalVel = jump;
        }
        else
        {
            verticalVel += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(deltaX, verticalVel * Time.deltaTime, speed * Time.deltaTime);
        cc.Move(move);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            GameManager.Instance.HitObstacle();
        }
    }
}
