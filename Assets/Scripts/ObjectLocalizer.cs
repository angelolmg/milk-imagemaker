using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLocalizer : MonoBehaviour
{

    Camera cam;
    Plane[] planes;
    GameObject[] itemPool;
    ScreenshotManager screenshotManager;
    
    Vector3[] VerticesFromObjectCollider(GameObject obj)
    {
        BoxCollider b = obj.GetComponent<BoxCollider>(); //retrieves the Box Collider of the GameObject called obj
        Vector3[] vertices = new Vector3[8];

        vertices[0] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z)*0.5f);
        vertices[1] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z)*0.5f);
        vertices[2] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z)*0.5f);
        vertices[3] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z)*0.5f);
        vertices[4] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z)*0.5f);
        vertices[5] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, -b.size.z)*0.5f);
        vertices[6] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, b.size.z)*0.5f);
        vertices[7] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, b.size.z)*0.5f);

        return vertices;
    }

    Vector2[] ScreenSpaceCornersFromVertices(Vector3[] vertices)
    {
        Vector2[] screenSpaceCorners = new Vector2[8];
        for (int i = 0; i < 8; i++)
            screenSpaceCorners[i] = cam.WorldToScreenPoint(vertices[i]);
        
        return screenSpaceCorners;
    }

    Rect RectFromScreenSpaceCorners(Vector2[] screenSpaceCorners){
        
        float min_x = screenSpaceCorners[0].x;
        float min_y = screenSpaceCorners[0].y;
        float max_x = screenSpaceCorners[0].x;
        float max_y = screenSpaceCorners[0].y;

        for (int i = 1; i < 8; i++) {

            if(screenSpaceCorners[i].x < min_x) {
                min_x = screenSpaceCorners[i].x;
            }
            if(screenSpaceCorners[i].y < min_y) {
                min_y = screenSpaceCorners[i].y;
            }
            if(screenSpaceCorners[i].x > max_x) {
                max_x = screenSpaceCorners[i].x;
            }
            if(screenSpaceCorners[i].y > max_y) {
                max_y = screenSpaceCorners[i].y;
            }
        }

        min_x = Mathf.Clamp(min_x, 0, Screen.width);
        min_y = Mathf.Clamp(min_y, 0, Screen.height);
        max_x = Mathf.Clamp(max_x, 0, Screen.width);
        max_y = Mathf.Clamp(max_y, 0, Screen.height);

        return Rect.MinMaxRect( min_x, min_y, max_x, max_y );
    }

    public int Localize()
    {
        int detectedObjs = 0;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Product");
        List<LabelingInfo> labelingInfos = new List<LabelingInfo>();

        foreach (GameObject obj in objects)
        {
            if (GeometryUtility.TestPlanesAABB(planes, obj.GetComponent<Collider>().bounds))
            {

                Vector3[] vertices = VerticesFromObjectCollider(obj);
                Vector2[] screenSpace = ScreenSpaceCornersFromVertices(vertices);
                Rect objectRect = RectFromScreenSpaceCorners(screenSpace);

                for (int i = 0; i < itemPool.Length; i++){
                    if(itemPool[i].GetComponent<ObjectInfo>().objectName == obj.GetComponent<ObjectInfo>().objectName){
                        LabelingInfo li = new LabelingInfo(i, objectRect.center.x/Screen.width,
                                                              (1 - objectRect.center.y/Screen.height),
                                                              objectRect.width/Screen.width,
                                                              objectRect.height/Screen.height);

                        float threshold = 0.08f;
                        if(li.width < threshold || li.height < threshold)
                            continue;
                            
                        labelingInfos.Add(li);
                        
                    }
                }
                 
                detectedObjs++;
            }

        }
        
        if(detectedObjs > 0)
            screenshotManager.TakeScreenshotAndLabel(labelingInfos);
        
        return detectedObjs;

    }

    void Start()
    {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        itemPool = GameObject.FindObjectOfType<spawnitem>().items;
        screenshotManager = GameObject.FindObjectOfType<ScreenshotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Take screenshot once
        if (Input.GetKeyDown("space"))
        {
            print("Localizing objects...");
            Localize();
        }
     
    }
}
