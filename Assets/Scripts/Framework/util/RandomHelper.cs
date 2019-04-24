using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

public class RandomHelper {

	public static string defaultWeightedPropertyName = "weight";

	public static T GetWeightedRandomByExpanding<T>(List<T> inputList, string weightPropertyName) {
		Type typeParameterType = typeof(T);
		FieldInfo field = typeParameterType.GetField(weightPropertyName);
		if(field != null) {
			List<T> randomResults = new List<T>();

			foreach(T item in inputList) {
				int weightedValue = (int)field.GetValue(item);
				var itemRepeated = Enumerable.Repeat(item, weightedValue);
				randomResults.AddRange(itemRepeated);
			}

			int chosenRandomIndex = UnityEngine.Random.Range(0, randomResults.Count);
			T chosenRandom = randomResults[chosenRandomIndex];
			return chosenRandom;
		} 
		return default(T);
	}

	public static T GetWeightedRandomByExpanding<T>(List<T> inputList) {
		return GetWeightedRandomByExpanding<T>(inputList, RandomHelper.defaultWeightedPropertyName);
	}

	/*

	
		

	*/
}
