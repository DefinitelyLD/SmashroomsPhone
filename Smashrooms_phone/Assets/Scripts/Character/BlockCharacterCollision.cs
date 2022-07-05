using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D characterCollider;
    [SerializeField] BoxCollider2D characterBlockerCollider;
    private void Start() => Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
}
