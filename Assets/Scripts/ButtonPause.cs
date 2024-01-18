using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Time.timeScale == 0) {
                Time.timeScale = 1f;
            }
            else {
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Camera.main.GetComponent<Global>().Player.GetComponent<PlayerController>().walkingSpeed -= 10;
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            Camera.main.GetComponent<Global>().Player.GetComponent<PlayerController>().walkingSpeed += 10;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            Time.timeScale = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            Time.timeScale = 2f;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Time.timeScale = 4f;
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            Time.timeScale = 8f;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
