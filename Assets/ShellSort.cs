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
    }

    //Change from 3
    public List<int> ShellSortF(List<int> sortList)
    {
        StartCoroutine(ShellFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        ""
        };

    IEnumerator ShellFuncStart(List<int> sortList)
    {
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
                for (j = i; j >= gap && sortList[j - gap] > temp; j -= gap) { 
                    Debug.Log(sortList[j]);
                    sortList[j] = sortList[j - gap];
                    Debug.Log(sortList[j]);
                    UpdateVisualizer("assign", j, sortList[j - gap],
                        new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", j.ToString() },
                        0, algText);//Return to for loop
                    yield return new WaitUntil(() => algVisualizer.continueGoing);
                }

                //  put temp (the original a[i]) in its correct location 
                sortList[j] = temp;
                UpdateVisualizer("assign", j, temp,
                        new string[] { "gap", gap.ToString(), "i", i.ToString(), "j", j.ToString() },
                        0, algText);//Return to for loop
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
        }
        algVisualizer.finish();
    }


}

