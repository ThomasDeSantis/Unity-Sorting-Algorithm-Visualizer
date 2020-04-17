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
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Change from 3
    public List<int> BogoSortF(List<int> sortList)
    {
        StartCoroutine(BogoFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        "procedure bogoSort(A : list of sortable items )\n",
        "    while not sorted\n",
        "    for i = 0 to Length(A)\n",
        "        randSwapIndex = random number between 0 and Length(A)\n",
        "        swap i and randSwapIndex\n",
        "end procedure\n"
        };

    string descText = "Bogo sort:\n\nDescription: Bogo sort is a notoriously bad algorithm. It randomly generates permutations of a set of numbers until one happens to be sorted. This means its possible for it to never be sorted if the permutation is never used. This is the version that randomly generates permutations, if you generated all permutations instead then you would eventually find it.\n\nUse: There is no good use for this algorithm.\n\nWorst Case Time Complexity:   O(∞)\nAverage Case Time Complexity: Θ(n*n!)\nBest Case Time Complexity:    Ω(1)\nSpace Complexity:             O(1)";

    // To check if array is sorted or not  
    bool isSorted(List<int> sortList)
    {
        int i = 0;
        addActions(1);
        while (i < sortList.Count - 1)
        {
            addActions(6);//About 6 actions happen per loop
            if (sortList[i] > sortList[i + 1])
            {
                return false;
            }
            i++;

        }
        return true;
    }

    IEnumerator BogoFuncStart(List<int> sortList)
    {
        UpdateAlgLine(0, new string[] { "i", "Null", "randSwapIndex", "Null" }, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        System.Random rand = new System.Random();

        while (!isSorted(sortList)){

            UpdateAlgLine(1, new string[] { "i", "Null", "randSwapIndex", "Null" }, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            for (int i = 0; i < sortList.Count; i++)
            {
                UpdateAlgLine(2, new string[] { "i", i.ToString(), "randSwapIndex", "Null" }, algText);//For loop
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                int randSwapIndex = rand.Next(0, sortList.Count);

                UpdateAlgLine(3, new string[] { "i", i.ToString(), "randSwapIndex", randSwapIndex.ToString() }, algText);//For loop
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                Swap(sortList, i, randSwapIndex);
                UpdateVisualizer("swap", i, randSwapIndex,
                    new string[] { "i", i.ToString(), "randSwapIndex", randSwapIndex.ToString() }, 4, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
        }
        algVisualizer.finish();

    }

    static bool isSortedTime(List<int> sortList)
    {
        int i = 0;
        while (i < sortList.Count - 1)
        {
            if (sortList[i] > sortList[i + 1])
            {
                return false;
            }
            i++;

        }
        return true;
    }

    public static string BogoSortTime(List<int> sortList)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        System.Random rand = new System.Random();

        while (!isSortedTime(sortList))
        {

            for (int i = 0; i < sortList.Count; i++)
            {
                int randSwapIndex = rand.Next(0, sortList.Count);

                Swap(sortList, i, randSwapIndex);
            }
            long ticks = (int)(stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency;
            Debug.Log(ticks);
            if (ticks >= 10000){
                return "Time out";
            }
        }

        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency).ToString();//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero

    }


}

