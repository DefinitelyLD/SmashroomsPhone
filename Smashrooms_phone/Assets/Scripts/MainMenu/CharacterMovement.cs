using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GameObject character;

    private void Start() => MoveCharacterUp();

    private void MoveCharacterUp()
    {
        int id = LeanTween.moveLocalY(character, character.transform.localPosition.y + 25, 10f).id;
        LTDescr d = LeanTween.descr(id);

        d.setOnComplete(MoveCharacterDown);
    }
    private void MoveCharacterDown()
    {
        int id = LeanTween.moveLocalY(character, character.transform.localPosition.y -25, 10f).id;
        LTDescr d = LeanTween.descr(id);

        d.setOnComplete(MoveCharacterUp);
    }
}
