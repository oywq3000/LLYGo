using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotController : MonoBehaviour
{
    public Image characterImg;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterLevel;
    public TextMeshProUGUI expText;
    public Slider expValue;
    public Button deleteBtn;
    public void SetCharacterImg(Sprite sprite)
    {
        characterImg.sprite = sprite;
    }

    public void SetCharacterLevel(int exp)
    {
        //set the level and exp
        characterLevel.text = (exp / 100).ToString();
        expText.text = (exp % 100) + "/100";
        expValue.value = (exp % 100) / 100f;
    }

    public void SetCharacterName(string name)
    {
        characterName.text = name;
    }
}
