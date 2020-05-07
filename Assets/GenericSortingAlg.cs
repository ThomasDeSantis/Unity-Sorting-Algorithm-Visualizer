using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSortingAlg : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        ;
	}

    public List<int> TempStorageList = new List<int>();

    public Visualizer algVisualizer;
    //Given a list, swaps indices x and y
    public static void Swap(List<int> sortList, int x, int y)
    {
        int temp = sortList[x];
        sortList[x] = sortList[y];
        sortList[y] = temp;
    }
    int actions = 0;
    int assigns = 0;
    public bool multipleArrays;


    //,"swap", "assign", or "finish"
    public void UpdateVisualizer(string typeOfChange, int indexSwap1, int indexSwap2, string[] varList, int algLine,string[] algTextDescription)
    {
        actions++;
        if (algVisualizer.tempUnpaused)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }
        algVisualizer.continueGoing = false;//Need to wait again
        switch (typeOfChange)
        {
            case "swap":
                algVisualizer.swapThem(indexSwap1, indexSwap2);
                assigns += 2;
                actions++;//Swaps count as 2 actions
                break;
            case "assign":
                algVisualizer.assign(indexSwap1, indexSwap2);//1 represents the index, 2 represents the value
                assigns++;
                break;
            case "finish":
                algVisualizer.finish();
                break;
        }
        string[] algText = new string[algTextDescription.Length];
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions,assigns);
        algTextDescription.CopyTo(algText, 0);
        algVisualizer.changeVariables(varList);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //TO DO: Make generic for many lists
    //"considerSwap","swap","doNotSwap","finish","assign"
    //iList1 is the index of list1
    public void UpdateVisualizer(List<List<int>> lists, string typeOfChange, int indexSwap1,int iList1, int indexSwap2, int iList2, string[] varList, int algLine, string[] algTextDescription)
    {
        actions++;
        if (algVisualizer.tempUnpaused)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }
        if(iList1 != 0)
        {
            indexSwap1 += lists[0].Count;
        }
        if (iList2 != 0)
        {
            indexSwap1 += lists[0].Count;
        }
        algVisualizer.continueGoing = false;//Need to wait again
        switch (typeOfChange)
        {
            case "swap":
                algVisualizer.swapThem(indexSwap1, indexSwap2);
                assigns += 2;
                actions++;
                break;
            case "assign":
                algVisualizer.assign(indexSwap1, lists);//1 represents the index, 2 represents the value
                assigns++;
                break;
            case "finish":
                algVisualizer.finish();
                break;
        }
        string[] algText = new string[algTextDescription.Length];
        algTextDescription.CopyTo(algText, 0);
        algVisualizer.changeVariables(varList);
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    public void UpdateAlgLine(int algLine, string[] varList,string[] algTextDescription)
    {
        actions++;
        if (algVisualizer.tempUnpaused && algVisualizer.algContainer.activeInHierarchy)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.continueGoing = false;//Need to wait again (if in alg mode;
        string[] algText = new string[algTextDescription.Length];
        algTextDescription.CopyTo(algText, 0);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.changeVariables(varList);
        if (char.IsWhiteSpace(algText[algLine][0])) algText[algLine].Substring(1);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    public void UpdateAlgLine(int algLine,int newActions, string[] varList, string[] algTextDescription)
    {
        actions += newActions;
        if (algVisualizer.tempUnpaused && algVisualizer.algContainer.activeInHierarchy)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.continueGoing = false;//Need to wait again (if in alg mode;
        string[] algText = new string[algTextDescription.Length];
        algTextDescription.CopyTo(algText, 0);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.changeVariables(varList);
        if (char.IsWhiteSpace(algText[algLine][0])) algText[algLine].Substring(1);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    public void addActions(int newActions)
    {
        actions += newActions;
    }


}
