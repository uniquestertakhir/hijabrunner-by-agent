using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    bool grounded;
    [SerializeField] float laneHalfWidth = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Playing)
            return;

        float x = Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal") * 5f * Time.deltaTime, -laneHalfWidth, laneHalfWidth);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * Balance.JumpForce, ForceMode.VelocityChange);
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Playing)
            return;

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Balance.PlayerSpeed);
        rb.AddForce(Vector3.up * Balance.Gravity, ForceMode.Acceleration);
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                grounded = true;
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoins(Balance.CoinsPerPickup);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.HitObstacle();
        }
    }
}
