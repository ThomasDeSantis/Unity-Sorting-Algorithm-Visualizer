using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.geeksforgeeks.org/counting-sort/


public class CountingSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = BucketSortF;
    }

    //Change from 3
    public List<int> BucketSortF(List<int> sortList)
    {
        StartCoroutine(BucketFuncStart(sortList, 33));
        //MergeFunc(sortList);
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        "function bucketSort(array, n) is",
        "   buckets ← new array of n empty lists",
        "   M ← the maximum key value in the array",
        "   for i = 1 to length(array) do",
        "       insert array[i] into buckets[floor(n × array[i] / M)]",
        "   for i = 1 to n do",
        "       sort(buckets[i])",
        "return the concatenation of buckets[1], ...., buckets[n]"
        };

    IEnumerator BucketFuncStart(List<int> sortList, int n)
    {
        List<string> names = new List<string>();
        List<int>[] buckets = new List<int>[n];
        //Init new lists
        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<int>();
        }
        List<int> sortedArray = new List<int>();

        //Make a visualizer list to represent all lists
        algVisualizer.lists = new List<List<int>>();
        algVisualizer.lists.Add(sortList);
        names.Add("Original List");
        for (int i = 0; i < n; i++)
        {
            algVisualizer.lists.Add(buckets[i]);
            string temp = "Bucket " + i.ToString();
            names.Add(temp);
        }
        algVisualizer.multipleLists = true;
        algVisualizer.lists.Add(sortedArray);
        names.Add("Sorted List");

        for (int i = 0; i < sortList.Count; i++)
        {
            int bucket = (sortList[i] / n);
            //Debug.Log(bucket);
            buckets[bucket].Add(sortList[i]);
            UpdateVisualizer(algVisualizer.lists, "assign", n, bucket + 1, 0, 0, new string[] { "n", n.ToString(),
                    "bucket", bucket.ToString(),"i", 0.ToString()}, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        for (int i = 0; i < n; i++)
        {
            if (buckets[i].Count != 0)
            {
                buckets[i].Sort();
                List<int> temp = buckets[i];
                sortedArray.AddRange(temp);
                UpdateVisualizer(algVisualizer.lists, "none", 0, 0, 0, 0, new string[] { "n", n.ToString(),
                    "bucket", 0.ToString(),"i", i.ToString()}, 0, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
        }
        sortList = sortedArray;
    }


}

