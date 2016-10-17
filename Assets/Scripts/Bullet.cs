using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float lifeSpan = 3f;

	void Start () {
		Destroy (gameObject, lifeSpan);
	}

    void OnCollisionEnter(Collision col) {
        if (col.transform.CompareTag("Destructible")) {
            col.gameObject.GetComponent<Destructible>().destroy(transform.position);
        }
        Destroy(gameObject);
    }
}
