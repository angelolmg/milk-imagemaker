using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCond : MonoBehaviour
{
    MeshFilter condMesh;
    Mesh mesh;

    void Start()
    {
        condMesh = GetComponent<MeshFilter>();
        mesh = condMesh.mesh;
        Vector2[] uvMap = mesh.uv;
        
        //order of vertices (inverse z)
        //bottom-left -> bottom-right -> top-left -> top-right
        //abscissa X, ordinate Y

        //Faces:
        //front
        uvMap[0] = new Vector2(0, 0.203f);
        uvMap[1] = new Vector2(0.305f, 0.203f);
        uvMap[2] = new Vector2(0, 0.797f);
        uvMap[3] = new Vector2(0.305f, 0.797f);


        //top
        uvMap[4] = new Vector2(0, 1);
        uvMap[5] = new Vector2(0.305f, 1);
        uvMap[8] = new Vector2(0, 0.797f);
        uvMap[9] = new Vector2(0.305f, 0.797f);
    
        //back
        uvMap[6] = new Vector2(0.203f, 0);
        uvMap[7] = new Vector2(0.797f, 0);
        uvMap[10] = new Vector2(0.797f, 0.305f);
        uvMap[11] = new Vector2(0.203f, 0.305f);

        //bottom
        uvMap[12] = new Vector2(0, 0);
        uvMap[13] = new Vector2(0.305f, 0);
        uvMap[14] = new Vector2(0, 0.203f);
        uvMap[15] = new Vector2(0.305f, 0.203f);

        //left
        uvMap[16] = new Vector2(0.203f, 0);
        uvMap[17] = new Vector2(0.797f, 0);
        uvMap[18] = new Vector2(0.797f, 0.305f);
        uvMap[19] = new Vector2(0.203f, 0.305f);

        //right
        uvMap[20] = new Vector2(0.203f, 0);
        uvMap[21] = new Vector2(0.797f, 0);
        uvMap[22] = new Vector2(0.797f, 0.305f);
        uvMap[23] = new Vector2(0.203f, 0.305f);

        mesh.uv = uvMap;
    }

}
