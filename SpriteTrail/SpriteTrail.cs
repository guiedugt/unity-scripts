using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvertaleGames
{
  [RequireComponent(typeof(SpriteRenderer))]
  public class SpriteTrail : MonoBehaviour
  {
    [SerializeField] Color startColor = Color.white;
    [SerializeField] Color endColor = Color.clear;
    [SerializeField] [Range(0.05f, 1f)] float interval = 0.1f;
    [SerializeField] [Range(0.01f, 0.5f)] float minDistance = 0.05f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Coroutine trailCoroutine;
    Vector2 lastPosition;
    List<SpriteRenderer> list = new List<SpriteRenderer>();

    void Awake()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      lastPosition = transform.position;
    }

    void OnEnable()
    {
      trailCoroutine = StartCoroutine(TrailCoroutine());
    }

    void OnDisable()
    {
      StopCoroutine(trailCoroutine);
    }

    IEnumerator TrailCoroutine()
    {
      WaitForSeconds wait = new WaitForSeconds(interval);

      while (true)
      {
        if (ShouldCreateSpriteTrail())
          CreateTrailSprite();

        lastPosition = transform.position;
        yield return wait;
      }
    }

    bool ShouldCreateSpriteTrail()
    {
      Vector2 lastSpriteTrailPosition =
        list.Count > 0
          ? (Vector2)list[list.Count - 1].gameObject.transform.position
          : lastPosition;

      return Vector2.Distance(transform.position, lastSpriteTrailPosition) >= minDistance;
    }

    SpriteRenderer CreateTrailSprite()
    {
      GameObject instance = new GameObject("Trail");
      instance.transform.position = transform.position;
      instance.transform.rotation = transform.rotation;

      SpriteRenderer instanceSpriteRenderer = instance.AddComponent<SpriteRenderer>();
      instanceSpriteRenderer.sprite = spriteRenderer.sprite;
      instanceSpriteRenderer.color = spriteRenderer.color;
      instanceSpriteRenderer.flipX = spriteRenderer.flipX;
      instanceSpriteRenderer.flipY = spriteRenderer.flipY;
      instanceSpriteRenderer.drawMode = spriteRenderer.drawMode;
      instanceSpriteRenderer.maskInteraction = spriteRenderer.maskInteraction;
      instanceSpriteRenderer.spriteSortPoint = spriteRenderer.spriteSortPoint;
      instanceSpriteRenderer.material = spriteRenderer.material;
      instanceSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
      instanceSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
      instanceSpriteRenderer.renderingLayerMask = spriteRenderer.renderingLayerMask;

      list.Add(instanceSpriteRenderer);
      StartCoroutine(FadeCoroutine(instanceSpriteRenderer));

      return instanceSpriteRenderer;
    }

    IEnumerator FadeCoroutine(SpriteRenderer instanceSpriteRenderer)
    {
      instanceSpriteRenderer.color = startColor;

      float delta = 0f;
      while (instanceSpriteRenderer.color != endColor)
      {
        delta += Time.deltaTime;
        instanceSpriteRenderer.color = Color.Lerp(startColor, endColor, delta);

        yield return null;
      }

      list.RemoveAt(0);
      Destroy(instanceSpriteRenderer.gameObject);
    }
  }
}
