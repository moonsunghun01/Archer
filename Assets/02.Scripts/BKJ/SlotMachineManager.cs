using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotSkillObject;

    public GameObject joystickUI;

    public GameObject slotPanel;
    public GameObject choicePanel;

    public Button[] slot;

    public Sprite[] skillSprite;

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> slotSprite = new List<Image>();
    }
    public DisplayItemSlot[] displayItemSlots;

    public Image displayResultImage;

    public List<int> startList = new List<int>();
    public List<int> resultIndexList = new List<int>();
    int skillCount = 3;
    int[] answer = { 2, 3, 1 };

    private void OnEnable()
    {
        MoveJoystick.Instance.Drop();

        joystickUI.SetActive(false);

        for (int i = 0; i < skillCount * slot.Length; i++)
        {
            startList.Add(i);
        }

        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < skillCount; j++)
            {
                slot[i].interactable = false;

                int randomIndex = Random.Range(0, startList.Count);
                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    resultIndexList.Add(startList[randomIndex]);
                }
                displayItemSlots[i].slotSprite[j].sprite = skillSprite[startList[randomIndex]];

                if (j == 0)
                {
                    displayItemSlots[i].slotSprite[skillCount].sprite = skillSprite[startList[randomIndex]];
                }
                startList.RemoveAt(randomIndex);
            }
        }

        for (int i = 0; i < slot.Length; i++)
        {
            StartCoroutine(StartSlot(i));
        }
    }

    IEnumerator StartSlot(int slotIndex)
    {
        for (int i = 0; i < (skillCount * (6 + slotIndex * 4) + answer[slotIndex]) * 2; i++)
        {
            slotSkillObject[slotIndex].transform.localPosition -= new Vector3(0, 50f, 0);
            if (slotSkillObject[slotIndex].transform.localPosition.y < 50f)
            {
                slotSkillObject[slotIndex].transform.localPosition += new Vector3(0, 300f, 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < skillCount; i++)
        {
            slot[i].interactable = true;
        }
    }

    private void OnDisable()
    {
        startList = new List<int>();
        resultIndexList = new List<int>();
        displayResultImage.sprite = null;

        slotPanel.SetActive(true);
        choicePanel.SetActive(false);

        joystickUI.SetActive(true);
    }

    public void ClickBtn(int index)
    {
        slotPanel.SetActive(false);
        choicePanel.SetActive(true);
        displayResultImage.sprite = skillSprite[resultIndexList[index]];

        for (int i = 0; i < 3; i++)
        {
            slotSkillObject[i].transform.localPosition = new Vector3(0, 300, 0);
        }
        if (displayResultImage.sprite.name == "AtkSpeedUp")
        {
            PlayerData.Instance.playerSkill[6] += 1;
            Debug.Log("스피드업 증가");
        }
        if (displayResultImage.sprite.name == "AtkUp")
        {
            PlayerData.Instance.playerSkill[4] += 1;
            Debug.Log("공격력업 증가");
        }
        if (displayResultImage.sprite.name == "HpUp")
        {
            PlayerData.Instance.playerSkill[5] += 1;
            Debug.Log("HP업 증가");
        }

        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}