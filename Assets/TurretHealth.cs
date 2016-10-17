using UnityEngine;
using System.Collections;

public class TurretHealth : MonoBehaviour {

    public int turretHealth = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Rock Physics")) {
            turretHealth -= 1;
            if(turretHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
