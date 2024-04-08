using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class PointCloudLoader : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] Shader PointCloudShader;
    [SerializeField] TextAsset PtsFile;
    [Range(0, 10)] public float PointRadius = 5;
    [Range(0, 100)] public float PointSize = 100;

    private ComputeBuffer posbuffer;
    private ComputeBuffer colbuffer;
    private Material material;
    private List<(Vector3, Vector3)> pts;
    private bool bufferReady = false;

    async void OnEnable()
    {
        if (PointCloudShader == null)
        {
            Debug.LogError("Point Cloud Shader Not Set!");
            return;
        }

        if (pts == null)
        {
            Debug.Log($"Start Load");
            pts = await PtsReader.Load(PtsFile);
            Debug.Log($"Point Count: [{pts.Count}]");
        }

        List<Vector3> positions = pts.Select(item => item.Item1).ToList();
        List<Vector3> colors = pts.Select(item => item.Item2).ToList();

        // �o�b�t�@�̈���m�ہE�Z�b�g
        // �m�ۂ���̈�T�C�Y�́A�f�[�^�� �~ �f�[�^�������̃T�C�Y
        int size = Marshal.SizeOf(new Vector3());
        posbuffer = new ComputeBuffer(positions.Count, size);
        colbuffer = new ComputeBuffer(colors.Count, size);
        posbuffer.SetData(positions);
        colbuffer.SetData(colors);

        // �S�̂�������ʒu�ɃJ�����𒲐�����
        Vector3 minVector = Vector3.positiveInfinity;
        Vector3 maxVector = -Vector3.positiveInfinity;
        for (int i = 0; i < positions.Count; i++)
        {
            minVector = Vector3.Min(positions[i], minVector);
            maxVector = Vector3.Max(positions[i], maxVector);
        }

        float maxLength = Mathf.Max(maxVector.x - minVector.x, maxVector.y - minVector.y, maxVector.z - minVector.z);
        Vector3 centerPos = (maxVector + minVector) / 2.0f;
        centerPos.y = minVector.y + 20.0f;
        Camera.transform.SetPositionAndRotation(centerPos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //centerPos.y += maxLength;
        //Camera.transform.SetPositionAndRotation(centerPos, Quaternion.Euler(90.0f, 0.0f, 0.0f));

        // �}�e���A�����쐬���o�b�t�@�ƃp�����[�^���Z�b�g
        material = new Material(PointCloudShader);
        material.SetBuffer("colBuffer", colbuffer);
        material.SetBuffer("posBuffer", posbuffer);
        ReflectParams();

        bufferReady = true;
        Debug.Log($"bufferReady: [{bufferReady}]");
    }

    void Update() {
        if (transform.hasChanged) {
            ReflectParams();
        }
    }

    void OnValidate() {
        ReflectParams();
    }

    public void ReflectParams() {
        if (material != null) {
            material.SetFloat("_Radius", PointRadius);
            material.SetFloat("_Size", PointSize);
            material.SetVector("_WorldPos", this.transform.position);
        }
    }

    public bool IsReady() {
        return this.bufferReady;
    }

    void OnRenderObject()
    {
        if (bufferReady)
        {
            // �����_�����O�̂��тɒ��_�̌����V�F�[�_�[�����s
            // MeshTopology.Points���w�肷�邱�ƂŁA�ʂł͂Ȃ����_���`�悳���            
            material.SetPass(0);
            Graphics.DrawProceduralNow(MeshTopology.Points, pts.Count);
        }
    }

    void OnDisable() {
        if (bufferReady) {
            posbuffer.Release();
            colbuffer.Release();
        }
        bufferReady = false;
    }
}
