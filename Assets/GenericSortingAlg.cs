using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSortingAlg : MonoBehaviour {

	
	//Update is called once per frame
    //No update is used
	void Update () {;}

    //Holds the value that will be returned
    //TODO: Remove or modify to work in all sorting algorithms
    public List<int> TempStorageList = new List<int>();

    //This allows it to communicate with the visualizer
    public Visualizer algVisualizer;

    //Given a list, swaps indices x and y
    public static void Swap(List<int> sortList, int x, int y)
    {
        int temp = sortList[x];
        sortList[x] = sortList[y];
        sortList[y] = temp;
    }

    //Actions and assigns holds the respective values
    //Actions refers to any any operation done by the program
    //Assigns refers to any operation that changes the value in the list
    //These values will be continuously updated and rendered onto a window for the user to see
    int actions = 0;
    int assigns = 0;

    //A bool that indicates if there are multiple lists being used
    public bool multipleArrays;


    //,"swap", "assign", or "finish" are the possible type of changes
    //indexSwap1 and indexSwap2 refers...
    //In swap, you swap 1 and 2
    //In assign you assign the index of 1 to the value of 2
    //In finish you dont do anything and just turn everything dark green to indicate it has finished
    //varList holds the name of variables in the even indices and the value of the variable in odd indices
    //algLine holds the line of the pseudocode the code is current running
    //algTextDescription holds an array of strings, with each element of the array being a line of the pseudocode
    //The 0th element is the first line of the element, the 1st element the second line, etc.
    //algTextDescription[algLine] will return the line of the algorithm it is currently on.
    public void UpdateVisualizer(string typeOfChange, int indexSwap1, int indexSwap2, string[] varList, int algLine,string[] algTextDescription)
    { 
        actions++;//You have done at least one action, so add one to it

        //Temp unpaused will be true if you hit the 'next' button
        //It is a temporary pass to allow it to continue despite being paused
        if (algVisualizer.tempUnpaused)
        {
            algVisualizer.tempUnpaused = false;//It is only true for one time, so set it to false
            algVisualizer.paused = true;//Set it back to being paused
        }
        algVisualizer.continueGoing = false;//Need to wait again
        
        //You must do different actions depending on what type of action the algorithm requires
        switch (typeOfChange)
        {
            case "swap"://A swap exchanges the value of two indices
                algVisualizer.swapThem(indexSwap1, indexSwap2);//This function swaps them
                assigns += 2;//2 assignments as the list was changed in two actions
                actions += 2; //3 actions are done here(2 swaps + creation of temp variable), add two on top of the one initially added
                break;
            case "assign"://An assignment assigns the value of indexSwap2 to the index of indexSwap1
                algVisualizer.assign(indexSwap1, indexSwap2);//1 represents the index, 2 represents the value
                assigns++;//An assignment is just one assign
                break;
            case "finish"://Finish indicates the visualization has finished
                algVisualizer.finish();//This will turn all bars green to indicate it has finished
                break;
        }

        //The compText will store how many actions and assignments have occured
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);//Transfer it to the visualizer

        string[] algText = new string[algTextDescription.Length];//Create a new array of the size of the description
        algTextDescription.CopyTo(algText, 0);//Deep copy the entire array over to your new array
        algVisualizer.changeVariables(varList);//Update the variable list window

        //Create a string that holds the pseudocode with the proper line bolded
        string newAlgFullText = "";//Start it off blank
        for (int k = 0; k < algText.Length; k++)//Iterate through the entire list of strings
        {
            if (k == algLine)//If you reach the line the code has designated it is on that corresponds to the pseudocode
            {
                algText[k] = "<b>" + algText[k] + "</b>";//Bold that line to indicate to the user you are currently on it
            }
            newAlgFullText += algText[k];//Add that line to the full algorithm
        }
        algVisualizer.algText.text = newAlgFullText;//Finally, set the string to be the new pseudocode
    }

    //This overload of updateVisualizer takes a list of lists and will render that upon the visualizer
    //iList1 and iList2 distinguish which lists they take place on
    //TODO:Fix swap color
    public void UpdateVisualizer(List<List<int>> lists, string typeOfChange, int indexSwap1,int iList1, int indexSwap2, int iList2, string[] varList, int algLine, string[] algTextDescription)
    {
        actions++;
        if (algVisualizer.tempUnpaused)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }

        //You must adjust indexSwap1/indexSwap2 to account for it not being the first list
        if(iList1 != 0)
        {
            indexSwap1 += lists[0].Count;
        }
        if (iList2 != 0)
        {
            indexSwap1 += lists[0].Count;
        }
        algVisualizer.continueGoing = false;

        //This will work fine considering you added a buffer to the list if they were not on the first list
        //The rest should be the same as the original version
        switch (typeOfChange)
        {
            case "swap":
                algVisualizer.swapThem(indexSwap1, indexSwap2);
                assigns += 2;
                actions++;
                break;
            case "assign":
                algVisualizer.assign(indexSwap1, lists);
                assigns++;
                break;
            case "finish":
                algVisualizer.finish();
                break;
        }
        string[] algText = new string[algTextDescription.Length];
        algTextDescription.CopyTo(algText, 0);
        algVisualizer.changeVariables(varList);
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Updates the current line of the algorithm
    //algLine , varList, and algTextDescription are the same as they are in updateVisualizer
    public void UpdateAlgLine(int algLine, string[] varList,string[] algTextDescription)
    {
        actions++;//Increase actions

        //While alg view is open, rather than pausing upon seeing just a swap in the list
        //you pause upon any change, whether it be line of the pseudocode or in the list
        if (algVisualizer.tempUnpaused && algVisualizer.algContainer.activeInHierarchy)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }

        //The rest is the same as is in the update visualizer function
        //The only difference is that the visualizer update part of the function that was present in update visualizer is not here
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.continueGoing = false;
        string[] algText = new string[algTextDescription.Length];
        algTextDescription.CopyTo(algText, 0);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.changeVariables(varList);
        if (char.IsWhiteSpace(algText[algLine][0])) algText[algLine].Substring(1);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //This is the same as the prior function, except you now take a new variable newActions
    //newActions hold how many actions to add incase a given line of the pseudocode would do more than one action
    public void UpdateAlgLine(int algLine,int newActions, string[] varList, string[] algTextDescription)
    {
        actions += newActions;//Rather than actions++, you instead add newAction number of actions to your actions global
        //The rest is the same as the normal version of the function
        if (algVisualizer.tempUnpaused && algVisualizer.algContainer.activeInHierarchy)
        {
            algVisualizer.tempUnpaused = false;
            algVisualizer.continueGoing = false;
            algVisualizer.paused = true;
        }
        string compText = "Actions:\n{0}\nAssignments:\n{1}";
        algVisualizer.comparisonText.text = string.Format(compText, actions, assigns);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.continueGoing = false;//Need to wait again (if in alg mode;
        string[] algText = new string[algTextDescription.Length];
        algTextDescription.CopyTo(algText, 0);
        if (algVisualizer.algContainer.activeInHierarchy) algVisualizer.changeVariables(varList);
        if (char.IsWhiteSpace(algText[algLine][0])) algText[algLine].Substring(1);
        string newAlgFullText = "";
        for (int k = 0; k < algText.Length; k++)
        {
            if (k == algLine)
            {
                algText[k] = "<b>" + algText[k] + "</b>";
            }
            newAlgFullText += algText[k];
        }
        algVisualizer.algText.text = newAlgFullText;
    }

    //Add action is simply a function that allows a algorithm class to add a 'newActions' number of actions to the actions global
    //Without calling the updateAlgLine function
    //Useful in functions that are not in IEnumertor functions
    public void addActions(int newActions)
    {
        actions += newActions;
    }


}
