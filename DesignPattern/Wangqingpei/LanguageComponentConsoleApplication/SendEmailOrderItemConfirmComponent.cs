using System;

namespace LanguageComponentConsoleApplication
{
    public class SendEmailOrderItemConfirmComponent : LanguageComponent
    {
        public SendEmailOrderItemConfirmComponent()
        {
        }

        public override void Run(object parameter, LanguageComponentManager trackMark)
        {
            if(parameter != null)
            {
                var orderItem = parameter as OrderItem;
                Console.WriteLine("发送采购商品确定邮件，商品名称：{0},数量：{1}", orderItem.ProductInfo.pName, orderItem.Number);
            }
        }
    }
}