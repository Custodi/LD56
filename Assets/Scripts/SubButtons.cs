using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextButton;

    private void OnEnable()
    {
        LevelController.Instance.OnTargetCompleted += OnTargetCompleted;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnTargetCompleted -= OnTargetCompleted;
    }

    private void OnTargetCompleted()
    {
        _nextButton.SetActive(true);
    }

    public void ReloadlLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.sceneCountInBuildSettings <= nextIndex)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextIndex);
        }
       
    }
}
