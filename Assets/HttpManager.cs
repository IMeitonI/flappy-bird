using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{

    [SerializeField]
    private string URL;
    [SerializeField] Text[] texts;
    int textCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        string url = URL + "/leaders";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if(www.responseCode == 200){
            //Debug.Log(www.downloadHandler.text);
            Scores resData = JsonUtility.FromJson<Scores>(www.downloadHandler.text);
            textCount = 0;
            foreach (ScoreData score in resData.scores)
            { 
                texts[textCount].text= score.userId.ToString() +". Score: "+score.value.ToString();
                textCount += 1;
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
   
}


[System.Serializable]
public class ScoreData
{
    public int userId;
    public int value;

}

[System.Serializable]
public class Scores
{
    public ScoreData[] scores;
}
