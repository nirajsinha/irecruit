using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Omu.ValueInjecter;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace iRecruit.Data.Helpers
{
    public class StoredProcedureParameter
    {
        public StoredProcedureParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            Name = name;
            Value = value;
            DbType = dbType;
            Direction = direction;
        }

        public string Name { get; private set; }
        public object Value { get; set; }
        public DbType DbType { get; private set; }
        public ParameterDirection Direction { get; private set; }
    }
}
