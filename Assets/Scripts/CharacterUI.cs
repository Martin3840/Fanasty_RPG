using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _hpBarRect;
    [SerializeField]
    private RectMask2D _hpMask;
    [SerializeField]
    private TMP_Text _hpIndicator;

    [SerializeField]
    private RectTransform _manaBarRect;
    [SerializeField]
    private RectMask2D _manaMask;
    [SerializeField]
    private TMP_Text _manaIndicator;

    private Image profilePicture;

    private float currHP;
    private float maxHP;

    private float currMana;
    private float maxMana;

    public void Setup(float newCurrHP, float newMaxHP, float newCurrMana, float newMaxMana, Image newProfilePic)
    {
        profilePicture = newProfilePic;

        currHP = newCurrHP;
        maxHP = newMaxHP;

        currMana = newCurrMana;
        maxMana = newMaxMana;

        _hpIndicator.SetText(((int)currHP).ToString() + "/" + ((int)maxHP).ToString());
        _manaIndicator.SetText(currMana.ToString() + "/" + maxMana.ToString());
    }

    public void SetHpValue(int damage)
    {
        StartCoroutine(HpDecrease(damage));
    }
    public IEnumerator HpDecrease(int damage)
    {
        var padding = _hpMask.padding;
        float tempHP = currHP;
        float onePercent = damage / 50;
        for (int i = 0; i <= 50 && currHP >= 0; i++)
        {
            currHP -= onePercent;
            padding.z = (float)(maxHP - currHP) / maxHP * _hpBarRect.rect.width * 3;
            _hpMask.padding = padding;
            _hpIndicator.SetText(((int)currHP).ToString() + "/" + ((int)maxHP).ToString());
            yield return new WaitForSeconds(0.01f);
        }

        currHP = tempHP - damage;
        if (currHP < 0)
            currHP = 0;
        padding.z = (float)(maxHP - currHP) / maxHP * _hpBarRect.rect.width * 3;
        _hpMask.padding = padding;
        _hpIndicator.SetText(((int)currHP).ToString() + "/" + ((int)maxHP).ToString());
    }
    public IEnumerator SetManaValue(int usedMana)
    {
        var padding = _manaMask.padding;

        float onePercent = currMana / 100;
        for (int i = 0; i < 100 && currHP > 0; i++)
        {
            currMana -= onePercent;
            padding.z = (float)(maxMana - currMana) / maxMana * _manaBarRect.rect.width * 3;
            _manaMask.padding = padding;
            _manaIndicator.SetText(((int)currMana).ToString() + "/" + ((int)maxMana).ToString());
            yield return new WaitForSeconds(0.01f);
        }
    }
}
