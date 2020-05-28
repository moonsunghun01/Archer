using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieScript : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private float speed;

    private Touch tempTouchs;
    private Vector3 touchedPos;
    private bool touchOn;

    public GameObject image;
    public Text inGameMoney;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        inGameMoney = GameObject.Find("InGameMoney").GetComponent<Text>();
        inGameMoney.text = "획득한 골드 : " + PlayerData.Instance.inGameMoney;

        PlayerData.Instance.money += PlayerData.Instance.inGameMoney;
        PlayerData.Instance.inGameMoney = 0;
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        speed = -5.0f;
        PlayerData.Instance.playerSkill[0] = 0;
        PlayerData.Instance.playerSkill[1] = 0;
        PlayerData.Instance.playerSkill[2] = 0;
        PlayerData.Instance.playerSkill[3] = 0;
        touchOn = false;

        PlayerData.Instance.maxHp = 500 + PlayerData.Instance.playerSkill[5] * 200;
        PlayerData.Instance.currentHp = PlayerData.Instance.maxHp;

        PlayerData.Instance.updateJson();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.velocity = new Vector3(0, 0, speed);

        if (transform.position.z <= 0.5f)
        {
            playerAnimator.SetBool("DIE", true);
            speed = 0;
        }

        if (touchOn)
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
                PlayerData.Instance.money += PlayerData.Instance.inGameMoney;
                PlayerData.Instance.inGameMoney = 0;
                PlayerData.Instance.updateDataBase();
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "OnText")
        {
            image.SetActive(false);
            touchOn = true;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
