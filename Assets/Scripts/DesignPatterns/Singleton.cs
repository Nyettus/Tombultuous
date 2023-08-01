using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This does not need to exist because the interface implementation is going to be independant of the class in all usages
//public interface ISingleton<T> where T : MonoBehaviour
//{
//    public static T _ { get; private set; }
//    protected void Startup(T instance);
//}

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _ { get; private set; }
    protected void Startup(T instance)
    {
        _ = instance;
    }
}
public abstract class SingletonPersist<T> : MonoBehaviour where T : MonoBehaviour
{
    // The static instance will persist in the duration of the Domain, even if the gameObject is destroyed.
    // That means it will stay across reloading scenes and changing scenes in editor
    // If we want to reload the singleton, enable reload domain in editor settings.
    public static T _ = null;
    protected void Startup(T instance)
    {
        if (!Application.isPlaying) return;
        if (_ == null)
        {
            _ = instance;
            DontDestroyOnLoad(_);
            RunOnce();
        }
        else
            Destroy(gameObject);
    }
    protected virtual void RunOnce()
    {
        //Debug.Log($"Starting Up SingletonPersist {_.name} of class {_.GetType().Name}");
    }
}
//public abstract class SingletonPersistAndUnload<T> : SingletonPersist<T> where T : MonoBehaviour
//{
//    [SerializeField] public string sceneToUnload = "Menu";
//    protected override void RunOnce()
//    {
//        base.RunOnce();
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }
//    void OnSceneLoaded(Scene scene, LoadSceneMode moe)
//    {
//        if (scene.name == sceneToUnload)
//        {
//            SceneManager.sceneLoaded -= OnSceneLoaded;
//            Destroy(gameObject);
//        }
//    }
//}
