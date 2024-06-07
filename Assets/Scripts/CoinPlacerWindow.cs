/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CoinPlacerWindow : EditorWindow
{

    bool placeObject;
    Object ObjectToInstiate;
    Transform under;
    float offset = 0;

    [MenuItem("Window/CoinPlace")]
    static void Init()
    {
        CoinPlacerWindow coinPlacerWindow = (CoinPlacerWindow)EditorWindow.GetWindow(typeof(CoinPlacerWindow));
        coinPlacerWindow.Show();
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui -= CustomUpdate;
        SceneView.duringSceneGui += CustomUpdate;
    }


    private void OnGUI()
    {
        placeObject = GUILayout.Toggle(placeObject, "PlaceCoins", "button");

        ObjectToInstiate = EditorGUILayout.ObjectField(ObjectToInstiate, typeof(GameObject), true);
        under = EditorGUILayout.ObjectField(under, typeof(Transform), true) as Transform;
        offset = EditorGUILayout.FloatField(offset);

    }

    void CustomUpdate(SceneView cv)
    {

        Event e = Event.current;

        if ((e.type == EventType.MouseDown) && e.button == 0)
        {
            if (placeObject)
            {
                RaycastHit hit;
                Tools.current = Tool.View;
                int layer = 1 << 8;
                if (Physics.Raycast(Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x,
                    Camera.current.pixelHeight - e.mousePosition.y, 0)), out hit, Mathf.Infinity, layer))
                {
                    Object parent = PrefabUtility.GetCorrespondingObjectFromSource((GameObject)ObjectToInstiate);
                    GameObject placeobject = (GameObject)PrefabUtility.InstantiatePrefab(parent == null ? ObjectToInstiate : parent);
                    placeobject.transform.position = hit.point + new Vector3(0f, offset, 0f);
                    placeobject.transform.SetParent(under.transform);
                    e.Use();
                    Undo.RegisterCreatedObjectUndo(placeobject, "undo Sphare");
                }
            }
        }
    }
}
*/

