using System;
using System.Data;
using Omu.ValueInjecter;

namespace iRecruit.Data
{
    internal class ReaderInjection : KnownSourceValueInjection<IDataReader>
    {
        protected override void Inject(IDataReader source, object target)
        {
            try
            {
                for (var i = 0; i < source.FieldCount; i++)
                {
                    var targetProp = target.GetProps().GetByName(source.GetName(i), true);
                    if (targetProp == null) continue;

                    var value = source.GetValue(i);

                    // temporary convertion. Will be removed later 
                    if (value == DBNull.Value) value = null;
                    else
                    {
                        if (value is double)
                        {
                            value = Convert.ToDecimal(value);
                        }
                    }
                    targetProp.SetValue(target, value);

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}