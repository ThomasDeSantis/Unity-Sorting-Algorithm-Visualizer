using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://en.wikipedia.org/wiki/Bucket_sort


public class BucketSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = BucketSortF;
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Change from 3
    public List<int> BucketSortF(List<int> sortList)
    {
        StartCoroutine(BucketFuncStart(sortList, 34));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        "function bucketSort(array, n) is\n",
        "   buckets ← new array of n empty lists\n",
        "   sortedList = empty list\n",
        "   for i = 1 to length(array) do\n",
        "       bucket = floor(array[i]] / n)\n",
        "       insert array[i] into buckets[bucket]\n",
        "   for i = 1 to n do\n",
        "       sort(buckets[i])\n",
        "       concatenate sortedList and buckets[i]\n",
        "return sortedList\n"
        };

    string descText = "Bucket sort:\n\nDescription: Bucket sort distrubtes each element in an list into 'buckets', which are their own seperate array. The smallest elements are put into the smallest bucket, and you increase the bucket number the larger the element is. This creates several smaller lists, which you then sort with insertion sort or your algorithm of choice. Due to each bucket being much smaller it is linear in time despite insertion sort not being linear.\n\nUse: This algorithm can be done in linear time, with linear being n + k where k is the number of buckets. This can make it very efficient in some cases.\n\nWorst Case Time Complexity:   O(n^2)\nAverage Case Time Complexity: Θ(n + k)\nBest Case Time Complexity:    Ω(n + k)\nSpace Complexity:             O(n + k)";

    IEnumerator BucketFuncStart(List<int> sortList, int n)
    {
        List<string> names = new List<string>();
        List<int>[] buckets = new List<int>[n];
        //Init new lists
        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<int>();

            UpdateAlgLine(1, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }
        }

        List<int> sortedArray = new List<int>();

        UpdateAlgLine(2, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }


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
            UpdateAlgLine(3,3, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            int bucket = (sortList[i] / n);

            UpdateAlgLine(4, 3, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            buckets[bucket].Add(sortList[i]);
            UpdateVisualizer(algVisualizer.lists, "assign", n,bucket + 1, 0, 0, new string[] { "n", n.ToString(),
                    "bucket", bucket.ToString(),"i", 0.ToString()}, 5, algText);
            algVisualizer.fixVisualization(algVisualizer.lists,names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        for (int i = 0; i < n; i++)
        {

            UpdateAlgLine(6, 3, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            if (buckets[i].Count != 0)
            {
                buckets[i].Sort();
                UpdateAlgLine(7, buckets[i].Count * buckets[i].Count, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                List<int> temp = buckets[i];
                sortedArray.AddRange(temp);
                UpdateVisualizer(algVisualizer.lists, "none", 0, 0, 0, 0, new string[] { "n", n.ToString(),
                    "bucket", 0.ToString(),"i", i.ToString()}, 8, algText);
                algVisualizer.fixVisualization(algVisualizer.lists,names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
        }
        UpdateAlgLine(9, new string[] { "n", n.ToString(),
                    "bucket", "Null","i", 0.ToString()}, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        sortList = sortedArray;
        algVisualizer.fixVisualization(sortList);
        algVisualizer.finish();

    }

    public static string BucketSortTime(List<int> sortList,int n)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        List<int>[] buckets = new List<int>[n];
        //Init new lists
        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<int>();
        }

        List<int> sortedArray = new List<int>();

        for (int i = 0; i < sortList.Count; i++)
        {

            int bucket = (sortList[i] / n);

            buckets[bucket].Add(sortList[i]);
        }

        for (int i = 0; i < n; i++)
        {

            if (buckets[i].Count != 0)
            {
                buckets[i].Sort();

                List<int> temp = buckets[i];
                sortedArray.AddRange(temp);
            }
        }
        sortList = sortedArray;

        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency).ToString();//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero

    }

}

