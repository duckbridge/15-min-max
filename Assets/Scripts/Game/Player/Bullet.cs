using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public Explosion explosion;		// Prefab of explosion effect.
	public float speed = 1000f;
	public float destroyTimeout = 1f;
	
	private bool isActive = true;
	void Start () 
	{
		Destroy(gameObject, destroyTimeout);
	}

	public void FixedUpdate() {
		if(isActive)
			this.transform.position = new Vector3(this.transform.position.x + speed, this.transform.position.y);
	}

	void OnExplode() {
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col)  {
		OnCollide(col);
	}

	private void OnCollide(Collider2D col) {
		ShootEnemy shootEnemy = col.gameObject.GetComponent<ShootEnemy>();
		Dude dude = col.gameObject.GetComponent<Dude>();
		
		if(dude) dude.OnShot();

		if(shootEnemy) {
			isActive = false;
			
			shootEnemy.OnHit();
			
			OnExplode();
			
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		OnCollide(col);
	}
}
