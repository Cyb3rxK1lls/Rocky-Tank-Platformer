using UnityEngine;
using System.Collections;

public class AggroBox : MonoBehaviour {

    public bool inBox = false;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("in");
            inBox = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("out");
            inBox = false;
        }
    }
}
