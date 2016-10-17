using UnityEngine;
using System.Collections;

public class Fragment : MonoBehaviour {
    private float timeAlive = 0;

    void Update() {
        timeAlive += Time.deltaTime;
    }

	void OnCollisionEnter() {
        if(timeAlive > 0.1f) {
            Destroy(gameObject);
        }
    }
}
