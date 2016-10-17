using UnityEngine;
using System.Collections;

[RequireComponent( typeof (Rigidbody))]
public class TankController : MonoBehaviour
{
    public float maxAngularVel = 5; // Max angular velocity (prevents rotating too quickly)
    public float rotSpeed = 1;      // Speed of rotation
    public float moveAccel = 1;     // Acceleration of movement
    public float maxSpeed = 5;      // Max speed of movement
    public float friction = 2f;     // Ground friction
    public float jumpForce = 10;    // Force of jump
    public float projSpeed = 20;    // Speed of the projectile when fired
    public float fireRate = 1;      // Fire rate in rounds/second
    public GameObject projectile;   // Object to be fired out of the gun
    public GameObject projectile2;   // Object to be fired out of the gun

    private Rigidbody rb;           // Rigidbody attached to this
    private float distToGround;     // Distance from collider to the ground
    private Transform gun;          // Transform of the gun attachted to this
    private float lastShot = float.MinValue;
	private bool grounded;

    public float minimumY = -10f;   // Respawn if you fall below this level

    private float time = 0f;
    private int seconds = 0;

    private float yRot = 0;

    private Vector3 initialPos;     // Initial position to respawn from
    private Quaternion initialRot;  // Initial rotation to respawn from

    private GameObject groundCheck;
    private Vector3 groundCheckExtents;


    void Start()
    {
		rb = GetComponent<Rigidbody>();
		gun = transform.FindChild ("Tank_Model").FindChild ("Tank_Gun");
        groundCheck = transform.FindChild("GroundCheck").gameObject;
        groundCheckExtents = groundCheck.GetComponent<BoxCollider>().bounds.extents;
        yRot = transform.eulerAngles.y;
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Rotate();
        }

        if(true)
        {
            time++;
            if (time >= 55)
            {
                seconds++;
                time = 0;
                Score.Add(seconds);
                seconds = 0;
            }
        }
        

        if(Input.GetAxisRaw("Vertical") != 0)
        {
            Move();
        }

        if(Input.GetAxisRaw("Jump") != 0)
        {
			if(grounded)
                Jump();
        }

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            Fire();
        }
        if (Input.GetAxisRaw("Fire2") != 0)
        {
            Fire2();
        }

        if (transform.position.y < minimumY) {
            // Respawn
            Respawn();
        }
        Debug.DrawRay(transform.position, new Vector3(0, -1f, 0), Color.red);
        GroundTest();
        Friction();
    }

    private void Rotate()
    {
        yRot = (rotSpeed * Input.GetAxis("Horizontal")) + yRot;
        rb.MoveRotation(Quaternion.Euler(0, yRot, 0));
    }

    private void Friction() {
        if (grounded) {
            rb.velocity = new Vector3(rb.velocity.x / friction, rb.velocity.y, rb.velocity.z / friction);
        }
    }

    private void Move()
    {
        // Check if local velocity is faster than max velocity
        if (rb.velocity.magnitude < maxSpeed){
            Vector3 moveForce = new Vector3(Mathf.Clamp(moveAccel * Input.GetAxis("Vertical"), -maxSpeed, maxSpeed), 0, 0);
            rb.AddRelativeForce(moveForce, ForceMode.Impulse);
		}
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce * Input.GetAxis("Jump"), rb.velocity.z);
    }

    private void Fire()
    {
        if (Time.time - lastShot < fireRate)
            return;

		Rigidbody rb = ((GameObject)Instantiate(projectile, gun.position, transform.rotation)).GetComponent<Rigidbody>();

        if(rb != null)
			rb.AddForce(transform.TransformDirection(new Vector3(projSpeed, 0, 0)), ForceMode.Impulse);

        lastShot = Time.time;
        gun.GetComponent<AudioSource>().Play();
    }
    private void Fire2()
    {
        if (Time.time - lastShot < fireRate)
            return;

        Rigidbody rb = ((GameObject)Instantiate(projectile2, gun.position, transform.rotation)).GetComponent<Rigidbody>();

        if (rb != null)
            rb.AddForce(transform.TransformDirection(new Vector3(projSpeed, 0, 0)), ForceMode.Impulse);

        lastShot = Time.time;
        gun.GetComponent<AudioSource>().Play();
    }

    private void Respawn()
    {
        // Reset position
        transform.position = initialPos;
        // Reset rotation
        transform.rotation = initialRot;
        yRot = transform.eulerAngles.y;
        // Clear velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void GroundTest()
    {
        Collider[] overlaps = Physics.OverlapBox(groundCheck.transform.position, groundCheckExtents, groundCheck.transform.rotation);
        grounded = false;
        for (int i = 0; i < overlaps.Length; i++) {
            if (overlaps[i].CompareTag("Ground")) {
                grounded = true;
            }
        }
    }
}
