using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyDisplay : MonoBehaviour
{
    public RPGStats enemy;

    public GameObject spriteImage;
    // Start is called before the first frame update
    void Start()
    {
        spriteImage.GetComponent<SpriteRenderer>().sprite = enemy.sprite;
    }
}
