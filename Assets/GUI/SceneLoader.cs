using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
	public Button _button;
	
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
