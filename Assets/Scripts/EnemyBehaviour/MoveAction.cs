using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Move"), System.Serializable]
public class MoveAction : EnemyActions
{
    [SerializeField]
    private float moveDistance;
    private Vector3 movementDirection;
    private Vector3 startPosition;
    private bool startPositionSet = false;
    private Rigidbody rigidbody;

    public override void Execute(Enemy enemy)
    {
        rigidbody = enemy.gameObject.GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = enemy.gameObject.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
        }
        if (startPositionSet == false)
        {
            startPosition = rigidbody.position;
            ChangeMovementDirection(enemy);
            startPositionSet = true;
        }
        if (Vector3.Distance(startPosition, enemy.transform.position) < moveDistance)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(enemy.transform.position, movementDirection, out raycastHit, 1f))
            {
                if (raycastHit.collider.tag != "Player" && raycastHit.collider.tag != "Enemy" && raycastHit.collider.tag != "Bullet")
                {
                    ChangeMovementDirection(enemy);
                }
            }
            rigidbody.velocity = movementDirection * enemy.MovementSpeed;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            startPositionSet = false;
            enemy.ActionCompleted();
        }
    }

    public void ChangeMovementDirection(Enemy enemy)
    {
        movementDirection = Quaternion.Euler(0, Random.Range(-180, 181), 0) * enemy.transform.forward;
    }
}
