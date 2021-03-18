using System;
using System.Collections;
using UnityEngine;

namespace EvertaleGames
{
  [RequireComponent(typeof(Collider2D))]
  public class Checkpoint : MonoBehaviour
  {
    [SerializeField] bool isDefault = false;
    [SerializeField] Transform respawnPosition = null;
    [SerializeField] float respawnSpeed = 2f;

    void Awake()
    {
      if (isDefault)
      {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
          monoBehaviour.TryGetComponent<ICheckpointEntity>(out ICheckpointEntity entity);
          if (entity != null) Save(entity);
        }
      }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
      if (col.TryGetComponent<ICheckpointEntity>(out ICheckpointEntity entity)) Save(entity);
    }

    void Save(ICheckpointEntity entity)
    {
      // Don't subscribe to event twice
      entity.LoadCheckpoint -= Load;
      entity.LoadCheckpoint += Load;
    }

    void Load(GameObject obj, Action callback)
    {
      StartCoroutine(LoadCoroutine(obj, callback));
    }

    IEnumerator LoadCoroutine(GameObject obj, Action callback)
    {
      float delta = 0f;
      print(obj.name);
      while (Vector2.Distance(obj.transform.position, respawnPosition.position) > Mathf.Epsilon)
      {
        delta += Time.deltaTime;
        obj.transform.position = Vector2.MoveTowards(obj.transform.position, respawnPosition.position, delta * respawnSpeed);

        yield return null;
      }

      callback();
    }
  }
}
