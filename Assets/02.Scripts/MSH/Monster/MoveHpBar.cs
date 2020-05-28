using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveHpBar : MonoBehaviour
{
    public Slider hpBar;
    public Transform monster;
    public float maxHp = 100;
    public float currentHp = 100; 

    void Start()
    {
        hpBar.direction = Slider.Direction.RightToLeft;
    }
     
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            currentHp = currentHp - 10;
        }
        transform.SetPositionAndRotation(new Vector3(monster.transform.position.x, monster.transform.position.y, monster.transform.position.z + 0.5f), transform.rotation);
        
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / maxHp, Time.deltaTime * 5);
    }
}
