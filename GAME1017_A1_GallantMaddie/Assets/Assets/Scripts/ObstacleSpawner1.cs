
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObstacleSpawner1 : MonoBehaviour
{


    [SerializeField] private GameObject[] segmentPrefabs;

    

    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private int segmentListSize = 5;

    private Renderer lastRenderer, currentRenderer;
    private GameObject lastGameObject, currentGameObject;

    private List<GameObject> segments = new();
    [Tooltip("Represents the min (x) and max (y) distance that segments can spawn apart from each other.")]
    [SerializeField] Vector2 gapRange;
    [Tooltip("Represents the min (x) and max (y) height that segments can spawn apart from each other.")]
    [SerializeField] private Vector2 heightRange;
    private GameObject player;
    private float ySpawnPosition;
    private int lastIndex;

    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {

        if (segmentPrefabs == null || segmentPrefabs.Length == 0)
        {
            return;
        }

        if (GameManager.Instance == null)
        {
            return;
        }

       

        player = GameManager.Instance.Player?.gameObject;

        //Segment 1
        lastGameObject = Instantiate(segmentPrefabs[0], new Vector3(player.transform.position.x, ySpawnPosition - 40, 0), Quaternion.identity, transform);

        lastRenderer = lastGameObject.GetComponent<Renderer>();
        segments.Add(lastGameObject);

        //Segment 2
        currentGameObject = Instantiate(segmentPrefabs[0], transform);
        currentRenderer = currentGameObject.GetComponent<Renderer>();
        segments.Add(currentGameObject);

        //Last renderer and current renderer
        float initialGapSize = Random.Range(gapRange.x, gapRange.y);
        float xSpawnPosition = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2f) + initialGapSize;
        currentGameObject.transform.position = new Vector3(xSpawnPosition + 150, player.transform.position.y - 1 + 150, 0);

        lastGameObject = currentGameObject;
        lastRenderer = currentRenderer;

        lastIndex = 1;

    }


    private void Update()
    {

        if (lastRenderer == null || player == null)
        {
            return;
        }
        // Last GameObject.bounds.max.x < player.position.x + maxDistanceFromPlayer

        if (lastRenderer.bounds.max.x < player.transform.position.x + maxDistanceFromPlayer)
        {
           float gapSize = Random.Range(gapRange.x, gapRange.y);

            //changed height offset here
            float playerY = player.transform.position.y;
            float spawnY;

            do
            {
                spawnY = playerY + Random.Range(heightRange.x, heightRange.y);
            }
            while (Mathf.Abs(spawnY - playerY) < 2f);

            List<int> possibleIndices = new();

            //obstacle 2 and 3 can not be beside each other
            //same object can not spawn twice in a row


            if (lastIndex ==2 || lastIndex == 3)
            {
                possibleIndices.Add(0);
                possibleIndices.Add(1);
            }
            else 
            {
                for (int i =0; i < segmentPrefabs.Length; i++)
                {
                    if (i == lastIndex) continue;

                    possibleIndices.Add(i);
                }
            }

            //this index cannot equal last index

                int ind = Random.Range(0, possibleIndices.Count);
                int index = possibleIndices[ind];
            

            currentGameObject = Instantiate(segmentPrefabs[index], transform);
            currentRenderer = currentGameObject.GetComponent<Renderer>();
            lastIndex = index;
          float  xSpawnPosition = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;

            //adjusted this too
            currentGameObject.transform.position = new Vector3(xSpawnPosition, spawnY, 0f);
            segments.Add(currentGameObject);


            if (segments.Count > segmentListSize)
            {
                Destroy(segments[0]);
                segments.RemoveAt(0);
            }
            lastGameObject = currentGameObject;
            lastRenderer = currentRenderer;
        }
        

        FindFirstObjectByType<PlayerController>();
    }

    public void Reset()
    {
        lastRenderer = null;
        currentRenderer = null;

        lastGameObject = null;
        currentGameObject = null;

        lastIndex = 0;

        foreach(GameObject gameObject in segments)
        {
            Destroy(gameObject);
            
        }
        segments.Clear();
    }
}
