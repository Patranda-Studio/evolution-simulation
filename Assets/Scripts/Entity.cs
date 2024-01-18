using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Entity : MonoBehaviour {
    public int id;
    public float max_health;
    public float health;
    public float speed;
    public float damage;
    public float visibility;
    float min_Distance_Resource;
    float min_Distance_Entity;
    public GameObject near_Resource;
    public GameObject near_Entity;
    GameObject objective;
    void Start() {
        
    }

    void Update() {
        min_Distance_Resource = float.PositiveInfinity;
        for (int i = 0; i < Camera.main.GetComponent<Global>().Resources.Length; i++)
        {
            if ((Camera.main.GetComponent<Global>().Resources[i] != null) && (Vector3.Distance(Camera.main.GetComponent<Global>().Resources[i].transform.position, transform.position) <= min_Distance_Resource) && (Vector3.Distance(Camera.main.GetComponent<Global>().Resources[i].transform.position, transform.position) <= visibility))
            {
                min_Distance_Resource = Vector3.Distance(Camera.main.GetComponent<Global>().Resources[i].transform.position, transform.position);
                near_Resource = Camera.main.GetComponent<Global>().Resources[i];
            }
        }

        min_Distance_Entity = float.PositiveInfinity;
        for (int i = 0; i < Camera.main.GetComponent<Global>().Entitys.Length; i++)
        {
            if ((Camera.main.GetComponent<Global>().Entitys[i] != null) && (Vector3.Distance(Camera.main.GetComponent<Global>().Entitys[i].transform.position, transform.position) <= min_Distance_Entity) && (Vector3.Distance(Camera.main.GetComponent<Global>().Entitys[i].transform.position, transform.position) <= visibility))
            {
                if (Camera.main.GetComponent<Global>().Entitys[i].GetComponent<Entity>().damage < damage)
                {
                    min_Distance_Entity = Vector3.Distance(Camera.main.GetComponent<Global>().Entitys[i].transform.position, transform.position);
                    near_Entity = Camera.main.GetComponent<Global>().Entitys[i];
                }
            }
        }
        if (min_Distance_Resource < min_Distance_Entity)
        {
            transform.Translate(Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(near_Resource.transform.position), 8f * Time.deltaTime);
            transform.position += (near_Resource.transform.position - transform.position).normalized * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        } else
        {
            transform.Translate(-1 * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(near_Entity.transform.position), 8f * Time.deltaTime);
            transform.position += (near_Entity.transform.position + transform.position).normalized * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
        if ((near_Resource.transform.position - transform.position).sqrMagnitude <= 0.75f) {
            health += near_Resource.GetComponent<Resource>().score;
            Destroy(Camera.main.GetComponent<Global>().Resources[near_Resource.GetComponent<Resource>().id]);
            Camera.main.GetComponent<Global>().Resources[near_Resource.GetComponent<Resource>().id] = null;
            //Camera.main.GetComponent<Global>().Create_Resource();
        }
        for (int i = 0; i < Camera.main.GetComponent<Global>().Entitys.Length; i++){
            if ((Camera.main.GetComponent<Global>().Entitys[i] != null) && (i != id)){
                if (Vector3.Distance(Camera.main.GetComponent<Global>().Entitys[i].transform.position, transform.position) < 3f){
                    if (Camera.main.GetComponent<Global>().Entitys[i].GetComponent<Entity>().health < health){
                        health += Camera.main.GetComponent<Global>().Entitys[i].GetComponent<Entity>().health;
                        Destroy(Camera.main.GetComponent<Global>().Entitys[i]);
                    }
                    else{
                        Camera.main.GetComponent<Global>().Entitys[i].GetComponent<Entity>().health += health;
                        Destroy(Camera.main.GetComponent<Global>().Entitys[id]);
                    }
                }
            }
        }
        health -= Time.deltaTime * speed;
        if ((health <= 0) || (damage <= 0) || (health >= 150) || (speed <= 0))
        {
            Destroy(Camera.main.GetComponent<Global>().Entitys[id]);
        }
        if (health >= max_health){
            Camera.main.GetComponent<Global>().Create_Entity(Camera.main.GetComponent<Global>().Entitys[id]);
            health *= 0.5f;
        }
        Camera.main.GetComponent<Global>().Entitys[id].GetComponent<Renderer>().material.color = new Color(damage*0.1f, max_health*0.01f, speed*0.1f, health*0.01f);
    }
    void OnMouseEnter(){
        Camera.main.GetComponent<Global>().CharacteristicPanel.SetActive(true);
        Camera.main.GetComponent<Global>().max_health_Text.text = max_health.ToString();
        Camera.main.GetComponent<Global>().health_Text.text = health.ToString();
        Camera.main.GetComponent<Global>().speed_Text.text = speed.ToString();
        Camera.main.GetComponent<Global>().damage_Text.text = damage.ToString();
    }
    void OnMouseExit(){
        Camera.main.GetComponent<Global>().CharacteristicPanel.SetActive(false);
    }
}
