using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour
{
    Spawner spawn;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
        	collider2D.enabled = false;
            SceneUtils.FindObjectOf<SpawnManager>().DoSpawn();
        }
    }
}
