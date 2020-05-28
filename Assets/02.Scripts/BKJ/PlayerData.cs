using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro; 
using MySql.Data.MySqlClient; 
using UnityEngine.Networking;


using System;
public class playerInfo
{ 
    public int money;
    public int atkPoint;
    public int hpPoint;
    public int AspPoint;


    public playerInfo(int money, int atkPoint, int hpPoint, int AspPoint)
    {
        
        this.money = money;
        this.atkPoint = atkPoint;
        this.hpPoint = hpPoint;
        this.AspPoint = AspPoint;
       
    }
} 
public class PlayerData : MonoBehaviour
{
  
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // DB확인 // 
        StartCoroutine(selectStart());
    } 
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerData>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("PlayerData");
                    instance = instanceContainer.AddComponent<PlayerData>();
                }
            }
            return instance;
        }
    }
    private static PlayerData instance;

    public float playerDamage = 1.0f;
     
    public float maxHp = 500;
    public float currentHp = 500.0f;

    public float atkSpeed = 1;

    public bool isAbs;
    public float nextAbsTime = 0;
    public float absRate = 1.5f;

    public int money = 0;
    public int inGameMoney = 0;

   
    /// <summary>
    /// 스킬 목록
    /// 000 : 멀티샷
    /// 001 : 전방화살 + 1
    /// 002 : 측면화살 + 1
    /// 003 : 사선화살 + 1
    /// 004 : 공격력 증가
    /// 005 : HP 증가
    /// 006 : 공격속도 증가
    /// 007 : 인게임 공격력 증가
    /// 008 : 인게임 Hp 증가
    /// 009 : 인게임 공격속도 증가 
    /// </summary>

    public List<int> playerSkill = new List<int>();

    public GameObject itemCoin;

    /////////////////////////////// DB 정보///////////////////////////////
    MySqlConnection sqlconn = null;
    private string sqlDBip = "msh4585.cafe24.com";
    private string sqlDBname = "msh6618";
    private string sqlDBid = "msh6618";
    private string sqlDBpw = "ruddlf123@";

    //////////////////////////////////////////////////////////////////  
    private void Start()
    {
        currentHp = maxHp;
    } 
    private void Update()
    { 
        if (isAbs)
        { 
            if (Time.time >= nextAbsTime)
            {
                isAbs = false;
                nextAbsTime = Time.time + absRate;
            }
        }
    }

    // JSON 업데이트
    public void updateJson()
    {
        playerInfo data;
        data = new playerInfo(PlayerData.Instance.money, PlayerData.Instance.playerSkill[4], PlayerData.Instance.playerSkill[5], PlayerData.Instance.playerSkill[6]);
        if(!Directory.Exists(Application.persistentDataPath + "/Json"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Json");
        }
        string jdata = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + "/Json/save.json" , jdata);
    }
    public void updateJson(int money,int atkPoint,int hpPoint,int aspPoint)
    {
        playerInfo data;
        data = new playerInfo(money,atkPoint, hpPoint, aspPoint);
        if (!Directory.Exists(Application.persistentDataPath + "/Json"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Json");
        } 
        string jdata = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + "/Json/save.json", jdata);
        // string jdata = JsonConvert.SerializeObject(data);
        // File.WriteAllText(Application.dataPath + "/save.json", jdata);
    }


    //DB 조회
    public void selectDataBase()
    { 
        StartCoroutine(selectStart()); 
    }
    IEnumerator selectStart()
    {
        WWWForm form = new WWWForm();

        form.AddField("ID", "msh6618");
        form.AddField("PASS", "asd123");
        string url = "msh4585.cafe24.com/Login.php";
        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);

        yield return webRequest.SendWebRequest(); 
        string str = webRequest.downloadHandler.text.ToString();
        string[] requsetData = str.Split('-');
        updateJson(Convert.ToInt32(requsetData[0].Trim()), Convert.ToInt32(requsetData[1].Trim()), Convert.ToInt32(requsetData[2].Trim()), Convert.ToInt32(requsetData[3].Trim()));
         
        playerInfo data;
        //Load

        //string jdata = File.ReadAllText(Application.dataPath + "/save.json");
        string jdata = File.ReadAllText(Application.persistentDataPath + "/Json/save.json");

        data = JsonConvert.DeserializeObject<playerInfo>(jdata);

        money = data.money;
        playerSkill[4] = data.atkPoint;
        playerSkill[5] = data.hpPoint;
        playerSkill[6] = data.AspPoint;

        playerDamage += data.atkPoint * 2;
        maxHp += data.hpPoint * 200;
        currentHp += data.hpPoint * 200;
        atkSpeed += data.AspPoint * 0.5f;
    }

    //DB 업데이트
    public void updateDataBase()
    {
        StartCoroutine(updateStart());
    }
    IEnumerator updateStart()
    {
        WWWForm form = new WWWForm();
        form.AddField("MONEY", PlayerData.Instance.money);
        form.AddField("ATKPOINT", PlayerData.Instance.playerSkill[4]);
        form.AddField("HPPOINT", PlayerData.Instance.playerSkill[5]);
        form.AddField("ASPPOINT", PlayerData.Instance.playerSkill[6]);
        string url = "msh4585.cafe24.com/Update.php";
        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
    }
}
