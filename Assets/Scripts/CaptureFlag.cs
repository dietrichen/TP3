using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// This script is attached to the player prefab. 
// Basically, it keeps track of what flag a player picks up,
// tells the server to destroy a flag if a player runs into it, 
// spawns a flag if you pick up a flag and touch the wrong collider, 
// and I think that's it. It contains most of the capture the flag stuff. 

public class CaptureFlag : NetworkBehaviour {

    public int flag = 0; // Variable to tell if this player is holding a flag, and which one.
    public GameObject FlagOne;
    public GameObject FlagTwo;

    [Command]
    public void Cmd_SpawnOne()
    {
        GameObject FlagOneInstance = GameObject.Instantiate(FlagOne) as GameObject;
        FlagOneInstance.name = "FlagOne";
        NetworkServer.Spawn(FlagOneInstance);
    }
    [Command]
    public void Cmd_SpawnTwo()
    {
        GameObject FlagTwoInstance = GameObject.Instantiate(FlagTwo) as GameObject;
        FlagTwoInstance.name = "FlagTwo";
        NetworkServer.Spawn(FlagTwoInstance);
    }
    [Command]
    void Cmd_DestroyFlag(GameObject aGame)
    {
        NetworkServer.Destroy(aGame);
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.name == "FlagOne" || target.gameObject.name == "FlagOne(Clone)")
        {
            flag = 1; // Flag One has been "picked up". Actually it gets destroyed and we store an int to simulate picking it up.  
            Cmd_DestroyFlag(target.gameObject);
        }
        if (target.gameObject.name == "FlagTwo" || target.gameObject.name == "FlagTwo(Clone)")
        {
            flag = 2; // Flag two has been "picked up". Actually destroyed etc. 
            Cmd_DestroyFlag(target.gameObject);
        }
        if (target.gameObject.name == "Flag2_Win")  
        {
            if (flag == 2) 
            {
                // Call Game Over Scene. 
            }
            if (flag == 1)
            {
                Cmd_SpawnOne();
                flag = 0;
            }
        }
        if (target.gameObject.name == "Flag1_Win")
        {
            if (flag == 1)
            {
                // Call Game Over Scene. 
            }
            if (flag == 2)
            {
                Cmd_SpawnTwo();
                flag = 0;
            }
        }
    }
}
