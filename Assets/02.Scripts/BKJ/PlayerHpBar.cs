using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public static PlayerHpBar Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerHpBar>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerHpBar");
                    instance = instanceContainer.AddComponent<PlayerHpBar>();
                }
            }
            return instance;
        }
    }
    private static PlayerHpBar instance;

    public Transform player;
    public Slider hpBar;

    public GameObject HpLineFolder;
    public GameObject HpLine;
    public Text playerHpText;
    float unitHp = 200f;
    int hpLv;

    public void Start()
    {
        hpLv = PlayerData.Instance.playerSkill[5];
    }

    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(player.transform.position.x, 3.0f, player.transform.position.z + 0.5f), transform.rotation);

        hpBar.value = Mathf.Lerp(hpBar.value, PlayerData.Instance.currentHp / PlayerData.Instance.maxHp, Time.deltaTime * 5);

        playerHpText.text = PlayerData.Instance.currentHp.ToString();

        // 스킬이 증가하면 hp를 증가시키는 함수를 불러온다.
        if (hpLv != PlayerData.Instance.playerSkill[5] && hpLv < PlayerData.Instance.playerSkill[5])
        {
            GetHpBoost();
            hpLv = PlayerData.Instance.playerSkill[5];
        }
    }

    public void GetHpBoost()
    {
        PlayerData.Instance.maxHp += 150;
        PlayerData.Instance.currentHp += 150;
        float scaleX = (1000f / unitHp) / (PlayerData.Instance.maxHp / unitHp);
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);

        Instantiate(HpLine, HpLineFolder.transform);

        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}