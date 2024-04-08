using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour {

    [SerializeField] GameObject PointCloudAnchor;
    [SerializeField] GameObject canvas;
    [SerializeField] Text mainText;
    private PointCloudLoader loader;
    private bool loaded = false;


    private PointCloudLoader GetLoader() {
        if (!loader) {
            loader = PointCloudAnchor.GetComponent<PointCloudLoader>();
        }
        return loader;
    }

    private bool IsUIAccessible() {
        if (loaded) {
            return true;
        }

        if (GetLoader().IsReady()) {
            // 初めてReadyになった瞬間のみ到達
            canvas.SetActive(false);
            loaded = true;
            return true;
        }

        return false;
    }

    void Update() {
        var loader = GetLoader();

        if (!IsUIAccessible()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            loader.PointRadius -= 10;
            UpdateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            loader.PointRadius += 10;
            UpdateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            canvas.SetActive(false);
        }
        //if (OVRInput.GetDown(OVRInput.RawButton.X) || Input.GetKeyDown(KeyCode.X))
    }

    private void UpdateCanvas()
    {
        loader.ReflectParams();
        canvas.SetActive(true);
        mainText.text = $"Point Size: {loader.PointRadius}\n(Press B to close)";
    }
}
