using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preloader : MonoBehaviour
{
    [SerializeField] private float _delayInSeconds = 3f;

    private void Start()
    {
        StartCoroutine(LoadMainMenuWithDelay());
    }

    private IEnumerator LoadMainMenuWithDelay()
    {
        yield return new WaitForSeconds(_delayInSeconds);
        SceneManager.LoadScene(1);
    }
}
