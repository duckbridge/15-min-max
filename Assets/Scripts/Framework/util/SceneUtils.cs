using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneUtils : MonoBehaviour {

	public static List<T> FindObjectsOf<T>()
	{
		T[] foundObjects = GameObject.FindObjectsOfType(typeof(T)) as T[];
		List<T> returnList = new List<T>(foundObjects);
		return returnList;
	}

	public static T FindObjectOf<T>() //TODO: improve
	{
		List<T> foundObjects = SceneUtils.FindObjectsOf<T>();
		if(foundObjects.Count > 0) {
			return foundObjects[0];
		}
		Debug.Log("[SceneUtils] {WARN} could not find the object!");
		return default(T);
	}
}
