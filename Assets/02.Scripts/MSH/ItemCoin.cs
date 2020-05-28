using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public GameObject player;

    private bool isCollision;

    public float moveTime;
    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(getCoin());
    }
    
    IEnumerator getCoin()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            while(!isCollision)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.2f);
                yield return null;
            }
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player")))
        {
            PlayerData.Instance.inGameMoney += 500;
            isCollision = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
