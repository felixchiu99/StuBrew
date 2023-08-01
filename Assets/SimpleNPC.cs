using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNPC : MonoBehaviour
{
    [SerializeField] Transform SelfQueuePos;
    [SerializeField] Transform target;
    [SerializeField] Transform lookAt;

    public float waypointDeviation = 0.1f;
    public float slowDownDeviation = 0.3f;
    public float rotationSpeed = 1f;
    public float moveSpeed = 1f;
    public float maxVelocity = 1f;

    bool isInQueue = false;

    int stuckCounter = 0;

    Rigidbody rigidbody;
    [SerializeField] Collider collider;
    QueueManager queueManager;

    PhysicMaterial original;
    [SerializeField] PhysicMaterial modded;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (!queueManager)
            queueManager = (QueueManager)FindObjectOfType(typeof(QueueManager));

        queueManager.OnQueueEndChange += ChangeQueueTarget;
        if(target == null)
        {
            target = queueManager.GetLastPlace();
        }
        if (lookAt == null)
        {
            lookAt = queueManager.GetLastLookAt();
        }
    }

    void Start()
    {
        original = collider.sharedMaterial;
    }

    void Update()
    {
        float dist = DistToTarget();
        if (dist > waypointDeviation)
        {
            Rotate(target);
            Move(dist > slowDownDeviation ? 1 : 0.1f);
        }
        else
        {
            if (!isInQueue)
            {
                isInQueue = true;
                queueManager.AddToQueueEnd(gameObject);
            }
            if(lookAt != null)
                Rotate(lookAt);
            Stop();
        }

        if (Vector3.Angle(rigidbody.velocity.normalized, GetTargetDirection(target).normalized) > 30)
        {
            Stop();
        }

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, GetTargetDirection(target), Color.red);

        if(lookAt)
            Debug.DrawRay(transform.position, GetTargetDirection(lookAt), Color.blue);
        if (!lookAt)
            queueManager.FindLookAt(this);
    }

    void Move(float moveSpeedMod = 1)
    {
        if (rigidbody.velocity.magnitude < maxVelocity)
        {
            collider.sharedMaterial = original;
            rigidbody.AddForce(GetTargetDirection(target).normalized * moveSpeed * moveSpeedMod, ForceMode.VelocityChange);
        }
        if(rigidbody.velocity.magnitude < 0.1f && !isInQueue)
        {
            stuckCounter++;
        }
        else
        {
            stuckCounter = 0;
        }
        if(stuckCounter > 10)
        {
            rigidbody.AddForce(transform.right * -0.2f, ForceMode.VelocityChange);
        }
    }
    void Stop()
    {
        collider.sharedMaterial = modded;
    }
    bool Rotate(Transform lookTarget)
    {
        Vector3 targetDirection = GetTargetDirection(lookTarget);

        // The step size is equal to speed times frame time.
        float singleStep = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        var angle = Vector3.Angle(transform.forward, newDirection);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        return angle != 0;

    }

    float DistToTarget()
    {
        var object1 = transform.position;
        var object2 = target.position;

        object1.y = 0;
        object2.y = 0;

        return Vector3.Distance(object1, object2);
    }

    Vector3 GetTargetDirection(Transform lookTarget)
    {
        var object1 = transform.position;
        var object2 = lookTarget.position;

        object1.y = 0;
        object2.y = 0;

        return object2 - object1;
    }

    void ChangeQueueTarget(Transform queueEnd, Transform lookAt)
    {
        if (!isInQueue)
        {
            Stop();
            stuckCounter = 0;
            UpdateQueueTarget(queueEnd, lookAt);
        }
    }

    public void UpdateQueueTarget(Transform queueEnd, Transform lookAt)
    {
        target = queueEnd;
        this.lookAt = lookAt;
    }

    public Transform GetQueuePos()
    {
        return SelfQueuePos;
    }

    public void Destroy(float time)
    {
        Destroy(gameObject, time);
    }
}
