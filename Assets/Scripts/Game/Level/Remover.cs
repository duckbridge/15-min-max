using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;


	void OnTriggerEnter2D(Collider2D col)
	{
		PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
		if(player){
			player.OnDie();
		}

	}
}
