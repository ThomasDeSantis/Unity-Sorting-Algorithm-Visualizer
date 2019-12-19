﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://en.wikipedia.org/wiki/Quicksort

public class QuickSort : GenericSortingAlg {
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = QuickSortF;
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
}