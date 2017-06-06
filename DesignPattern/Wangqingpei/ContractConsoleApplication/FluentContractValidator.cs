using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    public static class FluentContractValidator
    {
        public static ValidatorContrainer BeginValidator(this object obj)
        {
            return new ValidatorContrainer() { ValidatorObject = obj };
        }

        public static ValidatorContrainer IsNullOrEmpty(this ValidatorContrainer container)
        {
            container.Add(new Validator()
            {
                Container = container,
                Validat = (object obj) =>
                {
                    if (obj == null)
                    {
                        return false;
                    }
                    else if (obj is string)
                    {
                        return !string.IsNullOrEmpty(obj.ToString());
                    }
                    else if (obj is IEnumerable)
                    {
                        return (obj as IEnumerable).GetEnumerator().MoveNext();
                    }
                    return true;
                }
            });
            return container;
        }

        public static ValidatorContrainer StartWith(this ValidatorContrainer container, string strContent)
        {
            container.Add(new Validator()
            {
                Container = container,
                Validat = (object obj) =>
                {
                    if (obj == null) return false;
                    return obj.ToString().StartsWith(strContent);
                }
            });
            return container;
        }

        public static ValidatorContrainer EndValidator(this ValidatorContrainer container)
        {
            return container;
        }

    }
}
