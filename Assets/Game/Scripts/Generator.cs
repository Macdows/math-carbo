using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    private List<int> _output = new List<int>();
    private int _result;

    //Create a random list of ints with the first three between 1 and 12 and the last two between 1 and 24
    public List<int> RandomList()
    {
        _output = new List<int>();
        for (int i = 0; i < 3; i++)
        {
			_output.Add(Random.Range(1,13));
        }
        for (int i = 0; i < 2; i++)
        {
			_output.Add(Random.Range(1, 25));
        }

        return _output;
    }

    //Perform random operations on the random list, a minimum of two and a maximum of four operations are aloud
    //If an operation is illegal (- or /) do not count as an operation 
	public int PerformRandomOperations(List<int> inputList)
    {
        //clone the list so we do not modify the original list
        List<int> cloneList = new List<int>(inputList);
        //Get a random number of operations to perform on the list
        int numberOfOperations = Random.Range(2, 5);
        for (int i = 1; i < numberOfOperations; i++)
        {
            //Get 2 random numbers from the list and initialise the int for the result
            var numbers = GetTwoAtRandomIndexes(cloneList);
            int a = numbers[0];
            int b = numbers[1];
            int c;
            //Determine which operation is to be performed next
            int indexOfOperation = Random.Range(1, 5);
            switch(indexOfOperation)
            {
                case 1:
                    c = a + b;
                    cloneList.Add(c);
                    break;
                case 2:
                    c = a * b;
                    cloneList.Add(c);
                    break;
                case 3:
                    if (a < b)
                    {
                        c = b - a;
                    }
                    else if (a > b)
                    {
                        c = a - b;
                    }
                    //If a = b the substraction can not be performed so we put back the ints in the list, decrease the counter and try a different operation
                    else
                    {
                        cloneList.Add(a);
                        cloneList.Add(b);
                        i--;
                        continue;
                    }
                    cloneList.Add(c);
                    break;

                case 4:
                    if (a % b == 0)
                    {
                        c = a / b;
                    }
                    else if (b % a == 0)
                    {
                        c = b / a;
                    }
                    //If a%b or b%a != 0 the division can not be performed so we put back the ints in the list, decrease the counter and try a different operation
                    else
                    {
                        cloneList.Add(a);
                        cloneList.Add(b);
                        i--;
                        continue;
                    }
                    cloneList.Add(c);
                    break;
            }
        }
        //after all the operations are performed the result is the list int of the clone string as the result is always added to the list last
        _result = cloneList[cloneList.Count-1];
		return _result;
    }

    //Get two ints from the list and remove them from said list
    public int[] GetTwoAtRandomIndexes(List<int> inputList)
    {
        //get the first random index and retrieve the number present at the index in the list before removing it from the list
		int index = Random.Range(0, inputList.Count);
        int a = inputList[index];
        inputList.RemoveAt(index);
        //Do the same for the second int
        index = Random.Range(0, inputList.Count);
        int b = inputList[index];
        inputList.RemoveAt(index);
        //return the two ints we obtained
        return new int[] {a,b};
    }
    
	public int GenerateResult(List<int> list)
    {
    	//Perform random operations on random lists
        return PerformRandomOperations(list);
    }
}
