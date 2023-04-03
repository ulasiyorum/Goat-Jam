using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public Transform location1;
    public Transform location2;
    private int count;
    private bool started;
    public int currentWave;
    public List<GameObject> enemies;

    [SerializeField] GameObject destroyDoor;
    private float nextWaveCounter;
    public TMP_Text countdown;

    public static MobSpawner instance;
    private bool isStory;
    // Start is called before the first frame update
    void Start()
    {
        isStory = SceneManager.GetActiveScene().name == "ArenaStory";

        countdown.gameObject.SetActive(false);
        started = false;
        instance = this;
        count = 0;
        enemies = new List<GameObject>();
        currentWave = 1;
        StartCoroutine(Spawn(location1));
        StartCoroutine(Spawn(location2));
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0 && started && isStory && currentWave == 5)
        {
            Destroy(destroyDoor);
            StartPopUpMessage.Message("You're free to leave now!", Color.green);
            this.enabled = false;
        }
        if (enemies.Count == 0 && started && !(currentWave == 5 && isStory))
        {
            PlayerAttributes.instance.temporaryDamage = 0;
            countdown.gameObject.SetActive(true);
            started = false;
            nextWaveCounter = 0;
            count = 0;
        }

        if(countdown.gameObject.activeInHierarchy)
        {
            countdown.text = "Next Wave In : " + (int)(10 - nextWaveCounter);
            nextWaveCounter += Time.deltaTime;
        }

        if(nextWaveCounter > 10)
        {
            nextWaveCounter = 0;
            countdown.gameObject.SetActive(false);
            currentWave++;
            StartCoroutine(Spawn(location1));
            StartCoroutine(Spawn(location2));
        }
    }

    public IEnumerator Spawn(Transform location)
    {
        count++;
        yield return new WaitForSeconds(5);

        GameObject go = Instantiate(enemy);
        started = true;
        go.transform.position = location.position;
        go.transform.rotation = location.rotation;
        enemies.Add(go);

        if(count < currentWave + 1)
            StartCoroutine(Spawn(location));
    }
}
