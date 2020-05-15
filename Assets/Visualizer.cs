using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Visualizer : MonoBehaviour {
    private List<VisualizerObject> visualizerObjects;
    private Canvas can;
    [SerializeField]
    public double maxHeight =  550; //Refers to the maximum height of a bar
    [SerializeField]
    public double maxNum = 1000; //Refers to the largest value a single element of the list can be

    public double width;//Will store the standardized width of each bar; 

    [SerializeField]
    public double sideBuffer = 90; // The buffer of space between the list and the side
    public double innerSideBuffer = 25;//The buffer of space between each list

    public bool paused = false; //Used as a boolean check whether or not to continue running the algorithm. If paused it will not continue.
    public bool tempUnpaused = false; //Used a boolean check when the next button is hit. It allows a user to continue for one step even if paused.
    public bool continueGoing = false; //Used specifically to check whether the visualizer can continue running (while not in algorithm mode)

    public float beginTime = -1f;//Will hold the ticker that tells the visualizer it can do another step. -1 represents the visualizer not starting yet.
    public float delayTime;//Will hold how long to wait before continuing after each step

    List<int> numList;//Will hold the list of elements that will be sorted.

    public int numElements = 10;//Holds how many elements will be sorted. By default 10.
    public InputField numElementInputField;//Input field that takes an integer, will set numElements equal to this field after pressing the confirm button.
    public InputField manualInputField;//Input field that takes integers seperated by a comma
    public Slider speedSlider;//A slider that will adjust speed as you move it to the right or left.


    public GameObject algContainer;//Holds the algorithm view
    public GameObject Bar;//Will hold the template for the bar
    Vector3 algContainerVector;//Holds the coordinates of the alg container
    public Text algText;//Will hold the text that holds the pseudocode of the algorithm
    Vector3 algTextVector;//Will hold the coordinates of algText
    public Text algDescription;//Will hold the description of the algorthm
    Vector3 algDescriptionVector;//Will hold the coordinates of algDescription
    public Button algHalfButton;//When pressed will half the size of the algorithm container, so you can see both the list and pseudocode.
    Vector3 algHalfButtonVector;//Will hold coordinates of the half button
    public Button algButton;//Reveals the algorithm view
    public Text variableText;//Holds the text for the variable window
    public Button PauseButton;//Pauses the visualization
    bool half = false;//Will be set to true if set to half view
    public Text comparisonText;//Will hold the text that shows assignments and actions
    public Text speedCompText;//Will hold the text that compares speed

    public bool multipleLists = false;//Boolean check that checks if multiple lists are being visualized.
    public List<List<int>> lists;//Will hold multiple lists of ints (if multiple lists are being used)
    public List<GameObject> textboxes;//Will hold the textboxes renderered below the multiple lists

    public delegate List<int> AlgStart(List<int> sortList);//Whichever sorting algorithm is running will set this to be the relevant function's starting algorithm
    public AlgStart algorithmInQuestion;

    List<int> copy;//Will hold a copy of the original list

    private int screenWidth = 1366;//Default width/height
    private int screenHeight = 768;

    private float scaleFactor;//Scales the width and height for when it is a different resolution

    public int numArrays = 1;//Default number of lists
    public int currentArray = 0;//Current array indexs from 0


    //When this function is run, the color of all bars is set to white
    public void WhiteOutAllBars()
    {
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].GetComponent<Image>().color = Color.white;
    }

    //When the confirm button is pressed, it will set the number of elements to however many is stated on number of elements input field
    public void confirmNumElements()
    {
        setNumElements();
    }

    //Sets the number of elements when called.
    private void setNumElements()
    {
        
        if (numElementInputField.text != "")
        {
            //If its not empty, parse the input field for a number
            numElements = int.Parse(numElementInputField.text);
        }
        else
        {
            //If it is empty, set it to 10, the default
            numElements = 10;
        }
        if (numElements == 0)
        {
            //There cannot be 0 elements, so set it to 1.
            numElements = 1;
            numElementInputField.text = "1";
        }
        else if(numElements > 250)
        {
            //There cannot be more than 250 elements
            //There is no hard limit but during testing I found after 250 or so the bars become so small
            //That the point of visualization has become meaningless.
            numElements = 250;
            numElementInputField.text = "250";
        }
        //Once this is done randomize the list
        rerollHeight();
    }

    //Called right as the scene starts from Start()
    //Generates an initial list.
    private void initList()
    {
        numList = new List<int>(numElements);
        for (int i = 0; i < numElements; i++) numList.Add(Random.Range(0, 1000));
        copy = new List<int>(numList);
    }


    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "Sort")
        {
            Debug.Log(SortName.sortMethod);//Log the algorithm type

            //The hub menu passes it a string representing an algorithm name
            //It then adds the relevant algorithm to the scene
            switch (SortName.sortMethod)
            {
                case "BubbleSort":
                    gameObject.AddComponent<BubbleSort>();
                    break;
                case "CocktailSort":
                    gameObject.AddComponent<CocktailSort>();
                    break;
                case "CombSort":
                    gameObject.AddComponent<CombSort>();
                    break;
                case "HeapSort":
                    gameObject.AddComponent<HeapSort>();
                    break;
                case "InsertionSort":
                    gameObject.AddComponent<InsertionSort>();
                    break;
                case "Mergesort":
                    gameObject.AddComponent<Mergesort>();
                    break;
                case "QuickSort":
                    gameObject.AddComponent<QuickSort>();
                    break;
                case "ShellSort":
                    gameObject.AddComponent<ShellSort>();
                    break;
                case "FlashSort":
                    gameObject.AddComponent<FlashSort>();
                    break;
                case "BogoSort":
                    gameObject.AddComponent<BogoSort>();
                    break;
                case "RadixSort":
                    gameObject.AddComponent<RadixSort>();
                    break;
                case "BucketSort":
                    gameObject.AddComponent<BucketSort>();
                    break;
                default:
                    Debug.Log("Incorrect sort method name.");
                    break;
            }
        }
        //Store the scaleFactor so the UI doesnt break
        can = GetComponentInParent<Canvas>();
        scaleFactor = can.scaleFactor;

        maxHeight *= scaleFactor;//The max height * the scale factor is the correct max height

        visualizerObjects = new List<VisualizerObject>();

        delayTime = .35f;//The delay between each time the visualizer does something. Set to .35 seconds

        initList();//Fill the visualizer with a list.

        fixVisualization();//Fix the visualization to represent the list

        //Store the default position of each game object.
        algContainerVector = algContainer.transform.position;
        algTextVector = algText.transform.position;
        algHalfButtonVector = algHalfButton.transform.position;
        algTextVector = algText.transform.position;
        algDescription.resizeTextForBestFit = true;
	}

    bool runningLock = false;//startAlg sets a running lock that only allows a new alg to start if its finished
    public void startAlg()
    {
        //As long as the visualizer isnt already running
        if (!runningLock)
        {
            WhiteOutAllBars();//Set all bars to white. (May be dark green if previous visualization finished)
            runningLock = true;//Lock the start button
            algorithmInQuestion(numList);//Begin the algorithm
        }
    }

    //Generate a new bar
    //Run when the visualizer is trying to represent an element
    //But not enough bars exist
    //This will return a new bar
    private GameObject genBar()
    {
        GameObject temp = GameObject.Instantiate(Bar);
        temp.transform.SetParent(this.transform);
        temp.transform.SetAsFirstSibling();//Make sure the list doesnt overlap over algorithm view
        return temp;
    }
    
    //Fixes the visualization
    public void fixVisualization()
    {
        WhiteOutAllBars();//First white out all the bars

        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].delNum();//Remove all elements and textboxes
        for (int i = 0; i < textboxes.Count; i++) textboxes[i].GetComponent<Text>().text = "";
        double currentXLocation = 0;//Initialize a location counter to 0
        currentXLocation += sideBuffer; //Do not count the area set aside for the screen side buffers
        calcWidth();//Calculate the width of each bar. Stored in the width variable.
        if (half && algContainer.activeInHierarchy) currentXLocation += (screenWidth / 2.0f);//If its set to half view, you should change the x location to match that.
        maxNum = findMaxNum();//Find the maximum number, as that will be the max height element
        for (int i = 0; i < visualizerObjects.Count;i++) visualizerObjects[i].GetComponent<Image>().enabled = false; //Disable all of them, assume all will not be used
        for (int i = 0; i < numElements; i++)
        {
            //For every element...
            if(visualizerObjects.Count == i)//If there are not enough bars, create a new one
            {
                visualizerObjects.Add(genBar().GetComponent<VisualizerObject>());
            }
            //Set the position
            visualizerObjects[i].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation * scaleFactor, 30);

            //Based on the max height, and the element it is representing, set the height accordingly
            double ratio = maxHeight / maxNum; 
            double height = (int)(ratio * (double)numList[i]);
            visualizerObjects[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(((float)width)*scaleFactor, (float)height);
            visualizerObjects[i].GetComponent<Image>().enabled = true;
            visualizerObjects[i].changeNum(numList[i], (float)currentXLocation*scaleFactor, 30 , (float)width*scaleFactor, (float)height);
            currentXLocation += width;//Set the x location to be adjacent (to the right) of the prior bar
        }
    }

    //This does the same as the prior fixVisualization function
    //Except it changes the visualization to represent a given list.
    public void fixVisualization(List<int> visList)
    {
        WhiteOutAllBars();
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].delNum();
        for (int i = 0; i < textboxes.Count; i++) textboxes[i].GetComponent<Text>().text = "";
        double currentXLocation = 0;
        currentXLocation += sideBuffer;
        calcWidth(visList);
        if (half && algContainer.activeInHierarchy) currentXLocation += (screenWidth / 2.0f);
        maxNum = findMaxNum(visList);
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].GetComponent<Image>().enabled = false;
        //The main difference for this function is that rather than using the default numList
        //You use the given visList list.
        for (int i = 0; i < visList.Count; i++)
        {
            if (visualizerObjects.Count < i)
            {
                visualizerObjects.Add(genBar().GetComponent<VisualizerObject>());
            }
            visualizerObjects[i].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation * scaleFactor, 30);
            double ratio = maxHeight / maxNum; 
            double height = (int)(ratio * (double)visList[i]);
            visualizerObjects[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2((float)width * scaleFactor, (float)height);
            visualizerObjects[i].GetComponent<Image>().enabled = true;
            visualizerObjects[i].changeNum(visList[i], (float)currentXLocation * scaleFactor, 30, (float)width * scaleFactor, (float)height);
            currentXLocation += width;
        }
    }

    //Used to display given multiple lists
    public void fixVisualization(List<List<int>> visLists)
    {
        WhiteOutAllBars();
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].delNum();
        for (int i = 0; i < textboxes.Count; i++) textboxes[i].GetComponent<Text>().text = "";
        double currentXLocation = 0;
        int currentImage = 0;
        currentXLocation += sideBuffer;
        calcWidth(visLists);
        if (half && algContainer.activeInHierarchy) currentXLocation += (screenWidth / 2.0f);
        maxNum = findMaxNum(visLists);
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].GetComponent<Image>().enabled = false;
        //For each list you are given
        for (int i = 0; i < visLists.Count; i++)
        {
           
            if (visLists[i].Count != 0)//Only do so on lists with elements
            {
                for (int j = 0; j < visLists[i].Count; j++)
                {
                    if (visualizerObjects.Count == currentImage)
                    {
                        visualizerObjects.Add(genBar().GetComponent<VisualizerObject>());
                    }
                    visualizerObjects[currentImage].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation * scaleFactor, 30);
                    double ratio = maxHeight / maxNum; 
                    double height = (int)(ratio * (double)visLists[i][j]);
                    visualizerObjects[currentImage].GetComponent<Image>().rectTransform.sizeDelta = new Vector2((float)width * scaleFactor, (float)height);
                    visualizerObjects[currentImage].GetComponent<Image>().enabled = true;
                    visualizerObjects[currentImage].changeNum(visLists[i][j], (float)currentXLocation * scaleFactor, 30, (float)width * scaleFactor, (float)height);
                    currentXLocation += width;
                    currentImage++;
                }
                currentXLocation += innerSideBuffer;//Add the space of innerSideBuffer between each list
            }
        }
    }

    //Used to display given multiple lists
    //Used when you want to display names under the lists
    public void fixVisualization(List<List<int>> visLists, List<string> names)
    {
        WhiteOutAllBars();
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].delNum();
        for (int i = 0; i < textboxes.Count; i++) textboxes[i].GetComponent<Text>().text = "";
        double currentXLocation = 0;
        int currentImage = 0;
        int listCount = 0;
        currentXLocation += sideBuffer; //Do not count the area set aside for the screen side buffers
        calcWidth(visLists);
        if (half && algContainer.activeInHierarchy) currentXLocation += (screenWidth / 2.0f);
        maxNum = findMaxNum(visLists);
        for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].GetComponent<Image>().enabled = false; //Disable all of them, assume all will not be used
        for (int i = 0; i < visLists.Count; i++)
        {
            if (visLists[i].Count != 0)
            {
                //Create textboxes if you need them
                if (textboxes.Count <= listCount)
                {
                    textboxes.Add(new GameObject("bottomText"));
                    textboxes[listCount].transform.SetParent(this.transform);
                    textboxes[listCount].AddComponent<Text>();
                    textboxes[listCount].GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    textboxes[listCount].GetComponent<Text>().fontSize = 12;
                }
                double begin = currentXLocation;//Get the beginning to properly place the text
                for (int j = 0; j < visLists[i].Count; j++)
                {
                    if (visualizerObjects.Count == currentImage)
                    {
                        visualizerObjects.Add(genBar().GetComponent<VisualizerObject>());
                    }
                    visualizerObjects[currentImage].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation * scaleFactor, 30);
                    double ratio = maxHeight / maxNum;
                    double height = (int)(ratio * (double)visLists[i][j]);
                    visualizerObjects[currentImage].GetComponent<Image>().rectTransform.sizeDelta = new Vector2((float)width * scaleFactor, (float)height);
                    visualizerObjects[currentImage].GetComponent<Image>().enabled = true;
                    visualizerObjects[currentImage].changeNum(visLists[i][j], (float)currentXLocation * scaleFactor, 30, (float)width * scaleFactor, (float)height);
                    currentXLocation += width;
                    currentImage++;
                }
                double end = currentXLocation;//Get the end to properly place the text
                currentXLocation += innerSideBuffer;
                //Set the textbox under the given list, with the matching string
                textboxes[listCount].GetComponent<Text>().transform.position = new Vector3(((float)((end + begin + innerSideBuffer)/2.0f))*scaleFactor, 10);
                textboxes[listCount].GetComponent<Text>().rectTransform.sizeDelta = new Vector2((float)(end - begin + innerSideBuffer)*scaleFactor, 20.0f);
                textboxes[listCount].GetComponent<Text>().text = names[i];
                listCount++;
            }
        }
    }


    //Find the maximum element for numList
    private int findMaxNum()
    {
        int currentMax = numList[0];
        for (int i = 1; i < numElements; i++)
        {
            if (currentMax < numList[i]) currentMax = numList[i];
        }
        return currentMax;
    }

    //Find the maximum element for a given list
    private int findMaxNum(List<int> visList)
    {
        int currentMax = visList[0];
        for (int i = 1; i < visList.Count; i++)
        {
            if (currentMax < visList[i]) currentMax = visList[i];
        }
        return currentMax;
    }

    //Find the maximum element given multiple lists
    private int findMaxNum(List<List<int>> visLists)
    {
        int currentMax = visLists[0][0];
        for(int i = 0; i < visLists.Count; i++)
        {
            for (int j = 0; j < visLists[i].Count; j++)
            {
                if (currentMax < visLists[i][j]) currentMax = visLists[i][j];
            }
        }
        return currentMax;
    }

    //Calculate the width for the standard numList
    private void calcWidth()
    {
        if (half && algContainer.activeInHierarchy) width = screenWidth / 2;//Account for the half view if it is selected
        else { width = screenWidth; }
        width = width - (sideBuffer * 2);//Side buffer is the size on each side between the edge of screen and beginning of first/last bar
        width = (width / (double)numElements);//You can get the width by dividing the remaining space by the number of elements
        
    }

    //Do the same as the previous function, but instead for a given list
    private void calcWidth(List<int> visList)
    {
        if (half && algContainer.activeInHierarchy) width = screenWidth / 2;
        else { width = screenWidth; }
        width = width - (sideBuffer * 2);
        width = (width / (double)visList.Count);

    }

    //Calculate the width given multiple lists
    private void calcWidth(List<List<int>> visLists)
    {
        int allEls = 0;//This counts all  elements in all lists
        int trueCount = 0;//This counts how many viable (non zero element) lists exist in the list of lists
        for (int i = 0; i < visLists.Count; i++)
        {
            if(visLists[i].Count != 0)
            {
                trueCount++;
                allEls += visLists[i].Count;
            }
        }
        if (half && algContainer.activeInHierarchy) width = screenWidth / 2;
        else { width = screenWidth; }
        width = width - (innerSideBuffer * (2 + (trueCount + 4)));//Calculate how many inner side buffer should be use between lists to get the proper width
        width = (width / (double)allEls); //Then divide the altered width for the number of elements in every list
    }


    //Randomly generate a list given the number of elements
    public void rerollHeight()
    {
        if (!runningLock)//Only allow it to reroll if it is not already running
        {
            Dropdown method = GetComponentInChildren<Dropdown>();
            int currentVal;
            //Get the current method for generating the list
            switch (method.value)
            {
                case 0://Default method. Randomly generates each element.
                    numList = new List<int>(numElements);
                    for (int i = 0; i < numElements; i++) numList.Add(Random.Range(0, 1000));
                    break;
                case 1: //Weighted to the left (smallest elements to the left)
                    currentVal = Random.Range(0, 25);
                    numList = new List<int>(numElements);
                    numList.Add(currentVal);
                    for (int i = 1; i < numElements; i++)
                    {
                        currentVal = Mathf.Abs(currentVal + Random.Range(-25, 100));//Add between -25 and 100 to make it appear weighted ,abs to make no negative
                        numList.Add(currentVal);
                    }
                    break;
                case 2: //Weighted to the right (smallest element to the right)
                    currentVal = Random.Range(0, 25);
                    numList = new List<int>(numElements);
                    numList.Add(currentVal);
                    for (int i = 1; i < numElements; i++)
                    {
                        currentVal = Mathf.Abs(currentVal + Random.Range(-25, 100));//Add between -25 and 100 to make it appear weighted ,abs to make no negative
                        numList.Add(currentVal);
                    }
                    numList.Reverse();//Same process as left weighted, except reverse the list
                    break;
                case 3://Manual input
                    string[] elementsString = manualInputField.text.Split(',');
                    numElements = elementsString.Length;
                    if (numElements > 250) numElements = 250;
                    numList = new List<int>(numElements);
                    for (int i = 0; i < numElements; i++) numList.Add(int.Parse(elementsString[i]));
                    break;
                case 4://n+k (Elements are smaller than n, the number of element, *.75)
                    numList = new List<int>(numElements);
                    for (int i = 0; i < numElements; i++) numList.Add(Random.Range(0, (int)(numElements * .75f)));
                    break;
                case 5://even (Every element 0-n rearranged)
                    System.Random rand = new System.Random();
                    List<int> tempList = new List<int>(numElements);
                    for (int i = 0; i < numElements; i++) tempList.Add(i);
                    numList = tempList.OrderBy(x => Random.value).ToList();
                    break;
                case 6://Tight (Useful for flash sort, elements can only be within range of .1*n)
                    numList = new List<int>(numElements);
                    for (int i = 0; i < numElements; i++) numList.Add(Random.Range(0, (int)(numElements * .10f)));
                    break;
                default:
                    break;
            }
            for (int i = 0; i < visualizerObjects.Count; i++) visualizerObjects[i].GetComponent<Image>().color = Color.white;
            copy = new List<int>(numList);
            fixVisualization();
        }
    }

    
    //Temporarily unpauses it.
    //This sets temp unpaused to be true.
    //When the program finds this to be true, it will set temp unpaused and continue going to false at the next step.
    //This ensures the next button only ever does once action
    public void hitNextButton()
    {
        continueGoing = true;
        beginTime = Time.time;
        if (paused)
        {
            tempUnpaused = true;
            paused = false;
        }
    }

    bool algView = true;//True means you are looking at the algorithm pseudocode. False means you are looking at the speed comparison.
    string oldAlgText;

    //This will show how the num list runs on each algorithm
    //This allows the user to compare them
    public void hitSpeedCompButton()
    {
        algView = !algView;//Change whether you are looking at the algorithm or speed comparison
        if (algView)//If you are now viewing at the algorithm pseudocode
        {
            algText.text = oldAlgText;//Change the text back to the pseudocode
            if (!half)
            {
                algDescription.enabled = true;
            }
            speedCompText.text = "Speed Comparison";
        }
        else//Otherwise display the speed comparison
        {
            oldAlgText = algText.text;
            string storageText = "Bubble Sort:{0} μs\nCocktail Sort: {1} μs\nComb Sort: {2} μs\nHeap Sort: {3}" +
                " μs\nInsertion Sort: {4} μs\nMerge Sort:{5} μs\nQuick Sort:{6} μs\nShell Sort:{7} μs\nFlash Sort:{8} μs" +
                "\nBucket Sort:{9} μs\nRadix Sort:{10} μs";
            //Format the text to show each element in microseconds
            algText.text = string.Format(storageText, BubbleSort.BubbleSortTime(new List<int>(copy)),
                CocktailSort.CocktailSortTime(new List<int>(copy)),CombSort.CombSortTime(new List<int>(copy)),
                HeapSort.HeapSortStartTime(new List<int>(copy)), InsertionSort.InsertionSortTime(new List<int>(copy)),
                Mergesort.MergeSortTime(new List<int>(copy), 0, copy.Count - 1),QuickSort.QuickSortStartTime(new List<int>(copy), 0, copy.Count - 1),
                ShellSort.ShellSortTime(new List<int>(copy)), FlashSort.FlashSortTime(new List<int>(copy)),BucketSort.BucketSortTime(new List<int>(copy),34)
                , RadixSort.RadixSortTime(new List<int>(copy)));
            algDescription.enabled = false;
            speedCompText.text = "Algorithm";
        }
    }

    //When you press the pause button, this runs
    //Prevents the visualizer from taking actions
    public void setPause()
    {
        paused = !paused;
        if (paused) PauseButton.GetComponent<Image>().material = (Material)Resources.Load("images/materials/pauseMat", typeof(Material));
        else PauseButton.GetComponent<Image>().material = (Material)Resources.Load("images/materials/playMat", typeof(Material));

    }

    //If the algorithm demands a swap takes place, this will change the list to represent that
    public void swapThem(int indexX, int indexY)
    {
        WhiteOutAllBars();
        int numTemp = visualizerObjects[indexX].num;//Set num temp to hold the element at indexX
        //Exchange the bar sizes, which represents a swap
        Vector2 temp = visualizerObjects[indexX].GetComponent<Image>().rectTransform.sizeDelta;
        visualizerObjects[indexX].GetComponent<Image>().rectTransform.sizeDelta = visualizerObjects[indexY].GetComponent<Image>().rectTransform.sizeDelta;
        visualizerObjects[indexY].GetComponent<Image>().rectTransform.sizeDelta = temp;
        visualizerObjects[indexX].changeNumHeight(visualizerObjects[indexY].num, visualizerObjects[indexX].GetComponent<Image>().rectTransform.sizeDelta.y);
        visualizerObjects[indexY].changeNumHeight(numTemp, visualizerObjects[indexY].GetComponent<Image>().rectTransform.sizeDelta.y);
        //Change the color to green to represent it has been swapped
        visualizerObjects[indexX].GetComponent<Image>().color = Color.green;
        visualizerObjects[indexY].GetComponent<Image>().color = Color.green;
        return;        
    }

    //When the visualizer demands an assignment, change the list with this
    public void assign(int index, int value) 
    {
        WhiteOutAllBars();//White out bars to remove any green bars
        numList[index] = value;//Set the num list to the value
        fixVisualization();//Fix the visualization
        visualizerObjects[index].GetComponent<Image>().color = Color.green;//Set it to green
        return;
    }

    //Assign, but when you have multiple lists
    public void assign(int index, List<List<int>> lists) 
    {
        WhiteOutAllBars();//White out the bars
        if (currentArray == numArrays)
        {
            fixVisualization(lists);
        }
        else
        {
            fixVisualization(lists[currentArray]);
        }
        Debug.Log(index);
        if (visualizerObjects.Count > index)
        {
            visualizerObjects[index].GetComponent<Image>().color = Color.green;
        }
        return;
    }

    //Turns all elements dark green to indicate it has finished
    public void finish()
    {
        for (int i = 0; i < numElements; i++) visualizerObjects[i].GetComponent<Image>().color = new Color(.01f,.50f,.30f);
        tempUnpaused = false;
        paused = false;
        runningLock = false;
        return;
    }

    //Given a list of strings, this will set the variable list.
    //Even numbers contain the name of variables
    //Odd numbers contain the value of the variable
    public void changeVariables(string[] newVarList)
    {
        variableText.text = "Variable List:\n";
        for (int i = 0;i < newVarList.Length; i += 2)
        {
            variableText.text = variableText.text + newVarList[i] + ": " + newVarList[i + 1] + "\n";
        }
        return;
    }

    //Called when using the slider.
    //As you bring it closer to the left/right, it changes the speed exponentially slower/faster
    public void setSpeed()
    {
        delayTime = Mathf.Pow(.35f, speedSlider.value);
    }

    //Shows the algorithm view
    public void AlgEnable()
    {
        if(paused == algContainer.activeInHierarchy)setPause();
        algContainer.SetActive(!algContainer.activeInHierarchy);
        fixVisualization();
    }

    //Turns on the half view, where you can view the pseudocode and the list
    public void AlgHalfView()
    {
        if (!half)//If it is not half view, enable the half view
        {
            half = true;
            algContainer.transform.position = new Vector3(0, algContainer.transform.position.y);
            algText.transform.position = new Vector3((screenWidth / 4.0f)*scaleFactor,algText.transform.position.y);
            algDescription.enabled = false;
            if (multipleLists)
            {
                fixVisualization(lists);
            }
            else
            {
                fixVisualization();
            }
        }
        else//If it is already the half view, then it will switch to the full view
        {
            half = false;
            algContainer.transform.position = algContainerVector;
            algText.transform.position = algTextVector;
            algHalfButton.transform.position = algHalfButtonVector;
            algDescription.enabled = true;
            if (multipleLists)
            {
                fixVisualization(lists);
            }
            else
            {
                fixVisualization();
            }
        }
    }


    
    //Changes the font size
    public void changeTextSize(int size)
    {
        algText.GetComponent<Text>().fontSize = size;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))//Exit the visualizer on hit escape
        {
            SceneManager.LoadScene("Hub", LoadSceneMode.Single);
        }
        //When you press TAB with multiple lists, switch the list you are viewing
        if(numArrays > 1)//TODO: Work on allowing this to work with lists that may pass 0 lists
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentArray++;
                currentArray = currentArray % (numArrays+1);
                Debug.Log(currentArray);
                if(currentArray == numArrays)
                {
                    fixVisualization(lists);
                }
                else
                {
                    fixVisualization(lists[currentArray]);
                }
            }
        }
        if (paused)
        {
            return;
        }
        if (beginTime == -1f) return;//Has not started yet
        if (!continueGoing)//If you have not yet given the signal to continue, check if you should
        {
            if((Time.time - beginTime) > delayTime)
            {
                continueGoing = true;
                beginTime = Time.time;
                return;
            }
        }
	}

}
