using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    public GameObject roulettePlate;
    public GameObject roulettePanel;
    public GameObject resultImage;

    public Transform needle;

    public GameObject joystickUI;

    public Sprite[] skillSprite;
    public Image[] displayItemSlot;

    List<int> startList = new List<int>();
    List<int> resultIndexList = new List<int>();
    int itemCount = 6;

    void OnEnable()
    {
        MoveJoystick.Instance.Drop();

        joystickUI.SetActive(false);

        for (int i = 0; i < itemCount; i++)
        {
            startList.Add(i);
        }
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, startList.Count);
            resultIndexList.Add(startList[randomIndex]);
            displayItemSlot[i].sprite = skillSprite[startList[randomIndex]];
            startList.RemoveAt(randomIndex);
        }
        //displayItemSlot[6].sprite

        StartCoroutine(StartRoulette());
    }

    IEnumerator StartRoulette()
    {
        yield return new WaitForSeconds(1.0f);

        float randomSpeed = Random.Range(1.0f, 5.0f);
        float rotateSpeed = 100.0f * randomSpeed;

        while(true)
        {
            yield return null;
            if (rotateSpeed <= 0.01f) break;

            rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 2.0f);
            roulettePlate.transform.Rotate(0, 0, rotateSpeed);
        }
        yield return new WaitForSeconds(0.5f);

        Result();
    }

    void Result()
    {
        int closetIndex = -1;
        float closetDis = 500f;
        float currentDis = 0f;

        for (int i = 0; i < itemCount; i++)
        {
            currentDis = Vector2.Distance(displayItemSlot[i].transform.position, needle.position);
            if (closetDis > currentDis)
            {
                closetDis = currentDis;
                closetIndex = i;
            }
        }

        if (closetIndex == -1)
        {
            Debug.Log("Something is wrong!");
        }
        displayItemSlot[itemCount].sprite = displayItemSlot[closetIndex].sprite;

        if (displayItemSlot[itemCount].sprite.name == "멀티샷")
        {
            PlayerData.Instance.playerSkill[0] += 1;
            Debug.Log("멀티샷 획득");
        }
        if (displayItemSlot[itemCount].sprite.name == "전방화살")
        {
            PlayerData.Instance.playerSkill[1] += 1;
            Debug.Log("전방화살 획득");
        }
        if (displayItemSlot[itemCount].sprite.name == "측면화살")
        {
            PlayerData.Instance.playerSkill[2] += 1;
            Debug.Log("측면화살 획득");
        }
        if (displayItemSlot[itemCount].sprite.name == "사선화살")
        {
            PlayerData.Instance.playerSkill[3] += 1;
            Debug.Log("사선화살 획득");
        }


        resultImage.SetActive(true);
        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        resultImage.SetActive(false);
        startList = new List<int>();
        resultIndexList = new List<int>();
        displayItemSlot[itemCount].sprite = null;

        joystickUI.SetActive(true);
    }
}
