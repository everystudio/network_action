using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsPractice : MonoBehaviour
{
    public int SaveTest;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(PlayerPrefs.HasKey("save_test"))
            {
                Debug.Log(PlayerPrefs.GetInt("save_test", 0));
            }
            else
            {
                Debug.Log("save_testに値は保存されていません");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SaveTest = PlayerPrefs.GetInt("save_test", 0);
            SaveTest += 1;
            PlayerPrefs.SetInt("save_test", SaveTest);
            PlayerPrefs.Save();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (PlayerPrefs.HasKey("save_test"))
            {
                PlayerPrefs.DeleteKey("save_test");
            }
        }
    }
}
