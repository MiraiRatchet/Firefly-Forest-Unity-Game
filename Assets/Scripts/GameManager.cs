using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject canv;
    public PlayableDirector director;
    public List<GameObject> my_bugs;

    public List<GameObject> evil_bugs;

    private List<GameObject> existingEnvironments;

    public List<GameObject> environmentPrefabs;

    public int numberOfEnvironmentInArea=50;

    public int radiusOfGenerationEnvironment;

    private GameObject player;
    Transform playerTransform;

    private AudioManager audioManager;

    public GameObject goodBugPrefab;

    public GameObject evilBugPrefab;

    public int numberOfEvilBugs;

    public int numberOfGoodBugs;//not GG bugs, free bugs

    public float radiusOfGenerationGood;

    public float radiusOfGenerationEvil;

    public int numberOfBugsToWin;

    public Animation LightningAnimation;


    public float radiusOfExistingEnv;

    Text scoreText;

    LevelLoader lvlLoader;

    public GameObject settings;

    Vector3 offset;

    // Start is called before the first frame update

    void Awake() {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = "1/" + numberOfBugsToWin.ToString();
    }
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Set");
        numberOfBugsToWin = settings.GetComponent<SettingsScript>().level;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        audioManager = FindObjectOfType<AudioManager>();
        lvlLoader = FindObjectOfType<LevelLoader>();
        audioManager.Stop("MusicMainTheme");
        audioManager.Play("Rain");
        my_bugs = new List<GameObject>();
        offset.z = 0;
        offset.y = 1;
        offset.x = -1;//Random.Range(0, 10);
        //my_bugs.Add(Instantiate(goodBugPrefab, player.GetComponent<Transform>().position + offset, Quaternion.identity));
        Instantiate(goodBugPrefab, playerTransform.position + offset, Quaternion.identity);
        for (int i = 0; i < numberOfEvilBugs; i++)
        {
            spawnEvilBug();
        }

        for (int i = 0; i < numberOfGoodBugs; i++)
        {
            spawnGoodBug();
        }
        existingEnvironments = new List<GameObject>();
        for (int i = 0; i < numberOfEnvironmentInArea; i++)
        {
            spawnAllEnvironment();
        }

        StartCoroutine(LightningCoroutine());
    }

    IEnumerator LightningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15,100));
            audioManager.Play("Lightning");
            LightningAnimation.Play();

        }
    }

    void spawnAllEnvironment()
    {
        foreach (GameObject obj in environmentPrefabs)
        {
            float minSizeOfObject = 2f;
            int L1 = Random.Range(-radiusOfGenerationEnvironment, radiusOfGenerationEnvironment);
            int L2 = Random.Range(-radiusOfGenerationEnvironment, radiusOfGenerationEnvironment);
            existingEnvironments.Add(Instantiate(obj, new Vector3(L1 * minSizeOfObject, L2 * minSizeOfObject, 0), Quaternion.identity));
        }
    }


    public void spawnGoodBug()
    {
        float radius = Random.Range(radiusOfGenerationGood, 2 * radiusOfGenerationGood);
        float angle = Random.Range(0, 2 * 3.14f);

        offset.x = radius * Mathf.Cos(angle);
        offset.y = radius * Mathf.Sin(angle);
        offset.z = 0;
        Instantiate(goodBugPrefab, playerTransform.position + offset, Quaternion.identity);
    }


    public void spawnEvilBug()
    {
        float radius = Random.Range(radiusOfGenerationEvil, 2 * radiusOfGenerationEvil);
        float angle = Random.Range(0, 2 * 3.14f);

        offset.x = radius * Mathf.Cos(angle);
        offset.y = radius* Mathf.Sin(angle);
        offset.z = 0;
        evil_bugs.Add(Instantiate(evilBugPrefab, playerTransform.position + offset, Quaternion.identity));
    }

    IEnumerator goToMenu() {
        yield return new WaitForSeconds(5f);
        audioManager.Play("MusicMainTheme");
        lvlLoader.loadlvl(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (my_bugs.Count == numberOfBugsToWin)
        {
            Debug.Log("Win!!!");
            director.Play();
            audioManager.Stop("FootSteps");
            audioManager.Stop("Rain");
            audioManager.Stop("Lightning");
            StartCoroutine(goToMenu());
        }
        scoreText.text = my_bugs.Count.ToString() + "/" + numberOfBugsToWin.ToString();

        Vector3 playerPos = playerTransform.position;
        foreach (GameObject obj in existingEnvironments)
        {
            float distance = (playerPos - obj.GetComponent<Transform>().position).magnitude;
            if (distance > radiusOfExistingEnv)
            {
                float radius = Random.Range(0.5f * radiusOfGenerationEnvironment, 1.5f * radiusOfGenerationEnvironment);
                float angle = Random.Range(0, 2 * 3.14f);
                obj.GetComponent<Transform>().position = playerPos + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
            }
        }
    }

    public void goToMainMenu() {
        Time.timeScale = 1f;
        audioManager.Play("MouseClick");
        lvlLoader.loadlvl(0);
        audioManager.Stop("Rain");
        audioManager.Play("MusicMainTheme");
    }

    public void resumeGame() {
        Time.timeScale = 1f;
        audioManager.Play("MouseClick");
        pauseMenuUI.SetActive(false);
        canv.SetActive(true);
    }
    public void pauseMenu() {
        audioManager.Play("MouseClick");
        pauseMenuUI.SetActive(true);
        canv.SetActive(false);
        Time.timeScale = 0f;
    }
}
