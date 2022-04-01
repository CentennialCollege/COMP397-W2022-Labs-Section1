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
        // spawn each player in a random position
        RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsServer)
        {
            // server update
            ServerUpdate();
        }

        if(IsClient && IsOwner)
        {
            // client update
            ClientUpdate();
        }
    }

    public void RandomSpawnPosition()
    {
        var x = Random.Range(-3.0f, 3.0f);
        var z = Random.Range(-3.0f, 3.0f);
        transform.position = new Vector3(x, 1.0f, z);
    }

    private void ServerUpdate()
    {
        transform.position = new Vector3(transform.position.x + horizontalPosition.Value, transform.position.y,
            transform.position.z + verticalPosition.Value);
    }

    public void ClientUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        var verticalInput = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        // network update
        if(localHorizontal != horizontalInput || localVertical != verticalInput)
        {
            localHorizontal = horizontalInput;
            localVertical = verticalInput;

            // update the Client position on the network
            UpdateClientPositionServerRpc(horizontalInput, verticalInput);
        }
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float horizontal, float vertical)
    {
        // set the network variables for horizontal and vertical input
        horizontalPosition.Value = horizontal;
        verticalPosition.Value = vertical;
    }
}
