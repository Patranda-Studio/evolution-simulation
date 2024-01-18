using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class Global : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Entitys;
    public GameObject prefab_Entity;
    public GameObject[] Resources;
    public GameObject prefab_Resource;
    public GameObject CharacteristicPanel;
    public Text max_health_Text;
    public Text health_Text;
    public Text speed_Text;
    public Text damage_Text;
    public Text coolDownText;
    public void Create_Entity(GameObject Parent){
        bool f = false;
        for (int i = 0; i < Entitys.Length; i++){
            if (Entitys[i] == null){
                Entitys[i] = Instantiate(prefab_Entity, new Vector3(Parent.transform.position.x + Range(-2f, 2f), 0.25f, Parent.transform.position.z + Range(-2f, 2f)), Quaternion.identity);
                Entitys[i].GetComponent<Entity>().id = i;
                Entitys[i].GetComponent<Entity>().speed = Parent.GetComponent<Entity>().speed + Range(-1f, 1f);
                Entitys[i].GetComponent<Entity>().max_health = Parent.GetComponent<Entity>().max_health + Range(-Parent.GetComponent<Entity>().max_health, 10f);
                Entitys[i].GetComponent<Entity>().health = Parent.GetComponent<Entity>().health*0.5f;
                Entitys[i].GetComponent<Entity>().damage = Parent.GetComponent<Entity>().damage + Range(-1f, 1f);
                f = true;
            }
        }
        if ((f == false) && (Entitys.Length <= 100)){
            Array.Resize(ref Entitys, Entitys.Length + 1);
            Entitys[Entitys.Length - 1] = Instantiate(prefab_Entity, Parent.transform.position, Quaternion.identity);
            Entitys[Entitys.Length - 1].GetComponent<Entity>().id = Entitys.Length - 1;
            do {
                Entitys[Entitys.Length - 1].GetComponent<Entity>().speed = Parent.GetComponent<Entity>().speed + Range(-1f, 1f);
            } while (Entitys[Entitys.Length - 1].GetComponent<Entity>().speed <= 0);
            Entitys[Entitys.Length - 1].GetComponent<Entity>().max_health = Parent.GetComponent<Entity>().max_health + Range(-10f, 10f);
            Entitys[Entitys.Length - 1].GetComponent<Entity>().health = Parent.GetComponent<Entity>().health * 0.5f;
            Entitys[Entitys.Length - 1].GetComponent<Entity>().damage = Parent.GetComponent<Entity>().damage + Range(-1f, 1f);

        }
    }
    public void Create_Resource(){
        bool f = false;
        for (int i = 0; i < Resources.Length; i++){
            if (Resources[i] == null){
                Resources[i] = Instantiate(prefab_Resource,transform.position, Quaternion.identity);
                Resources[i].GetComponent<Resource>().id = i;
                f = true;
            }
        }
        if (f == false){
            Array.Resize(ref Resources, Resources.Length + 1);
            Resources[Resources.Length - 1] = Instantiate(prefab_Resource, transform.position, Quaternion.identity);
            Resources[Resources.Length - 1].GetComponent<Resource>().id = Resources.Length - 1;
        }
    }
    void Start()
    {
        Entitys = new GameObject[0];
        for (int i = 0; i < 5; i++){
            GameObject Progenitor = prefab_Entity;
            Progenitor.transform.position = new Vector3(Range(-25f, 25f), 0.25f, Range(-25f, 25f));
            Progenitor.GetComponent<Entity>().speed = Range(1f, 10f);
            Progenitor.GetComponent<Entity>().max_health = Range(10f, 100f);
            Progenitor.GetComponent<Entity>().health = Progenitor.GetComponent<Entity>().max_health;
            Progenitor.GetComponent<Entity>().damage = Range(1f, 10f);
            Create_Entity(Progenitor);
        }
        Resources = new GameObject[0];
        for (int i = 0; i < 100; i++){
            Create_Resource();
        }
    }

    float coolDown = 25;
    void Update()
    {
        if (Entitys.Length >= 200){
            Application.Quit();
        }
        coolDown -= Time.deltaTime;
        coolDownText.text = coolDown.ToString();
        if (coolDown <= 0)
        {
            Create_Resource();
            coolDown = 25;
        }
        for (int i = 0; i < Resources.Length; i++)
        {
            if ((Resources[i] != null))
            {
                if ((Resources[i].transform.position.y <= -10) || (Resources[i].transform.position.x > 25) || (Resources[i].transform.position.x < -25) || (Resources[i].transform.position.z > 25) || (Resources[i].transform.position.z < -25))
                {
                    Destroy(Resources[i]);
                    Resources[i] = null;
                    Create_Resource();
                }
            }
        }
        for (int i = 0; i < Entitys.Length; i++)
        {
            if ((Entitys[i] != null))
            {
                if ((Entitys[i].transform.position.y <= -10) || (Entitys[i].transform.position.x > 25) || (Entitys[i].transform.position.x < -25) || (Entitys[i].transform.position.z > 25) || (Entitys[i].transform.position.z < -25))
                {
                    Destroy(Entitys[i]);
                    Entitys[i] = null;
                }
            }
        }
    }
}
