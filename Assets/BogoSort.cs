using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.geeksforgeeks.org/bogosort-permutation-sort/


public class BogoSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = BogoSortF;
    }

    //Change from 3
    public List<int> BogoSortF(List<int> sortList)
    {
        StartCoroutine(BogoFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        ""
        };

    // To check if array is sorted or not  
    public static bool isSorted(List<int> sortList)
    {
        int i = 0;
        while (i < sortList.Count - 1)
        {
            if (sortList[i] > sortList[i + 1])
                return false;
            i++;
        }
        return true;
    }

    IEnumerator BogoFuncStart(List<int> sortList)
    {
        System.Random rand = new System.Random();
        while (!isSorted(sortList)){
            for(int i = 0; i < sortList.Count; i++)
            {
                int randSwapIndex = rand.Next(0, sortList.Count);
                Swap(sortList, i, randSwapIndex);
                UpdateVisualizer("swap", i, randSwapIndex,
                    new string[] { "i", i.ToString(), "randSwapIndex", randSwapIndex.ToString() }, 0, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
        }
        algVisualizer.finish();

    }


}

