using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int id;
    public float score;
    void Start() {
        score = Random.Range(0f, 10f);
        transform.position = new Vector3(Random.Range(-25f, 25f), 0.25f, Random.Range(-25f, 25f));
        transform.GetComponent<Renderer>().material.color = new Color(0, score*0.1f, 0, 1);
    }

    void Update() {
        
    }
}
