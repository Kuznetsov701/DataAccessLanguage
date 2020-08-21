using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace DataAccessLanguage.Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Json = File.ReadAllText("Example/json1.json");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonToDictionaryConverter());
            Data = new DataObject(JsonSerializer.Deserialize<Dictionary<string, object>>(Json, options), new ExpressionFactory());
            Data.PropertyChanged += Data_PropertyChanged;
        }

        #region string Json = null
        public string Json
        {
            get { return (string)GetValue(JsonProperty); }
            set { SetValue(JsonProperty, value); }
        }
        public static readonly DependencyProperty JsonProperty =
            DependencyProperty.Register(
                nameof(Json),
                typeof(string),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion

        #region DataObject Data = null
        public DataObject Data
        {
            get { return (DataObject)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                nameof(Data),
                typeof(DataObject),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion

        #region object ExpressionResult1 = null
        public object ExpressionResult1
        {
            get { return (object)GetValue(ExpressionResult1Property); }
            set { SetValue(ExpressionResult1Property, value); }
        }
        public static readonly DependencyProperty ExpressionResult1Property =
            DependencyProperty.Register(
                nameof(ExpressionResult1),
                typeof(object),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion

        #region object ExpressionResult2 = null
        public object ExpressionResult2
        {
            get { return (object)GetValue(ExpressionResult2Property); }
            set { SetValue(ExpressionResult2Property, value); }
        }
        public static readonly DependencyProperty ExpressionResult2Property =
            DependencyProperty.Register(
                nameof(ExpressionResult2),
                typeof(object),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion

        #region string Expression = null    
        public string Expression
        {
            get { return (string)GetValue(ExpressionProperty); }
            set { SetValue(ExpressionProperty, value); }
        }
        public static readonly DependencyProperty ExpressionProperty =
            DependencyProperty.Register(
                nameof(Expression),
                typeof(string),
                typeof(MainWindow),
                new PropertyMetadata(null, (d, e) => (d as MainWindow).OnExpressionPropertyChanged(e)));
        protected virtual void OnExpressionPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            ExpressionResult = Data[Expression];
        }
        #endregion

        #region object ExpressionResult = null
        public object ExpressionResult
        {
            get { return (object)GetValue(ExpressionResultProperty); }
            set { SetValue(ExpressionResultProperty, value); }
        }
        public static readonly DependencyProperty ExpressionResultProperty =
            DependencyProperty.Register(
                nameof(ExpressionResult),
                typeof(object),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ExpressionResult1 = Data["roles.for(1..).select(name).join(, )"];
            ExpressionResult2 = Data["roles.for(0..).select(num).sum()"];
            ExpressionResult = Data[Expression];
            Json = JsonSerializer.Serialize(Data.Object);
        }
        private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ExpressionResult1 = Data["roles.for(1..).select(name).join(, )"];
            ExpressionResult2 = Data["roles.for(0..).select(num).sum()"];
            ExpressionResult = Data[Expression];
            Json = JsonSerializer.Serialize(Data.Object);
        }
    }
}
