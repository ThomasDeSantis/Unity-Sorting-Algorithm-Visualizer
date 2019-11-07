using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// en.wikipedia.org/wiki/Bubble_sort
public class BubbleSort : MonoBehaviour{

    void Start()
    {
        
    }

    void Update()
    {
        ;
    }

    public Visualizer myVis;
    //Given a list, swaps indices x and y
    private static void Swap(List<int> sortList, int x, int y)
    {
        int temp = sortList[x];
        sortList[x] = sortList[y];
        sortList[y] = temp;
    }

    //"considerSwap","swap","doNotSwap","finish"
    private void UpdateVisualizer(string typeOfChange,int indexSwap1,int indexSwap2,int n,int i, bool swapped,int algLine)
    {
        if (myVis.tempUnpaused)
        {
            myVis.tempUnpaused = false;
            myVis.continueGoing = false;
            myVis.paused = true;
        }
        myVis.continueGoing = false;//Need to wait again
        switch (typeOfChange)
        {
            case "considerSwap":
                myVis.considerSwapThem(indexSwap1, indexSwap2);
                break;
            case "swap":
                myVis.swapThem(indexSwap1, indexSwap2);
                break;
            case "doNotSwap":
                myVis.doNotSwapThem(indexSwap1, indexSwap2);
                break;
            case "finish":
                myVis.finish();
                break;
        }
        string[] varList = new string[]{"n",n.ToString(),"i",i.ToString(),"swapped",swapped.ToString() };
        myVis.changeVariables(varList);
        string[] algText =
        {
            "procedure bubbleSort(A : list of sortable items )\n",
            "    n = length(A)\n",
            "    repeat\n",
            "        swapped = false\n",
            "        for i = 1 to n - 1 inclusive do\n",
            "            if A[i - 1] > A[i] then\n",
            "                swapped = true\n",
            "                swap(A[i - 1], A[i])\n",
            "            end if\n",
            "        end for\n",
            "   until not swapped\n",
            "end procedure"
        };
        string newAlgFullText = "";
        for (int j = 0; j < algText.Length; j++)
        {
            if (j == algLine)
            {
                algText[j] = "<b>" + algText[j] + "</b>";
            }
            newAlgFullText += algText[j];
        }
        myVis.algText.text = newAlgFullText;
    }

    private void UpdateAlgLine(int algLine)
    {
        if (myVis.tempUnpaused && myVis.algContainer.activeInHierarchy)
        {
            myVis.tempUnpaused = false;
            myVis.continueGoing = false;
            myVis.paused = true;
        }
        if (myVis.algContainer.activeInHierarchy) myVis.continueGoing = false;//Need to wait again (if in alg mode;
        string[] algText = {
            "procedure bubbleSort(A : list of sortable items )\n",
            "    n = length(A)\n",
            "    repeat\n",
            "        swapped = false\n",
            "        for i = 1 to n - 1 inclusive do\n",
            "            if A[i - 1] > A[i] then\n",
            "                swapped = true\n",
            "                swap(A[i - 1], A[i])\n",
            "            end if\n",
            "        end for\n",
            "   until not swapped\n",
            "end procedure"
        };
        if (char.IsWhiteSpace(algText[algLine][0])) algText[algLine].Substring(1);
        string newAlgFullText = "";
        for (int j = 0; j < algText.Length; j++)
        {
            if (j == algLine)
            {
                algText[j] = "<b>" + algText[j] + "</b>";
            }
            newAlgFullText += algText[j];
        }
        myVis.algText.text = newAlgFullText;
    }

    IEnumerator BubFunc(List<int> sortList)
    {
        UpdateAlgLine(0);//Start Procedure
        if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
        else { yield return new WaitUntil(() => !myVis.paused); }
        UpdateAlgLine(1);//Setting n
        if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
        else { yield return new WaitUntil(() => !myVis.paused); }
        int n = sortList.Count;
        int i;
        bool swapped;
        UpdateAlgLine(2);//Starting the repeat loop
        if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
        else { yield return new WaitUntil(() => !myVis.paused); }
        do
        {
            UpdateAlgLine(3);//Setting swap to false
            if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
            else { yield return new WaitUntil(() => !myVis.paused); }
            swapped = false;
            UpdateAlgLine(4);//Go into the for loop
            if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
            else { yield return new WaitUntil(() => !myVis.paused); }
            for (i = 1; i <= n - 1; i++)
            {
                UpdateVisualizer("considerSwap", i - 1, i, n, i, swapped,5);
                yield return new WaitUntil(() => myVis.continueGoing);
                if (sortList[i - 1] > sortList[i])
                {
                    swapped = true;
                    UpdateAlgLine(6);//Swapped is true
                    if (myVis.algContainer.activeInHierarchy) {yield return new WaitUntil(() => myVis.continueGoing); }
                    else { yield return new WaitUntil(() => !myVis.paused); }
                    Swap(sortList, i - 1, i);
                    UpdateVisualizer("swap", i - 1, i, n, i, swapped, 7);
                    yield return new WaitUntil(() => myVis.continueGoing);
                    UpdateAlgLine(8);//End if
                    if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
                    else { yield return new WaitUntil(() => !myVis.paused); }
                    UpdateAlgLine(4);//Return to the for loop
                    if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
                    else { yield return new WaitUntil(() => !myVis.paused); }
                }
                else
                {
                    UpdateVisualizer("doNotSwap", i - 1, i, n, i, swapped, 4);//Return to for loop
                    yield return new WaitUntil(() => myVis.continueGoing);
                }
            }
            UpdateAlgLine(9);//End for
            if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
            else { yield return new WaitUntil(() => !myVis.paused); }
        } while (swapped);
        UpdateAlgLine(10);//Until not swapped
        if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
        else { yield return new WaitUntil(() => !myVis.paused); }
        myVis.finish();
        UpdateAlgLine(11);//End procedure
        if (myVis.algContainer.activeInHierarchy) { yield return new WaitUntil(() => myVis.continueGoing); }
        else { yield return new WaitUntil(() => !myVis.paused); }
        TempStorageList = sortList;// Essentially returns the sorted list to the main function
    }

    List<int> TempStorageList = new List<int>();

    public List<int> BubbleSortF(List<int> sortList)
    {
        StartCoroutine(BubFunc(sortList));
        myVis = GetComponent<Visualizer>();
        BubFunc(sortList);
        myVis.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }
}
