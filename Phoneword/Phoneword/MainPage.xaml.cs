using System;
using System.Collections.Generic;
using System.Diagnostics; // So we can do Debug.Writeline
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Phoneword
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        // This value is accessed in XAML using x:staticextension
        public const double MyBorderWidth = 2;

        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
            Debug.WriteLine("In MainPage...");
        }

        /// <summary>
        /// Handles the user pressing the call button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void OnCall(object sender, EventArgs e)
        {
            Debug.WriteLine("In OnCall method..");
            // Have OK and Cancel buttons
            if (await this.DisplayAlert("Dial a number?", "Do you want to call " + translatedNumber + "?", "Yes", "No"))
            {
                //  Dial the phone number
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
                }
                catch (Exception)
                {
                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }

        /// <summary>
        /// Handles the user clicking the Translate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTranslate(object sender, EventArgs e)
        {
            Debug.WriteLine("In Ontranslate method..");
            // Get the phonenumber from the entry field
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                // We have a translated number
                // Enable the call button
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                // We got nothing back for the translated number so disable the button
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }
    }
}