using UnityEngine;

public class CharacterWallTest : MonoBehaviour
{
    public bool IsWalled;

    void OnTriggerEnter2D(Collider2D collider)
    {
        IsWalled = true;
        print("Walled");
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        IsWalled = false;
        print("Unwalled");
    }

}
