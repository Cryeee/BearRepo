using UnityEngine;
using System.Collections;

public class FixDeformations : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public MeshFilter bakedMeshFilter;
    public bool updateNormal;
    Mesh bakedMesh;
    public int num;

    void Awake()
    {
        num = 1;
        bakedMesh = bakedMeshFilter.mesh;
    }

    void Update()
    {
        if (num == 1)
        {
            skinnedMeshRenderer.BakeMesh(bakedMesh);

            if (updateNormal)
            {
                bakedMesh.RecalculateNormals2(180);
            }
            num = 2;
        }
        else
        {
            num = 1;
        }
    }
}