using System;
using Framework.Data;

namespace Framework.References
{
    [Serializable]
    public class StringReference : DataReference<string>
    {
        public StringVariable variable;

        public override Variable<string> Variable => variable;
    }
}