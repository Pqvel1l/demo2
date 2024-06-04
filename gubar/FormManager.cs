using System.Windows.Forms;

public static class FormManager
{
    private static Form _currentForm;
    private static Form _previousForm;

    public static void ShowForm(Form newForm)
    {
        if (_currentForm != null)
        {
            _previousForm = _currentForm;
            _currentForm.Hide();
        }

        _currentForm = newForm;
        _currentForm.Show();
    }

    public static void ShowPreviousForm()
    {
        if (_previousForm != null)
        {
            _currentForm.Hide();
            _previousForm.Show();
            _currentForm = _previousForm;
            _previousForm = null;
        }
    }
}