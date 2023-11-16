using UnityEngine;

public static class Singleton
{
    public static T Get<T>() where T : MonoBehaviour
    {
        T[] instances = GameObject.FindObjectsOfType<T>();

        for (int i = 1; i < instances.Length; i++)
            GameObject.Destroy(instances[i].gameObject);

        return instances[0];
    }
}