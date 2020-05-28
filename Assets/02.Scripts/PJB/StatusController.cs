using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class StatusController : MonoBehaviour
{

    public TextMeshProUGUI Atk_text;
    public TextMeshProUGUI Hp_test;
    public TextMeshProUGUI Asp_test;

    ButtonController bc;
    float Atk;
    float Hp;
    float Asp;

    void Start()
    {
        
        Atk = 30;
        Hp = 1000;
        Asp = 1;
    }

    
    void Update()
    {
        Atk_text.text = (Atk + PlayerData.Instance.playerSkill[4] * 10).ToString();
        Hp_test.text = (Hp + PlayerData.Instance.playerSkill[5] * 200).ToString();
        Asp_test.text = (Asp + PlayerData.Instance.playerSkill[6] * 1).ToString();
    }



    public void Status_Atk()
    {
        Atk += 1;
        Atk_text.text = Atk.ToString();
    }
    public void Status_Hp()
    {
        Hp += 1;
        Hp_test.text = Hp.ToString();
    }
    public void Status_Asp()
    {
        Asp += 1;
        Asp_test.text = Asp.ToString();
    }
}
