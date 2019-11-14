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

}

