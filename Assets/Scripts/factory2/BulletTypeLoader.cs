using System;
using System.Collections.Generic;
using factory2.factory;

namespace DefaultNamespace
{
    public class BulletTypeLoader
    {
        public static List<System.Type> GetAllBulletTypes()
        {
            var bulletTypes = new List<System.Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IBullet).IsAssignableFrom(type)
                        && !type.IsAbstract
                        && !type.IsInterface)
                    {
                        bulletTypes.Add(type);
                    }
                }
            }
            
            return bulletTypes;
        }
    }
}