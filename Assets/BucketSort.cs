using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.interviewbit.com/tutorial/merge-sort-algorithm/


public class MergeSort : GenericSortingAlg
{
    //Error checking for lists that will go over 250 images
    void Start()
    {
        algVisualizer = GetComponent<Visualizer>();
        algVisualizer.algorithmInQuestion = MergeSortF;
    }

    public List<int> MergeSortF(List<int> sortList)
    {
        StartCoroutine(MergeFuncStart(sortList, 0, sortList.Count - 1));
        //MergeFunc(sortList);
        algVisualizer.beginTime = Time.time;//Start the clock
        return TempStorageList;
    }

    string[] algText = { "", "" };

    /*
    IEnumerator MergeFunc(List<int> A)
    {
        int n = A.Count - 1;
        List<int> B = new List<int>(A);//Copy the list
        for (int width = 1; width < n; width = 2 * width)
        {
            // Array A is full of runs of length width.
            for (int i = 0; i < n; i = i + 2 * width)
            {
                Debug.Log(width);
                Debug.Log("...");
                for (int l = 0; l < B.Count; l++)
                {
                    Debug.Log(B[l]);
                }
                Debug.Log("---");
                for (int l = 0; l < A.Count; l++)
                {
                    Debug.Log(A[l]);
                }
                int iLeft = i;
                int iRight = Mathf.Min(i + width, n);
                int j = iRight;
                int iEnd = Mathf.Min(i+2 * width, n);
                for (int k = iLeft; k < iEnd; k++)
                {
                    // If left run head exists and is <= existing right run head.

                    if (i < iRight && (j >= iEnd || A[i] <= A[j]))
                    {
                        B[k] = A[i];
                        UpdateVisualizer("assign", k, A[i], new string[] { }, 0, algText);
                        //algVisualizer.fixVisualization(B);
                        yield return new WaitUntil(() => algVisualizer.continueGoing);
                        i = i + 1;
                    }
                    else
                    {
                        B[k] = A[j];
                        UpdateVisualizer("assign", k, A[j], new string[] { }, 0, algText);
                        //algVisualizer.fixVisualization(B);
                        yield return new WaitUntil(() => algVisualizer.continueGoing);
                        j = j + 1;
                    }
                }
                //end func
                i = iLeft;
            }
            Debug.Log("...");
            for (int l = 0; l < B.Count; l++)
            {
                Debug.Log(B[l]);
            }
            A = new List<int>(B);//Copy the list
            // Now work array B is full of runs of length 2*width.
            // Copy array B to array A for next iteration.
            // A more efficient implementation would swap the roles of A and B.
            //B.ForEach(Debug.Log();
            // Now array A is full of runs of length 2*width.
        }
        algVisualizer.finish();
        yield return new WaitForSeconds(.01f);
    }
    */

    IEnumerator MergeFuncStart(List<int> A, int startI, int endI)
    {
        yield return StartCoroutine(MergeFunc(A, 0, A.Count - 1));
        algVisualizer.finish();
    }

    IEnumerator MergeFunc(List<int> A, int startI, int endI)
    {
        algVisualizer.fixVisualization(new List<List<int>> { A, A });
        List<int> B = new List<int>(A);//Copy the list

        if (startI < endI)
        {
            int middleI = (startI + endI) / 2;
            yield return StartCoroutine(MergeFunc(A, startI, middleI));
            yield return StartCoroutine(MergeFunc(A, middleI + 1, endI));
            yield return StartCoroutine(Merge(A, B, startI, middleI, endI));
        }
    }

    IEnumerator Merge(List<int> A, List<int> B, int startI, int middleI, int endI)
    {
        // crawlers for both intervals and for temp
        int i = startI;
        int j = middleI + 1;
        int k = 0;

        // traverse both arrays and in each iteration add smaller of both elements in temp 
        while (i <= middleI && j <= endI)
        {
            if (A[i] <= A[j])
            {
                B[k] = A[i];
                UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[i], 0, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 0, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
                k += 1;
                i += 1;
            }
            else
            {
                B[k] = A[j];
                UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[j], 0, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 0, algText);
                yield return new WaitUntil(() => algVisualizer.continueGoing);
                k += 1;
                j += 1;
            }
        }

        // add elements left in the first interval 
        while (i <= middleI)
        {
            B[k] = A[i];
            UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[i], 0, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 0, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
            k += 1;
            i += 1;
        }

        // add elements left in the second interval 
        while (j <= endI)
        {
            B[k] = A[j];
            UpdateVisualizer(new List<List<int>> { A, B }, "assign", k, 1, A[j], 0, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 0, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
            k += 1;
            j += 1;
        }

        // copy temp to original interval
        for (i = startI; i <= endI; i += 1)
        {
            A[i] = B[i - startI];
            UpdateVisualizer(new List<List<int>> { A, B }, "assign", i, 0, B[i - startI], 0, new string[] { "startI", startI.ToString(), "middleI", middleI.ToString(), "endI", endI.ToString(), "i", i.ToString(), "j", j.ToString(), "k", k.ToString() }, 0, algText);
            yield return new WaitUntil(() => algVisualizer.continueGoing);
        }
    }

}

