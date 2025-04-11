using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretSpawner : MonoBehaviour
{
    public PlayerTurret Spawn(PlayerTurret playerPrefab, Transform location)
    {
        PlayerTurret newTurret = Instantiate(playerPrefab, location.position, location.rotation);
        return playerPrefab;
    }
}
