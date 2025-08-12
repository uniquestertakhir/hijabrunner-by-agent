using UnityEngine;

public class PlayerMoveExample : MonoBehaviour
{
    private CharacterController _cc;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        if (_cc == null) _cc = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        // Simple endless-runner-ish forward movement & jump sample using Balance values
        Vector3 move = Vector3.forward * Balance.PlayerSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move.y = Balance.JumpForce;
        }
        move.y += Balance.Gravity * Time.deltaTime;
        _cc.Move(move * Time.deltaTime);
    }
}
