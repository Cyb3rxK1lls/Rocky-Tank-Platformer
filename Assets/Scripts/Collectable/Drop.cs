using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour
{
	public GameObject prefab;	// Prefab to be dropped

	public void Activate()
	{
		GameObject go = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
	}
}
