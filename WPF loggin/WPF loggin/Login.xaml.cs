using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;

namespace WPF_loggin
{

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private WPFDBEntities db = new WPFDBEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginCheck())
            {
                App.Current.Properties["UserName"] = UserNameInput.Text;
                var main = new MainWindow();
                this.Close();
                main.Show();
            }
            else
            {
                MessageBox.Show("登入失敗");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUserName())
            {
                var result = HashPassword();
                var dto = new User();
                dto.UserName = UserNameInput.Text;
                dto.PasswordHash = result;
                
                db.Users.Add(dto);
                db.SaveChanges();
                MessageBox.Show("註冊成功，請重新登入!");
                UserNameInput.Text = string.Empty;
                PasswordInput.Password = string.Empty;
            }
            else
            {
                MessageBox.Show("帳號重複，請重新輸入");
            }
        }

        private bool LoginCheck()
        {
            var dto = db.Users.FirstOrDefault(x => x.UserName == UserNameInput.Text);
            if (dto == null) return false;
            var result = HashPassword();
            if (dto.PasswordHash != result) return false;
            return true;
        }

        private string HashPassword()
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider(); //建立一個SHA512
            var salt = UserNameInput.Text;
            var password = Encoding.Default.GetBytes(salt + PasswordInput.Password); //將字串轉為Byte[]
            var crypto = sha512.ComputeHash(password); //進行SHA512加密
            var result = Convert.ToBase64String(crypto); //把加密後的字串從Byte[]轉為字串
            return result;
        }

        private bool CheckUserName()
        {
            var dto = db.Users.FirstOrDefault(x => x.UserName == UserNameInput.Text);
            if (dto == null) return false;
            return true;
        }
    }
}
