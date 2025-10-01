using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task1;

public class MainForm : Form
{
    private Button loadButton;
    private ListBox listBox;

    public MainForm()
    {
        Text = "Cartoon Filter";
        Width = 600;
        Height = 400;

        loadButton = new Button { Text = "Load Data", Dock = DockStyle.Top, Height = 40 };
        listBox = new ListBox { Dock = DockStyle.Fill };

        loadButton.Click += LoadData;

        Controls.Add(listBox);
        Controls.Add(loadButton);
    }

    private void LoadData(object sender, EventArgs e)
    {
        string inputPath = "inData.txt";
        string outputPath = "outData.txt";

        if (!File.Exists(inputPath))
        {
            MessageBox.Show("Input file not found");
            return;
        }

        var lines = File.ReadAllLines(inputPath);
        string[,] data = new string[lines.Length, 7];

        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(';');
            for (int j = 0; j < parts.Length - 1; j++)
                data[i, j] = parts[j].Trim();
        }

        var filtered = from col in Enumerable.Range(0, data.GetLength(0))
                       where data[col, 4] == "Walt Disney"
                       select string.Join("; ", Enumerable.Range(0, 7).Select(row => data[col, row]));


        File.WriteAllLines(outputPath, filtered);
        listBox.Items.Clear();
        foreach (var item in filtered)
            listBox.Items.Add(item);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }
}