using System.IO;

namespace FileBrowser;

public partial class FileBrowserForm : Form
{
    private FilePreviewer _filePreviewer;
    public FileBrowserForm()
    {
        InitializeComponent();
        nextButton.Enabled = false;
        closeButton.Enabled = false;
    }

    private void openButton_Click(object sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            /*using var streamReader = new StreamReader(openFileDialog.FileName);
            resultTextBox.Text = streamReader.ReadLine();
            var streamReader = new StreamReader(openFileDialog.FileName);

            resultTextBox.Text = streamReader.ReadLine();
            streamReader.Dispose();*///calling dispose manually
            _filePreviewer = new FilePreviewer(openFileDialog.FileName);
            openButton.Enabled = false;
            nextButton.Enabled = true;
            closeButton.Enabled = true;
        }
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
        resultTextBox.Text = _filePreviewer.GetNextLine();
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        _filePreviewer.Dispose();
        openButton.Enabled = true;
        nextButton.Enabled = false;
        resultTextBox.Clear();
    }
}
public class FilePreviewer : IDisposable
{
    private readonly StreamReader _reader;
    public FilePreviewer(string path) => _reader = new StreamReader(path);
    public string GetNextLine()
    {
        if (_reader.EndOfStream)
        {
            return "--END OF FILE--";
        }
        return _reader.ReadLine();
    }
    public void Dispose()
    {
        _reader.Dispose();
    }
}