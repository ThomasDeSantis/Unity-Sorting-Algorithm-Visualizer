using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// en.wikipedia.org/wiki/Bubble_sort
public class CocktailSort : GenericSortingAlg
{

    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = CocktailSortF;
    }

    public List<int> CocktailSortF(List<int> sortList)
    {
        StartCoroutine(CocktailFunc(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
            "procedure cocktailShakerSort(A : list of sortable items)\n",
            "   swapped = true\n",
            "	while swapped\n",
            "        swapped = false\n",
            "        for i = 0 to length( A ) - 2\n",
            "            if A[ i ] > A[ i + 1 ]\n",
            "                swap( A[ i ], A[ i + 1 ] )\n",
            "                swapped = true\n",
            "        if not swapped then\n",
            "            break while loop\n",
            "        swapped = false\n",
            "        for i = length( A ) - 2 down to 0\n",
            "            if A[ i ] > A[ i + 1 ]\n",
            "                swap( A[ i ], A[ i + 1 ] )\n",
            "                swapped = true\n"
        };

    IEnumerator CocktailFunc(List<int> A)
    {
        UpdateAlgLine(0, new string[] { "swapped", 0.ToString(), "i", 0.ToString() }, algText);//End if
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        bool swapped = true;

        UpdateAlgLine(1, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        UpdateAlgLine(2, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        while (swapped)
        {
            swapped = false;

            UpdateAlgLine(3, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            UpdateAlgLine(4, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            for (int i = 0; i <= A.Count - 2; i++)
            {

                UpdateAlgLine(5, new string[] { "swapped", swapped.ToString(), "i", i.ToString() }, algText);//End if
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                if (A[i] > A[i + 1])
                {
                    Swap(A, i, i + 1);
                    UpdateVisualizer("swap", i, i + 1, new string[] { "swapped", swapped.ToString(), "i", i.ToString() }, 6, algText);
                    yield return new WaitUntil(() => algVisualizer.continueGoing);

                    swapped = true;

                    UpdateAlgLine(7, new string[] { "swapped", swapped.ToString(), "i", i.ToString() }, algText);//End if
                    if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }

                }
            }

            UpdateAlgLine(8, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            if (!swapped)
            {

                UpdateAlgLine(9, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                break;
            }

            swapped = false;

            UpdateAlgLine(10, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            UpdateAlgLine(11, new string[] { "swapped", swapped.ToString(), "i", 0.ToString() }, algText);//End if
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            for (int i = A.Count - 2; i >= 0; i--)
            {

                UpdateAlgLine(12, new string[] { "swapped", swapped.ToString(), "i", i.ToString() }, algText);//End if
                if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                else { yield return new WaitUntil(() => !algVisualizer.paused); }

                if (A[i] > A[i + 1])
                {
                    Swap(A, i, i + 1);
                    UpdateVisualizer("swap", i, i + 1, new string[] { "swapped", swapped.ToString(), "i", i.ToString() }, 13, algText);
                    yield return new WaitUntil(() => algVisualizer.continueGoing);

                    swapped = true;

                    UpdateAlgLine(14, new string[] { "swapped", swapped.ToString(), "i", i.ToString() }, algText);//End if
                    if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
                    else { yield return new WaitUntil(() => !algVisualizer.paused); }

                }
            }
        }
        algVisualizer.finish();
    }

}
