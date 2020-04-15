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
    }

    //Change from 3
    public List<int> RadixSortF(List<int> sortList)
    {
        StartCoroutine(RadixFuncStart(sortList, 33));
        //MergeFunc(sortList);
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        ""
        };

    //Returns the maximum int in a list
    int maxInt(List<int> sortList)
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

    IEnumerator RadixFuncStart(List<int> sortList, int n)
    {
        //Get the max number
        int max = maxInt(sortList);
        //Do this for every single digit spot for the max number
        for (int exp = 1;max/exp > 0; exp *= 10)
        {
            yield return StartCoroutine(CountSort(sortList, exp));
            algVisualizer.fixVisualization(sortList);
            UpdateVisualizer("none", 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 0, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        algVisualizer.finish();

    }


    IEnumerator CountSort(List<int> sortList, int exp)
    {
        Debug.Log("Here");
        List<int> output = new List<int>();//Will hold the array sorted for the current digit
        for (int i = 0; i < sortList.Count; i++)
        {
            output.Add(0);//init output to size of sortlist
        }
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


        for (int i = 0; i < 10; i++)
        {
            count.Add(0);//Init count to all zeroes
            //UpdateVisualizer(algVisualizer.lists, "assign", i, 2, 0, 0, new string[] { "i", 0.ToString(),"exp",exp.ToString()}, 0, algText);
            //algVisualizer.fixVisualization(algVisualizer.lists, names);
            //yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //Count the location where each digit would be hashed into
        for(int i = 0; i < sortList.Count; i++)
        {
            count[(sortList[i] / exp) % 10]++;
            UpdateVisualizer(algVisualizer.lists, "assign", (sortList[i] / exp) % 10, 2, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //Have count be put in the slot of the actual digit
        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
            UpdateVisualizer(algVisualizer.lists, "assign", i, 2, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }
        Debug.Log("Here");
        for (int i = sortList.Count - 1; i >= 0; i--)
        {
            Debug.Log("Output");
            int OutputHash = count[(sortList[i] / exp) % 10] - 1;
            Debug.Log(count[(sortList[i] / exp) % 10] - 1);
            output[OutputHash] = sortList[i];
            Debug.Log("here");
            UpdateVisualizer(algVisualizer.lists, "none", (OutputHash), 1, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            Debug.Log("Count");
            int CountHash = (sortList[i] / exp) % 10;
            Debug.Log((sortList[i] / exp) % 10 - 1);
            count[CountHash]--;
            UpdateVisualizer(algVisualizer.lists, "assign", CountHash, 2, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //Copy it to the sort list
        for (int i = 0; i < sortList.Count; i++)
        {
            sortList[i] = output[i];
            UpdateVisualizer(algVisualizer.lists, "assign", i, 0, 0, 0, new string[] { "i", 0.ToString(), "exp", exp.ToString() }, 0, algText);
            //algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        //algVisualizer.multipleLists = false;

    }

}

