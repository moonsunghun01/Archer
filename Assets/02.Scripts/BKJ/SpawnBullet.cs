using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{

    AudioSource arrowAudio;//
    public Transform[] spawnPoint;

    public void Spawn()
    {
        arrowAudio = GetComponent<AudioSource>();//

        if (PlayerData.Instance.playerSkill[1] == 0)
        {
            AddShot(0);

            if (PlayerData.Instance.playerSkill[0] != 0)
            {
                StartCoroutine(MultiShot(0));
            }
        }
        else if(PlayerData.Instance.playerSkill[1] != 0)
        {
            AddShot(1);
            AddShot(2);

            if (PlayerData.Instance.playerSkill[0] != 0)
            {
                StartCoroutine(MultiShot(1));
                StartCoroutine(MultiShot(2));
            }
        }

        if(PlayerData.Instance.playerSkill[2] != 0)
        {
            AddShot(3);
            AddShot(4);

            if (PlayerData.Instance.playerSkill[0] != 0)
            {
                StartCoroutine(MultiShot(3));
                StartCoroutine(MultiShot(4));
            }
        }

        if (PlayerData.Instance.playerSkill[3] != 0)
        {
            AddShot(5);
            AddShot(6);

            if (PlayerData.Instance.playerSkill[0] != 0)
            {
                StartCoroutine(MultiShot(5));
                StartCoroutine(MultiShot(6));
            }
        }
        arrowAudio.Play();//
    }
    
    public void AddShot(int num)
    {
        var _bullet = ObjectPool.instance.GetBullet();

        if (_bullet != null)
        {
            _bullet.transform.position = spawnPoint[num].position;
            _bullet.transform.rotation = spawnPoint[num].rotation;
            _bullet.SetActive(true);
        }
    }

    IEnumerator MultiShot(int num)
    {
        yield return new WaitForSeconds(0.1f);

        var _bullet = ObjectPool.instance.GetBullet();

        if (_bullet != null)
        {
            _bullet.transform.position = spawnPoint[num].position;
            _bullet.transform.rotation = spawnPoint[num].rotation;
            _bullet.SetActive(true);
        }
    }
}
