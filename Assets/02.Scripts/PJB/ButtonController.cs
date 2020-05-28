using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Newtonsoft.Json;
using System.IO;
public class ButtonController : MonoBehaviour
{
    public TextMeshProUGUI text;

    int Atk_number;
    int Hp_number;
    int Asp_number;

    int Atk_money;
    int Hp_money;
    int Asp_money;

    bool updateUI;
    bool pointerDown;
    bool isReset;


    void Start()
    {
        Atk_number =0;
        Hp_number = 0;
        Asp_number = 0;

        Atk_money = 3000;
        Hp_money = 1500;
        Asp_money = 2000;

        updateUI = false;
        isReset = false; 
    }

  
    void Update()
    {

        if (Input.GetMouseButtonDown(0)&& pointerDown)
        { 
            AtkUp();
            HpUp();
            AtkSpeed();
        }
        else
        {
            pointerDown = false;
        }

        Atk_number = PlayerData.Instance.playerSkill[4];
        Hp_number = PlayerData.Instance.playerSkill[5];
        Asp_number = PlayerData.Instance.playerSkill[6];
    } 
    public void PointerDown()
    {
        pointerDown = true;
    }
    public void PointerUp()
    {
        pointerDown = false;
    } 

    public void AtkReset()
    {
        PlayerData.Instance.money += PlayerData.Instance.playerSkill[4] * Atk_money;
        Atk_number = 0;
        text.text = Atk_number.ToString();

        PlayerData.Instance.playerDamage -= PlayerData.Instance.playerSkill[4] * 2;

        PlayerData.Instance.playerSkill[4] = 0; 
        PlayerData.Instance.updateJson();
        PlayerData.Instance.updateDataBase();
    }
    public void HpReset()
    {
        PlayerData.Instance.money += PlayerData.Instance.playerSkill[5] * Hp_money;
        Hp_number = 0;
        text.text = Hp_number.ToString();

        PlayerData.Instance.maxHp -= PlayerData.Instance.playerSkill[5] * 200;
        PlayerData.Instance.currentHp -= PlayerData.Instance.playerSkill[5] * 200;

        PlayerData.Instance.playerSkill[5] = 0; 
        PlayerData.Instance.updateJson(); 
        PlayerData.Instance.updateDataBase();
    }
    public void AspReset()
    { 
        PlayerData.Instance.money += PlayerData.Instance.playerSkill[6] * Asp_money;
        Asp_number = 0;
        text.text = Asp_number.ToString();

        PlayerData.Instance.atkSpeed = 1;
        PlayerData.Instance.playerSkill[6] = 0;
        PlayerData.Instance.updateJson();
        PlayerData.Instance.updateDataBase();
    }


    public void AtkUp()
    {
        if (PlayerData.Instance.money >= Atk_money)
        {
            Atk_number += 1;
            text.text = Atk_number.ToString();
            PlayerData.Instance.playerSkill[4] += 1;
            PlayerData.Instance.money -= Atk_money;
            PlayerData.Instance.playerDamage += 2;
            PlayerData.Instance.updateJson();
            PlayerData.Instance.updateDataBase();
        }
    }
    public void HpUp()
    {
        if (PlayerData.Instance.money >= Hp_money)
        {
            Hp_number += 1;
            text.text = Hp_number.ToString();
            PlayerData.Instance.playerSkill[5] += 1;
            PlayerData.Instance.money -= Hp_money;
            PlayerData.Instance.maxHp += 200;
            PlayerData.Instance.currentHp += 200;

            PlayerData.Instance.updateJson();
            PlayerData.Instance.updateDataBase();
        }
    }
    public void AtkSpeed()
    {
        if (PlayerData.Instance.money >= Asp_money)
        {
            Asp_number += 1;
            text.text = Asp_number.ToString();
            PlayerData.Instance.playerSkill[6] += 1;
            PlayerData.Instance.money -= Asp_money;
            PlayerData.Instance.atkSpeed += 0.5f;
            PlayerData.Instance.updateJson();
            PlayerData.Instance.updateDataBase();
        }
    }

    //Json 으로 저장
   

}
