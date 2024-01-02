using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    public List<CharacterUI> characterUISlots = new();
    void Awake()
    {
        Character.OnBattleJoined += Setup;
    }

    void Setup(Character character)
    {
        foreach (CharacterUI ui in characterUISlots)
        {
            if (ui.isOccupied)
            {
                ui.Setup(character);
                return;
            }
        }
    }
}