namespace Task3;

public class MainForm : Form
{
    private RadioButton addBtn, subBtn, mulBtn, divBtn, powBtn;
    private Button importBtn, calcBtn, exportBtn;
    private TextBox resultBox;
    private double param1, param2;
    private string operation;
    private string inputPath = "inData.txt";
    private string outputPath = "outData.txt";
    private string logPath = "log.txt";

    public MainForm()
    {
        Text = "Calculator";
        Width = 400;
        Height = 300;

        addBtn = new RadioButton { Text = "+", Top = 20, Left = 20, Width = 50 };
        subBtn = new RadioButton { Text = "-", Top = 50, Left = 20, Width = 50 };
        mulBtn = new RadioButton { Text = "*", Top = 80, Left = 20, Width = 50 };
        divBtn = new RadioButton { Text = "/", Top = 110, Left = 20, Width = 50 };
        powBtn = new RadioButton { Text = "^", Top = 140, Left = 20, Width = 50 };

        importBtn = new Button { Text = "Import", Top = 20, Left = 100, Width = 100 };
        calcBtn = new Button { Text = "Calculate", Top = 60, Left = 100, Width = 100 };
        exportBtn = new Button { Text = "Export", Top = 100, Left = 100, Width = 100 };

        resultBox = new TextBox { Top = 140, Left = 100, Width = 200 };

        Controls.Add(addBtn);
        Controls.Add(subBtn);
        Controls.Add(mulBtn);
        Controls.Add(divBtn);
        Controls.Add(powBtn);
        Controls.Add(importBtn);
        Controls.Add(calcBtn);
        Controls.Add(exportBtn);
        Controls.Add(resultBox);

        importBtn.Click += ImportData;
        calcBtn.Click += Calculate;
        exportBtn.Click += ExportResult;

        File.WriteAllText(logPath, "");
/*        File.WriteAllText(inputPath, "");
        File.WriteAllText(outputPath, "");*/
        Log("Application started");
        FormClosed += (s, e) => Log("Application closed");
    }

    private void ImportData(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(inputPath))
                throw new Exception("File not found");

            var content = File.ReadAllText(inputPath).Trim();
            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("File is empty, enter data");

            var parts = content.Split(new[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2 || !double.TryParse(parts[0], out param1) || !double.TryParse(parts[1], out param2))
                throw new Exception("Invalid parameter values");

            Log("Import input data");
            MessageBox.Show($"Data imported: {param1}, {param2}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Calculate(object sender, EventArgs e)
    {
        try
        {
            if (addBtn.Checked) { operation = "+"; }
            else if (subBtn.Checked) { operation = "-"; }
            else if (mulBtn.Checked) { operation = "*"; }
            else if (divBtn.Checked) { operation = "/"; }
            else if (powBtn.Checked) { operation = "^"; }
            else throw new Exception("Select an operation");

            Log($"Operation selected {operation}");

            double result = 0;
            switch (operation)
            {
                case "+": result = param1 + param2; break;
                case "-": result = param1 - param2; break;
                case "*": result = param1 * param2; break;
                case "/":
                    if (param2 == 0) throw new DivideByZeroException("Division by zero is forbidden");
                    result = param1 / param2; break;
                case "^": result = Math.Pow(param1, param2); break;
            }

            resultBox.Text = result.ToString();
            Log("Calculate expression");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ExportResult(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(resultBox.Text))
                throw new Exception("Nothing to export");

            string line = $"{param1} {operation} {param2}, Result: {resultBox.Text}";
            File.AppendAllText(outputPath, line + Environment.NewLine);
            MessageBox.Show("Result exported");
            Log("Export result to file");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Log(string action)
    {
        File.AppendAllText(logPath, $"Action: {action}{Environment.NewLine}");
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }
}