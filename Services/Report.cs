using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Avalonia.Platform;
using kursachRVV.Models;
using static iTextSharp.text.pdf.AcroFields;

namespace kursachRVV.Services
{
    public class Report
    {
        public Document PdfDoc { get; set; }

        public DateOnly[] DateRange { get; set; }

        public List<Zayavki> DoneRequests { get; set; }

        public List<Zayavki> DeniedRequests { get; set; }

        public List<Zayavki> WorkingRequests { get; set; }

        public Report(DateOnly[] range)
        {
            DateRange = range;
        }

        public async Task GetReportData()
        {
            try
            {
                DoneRequests = await DBCall.GetDoneRequest(DateRange);
                DeniedRequests = await DBCall.GetDeniedRequest(DateRange);
                WorkingRequests = await DBCall.GetWorkingRequest(DateRange);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private void CreateTitle(string text)
        {
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");
            BaseFont fgBaseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            var title = new Paragraph(text, new Font(fgBaseFont, 14, Font.BOLD, new BaseColor(0, 0, 0)))
            {
                SpacingAfter = 25f,
                SpacingBefore = 25f,
                Alignment = Element.ALIGN_CENTER
            };
            PdfDoc.Add(title);
        }

        public void CreateReport()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            PdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PdfWriter.GetInstance(PdfDoc, new FileStream(desktopPath + $"\\Отчет {DateTime.Now:yyyy-MM-dd_HH-mm-ss}.pdf", FileMode.Create));
            PdfDoc.Open();

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");
            BaseFont fgBaseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fgFont = new Font(fgBaseFont, 14, Font.NORMAL, new BaseColor(0, 0, 0));

            //AddReportLogo();

            var spacer = new Paragraph("")
            {
                SpacingAfter = 10f,
                SpacingBefore = 10f
            };
            PdfDoc.Add(spacer);

            var title = new Paragraph($"ОТЧЕТ ЗАЯВОК В ТЕХ. ОТДЕЛ ЗА ПЕРОИД \r ОТ {DateRange[0]} ДО {DateRange[1]}", new Font(fgBaseFont, 14, Font.BOLD, new BaseColor(0, 0, 0)))
            {
                SpacingAfter = 25f,
                Alignment = Element.ALIGN_CENTER
            };
            PdfDoc.Add(title);

            AddHeaderTable(fgFont);

            AddDoneTable(fgFont);

            AddDeniedTable(fgFont);

            AddWorkingTable(fgFont);

            PdfDoc.Close();
        }

        private void AddReportLogo()
        {
            var image = AssetLoader.Open(new Uri("avares://kursachRVV/Assets/logo.png"));
            var png = Image.GetInstance(System.Drawing.Image.FromStream(image), ImageFormat.Png);
            png.ScalePercent(25f);
            png.SetAbsolutePosition(PdfDoc.Left, PdfDoc.Top);
            PdfDoc.Add(png);
        }

        private void AddHeaderTable(Font fgFont)
        {
            var headerTable = new PdfPTable(new[] { .75f })
            {
                HorizontalAlignment = 0,
                WidthPercentage = 75,
                DefaultCell = { MinimumHeight = 22f },
            };

            PdfPCell cell = new PdfPCell(new Phrase($"Дата: {DateTime.Now.ToString("dd.MM.yyyy")}", fgFont));
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);

            PdfDoc.Add(headerTable);
        }

        private void AddDoneTable(Font fgFont)
        {
            CreateTitle("Выполненные заявки");
            if (DoneRequests.Count>0)
            {
                var statisticsTitleTable = new PdfPTable(new[] { .75f, 1f, 1f, .50f })
                {
                    HorizontalAlignment = 0,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f },
                };

                PdfPCell title1 = new PdfPCell(new Phrase("Описание", fgFont));
                statisticsTitleTable.AddCell(title1);

                PdfPCell title2 = new PdfPCell(new Phrase("Расположение", fgFont));
                statisticsTitleTable.AddCell(title2);

                PdfPCell title3 = new PdfPCell(new Phrase("Дата создания", fgFont));
                statisticsTitleTable.AddCell(title3);

                PdfPCell title4 = new PdfPCell(new Phrase("Исполнитель", fgFont));
                statisticsTitleTable.AddCell(title4);

                foreach (var item in DoneRequests)
                {
                    PdfPCell privateRequestsDepartmentCellKey = new PdfPCell(new Phrase(item.Opisanie.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey);

                    PdfPCell privateRequestsDepartmentCellKey1 = new PdfPCell(new Phrase(item.Raspolozenie.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey1);

                    PdfPCell privateRequestsDepartmentCellKey2 = new PdfPCell(new Phrase(item.DateAndTime.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey2);

                    PdfPCell privateRequestsDepartmentCellKey3 = new PdfPCell(new Phrase(item.IspolnitelNavigation.TexOtSotrydnikNavigation.Familia.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey3);

                }
                PdfDoc.Add(statisticsTitleTable);
            }
            else
            {
                var title = new Paragraph($"Отсутствует за период", fgFont)
                {
                    SpacingAfter = 25f,
                    Alignment = Element.ALIGN_CENTER
                };
                PdfDoc.Add(title);

            }

        }

        private void AddDeniedTable(Font fgFont)
        {
            CreateTitle("Отказанные заявки");

            if (DoneRequests.Count>0)
            {
                var statisticsTitleTable = new PdfPTable(new[] { .75f, 1f, 1f })
                {
                    HorizontalAlignment = 0,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f },
                };

                PdfPCell title1 = new PdfPCell(new Phrase("Описание", fgFont));
                statisticsTitleTable.AddCell(title1);

                PdfPCell title2 = new PdfPCell(new Phrase("Расположение", fgFont));
                statisticsTitleTable.AddCell(title2);

                PdfPCell title3 = new PdfPCell(new Phrase("Дата создания", fgFont));
                statisticsTitleTable.AddCell(title3);

                foreach (var item in DoneRequests)
                {
                    PdfPCell privateRequestsDepartmentCellKey = new PdfPCell(new Phrase(item.Opisanie.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey);

                    PdfPCell privateRequestsDepartmentCellKey1 = new PdfPCell(new Phrase(item.Raspolozenie.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey1);

                    PdfPCell privateRequestsDepartmentCellKey2 = new PdfPCell(new Phrase(item.DateAndTime.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey2);

                }
                PdfDoc.Add(statisticsTitleTable);
            }
            else
            {
                var title = new Paragraph($"Отсутствует за период", fgFont)
                {
                    SpacingAfter = 25f,
                    Alignment = Element.ALIGN_CENTER
                };
                PdfDoc.Add(title);
            }
        }

        private void AddWorkingTable(Font fgFont)
        {
            CreateTitle("Заявки в работе");

            if (WorkingRequests.Count>0)
            {
                var statisticsTitleTable = new PdfPTable(new[] { .75f, 1f, 1f })
                {
                    HorizontalAlignment = 0,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f },
                };
                PdfPCell title1 = new PdfPCell(new Phrase("Описание", fgFont));
                statisticsTitleTable.AddCell(title1);

                PdfPCell title2 = new PdfPCell(new Phrase("Расположение", fgFont));
                statisticsTitleTable.AddCell(title2);

                PdfPCell title3 = new PdfPCell(new Phrase("Дата создания", fgFont));
                statisticsTitleTable.AddCell(title3);

                foreach (var item in WorkingRequests)
                {
                    PdfPCell privateRequestsDepartmentCellKey = new PdfPCell(new Phrase(item.Opisanie.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey);

                    PdfPCell privateRequestsDepartmentCellKey1 = new PdfPCell(new Phrase(item.Raspolozenie.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey1);

                    PdfPCell privateRequestsDepartmentCellKey2 = new PdfPCell(new Phrase(item.DateAndTime.ToString(), fgFont));
                    statisticsTitleTable.AddCell(privateRequestsDepartmentCellKey2);

                }
                PdfDoc.Add(statisticsTitleTable);
            }
            else
            {
                var title = new Paragraph($"Отсутствует за период", fgFont)
                {
                    SpacingAfter = 25f,
                    Alignment = Element.ALIGN_CENTER
                };
                PdfDoc.Add(title);
            }
        }

    }
}
