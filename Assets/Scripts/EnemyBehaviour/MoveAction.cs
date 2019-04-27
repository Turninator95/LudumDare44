using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Move"), System.Serializable]
public class MoveAction : EnemyActions
{
    [SerializeField]
    private float moveDistance;
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
            startPositionSet = true;
        }
        if (Vector3.Distance(startPosition, enemy.transform.position) < moveDistance)
        {
            rigidbody.velocity = enemy.transform.forward  * enemy.MovementSpeed;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            startPositionSet = false;
            enemy.ActionCompleted();
        }
    }
}
