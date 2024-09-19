using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
  public class DownloadBar : MonoBehaviour
  {
    [SerializeField] private Image _imageFill;

    public void SetProgress(float percentageProgress)
    {
      _imageFill.fillAmount = percentageProgress;
    }
  }
}