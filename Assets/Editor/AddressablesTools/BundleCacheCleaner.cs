using UnityEditor;
using UnityEngine;

namespace Editor
{
  public class BundleCacheCleaner
  {
    [MenuItem("Tools/Clear Addressable bundles cache")]
    private static void ClearBundleCache()
    {
      Caching.ClearCache();
    }
  }
}