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
    public class StoredProcedureSettings
    {
        public StoredProcedureSettings(string procedureName, List<StoredProcedureParameter> parameters)
        {
            this.ProcedureName = procedureName;
            this.Parameters = parameters;
        }

        public string ProcedureName { get; private set; }
        public List<StoredProcedureParameter> Parameters { get; private set; }
    }
}
