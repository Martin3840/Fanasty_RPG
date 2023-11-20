using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateCombat : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {

            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(collision.gameObject);

            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);

            var scene = SceneManager.LoadSceneAsync("Combat");
            scene.completed += (x) => {
                BattleSystem battleSystemObject = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
                battleSystemObject.playerObject = this.gameObject;
                battleSystemObject.enemyObject = collision.gameObject;
                battleSystemObject.StartBattle();
            };
        }
    }
}
