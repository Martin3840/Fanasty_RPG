using UnityEngine;

[CreateAssetMenu(fileName = "NewFireBallSkill", menuName = "Skill/Fireball")]
public class FireBall : Skill
{
    public GameObject crosshairPrefab;
    bool Crit;
    GameObject target;
    public override void Cast(Character caster)
    {
        Debug.Log("Casted!");
    }
}
