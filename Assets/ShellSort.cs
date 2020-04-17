using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.geeksforgeeks.org/shellsort/


public class ShellSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = ShellSortF;
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Change from 3
    public List<int> ShellSortF(List<int> sortList)
    {
        StartCoroutine(ShellFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
            "procedure shellSort(A : list of sortable items )\n",
            "    for (int gap = A.Count / 2; gap > 0; gap /= 2)\n",
            "        for (int i = gap; i < n; i += 1)\n",
            "            temp = A[i]\n",
            "            int j\n",
            "            for (j = i; j >= gap && A[j - gap] > temp; j -= gap)\n",
            "                A[j] = A[j - gap]\n",
            "            A[j] = temp\n",
            "end procedure\n"
        };

    string descText = "Shell sort:\n\nDescription: Shell sort is a variation on insertion sort where you insert items across a gap, rather than one position ahead. There are three for loops nested. The outermost for loop gradually shrinks the gap. The next for loop gap sorts it. The next for loop shifts it to the correct spot. This algorithm is better demonstrated on a slightly larger list.\n\nUse: Shell sort, unlike insertion sort, is able to insert at any distance allowing it to be faster than insertion sort in many situations.\n\nWorst Case Time Complexity:   O(n^2)\nAverage Case Time Complexity: Θ(n^2)\nBest Case Time Complexity:    Ω(n)\nSpace Complexity:             O(1)";

    IEnumerator ShellFuncStart(List<int> sortList)
    {
        // Slowly close the gap
        for (int gap = sortList.Count / 2; gap > 0; gap /= 2)
        {

            UpdateAlgLine(1,7, new string[] { "gap", gap.ToString(), "i", "null", "j", "null", "temp", "null" }, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }


            // Do a gapped insertion sort for this gap size. 
            // The first gap elements a[0..gap-1] are already in gapped order 
            // keep adding one more element until the entire array is 
            // gap sorted  
            for (int i = gap; i < sortList.Count; i += 1)
            {

                UpdateAlgLine(2, new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", "null", "temp", "null" }, algText);//For loop
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }


                // add a[i] to the elements that have been gap sorted 
                // save a[i] in temp and make a hole at position i 
                int temp = sortList[i];

                UpdateAlgLine(3, new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", "null", "temp", temp.ToString() }, algText);//For loop
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                // shift earlier gap-sorted elements up until the correct  
                // location for a[i] is found 
                int j;

                UpdateAlgLine(4, new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", 0.ToString(), "temp", temp.ToString() }, algText);//For loop
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                for (j = i; j >= gap && sortList[j - gap] > temp; j -= gap) {

                    UpdateAlgLine(5, new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", j.ToString(), "temp", temp.ToString() }, algText);//For loop
                    if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }

                    sortList[j] = sortList[j - gap];
                    UpdateVisualizer("assign", j, sortList[j - gap],
                        new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", j.ToString(),"temp", temp.ToString() },
                        6, algText);//Return to for loop
                    yield return new WaitUntil(() => algVisualizer.continueGoing);
                }

                //  put temp (the original a[i]) in its correct location 
                sortList[j] = temp;
                UpdateVisualizer("assign", j, temp,
                        new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", j.ToString(),"temp", temp.ToString() },
                        7, algText);//Return to for loop
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
        }
        algVisualizer.finish();
    }

    public static float ShellSortTime(List<int> sortList)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        // Slowly close the gap
        for (int gap = sortList.Count / 2; gap > 0; gap /= 2)
        {
            // Do a gapped insertion sort for this gap size. 
            // The first gap elements a[0..gap-1] are already in gapped order 
            // keep adding one more element until the entire array is 
            // gap sorted  
            for (int i = gap; i < sortList.Count; i += 1)
            {
                // add a[i] to the elements that have been gap sorted 
                // save a[i] in temp and make a hole at position i 
                int temp = sortList[i];
                // shift earlier gap-sorted elements up until the correct  
                // location for a[i] is found 
                int j;
                for (j = i; j >= gap && sortList[j - gap] > temp; j -= gap)
                {
                    sortList[j] = sortList[j - gap];
                }

                //  put temp (the original a[i]) in its correct location 
                sortList[j] = temp;
            }
        }


        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero
    }


}

