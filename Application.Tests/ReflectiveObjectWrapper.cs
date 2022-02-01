using System.Reflection;

namespace Application.Tests;

public class ReflectiveObjectWrapper<T> where T : class
{
    private readonly object? _objectInstance;
    private readonly Type _objectType;

    public ReflectiveObjectWrapper()
    {
        _objectType = typeof(T);
        _objectInstance = Activator.CreateInstance<T>();
    }

    public object Object => _objectInstance!;
    
    public void Set(object propertyValue, string propertyName)
    {
        if (_objectInstance is null)
            throw new Exception("Need to instantiate the type using the ReflectiveWrapper Ctor");

        var property = GetProperty(propertyName);
        if ( property is null)
            throw new Exception($"Object does not have property called {propertyName}");
        
        SetProperty(property, propertyValue);
    }
    private PropertyInfo? GetProperty(string propertyName) => _objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

    private void SetProperty(PropertyInfo property, object value) => property.SetValue(_objectInstance, value);
}