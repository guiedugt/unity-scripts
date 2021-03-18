using System;
using UnityEngine;

namespace EvertaleGames
{
  public delegate void LoadCheckpointEventHandler(GameObject obj, Action callback = null);

  public interface ICheckpointEntity
  {
    event LoadCheckpointEventHandler LoadCheckpoint;
  }
}
