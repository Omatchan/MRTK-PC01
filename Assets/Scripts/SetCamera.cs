using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    [SerializeField] Camera Camera;


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DelayMethod), 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DelayMethod()
    {
        var pos = new Vector3(125.747765f, 48.5f, 38.831459f);
        var qua = new Quaternion(0.168716088f, -0.360981047f, 0.0665780976f, 0.914764941f);
        this.Camera.transform.SetLocalPositionAndRotation(pos, qua);
        Debug.Log("Delay call");
    }
    private void OnDestroy()
    {
        // DestroyŽž‚É“o˜^‚µ‚½Invoke‚ð‚·‚×‚ÄƒLƒƒƒ“ƒZƒ‹
        CancelInvoke();
    }
}
