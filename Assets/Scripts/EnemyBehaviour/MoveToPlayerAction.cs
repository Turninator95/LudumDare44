using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Move To Player"), System.Serializable]
public class MoveToPlayerAction : EnemyActions
{
    [SerializeField]
    private float moveDistance, playerDistance;
    private Vector3 movementDirection;
    private Vector3 startPosition;
    private bool startPositionSet = false;
    private Rigidbody rigidbody;
    private Player player;
    public override void Execute(Enemy enemy)
    {
        CheckVariables(enemy);

        if (Vector3.Distance(startPosition, enemy.transform.position) < moveDistance && Vector3.Distance(enemy.transform.position, player.transform.position) >= playerDistance)
        {
            Collider[] colliders = Physics.OverlapBox(enemy.transform.position, enemy.transform.localScale / 2);

            foreach (Collider collider in colliders)
            {
                if (collider.tag != "Player" && collider.tag != "Enemy" && collider.tag != "Bullet")
                {
                    ChangeMovementDirection(enemy);
                    break;
                }
            }
            rigidbody.velocity = movementDirection * enemy.MovementSpeed;
        }
        else
        {
            MoveCompleted(enemy);
        }
    }

    private void MoveCompleted(Enemy enemy)
    {
        rigidbody.velocity = Vector3.zero;
        startPositionSet = false;
        enemy.ActionCompleted();
    }

    public void ChangeMovementDirection(Enemy enemy)
    {
        movementDirection = Quaternion.Euler(0, Random.Range(-180, 181), 0) * enemy.transform.forward;
    }

    private void CheckVariables(Enemy enemy)
    {
        rigidbody = enemy.gameObject.GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = enemy.gameObject.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
        }
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (startPositionSet == false)
        {
            startPosition = rigidbody.position;
            movementDirection = (player.transform.position - enemy.transform.position).normalized;
            startPositionSet = true;
        }
    }
}
