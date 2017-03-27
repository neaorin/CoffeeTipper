using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;

namespace MyUiTest
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {

            app = ConfigureApp
                .Android
                .ApkFile (@"C:\Users\sorinpe\Source\Repos\CoffeeTipper2\Droid\bin\Release\com.xamarin.coffee_tip.apk")
                .StartApp();
        }

        //[Test]
        //public void Repl()
        //{
        //    app.Repl();
        //}

        //[Test]
        //public void MyFirstTest()
        //{
        //    app.ClearText("SubTotal");
        //    app.EnterText("SubTotal", "3.00");
        //    app.Tap("DrinkType");
        //    app.Tap("Latte");
        //    app.Tap("Tamered");
        //    var result = app.Query("Total").First().Text;
        //    Assert.AreEqual(result, "Total: $4.00");
        //}

        //[Test]
        //public void MySecondTest()
        //{
        //    app.Tap(x => x.Marked("SubTotal"));
        //    app.ClearText(x => x.Marked("SubTotal"));
        //    app.EnterText(x => x.Marked("SubTotal"), "4");
        //    app.Tap(x => x.Marked("DrinkType"));
        //    app.Tap(x => x.Text("Cappuccino"));
        //    app.Tap(x => x.Marked("Tamered"));
        //    app.Tap(x => x.Marked("Starbucks"));
        //    var result = app.Query("Total").First().Text;
        //    Assert.AreEqual(result, "Total: $4.00");
        //    app.Screenshot("Tapped on view with class: SwitchCompat with marked: Starbucks");
        //}

        [Test]
        public void TamperedEnabled_CappuccinoTest()
        {
            app.Tap(x => x.Marked("DrinkType"));
            app.Screenshot("Tapped Drink Type");

            app.Tap(x => x.Marked("Cappuccino"));
            app.Screenshot("Tapped on Cappuccino");

            app.DismissKeyboard();

            app.Screenshot("Dismissed Keyboard");
            var result = app.Query("Tamered").First();
            Assert.IsTrue(result.Enabled, "Tampered is enabled");
        }





        [Test]
        public void EspressoTest()
        {

            app.Screenshot("When I run the app");

            app.ClearText("SubTotal");
            app.Screenshot("When I clear text");

            app.EnterText("SubTotal", "4.00");
            app.Screenshot("And Enter $4.00");

            var total = app.Query("Total").First();
            var tip = app.Query("TipAmount").First();

            Assert.AreEqual(total.Text, "Total: $4.50");
            Assert.AreEqual(tip.Text, "Tip: $0.50");


            app.Screenshot("Total is $4.50 with $0.50 tip");

        }

        [Test]
        public void AmericanoTest()
        {
            app.Tap(x => x.Marked("SubTotal"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "4");
            app.Tap(x => x.Marked("DrinkType"));
            app.Tap(x => x.Text("Americano"));
            app.Tap(x => x.Marked("Tamered"));
            app.Screenshot("Tapped on view with class: SwitchCompat with marked: Tamered");
        }

        [Test]
        public void LatteTest()
        {
            app.Tap(x => x.Marked("SubTotal"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "5");
            app.Tap(x => x.Marked("DrinkType"));
            app.Tap(x => x.Text("Latte"));
            app.Tap(x => x.Marked("Tamered"));
            app.WaitForElement(x => x.Marked("Total"));
            app.Screenshot("Latte, Tampered");

            var total = app.Query("Total").First();
            var tip = app.Query("TipAmount").First();

            //Assert.AreEqual(total.Text, "Total: $4.50");
            //Assert.AreEqual(tip.Text, "Tip: $0.50");
        }
    }
}

