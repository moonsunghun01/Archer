using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource buttonAudio;
    bool pointerDown;

    int Atk_number;
    int Hp_number;
    int Asp_number;

    void Start()
    {
        buttonAudio = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            buttonAudio.Play();
        

        }
        Atk_number = PlayerData.Instance.playerSkill[4];
        Hp_number = PlayerData.Instance.playerSkill[5];
        Asp_number = PlayerData.Instance.playerSkill[6];

        //else
        //{
        //    pointerDown = false;
        //}

    }
}
