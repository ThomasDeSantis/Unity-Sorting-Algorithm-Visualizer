using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://en.wikipedia.org/wiki/Quicksort

public class QuickSort : GenericSortingAlg {
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = QuickSortF;
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    public List<int> QuickSortF(List<int> sortList)
    {
        StartCoroutine(QuickFuncStart(sortList,0,sortList.Count - 1));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
    "procedure quickSort(A : list of sortable numbers, lo : the beginning of the list, hi : the end of the list)\n",
    "    if lo < hi then\n",
    "        p = partition(A, lo, hi)\n",
    "        quicksort(A, lo, p - 1)\n",
    "        quicksort(A, p + 1, hi)\n",
    "\n",
    "procedure partition(A, lo, hi)\n",
    "    pivot = A[hi]\n",
    "    i = lo\n",
    "    for j = lo to hi\n",
    "        if A[j] < pivot\n",
    "            swap A[i] with A[j]\n",
    "            i = i + 1\n",
    "    swap A[i] with A[hi]\n",
    "    return i\n"
        };

    string descText = "Quick Sort: \n \nDescription: This algorithm sorts an unsorted list by first partitioning a list into two different sublists. The size of each sublist is decided by a 'pivot', which in this particular algorithm is always the last element of the sublist. The two sublists are then arranged so all the elements smaller than the pivot are to the left of the sublist, and all elements larger than the pivot are to the right of it. This means the pivot is in the correctly sorted position. This is recursively done to increasingly smaller sublists until the list is completely sorted. \n \nUse: Quick sort is, as its name implies, very quick (on average), and is often the fastest sorting algorithm. In its worst case, however, which is a list reverse sorted with the largest elements to the left of the list, it takes O(n^2) time. The worst case scenario is very rare, however, and generally happens when a poor pivot is chosen. Some versions of the algorithm use randomly chosen pivots to make the worst case less likely. \n \nWorst Case Time Complexity : O(n^2) \nAverage Case Time Complexity: Θ(n log n) \nBest Case Time Complexity : Ω(n log n) \nSpace Complexity : O(1) ";

    private int pCont = 0;

    IEnumerator QuickFuncStart(List<int> sortList, int lo, int hi)
    {
        yield return StartCoroutine(QuickFunc(sortList, 0, sortList.Count - 1));
        yield return new WaitUntil(() => algVisualizer.continueGoing);
        algVisualizer.finish();
    }

    IEnumerator QuickFunc(List<int> sortList, int lo, int hi)
    {

        UpdateAlgLine(0, new string[]{ "pivot", "0", "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", "0" }, algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        UpdateAlgLine(1, new string[]{ "pivot", "0", "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", "0" }, algText);//if lo < hi
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        if (lo < hi)
        {

            UpdateAlgLine(2, new string[]{ "pivot", "0", "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", "0" }, algText);//partition
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(partition(sortList,lo,hi));
            int p = pCont;//pCont used to return value as it must return IEnumerable, not int

            UpdateAlgLine(3, new string[]{ "pivot", "0", "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", p.ToString() }, algText);//quickfunc
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(QuickFunc(sortList, lo, p - 1));

            UpdateAlgLine(4, new string[]{ "pivot", "0", "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", p.ToString() }, algText);//quickfunc
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(QuickFunc(sortList, p + 1, hi));
        }
    }

    void Swap(List<int> sortList,int i1,int i2)
    {
        int temp = sortList[i1];
        sortList[i1] = sortList[i2];
        sortList[i2] = temp;
    }


    IEnumerator partition(List<int> sortList, int lo, int hi)
    {

        UpdateAlgLine(6, new string[]{ "pivot", "0", "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", 0.ToString() }, algText);//proc
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        int pivot = sortList[hi];

        UpdateAlgLine(7, new string[]{ "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", 0.ToString(), "j", "0", "p", 0.ToString() }, algText);//pivot set
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        int i = lo;

        UpdateAlgLine(8, new string[]{ "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", "0", "p", 0.ToString() }, algText);//i set
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        UpdateAlgLine(9, new string[]{ "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", "0", "p", 0.ToString() }, algText);//for loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        for (int j = lo;j <= hi; j++)
        {

            UpdateAlgLine(10, new string[]{ "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", j.ToString(), "p", 0.ToString() }, algText);//if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            if (sortList[j] < pivot)
            {
                Swap(sortList, i, j);
                UpdateVisualizer("swap", i, j, new string[] { "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", j.ToString(), "p", 0.ToString() }, 11, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
                i = i + 1;

                UpdateAlgLine(12, new string[]{ "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", j.ToString(), "p", 0.ToString() }, algText);//if
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }
            }
        }
        Swap(sortList, i, hi);
        UpdateVisualizer("swap", i, hi, new string[] { "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", 0.ToString(), "p", 0.ToString() }, 13, algText);
        yield return new WaitUntil(() => algVisualizer.continueGoing);
        pCont = i;
        UpdateAlgLine(14, new string[]{ "pivot", pivot.ToString(), "lo", lo.ToString(), "hi", hi.ToString(),
            "i", i.ToString(), "j", 0.ToString(), "p", 0.ToString() }, algText);//if
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
    }

    public static float QuickSortStartTime(List<int> sortList, int lo, int hi)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        QuickSortTime(sortList, 0, sortList.Count - 1);
        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero
    }

    static void QuickSortTime(List<int> sortList, int lo, int hi)
    {
        if (lo < hi)
        {
            int p = partitionTime(sortList, lo, hi);
            QuickSortTime(sortList, lo, p - 1);
            QuickSortTime(sortList, p + 1, hi);
        }
    }

    static void SwapTime(List<int> sortList, int i1, int i2)
    {
        int temp = sortList[i1];
        sortList[i1] = sortList[i2];
        sortList[i2] = temp;
    }


    static int partitionTime(List<int> sortList, int lo, int hi)
    {
        int pivot = sortList[hi];
        int i = lo;
        for (int j = lo; j <= hi; j++)
        {
            if (sortList[j] < pivot)
            {
                SwapTime(sortList, i, j);
                i = i + 1;
            }
        }
        SwapTime(sortList, i, hi);
        return i;//pCont = i
    }
}
