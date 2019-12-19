using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.geeksforgeeks.org/heap-sort/


public class HeapSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = HeapSortF;
    }

    public List<int> HeapSortF(List<int> sortList)
    {
        StartCoroutine(HeapFuncStart(sortList));
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = {
        "procedure heapSort(A : list of sortable numbers)\n",
        "    heapSize = A.Length\n",
        "    buildMaxHeap(A)\n",
        "    for k = A.length -1 downto 0\n",
        "        swap A[0] with A[k]\n",
        "        A.heapSize = k\n",
        "        maxHeapify(A,0)\n",
        "\n",
        "procedure buildMaxHeap(A)\n",
        "    for i = A.heapSize/2 - 1 downto 0\n",
        " 	    maxHeapify(A,i)\n",
        "\n",
        "procedure maxHeapify(A,i : the head of the heap)\n",
        "    left = 2*i + 1\n",
        "	right = 2*i + 2\n",
        "    largest = i\n",
        "    if left < A.heapSize and A[left] > A[largest]\n",
        "        largest = left\n",
        "    if right < A.heapSize and A[right] > A[largest]\n",
        "        largest = right\n",
        "    if largest is not if\n",
        "        swap A[i] with A[largest]\n",
        "        maxHeapify(A,largest)\n"
    };

    int heapSize;

    IEnumerator HeapFuncStart(List<int> A)
    {
        UpdateAlgLine(0, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", 0.ToString()}, algText);//Start Procedure
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        heapSize = A.Count;

        UpdateAlgLine(1, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//Assign heap size
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        UpdateAlgLine(2, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//Build max heap
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        yield return StartCoroutine(BuildMaxHeap(A));
        //yield return new WaitForSeconds(100);

        UpdateAlgLine(3, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//For loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        for (int k = heapSize - 1; k >= 0; k--)
        {
            Swap(A, 0, k);
            UpdateVisualizer("swap", 0, k, new string[] {"k", k.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, 0, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            heapSize = k;

            UpdateAlgLine(5, new string[] {"k", k.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//heap size = i
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            UpdateAlgLine(6, new string[] {"k", k.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//maxheapify
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
            else { yield return new WaitUntil(() => !algVisualizer.paused); }

            yield return StartCoroutine(MaxHeapify(A, 0));
        }
        algVisualizer.finish();
    }

    IEnumerator BuildMaxHeap(List<int> A)
    {

        UpdateAlgLine(8, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//proc
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }

        UpdateAlgLine(9, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", 0.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//for loop
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        else { yield return new WaitUntil(() => !algVisualizer.paused); }
        for (int i = heapSize / 2 - 1; i >= 0; i--)
        {

            UpdateAlgLine(10, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//max heapify
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

            yield return StartCoroutine(MaxHeapify(A, i));
        }
    }

    IEnumerator MaxHeapify(List<int> A, int i)
    {
        UpdateAlgLine(12, new string[] {"k", 0.ToString(),"left", 0.ToString(), "right", 0.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//proc
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

        int L = 2 * i + 1;

        UpdateAlgLine(13, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", 0.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//left
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

        int R = 2 * i + 2;

        UpdateAlgLine(14, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//right
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

        int largest = i;

        UpdateAlgLine(15, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", largest.ToString(), "heapSize", heapSize.ToString()}, algText);//largest
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

        UpdateAlgLine(16, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", largest.ToString(), "heapSize", heapSize.ToString()}, algText);//if L
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

        if (L < heapSize && A[L] > A[largest])
        {
            largest = L;

            UpdateAlgLine(17, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", largest.ToString(), "heapSize", heapSize.ToString()}, algText);//lar = L
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        }

        UpdateAlgLine(18, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//if R
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        if (R < heapSize && A[R] > A[largest])
        {
            largest = R;
            UpdateAlgLine(19, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//lar = R
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }
        }

        UpdateAlgLine(20, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//lar = R
        if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

        if (largest != i)
        {
            Swap(A, i, largest);
            UpdateVisualizer("swap", i, largest, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", largest.ToString(), "heapSize", heapSize.ToString()}, 21, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);

            UpdateAlgLine(22, new string[] {"k", 0.ToString(),"left", L.ToString(), "right", R.ToString(), "i", i.ToString(),
            "largest", 0.ToString(), "heapSize", heapSize.ToString()}, algText);//max heapify
            if (algVisualizer.algContainer.activeInHierarchy) { yield return new WaitUntil(() => algVisualizer.continueGoing); }

            yield return StartCoroutine(MaxHeapify(A, largest));
        }
    }


}

