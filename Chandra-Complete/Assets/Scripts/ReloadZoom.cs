namespace Mapbox.Examples
{
    using Mapbox.Geocoding;
    using UnityEngine.UI;
    using Mapbox.Unity.Map;
    using UnityEngine;
    using System;
    using System.Collections;
    using UnityEngine.SceneManagement;

    public class ReloadZoom : MonoBehaviour
    {
        Camera _camera;
        Vector3 _cameraStartPos;
        AbstractMap _map;

        //[SerializeField]
        //ForwardGeocodeUserInput _forwardGeocoder;

        [SerializeField]
        Slider _zoomSlider;

        private HeroBuildingSelectionUserInput[] _heroBuildingSelectionUserInput;

        Coroutine _reloadRoutine;

        WaitForSeconds _wait;

        public string nextScene;

        void Awake()
        {
            _camera = Camera.main;
            _cameraStartPos = _camera.transform.position;
            _map = FindObjectOfType<AbstractMap>();
            if (_map == null)
            {
                Debug.LogError("Error: No Abstract Map component found in scene.");
                return;
            }
            if (_zoomSlider != null)
            {
                _map.OnUpdated += () => { _zoomSlider.value = _map.Zoom; };
                _zoomSlider.onValueChanged.AddListener(Reload);
            }

            _wait = new WaitForSeconds(.3f);
        }

        void Update() 
        {
            if (_map.Zoom == 15) {
                SceneManager.LoadScene(nextScene);
            }
        }

        void Reload(float value)
        {
            if (_reloadRoutine != null)
            {
                StopCoroutine(_reloadRoutine);
                _reloadRoutine = null;
            }
            _reloadRoutine = StartCoroutine(ReloadAfterDelay((int)value));
        }

        IEnumerator ReloadAfterDelay(int zoom)
        {
            yield return _wait;
            _camera.transform.position = _cameraStartPos;
            _map.UpdateMap(_map.CenterLatitudeLongitude, zoom);
            _reloadRoutine = null;
        }
    }
}