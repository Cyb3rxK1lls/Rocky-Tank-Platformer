using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour
{
	public GameObject destroyedMesh;	// Mesh to be used upon collision
	public GameObject droppedItem;	// Item (normally collectable) to be created when destroyed
    public float explosionForce = 10;
    public AudioClip explosionSound;

	public void destroy(Vector3 hitPoint)
	{
        Debug.Log("broken");
        GetComponent<MeshCollider>().enabled = false;
		if(droppedItem != null)
			Instantiate (droppedItem, transform.position, transform.rotation);  // Create droppedItem at this position
        GameObject broken = Instantiate(destroyedMesh, transform.position,  transform.rotation) as GameObject;
        broken.transform.localScale = transform.localScale;
        Transform children = broken.transform.GetChild(0);
        foreach (Transform child in children) {
            child.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, hitPoint, 10);
        }
        broken.GetComponent<AudioSource>().PlayOneShot(explosionSound);
        Destroy(gameObject);
	}
}
