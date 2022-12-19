using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateName : MonoBehaviour
{
    [SerializeField] private GameObject input;

    public void StartScene()
    {
        PlayerPrefs.SetString("Name", input.GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("Menu");
    }
}
