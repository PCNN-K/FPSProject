using UnityEngine;

public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
{
	private static WeakReference<T> _instance;
	private static bool quit = false;

	void OnApplicationQuit ()
	{
		quit = true;
	}

	public static bool HasInstance ()
	{
		if (_instance == null || _instance.Target == null) {
			return false;
		}
		
		return true;
	}

	public static void ResetInstance()
	{
		UnloadInstance();
		SetInstance();
	}

	public static void UnloadInstance()
	{
		_instance.Target = null;
	}

	public static T Instance {
		get {
			if (quit) {
				return null;
			}
			
			if (_instance != null && _instance.Target != null) {
				return _instance.Target;
			}

			SetInstance();
			
			return _instance.Target;
		}
	}

	private static void SetInstance()
	{
		T instance = FindObjectOfType<T> ();

		if (instance == null) {
			GameObject container = new GameObject ();
			container.name = "_" + typeof(T).Name;
			instance = container.AddComponent (typeof(T)) as T;	
		} 

		_instance = new WeakReference<T> (instance);
	}
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;
	
	private static object _lock = new object ();

	protected void InstancingSelf()
	{
		//명시적으로 인스턴스를 생성해준다.
		//씬에 달린 싱글턴 instance가 있는데
		//Instancing이전에 씬이 넘어가게 되면 싱글턴이 파괴된다.
		var instancing = Instance;
	}

	public static T Instance {
		get {
			if (applicationIsQuitting) {
				Debug.LogWarning ("[Singleton] Instance '" + typeof(T) +
				"' already destroyed on application quit." +
				" Won't create again - returning null.");
				return null;
			}
			
			lock (_lock) {
				if (_instance == null) {
					_instance = (T)FindObjectOfType (typeof(T));
					
					if (FindObjectsOfType (typeof(T)).Length > 1) {
						Debug.LogError ("[Singleton] Something went really wrong " +
						" - there should never be more than 1 singleton!" +
						" Reopening the scene might fix it.");
						return _instance;
					}
					
					if (_instance == null) {
						GameObject singleton = new GameObject ();
						_instance = singleton.AddComponent<T> ();
						singleton.name = "_" + typeof(T);
					} 
					
					DontDestroyOnLoad (_instance);
				}
				 
				return _instance;
			}
		}
	}

	private static bool applicationIsQuitting = false;
	
	public virtual void OnDestroy ()
	{
		applicationIsQuitting = true;
	}

	protected bool _IsExistInstanceDuringAwake ()
	{
		if (_instance != null) {
			Destroy (gameObject);
			
			return true;
		}
		
		_instance = gameObject.GetComponent<T> ();
		DontDestroyOnLoad (gameObject);
		
		return false;
	}

	public static bool HasInstance ()
	{
		return (bool)_instance;
	}
}
