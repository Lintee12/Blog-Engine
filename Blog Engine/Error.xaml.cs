using System.Windows;

namespace Blog_Engine;

public partial class Error
{
    public Error()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty ErrorMessageProperty =
        DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(Error), new PropertyMetadata("Default Error"));
    
    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        init => SetValue(ErrorMessageProperty, value);
    }
}