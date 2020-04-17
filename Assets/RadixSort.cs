using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//https://www.geeksforgeeks.org/radix-sort/


public class RadixSort : GenericSortingAlg
{      
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = RadixSortF;
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Change from 3
    public List<int> RadixSortF(List<int> sortList)
    {
        StartCoroutine(RadixFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        "function radixSort(list) is\n",
        "   max = the maximum key in the array\n",
        "   for (int exp = 1; max/exp > 0; exp *= 10)//(for every digit) \n",
        "       countSort(list,exp)\n",
        "function countSort(list, exp) is\n",
        "    create new list with n spaces named output\n",
        "    create new list with 10 spaces named count\n",
        "    store how many times you find each digit in count\n",
        "    change count[i] to represent the position in output\n",
        "    for (i = n - 1; i >= 0; i--)\n",
        "        output[count[ (list[i]/exp)%10 ] - 1] = list[i]\n",
        "        count[ (list[i]/exp)%10 ]--",
        "    copy output to list"
        };

    string descText = "Radix sort:\n\nDescription: Radix sort is a distributive sorting algorithm that uses counting sort to sort each digit. It takes n + k time, with the largest digit being k. This means so long as the largest digit is not n^2 it runs in linear time.\n\nUse: As it runs it can run in linear time it can be much faster than algorithms such as quick sort in certain situations. \n\nWorst Case Time Complexity:   O(nk)\nAverage Case Time Complexity: Θ(n + k)\nBest Case Time Complexity:    Ω(n + k)\nSpace Complexity:             O(n + k)";

    //Returns the maximum int in a list
    static int maxInt(List<int> sortList)
    {
        int max = sortList[0];

        for(int i = 1; i < sortList.Count; i++)
        {
            if(sortList[i] > max)
            {
                max = sortList[i];
            }
        }
        return max;
    }

    IEnumerator RadixFuncStart(List<int> sortList)
    {

        UpdateAlgLine(0, new string[] { "i", "Null", "exp", "Null","max","Null" }, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        //Get the max number
        int max = maxInt(sortList);

        UpdateAlgLine(1,3+sortList.Count, new string[] { "i", "Null", "exp", "Null", "max", max.ToString() }, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        //Do this for every single digit spot for the max number
        for (int exp = 1;max/exp > 0; exp *= 10)
        {

            UpdateAlgLine(2, new string[] { "i", "Null", "exp", exp.ToString(), "max", max.ToString() }, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(CountSort(sortList, exp));
            algVisualizer.fixVisualization(sortList);
            UpdateVisualizer("none", 0, 0, new string[] { "i", "Null", "exp", exp.ToString(), "max", max.ToString() }, 3, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        algVisualizer.finish();

    }


    IEnumerator CountSort(List<int> sortList, int exp)
    {

        UpdateAlgLine(4, new string[] { "i", "Null", "exp", exp.ToString(), "max", "Null" }, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        int i;
        List<int> output = new List<int>();//Will hold the array sorted for the current digit
        for (i = 0; i < sortList.Count; i++)
        {
            output.Add(0);//init output to size of sortlist
        }

        UpdateAlgLine(5,sortList.Count*2, new string[] { "i", i.ToString(), "exp", exp.ToString(), "max", "Null" }, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        List<int> count = new List<int>();
        List<string> names = new List<string>();

        algVisualizer.lists = new List<List<int>>();
        algVisualizer.multipleLists = true;

        algVisualizer.lists.Add(sortList);
        names.Add("Original List");

        algVisualizer.lists.Add(output);
        names.Add("Output List");

        algVisualizer.lists.Add(count);
        names.Add("Count");


        for (i = 0; i < 10; i++)
        {
            count.Add(0);//Init count to all zeroes
            //UpdateVisualizer(algVisualizer.lists, "assign", i, 2, 0, 0, new string[] { "i", 0.ToString(),"exp",exp.ToString()}, 0, algText);
            //algVisualizer.fixVisualization(algVisualizer.lists, names);
            //yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        UpdateAlgLine(6, 20, new string[] { "i", i.ToString(), "exp", exp.ToString(), "max", "Null" }, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        //Count the location where each digit would be hashed into
        for (i = 0; i < sortList.Count; i++)
        {
            count[(sortList[i] / exp) % 10]++;

            UpdateVisualizer(algVisualizer.lists, "assign", (sortList[i] / exp) % 10, 2, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 7, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //Have count be put in the slot of the actual digit
        for (i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
            UpdateVisualizer(algVisualizer.lists, "assign", i, 2, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 8, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        for (i = sortList.Count - 1; i >= 0; i--)
        {
            UpdateAlgLine(9, 4, new string[] { "i", i.ToString(), "exp", exp.ToString(), "max", "Null" }, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            int OutputHash = count[(sortList[i] / exp) % 10] - 1;
            output[OutputHash] = sortList[i];
            UpdateVisualizer(algVisualizer.lists, "none", (OutputHash), 1, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 10, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            int CountHash = (sortList[i] / exp) % 10;

            count[CountHash]--;
            UpdateVisualizer(algVisualizer.lists, "assign", CountHash, 2, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 11, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //Copy it to the sort list
        for (i = 0; i < sortList.Count; i++)
        {
            sortList[i] = output[i];
            UpdateVisualizer(algVisualizer.lists, "assign", i, 0, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 12, algText);
            //algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //algVisualizer.multipleLists = false;

    }

    static void CountSortTime(List<int> sortList, int exp)
    {

        int i;
        List<int> output = new List<int>();//Will hold the array sorted for the current digit
        for (i = 0; i < sortList.Count; i++)
        {
            output.Add(0);//init output to size of sortlist
        }

        List<int> count = new List<int>();


        for (i = 0; i < 10; i++)
        {
            count.Add(0);//Init count to all zeroes
            //UpdateVisualizer(algVisualizer.lists, "assign", i, 2, 0, 0, new string[] { "i", 0.ToString(),"exp",exp.ToString()}, 0, algText);
            //algVisualizer.fixVisualization(algVisualizer.lists, names);
            //yield return new WaitUntil(() => algVisualizer.continueGoing);
        }


        //Count the location where each digit would be hashed into
        for (i = 0; i < sortList.Count; i++)
        {
            count[(sortList[i] / exp) % 10]++;
        }

        //Have count be put in the slot of the actual digit
        for (i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
        }

        for (i = sortList.Count - 1; i >= 0; i--)
        {
            int OutputHash = count[(sortList[i] / exp) % 10] - 1;
            output[OutputHash] = sortList[i];

            int CountHash = (sortList[i] / exp) % 10;

            count[CountHash]--;

        }

        //Copy it to the sort list
        for (i = 0; i < sortList.Count; i++)
        {
            sortList[i] = output[i];
        }

        //algVisualizer.multipleLists = false;

    }

    public static string RadixSortTime(List<int> sortList)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        //Get the max number
        int max = maxInt(sortList);

        //Do this for every single digit spot for the max number
        for (int exp = 1; max / exp > 0; exp *= 10)
        {
            CountSortTime(sortList, exp);
        }


        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency).ToString();//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero

    }


}

