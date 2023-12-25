using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateCombat : MonoBehaviour
{
    public GameObject world;
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {

            world.SetActive(false);

            var scene = SceneManager.LoadSceneAsync("Combat",LoadSceneMode.Additive);
            scene.completed += (x) => {
                BattleSystem battleSystemObject = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
                battleSystemObject.playerObject = this.gameObject;
                battleSystemObject.enemyObject = collision.gameObject;
                battleSystemObject.StartBattle();
                battleSystemObject.world = world;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Combat"));
            };
        }
    }
}
