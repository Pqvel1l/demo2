using gubar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()//верный логин и пароль
        {
            {
                var loginForm = new LoginForm();
                loginForm.text_Login.Text = "login1";// Тестовое значение для входа
                loginForm.text_Pass.Text = "pass1";// Тестовое значение для входа
                loginForm.button2Click(null, null);//"Нажатие" на кнопку
                Assert.IsFalse(loginForm.Visible);

            }
        }
        [TestMethod]
        public void TestMethod2() // NEверный логин и пароль
        {
            var loginForm = new LoginForm();
            loginForm.text_Login.Text = "123"; // Тестовое значение для входа
            loginForm.text_Pass.Text = "123"; // Тестовое значение для входа
            loginForm.button2Click(null, null); // "Нажатие" на кнопку
            Assert.IsFalse(loginForm.Visible);
        }
    }
}
