using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LanguageComponentConsoleApplication
{
    internal class LanguageComponentManagerFactory
    {
        public const string LanguageFileName = "Language.xml";

        public static LanguageComponentManager CreateNewOrderLanguageComponent(Order order)
        {
            LanguageComponentManager result = new LanguageComponentManager { Parameters = order };
            var sendEmail = new SendEmailOrderItemConfirmComponent();
            var forlanguage = new ForLanguageComponent<Order>(sendEmail, null);
            var ifelseLanguage = new IfLanguageComponent<Order>(forlanguage, null);
            ifelseLanguage.SetIfExpression(p => p.CustomerInfo.CunstomerType == CustomerTypeEnum.Vip, order);
            result.trackMark += ifelseLanguage.Run;
            result.trackMark += forlanguage.Run;
            result.trackMark += sendEmail.Run;

            result.FirstLanguage = ifelseLanguage;
            return result;
        }

        public static LanguageComponentManager RebuildLanguageComponent()
        {
            using(Stream stream = File.Open(LanguageFileName, FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                return format.Deserialize(stream) as LanguageComponentManager;
            }
        }

        public static void FreezLanguageComponentManagerObject(LanguageComponentManager languageComponentManager)
        {
            using(Stream stream = File.Open(LanguageFileName, FileMode.Create))
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(stream,languageComponentManager);
            }
        }
    }
}