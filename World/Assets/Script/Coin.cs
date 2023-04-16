using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator animator;
    private GameObject character;
    private float spawnDistanceMin = 10f;
    private float spawnDistanceMax = 20f;
    private float coinSpawnOffsetY;
    private static int coinCount = 0;
    private TMPro.TextMeshProUGUI coinCountText;

    public static int CoinCount
    {
        get => coinCount;
        set => coinCount = value;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.Find("Character");
        coinCountText = GameObject.Find("CoinCount").GetComponent<TMPro.TextMeshProUGUI>();
        float terraHeight = Terrain.activeTerrain.SampleHeight(this.transform.position);
        coinSpawnOffsetY =
            this.transform.position.y
            - terraHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if ((character.transform.position - this.transform.position).magnitude < 5)
        {
            animator.SetBool(
                "IsNear",
                true);
        }
        else
        {
            animator.SetBool(
              "IsNear",
              false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Character"))
        {
            animator.SetBool(
              "IsPicked",
              true);
            CoinCount += 10;
            coinCountText.SetText(CoinCount.ToString());
            Pickup();

        }
        else if (other.transform.parent?.name != "Character")
        {

        }
    }

    public void Pickup()
    {
        Vector3 spawnPosition;
        float spawnDistance;
        do
        {
            spawnPosition = new Vector3(
                this.transform.position.x + Random.Range(-spawnDistanceMax, spawnDistanceMax),
                this.transform.position.y,
                this.transform.position.z + Random.Range(-spawnDistanceMax, spawnDistanceMax));
            spawnDistance = Vector3.Distance(spawnPosition, this.transform.position);
        } while (spawnDistance > spawnDistanceMax || spawnDistance < spawnDistanceMin ||
                spawnPosition.x < 15 || spawnPosition.z < 15 ||
                spawnPosition.x > 985||spawnPosition.z>985);

        //RaycastHit hit;
        //Vector3 rayOrigin =
        //    spawnPosition +
        //    Vector3.up * 100;
        //Physics.Raycast(rayOrigin,
        //    Vector3.down,
        //    out hit);
        float terraHeight = Terrain.activeTerrain.SampleHeight(spawnPosition);
        spawnPosition.y = terraHeight + coinSpawnOffsetY;
        this.transform.position = spawnPosition;
        //Debug.Log("Respawn");
        animator.SetBool("IsPicked", false);
    }
}
