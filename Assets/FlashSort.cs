using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//http://guihaire.com/code/?p=552


public class FlashSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = FlashSortF;
    }

    //Change from 3
    public List<int> FlashSortF(List<int> sortList)
    {
        StartCoroutine(FlashFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        ""
        };

    IEnumerator FlashFuncStart(List<int> sortList)
    {
        //var n:int = a.length;
        //var i:int = 0, j:int = 0, k:int = 0, t:int;
        int k = 0;
        int j = 0;
        int i = 0;
        int t;
        int m = (int)(sortList.Count * .125);
        List<int> list2 = new List<int>();
        for (i = 0; i < m; i++) list2.Add(0);
        int sortListMin = sortList[0];
        int maxIndex  = 0;
        int move = 0;

        List<string> names = new List<string>();
        algVisualizer.lists = new List<List<int>>();

        algVisualizer.lists.Add(sortList);
        names.Add("Original List");

        algVisualizer.lists.Add(list2);
        names.Add("Second List");

        algVisualizer.multipleLists = true;
        algVisualizer.fixVisualization(algVisualizer.lists,names);

        //Get the min number and index of the max number in the list
        for (i = 1; i < sortList.Count; i++)
        {
            if (sortList[i] < sortListMin) sortListMin = sortList[i];
            if (sortList[i] > sortList[maxIndex]) maxIndex = i;
        }

        //If the min equals the max the list is already sorted, you can quit
        if (sortListMin == sortList[maxIndex]) yield break;

        int c1 = (m - 1) / (sortList[maxIndex] - sortListMin);

        for (i = 0; i < sortList.Count; i++)
        {
            k = (int)(c1 * (sortList[i] - sortListMin));
            Debug.Log(k);
            list2[k]++;

            UpdateVisualizer(algVisualizer.lists, "assign", k, 1, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null"}, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        for (k = 1; k < m; k++)
        {
            list2[k] += list2[(int)(k - 1)];

            UpdateVisualizer(algVisualizer.lists, "assign", k, 1, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", "Null"}, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        int hold = sortList[maxIndex];
        sortList[maxIndex] = sortList[0];

        UpdateVisualizer(algVisualizer.lists, "assign", maxIndex, 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 0, algText);
        algVisualizer.fixVisualization(algVisualizer.lists, names);
        yield return new WaitUntil(() => algVisualizer.continueGoing);

        sortList[0] = hold;

        UpdateVisualizer(algVisualizer.lists, "assign", 0, 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 0, algText);
        algVisualizer.fixVisualization(algVisualizer.lists, names);
        yield return new WaitUntil(() => algVisualizer.continueGoing);

        int flash;
        j = 0;
        k = (int)(m - 1);
        i = (int)(sortList.Count - 1);

        while (move < i)
        {
            while (j > (list2[k] - 1))
            {
                k = (int)(c1 * (sortList[(int)(++j)] - sortListMin));
            }

            flash = sortList[j];

            while (!(j == list2[k]))
            {
                k = (int)(c1 * (flash - sortListMin));
                hold = sortList[(t = (int)(list2[k] - 1))];
                sortList[t] = flash;

                UpdateVisualizer(algVisualizer.lists, "assign", t, 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, 0, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);

                flash = hold;

                list2[k]--;

                UpdateVisualizer(algVisualizer.lists, "assign", k, 1, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, 0, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);

                move++;

            }
        }

        for (j = 1; j < sortList.Count; j++)
        {
            hold = sortList[j];
            i = (int)(j - 1);
            while (i >= 0 && sortList[i] > hold)
            {
                sortList[(int)(i + 1)] = sortList[(int)(i--)];

                UpdateVisualizer(algVisualizer.lists, "assign", (i+1), 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 0, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
            sortList[(int)(i + 1)] = hold;

            UpdateVisualizer(algVisualizer.lists, "assign", (i + 1), 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 0, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        algVisualizer.finish();

    }


}

