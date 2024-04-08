using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charcontroller : MonoBehaviour
{
    public float mainSPEED;
    private int cameraCount;
    void Start()
    {
        mainSPEED = 50.0F;
        cameraCount = 0;
    }

    void Update()
    {
        Transform trans = transform;
        transform.position = trans.position;
        // trans.position += trans.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * (mainSPEED / 100.0F);
        // trans.position += trans.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * (mainSPEED / 100.0F);
        if(Input.GetKey(KeyCode.W)){
            trans.position += trans.TransformDirection(Vector3.forward) * (mainSPEED / 5.0F) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)){
            trans.position += trans.TransformDirection(Vector3.forward) * (-mainSPEED / 5.0F)  * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)){
            trans.position += trans.TransformDirection(Vector3.right) * (-mainSPEED / 5.0F) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)){
            trans.position += trans.TransformDirection(Vector3.right) * (mainSPEED / 5.0F)  * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Q)){
            trans.position += trans.TransformDirection(Vector3.up) * (-mainSPEED / 5.0F) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.E)){
            trans.position += trans.TransformDirection(Vector3.up) * (mainSPEED / 5.0F)  * Time.deltaTime;
        }

        // 回転
        if(Input.GetKey(KeyCode.T)){
            trans.Rotate(-mainSPEED * 1.0F * Time.deltaTime,0,0);
        }
        if(Input.GetKey(KeyCode.G)){
            trans.Rotate(mainSPEED * 1.0F * Time.deltaTime,0,0);
        }
        if(Input.GetKey(KeyCode.H)){
            trans.Rotate(0,0,mainSPEED * 1.0F * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.F)){
            trans.Rotate(0,0,-mainSPEED * 1.0F * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.R)){
            trans.Rotate(0,-mainSPEED * 1.0F * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.Y)){
            trans.Rotate(0,mainSPEED * 1.0F * Time.deltaTime, 0);
        }

        // カメラ位置
        if(Input.GetKey(KeyCode.C)){
            cameraCount += 1;
            if (cameraCount > 5) cameraCount = 0;

            switch (cameraCount)
            {
                case 1:
                    //trans.position = new Vector3(0.0f, 0.0f, 2.0f);
                    trans.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                    break;
                case 2:
                    //trans.position = new Vector3(0.0f, 0.11f, 2.0f);
                    trans.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                    break;
                case 3:
                    //trans.position = new Vector3(0.0f, 0.0f, 2.0f);
                    trans.rotation = Quaternion.Euler(-270.0f, 90.0f, -90.0f);
                    break;
                case 4:
                    //trans.position = new Vector3(0.0f, -0.15f, 2.0f);
                    trans.rotation = Quaternion.Euler(-270.0f, 90.0f, -90.0f);
                    break;
                case 5:
                    //trans.position = new Vector3(-0.02819737f, -0.1162298f, 1.970625f);
                    trans.rotation = Quaternion.Euler(-247.615f, 89.99999f, -90.0f);
                    break;
                default:
                    //trans.position = new Vector3(0.0f, 0.0f, 0.0f);
                    trans.rotation = Quaternion.Euler(0.0f, 0.0f, 1.0f);
                    break;
            }
        }

    }
}
