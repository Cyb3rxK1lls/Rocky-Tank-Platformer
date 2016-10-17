using UnityEngine;
using System.Collections;

public class TurretMove : MonoBehaviour {

    public Transform[] moveToSpots;
    public float speed = 1f;

    private int count = 0;
    private float timeAtTurn = 0;
    private Vector3[] moveToVectors;

	void Start () {
        moveToVectors = new Vector3[moveToSpots.Length+1];
        moveToVectors[0] = transform.position;
        for(int i = 0; i < moveToSpots.Length; i++) {
            moveToVectors[i + 1] = moveToSpots[i].position;
        }
	}
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, moveToVectors[count], (Time.time - timeAtTurn) * speed);
        if((transform.position - moveToVectors[count]).magnitude < 0.1f) {
            count++;
            timeAtTurn = Time.time;
        }
        if (count >= moveToVectors.Length) {
            count = 0;
        }
    }
}
