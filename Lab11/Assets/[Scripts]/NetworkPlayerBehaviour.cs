using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
    [Header("Player Movement Properties")]
    public float speed;

    private NetworkVariable<float> verticalPosition = new NetworkVariable<float>();
    private NetworkVariable<float> horizontalPosition = new NetworkVariable<float>();

    private float localHorizontal;
    private float localVertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsServer)
        {
            // server update
        }

        if(IsClient && IsOwner)
        {
            // client update
        }
    }
}
