using UnityEngine;
using System.Collections;
using UnityEditor;
public class OperatePrefab : MonoBehaviour
{

    [ExecuteInEditMode]
    [MenuItem("Tools/RecordPoint Add Flame")]
    private static void ChangePrefab()
    {
        GameObject twoSphere = AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/TwoSphere.prefab", typeof(GameObject)) as GameObject;

        string[] ids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Resources/Prefabs" });
        for (int i = 0; i < ids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(ids[i]);
            Debug.Log(path);
            if (!path.Contains("TwoCube"))
            {
                continue;
            }
            GameObject originTwoCube = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            GameObject twoCube = PrefabUtility.InstantiatePrefab(originTwoCube) as GameObject;

            foreach (Transform item in twoCube.transform)
            {
                if (item.FindChild("TwoSphere") == null)
                {
                    GameObject ts = PrefabUtility.InstantiatePrefab(twoSphere) as GameObject;
                    ts.transform.parent = item;
                    ts.transform.localPosition = new Vector3(0f, -1.18f, 0f);
                }
            }

            var newprefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/TwoCube.prefab");
            PrefabUtility.ReplacePrefab(twoCube, newprefab, ReplacePrefabOptions.Default);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Done");
    }
}