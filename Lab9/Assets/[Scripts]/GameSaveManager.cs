using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        // keyboard support
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    public void OnSaveButton_Pressed()
    {
        SaveGame();
    }

    public void OnLoadButton_Pressed()
    {
        LoadGame();
    }

    // Serializing Data (Encoding)
    public void SaveGame()
    {
        PlayerPrefs.SetString("PlayerPosition", JsonUtility.ToJson(playerTransform.position)); 
        PlayerPrefs.SetString("PlayerRotation", JsonUtility.ToJson(playerTransform.rotation.eulerAngles));
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    // Deserializing Data (Decoding)
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;
            playerTransform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
            playerTransform.rotation = Quaternion.Euler(JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerRotation")));
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = true;
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data reset complete");
    }
}
