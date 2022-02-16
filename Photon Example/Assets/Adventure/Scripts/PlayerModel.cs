using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField]
    private TextMesh NicknameText;

    public void SetModelNickname(string text)
    {
        NicknameText.text = text;
        name = text;
    }

    public void SetModelPosition(Vector3 hexPosition)
    {
        transform.position = hexPosition + Vector3.up * 2.5F;
    }
}
