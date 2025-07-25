using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static string spawnPointName;

    private void Start()
    {
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawnPoint = GameObject.Find(spawnPointName);
            if (spawnPoint != null )
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnPoint.transform.position;
                }
            }
        }
    }
}
