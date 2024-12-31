using System.Data;

namespace DBHelper
{
    /// <summary>
    /// Team (ValueFirst)
    /// </summary>
    public class DbParameter
    {

        #region "Constructors"
        /// <summary>
        /// Default constructor. Parameter name, vale, type and direction needs to be assigned explicitly by using the public properties exposed.
        /// </summary>
        public DbParameter()
        {
            Type = DbType.String;
        }


        /// <summary>
        /// Creates a parameter with the name and value specified. Default data type and direction is String and Input respectively.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        public DbParameter(string name, object value)
        {
            Type = DbType.String;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Creates a parameter with the name, value and direction specified. Default data type is String.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        /// <param name="paramDirection">Parameter direction</param>
        public DbParameter(string name, object value, ParameterDirection paramDirection)
        {
            Type = DbType.String;
            Name = name;
            Value = value;
            ParamDirection = paramDirection;
        }

        /// <summary>
        /// Creates a parameter with the name, value and Data type specified. Default direction is Input. 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        /// <param name="dbType">Data type</param>
        public DbParameter(string name, object value, DbType dbType)
        {
            Name = name;
            Value = value;
            Type = dbType;
        }

        /// <summary>
        /// Creates a parameter with the name, value, data type and direction specified. 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        /// <param name="dbType">Data type</param>
        /// <param name="paramDirection">Parameter direction</param>
        public DbParameter(string name, object value, DbType dbType, ParameterDirection paramDirection)
        {
            Name = name;
            Value = value;
            Type = dbType;
            ParamDirection = paramDirection;
        }
        #endregion

        #region "Public Properties"
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// Gets or sets the value associated with the parameter.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets or sets the type of the parameter.
        /// </summary>
        public DbType Type { get; private set; }

        /// <summary>
        /// Gets or sets the direction of the parameter.
        /// </summary>
        public ParameterDirection ParamDirection { get; private set; }

        #endregion
    }
}
