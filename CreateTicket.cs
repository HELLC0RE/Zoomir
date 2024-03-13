using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoomir
{
    public class CreateTicket
    {
        public void TicketCreater(int orderId, DateTime orderDate, List<Product> products, Dictionary<Product, int> selectedProductsCount, decimal orderSumm, decimal orderDiscount, int deliveryTime, string point, int code)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = orderId + ".pdf";
            saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";

            string filePath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                Graphics graphics = e.Graphics;
                Font font = new Font("Arial", 12);
                Font titleFont = new Font("Arial", 16);
                float fontHeight = font.GetHeight();

                float startX = 10;
                float startY = 10;
                float offset = 40;

                graphics.DrawString("Талон на получение", titleFont, Brushes.Black, (e.PageBounds.Width - graphics.MeasureString("Талон на получение", font).Width) / 2, startY);
                startY += offset;

                graphics.DrawString($"Дата заказа: {orderDate.ToShortDateString()}", font, Brushes.Black, startX, startY);
                startY += offset;

                graphics.DrawString($"Номер заказа: {orderId}", font, Brushes.Black, startX, startY);
                startY += offset;

                graphics.DrawString("Состав заказа:", font, Brushes.Black, startX, startY);
                startY += offset;

                foreach (var product in products)
                {
                    int quantity = selectedProductsCount.ContainsKey(product) ? selectedProductsCount[product] : 0;
                    graphics.DrawString($"- {product.Title} - {quantity} шт.", font, Brushes.Black, startX, startY);
                    startY += offset;
                }

                graphics.DrawString($"Сумма заказа: {orderSumm}", font, Brushes.Black, startX, startY);
                startY += offset;

                graphics.DrawString($"Сумма скидки: {orderDiscount}", font, Brushes.Black, startX, startY);
                startY += offset;

                graphics.DrawString($"Пункт выдачи: {point}", font, Brushes.Black, startX, startY);
                startY += offset;
                if (deliveryTime > 3)
                {
                    graphics.DrawString($"Срок доставки: {deliveryTime} дней", font, Brushes.Black, startX, startY);
                    startY += offset;
                }
                else
                {
                    graphics.DrawString($"Срок доставки: {deliveryTime} дня", font, Brushes.Black, startX, startY);
                    startY += offset;
                }

                graphics.DrawString($"Код для получения: {code}", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, startX, startY);
            };

            printDocument.PrinterSettings.PrintToFile = true;
            printDocument.PrinterSettings.PrintFileName = filePath;
            printDocument.Print();
        }
    }
}
