using System.Windows;

namespace Demo {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e) {
            if (IsReg1(0, string.Empty)) {
                MessageBox.Show("已注册！");
            } else {
                MessageBox.Show("未注册！");
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e) {
            if (IsReg2()) {
                MessageBox.Show("已注册！");
            } else {
                MessageBox.Show("未注册！");
            }
        }

        public bool IsReg1(int i, string s) {
            return false;
        }
        public bool IsReg2() {
            return false;
        }

    }
}
