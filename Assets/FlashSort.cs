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
        algVisualizer.changeTextSize(15);//Long alg text
        algVisualizer.algDescription.text = descText;
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Change from 3
    public List<int> FlashSortF(List<int> sortList)
    {
        StartCoroutine(FlashFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
            "procedure flashSort(A : list of sortable items )\n",
            "    k, j, i,maxIndex,move,flash = 0\n",
            "    m = floor(Length(A) * .125)\n",
            "    B = list of all zeroes of size m\n",
            "    min = minimum of A, maxIndex = index of max of A\n",
            "    c1 = (m - 1) / (A[maxIndex] - min)\n",
            "    for (i = 0; i < Length(A); i++)\n",
            "        k = floor(c1 * (A[i] - min))\n",
            "        B[k] = B[k] + 1\n",
            "    for (k = 1; k < m; k++)\n",
            "        B[k] = B[k - 1] + B[k]\n",
            "    hold = A[maxIndex]\n",
            "    A[maxIndex] = A[0]\n",
            "    A[0] = hold\n",
            "    j = 0\n",
            "    k = floor(m - 1)\n",
            "    i = Length(A) - 1\n",
            "    while (move < 1)\n",
            "        while (j > B[k] - 1))\n",
            "            k = floor(c1 * (A[(++j)] - min));\n",
            "        flash  = A[j]\n",
            "        while(j != B[k])\n",
            "            k = floor(c1 * (flash - min))\n",
            "            hold = A[(t = B[k] - 1)]\n",
            "            A[t] = flash\n",
            "            flash = hold\n",
            "            B[k] = B[k] - 1\n",
            "            move = move + 1\n",
            "    for (j = 1; j < Length(A))\n",
            "        hold = A[j]\n",
            "        i = j - 1\n",
            "        while (i >= 0 && A[i] > hold)\n",
            "            A[i+1] = A[i = i - 1]\n",
            "        A[i+1] = hold\n",
            "end procedure"
        };

    string descText = "Flash sort:\n\nDescription: Flash sort is a sorting algorithm that depends on knowing the distribution of elements. It is very efficient if you provide it a dataset that fits the expected distribution and range, for this program you can use the 'Tight' distribution. Flashsort can 'guess' the position of an element based on its size and the distribution of the list. The more accurate the guesses are the faster it is. So among an evenly distributed dataset of a certain range it will work very well.\n\nUse: Flash sort is very efficient, but only on datasets that are evenly distributed and that you know the range to.\n\nWorst Case Time Complexity:   O(n^2)\nAverage Case Time Complexity: Θ(n)\nBest Case Time Complexity:    Ω(n)\nSpace Complexity:             O(n)";

    IEnumerator FlashFuncStart(List<int> sortList)
    {
        int k = 0;
        int j = 0;
        int i = 0;
        int flash = 0;
        int maxIndex = 0;
        int move = 0;
        int t;

        UpdateAlgLine(1, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", "Null"
                , "min", "Null", "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", "Null", "hold", "Null", "flash", flash.ToString()}, algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        int m = (int)(sortList.Count * .125);

        UpdateAlgLine(2, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", "Null", "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", "Null", "hold", "Null"}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        List<int> list2 = new List<int>();
        for (i = 0; i < m; i++)
        {
            list2.Add(0);
            addActions(2);
        }

        UpdateAlgLine(3, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", "Null", "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", "Null", "hold", "Null", "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        int sortListMin = sortList[0];
        addActions(1);

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
            addActions(4);//Approximately 4 actions are done here through the for loop
        }

        UpdateAlgLine(4, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", "Null", "hold", "Null", "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        //If the min equals the max the list is already sorted, you can quit
        if (sortListMin == sortList[maxIndex]) yield break;

        int c1 = (m - 1) / (sortList[maxIndex] - sortListMin);

        UpdateAlgLine(5, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        for (i = 0; i < sortList.Count; i++)
        {

            UpdateAlgLine(6, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            k = (int)(c1 * (sortList[i] - sortListMin));

            UpdateAlgLine(7,4, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            list2[k]++;

            UpdateVisualizer(algVisualizer.lists, "assign", k, 1, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", flash.ToString()}, 8, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        for (k = 1; k < m; k++)
        {

            UpdateAlgLine(9, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            list2[k] += list2[(int)(k - 1)];

            UpdateVisualizer(algVisualizer.lists, "assign", k, 1, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", "Null", "flash", flash.ToString()}, 10, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        int hold = sortList[maxIndex];

        UpdateAlgLine(11, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        sortList[maxIndex] = sortList[0];

        UpdateVisualizer(algVisualizer.lists, "assign", maxIndex, 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 12, algText);
        algVisualizer.fixVisualization(algVisualizer.lists, names);
        yield return new WaitUntil(() => algVisualizer.continueGoing);

        sortList[0] = hold;

        UpdateVisualizer(algVisualizer.lists, "assign", 0, 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, 13, algText);
        algVisualizer.fixVisualization(algVisualizer.lists, names);
        yield return new WaitUntil(() => algVisualizer.continueGoing);

        //int flash;
        j = 0;

        UpdateAlgLine(14, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        k = (int)(m - 1);

        UpdateAlgLine(15, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        i = (int)(sortList.Count - 1);

        UpdateAlgLine(16, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        while (move < i)
        {

            UpdateAlgLine(17, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            while (j > (list2[k] - 1))
            {

                UpdateAlgLine(18, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                k = (int)(c1 * (sortList[(int)(++j)] - sortListMin));

                UpdateAlgLine(19,7, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

            }

            flash = sortList[j];

            UpdateAlgLine(20, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            while (!(j == list2[k]))
            {

                UpdateAlgLine(21, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                k = (int)(c1 * (flash - sortListMin));

                UpdateAlgLine(22, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                hold = sortList[(t = (int)(list2[k] - 1))];

                UpdateAlgLine(23, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                sortList[t] = flash;

                UpdateVisualizer(algVisualizer.lists, "assign", t, 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, 24, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);

                flash = hold;

                UpdateAlgLine(25, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                list2[k]--;

                UpdateVisualizer(algVisualizer.lists, "assign", k, 1, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, 26, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);

                move++;

                UpdateAlgLine(27, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", t.ToString(), "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }
            }
        }

        for (j = 1; j < sortList.Count; j++)
        {

            UpdateAlgLine(28, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            hold = sortList[j];

            UpdateAlgLine(29, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            i = (int)(j - 1);

            UpdateAlgLine(30, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            while (i >= 0 && sortList[i] > hold)
            {

                UpdateAlgLine(31, new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "min", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", flash.ToString()}, algText);
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                sortList[(int)(i + 1)] = sortList[(int)(i--)];

                UpdateVisualizer(algVisualizer.lists, "assign", (i+1), 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 32, algText);
                algVisualizer.fixVisualization(algVisualizer.lists, names);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
            }
            sortList[(int)(i + 1)] = hold;

            UpdateVisualizer(algVisualizer.lists, "assign", (i + 1), 0, 0, 0,
                new string[] { "i", i.ToString(), "j", j.ToString(), "k", k.ToString(), "t", "Null", "m", m.ToString()
                , "sortListMin", sortListMin.ToString(), "maxIndex", maxIndex.ToString(), "move", move.ToString()
                , "c1", c1.ToString(), "hold", hold.ToString(), "flash", "Null"}, 33, algText);
            algVisualizer.fixVisualization(algVisualizer.lists, names);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }

        algVisualizer.finish();

    }

    public static float FlashSortTime(List<int> sortList)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        int k = 0;
        int j = 0;
        int i = 0;
        int flash = 0;
        int maxIndex = 0;
        int move = 0;
        int t;
        int m = (int)(sortList.Count * .125);
        List<int> list2 = new List<int>();
        for (i = 0; i < m; i++) list2.Add(0);
        int sortListMin = sortList[0];

        //Get the min number and index of the max number in the list
        for (i = 1; i < sortList.Count; i++)
        {
            if (sortList[i] < sortListMin) sortListMin = sortList[i];
            if (sortList[i] > sortList[maxIndex]) maxIndex = i;
        }

        //If the min equals the max the list is already sorted, you can quit
        if (sortListMin == sortList[maxIndex])
        {

            stopwatch.Stop();
            return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero
        }

        int c1 = (m - 1) / (sortList[maxIndex] - sortListMin);

        for (i = 0; i < sortList.Count; i++)
        {
            k = (int)(c1 * (sortList[i] - sortListMin));
            list2[k]++;
        }

        for (k = 1; k < m; k++)
        {
            list2[k] += list2[(int)(k - 1)];
        }

        int hold = sortList[maxIndex];
        sortList[maxIndex] = sortList[0];
        sortList[0] = hold;

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

                flash = hold;

                list2[k]--;

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
            }
            sortList[(int)(i + 1)] = hold;
        }

        stopwatch.Stop();
        return ((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);//Calculates micro seconds by ticks,otherwise rounding errors will cause it to equal zero
    }


}

