using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public static Character caster;

    public SkillButton attack;
    public SkillButton skill1;
    public SkillButton skill2;
    public SkillButton skill3;
    public SkillButton ultimate;

    void Awake()
    {
        SkillButton.OnSkillButtonClicked += CastSkill;
        Character.OnTurnStart += Setup;
        Character.OnTurnEnded += Hide;
    }
    void OnDestroy()
    {
        SkillButton.OnSkillButtonClicked -= CastSkill;
        Character.OnTurnStart -= Setup;
        Character.OnTurnEnded -= Hide;
    }
    void Setup(Character character)
    {
        caster = character;
        attack.Setup(character.attack.icon);
        skill1.Setup(character.skill1.icon);
        skill2.Setup(character.skill2.icon);
        skill3.Setup(character.skill3.icon);
        ultimate.Setup(character.ultimate.icon);

        Debug.Log("aosidaouefhouesf seufhso efhosue fhoushe f");
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    void Hide(Character character)
    {
        if (character = caster)
            caster = null;
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    public void CastSkill(SkillType type)
    {
        switch (type)
        {
            case SkillType.Skill1:
                caster.skill1.Cast(caster);
                break;
            case SkillType.Skill2:
                caster.skill2.Cast(caster);
                break;
            case SkillType.Skill3:
                caster.skill3.Cast(caster);
                break;
            case SkillType.Ultimate:
                caster.ultimate.Cast(caster);
                break;
            case SkillType.Attack:
                caster.attack.Cast(caster);
                break;
            default:
                Debug.Log("Skill Cast: No Skill Type Matched");
                break;
        }
    }

    public void GetSkillDescription(SkillType type)
    {
        switch (type)
        {
            case SkillType.Skill1:
                Debug.Log(caster.skill1.description);
                break;
            case SkillType.Skill2:
                Debug.Log(caster.skill2.description);
                break;
            case SkillType.Skill3:
                Debug.Log(caster.skill3.description);
                break;
            case SkillType.Ultimate:
                Debug.Log(caster.ultimate.description);
                break;
            case SkillType.Attack:
                Debug.Log(caster.attack.description);
                break;
            default:
                Debug.Log("Skill Description: No Skill Type Matched");
                break;
        }
    }
}
