using System;
using System.Windows.Forms;
using DevExpress.DataAccess.Excel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Configuration;
// ...

namespace BindingReportToExcelWorkbook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an empty report.
            XtraReport report = new XtraReport();

            // Create a new Excel data source.
            ExcelDataSource excelDataSource = new ExcelDataSource();
            excelDataSource.FileName = "..//..//Northwind.xlsx";

            // Select a required worksheet.
            ExcelWorksheetSettings excelWorksheetSettings = new ExcelWorksheetSettings();
            excelWorksheetSettings.WorksheetName = "Sheet_Categories";

            // Specify import settings.
            ExcelSourceOptions excelSourceOptions = new ExcelSourceOptions();
            excelSourceOptions.ImportSettings = excelWorksheetSettings;
            excelSourceOptions.SkipHiddenRows = false;
            excelSourceOptions.SkipHiddenColumns = false;
            excelDataSource.SourceOptions = excelSourceOptions;

            // Create new fields and specify their settings.
            FieldInfo fieldCategoryID = new FieldInfo { Name = "CategoryID", Type = typeof(double), Selected = false };
            FieldInfo fieldCategoryName = new FieldInfo { Name = "CategoryName", Type = typeof(string) };
            FieldInfo fieldDescription = new FieldInfo { Name = "Description", Type = typeof(string) };
            // Add the created fields to the data source schema in the order that matches the column order in the source file.  
            excelDataSource.Schema.AddRange(new FieldInfo[] { fieldCategoryID, fieldCategoryName, fieldDescription });

            // Assign the data source to the report.
            report.DataSource = excelDataSource;

            // Add a detail band to the report.
            DetailBand detailBand = new DetailBand();
            detailBand.Height = 50;
            report.Bands.Add(detailBand);

            // Create a new label.
            XRLabel label = new XRLabel();
            // Specify the label's binding depending on the data binding mode.
            if (Settings.Default.UserDesignerOptions.DataBindingMode == DataBindingMode.Bindings)
                label.DataBindings.Add("Text", null, "CategoryName");
            else label.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[CategoryName]"));
            // Add the label to the detail band.
            detailBand.Controls.Add(label);


            // Show the report's print preview.
            report.ShowPreview();  
        }
    }
}
