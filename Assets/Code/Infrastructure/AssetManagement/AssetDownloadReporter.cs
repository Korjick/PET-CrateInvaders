using System;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetDownloadReporter : IAssetDownloadReporter
  {
    private const float UpdateThreshold = 0.01f;
    
    public float Progress { get; private set; }
    public event Action ProgressUpdated;
    
    public void Report(float value)
    {
      if (Mathf.Abs(Progress - value) < UpdateThreshold)
        return;
      
      Progress = value;
      ProgressUpdated?.Invoke();
    }

    public void Reset()
    {
      Progress = 0;
    }
  }
}