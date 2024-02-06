using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using UnityEngine.UI;

[Serializable]
public class dataToSave
{
    public string userName;
    public int totalCoins;
    public int crrLevel;
    public int highScore;//and many more

}
public class DataSaver : MonoBehaviour
{
    public Text dataSave;
    public dataToSave dts;
    public string userId;
    DatabaseReference dbRef;

    private void Awake()
    {
       
    }

    public void SaveDataFn()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        string json = JsonUtility.ToJson(dts);
        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
        dataSave.text = json;
    }

    public void LoadDataFn()
    {
        StartCoroutine(LoadDataEnum());
    }

    IEnumerator LoadDataEnum()
    {
        var serverData = dbRef.Child("users").Child(userId).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("process is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            print("server data found");

            dts = JsonUtility.FromJson<dataToSave>(jsonData);
        }
        else
        {
            print("no data found");
        }

    }
}