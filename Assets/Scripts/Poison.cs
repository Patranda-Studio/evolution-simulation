using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public float damage;
    void Start() {
        
    }
    void Update() {
        transform.GetComponent<Renderer>().material.color = new Color(0, damage*0.1f, 0, 1);
    }
}
