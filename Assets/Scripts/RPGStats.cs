using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterName", menuName = "RPGStats")] 
public class RPGStats : ScriptableObject
{
    public new string name;
    public int level;
    public Sprite sprite;
    public Image profilePic;
    public float health;
    public float attack;
    public float defense;
    public float speed;
    public float mana;
    public bool isPlayer;
}
