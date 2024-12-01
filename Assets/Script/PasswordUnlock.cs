using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordUnlock : MonoBehaviour {
    public int passwordLength = 8;
    public TMP_InputField passwordInputField;
    public TMP_Text password;
    public TMP_Text feedbackText;
    public GameObject door;
    public GameObject miniGameUI; 
    public GameObject player;

    private string correctPassword;
    private const string characters = "1234567890";

    void Start() {
        miniGameUI.SetActive(false);
    }

    void OnEnable() {
        GenerateRandomPassword();
    }

    void GenerateRandomPassword() {
        correctPassword = "";
        for (int i = 0; i < passwordLength; i++) {
            int index = Random.Range(0, characters.Length);
            correctPassword += characters[index];
        }
        Debug.Log("Password: " + correctPassword);
        password.text = correctPassword;
    }

    public void CheckPassword() {
        if (passwordInputField.text == correctPassword) {
            feedbackText.text = "Password is correct, open the door";
            UnlockDoor();
        } else {
            feedbackText.text = "Wrong password, please try again!";
            passwordInputField.text = "";
        }
    }

    private void UnlockDoor() {
        passwordInputField.text = "";
        password.text = "";
        feedbackText.text = "";
        if (door != null) {
            door.SetActive(false);
        }
        miniGameUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            Debug.Log("Player is near the door, starting mini-game.");
            StartMiniGame();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            Debug.Log("Player left the door, stopping mini-game.");
            StopMiniGame();
        }
    }

    private void StartMiniGame() {
        miniGameUI.SetActive(true);
        GenerateRandomPassword();
    }

    private void StopMiniGame() {
        miniGameUI.SetActive(false);
    }
}
