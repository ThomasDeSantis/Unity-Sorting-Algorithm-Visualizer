using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// https://www.interviewbit.com/tutorial/insertion-sort-algorithm/
public class InsertionSort : GenericSortingAlg
{

    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = InsertionSortF;
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algTextDescription.Length; k++)
        {
            newAlgFullText += algTextDescription[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }


    string[] algTextDescription = {
            "procedure insertionSort(A : list of sortable items )\n",
            "    for i = 1 to n\n",
            "        key = A [1]\n",
            "        j = 1 - 1\n",
            "        while j > 0 and A[j] > key\n",
            "            A[j+1] = A[j]\n",
            "            j = j - 1\n",
            "        end while\n",
            "        A[j+1] = key\n",
            "    end for\n",
            "end procedure\n"
        };

    string descText = "Insertion Sort: \n \nDescription: This algorithm takes an element (in order starting from 1, not 0). It then assumes that the first element is already sorted. It compares the element with the one directly before it. If it is less, it is unsorted and replaces the element before it. It does this until it reaches an element who it is larger than, and at this point the element has become sorted. This causes the array prior to the currently inspected element to be always sorted, and each element being sorted is simply 'inserted' into the correct spot. \n \nUse: It is generally efficient, despite being O(n^2) in the worst case (which is if the list is sorted reversely, with the largest numbers at index 0 and the smallest at the end of the list). However, it is very efficient at sorted already sorted or nearly sorted lists with a best case complexity of Ω(n). This makes it efficient if you are sorting a list which you have just added an element on to, it would do that very quickly. \n \nWorst Case Time Complexity : O(n^2) \nAverage Case Time Complexity: Θ(n^2) \nBest Case Time Complexity : Ω(n) \nSpace Complexity : O(1)";

    public List<int> InsertionSortF(List<int> sortList)
    {
        StartCoroutine(InsertFunc(sortList));
        InsertFunc(sortList);
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }
    

    IEnumerator InsertFunc(List<int> sortList)
    {
        int i = 0, j = 0, key = 0;
        UpdateAlgLine(0, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        for(i = 1; i < sortList.Count; i++)
        {
            UpdateAlgLine(1, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//for
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            key = sortList[i];
            UpdateAlgLine(2, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//key = a[i]
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            j = i - 1;
            UpdateAlgLine(3, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//j = i - 1
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            while(j >= 0 && sortList[j] > key)
            {
                UpdateAlgLine(4, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//while
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }
                sortList[j + 1] = sortList[j];
                UpdateVisualizer("assign", j + 1, sortList[j], new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, 5, algTextDescription);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
                j = j - 1;
                UpdateAlgLine(6, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//j = j - 1
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }
            }
            UpdateAlgLine(7, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//end while
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            sortList[j + 1] = key;//might not be needed with assign???
            UpdateVisualizer("assign", j + 1, key, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, 8, algTextDescription);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }
        UpdateAlgLine(9, new string[] { "i", i.ToString(), "j", j.ToString(), "key", key.ToString() }, algTextDescription);//end while
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        algVisualizer.finish();
    }

    public static float InsertionSortTime(List<int> sortList)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        int i = 0, j = 0, key = 0;
        
        for (i = 1; i < sortList.Count; i++)
        {
            key = sortList[i];
            j = i - 1;
            while (j >= 0 && sortList[j] > key)
            {
                sortList[j + 1] = sortList[j];
                j = j - 1;
            }
            sortList[j + 1] = key;
        }
        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero
    }

}

