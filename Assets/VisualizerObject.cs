using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizerObject : MonoBehaviour {
    private Text numText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int num = 0;

    public void changeNum(int newNum, float x, float y, float sizeX, float sizeY)
    {
        numText = GetComponentInChildren<Text>();
        numText.fontSize = 20;
        Debug.Log(numText.transform.position.x);
        num = newNum;
        numText.transform.position = new Vector3(x, y, 0);
        numText.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
        numText.text = num.ToString();
        numText.alignment = TextAnchor.MiddleCenter;
        numText.resizeTextForBestFit = true;
    }
    public void changeNumHeight(int newNum,float sizeY)
    {
        num = newNum;
        numText.text = num.ToString();
        numText.rectTransform.sizeDelta = new Vector2(numText.rectTransform.sizeDelta.x, sizeY);
    }
}
