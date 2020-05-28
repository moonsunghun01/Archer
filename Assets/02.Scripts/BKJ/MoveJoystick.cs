using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour
{
    public static MoveJoystick Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MoveJoystick>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("MoveJoystick");
                    instance = instanceContainer.AddComponent<MoveJoystick>();
                }
            }
            return instance;
        }
    }
    private static MoveJoystick instance;
    //public Animator playerAnimator;

    public GameObject smallStick;
    public GameObject joystickBackground;
    Vector3 stickFirstPosition;
    public Vector3 joystickVector;
    Vector3 joyStickFirstPosition;
    float stickRadius;
    //Vector3 theTouch;

    public bool isPlayerMoving = false;

    void Start()
    {
        //playerAnimator = GetComponent<Animator>();
        stickRadius = joystickBackground.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        joyStickFirstPosition = joystickBackground.transform.position;
    }

    public void PointDown()
    {
        if (Input.touchCount <= 0)
        {
            joystickBackground.transform.position = Input.mousePosition;
            smallStick.transform.position = Input.mousePosition;
            stickFirstPosition = Input.mousePosition;
        }
        else
        {
            Vector2 pos = Input.GetTouch(0).position;

            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);

            joystickBackground.transform.position = theTouch;
            smallStick.transform.position = theTouch;
            stickFirstPosition = theTouch;
        }

        if (!MovePlayer.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MOVE"))
        {
            //Debug.Log ( "MOVE!" );
            MovePlayer.Instance.playerAnimator.SetBool("ATTACK", false);
            MovePlayer.Instance.playerAnimator.SetBool("IDLE", false);
            MovePlayer.Instance.playerAnimator.SetBool("MOVE", true);
        }

        isPlayerMoving = true;
        PlayerTargeting.Instance.getATarget = false;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 dragPosition = pointerEventData.position;
        joystickVector = (dragPosition - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(dragPosition, stickFirstPosition);

        if (stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joystickVector * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joystickVector * stickRadius;
        }
    }

    public void Drop()
    {
        joystickVector = Vector3.zero;
        joystickBackground.transform.position = joyStickFirstPosition;
        smallStick.transform.position = joyStickFirstPosition;

        if (!MovePlayer.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
        {
            //            Debug.Log ( "IDLE!" );
            MovePlayer.Instance.playerAnimator.SetBool("ATTACK", false);
            MovePlayer.Instance.playerAnimator.SetBool("MOVE", false);
            MovePlayer.Instance.playerAnimator.SetBool("IDLE", true);
        }

        isPlayerMoving = false;
    }
}