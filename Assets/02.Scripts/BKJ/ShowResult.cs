using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowResult : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private float speed;
    
    private Touch tempTouchs;
    private Vector3 touchedPos;
    private bool touchOn;

    public Text inGameMoney;

    void Start()
    {
        MoveJoystick.Instance.Drop();

        inGameMoney = GameObject.Find("InGameMoney").GetComponent<Text>();
        inGameMoney.text = "획득한 골드 : " + PlayerData.Instance.inGameMoney;

        PlayerData.Instance.money += PlayerData.Instance.inGameMoney;
        PlayerData.Instance.inGameMoney = 0;

        PlayerData.Instance.playerSkill[0] = 0;
        PlayerData.Instance.playerSkill[1] = 0;
        PlayerData.Instance.playerSkill[2] = 0;
        PlayerData.Instance.playerSkill[3] = 0;
         
        PlayerData.Instance.updateJson();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouchs = Input.GetTouch(i);
                if (tempTouchs.phase == TouchPhase.Began)
                {
                    SceneManager.LoadScene(0);
                    break;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            PlayerData.Instance.currentHp = PlayerData.Instance.maxHp;
            PlayerData.Instance.money += PlayerData.Instance.inGameMoney;
            PlayerData.Instance.inGameMoney = 0; 
            PlayerData.Instance.updateDataBase(); 
            SceneManager.LoadScene(0);
        }
    }
}
