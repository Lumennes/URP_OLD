using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainer : MonoBehaviour
{
    public Terrain[] terrains;

    public int xBase;
    public int yBase;
    public int width;
    public int height;

    public float[,] heights;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        foreach(Terrain terrain in terrains)
        {
             heights = terrain.terrainData.GetHeights(xBase, yBase, width, height);
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Debug.Log($"x: {i}, y: {j}, z: {heights[i,j]}");

                yield return new WaitForEndOfFrame();
            }
        }

        yield return null;

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
