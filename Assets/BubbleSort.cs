using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// en.wikipedia.org/wiki/Bubble_sort
public class BubbleSort : GenericSortingAlg{

    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = BubbleSortF;
    }

    public List<int> BubbleSortF(List<int> sortList)
    {
        StartCoroutine(BubFunc(sortList));
        BubFunc(sortList);
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

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

    IEnumerator BubFunc(List<int> sortList)
    {
        int n = sortList.Count;
        int i = 0;
        bool swapped = false;
        UpdateAlgLine(0, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() },algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        UpdateAlgLine(1, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Setting n
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        UpdateAlgLine(2, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Starting the repeat loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        do
        {
            UpdateAlgLine(3, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Setting swap to false
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            swapped = false;
            UpdateAlgLine(4, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Go into the for loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            for (i = 1; i <= n - 1; i++)
            {
                UpdateVisualizer("considerSwap", i - 1, i, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, 5,algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
                if (sortList[i - 1] > sortList[i])
                {
                    swapped = true;
                    UpdateAlgLine(6, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Swapped is true
                    if (algVisualizer.algContainer.activeInHierarchy) {yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }
                    Swap(sortList, i - 1, i);
                    UpdateVisualizer("swap", i - 1, i, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, 7, algText);
                    yield return new WaitUntil(() => algVisualizer.continueGoing);
                    UpdateAlgLine(8, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//End if
                    if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }
                    UpdateAlgLine(4, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Return to the for loop
                    if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }
                }
                else
                {
                    UpdateVisualizer("doNotSwap", i - 1, i, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, 4, algText);//Return to for loop
                    yield return new WaitUntil(() => algVisualizer.continueGoing);
                }
            }
            UpdateAlgLine(9, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//End for
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
        } while (swapped);
        UpdateAlgLine(10, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//Until not swapped
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        algVisualizer.finish();
        UpdateAlgLine(11, new string[] { "n", n.ToString(), "i", i.ToString(), "swapped", swapped.ToString() }, algText);//End procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        TempStorageList = sortList;// Essentially returns the sorted list to the main function
    }

}
