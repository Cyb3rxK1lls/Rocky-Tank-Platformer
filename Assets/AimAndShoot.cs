using UnityEngine;
using System.Collections;

public class AimAndShoot : MonoBehaviour {

    public AggroBox aggroBox;
    public GameObject projectile;
    public float rotateSpeed = 4f;
    public float projectileForce;
    public float shootCooldown = 1f;
    public float distanceCanShoot = 20f;
    public LayerMask shootAtLayers;

    private float shootTimer;
    private float inFrontMultiplier = 1.5f;
    private Transform player;
    private Quaternion lookRotation;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void FixedUpdate () {
        if(aggroBox.inBox) {
            RotateToPlayer();
        }
        if(shootTimer > 0) {
            shootTimer -= Time.fixedDeltaTime;
        }
	}

    void RotateToPlayer() {
        lookRotation = Quaternion.LookRotation((player.position+Vector3.up) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        Debug.DrawRay(transform.position, transform.forward* distanceCanShoot, Color.red);
        if(Physics.Raycast(new Ray(transform.position, transform.forward), distanceCanShoot, shootAtLayers)) {
            if (shootTimer <= 0) {
                Shoot();
                shootTimer = shootCooldown;
            }
        }
    }

    void Shoot() {
        GameObject proj = Instantiate(projectile);
        proj.transform.position = transform.position + transform.forward * inFrontMultiplier;
        proj.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);
    }
}
