using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// en.wikipedia.org/wiki/Bubble_sort
public class CombSort : GenericSortingAlg
{

    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = CombSortF;
    }

    public List<int> CombSortF(List<int> sortList)
    {
        StartCoroutine(CombSortFunc(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
            "procedure combsort(A : list of sortable numbers)\n",
            "    gap = A.Length\n",
            "    shrink = 1.3\n",
            "    sorted = false\n",
            "    while sorted = false\n",
            "        gap = floor(gap / shrink)\n",
            "        if gap <= 1\n",
            "            gap = 1\n",
            "            sorted = true\n",
            "        for i = 0 until i + gap < A.Length\n",
            "            if A[i] > A[i+gap]\n",
            "                swap(A[i], A[i+gap])\n",
            "                sorted = false\n",
        };

    IEnumerator CombSortFunc(List<int> A)
    {
        UpdateAlgLine(0, new string[] { "gap", 0.ToString(), "shrink", 0.ToString(),
                        "sorted", 0.ToString(), "i", 0.ToString() }, algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        int gap = A.Count;

        UpdateAlgLine(1, new string[] { "gap", gap.ToString(), "shrink", 0.ToString(),
                        "sorted", 0.ToString(), "i", 0.ToString() }, algText);//Set gap
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        float shrink = 1.3f;

        UpdateAlgLine(2, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", 0.ToString(), "i", 0.ToString() }, algText);//Set shrink
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        bool sorted = false;

        UpdateAlgLine(3, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//Set sorted
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        UpdateAlgLine(4, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//Start while loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        while (!sorted)
        {
            gap = Mathf.FloorToInt(gap / shrink);

            UpdateAlgLine(5, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//Set gap
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }


            UpdateAlgLine(6, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
            if (gap <= 1)
            {
                gap = 1;

                UpdateAlgLine(7, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//set gap
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                sorted = true;

                UpdateAlgLine(8, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//set sorted
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }
            }

            UpdateAlgLine(9, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", 0.ToString() }, algText);//for loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            for (int i = 0; i + gap < A.Count;i++)
            {

                UpdateAlgLine(10, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", i.ToString() }, algText);//if statement
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                if (A[i] > A[i + gap])
                {
                    Swap(A, i, i + gap);
                    UpdateVisualizer("swap", i, i + gap, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", i.ToString() }, 11,algText);
                    yield return new WaitUntil(() => algVisualizer.continueGoing);

                    sorted = false;

                    UpdateAlgLine(12, new string[] { "gap", gap.ToString(), "shrink", shrink.ToString(),
                        "sorted", sorted.ToString(), "i", i.ToString() }, algText);//set sorted
                    if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }
                }
            }
            
        }
        algVisualizer.finish();
    }

}
