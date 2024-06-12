using MealApp.BusinessCode;

namespace MealApp
{
    public partial class App : Application
    {
        //TODO : To Define Global Variables Here....
        private static Autofac.IContainer _container;
        public App()
        {
            InitializeComponent();
            //To initialize Containers..
            AppSetup appSetup = new AppSetup();
            _container = appSetup.CreateContainer();
            MainPage = new NavigationPage(new Views.CategoryPage());
        }
    }
}
