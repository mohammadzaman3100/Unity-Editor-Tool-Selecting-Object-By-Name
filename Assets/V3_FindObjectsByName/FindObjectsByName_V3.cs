using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public class FindObjectsByName_V3 : EditorWindow
{
    // These string fields allow the user to enter the name of the script or GameObject to search for
    private string scriptName = "Example";
    private string gameObjectName = "Example";

    // This function is called when the user clicks the "Select Scripts" button in the editor window
    [MenuItem("Window/Editor Plugins/Find Scripts By Name")]
    public static void SelectScripts()
    {
        // Get a list of all GameObjects in the scene
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        // Create a new list to store the GameObjects that match the search criteria
        List<GameObject> matchingGameObjects = new List<GameObject>();

        // Get the search criteria from the ExampleSelector window
        FindObjectsByName_V3 window = (FindObjectsByName_V3)GetWindow(typeof(FindObjectsByName_V3));
        string scriptName = window.scriptName;
        string gameObjectName = window.gameObjectName;

        // Iterate through the list of GameObjects
        foreach (GameObject gameObject in gameObjects)
        {
            // Check if the GameObject has a script component with the specified name, or if the GameObject's name matches the specified name
            if ((gameObject.GetComponent(scriptName) != null) || (gameObject.name == gameObjectName))
            {
                // If it does, add it to the list
                matchingGameObjects.Add(gameObject);
            }
        }

        // Select the GameObjects that match the search criteria
        Selection.objects = matchingGameObjects.ToArray();
    }

    // This function is called when the editor window is displayed
    void OnGUI()
    {
        // Create labels and text fields for the script name and GameObject name
        GUILayout.Label("Script Name:", EditorStyles.boldLabel);
        scriptName = EditorGUILayout.TextField(scriptName);
        if (GUILayout.Button("Select Scripts"))
        {
            SelectScripts();
        }

        GUILayout.Label("GameObject Name:", EditorStyles.boldLabel);
        gameObjectName = EditorGUILayout.TextField(gameObjectName);
        if (GUILayout.Button("Select GameObjects"))
        {
            SelectGameObjects();
        }
    }

    // This function is called when the user clicks the "Select GameObjects" button in the editor window
    [MenuItem("Window/Editor Plugins/Find Objects By Name")]
    public static void SelectGameObjects()
    {
        // Get a list of all GameObjects in the scene
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        // Create a new list to store the GameObjects that match the search criteria
        List<GameObject> matchingGameObjects = new List<GameObject>();

        // Get the search criteria from the ExampleSelector window
        FindObjectsByName_V3 window = (FindObjectsByName_V3)GetWindow(typeof(FindObjectsByName_V3));
        string gameObjectName = window.gameObjectName;

        // Iterate through the list of GameObjects
        foreach (GameObject gameObject in gameObjects)
        {
            // Check if the GameObject's name matches the specified name, ignoring curly brackets and any characters inside them
            if (Regex.IsMatch(gameObject.name, @"^" + Regex.Escape(gameObjectName).Replace(@"\{.*\}", "") + @"$"))
            {
                // If it does, add it to the list
                matchingGameObjects.Add(gameObject);
            }
        }

        // Select the GameObjects that match the search criteria
        Selection.objects = matchingGameObjects.ToArray();
    }
}