using System.Reflection;

namespace core.lib.extensions
{
    public static class CopyExtension
    {
        public static void CopyProperties(this object to, object from)
        {
            var fromPublicProperties = from.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in fromPublicProperties)
            {
                if (prop.CanRead)
                {
                    var propToSet = to.GetType().GetProperty(prop.Name);
                    if (propToSet != null && propToSet.CanWrite)
                    {
                        if (propToSet.PropertyType.IsAssignableFrom(prop.PropertyType))
                        {
                            propToSet.SetValue(to, prop.GetValue(from));
                        }
                    }
                }
            }
        }
    }
}
