using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class CollisionEventEntry
{
    public CollisionTag collisionTag;
    public UnityEvent collisionEvent;
}

public class TriggerEventHandler : MonoBehaviour
{
    [SerializeField] private List<CollisionEventEntry> collisionEventEntries;
    [SerializeField] private UnityEvent defaultEvent;

    private Dictionary<CollisionTag, UnityEvent> collisionEvents;

    private void Awake()
    {
        collisionEvents = new Dictionary<CollisionTag, UnityEvent>();
        collisionEventEntries.ForEach(entry => collisionEvents.Add(entry.collisionTag, entry.collisionEvent));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionTag collisionTag = GetCollisionTag(collision.tag);
        (collisionEvents.TryGetValue(collisionTag, out UnityEvent eventToInvoke) ? eventToInvoke : defaultEvent)?.Invoke();
    }

    private CollisionTag GetCollisionTag(string tag)
    {
        return System.Enum.TryParse(tag, out CollisionTag collisionTag) ? collisionTag : CollisionTag.Player;
    }
}
