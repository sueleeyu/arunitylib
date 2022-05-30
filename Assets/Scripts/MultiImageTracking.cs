using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class MultiImageTracking : MonoBehaviour
    {
        ARTrackedImageManager ImgTrackedManager;
        private Dictionary<string, GameObject> mPrefabs = new Dictionary<string, GameObject>();

        private void Awake()
        {
            ImgTrackedManager = GetComponent<ARTrackedImageManager>();
        }

        void Start()
        {
            mPrefabs.Add("Chain", Resources.Load("Chain") as GameObject);
            mPrefabs.Add("Lipstick", Resources.Load("Lipstick") as GameObject);
        }

        private void OnEnable()
        {
            ImgTrackedManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
        void OnDisable()
        {
            ImgTrackedManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                OnImagesChanged(trackedImage);
            }
            // foreach (var trackedImage in eventArgs.updated)
            // {
            //     OnImagesChanged(trackedImage.referenceImage.name);
            // }
        }

        private void OnImagesChanged(ARTrackedImage referenceImage)
        {
            Debug.Log("Image name:" + referenceImage.referenceImage.name);
            Instantiate(mPrefabs[referenceImage.referenceImage.name], referenceImage.transform);
        }

        public void ClearInstance()
        {
            Logger.Log("clearInstance");

            foreach (var key in mPrefabs.Keys)
            {
                Destroy(mPrefabs[key]);
                mPrefabs.Remove(key);
            }

        }
    }
}
