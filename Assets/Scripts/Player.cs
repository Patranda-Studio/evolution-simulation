using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    public Text Health_Text;
    float min_Distance_Resource;
    public GameObject near_Resource;
    void Start() {
        health = 100;
    }
    void Update() {
        Health_Text.text = health.ToString();
        health -= Time.deltaTime * transform.GetComponent<PlayerController>().walkingSpeed;
        if (health <= 0) {
            Application.Quit();
        }
        min_Distance_Resource = float.PositiveInfinity;
        for (int i = 0; i < Camera.main.GetComponent<Global>().Resources.Length; i++){
            if (Vector3.Distance(Camera.main.GetComponent<Global>().Resources[i].transform.position, transform.position) <= min_Distance_Resource){
                min_Distance_Resource = Vector3.Distance(Camera.main.GetComponent<Global>().Resources[i].transform.position, transform.position);
                near_Resource = Camera.main.GetComponent<Global>().Resources[i];
            }
        }
        if ((near_Resource.transform.position - transform.position).sqrMagnitude <= 2f) {
            health += near_Resource.GetComponent<Resource>().score;
            Destroy(Camera.main.GetComponent<Global>().Resources[near_Resource.GetComponent<Resource>().id]);
            Camera.main.GetComponent<Global>().Resources[near_Resource.GetComponent<Resource>().id] = null;
            Camera.main.GetComponent<Global>().Create_Resource();
        }
        for (int i = 0; i < Camera.main.GetComponent<Global>().Entitys.Length; i++){
            if (Camera.main.GetComponent<Global>().Entitys[i] != null){
                if (Vector3.Distance(Camera.main.GetComponent<Global>().Entitys[i].transform.position, transform.position) < 3f){
                    if (Camera.main.GetComponent<Global>().Entitys[i].GetComponent<Entity>().health < health){
                        health += Camera.main.GetComponent<Global>().Entitys[i].GetComponent<Entity>().health;
                        Destroy(Camera.main.GetComponent<Global>().Entitys[i]);
                    }
                }
            }
        }
    }
}
