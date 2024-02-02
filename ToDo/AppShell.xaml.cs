using ToDo.Pages;

namespace ToDo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Edit), typeof(Edit));
        }
    }
}
