using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.interviewbit.com/tutorial/merge-sort-algorithm/


public class Mergesort : GenericSortingAlg
{

    
    //Error checking for lists that will go over 250 images
    void Start()
    {
        
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = MergeSortF;
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    List<List<int>> lists;

    public List<int> MergeSortF(List<int> sortList)
    {
        StartCoroutine(MergeFuncStart(sortList, 0, sortList.Count - 1));
        //MergeFunc(sortList);
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
    "procedure mergeSort(A : list of sortable items, startI : starting index, endI : ending index)\n",
    "    B = copy of A\n",
    "    if startI < endI\n",
    "        middleI = (startI + endI)/2\n",
    "        mergeSort(A, startI, middleI)\n",
    "        mergeSort(A, middleI + 1, endI)\n",
    "        merge(A,B, startI, middleI, endI)\n",
    "\n",
    "procedure merge(A , B : copy of A, startI , middleI : middle index, endI)\n",
    "    i = startI, j = middleI + 1, k = 0\n",
    "    while i <= middleI and j <= endI\n",
    "        if A[i] <= A[j]\n",
    "            B[k] = A[i]\n",
    "            k = k + 1, i = i + 1\n",
    "        else\n",
    "            B[k] = A[j]\n",
    "            k = k + 1, j = j + 1\n",
    "    while i <= middleI\n",
    "        B[k] = A[i]\n",
    "        k = k + 1, i = i + 1\n",
    "    while j <= endI\n",
    "        B[k] = A[j]\n",
    "        k = k + 1, j = j + 1\n",
    "    for i = startI to endI\n",
    "        A[i] = B[i - startI]\n"};

    string descText = "Merge Sort: \n \nDescription: Merge sort is a recursive divide and conquer sorting algorithm. Given a list, it will split the list into halves recursively until sublist is only one number long. It keeps track of the beginning and end of the sublists through tracker variables. \n \nAfter this, it continually 'merges' the lists. This means after the two sublists are merged together, and after the function runs, those two sublists will be combined and sorted. It does this until the sublist consists of the entire list of numbers, after which it will be sorted. \n \nUse: Merge sort is useful as it is fairly fast as a sorting algorithm, although not the fastest. It works well to demonstrate a divide and conquer algorithm and is generally fast. One major drawback is that it requires temporary storage, making the space complexity higher than some other algorithms. It has O(n log n) time complexity ensuring it will never run too long, even in its worst case scenario. \n \nWorst Case Time Complexity : O(n log n) \nAverage Case Time Complexity: Θ(n log n) \nBest Case Time Complexity : Ω(n log n) \nSpace Complexity : O(n) ";

    IEnumerator MergeFuncStart(List<int> A, int startI, int endI)
    {
        yield return StartCoroutine(MergeFunc(A, 0, A.Count - 1));
        algVisualizer.finish();
    }

    bool firstRun = true;
        
    IEnumerator MergeFunc(List<int> A, int startI, int endI)
    {
        UpdateAlgLine(0, new string[] { "startI", startI.ToString(), "middleI", 0.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        List<int> B;

        if (firstRun)
        {
            algVisualizer.fixVisualization(new List<List<int>> { A, A });
            algVisualizer.numArrays = 2;
            algVisualizer.currentArray = 2;//Index from 0, 2 represents that it is one after the initial two arrays.
            B = new List<int>(A);
            UpdateAlgLine(1, new string[] { "startI", startI.ToString(), "middleI", 0.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//Copy the list
            algVisualizer.WhiteOutAllBars();
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            algVisualizer.multipleLists = true;
            algVisualizer.lists = new List<List<int>>();
            algVisualizer.lists.Add(A);
            algVisualizer.lists.Add(B);

            firstRun = false;
        }
        else
        {
            B = new List<int>(A);
            UpdateAlgLine(1, new string[] { "startI", startI.ToString(), "middleI", 0.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//Copy the list
            algVisualizer.WhiteOutAllBars();
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        UpdateAlgLine(2, new string[] { "startI", startI.ToString(), "middleI", 0.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//startI < endI
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        if (startI < endI)
        {
            int middleI = (startI + endI) / 2;

            UpdateAlgLine(3, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//set middleI
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            UpdateAlgLine(4, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//mergeSort(A,s,m)
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(MergeFunc(A, startI, middleI));

            UpdateAlgLine(5, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//mergeSort(A,m+1,e)
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(MergeFunc(A, middleI + 1, endI));

            UpdateAlgLine(6, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//merge
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(Merge(A, B, startI, middleI, endI));
        }
    }

    IEnumerator Merge(List<int> A, List<int> B, int startI, int middleI, int endI)
    {
        UpdateAlgLine(8, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(),
            "i", 0.ToString(), "j", 0.ToString(), "k", 0.ToString() }, algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        int i = startI;
        int j = middleI + 1;
        int k = 0;
        UpdateAlgLine(9, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//set indexes
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }



        UpdateAlgLine(10, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//start while loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        while (i <= middleI && j <= endI)
        {

            UpdateAlgLine(11, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//A[i] <= A[j]
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            if (A[i] <= A[j])
            {
                B[k] = A[i];
                UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[i], 0, new string[] { "startI", startI.ToString(),
                    "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 12, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);

                k += 1;
                i += 1;

                UpdateAlgLine(13, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//increment k & i
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

            }
            else
            {
                UpdateAlgLine(14, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//else
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                B[k] = A[j];
                UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[j], 0, new string[] { "startI", startI.ToString(),
                    "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 15, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);

                k += 1;
                j += 1;
                UpdateAlgLine(16, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//increment k & j
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }
            }
        }

        UpdateAlgLine(17, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//while statement
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        while (i <= middleI)
        {
            B[k] = A[i];
            UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[i], 0, new string[] { "startI", startI.ToString(),
                "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 18, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            k += 1;
            i += 1;
            UpdateAlgLine(19, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//increment k & i
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
        }

        UpdateAlgLine(20, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//start while loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        while (j <= endI)
        {
            B[k] = A[j];
            UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[j], 0, new string[] { "startI", startI.ToString(),
                "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 21, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            k += 1;
            j += 1;
            UpdateAlgLine(22, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//increment k & i
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
        }

        // copy temp to original interval
        UpdateAlgLine(23, new string[] { "startI", 0.ToString(), "middleI", 0.ToString(), "endI", 0.ToString(),
            "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, algText);//start for loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        for (i = startI; i <= endI; i += 1)
        {
            A[i] = B[i - startI];
            UpdateVisualizer(new List<List<int>> { A, B }, "assign", i, 0, B[i - startI], 0, new string[] { "startI", startI.ToString(),
                "middleI", middleI.ToString(), "endI", endI.ToString(),
                "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 24, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }
    }

     public static float MergeSortTime(List<int> A, int startI, int endI)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        List<int> B = new List<int>(A);

        if (startI < endI)
        {
            int middleI = (startI + endI) / 2;

            MergeSortTimeDepth2(A, startI, middleI);

            MergeSortTimeDepth2(A, middleI + 1, endI);

            MergeTime(A, B, startI, middleI, endI);
        }
        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);//Calculates micro seconds by ticks
    }

    public static void MergeSortTimeDepth2(List<int> A, int startI, int endI)//So that merge sort wont need to restart the stopwatch
    {
        List<int> B = new List<int>(A);

        if (startI < endI)
        {
            int middleI = (startI + endI) / 2;

            MergeSortTimeDepth2(A, startI, middleI);

            MergeSortTimeDepth2(A, middleI + 1, endI);

            MergeTime(A, B, startI, middleI, endI);
        }
    }
    public static void MergeTime(List<int> A, List<int> B, int startI, int middleI, int endI)
    {
        int i = startI;
        int j = middleI + 1;
        int k = 0;

        while (i <= middleI && j <= endI)
        {
            if (A[i] <= A[j])
            {
                B[k] = A[i];

                k += 1;
                i += 1;
            }
            else
            {
                B[k] = A[j];

                k += 1;
                j += 1;
            }
        }
        while (i <= middleI)
        {
            B[k] = A[i];

            k += 1;
            i += 1;
        }

        while (j <= endI)
        {
            B[k] = A[j];

            k += 1;
            j += 1;
        }

        for (i = startI; i <= endI; i += 1)
        {
            A[i] = B[i - startI];
        }
    }


}

