using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void loadlvl(int lvlIndex)
    {
        StartCoroutine(LoadLevel(lvlIndex));
    }

    IEnumerator LoadLevel(int lvlIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(lvlIndex);
    }
}
