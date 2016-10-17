using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
	public int val = 1;
    private Vector3 initialPos;
    private Vector3 initialRot;
    public float floatFactor = 0.2f;

    public AudioClip collectSound;
    public float collectVolume = 0.6f;

    void Start() {
        initialPos = transform.position;
    }

    void Update() {
        transform.position = new Vector3(initialPos.x, initialPos.y + Mathf.Sin(Time.time) * floatFactor, initialPos.z);
    }

	public void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			//Score.Add (val);
			Destroy (gameObject);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(collectSound, collectVolume);
        }
	}
}
