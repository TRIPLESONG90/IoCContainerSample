namespace IoCContainer
{
    public class Container
    {
        Dictionary<Type, Func<Container, object>> services = new Dictionary<Type, Func<Container, object>>();
        Dictionary<Type, object> instances = new Dictionary<Type, object>();
        public void RegisterService<T>()
        {
            services.Add(typeof(T), null);
        }
        public void RegisterService<T>(Func<Container, T> action)
        {
            services.Add(typeof(T), (Func<Container, object>)(object)action);
        }
        public T GetService<T>()
        {
            var srv = services.Where(x => x.Key.Name == typeof(T).Name)
                              .FirstOrDefault();
            var t = srv.Key;


            if (instances.TryGetValue(t, out var instance))
                return (T)instance;

            else
            {
                object obj = null;
                if (typeof(T).GetConstructor(Type.EmptyTypes) != null)
                    obj = (T)Activator.CreateInstance(t);
                else
                    obj = srv.Value.Invoke(this);
                instances.Add(t, obj);
                return (T)instances[t];
            }
        }
    }
}