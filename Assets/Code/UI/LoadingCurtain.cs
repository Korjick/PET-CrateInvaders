using System;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    public Canvas Canvas;
    public DownloadBar DownloadBar;
    private IAssetDownloadReporter _downloadReporter;

    [Inject]
    private void Construct(IAssetDownloadReporter downloadReporter)
    {
      _downloadReporter = downloadReporter;
    }

    private void Awake()
    {
      _downloadReporter.ProgressUpdated += DisplayDownloadProgress;
    }

    public void Show()
    {
      Canvas.enabled = true;
    }

    public void Hide()
    {
      Canvas.enabled = false;
      DownloadBar.gameObject.SetActive(false);
    }

    private void DisplayDownloadProgress()
    {
      DownloadBar.gameObject.SetActive(true);
      DownloadBar.SetProgress(_downloadReporter.Progress);
    }

    private void OnDestroy()
    {
      _downloadReporter.ProgressUpdated -= DisplayDownloadProgress;
    }
  }
}