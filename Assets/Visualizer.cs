using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Visualizer : MonoBehaviour {
    private VisualizerObject[] visualizerObjects;
    private Canvas can;
    //public Color visualizerColor = Color.white;
    [SerializeField]
    public double maxHeight =  550;
    [SerializeField]
    public double maxNum = 1000;

    public double height;
    public double width;

    [SerializeField]
    public double sideBuffer = 90;

    public bool paused = false;
    public bool tempUnpaused = false;

    public bool continueGoing = false;
    public float beginTime = -1f;
    public float delayTime = 1f;//Wait .35 seconds before continuing

    List<int> numList;

    public int numElements = 10;
    public InputField numElementInputField;
    public InputField manualInputField;
    public Slider speedSlider;

    public GameObject algContainer;
    Vector3 algContainerVector;
    public Text algText;
    Vector3 algTextVector;
    public Text algDescription;
    Vector3 algDescriptionVector;
    public Button algHalfButton;
    Vector3 algHalfButtonVector;
    public Button algChangeButton;
    Vector3 algChangeButtonVector;
    public Text variableText;
    public Button PauseButton;
    bool half = false;

    public bool multipleLists = false;
    public List<List<int>> lists;

    public delegate List<int> AlgStart(List<int> sortList);
    public AlgStart algorithmInQuestion;

    public int numArrays = 1;
    public int currentArray = 0;//Current array indexs from 0

    public void WhiteOutAllBars()
    {
        for (int i = 0; i < visualizerObjects.Length; i++) visualizerObjects[i].GetComponent<Image>().color = Color.white;
    }

    public void confirmNumElements()
    {
        setNumElements();
    }

    private void setNumElements()
    {
        if (numElementInputField.text != "")
        {
            numElements = int.Parse(numElementInputField.text);
        }
        else
        {
            numElements = 10;
        }
        if (numElements == 0)
        {
            numElements = 1;
            numElementInputField.text = "1";
        }
        else if(numElements > 250)
        {
            numElements = 250;
            numElementInputField.text = "250";
        }
        rerollHeight();
    }

    private void initList()
    {
        numList = new List<int>(numElements);
        for (int i = 0; i < numElements; i++) numList.Add(Random.Range(0, 1000));
    }
    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "Sort")
        {
            Debug.Log(SortName.sortMethod);
            switch (SortName.sortMethod)
            {
                case "BubbleSort":
                    Debug.Log("Wheee im in bubble for some fucking reason");
                    gameObject.AddComponent<BubbleSort>();
                    break;
                case "CocktailSort":
                    Debug.Log("Yay im in cocktail sort like i should be!!!!!");
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
                default:
                    Debug.Log("Incorrect sort method name.");
                    break;

            }
        }
        visualizerObjects = GetComponentsInChildren<VisualizerObject>();
        initList();
        fixVisualization();
        StartCoroutine(PauseIt());
        delayTime = .35f;
        algContainerVector = algContainer.transform.position;
        //textWidth = algText.GetComponent<RectTransform>().rect.width;
        algTextVector = algText.transform.position;
        algHalfButtonVector = algHalfButton.transform.position;
        algChangeButtonVector = algChangeButton.transform.position;
        algTextVector = algText.transform.position;
        algDescription.resizeTextForBestFit = true;
        Debug.Log(beginTime);
	}
	
    public void startAlg()
    {
        algorithmInQuestion(numList);
    }

    public void fixVisualization()
    {
        for (int i = 0; i < visualizerObjects.Length; i++) visualizerObjects[i].delNum();
        double currentXLocation = 0;
        currentXLocation += sideBuffer; //Do not count the area set aside for the screen side buffers
        calcWidth();
        if (half && algContainer.activeInHierarchy) currentXLocation += (Screen.width / 2.0f);
        maxNum = findMaxNum();
        for (int i = 0; i < visualizerObjects.Length;i++) visualizerObjects[i].GetComponent<Image>().enabled = false; //Disable all of them, assume all will not be used
        for (int i = 0; i < numElements; i++)
        {
            visualizerObjects[i].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation, 30);
            double ratio = maxHeight / maxNum; // This might be a bit fucked
            height = (int)(ratio * (double)numList[i]);
            visualizerObjects[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2((float)width, (float)height);
            visualizerObjects[i].GetComponent<Image>().enabled = true;
            visualizerObjects[i].changeNum(numList[i], (float)currentXLocation,30 , (float)width, (float)height);
            currentXLocation += width;
        }
    }

    public void fixVisualization(List<int> visList)
    {
        for (int i = 0; i < visualizerObjects.Length; i++) visualizerObjects[i].delNum();
        double currentXLocation = 0;
        currentXLocation += sideBuffer; //Do not count the area set aside for the screen side buffers
        calcWidth(visList);
        if (half && algContainer.activeInHierarchy) currentXLocation += (Screen.width / 2.0f);
        maxNum = findMaxNum(visList);
        for (int i = 0; i < visualizerObjects.Length; i++) visualizerObjects[i].GetComponent<Image>().enabled = false; //Disable all of them, assume all will not be used
        for (int i = 0; i < visList.Count; i++)
        {
            visualizerObjects[i].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation, 30);
            double ratio = maxHeight / maxNum; // This might be a bit fucked
            height = (int)(ratio * (double)visList[i]);
            visualizerObjects[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2((float)width, (float)height);
            visualizerObjects[i].GetComponent<Image>().enabled = true;
            visualizerObjects[i].changeNum(visList[i], (float)currentXLocation, 30, (float)width, (float)height);
            currentXLocation += width;
        }
    }

    public void fixVisualization(List<List<int>> visLists)
    {
        for (int i = 0; i < visualizerObjects.Length; i++) visualizerObjects[i].delNum();
        double currentXLocation = 0;
        int currentImage = 0;
        currentXLocation += sideBuffer; //Do not count the area set aside for the screen side buffers
        calcWidth(visLists);
        if (half && algContainer.activeInHierarchy) currentXLocation += (Screen.width / 2.0f);
        maxNum = findMaxNum(visLists);
        for (int i = 0; i < visualizerObjects.Length; i++) visualizerObjects[i].GetComponent<Image>().enabled = false; //Disable all of them, assume all will not be used
        for (int i = 0; i < visLists.Count; i++)
        {
            for(int j = 0; j < visLists[i].Count; j++)
            {
                visualizerObjects[currentImage].GetComponent<Image>().transform.position = new Vector3((float)currentXLocation, 30);
                double ratio = maxHeight / maxNum; // This might be a bit fucked
                height = (int)(ratio * (double)visLists[i][j]);
                visualizerObjects[currentImage].GetComponent<Image>().rectTransform.sizeDelta = new Vector2((float)width, (float)height);
                visualizerObjects[currentImage].GetComponent<Image>().enabled = true;
                visualizerObjects[currentImage].changeNum(visLists[i][j], (float)currentXLocation, 30, (float)width, (float)height);
                currentXLocation += width;
                currentImage++;
            }
            currentXLocation += sideBuffer;
        }
    }

    private int findMaxNum()
    {
        int currentMax = numList[0];
        for (int i = 1; i < numElements; i++)
        {
            if (currentMax < numList[i]) currentMax = numList[i];
        }
        return currentMax;
    }

    private int findMaxNum(List<int> visList)
    {
        int currentMax = visList[0];
        for (int i = 1; i < visList.Count; i++)
        {
            if (currentMax < visList[i]) currentMax = visList[i];
        }
        return currentMax;
    }

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

    private void calcWidth()
    {
        if (half && algContainer.activeInHierarchy) width = Screen.width / 2;
        else { width = Screen.width; }
        width = width - (sideBuffer * 2);//Side buffer is the size on each side between the edge of screen and beginning of first/last bar
        width = (width / (double)numElements);
        
    }

    private void calcWidth(List<int> visList)
    {
        if (half && algContainer.activeInHierarchy) width = Screen.width / 2;
        else { width = Screen.width; }
        width = width - (sideBuffer * 2);//Side buffer is the size on each side between the edge of screen and beginning of first/last bar
        width = (width / (double)visList.Count);

    }

    private void calcWidth(List<List<int>> visLists)
    {
        int allEls = 0;
        for (int i = 0; i < visLists.Count; i++)
        {
            allEls += visLists[i].Count;
        }
        if (half && algContainer.activeInHierarchy) width = Screen.width / 2;
        else { width = Screen.width; }
        width = width - (sideBuffer * (2 + (visLists.Count - 1)));//Side buffer is the size on each side between the edge of screen and beginning of first/last bar
        width = (width / (double)allEls);
    }

    public void rerollHeight()
    {
        Dropdown method = GetComponentInChildren<Dropdown>();
        int currentVal;
        switch (method.value)
        {
            case 0:
                numList = new List<int>(numElements);
                for (int i = 0; i < numElements; i++) numList.Add(Random.Range(0, 1000));
                break;
            case 1: //Weighted to the left
                currentVal = Random.Range(0, 25);
                numList = new List<int>(numElements);
                numList.Add(currentVal);
                for (int i = 1; i < numElements; i++)
                {
                    currentVal = Mathf.Abs(currentVal + Random.Range(-25, 100));//Add between -25 and 100 to make it appear weighted ,abs to make no negative
                    numList.Add(currentVal);
                }
                break;
            case 2: //Weighted to the right
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
            case 3:
                string[] elementsString = manualInputField.text.Split(',');
                numElements = elementsString.Length;
                if (numElements > 250) numElements = 250;
                numList = new List<int>(numElements);
                for (int i = 0; i < numElements; i++) numList.Add(int.Parse(elementsString[i]));
                break;
            default:
                break;
        }
        for (int i = 0; i < numElements; i++) visualizerObjects[i].GetComponent<Image>().color = Color.white;
        fixVisualization();
    }

    IEnumerator PauseIt()
    {
        yield return new WaitForSeconds(.5f);
    }

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

    public void setPause()
    {
        paused = !paused;
        if (paused) PauseButton.GetComponent<Image>().material = (Material)Resources.Load("images/materials/pauseMat", typeof(Material));
        else PauseButton.GetComponent<Image>().material = (Material)Resources.Load("images/materials/playMat", typeof(Material));

    }

    public void considerSwapThem(int indexX, int indexY) // Might want to change value in list, maybe it already does that??
    {
        WhiteOutAllBars();
        visualizerObjects[indexX].GetComponent<Image>().color = Color.yellow;
        visualizerObjects[indexY].GetComponent<Image>().color = Color.yellow;
        return;
    }

    public void swapThem(int indexX, int indexY) // Might want to change value in list, maybe it already does that??
    {
        WhiteOutAllBars();
        int numTemp;
        numTemp = visualizerObjects[indexX].num;
        Vector2 temp = visualizerObjects[indexX].GetComponent<Image>().rectTransform.sizeDelta;
        visualizerObjects[indexX].GetComponent<Image>().rectTransform.sizeDelta = visualizerObjects[indexY].GetComponent<Image>().rectTransform.sizeDelta;
        visualizerObjects[indexY].GetComponent<Image>().rectTransform.sizeDelta = temp;
        visualizerObjects[indexX].changeNumHeight(visualizerObjects[indexY].num, visualizerObjects[indexX].GetComponent<Image>().rectTransform.sizeDelta.y);
        visualizerObjects[indexY].changeNumHeight(numTemp, visualizerObjects[indexY].GetComponent<Image>().rectTransform.sizeDelta.y);
        visualizerObjects[indexX].GetComponent<Image>().color = Color.green;
        visualizerObjects[indexY].GetComponent<Image>().color = Color.green;
        return;        
    }

    public void assign(int index, int value) // Might want to change value in list, maybe it already does that??
    {
        WhiteOutAllBars();
        numList[index] = value;
        fixVisualization();
        visualizerObjects[index].GetComponent<Image>().color = Color.green;
        return;
    }

    public void assign(int index, List<List<int>> lists) // Might want to change value in list, maybe it already does that??
    {
        WhiteOutAllBars();
        if (currentArray == numArrays)
        {
            fixVisualization(lists);
        }
        else
        {
            fixVisualization(lists[currentArray]);
        }
        visualizerObjects[index].GetComponent<Image>().color = Color.green;
        return;
    }

    public void doNotSwapThem(int indexX, int indexY) // Might want to change value in list, maybe it already does that??
    {
        WhiteOutAllBars();
        visualizerObjects[indexX].GetComponent<Image>().color = Color.red;
        visualizerObjects[indexY].GetComponent<Image>().color = Color.red;
        return;
    }

    public void finish()
    {
        for (int i = 0; i < numElements; i++) visualizerObjects[i].GetComponent<Image>().color = new Color(.01f,.50f,.30f);
        tempUnpaused = false;
        paused = false;
        return;
    }

    //Remake the list of variables
    public void changeVariables(string[] newVarList)
    {
        variableText.text = "Variable List:\n";
        for (int i = 0;i < newVarList.Length; i += 2)
        {
            variableText.text = variableText.text + newVarList[i] + ": " + newVarList[i + 1] + "\n";
        }
        return;
    }

    public void setSpeed()
    {
        delayTime = Mathf.Pow(.35f, speedSlider.value);
    }

    public void AlgEnable()
    {
        if(paused == algContainer.activeInHierarchy)setPause();
        algContainer.SetActive(!algContainer.activeInHierarchy);
        fixVisualization();
    }

    public void AlgHalfView()
    {
        if (!half)
        {
            half = true;
            algContainer.transform.position = new Vector3(0, algContainer.transform.position.y);
            algText.transform.position = new Vector3(Screen.width/4.0f,algText.transform.position.y);
            algHalfButton.transform.position = new Vector3((Screen.width/2.0f) - (algHalfButton.GetComponent<RectTransform>().rect.width/2.0f), algHalfButton.transform.position.y);
            algDescription.enabled = false;
            //algDescription.transform.position = algText.transform.position;
            //algChangeButton.transform.position = new Vector3(algHalfButton.transform.position.x, algChangeButton.transform.position.y,0);
            //algChangeButton.enabled = true;
            if (multipleLists)
            {
                fixVisualization(lists);
            }
            else
            {
                fixVisualization();
            }
        }
        else
        {
            half = false;
            algContainer.transform.position = algContainerVector;
            algText.transform.position = algTextVector;
            algHalfButton.transform.position = algHalfButtonVector;
            //algChangeButton.transform.position = algChangeButtonVector;
            //algDescription.transform.position = algDescriptionVector;
            algDescription.enabled = true;
            //algChangeButton.enabled = true;
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

    public void AlgChangeScreen()
    {
        algText.enabled = !algText.enabled;
        algDescription.enabled = !algDescription.enabled;
        
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Hub", LoadSceneMode.Single);
        }
        if(numArrays > 1)
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
