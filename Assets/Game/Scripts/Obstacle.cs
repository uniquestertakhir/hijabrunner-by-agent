using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
        gameObject.tag = "Obstacle";
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.HitObstacle();
            Destroy(gameObject);
        }
    }
}
