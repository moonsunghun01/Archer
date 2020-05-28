using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PowerController : MonoBehaviour
{ 
    public TextMeshProUGUI text;

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
        text.text = (Atk + PlayerData.Instance.playerSkill[4] * 10 + Hp + PlayerData.Instance.playerSkill[5] * 100 + Asp + PlayerData.Instance.playerSkill[6]).ToString();
    }

}
