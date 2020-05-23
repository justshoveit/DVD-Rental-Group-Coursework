using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq.Mapping;

using System.Reflection;
using System.Reflection.Context;
using System.Reflection.Emit;

namespace SAMS.Repository
{
    class DatabaseObjectMapper
    {
        private static readonly IDictionary<Type, IDictionary<string, string>> typeMappings =
        new Dictionary<Type, IDictionary<string, string>>();

        public static IDictionary<string, string> DBColumn<T>() where T : new()
        {
            var type = typeof(T);
            IDictionary<string, string> databaseMappings;

            if (typeMappings.TryGetValue(type, out databaseMappings))
            {
                return databaseMappings;
            }

            databaseMappings = new Dictionary<string, string>();
            foreach (var property in type.GetProperties())
            {
                var columnMapping = property
                    .GetCustomAttributes(false)
                    .FirstOrDefault(attribute => attribute is DbColumnAttribute);

                if (columnMapping != null)
                {
                    databaseMappings.Add(property.Name, ((DbColumnAttribute)columnMapping).Name);
                }
            }

            typeMappings.Add(type, databaseMappings);
            return databaseMappings;
        }

        public static IEnumerable<T> GetListFromDataReader<T>(IDataReader reader) where T : new()
        {
            var properties = typeof(T).GetProperties();

            var modelProperties = new List<string>();
            var columnList = (reader.GetSchemaTable().Select()).Select(r => r.ItemArray[0].ToString());
            while (reader.Read())
            {
                var element = Activator.CreateInstance<T>();
                Dictionary<string, string> dbMappings = DBColumn(element);
                string columnName;
                foreach (var f in properties)
                {

                    if (!columnList.Contains(f.Name) && !dbMappings.ContainsKey(f.Name))
                        continue;
                    columnName = dbMappings.ContainsKey(f.Name) ? dbMappings[f.Name] : f.Name;
                    var o = (object)reader[columnName];

                    if (o.GetType() != typeof(DBNull)) f.SetValue(element, ChangeType(o, f.PropertyType), null);
                }
                yield return element;
            }

        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t); ;
            }

            return Convert.ChangeType(value, t);
        }
    }
}
