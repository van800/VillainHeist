using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FloorBuilder : EditorWindow
{
    private float objSizeX = 1;
    private float objSizeZ = 1;
    private Texture2D image;
    private Vector3 buildPosition = new Vector3(0, 0, 0);
    private readonly static string windowName = "Floor Builder";
    private Dictionary<Color, GameObject> colorToObj = new Dictionary<Color, GameObject>();
    [Header("Adding a new Color Pair")]
    private Color addColor = new Color(0,0,0);
    private GameObject addObj;
    [Header("Walls")]
    private bool usingWalls = false;
    private Color wallColor;
    private GameObject wallObj;
    

    [MenuItem("Window/Floor Builder")]
    public static void ShowWindow()
    {
        GetWindow<FloorBuilder>(windowName);
    }

    private void OnGUI()
    {
        //Window Code
        GUILayout.Label("Welcome to the Floor Builder!", EditorStyles.largeLabel);
        DisplayColorToObj();
        objSizeX = EditorGUILayout.FloatField("Object Size X", objSizeX);
        objSizeZ = EditorGUILayout.FloatField("Object Size Z", objSizeZ);
        image = (Texture2D)EditorGUILayout.ObjectField(image, typeof(Texture2D));
        buildPosition = EditorGUILayout.Vector3Field("Floor Position", buildPosition);


        if (GUILayout.Button("Build Floor"))
        {
            BuildFloorAndWalls(colorToObj, usingWalls, wallColor, wallObj, buildPosition, image);
        }
    }

    private void DisplayColorToObj()
    {
        usingWalls = GUILayout.Toggle(usingWalls, "Using walls?");
        if (usingWalls)
        {
            GUILayout.Label("Wall Color-Object Pair", EditorStyles.label);
            GUILayout.BeginHorizontal();
                wallColor = EditorGUILayout.ColorField(wallColor);
                wallObj = (GameObject)EditorGUILayout.ObjectField("Wall Facing North", wallObj, typeof(GameObject));
            GUILayout.EndHorizontal();
        }

        GUILayout.Label("Current Colors-Object Pairs", EditorStyles.label);
        List<Color> colorKeys = new List<Color>(colorToObj.Keys);
        for (int i = 0; i < colorKeys.Count; i++)
        {
            GUILayout.BeginHorizontal();
                Color curColor = colorKeys[i];
                GameObject curObj = colorToObj[colorKeys[i]];
                // Field for Color
                Color newColor = EditorGUILayout.ColorField(curColor);
                if (!newColor.Equals(colorKeys[i]))
                {
                    if (colorToObj.ContainsKey(newColor))
                    {
                        // Ignore for now
                    }
                    else
                    {
                        colorToObj.Add(newColor, colorToObj[curColor]);
                        colorToObj.Remove(curColor);
                        curColor = newColor;
                    }
                }
                // Field for GameObject
                colorToObj[curColor] = (GameObject)EditorGUILayout.ObjectField(curObj, typeof(GameObject));
                // Button to remove
                if (GUILayout.Button("Remove Pair"))
                {
                    colorToObj.Remove(colorKeys[i]);
                }
            GUILayout.EndHorizontal();
        }

        GUILayout.Label("Add New Color-Object Pair", EditorStyles.label);
        GUILayout.BeginHorizontal();
            addColor = EditorGUILayout.ColorField(addColor);
            addObj = (GameObject)EditorGUILayout.ObjectField(addObj, typeof(GameObject));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add new Color to GameObject"))
        {
            if (colorToObj.ContainsKey(addColor))
            {
                // Ignore for now
            }
            else
            {
                colorToObj.Add(addColor, addObj);
            }
        }
    }

    private void BuildFloorAndWalls(Dictionary<Color, GameObject> colorToObj, bool usingWalls, Color wallColor, GameObject wallObj, Vector3 floorPos, Texture2D image)
    {
        GameObject floorWrapper = new GameObject("Floor");
        floorWrapper.transform.position = floorPos;
        int width = image.width;
        int height = image.height;
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log("color = " + image.GetPixel(x, y).b + " = " + new List<Color>(colorToObj.Keys)[0].b);
                Debug.Log("color = " + image.GetPixel(x, y).g + " = " + new List<Color>(colorToObj.Keys)[0].g);
                if (colorToObj.ContainsKey(image.GetPixel(x, y)))
                {
                    GameObject newFloorPreFab = colorToObj[image.GetPixel(x, y)];
                    GameObject newFloorObj = Instantiate(newFloorPreFab, floorWrapper.transform, false);
                    newFloorObj.transform.localPosition = new Vector3(objSizeX * (x - halfWidth), 0, objSizeZ * (y - halfHeight));
                    if (usingWalls)
                    {
                        BuildSuroundingWalls(wallColor, wallObj, floorWrapper, image, x, y);
                    }
                }
                else
                {
                    //throw new IllegalColorToFloorException();
                }
            }
        }
    }

    private void BuildSuroundingWalls(Color wallColor, GameObject wallObj, GameObject floorWrapper, Texture2D image, int x, int y)
    {
        BuildWall(wallColor, wallObj, floorWrapper, image, x + 1, y, 90);
        BuildWall(wallColor, wallObj, floorWrapper, image, x - 1, y, 270);
        BuildWall(wallColor, wallObj, floorWrapper, image, x, y + 1, 0);
        BuildWall(wallColor, wallObj, floorWrapper, image, x, y - 1, 180);
    }

    private void BuildWall(Color wallColor, GameObject wallObj, GameObject floorWrapper, Texture2D image, int x, int y, float eulerAngle)
    {
        int width = image.width;
        int height = image.height;
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (image.GetPixel(x, y).Equals(wallColor))
            {
                GameObject newWallObj = Instantiate(wallObj, floorWrapper.transform, false);
                newWallObj.transform.localPosition = new Vector3(objSizeX * (x - halfWidth), 0, objSizeZ * (y - halfHeight));
                newWallObj.transform.rotation = Quaternion.Euler(new Vector3(0, eulerAngle, 0));
            }
        }
    }
}

class IllegalColorToFloorException : Exception
{
    
}
