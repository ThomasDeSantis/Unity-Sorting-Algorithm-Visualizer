using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BubbleSwitch()
    {
        SortName.sortMethod = "BubbleSort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single );
    }

    public void CocktailSwitch()
    {
        SortName.sortMethod = "CocktailSort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single);
    }

    public void CombSwitch()
    {
        SortName.sortMethod = "CombSort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single);
    }

    public void HeapSwitch()
    {
        SortName.sortMethod = "HeapSort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single);
    }

    public void InsertionSwitch()
    {
        SortName.sortMethod = "InsertionSort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single);
    }

    public void MergeSwitch()
    {
        SortName.sortMethod = "Mergesort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single);
    }

    public void QuickSwitch()
    {
        SortName.sortMethod = "QuickSort";
        SceneManager.LoadScene("Sort", LoadSceneMode.Single);
    }


}
