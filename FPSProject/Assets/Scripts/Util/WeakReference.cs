using System;
using System.Runtime.Serialization;


[Serializable]    
public class WeakReference<T>        
    : System.WeakReference where T : class   
{
	public WeakReference(T target)            
        : base(target) { }
	public WeakReference(T target, bool trackResurrection) 
        : base(target, trackResurrection) { } 
    protected WeakReference(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }
    public new T Target 
    { 
        get => (T)base.Target;
        set => base.Target = value;
    }
    public static implicit operator WeakReference<T>(T target)        
    { 
        if (target == null)            
        { 
            throw new ArgumentNullException("target"); 
        } 
        return new WeakReference<T>(target); 
    } 
     
    public static implicit operator T(WeakReference<T> reference)
    {
	    return reference?.Target;
    } 
}
