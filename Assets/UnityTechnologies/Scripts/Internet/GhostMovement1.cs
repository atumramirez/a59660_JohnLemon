using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ClientObjectController : NetworkBehaviour
{
    public float speed = 5f;

    public void MoveUp()
    {
        if (IsOwner)
            MoveServerRpc(Vector3.up);
    }

    public void MoveDown()
    {
        if (IsOwner)
            MoveServerRpc(Vector3.down);
    }

    public void MoveLeft()
    {
        if (IsOwner)
            MoveServerRpc(Vector3.left);
    }

    public void MoveRight()
    {
        if (IsOwner)
            MoveServerRpc(Vector3.right);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}