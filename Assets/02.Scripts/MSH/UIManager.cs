using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("UI");
                    instance = instanceContainer.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    private static UIManager instance;

    public bool isBossRoom;
    public Slider BossHpBarBack;
    public Slider BossHpBar;
    public TextMeshProUGUI coinText;
   // public Slider PlayerExpBar;
   // public Text playerLvText;

    public float BossCurrentHp;
    public float BossMaxHp;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerExpBar.gameObject.SetActive(true);
        //playerLvText.gameObject.SetActive(true);
        BossHpBarBack.gameObject.SetActive(false);
        BossHpBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBossRoom)
        {
          //  PlayerExpBar.gameObject.SetActive(false);
          //  playerLvText.gameObject.SetActive(false);

            BossHpBarBack.gameObject.SetActive(true);
            BossHpBar.gameObject.SetActive(true);

            BossHpBar.value = Mathf.Lerp(BossHpBar.value, BossCurrentHp / BossMaxHp, Time.deltaTime * 5);
        }
        else if (!isBossRoom)
        {
            BossHpBarBack.gameObject.SetActive(false);
            BossHpBar.gameObject.SetActive(false);

        }

        coinText.text = PlayerData.Instance.inGameMoney.ToString();
    }
}
