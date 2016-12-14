using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// Literally all this script does is spawn the flags on server start...

public class FlagHandlerScript : NetworkBehaviour
{

    public GameObject FlagOne; 
    public GameObject FlagTwo; 

    public override void OnStartServer() 
    {
        SpawnOne();
        SpawnTwo();
    }

    public void SpawnOne() 
    {
        GameObject FlagOneInstance = GameObject.Instantiate(FlagOne) as GameObject;
        NetworkServer.Spawn(FlagOneInstance);
        FlagOneInstance.name = "FlagOne";
    }
    public void SpawnTwo() 
    {
        GameObject FlagTwoInstance = GameObject.Instantiate(FlagTwo) as GameObject;
        NetworkServer.Spawn(FlagTwoInstance);
        FlagTwoInstance.name = "FlagTwo";
    }

}

