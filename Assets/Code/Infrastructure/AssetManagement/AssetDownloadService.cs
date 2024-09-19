using System;
using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetDownloadService : IAssetDownloadService
  {
    private readonly IAssetDownloadReporter _downloadReporter;
    private List<IResourceLocator> _catalogsLocators;
    private long _downloadSize;

    public AssetDownloadService(IAssetDownloadReporter downloadReporter)
    {
      _downloadReporter = downloadReporter;
    }
    
    public async UniTask InitializeDownloadDataAsync()
    {
      await Addressables.InitializeAsync().ToUniTask();
      await UpdateCatalogsAsync();
      await UpdateDownloadSizeAsync();
    }
    
    public float GetDownloadSizeMb() => 
      SizeToMb(_downloadSize);

    public async UniTask UpdateContentAsync()
    {
      if (_catalogsLocators == null) 
        await UpdateCatalogsAsync();
      
      IList<IResourceLocation> locations = await RefreshResourceLocations(_catalogsLocators);
      if(locations.IsNullOrEmpty())
        return;

      try
      {
        await DownloadContentWithPreciseProgress(locations);
      }
      catch (Exception e)
      {
        Debug.LogError(e);
      }
    }

    private async UniTask DownloadContent(IList<IResourceLocation> locations)
    {
      UniTask downloadTask = Addressables
        .DownloadDependenciesAsync(locations, autoReleaseHandle: true)
        .ToUniTask(progress: _downloadReporter);

      await downloadTask;

      if (downloadTask.Status.IsFaulted())
        Debug.LogError("Error while downloading catalog dependencies");

      _downloadReporter.Reset();
    }

    private async UniTask DownloadContentWithPreciseProgress(IList<IResourceLocation> locations)
    {
      AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(locations);
      
      while (!downloadHandle.IsDone && downloadHandle.IsValid())
      {
        await UniTask.Delay(100);
        _downloadReporter.Report(downloadHandle.GetDownloadStatus().Percent);
      }
      
      _downloadReporter.Report(1);
      if (downloadHandle.Status == AsyncOperationStatus.Failed) 
        Debug.LogError("Error while downloading catalog dependencies");

      if(downloadHandle.IsValid())
        Addressables.Release(downloadHandle);
      
      _downloadReporter.Reset();
    }

    private async UniTask UpdateCatalogsAsync()
    {
      List<string> catalogsToUpdate = await Addressables.CheckForCatalogUpdates().ToUniTask();
      if (catalogsToUpdate.IsNullOrEmpty())
      {
        _catalogsLocators = Addressables.ResourceLocators.ToList();
        return;
      }

      _catalogsLocators = await Addressables.UpdateCatalogs(catalogsToUpdate).ToUniTask();
    }

    private async UniTask UpdateDownloadSizeAsync()
    {
      IList<IResourceLocation> locations = await RefreshResourceLocations(_catalogsLocators);

      if(locations.IsNullOrEmpty())
        return;

      _downloadSize = await Addressables
        .GetDownloadSizeAsync(locations)
        .ToUniTask();
    }

    private async UniTask<IList<IResourceLocation>> RefreshResourceLocations(IEnumerable<IResourceLocator> locators)
    {
      IEnumerable<object> keysToCheck = locators.SelectMany(x => x.Keys);

      return await Addressables
        .LoadResourceLocationsAsync(keysToCheck, Addressables.MergeMode.Union)
        .ToUniTask();
    }

    private static float SizeToMb(long downloadSize) => downloadSize * 1f / 1048576;
  }
}