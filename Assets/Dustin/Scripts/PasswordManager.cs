using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordManager : MonoBehaviour
{
    [Header("Pasword Components")]
    public List<string> middleNames;
    public List<string> birthYears;
    public List<string> petNames;

    [Header("References")]
    public TMP_Text[] hintDisplays; // Assign this in the inspector for clue locations

    public string CorrectPassowrd { get; private set; }

    private void Start()
    {
        GeneratePassword();
    }

    private void GeneratePassword()
    {
        string middle = middleNames[Random.Range(0, middleNames.Count)];
        string year = birthYears[Random.Range(0, birthYears.Count)];
        string pet = petNames[Random.Range(0, petNames.Count)];

        CorrectPassowrd = middle + year + pet;
        Debug.Log("Generated Password: " + CorrectPassowrd); // Just to ensure this is working easily

        hintDisplays[0].text = middle;
        hintDisplays[1].text = year;
        hintDisplays[2].text = pet;
    }
}