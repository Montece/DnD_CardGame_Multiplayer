using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    private Card WhatCardDetails = null;
    private MyFieldCard MyFieldCard = null;

    private Camera Camera;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        bool onGameObject = Physics.Raycast(ray, out RaycastHit hit);

        if (onGameObject)
        {
            MyArmCard myArmCard = hit.collider.GetComponent<MyArmCard>();
            MyFieldCard myFieldCard = hit.collider.GetComponent<MyFieldCard>();
            EnemyFieldCard enemyFieldCard = hit.collider.GetComponent<EnemyFieldCard>();
            EnemyAva enemyAva = hit.collider.GetComponent<EnemyAva>();

            if (myArmCard != null)
            {
                //Показываем информацию о карточке
                WhatCardDetails = myArmCard.GetCard();
                DungeonInterface.Instance.ShowCardDetails(WhatCardDetails);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    DungeonManager.Instance.MoveCardToField(WhatCardDetails);
                    WhatCardDetails = null;
                }
            }
            else if (myFieldCard != null)
            {
                //Показываем информацию о карточке
                WhatCardDetails = myFieldCard.GetCard();
                DungeonInterface.Instance.ShowCardDetails(WhatCardDetails);

                if (Input.GetKeyDown(KeyCode.Mouse0) && myFieldCard.CanAttack)
                {
                    MyFieldCard = myFieldCard;
                }
            }
            else if (enemyFieldCard != null)
            {
                WhatCardDetails = enemyFieldCard.GetCard();
                DungeonInterface.Instance.ShowCardDetails(WhatCardDetails);

                if (Input.GetKeyDown(KeyCode.Mouse0) && MyFieldCard != null)
                {
                    DungeonManager.Instance.AttackCard(MyFieldCard.GetCard(), enemyFieldCard.GetCard());
                    MyFieldCard = null;
                }   
            }
            else if (WhatCardDetails != null)
            {
                //Показываем информацию о карточке
                WhatCardDetails = null;
                DungeonInterface.Instance.HideCardDetails();
            }
            else if (enemyAva != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && MyFieldCard != null)
                {
                    DungeonManager.Instance.AttackPlayer(MyFieldCard.GetCard());
                    MyFieldCard = null;
                }
            }
        }
        else
        {
            if (WhatCardDetails != null)
            {
                WhatCardDetails = null;
                DungeonInterface.Instance.HideCardDetails();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (MyFieldCard != null) MyFieldCard = null;
            }
        }
    }
}
