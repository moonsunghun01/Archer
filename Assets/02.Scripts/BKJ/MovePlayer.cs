using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public static MovePlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MovePlayer>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("MovePlayer");
                    instance = instanceContainer.AddComponent<MovePlayer>();
                }
            }
            return instance;
        }
    }
    private static MovePlayer instance;

    Rigidbody playerRigidbody;
    public Transform playerTransform;
    public Animator playerAnimator;
    float moveHorizontal;
    float moveVertical;
    public float moveSpeed;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        moveSpeed = 7.0f;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerRigidbody.velocity = new Vector3(moveHorizontal * moveSpeed, playerRigidbody.velocity.y, moveVertical * moveSpeed);
        playerRigidbody.velocity = new Vector3(MoveJoystick.Instance.joystickVector.x * moveSpeed, playerRigidbody.velocity.y, MoveJoystick.Instance.joystickVector.y * moveSpeed);
        playerRigidbody.rotation = Quaternion.LookRotation(new Vector3(MoveJoystick.Instance.joystickVector.x, 0, MoveJoystick.Instance.joystickVector.y));
    }
}
