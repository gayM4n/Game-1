using UnityEngine;

public class CharacterWallTestLeft : MonoBehaviour
{
    public bool IsWalledLeft = false;

    void OnTriggerStay2D(Collider2D other)
    {
        IsWalledLeft = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsWalledLeft = false;
    }

}
