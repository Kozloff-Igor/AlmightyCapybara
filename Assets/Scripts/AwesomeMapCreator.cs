using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class AwesomeMapCreator
{
    static bool buildingIsOn = false;
        
    [MenuItem("My Commands/First Command _b")]
    static void FirstCommand()
    {
        if (buildingIsOn)
        {
            Debug.Log("You used the shortcut B");
            AwesomeMapPrefabs awesomeMapPrefabs = SceneAsset.FindObjectOfType<AwesomeMapPrefabs>();
            GameObject newTile = GameObject.Instantiate(awesomeMapPrefabs.tiles[Random.Range(0, awesomeMapPrefabs.tiles.Length)]);
            newTile.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
            newTile.transform.SetParent(awesomeMapPrefabs.transform);
            if (awesomeMapPrefabs.lastSelectedTile)
            {
                newTile.transform.position = awesomeMapPrefabs.lastSelectedTile.transform.position;                
            }
            awesomeMapPrefabs.lastSelectedTile = newTile;
            Selection.objects = new Object[] { newTile};
        }
    }


    [MenuItem("My Commands/Special Command %g")]
    static void SpecialCommand()
    {
        buildingIsOn = !buildingIsOn;
        Debug.Log("Build="+buildingIsOn);
    }



}