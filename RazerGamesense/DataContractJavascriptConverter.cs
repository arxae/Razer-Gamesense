namespace RGS
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.Serialization;
	using System.Web.Script.Serialization;

	public class DataContractJavascriptConverter : JavaScriptConverter
	{
		private static readonly List<Type> SupportedTypesList = new List<Type>();

		static DataContractJavascriptConverter()
		{
			foreach (var type in Assembly.GetExecutingAssembly().DefinedTypes)
			{
				if (Attribute.IsDefined(type, typeof(DataContractAttribute)))
				{
					SupportedTypesList.Add(type);
				}
			}
		}

		private readonly bool _convertEnumToString;

		public DataContractJavascriptConverter() : this(false)
		{
		}

		public DataContractJavascriptConverter(bool convertEnumToString)
		{
			_convertEnumToString = convertEnumToString;
		}

		public override IEnumerable<Type> SupportedTypes => SupportedTypesList;

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (Attribute.IsDefined(type, typeof(DataContractAttribute)))
			{
				try
				{
					object instance = Activator.CreateInstance(type);

					IEnumerable<MemberInfo> members = ((IEnumerable<MemberInfo>)type.GetFields())
						.Concat(type.GetProperties().Where(property => property.CanWrite && property.GetIndexParameters().Length == 0))
						.Where((member) => Attribute.IsDefined(member, typeof(DataMemberAttribute)));
					foreach (MemberInfo member in members)
					{
						var attribute = (DataMemberAttribute)Attribute.GetCustomAttribute(member, typeof(DataMemberAttribute));

						if (dictionary.TryGetValue(attribute.Name, out object value) == false)
						{
							if (attribute.IsRequired)
							{
								throw new SerializationException($"Required DataMember with name {attribute.Name} not found");
							}
							continue;
						}
						if (member.MemberType == MemberTypes.Field)
						{
							FieldInfo field = (FieldInfo)member;
							object fieldValue;
							if (_convertEnumToString && field.FieldType.IsEnum)
							{
								fieldValue = Enum.Parse(field.FieldType, value.ToString());
							}
							else
							{
								fieldValue = serializer.ConvertToType(value, field.FieldType);
							}
							field.SetValue(instance, fieldValue);
						}
						else if (member.MemberType == MemberTypes.Property)
						{
							PropertyInfo property = (PropertyInfo)member;
							object propertyValue;
							if (_convertEnumToString && property.PropertyType.IsEnum)
							{
								propertyValue = Enum.Parse(property.PropertyType, value.ToString());
							}
							else
							{
								propertyValue = serializer.ConvertToType(value, property.PropertyType);
							}
							property.SetValue(instance, propertyValue);
						}
					}
					return instance;
				}
				catch (Exception)
				{
					return null;
				}
			}
			return null;
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (obj != null && Attribute.IsDefined(obj.GetType(), typeof(DataContractAttribute)))
			{
				Type type = obj.GetType();
				IEnumerable<MemberInfo> members = ((IEnumerable<MemberInfo>)type.GetFields())
					.Concat(type.GetProperties().Where(property => property.CanRead && property.GetIndexParameters().Length == 0))
					.Where((member) => Attribute.IsDefined(member, typeof(DataMemberAttribute)));
				foreach (MemberInfo member in members)
				{
					DataMemberAttribute attribute = (DataMemberAttribute)Attribute.GetCustomAttribute(member, typeof(DataMemberAttribute));
					object value;
					if (member.MemberType == MemberTypes.Field)
					{
						FieldInfo field = (FieldInfo)member;
						if (_convertEnumToString && field.FieldType.IsEnum)
						{
							value = field.GetValue(obj).ToString();
						}
						else
						{
							value = field.GetValue(obj);
						}
					}
					else if (member.MemberType == MemberTypes.Property)
					{
						PropertyInfo property = (PropertyInfo)member;
						if (_convertEnumToString && property.PropertyType.IsEnum)
						{
							value = property.GetValue(obj).ToString();
						}
						else
						{
							value = property.GetValue(obj);
						}
					}
					else
					{
						continue;
					}
					if (dictionary.ContainsKey(attribute.Name))
					{
						throw new SerializationException($"More than one DataMember found with name {attribute.Name}");
					}

					dictionary[attribute.Name] = value;
				}
			}

			return dictionary;
		}
	}
}
