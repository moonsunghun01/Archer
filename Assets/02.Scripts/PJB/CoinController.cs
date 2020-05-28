using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using TMPro;
public class CoinController : MonoBehaviour
{
    public TextMeshProUGUI coinText;
     
    void Start()
    { 
    } 
    void Update()
    {
        coinText.text = (PlayerData.Instance.money).ToString();
    } 
}
