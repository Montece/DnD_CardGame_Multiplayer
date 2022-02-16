using UnityEngine;

public class MyTurnPanel : MonoBehaviour
{
    private void OnEnable()
    {
        CancelInvoke("Disable");
        Invoke("Disable", 2F);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
